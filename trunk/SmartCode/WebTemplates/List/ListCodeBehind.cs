using System;
using System.Collections.Generic;
using System.Text;
using SmartCode.Template;
using SmartCode.Model;

namespace WebTemplates.List
{
    public class ListCodeBehind : TemplateBase
    {
        public ListCodeBehind()
        {
            CreateOutputFile = true;
            Description = "Generates the Code behind List for the entity";
            Name = "ListCodeBehind";
            OutputFolder = "Web";
        }

        public override string OutputFileName()
        {
            return Helper.ClassName(Entity.Code) + "List.aspx.cs";
        }

        public override void ProduceCode()
        {
            IList<ColumnSchema> primaryKeyColumns = Table.PrimaryKeyColumns();
            if (primaryKeyColumns.Count > 0)
            {
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
                WriteLine(@"");
                WriteLine(@"namespace {0}.Web", Helper.PascalCase(Domain.Code));
                WriteLine(@"{");
                WriteLine(@"    public partial class {0}List : System.Web.UI.Page", Helper.ClassName(Entity.Code));
                WriteLine(@"    {");
                WriteLine(@"        protected void Page_Load(object sender, EventArgs e)");
                WriteLine(@"        {");
                WriteLine(@"            if (!Page.IsPostBack)");
                WriteLine(@"            {");
                WriteLine(@"                BindGrid();");
                WriteLine(@"            }");
                WriteLine(@"        }");
                WriteLine(@"");
                WriteLine(@"        private void BindGrid()");
                WriteLine(@"        {");
                WriteLine(@"            IDaoFactory daoFactory = new NHibernateDaoFactory();");
                WriteLine(@"            I{0}Dao dao = daoFactory.Get{0}Dao();", Helper.ClassName(Entity.Code));
                WriteLine(@"");
                WriteLine(@"            gridData.DataSource = dao.GetAll();");
                WriteLine(@"            gridData.DataBind();");

                WriteLine(@"        }");
                WriteLine(@"");
                WriteLine(@"        protected void ChangeGridPage(object obj, DataGridPageChangedEventArgs e)");
                WriteLine(@"        {");
                WriteLine(@"            gridData.CurrentPageIndex = e.NewPageIndex;");
                WriteLine(@"            BindGrid();");
                WriteLine(@"        }");
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
