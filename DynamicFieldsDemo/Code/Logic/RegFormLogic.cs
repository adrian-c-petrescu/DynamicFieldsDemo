using DynamicFieldsDemo.Code.DocRepo;
using DynamicFieldsDemo.Code.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DynamicFieldsDemo.Code.Logic
{
    public class RegFormLogic
    {
        private readonly FieldContainerRepo _fieldContainerRepo;
        private readonly FormRepo _formRepo;

        public RegFormLogic(FieldContainerRepo fieldContainerRepo, FormRepo formRepo)
        {
            _fieldContainerRepo = fieldContainerRepo;
            _formRepo = formRepo;
        }

        public IEnumerable<AbstractField> GetFields(string formId)
        {
            var allFieldsDictionary = _fieldContainerRepo.LoadFields().Fields.ToDictionary(f => f.Key, f => f);
            var formFieldKeys = _formRepo.LoadForm(formId).FieldKeys;

            return formFieldKeys.Select(key => allFieldsDictionary[key]).ToList();
        }
    }
}