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
            var formFields = _regFormLogic.GetFields(formId);

            var viewModels = formFields.Select(GetViewModelFromField).ToList();
            viewModels.ForEach(vm => vm.LoadExtraViewData(_fieldDataVisitor));

            return View(viewModels);
        }

        [HttpPost]
        public ActionResult Post(FormCollection form)
        {
            var keys = form.Cast<string>().ToList();

            var builder = new StringBuilder();
            var model = string.Join("; ", keys.Select(k => string.Format("{0} = {1}", k, form[k])));

            return View("Result", (object)model);
        }


        private AbstractFieldViewModel GetViewModelFromField(AbstractField field)
        {
            if (field is TextField)
                return Mapper.Map<TextFieldViewModel>(field);
            if (field is DropdownBackendField)
                return Mapper.Map<DropdownBackendFieldViewModel>(field);
            if (field is DropdownEdenField)
                return Mapper.Map<DropdownEdenFieldViewModel>(field);

            throw new Exception("not supported");
        }

    }
}