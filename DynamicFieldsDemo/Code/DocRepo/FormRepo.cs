using DynamicFieldsDemo.Code.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace DynamicFieldsDemo.Code.DocRepo
{
    public class FormRepo
    {
        public const string DocTemplate = "DynamicFieldsDemo.App_Data.ConfigStorage.form_{0}.json";
        public Form LoadForm(string formId)
        {
            var docName = string.Format(DocTemplate, formId);
            
            using (var reader = new StreamReader(GetType().Assembly.GetManifestResourceStream(docName)))
            {
                var strConfig = reader.ReadToEnd(); ;
                return JsonConvert.DeserializeObject<Form>(strConfig);
            }
        }
    }
}