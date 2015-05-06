using AutoMapper;
using DynamicFieldsDemo.Code.DocRepo;
using DynamicFieldsDemo.Code.Logic;
using DynamicFieldsDemo.Code.Model;
using DynamicFieldsDemo.Code.ViewModels;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace DynamicFieldsDemo.Controllers
{
    public class RegFormController : Controller
    {
        private readonly RegFormLogic _regFormLogic;
        private readonly FieldDataVisitor _fieldDataVisitor;
		private readonly IValidatorFactory _validatorFactory;
        

        public RegFormController()
        {
            //IOC
            _regFormLogic = new RegFormLogic(new FieldContainerRepo(), new FormRepo());
            _fieldDataVisitor = new FieldDataVisitor();
			_validatorFactory = new ValidatorFactory();
        }

        // GET: RegForm
        public ActionResult Index(string formId)
        {           
            var viewModel = new DynamicFormViewModel(_validatorFactory, formId);

            foreach (var vm in viewModel.FieldViewModels)
                vm.LoadExtraViewData(_fieldDataVisitor);

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Post(
            [ModelBinder(typeof(DynFormModelBinder))]
            DynamicFormViewModel viewModel)
        {
			if (!ModelState.IsValid)
				return RedirectToAction("Index");

			var model = viewModel.ToString();

            return View("Result", (object)model);
        }
    }
}