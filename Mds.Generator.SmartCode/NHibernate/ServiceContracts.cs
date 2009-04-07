using System;
using System.Collections.Generic;
using System.Text;
using Kontac.Net.SmartCode.Model;
using Kontac.Net.SmartCode.Model.Templates;
using NHibernateTemplates;

namespace Medusa.NHibernateTemplates
{
    public class ServiceContracts : EntityTemplate
    {
        public ServiceContracts()
        {
            CreateOutputFile = true;
            Description = "Generates a C# class for the Service Interface (Contract)";
            Name = "ServiceContracts";
            OutputFolder = "Services/ServiceContracts";
        }

        public override string OutputFileName()
        {
            return String.Format("I{0}Service.cs", Helper.MakeSingle(Entity.Code));
        }

        public override void ProduceCode()
        {
            IList<ColumnSchema> primaryKeyColumns = Table.PrimaryKeyColumns();

            if (primaryKeyColumns.Count > 0)
            {
                WriteLine(@"using System;");
                WriteLine(@"using System.Collections.Generic;");
                WriteLine(@"using System.Text;");
                WriteLine(@"using System.ServiceModel;");
                WriteLine(@"using System.Runtime.Serialization;");
                WriteLine(@"using {0}.Domain;", Helper.PascalCase(Project.Code));
                W();
                WriteLine(@"namespace {0}.Service.Contract", Helper.PascalCase(Project.Code) );
                WriteLine(@"{");
                WriteLine(@"    /// <summary>");
                WriteLine(@"    /// Interfaz service preparada para WCF ");
                WriteLine(@"    /// </summary>");

                WriteLine(@"    [ServiceContract]");
                WriteLine(@"    public interface I{0}Service", Helper.MakeSingle(Entity.Code));
                WriteLine(@"    {");
                if (primaryKeyColumns.Count > 1)
                {
                    WriteLine(@"        [OperationContract]");
                    WriteLine(@"        [UseNHibernateDataContractSerializer]");
                    WriteLine(@"        {0} GetById({1}.DomainObjectID id);", primaryKeyColumns[0].NetDataType, Helper.MakeSingle(Entity.Code));
                    WriteLine(@"");
                    WriteLine(@"        [OperationContract]");
                    WriteLine(@"        [UseNHibernateDataContractSerializer]");
                    WriteLine(@"        {0} GetByIdLock({1}.DomainObjectID id, bool shouldLock);", primaryKeyColumns[0].NetDataType, Helper.MakeSingle(Entity.Code));
                    WriteLine(@"");
                }
                else
                {
                    WriteLine(@"        [OperationContract]");
                    WriteLine(@"        [UseNHibernateDataContractSerializer]");
                    WriteLine(@"        {0} GetById({1} id);", Helper.MakeSingle(Entity.Code), primaryKeyColumns[0].NetDataType);
                    WriteLine(@"");
                    WriteLine(@"        [OperationContract]");
                    WriteLine(@"        [UseNHibernateDataContractSerializer]");
                    WriteLine(@"        {0} GetByIdLock({1} id, bool shouldLock);", Helper.MakeSingle(Entity.Code), primaryKeyColumns[0].NetDataType);
                    WriteLine(@"");

                }

                WriteLine(@"        [OperationContract]");
                WriteLine(@"        [UseNHibernateDataContractSerializer]");
                WriteLine(@"        IList<{0}> GetAll();", Helper.MakeSingle(Entity.Code));
                WriteLine(@"");

                WriteLine(@"        [OperationContract]");
                WriteLine(@"        [UseNHibernateDataContractSerializer]");
                WriteLine(@"        IList<{0}> Find(String criteria);", Helper.MakeSingle(Entity.Code));
                WriteLine(@"");

                WriteLine(@"        [OperationContract]");
                WriteLine(@"        [UseNHibernateDataContractSerializer]");
                WriteLine(@"        {0} Insert({0} entity);", Helper.MakeSingle(Entity.Code));
                WriteLine(@"");

                WriteLine(@"        [OperationContract]");
                WriteLine(@"        [UseNHibernateDataContractSerializer]");
                WriteLine(@"        void Delete({0} entity);", Helper.MakeSingle(Entity.Code));
                WriteLine(@"");

                WriteLine(@"        [OperationContract]");
                WriteLine(@"        [UseNHibernateDataContractSerializer]");
                WriteLine(@"        {0} Update({0} entity);", Helper.MakeSingle(Entity.Code));
                WriteLine(@"");

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
