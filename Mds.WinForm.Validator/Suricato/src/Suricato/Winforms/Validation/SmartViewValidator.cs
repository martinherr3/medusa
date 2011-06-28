using System;
using System.Windows.Forms;
using Suricato.Winforms.Validation;
using NHibernate.Validator;
using System.Text;
using NHibernate.Validator.Engine;
using NHibernate.Validator.Exceptions;
using System.Collections.Generic;
using NHibernate.Proxy;

namespace Suricato.Winforms.Validation
{
	/// <summary>
	/// 
	/// </summary>
	public class SmartViewValidator : ViewValidator
	{
		public SmartViewValidator(ErrorProvider errorProvider)
			:base(errorProvider)
		{
		}

		public SmartViewValidator(): base()
		{
		}

        /// <summary>
        /// Obtiene un mensaje de las propiedades invalidas para ser mostrado al usuario
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public String GetInvalidMessage(object entity)
        {
            if (entity is INHibernateProxy)
            {
                ILazyInitializer init = ((INHibernateProxy)entity).HibernateLazyInitializer;
                entity = init.GetImplementation();
            }

            IEnumerable<InvalidValue> errors = GetInvalidValues(entity);
            StringBuilder sb = new StringBuilder();
            sb.Append("La operación no pudo ser realizada, los siguientes valores invalidos fueron encontrados:\n");
            int i = 0;

            foreach (InvalidValue error in errors)
            {
                string property = error.PropertyName;
                if (property.StartsWith("Id", StringComparison.CurrentCulture))
                {
                    property = property.Substring(2);
                }
                sb.Append("\n");
                sb.Append(property);
                sb.Append(": ");
                sb.Append(error.Message);
                i++;
            }

            if (i > 0)
            {
                return sb.ToString();
            }

            return String.Empty;       
        }

        /// <summary>
        /// Retorna los valores invalidos de una entidad dada
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public IEnumerable<InvalidValue> GetInvalidValues(object entity)
        {
            ClassValidator validator = new ClassValidator(entity.GetType());
            
            return validator.GetInvalidValues(entity);
        }

        /// <summary>
        /// Verifica si la entidad es valida para ser persistida 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool IsValid(object entity)
        {
            if (entity is INHibernateProxy)
            {
                ILazyInitializer init = ((INHibernateProxy)entity).HibernateLazyInitializer;
                entity = init.GetImplementation();
            }

            ClassValidator validator = new ClassValidator(entity.GetType());
            if (validator.HasValidationRules)
            {
                try
                {   
                    validator.AssertValid(entity);
                    return true;
                }
                catch (InvalidStateException)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Verifica si la entidad es valida para ser persistida
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public bool IsValid(object entity, System.Type type)
        {
            if (entity is INHibernateProxy)
            {
                ILazyInitializer init = ((INHibernateProxy)entity).HibernateLazyInitializer;
                entity = init.GetImplementation();
            }

            ClassValidator validator = new ClassValidator(type);
            if (validator.HasValidationRules)
            {
                try
                {
                    validator.AssertValid(entity);
                    return true;
                }
                catch(InvalidStateException)
                {
                    return false;
                }
            }
            return false;
        }

		/// <summary>
		/// Bindea el control con una property de la entidad
		/// </summary>
		/// <param name="control"></param>
		/// <param name="clazz"></param>
		public void Bind(Control control, Type type)
		{
            Bind(control, type, Utils.GetPropertyName(control.Name));
		}

        /// <summary>
        /// Bindea el control con una property de la entidad
        /// </summary>
        /// <param name="control"></param>
        /// <param name="type"></param>
        /// <param name="property"></param>
        new public void Bind(Control control, Type type, String property)
        {
            base.Bind(control, type, property);
            BindTheEventValidation(control);
        }

		/// <summary>
		/// Agrega el event handler de validacion al control
		/// </summary>
		/// <param name="control"></param>
		private void BindTheEventValidation(Control control)
		{
			control.Validating += new EventValidation(this).ValidatingHandler;
		}
	}
}