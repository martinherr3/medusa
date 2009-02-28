using System;
using System.Collections.Generic;
using System.Text;
using SmartCode.Template;
using SmartCode.Model;

namespace NHibernateTemplates
{
    public class NHibernateDaoFactory : TemplateBase
    {
        public NHibernateDaoFactory()
        {
            CreateOutputFile = true;
            IsProjectTemplate = true;
            Description = "Exposes access to NHibernate DAO classes";
            Name = "NHibernateDaoFactory";
            OutputFolder = "Data";
        }

        public override string OutputFileName()
        {
            return "NHibernateDaoFactory.cs";
        }

        public override void ProduceCode()
        {
            WriteLine(@"using System;");
            WriteLine(@"using System.Collections.Generic;");
            WriteLine(@"using {0}.Core.DataInterfaces;", Helper.PascalCase(Domain.Code));
            W();
            WriteLine(@"namespace {0}.Data", Helper.PascalCase(Domain.Code));
            WriteLine(@"{");
            WriteLine(@"    public class NHibernateDaoFactory : IDaoFactory");
            WriteLine(@"    {");
            foreach (TableSchema table in Domain.DatabaseSchema.Tables)
            {
                WriteLine(@"        public I{0}Dao Get{0}Dao()", Helper.MakeSingle(table.Code));
                WriteLine(@"        {");
                WriteLine(@"            return new {0}Dao();", Helper.MakeSingle(table.Code));
                WriteLine(@"        }");
            }
            WriteLine(@"    }");
            WriteLine(@"}");

        }
    }
}
