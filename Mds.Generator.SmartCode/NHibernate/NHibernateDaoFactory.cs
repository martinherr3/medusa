using System;
using System.Collections.Generic;
using System.Text;
using Kontac.Net.SmartCode.Model.Templates;
using Kontac.Net.SmartCode.Model;

namespace NHibernateTemplates
{
    public class NHibernateDaoFactory : ProjectTemplate
    {
        public NHibernateDaoFactory()
        {
            CreateOutputFile = true; 
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
            WriteLine(@"using Mds.Architecture.Data;");
            WriteLine(@"using {0}.Core.DataInterfaces;", Helper.PascalCase(Project.Code));
            W();
            WriteLine(@"namespace {0}.Data", Helper.PascalCase(Project.Code));
            WriteLine(@"{");
            WriteLine(@"    public class NHibernateDaoFactory : IDaoFactory");
            WriteLine(@"    {");
            foreach (TableSchema table in Project.DatabaseSchema.Tables)
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
