using System;
using System.Collections.Generic;
using System.Text;
using SmartCode.Template;
using SmartCode.Model;

namespace WebTemplates.Edit
{
    public class EditCodeBehindDesigner : TemplateBase
    {
        public EditCodeBehindDesigner()
        {
            CreateOutputFile = true;
            Description = "Generates the Code behind designer Edit for the entity";
            Name = "EditCodeBehindDesigner";
            OutputFolder = "Web";
        }

        public override string OutputFileName()
        {
            return Helper.ClassName(Entity.Code) + "Edit.aspx.designer.cs";
        }

        public override void ProduceCode()
        {
            IList<ColumnSchema> primaryKeyColumns = Table.PrimaryKeyColumns();
            if (primaryKeyColumns.Count > 0)
            {
 
                WriteLine(@"namespace {0}.Web", Helper.PascalCase(Domain.Code));
                WriteLine(@"{");
                WriteLine(@"    public partial class {0}Edit", Helper.ClassName(Entity.Code));
                WriteLine(@"    {");
                WriteLine(@"        protected System.Web.UI.HtmlControls.HtmlForm form;");
                WriteLine(@"        protected System.Web.UI.HtmlControls.HtmlInputCheckBox uiIsNew;");
                foreach (ColumnSchema column in Table.Columns())
                {
                    if (column.IsIdentity)
                    {
                        WriteLine(@"        protected System.Web.UI.HtmlControls.HtmlInputHidden ui{0};", column.Code);
                    }
                }
                foreach (ColumnSchema column in Table.Columns())
                {
                    if (!column.IsIdentity && column.NetDataType != "System.Byte[]")
                    {
                        if (column.Control is SmartCode.Model.Profile.ComboBox)
                        {
                            WriteLine(@"        protected System.Web.UI.WebControls.DropDownList ui{0};", column.Code);
                        }
                        else if (column.Control is SmartCode.Model.Profile.CheckBox )
                        {
                            WriteLine(@"        protected System.Web.UI.WebControls.CheckBox ui{0};", column.Code);
                        }
                        else
                        {
                            WriteLine(@"        protected System.Web.UI.WebControls.TextBox ui{0};", column.Code);
                        }
                    }
                }
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
