using DynamicFieldsDemo.Code.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DynamicFieldsDemo.Code.DocRepo;
using DynamicFieldsDemo.Code.Logic;
using DynamicFieldsDemo.Controllers;
using System.Dynamic;
using System.Collections.Specialized;
using System.Web.Mvc;

namespace DynamicFieldsDemo.Code.ViewModels
{
    public class DynamicFormViewModel
    {
        public IEnumerable<AbstractFieldViewModel> FieldViewModels { get; private set; }
		public string FormId { get; private set; }

        public DynamicFormViewModel(IValidatorFactory validatorFactory, string formId)
        {
			FormId = formId;

            var fieldModels = new RegFormLogic(
                    new FieldContainerRepo(), new FormRepo())
                .GetFields(formId).ToList();

            FieldViewModels = fieldModels
                .Select(f => f.CreateViewModel(validatorFactory))
                .ToArray();
        }

		public void PopulateFromForm(NameValueCollection formData)
		{
			foreach (var field in FieldViewModels)
			{
				field.LoadFormValue(formData);
			}
		}

		public void Validate(ModelStateDictionary modelStateDictionary)
		{
			foreach (var field in FieldViewModels)
			{
				field.RunValidation(modelStateDictionary);
			}
		}

		public override string ToString()
		{
			return string.Join(", ", FieldViewModels.Select(fvm => fvm.ToString()));
		}
	}
}