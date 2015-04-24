using DynamicFieldsDemo.Code.DocRepo;
using DynamicFieldsDemo.Code.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DynamicFieldsDemo.Controllers;

namespace DynamicFieldsDemo.Code.ViewModels
{
    //[ModelBinder(typeof(ModelBinder))]
    public class DynamicFormViewModel
    {
        public IEnumerable<AbstractFieldViewModel> FieldViewModels { get; private set; }
        public Dictionary<string, string> FormValues { get; private set; }

        public DynamicFormViewModel()
        {   //used on get
            FormValues = new Dictionary<string, string>();
            foreach (var vm in FieldViewModels)
                FormValues[vm.Key] = "";

            //hack to get this thing working
            FieldViewModels =
                new RegFormLogic(new FieldContainerRepo(), new FormRepo())
                    .GetFields("123")
                    .Select(fieldVm => RegFormController.GetViewModelFromField(fieldVm, FormValues))
                    .ToArray();
        }
    }

    //public class DynFormModelBinder : IModelBinder
    //{

    //    public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
    //    {
    //        bindingContext.
    //    }
    //}
}