using System;
using System.Collections.Generic;
using System.Text;
using SmartCode.Template;
using SmartCode.Model;

namespace WebTemplates.List
{
    public class EditCodeBehind : TemplateBase
    {
        public EditCodeBehind()
        {
            CreateOutputFile = true;
            Description = "Generates the Code behind edit for the entity";
            Name = "EditCodeBehind";
            OutputFolder = "Web";
        }

        public override string OutputFileName()
        {
            return Helper.ClassName(Entity.Code) + "Edit.aspx.cs";
        }

        public override void ProduceCode()
        {
            IList<ColumnSchema> primaryKeyColumns = Table.PrimaryKeyColumns();
            if (primaryKeyColumns.Count > 0)
            {
                string className = Helper.ClassName(Entity.Code);

                WriteLine(@"using System;");
                WriteLine(@"using System.Data;");
                WriteLine(@"using System.Configuration;");
                WriteLine(@"using System.Collections;");
                WriteLine(@"using System.Web;");
                WriteLine(@"using System.Web.Security;");
                WriteLine(@"using System.Web.UI;");
                WriteLine(@"using System.Web.UI.WebControls;");
                WriteLine(@"using System.Web.UI.WebControls.WebParts;");
                WriteLine(@"using System.Web.UI.HtmlControls;");
                WriteLine(@"using {0}.Core.DataInterfaces;", Helper.PascalCase(Domain.Code));
                WriteLine(@"using {0}.Data;", Helper.PascalCase(Domain.Code));
                WriteLine(@"using {0}.Core.Domain;", Helper.PascalCase(Domain.Code));
                WriteLine(@"");
                WriteLine(@"namespace {0}.Web", Helper.PascalCase(Domain.Code));
                WriteLine(@"{");
                WriteLine(@"    public partial class {0}Edit : System.Web.UI.Page", className);
                WriteLine(@"    {");
                WriteLine(@"        protected void Page_Load(object sender, EventArgs e)");
                WriteLine(@"        {");
                WriteLine(@"            if (!Page.IsPostBack)");
                WriteLine(@"            {");
                WriteLine(@"                fillComboBoxes();");
                WriteLine(@"                fillForm();");
                WriteLine(@"            }");
                WriteLine(@"        }");
                WriteLine(@"");
                W();
                WriteLine(@"        private void fillComboBoxes()");
                WriteLine(@"        {");
                WriteLine(@"            IDaoFactory daoFactory = new NHibernateDaoFactory();");

                foreach (ReferenceSchema inReference in Table.InReferences)
                {
                    foreach (ReferenceJoin join in inReference.Joins)
                    {
                        ColumnSchema column = join.ChildColumn;

                        if (column.Control is SmartCode.Model.Profile.ComboBox)
                        {
                            WriteLine(@"            ui{0}.Items.Add("""");", column.Code);

                            WriteLine(@"            ui{0}.DataSource = daoFactory.Get{1}Dao().GetAll();", column.Code, Helper.ClassName(inReference.ParentTable.Code));
                            WriteLine(@"            ui{0}.DataValueField = ""ID"";", column.Code);
                            if (column.GetLOV().Count > 0)
                            {
                                WriteLine(@"            ui{0}.DataTextField  = ""{1}"";", column.Code, column.GetLOV()[0].Code );
                            }
                            else
                            {
                                WriteLine(@"            ui{0}.DataTextField  = ""ID"";", column.Code);
                            }
                            WriteLine(@"            ui{0}.DataBind();", column.Code);
                            W();
                        }
                    }
                }
                
                WriteLine(@"        }");
                W();

                WriteLine(@"        private void fillForm()");
                WriteLine(@"        {");

                string nullIfStatement = "";

                foreach (ColumnSchema column in primaryKeyColumns)
                {
                    if (nullIfStatement != "")
                    {
                        nullIfStatement += " && ";
                    }
                    nullIfStatement += String.Format(@"Request.QueryString[""{0}""] != null ", column.Code);
                }
                WriteLine(@"            if ( {0} )", nullIfStatement);
                WriteLine(@"            {");

                string domainObjectIDArgs = "";

                if (primaryKeyColumns.Count == 1)
                {
                    WriteLine(@"                {0} ID = {1};", primaryKeyColumns[0].NetDataType, Helper.GetConvertToNETType(primaryKeyColumns[0], "ui" + primaryKeyColumns[0].Code + "." + Helper.GetValueOrText(primaryKeyColumns[0])));
                }
                else
                {
                    foreach (ColumnSchema column in primaryKeyColumns)
                    {
                        if (domainObjectIDArgs != "")
                        {
                            domainObjectIDArgs += ", ";
                        }
                        domainObjectIDArgs += Helper.GetConvertToNETType(column, "ui" + column.Code + "." + Helper.GetValueOrText(column));
                    }
                }
              
                W();

                foreach (ColumnSchema column in primaryKeyColumns)
                {
                    WriteLine(@"                ui{0}.{1} = Request.QueryString[""{0}""];", column.Code, Helper.GetValueOrText(column));
                }
                if (primaryKeyColumns.Count > 1)
                {
                    WriteLine("             {0}.DomainObjectID ID = new {0}.DomainObjectID({1});", className, domainObjectIDArgs);
                }
               
                WriteLine(@"                IDaoFactory daoFactory = new NHibernateDaoFactory();");
                WriteLine(@"                I{0}Dao dao = daoFactory.Get{0}Dao();", className);
                WriteLine(@"                {0} entity = dao.GetById(ID, false );", className);
                W();
                foreach (ColumnSchema column in Table.NoPrimaryKeyColumns())
                {
                    switch (column.NetDataType)
                    {
                        case "System.String":
                            WriteLine(@"                ui{0}.{1} = entity.{0};", column.Code, Helper.GetValueOrText(column));
                            break;
                        case "System.Byte[]":
                            break;
                        default:
                            if (column.Control is SmartCode.Model.Profile.CheckBox)
                            {
                                WriteLine(@"                ui{0}.Checked = entity.{0};", column.Code);
                            }
                            else
                            {
                                WriteLine(@"                ui{0}.{1} = entity.{0}.ToString();", column.Code, Helper.GetValueOrText(column));
                            }
                            break;
                    }
                }
                WriteLine(@"                uiIsNew.Checked = false;");
                WriteLine(@"            }");
                WriteLine(@"        }");
                W();

                WriteLine(@"        protected void Update(object sender, System.EventArgs e)");
                WriteLine(@"        {");
                WriteLine(@"            {0} entity = null;", className);
                W();
                WriteLine(@"            IDaoFactory daoFactory = new NHibernateDaoFactory();");
                WriteLine(@"            I{0}Dao dao = daoFactory.Get{0}Dao();", className);
                W();
                if (primaryKeyColumns.Count > 1)
                {
                    WriteLine("         {0}.DomainObjectID ID = new {0}.DomainObjectID({1});", className, domainObjectIDArgs);
                }
                else
                {
                    WriteLine(@"            {0} ID = {1};", primaryKeyColumns[0].NetDataType, Helper.GetConvertToNETType(primaryKeyColumns[0], "ui" + primaryKeyColumns[0].Code + "." + Helper.GetValueOrText(primaryKeyColumns[0])));
                }
              

                WriteLine(@"            if (! uiIsNew.Checked )");
                WriteLine(@"            {");                
                WriteLine(@"                entity = dao.GetById(ID, false );", className);

                WriteLine(@"                }");
                WriteLine(@"                else");
                WriteLine(@"                {");
                WriteLine(@"                    entity = new {0}(ID);", className);
                WriteLine(@"                }");
                W();
                foreach (ColumnSchema column in Table.NoPrimaryKeyColumns ())
                {
                    if (!column.IsIdentity)
                    {
                        switch (column.NetDataType)
                        {
                            case "System.String":
                                WriteLine(@"            entity.{0} = ui{0}.{1};", column.Code, Helper.GetValueOrText(column));
                                break;
                            case "System.Byte[]":
                                break;
                            default:
                                if (column.Control is SmartCode.Model.Profile.CheckBox)
                                {
                                    WriteLine(@"            entity.{0} = ui{0}.Checked;", column.Code);
                                }
                                else
                                {
                                    WriteLine(@"            entity.{0} = {1};", column.Code, Helper.GetConvertToNETType(column, "ui" + column.Code + "." + Helper.GetValueOrText(column)));
                                }
                                break;
                        }
                    }
                    
                }
                W();

                WriteLine(@"            if (uiIsNew.Checked)");
                WriteLine(@"            {");
                WriteLine(@"                dao.Save(entity);");
                foreach (ColumnSchema column in Table.Columns())
                {
                    if (column.IsIdentity)
                    {
                        if (column.IsPrimaryKey)
                            WriteLine(@"                ui{0}.Value = Convert.ToString(entity.ID);", column.Code);
                        else
                            WriteLine(@"                ui{0}.Value = Convert.ToString(entity.{0});", column.Code);
                    }
                }
                WriteLine(@"            }");
                WriteLine(@"            else");
                WriteLine(@"            {");
                WriteLine(@"                dao.SaveOrUpdate(entity);");
                WriteLine(@"            }");
                WriteLine(@"            uiIsNew.Checked = false;");

                WriteLine(@"        }");
                W();

                WriteLine(@"        protected void Delete(object sender, System.EventArgs e)");
                WriteLine(@"        {");
                WriteLine(@"            if (! uiIsNew.Checked)");
                WriteLine(@"            {");
                if (primaryKeyColumns.Count > 1)
                {
                    WriteLine(@"                {0}.DomainObjectID ID = new {0}.DomainObjectID({1});", className, domainObjectIDArgs);
                }
                else
                {
                    WriteLine(@"                {0} ID = {1};", primaryKeyColumns[0].NetDataType, Helper.GetConvertToNETType(primaryKeyColumns[0], "ui" + primaryKeyColumns[0].Code + "." + Helper.GetValueOrText(primaryKeyColumns[0])));
                }
                WriteLine(@"                IDaoFactory daoFactory = new NHibernateDaoFactory();");
                WriteLine(@"                I{0}Dao dao = daoFactory.Get{0}Dao();", className);

                WriteLine(@"                {0} entity = dao.GetById(ID, false );", className);
                W();

                WriteLine(@"                dao.Delete(entity);");
                W();

                WriteLine(@"                Response.Redirect(""{0}List.aspx"");", className);
                WriteLine(@"            }");
                WriteLine(@"        }");
                W();
                
                WriteLine(@"");
                WriteLine(@"    }");
                WriteLine(@"}");


            }
            else
            {
                WriteLine("//-- Entity " + Entity.Name + " has no primary key information.");
            }

        }
    }
}
