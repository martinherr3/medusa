using Castle.ActiveRecord;
using Castle.Components.Validator;


namespace Suricato.Business
{
	public class Business<T> where T : class, IValidatable
	{
		private static T _entity;
		private static bool _validation = false;
		private static Business<T> biz;

		public static string[] Errors
		{
			get { return _entity.ValidationErrorMessages; }
		}

		public static Business<T> Validate(T entity)
		{
			ActiveRecordValidationBase<T>
				EntityToValidate = entity as ActiveRecordValidationBase<T>;

			if (EntityToValidate == null) WithValidationBase(entity);
			else WithValidatorRunner(entity);

			return biz;
		}

		private static void WithValidatorRunner(object entity)
		{
			ValidatorRunner runner = new ValidatorRunner(new CachedValidationRegistry());
			_validation = runner.IsValid(entity) ? true : false;
		}

		private static void WithValidationBase(IValidatable entity)
		{
			Check.NotNull(entity, "entity", "You need to provide some not null object to be validate");
			_entity = (T) entity;
			biz = new Business<T>();
			_validation = entity.IsValid() ? true : false;
		}

		public Business<T> Then(Proc proc)
		{
			//if valid, do this here something
			proc.Invoke();
			return biz;
		}

		public Business<T> OnError(Proc proc)
		{
			//if not valid do this:
			if (_validation) proc.Invoke();

			return biz;
		}

		public Business<T> OnEachError(Proc<string> proc)
		{
			foreach(string item in Errors)
			{
				proc.Invoke(item);
			}
			return biz;
		}
	}
}