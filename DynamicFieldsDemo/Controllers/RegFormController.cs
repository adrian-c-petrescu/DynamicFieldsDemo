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
        

        public RegFormController()
        {
            //IOC
            _regFormLogic = new RegFormLogic(new FieldContainerRepo(), new FormRepo());
            _fieldDataVisitor = new FieldDataVisitor();
            
            Mapper.CreateMap<TextField, TextFieldViewModel>();
            Mapper.CreateMap<DropdownBackendField, DropdownBackendFieldViewModel>();
            Mapper.CreateMap<DropdownEdenField, DropdownEdenFieldViewModel>();
        }

        // GET: RegForm
        public ActionResult Index(string formId)
        {           
            var viewModel = new DynamicFormViewModel();

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
				//return View(viewModel);

			var model = viewModel.ToString();


            return View("Result", (object)model);
        }


        public static AbstractFieldViewModel GetViewModelFromField(AbstractField field)
        {
            AbstractFieldViewModel vm = null;

            if (field is TextField)
                vm = Mapper.Map<TextFieldViewModel>(field);
            if (field is DropdownBackendField)
                vm = Mapper.Map<DropdownBackendFieldViewModel>(field);
            if (field is DropdownEdenField)
                vm = Mapper.Map<DropdownEdenFieldViewModel>(field);

            if (vm == null)
                throw new Exception("not supported");

            return vm;
        }

    }
}