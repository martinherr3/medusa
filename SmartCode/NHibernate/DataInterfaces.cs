using System;
using System.Collections.Generic;
using System.Text;
using SmartCode.Template;
using SmartCode.Model;

namespace NHibernateTemplates
{
    public class DataInterfaces: TemplateBase
    {
        public DataInterfaces()
        {
            CreateOutputFile = true;
            Description = "Generates a C# class for the Data Interface";
            Name = "DataInterfaces";
            OutputFolder = "NHibernate/DataInterfaces";
        }

        public override string OutputFileName()
        {
            return String.Format("I{0}Dao.cs", Helper.MakeSingle(Entity.Code));
        }

        public override void ProduceCode()
        {
            IList<ColumnSchema> primaryKeyColumns = Table.PrimaryKeyColumns();

            if (primaryKeyColumns.Count > 0)
            {
                WriteLine(@"using System;");
                WriteLine(@"using System.Collections.Generic;");
                WriteLine(@"using Mds.Architecture.Domain;", Helper.PascalCase(Domain.Code));
                WriteLine(@"using Mds.Architecture.Data;", Helper.PascalCase(Domain.Code));
                WriteLine(@"using {0}.Domain;", Helper.PascalCase(Domain.Code));
                W();
                WriteLine(@"namespace {0}.Core.DataInterfaces", Helper.PascalCase(Domain.Code) );
                WriteLine(@"{");
                WriteLine(@"    /// <summary>");
                WriteLine(@"    /// Since this extends the <see cref=""IDao{TypeOfListItem, IdT}"" /> behavior, it's a good idea to ");
                WriteLine(@"    /// place it in its own file for manageability.  In this way, it can grow further without");
                WriteLine(@"    /// cluttering up <see cref=""IDaoFactory"" />.");
                WriteLine(@"    /// </summary>");
                if (primaryKeyColumns.Count > 1)
                {
                    WriteLine(@"    public interface I{0}Dao : IDao<{0}, {0}.DomainObjectID>", Helper.MakeSingle(Entity.Code));
                }
                else
                {
                    WriteLine(@"    public interface I{0}Dao : IDao<{0}, {1}>", Helper.MakeSingle(Entity.Code), primaryKeyColumns[0].NetDataType );
                }
                WriteLine(@"    {");
                WriteLine(@"");
                WriteLine(@"    }");
                WriteLine(@"}");
            }
            else
            {
                WriteLine(@"//-- Entity " + Entity.Name + " does not have a primary key.");
            }   
        }
    }
}
