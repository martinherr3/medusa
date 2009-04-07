
using System;
using System.Collections.Generic;
using System.Text;
using Kontac.Net.SmartCode.Model.Templates;
using Kontac.Net.SmartCode.Model;

namespace NHibernateTemplates
{
    public class NHibernateClass: EntityTemplate
    {
        public NHibernateClass()
        {
            CreateOutputFile = true;
            Description = "Generates a C# class for use with NHibernate";
            Name = "Class File";
            OutputFolder = "NHibernate/Project";
        }

        public override string OutputFileName()
        {
            return Helper.ClassName(Entity.Code) + ".cs";
        }

        public override void ProduceCode()
        {
            IList<ColumnSchema> primaryKeyColumns = Table.PrimaryKeyColumns();
 
            if (primaryKeyColumns.Count > 0)
            {
                WriteLine(@"using System;");
                WriteLine(@"using System.Collections.Generic;");
                WriteLine(@"using Mds.Architecture.Domain;");
                WriteLine(@"");
                WriteLine(@"namespace {0}.Domain", Helper.PascalCase(Project.Code));

                WriteLine(@"{");
                WriteLine(@"    /// <summary>");
                WriteLine(@"    /// {0} object for NHibernate mapped table {1}.", Helper.ClassName(Entity.Code), Entity.Name);
                WriteLine(@"    /// </summary>");

                WriteLine(@"    [Serializable]");
                if (primaryKeyColumns.Count > 1)
                {
                    WriteLine(@"    public class {0} : DomainObject<{0}.DomainObjectID>", Helper.ClassName(Entity.Code));
                }
                else
                {
                    WriteLine(@"    public class {0} : DomainObject<{1}>", Helper.ClassName(Entity.Code), primaryKeyColumns[0].NetDataType );
                } 
                WriteLine(@"    {");
                WriteLine(@"");

                if (primaryKeyColumns.Count > 1)
                {
                    WriteLine(@"        [Serializable]");
                    WriteLine(@"        public class DomainObjectID");
                    WriteLine(@"        {");
                    WriteLine(@"            public DomainObjectID() {}");
                    W();
                    string hashCode = "";
                    string constArguments = "";

                    foreach (ColumnSchema column in primaryKeyColumns)
                    {
                        WriteLine(@"            private {0} _{1};", column.NetDataType, Helper.PascalCase(column.Code));
                        if (hashCode != "")
                        {
                            hashCode += " ^ ";
                            constArguments += ", ";
                        }
                        hashCode += String.Format("{0}.GetHashCode()", Helper.PascalCase(column.Code));
                        constArguments += String.Format("{0} {1}", column.NetDataType, Helper.CamelCase(column.Code));
                    }
                    W();
                    //Agreguemos el constructor, sino perderemos horas y horas viendo por que no carga la joda esta

                    WriteLine(@"            public DomainObjectID(" + constArguments + ")");
                    WriteLine(@"            {");
                    foreach (ColumnSchema column in primaryKeyColumns)
                    {
                        WriteLine(@"                _{0} = {1};", Helper.PascalCase(column.Code), Helper.CamelCase(column.Code));
                    }
                    WriteLine(@"            }");

                    W();
                    foreach (ColumnSchema column in primaryKeyColumns)
                    {

                        WriteLine("         public " + column.NetDataType + " " + Helper.PascalCase(column.Code) + " {");
                        WriteLine("             get { return _" + Helper.PascalCase(column.Code) + "; }");
                        WriteLine("             protected set { _" + Helper.PascalCase(column.Code) + " = value;}");
                        WriteLine("         }");
                        W();
                    }
                    W();
                    WriteLine(@"            public override bool Equals(object obj)");
                    WriteLine(@"            {");
                    WriteLine(@"                if (obj == this) return true;");
                    WriteLine(@"                if (obj == null) return false;");
                    W();
                    WriteLine(@"                DomainObjectID that = obj as DomainObjectID;");
                    WriteLine(@"                if (that == null)");
                    WriteLine(@"                {");
                    WriteLine(@"                    return false;");
                    WriteLine(@"                }");
                    WriteLine(@"                else");
                    WriteLine(@"                {");
                    foreach (ColumnSchema column in primaryKeyColumns)
                    {
                        WriteLine(@"                    if (this.{0} != that.{0}) return false;", Helper.PascalCase(column.Code));
                    }
                    W();
                    WriteLine(@"                    return true;");
                    WriteLine(@"                }");
                    W();
                    WriteLine(@"            }");
                    W();
                    WriteLine(@"            public override int GetHashCode()");
                    WriteLine(@"            {");
                    WriteLine(@"                return {0};", hashCode);
                    WriteLine(@"            }");
                    W();
                    WriteLine(@"        }");
                }
                W();


                foreach (ColumnSchema column in Table.NoPrimaryKeyColumns())
                {
                    if (Helper.IsNullableType(column) && column.NetDataType != "System.Byte[]")
                        WriteLine(@"        private {0}? _{1};", column.NetDataType, Helper.PascalCase(column.Code));
                    else
                        WriteLine(@"        private {0} _{1};", column.NetDataType, Helper.PascalCase(column.Code));
                }

                foreach (ReferenceSchema inReference in Table.InReferences)
                {
                    foreach (ReferenceJoin join in inReference.Joins)
                    {
                        string propertyName = Helper.PascalCase(join.ChildColumn.Code + "Lookup");
                        WriteLine(@"        private {0} _{1};", Helper.ClassName(inReference.ParentTable.Code), propertyName);
                    }
                }

                foreach (ReferenceSchema outReference in Table.OutReferences)
                {
                    TableSchema childTable = outReference.ChildTable;

                    if (!Helper.IsManyToManyTable(childTable))
                    {
                        WriteLine(@"        private IList<{0}> _{1} = new List<{0}>();", Helper.ClassName(childTable.Code), Helper.MakePlural(childTable.Code));
                    }
                    else
                    {
                        ReferenceSchema parentSchema = childTable.InReferences[1];

                        if (parentSchema.ParentTable.ObjectID == Table.ObjectID)
                        {
                            parentSchema = childTable.InReferences[0];
                        }

                        WriteLine(@"        private IList<{0}> _{1} = new List<{0}>();", Helper.ClassName(childTable.Code), Helper.MakePlural(parentSchema.ParentTable.Code));
                    }
                }
                W();
                //Constructor
                WriteLine(@"        public {0}()", Helper.ClassName(Entity.Code));
                WriteLine(@"        {");          
                WriteLine(@"        }");
                W();
                if (primaryKeyColumns.Count > 1)
                {
                    WriteLine(@"        public {0}(DomainObjectID id)", Helper.ClassName(Entity.Code));
                }
                else
                {
                    WriteLine(@"        public {0}({1} id)", Helper.ClassName(Entity.Code), primaryKeyColumns[0].NetDataType);
                }
                WriteLine(@"        {");
                WriteLine(@"            base.ID = id;");
                WriteLine(@"        }");
                W();
                //Properties
                //Read-only PKs
                if (primaryKeyColumns.Count > 1)
                {
                    foreach (ColumnSchema column in primaryKeyColumns)
                    {
                        WriteLine("         public virtual " + column.NetDataType + " " + Helper.PascalCase(column.Code) + " {");
                        WriteLine("             get { return base.id." + Helper.PascalCase(column.Code) + "; }");
                        WriteLine("         }");
                        W();
                    }
                }

                foreach (ColumnSchema column in Table.NoPrimaryKeyColumns())
                {
                    if (Helper.IsNullableType(column) && column.NetDataType != "System.Byte[]")
                        WriteLine("         public virtual " + column.NetDataType + "? " + Helper.PascalCase(column.Code) + " {");
                    else
                        WriteLine("         public virtual " + column.NetDataType + " " + Helper.PascalCase(column.Code) + " {");
                    WriteLine("             get { return _" + Helper.PascalCase(column.Code) + "; }");
                    WriteLine("             set { _" + Helper.PascalCase(column.Code) + " = value;}");
                    WriteLine("         }");
                    W();
                }

                foreach (ReferenceSchema inReference in Table.InReferences)
                {
                    foreach (ReferenceJoin join in inReference.Joins)
                    {
                        string propertyName = Helper.PascalCase(join.ChildColumn.Code + "Lookup");

                        WriteLine("         public virtual " + Helper.ClassName(inReference.ParentTable.Code) + " " + propertyName + "{");
                        WriteLine("             get { return _" + propertyName + "; }");
                        WriteLine("             set { _" + propertyName + " = value;}");
                        WriteLine("         }");
                        W();
                    }
                }

                foreach (ReferenceSchema outReference in Table.OutReferences)
                {
                    TableSchema childTable = outReference.ChildTable;

                    if (!Helper.IsManyToManyTable(childTable))
                    {
                        WriteLine("         public virtual IList<" + Helper.ClassName(childTable.Code) + "> " + Helper.MakePlural(childTable.Code) + "{");
                        WriteLine("             get { return _" + Helper.MakePlural(childTable.Code) + "; }");
                        WriteLine("             set { _" + Helper.MakePlural(childTable.Code) + " = value; }");
                        WriteLine("         }");
                        W();

                    }
                    else
                    {
                        ReferenceSchema parentSchema = childTable.InReferences[1];

                        if (parentSchema.ParentTable.ObjectID == Table.ObjectID)
                        {
                            parentSchema = childTable.InReferences[0];
                        }
                        WriteLine("         public virtual IList<" + Helper.ClassName(childTable.Code) + "> " + Helper.MakePlural(parentSchema.ParentTable.Code) + "{");
                        WriteLine("             get { return _" + Helper.MakePlural(parentSchema.ParentTable.Code) + "; }");
                        WriteLine("             set { _" + Helper.MakePlural(parentSchema.ParentTable.Code) + " = value; }");
                        WriteLine("         }");
                        W();

                    }
                }
                WriteLine(@"");
                WriteLine(@"        public override int GetHashCode()");
                WriteLine(@"        {");
                WriteLine(@"            return ID.GetHashCode();");
                WriteLine(@"        }");
                WriteLine(@"");


                WriteLine("     }");
                WriteLine("}");
     
            }
            else
            {
                WriteLine("//-- Entity " + Entity.Name + " has no primary key information.");
            }   
        }
    }
}
