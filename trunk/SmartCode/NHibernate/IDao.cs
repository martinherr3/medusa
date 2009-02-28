using System;
using System.Collections.Generic;
using System.Text;
using SmartCode.Template;
using SmartCode.Model;

namespace NHibernateTemplates
{
    public class IDao : TemplateBase
    {
        public IDao()
        {
            CreateOutputFile = true;
            IsProjectTemplate = true;
            Description = "Generates the IDao Interface";
            Name = "IDao";
            OutputFolder = "NHibernate/DataInterfaces";
        }

        public override string OutputFileName()
        {
            return "IDao.cs";
        }

        public override void ProduceCode()
        {
            WriteLine(@"using System;");
            WriteLine(@"using System.Collections.Generic;");
            W();
            WriteLine(@"namespace {0}.Architecture.DataInterfaces", Helper.PascalCase(Domain.Code));

            WriteLine(@"{");
            WriteLine(@"    public interface IDao<T, IdT>");
            WriteLine(@"    {");
            WriteLine(@"        T GetById(IdT id, bool shouldLock);");
            WriteLine(@"        List<T> GetAll();");
            WriteLine(@"        List<T> GetByExample(T exampleInstance, params string[] propertiesToExclude);");
            WriteLine(@"        T GetUniqueByExample(T exampleInstance, params string[] propertiesToExclude);");
            WriteLine(@"        T Save(T entity);");
            WriteLine(@"        T SaveOrUpdate(T entity);");
            WriteLine(@"        void Delete(T entity);");
            WriteLine(@"        void CommitChanges();");
            WriteLine(@"    }");
            WriteLine(@"}");

        }
    }
}
