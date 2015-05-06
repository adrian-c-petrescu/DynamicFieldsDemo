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
		#region Metadata
		public string Type { get; set; }
        public string Key { get; set; }
        public string Label { get; set; }
        public string[] Validation { get; set; }
        public string ValidationMsg { get; set; }
		#endregion

		#region state
		public string Value { get; set; }
		#endregion

		#region actions
		public abstract string ViewName { get; }
        public abstract void LoadExtraViewData(IFieldDataVisitor fieldVisitor);

		public virtual void RunValidation(ModelStateDictionary modelStateDictionary)
		{
			if (string.IsNullOrEmpty(Value))
			{
				var modelState = new ModelState();
				modelState.Errors.Add(ValidationMsg);

				modelStateDictionary.Add(Key, modelState);
			}
		}

		public abstract IDictionary<string, object> ValidationHtmlAttributes { get; }

		public void LoadFormValue(NameValueCollection formData)
		{
			Value = formData[Key];
		}

		public override string ToString()
		{
			return string.Format("{0} = {1}", Key, Value);
		}
		#endregion
	}

    public class TextFieldViewModel : AbstractFieldViewModel
    {
        public override string ViewName { get { return "TextFieldView"; } }
        public override void LoadExtraViewData(IFieldDataVisitor fieldVisitor) { }

		public override IDictionary<string, object> ValidationHtmlAttributes
		{
			get
			{
				var result = new Dictionary<string, object>();
				result.Add("data-val", "true");

				if (Validation.Contains("required"))
				{
					result.Add("data-val-required", ValidationMsg);
				}
				if (Validation.Contains("minLength"))
				{
					result.Add("data-val-minlength", ValidationMsg);
					result.Add("data-val-minlength-min", "5");
				}
				if (Validation.Contains("maxLength"))
				{
					result.Add("data-val-maxlength", ValidationMsg);
					result.Add("data-val-maxlength-max", "50");
				}

				return result;
			}
		}

    }

    public abstract class DropdownFieldViewModel : AbstractFieldViewModel
    {
        public IEnumerable<SelectListItem> SelectListItems
        {
            get
            {
                return Options
                    .Select(o => new SelectListItem
                    {
                        Text = o,
                        Value = o
                    })
                    .ToArray();
            }
        }
        public string[] Options { get; set; }
        public override string ViewName { get { return "DropdownFieldView"; } }

		public override IDictionary<string, object> ValidationHtmlAttributes
		{
			get { return new Dictionary<string, object>(); }
		}
    }

    public class DropdownBackendFieldViewModel : DropdownFieldViewModel
    {
        public string FactoryKey { get; set; }
        
        public override void LoadExtraViewData(IFieldDataVisitor fieldVisitor)
        {
            fieldVisitor.LoadBackendFieldData(this);
        }
    }

    public class DropdownEdenFieldViewModel : DropdownFieldViewModel
    {
        public string WebserviceUrl { get; set; }

        public override void LoadExtraViewData(IFieldDataVisitor fieldVisitor)
        {
            fieldVisitor.LoadEdenFieldData(this);
        }
    }
}