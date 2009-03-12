using System;
using System.Collections.Generic;
using System.Text;
using Kontac.Net.SmartCode.Model.Templates;
using Kontac.Net.SmartCode.Model;

namespace NHibernateTemplates
{
    public class Dao : EntityTemplate
    {
        public Dao()
        {
            CreateOutputFile = true;
            Description = "Generates the Concrete DAO for accessing instances from DB";
            Name = "Dao";
            OutputFolder = "Data";
        }

        public override string OutputFileName()
        {
            return String.Format("{0}Dao.cs", Helper.MakeSingle(Entity.Code));
        }

        public override void ProduceCode()
        {
            IList<ColumnSchema> primaryKeyColumns = Table.PrimaryKeyColumns();

            if (primaryKeyColumns.Count > 0)
            {
                WriteLine(@"using System;");
                WriteLine(@"using System.Collections.Generic;");
                WriteLine(@"using Mds.Architecture.Data;", Helper.PascalCase(Project.Code));
                WriteLine(@"using {0}.Core.DataInterfaces;", Helper.PascalCase(Project.Code));
                WriteLine(@"using {0}.Domain;", Helper.PascalCase(Project.Code));
                W();
                WriteLine(@"namespace {0}.Data", Helper.PascalCase(Project.Code));

                WriteLine(@"{");
                if (primaryKeyColumns.Count == 1)
                    WriteLine(@"    public class {0}Dao : AbstractNHibernateDao<{0}, {1}>, I{0}Dao", Helper.MakeSingle(Entity.Code), primaryKeyColumns[0].NetDataType);
                else
                    WriteLine(@"    public class {0}Dao : AbstractNHibernateDao<{0}, {0}.DomainObjectID>, I{0}Dao", Helper.MakeSingle(Entity.Code));
                WriteLine(@"    {");

                WriteLine(@"        public {0}Dao(string sessionFactoryConfigPath)", Helper.MakeSingle(Entity.Code));
                WriteLine(@"            : base(sessionFactoryConfigPath)");
                WriteLine(@"        {");
                WriteLine(@"        }");

                WriteLine(@"    }");
                WriteLine(@"}");
            }
            else
            {
                WriteLine("//-- Entity " + Entity.Name + " does not have a primary key.");
            }  

        }
    }
}
