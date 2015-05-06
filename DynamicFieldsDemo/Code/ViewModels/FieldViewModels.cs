using DynamicFieldsDemo.Code.Logic;
using DynamicFieldsDemo.Code.Model;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DynamicFieldsDemo.Code.ViewModels
{
    public abstract class AbstractFieldViewModel
	{
		#region dependencies
		public AbstractField FieldMetadata { get; private set; }
		protected List<CustomValidator> Validators { get; private set; }
		#endregion

		#region state
		public string Value { get; set; }
		#endregion

		#region constructor
		protected AbstractFieldViewModel(AbstractField fieldMetadata, IValidatorFactory validatorFactory)
		{
			FieldMetadata = fieldMetadata;
			Validators = fieldMetadata.Validation.Select(vStr => validatorFactory.BuildValidator(vStr)).ToList();
		}
		#endregion

		#region actions
		public abstract string ViewName { get; }
        public abstract void LoadExtraViewData(IFieldDataVisitor fieldVisitor);

		public virtual void RunValidation(ModelStateDictionary modelStateDictionary)
		{
			if(Validators.Any(v => !v.IsValid(Value)))
			{
				var modelState = new ModelState();
				modelState.Errors.Add(FieldMetadata.ValidationMsg);
				modelStateDictionary.Add(FieldMetadata.Key, modelState);
			}
		}

		public IDictionary<string, object> GetValidationHtmlAttributes()
		{
				var result = new Dictionary<string, object>();
				if (Validators.Any())
				{
					result.Add("data-val", "true");

					foreach (var validator in Validators)
					{
						validator.AddHtmlAttributes(result, FieldMetadata.ValidationMsg);
					}
				}

				return result;
		}
		

		public void LoadFormValue(NameValueCollection formData)
		{
			Value = formData[FieldMetadata.Key];
		}

		public override string ToString()
		{
			return string.Format("{0} = {1}", FieldMetadata.Key, Value);
		}
		#endregion
	}

    public class TextFieldViewModel : AbstractFieldViewModel
    {
        public override string ViewName { get { return "TextFieldView"; } }
        public override void LoadExtraViewData(IFieldDataVisitor fieldVisitor) { }

		public TextFieldViewModel(AbstractField fieldMetadata, IValidatorFactory validatorFactory)
			: base(fieldMetadata, validatorFactory)
		{
		}
    }

    public abstract class DropdownFieldViewModel : AbstractFieldViewModel
    {
		protected DropdownFieldViewModel(AbstractField fieldMetadata, IValidatorFactory validatorFactory)
			: base(fieldMetadata, validatorFactory)
		{
		}

        public IEnumerable<SelectListItem> SelectListItems
        {
            get
            {
                return new[] { new SelectListItem { Text = string.Format("Please select {0} ...", FieldMetadata.Label), Value = "" } }
					.Concat(Options
						.Select(o => new SelectListItem
						{
							Text = o,
							Value = o
						}))
                    .ToArray();
            }
        }
        public string[] Options { get; set; }
        public override string ViewName { get { return "DropdownFieldView"; } }
    }

    public class DropdownBackendFieldViewModel : DropdownFieldViewModel
    {
        public string FactoryKey { get; private set; }

		public DropdownBackendFieldViewModel(DropdownBackendField fieldMetadata, IValidatorFactory validatorFactory)
			: base(fieldMetadata, validatorFactory)
		{
			FactoryKey = fieldMetadata.FactoryKey;
		}
        
        public override void LoadExtraViewData(IFieldDataVisitor fieldVisitor)
        {
            fieldVisitor.LoadBackendFieldData(this);
        }
    }

    public class DropdownEdenFieldViewModel : DropdownFieldViewModel
    {
        public string WebserviceUrl { get; private set; }

		public DropdownEdenFieldViewModel(DropdownEdenField fieldMetadata, IValidatorFactory validatorFactory)
			: base(fieldMetadata, validatorFactory)
		{
			WebserviceUrl = fieldMetadata.WebserviceUrl;
		}


        public override void LoadExtraViewData(IFieldDataVisitor fieldVisitor)
        {
            fieldVisitor.LoadEdenFieldData(this);
        }
    }
}