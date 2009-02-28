using System;
using System.Collections.Generic;
using System.Text;
using SmartCode.Template;
using SmartCode.Model;

namespace NHibernateTemplates
{
    public class DomainObject : TemplateBase
    {
        public DomainObject()
        {
            CreateOutputFile = true;
            IsProjectTemplate = true;
            Description = "Generates the DomainObject Base Class";
            Name = "DomainObject";
            OutputFolder = "NHibernate/Domain";
        }

        public override string OutputFileName()
        {
            return "DomainObject.cs";
        }

        public override void ProduceCode()
        {
            WriteLine(@"using System;");
            WriteLine(@"using System.Collections.Generic;");
            W();
            WriteLine(@"namespace {0}.Core.DataInterfaces", Helper.PascalCase(Domain.Code));

            WriteLine(@"{");
            WriteLine(@"  /// <summary>");
            WriteLine(@"/// For a discussion of this object, see ");
            WriteLine(@"/// http://devlicio.us/blogs/billy_mccafferty/archive/2007/04/25/using-equals-gethashcode-effectively.aspx");
            WriteLine(@"/// </summary>");
            WriteLine(@"public abstract class DomainObject<IdT>");
            WriteLine(@"{");
            WriteLine(@"    private IdT id = default(IdT);");
            W();
            WriteLine(@"    /// <summary>");
            WriteLine(@"    /// ID may be of type string, int, custom type, etc.");
            WriteLine(@"    /// Setter is protected to allow unit tests to set this property via reflection and to allow ");
            WriteLine(@"    /// domain objects more flexibility in setting this for those objects with assigned IDs.");
            WriteLine(@"    /// </summary>");
            WriteLine(@"    public IdT ID {");
            WriteLine(@"        get { return id; }");
            WriteLine(@"        protected set { id = value; }");
            WriteLine(@"    }");
            W();
            WriteLine(@"    public override sealed bool Equals(object obj) {");
            WriteLine(@"        DomainObject<IdT> compareTo = obj as DomainObject<IdT>;");
            W();
            WriteLine(@"        return (compareTo != null) &&");
            WriteLine(@"               (HasSameNonDefaultIdAs(compareTo) ||");
            WriteLine(@"                // Since the IDs aren't the same, either of them must be transient to ");
            WriteLine(@"                // compare business value signatures");
            WriteLine(@"                (((IsTransient()) || compareTo.IsTransient()) &&");
            WriteLine(@"                 HasSameBusinessSignatureAs(compareTo)));");
            WriteLine(@"    }");
            W();
            WriteLine(@"    /// <summary>");
            WriteLine(@"    /// Transient objects are not associated with an item already in storage.  For instance,");
            WriteLine(@"    /// a <see cref=""Customer"" /> is transient if its ID is 0.");
            WriteLine(@"    /// </summary>");
            WriteLine(@"    public bool IsTransient() {");
            WriteLine(@"        return ID == null || ID.Equals(default(IdT));");
            WriteLine(@"    }");
            W();
            WriteLine(@"    /// <summary>");
            WriteLine(@"    /// Must be provided to properly compare two objects");
            WriteLine(@"    /// </summary>");
            WriteLine(@"    public abstract override int GetHashCode();");
            W();
            WriteLine(@"    private bool HasSameBusinessSignatureAs(DomainObject<IdT> compareTo) {");
            WriteLine(@"        Check.Require(compareTo != null, ""compareTo may not be null"");");
            W();
            WriteLine(@"        return GetHashCode().Equals(compareTo.GetHashCode());");
            WriteLine(@"    }");
            W();
            WriteLine(@"    /// <summary>");
            WriteLine(@"    /// Returns true if self and the provided persistent object have the same ID values ");
            WriteLine(@"    /// and the IDs are not of the default ID value");
            WriteLine(@"    /// </summary>");
            WriteLine(@"    private bool HasSameNonDefaultIdAs(DomainObject<IdT> compareTo) {");
            WriteLine(@"        Check.Require(compareTo != null, ""compareTo may not be null"");");
            W();
            WriteLine(@"        return (ID != null && ! ID.Equals(default(IdT))) &&");
            WriteLine(@"               (compareTo.ID != null && ! compareTo.ID.Equals(default(IdT))) &&");
            WriteLine(@"               ID.Equals(compareTo.ID);");
            WriteLine(@"    }");
            W();
            WriteLine(@"}");
            WriteLine(@"}");

        }
    }
}
