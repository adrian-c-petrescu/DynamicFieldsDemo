using DynamicFieldsDemo.Code.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DynamicFieldsDemo.Code.ViewModels
{
    public class DynFormModelBinder : IModelBinder
    {
        public object BindModel(
            ControllerContext controllerContext,
            ModelBindingContext bindingContext)
        {
            var request = controllerContext.HttpContext.Request;

			var formId = request.Form["formId"];

            //build the generic view model
            var dynFormViewModel = new DynamicFormViewModel(new ValidatorFactory(), formId);
			dynFormViewModel.PopulateFromForm(request.Form);

			//run validation
			dynFormViewModel.Validate(bindingContext.ModelState);

			return dynFormViewModel;
        }
    }
}