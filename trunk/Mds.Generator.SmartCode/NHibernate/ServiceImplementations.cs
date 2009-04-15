using System;
using System.Collections.Generic;
using System.Text;
using Kontac.Net.SmartCode.Model.Templates;
using NHibernateTemplates;
using Kontac.Net.SmartCode.Model;

namespace Medusa.NHibernateTemplates
{
    public class ServiceImplementations : EntityTemplate
    {
        public ServiceImplementations()
        {
            CreateOutputFile = true;
            Description = "Generates a C# class for the Service Implementation";
            Name = "ServiceImplementations";
            OutputFolder = "Services/ServiceImplementations";
        }

        public override string OutputFileName()
        {
            return String.Format("{0}Service.cs", Helper.MakeSingle(Entity.Code));
        }

        public override void ProduceCode()
        {
            IList<ColumnSchema> primaryKeyColumns = Table.PrimaryKeyColumns();

            if (primaryKeyColumns.Count > 0)
            {
                WriteLine(@"using System;");
                WriteLine(@"using System.Collections.Generic;");
                WriteLine(@"using System.Text;");
                WriteLine(@"using System.Runtime.Serialization;");
                WriteLine(@"using System.Collections.ObjectModel;");
                WriteLine(@"using log4net.Config;");
                WriteLine(@"using {0}.Service.Contract;", Helper.PascalCase(Project.Code));
                WriteLine(@"using {0}.Core.DataInterfaces;", Helper.PascalCase(Project.Code));
                WriteLine(@"using {0}.Domain;", Helper.PascalCase(Project.Code));
                W();
                WriteLine(@"namespace {0}.Service.Impl", Helper.PascalCase(Project.Code) );
                WriteLine(@"{");
                WriteLine(@"    /// <summary>");
                WriteLine(@"    /// Implementacion service para la capa de negocio");
                WriteLine(@"    /// </summary>");

                WriteLine(@"    public class {0}Service : ServiceBase, I{0}Service", Helper.MakeSingle(Entity.Code));
                WriteLine(@"    {");

                WriteLine(@"        private I{0}Dao _dao;", Helper.MakeSingle(Entity.Code));
                WriteLine(@"        private {0} _entity;", Helper.MakeSingle(Entity.Code));
                WriteLine(@"");
                WriteLine(@"        public {0}Service()", Helper.MakeSingle(Entity.Code));
                WriteLine(@"        {");
                WriteLine(@"            _dao = DaoFactory.Get{0}Dao();", Helper.MakeSingle(Entity.Code));
                WriteLine(@"        }");
                WriteLine(@"");
                WriteLine(@"        #region I{0}Service Members", Helper.MakeSingle(Entity.Code));
                WriteLine(@"");
                WriteLine(@"        /// </summary>");
                WriteLine(@"        /// Obtiene un {0} por id", Helper.MakeSingle(Entity.Code));
                WriteLine(@"        /// </summary>");
                WriteLine(@"        /// <param name=id></param>");
                WriteLine(@"        /// <returns></returns>");
                WriteLine(@"        public {0} GetById(int id)", Helper.MakeSingle(Entity.Code));
                WriteLine(@"        {");
                WriteLine(@"            try");
                WriteLine(@"            {");
                WriteLine(@"                _entity = _dao.GetById(id, false);", Helper.MakeSingle(Entity.Code));
                WriteLine(@"                return _entity;");
                WriteLine(@"            }");
                WriteLine(@"            catch (Exception e)");
                WriteLine(@"            {");
                WriteLine(@"                Log.Error(e.Message, e);");
                WriteLine(@"                throw RaiseFault(e)");
                WriteLine(@"            }");
                WriteLine(@"        }");
                WriteLine(@"");
                WriteLine(@"        /// </summary>");
                WriteLine(@"        /// Obtiene un {0} por id y bloquea el registro", Helper.MakeSingle(Entity.Code));
                WriteLine(@"        /// </summary>");
                WriteLine(@"        /// <param name=id></param>");
                WriteLine(@"        /// <param name=shouldLock></param>");
                WriteLine(@"        /// <returns></returns>");
                WriteLine(@"        public {0} GetByIdLock(int id, bool shouldLock)", Helper.MakeSingle(Entity.Code));
                WriteLine(@"        {");
                WriteLine(@"            try");
                WriteLine(@"            {");
                WriteLine(@"                _entity = _dao.GetById(id, false);", Helper.MakeSingle(Entity.Code));
                WriteLine(@"                return _entity;");
                WriteLine(@"            }");
                WriteLine(@"            catch (Exception e)");
                WriteLine(@"            {");
                WriteLine(@"                Log.Error(e.Message, e);");
                WriteLine(@"                throw RaiseFault(e)");
                WriteLine(@"            }");
                WriteLine(@"        }");
                WriteLine(@"");
                WriteLine(@"        /// </summary>");
                WriteLine(@"        /// Obtiene todos los registros de la entidad {0}", Helper.MakeSingle(Entity.Code));
                WriteLine(@"        /// </summary>");
                WriteLine(@"        /// <returns></returns>");
                WriteLine(@"        public IList<{0}> GetAll()", Helper.MakeSingle(Entity.Code));
                WriteLine(@"        {");
                WriteLine(@"            try");
                WriteLine(@"            {");
                WriteLine(@"                IList<{0}> list = new List<{0}>();", Helper.MakeSingle(Entity.Code));
                WriteLine(@"                list = _dao.GetAll();");
                WriteLine(@"                return list;");
                WriteLine(@"            }");
                WriteLine(@"            catch (Exception e)");
                WriteLine(@"            {");
                WriteLine(@"                Log.Error(e.Message, e);");
                WriteLine(@"                throw RaiseFault(e)");
                WriteLine(@"            }");
                WriteLine(@"        }");
                WriteLine(@"");
                WriteLine(@"        /// </summary>");
                WriteLine(@"        /// Inserta un elemento de la entidad {0} en la base de datos", Helper.MakeSingle(Entity.Code));
                WriteLine(@"        /// </summary>");
                WriteLine(@"        /// <param name=entity></param>");
                WriteLine(@"        /// <returns></returns>");
                WriteLine(@"        public {0} Insert({0} entity)", Helper.MakeSingle(Entity.Code));
                WriteLine(@"        {");
                WriteLine(@"            try");
                WriteLine(@"            {");
                WriteLine(@"                return _dao.Save(entity);");
                WriteLine(@"            }");
                WriteLine(@"            catch (Exception e)");
                WriteLine(@"            {");
                WriteLine(@"                Log.Error(e.Message, e);");
                WriteLine(@"                throw RaiseFault(e)");
                WriteLine(@"            }");
                WriteLine(@"        }");
                WriteLine(@"");
                WriteLine(@"        /// </summary>");
                WriteLine(@"        /// Borra un {0} de la Base de Datos", Helper.MakeSingle(Entity.Code));
                WriteLine(@"        /// </summary>");
                WriteLine(@"        /// <param name=entity></param>");
                WriteLine(@"        public void Delete({0} entity)", Helper.MakeSingle(Entity.Code));
                WriteLine(@"        {");
                WriteLine(@"            try");
                WriteLine(@"            {");
                WriteLine(@"                _dao.Delete(entity);");
                WriteLine(@"                _dao.CommitChanges();");
                WriteLine(@"            }");
                WriteLine(@"            catch (Exception e)");
                WriteLine(@"            {");
                WriteLine(@"                Log.Error(e.Message, e);");
                WriteLine(@"                throw RaiseFault(e)");
                WriteLine(@"            }");
                WriteLine(@"        }");
                WriteLine(@"");
                WriteLine(@"        /// </summary>");
                WriteLine(@"        /// Actualiza un {0}", Helper.MakeSingle(Entity.Code));
                WriteLine(@"        /// </summary>");
                WriteLine(@"        /// <param name=entity></param>");
                WriteLine(@"        /// <returns></returns>");
                WriteLine(@"        public {0} Update({0} entity)", Helper.MakeSingle(Entity.Code));
                WriteLine(@"        {");
                WriteLine(@"            try");
                WriteLine(@"            {");
                WriteLine(@"                _entity = _dao.SaveOrUpdate(entity);");
                WriteLine(@"                _dao.CommitChanges();");
                WriteLine(@"                return _entity;");
                WriteLine(@"            }");
                WriteLine(@"            catch (Exception e)");
                WriteLine(@"            {");
                WriteLine(@"                Log.Error(e.Message, e);");
                WriteLine(@"                throw RaiseFault(e)");
                WriteLine(@"            }");
                WriteLine(@"        }");
                WriteLine(@"");
                WriteLine(@"        /// </summary>");
                WriteLine(@"        /// Encuentra {0} en base a un criterio", Helper.MakeSingle(Entity.Code));
                WriteLine(@"        /// </summary>");
                WriteLine(@"        /// <param name=criteria></param>");
                WriteLine(@"        /// <returns></returns>");
                WriteLine(@"        public IList<{0}> Find(string criteria)", Helper.MakeSingle(Entity.Code));
                WriteLine(@"        {");
                WriteLine(@"            try");
                WriteLine(@"            {");
                WriteLine(@"                IList<{0}> list = new List<{0}>();", Helper.MakeSingle(Entity.Code));
                WriteLine(@"                list = _dao.Find(criteria);");
                WriteLine(@"                return list;");
                WriteLine(@"            }");
                WriteLine(@"            catch (Exception e)");
                WriteLine(@"            {");
                WriteLine(@"                Log.Error(e.Message, e);");
                WriteLine(@"                throw RaiseFault(e)");
                WriteLine(@"            }");
                WriteLine(@"        }");
                WriteLine(@"");

                WriteLine(@"        #endregion");

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

