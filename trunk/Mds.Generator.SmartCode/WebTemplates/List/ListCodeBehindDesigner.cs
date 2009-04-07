using System;
using System.Collections.Generic;
using System.Text;
using Kontac.Net.SmartCode.Model.Templates;
using Kontac.Net.SmartCode.Model;

namespace WebTemplates.List
{
    public class ListCodeBehindDesigner : EntityTemplate
    {
        public ListCodeBehindDesigner()
        {
            CreateOutputFile = true;
            Description = "Generates the Code behind designer List for the entity";
            Name = "ListCodeBehindDesigner";
            OutputFolder = "Web";
        }

        public override string OutputFileName()
        {
            return Helper.ClassName(Entity.Code) + "List.aspx.designer.cs";
        }

        public override void ProduceCode()
        {
            IList<ColumnSchema> primaryKeyColumns = Table.PrimaryKeyColumns();
            if (primaryKeyColumns.Count > 0)
            {
 
                WriteLine(@"namespace {0}.Web", Helper.PascalCase(Project.Code));
                WriteLine(@"{");
                WriteLine(@"    public partial class {0}List", Helper.ClassName(Entity.Code));
                WriteLine(@"    {");
                WriteLine(@"        protected System.Web.UI.HtmlControls.HtmlForm {0}Form;", Helper.ClassName(Entity.Code));
                WriteLine(@"        protected System.Web.UI.WebControls.DataGrid gridData;");
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
