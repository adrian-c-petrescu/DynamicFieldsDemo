using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicFieldsDemo.Code.Logic
{
	public interface IValidatorFactory
	{
		CustomValidator BuildValidator(string validatorString);
	}

	public class ValidatorFactory : IValidatorFactory
	{

		public CustomValidator BuildValidator(string validatorString)
		{
			if (validatorString == "required")
				return new RequiredCustomValidator();

			if (validatorString.StartsWith("minlength"))
				return new MinLengthCustomValidator(validatorString);

			if (validatorString.StartsWith("maxlength"))
				return new MaxLengthCustomValidator(validatorString);

			if (validatorString.StartsWith("regex"))
				return new RegexCustomValidator(validatorString);

			throw new NotImplementedException(string.Format("Validator {0} not implemented", validatorString));
		}
	}
}
