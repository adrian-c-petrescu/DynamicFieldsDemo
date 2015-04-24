using AutoMapper;
using DynamicFieldsDemo.Code.DocRepo;
using DynamicFieldsDemo.Code.Logic;
using DynamicFieldsDemo.Code.Model;
using DynamicFieldsDemo.Code.ViewModels;
using System;
using System.Collections.Generic;
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

            //var formFields = _regFormLogic.GetFields(formId);

            //var viewModels = formFields.Select(GetViewModelFromField).ToList();
            //viewModels.ForEach(vm => vm.LoadExtraViewData(_fieldDataVisitor));

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Post(FormCollection form)
        {
            var keys = form.Cast<string>().ToList();

            var builder = new StringBuilder();
            var model = string.Join("; ", keys.Select(k => string.Format("{0} = {1}", k, form[k])));

            return View("Result", (object)model);
        }


        public static AbstractFieldViewModel GetViewModelFromField(AbstractField field, Dictionary<string, string> storage)
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

            vm.Storage = storage;
            return vm;
        }

    }
}