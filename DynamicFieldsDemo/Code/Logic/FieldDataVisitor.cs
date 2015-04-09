using DynamicFieldsDemo.Code.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DynamicFieldsDemo.Code.Logic
{
    public class FieldDataVisitor : IFieldDataVisitor
    {
        public void LoadBackendFieldData(ViewModels.DropdownBackendFieldViewModel dropdownBackendViewModel)
        {
            dropdownBackendViewModel.Options = GetBackendOptions(dropdownBackendViewModel.FactoryKey);
        }

        public void LoadEdenFieldData(ViewModels.DropdownEdenFieldViewModel dropdownEdenViewModel)
        {
            dropdownEdenViewModel.Options = GetEdenOptions(dropdownEdenViewModel.WebserviceUrl);
        }


        private string[] GetBackendOptions(string dataFactoryKey)
        {
            //in here, we should get data from the backend system
            if(dataFactoryKey == "countries")
            {
                return new[] { "Poland", "UK", "US" };
            }
            throw new ArgumentException("Key not implemented");
        }

        private string[] GetEdenOptions(string url)
        {
            //in here, we should get data from the provided URL
            return new[] { "Finance", "Accounting", "Banking" };
        }
    }
}