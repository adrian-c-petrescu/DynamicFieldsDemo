using DynamicFieldsDemo.Code.Logic;
using DynamicFieldsDemo.Code.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DynamicFieldsDemo.Code.Model
{
    public class FieldContainer
    {
        public AbstractField[] Fields { get; set; }
    }


    public abstract class AbstractField
    {
        public string Type { get; set; }
        public string Key { get; set; }
        public string Label { get; set; }
        public string[] Validation { get; set; }
        public string ValidationMsg { get; set; }

		public abstract AbstractFieldViewModel CreateViewModel(IValidatorFactory validatorFactory);
    }

    public class TextField : AbstractField
    {
		public override AbstractFieldViewModel CreateViewModel(IValidatorFactory validatorFactory)
		{
			return new TextFieldViewModel(this, validatorFactory);
		}
    }

    public class DropdownBackendField : AbstractField
    {
        public string FactoryKey { get; set; }

		public override AbstractFieldViewModel CreateViewModel(IValidatorFactory validatorFactory)
		{
			return new DropdownBackendFieldViewModel(this, validatorFactory);
		}
    }

    public class DropdownEdenField : AbstractField
    {
        public string WebserviceUrl { get; set; }

		public override AbstractFieldViewModel CreateViewModel(IValidatorFactory validatorFactory)
		{
			return new DropdownEdenFieldViewModel(this, validatorFactory);
		}
    }
}
