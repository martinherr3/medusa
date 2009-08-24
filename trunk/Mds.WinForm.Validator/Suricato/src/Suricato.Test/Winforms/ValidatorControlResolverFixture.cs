using System;
using System.Windows.Forms;
using Suricato.Winforms.Validation;
using Suricato.Winforms.Validation.ValidatorControls;
using Xunit;

namespace Suricato.Test.Winforms
{
	public class ValidatorControlResolverFixture
	{
		[Fact]
		public void CanBuild()
		{
			ValidatorControlResolver vcr = new ValidatorControlResolver();
		}


		[Fact]
		public void CanResolve()
		{
			ValidatorControlResolver vcr = new ValidatorControlResolver();

			object control = new TextBox();
			((TextBox) control).Text = "Terere";

			IControlValuable cv = vcr.GetControlValuable(control);

			Assert.NotNull(cv);
			Assert.Equal("Terere", (string) cv.GetValue((Control) control));
		}

		[Fact]
		public void CanResolveTextbox()
		{
			ValidatorControlResolver vcr = new ValidatorControlResolver();

			TextBox txt = new TextBox();
			txt.Text = "Terere";

			IControlValuable cv = vcr.GetControlValuable(txt);

			Assert.NotNull(cv);
			Assert.Equal("Terere", (string) cv.GetValue(txt));
		}

		[Fact]
		public void CanResolveDateTimePicker() 
		{
			ValidatorControlResolver vcr = new ValidatorControlResolver();

			DateTimePicker dtp = new DateTimePicker();
			
			IControlValuable cv = vcr.GetControlValuable(dtp);

			Assert.NotNull(cv);
			//Assert.Equal(DateTime., (DateTime)cv.GetValue(dtp));
		}
	}
}