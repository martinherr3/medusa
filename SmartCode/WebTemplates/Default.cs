using System;
using System.Collections.Generic;
using System.Text;
using SmartCode.Template;
using SmartCode.Model;

namespace WebTemplates
{
    public class Default : TemplateBase
    {
        public Default()
        {
            CreateOutputFile = true;
            IsProjectTemplate = true;
            Description = "Generates the Default HTML Page";
            Name = "Default";
            OutputFolder = "Web";
        }

        public override string OutputFileName()
        {
            return "Default.html";
        }

        public override void ProduceCode()
        {

            foreach (TableSchema table in Domain.DatabaseSchema.Tables)
            {
                if (table.HasPrimaryKey())
                {
                    WriteLine(@"        <p><a href='{0}List.aspx'>{0} List</a></p>", Helper.ClassName(table.Code));
                }
            }
        }
    }
}
