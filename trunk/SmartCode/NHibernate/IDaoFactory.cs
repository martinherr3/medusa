using System;
using System.Collections.Generic;
using System.Text;
using SmartCode.Template;
using SmartCode.Model;

namespace NHibernateTemplates
{
    public class IDaoFactory : TemplateBase
    {
        public IDaoFactory()
        {
            CreateOutputFile = true;
            IsProjectTemplate = true;
            Description = "Provides an interface for retrieving DAO objects";
            Name = "IDaoFactory";
            OutputFolder = "NHibernate/DataInterfaces";
        }

        public override string OutputFileName()
        {
            return "IDaoFactory.cs";
        }

        public override void ProduceCode()
        {
            WriteLine(@"using System;");
            WriteLine(@"using System.Collections.Generic;");
            W();
            WriteLine(@"namespace {0}.Core.DataInterfaces", Helper.PascalCase(Domain.Code));
            WriteLine(@"{");
            WriteLine(@"    public interface IDaoFactory");
            WriteLine(@"    {");
            foreach (TableSchema table in Domain.DatabaseSchema.Tables)
            {
                if (table.HasPrimaryKey())
                {
                    WriteLine(@"        I{0}Dao Get{0}Dao();", Helper.ClassName (table.Code));
                }
            }
            WriteLine(@"    }");
            WriteLine(@"}");

        }
    }
}
