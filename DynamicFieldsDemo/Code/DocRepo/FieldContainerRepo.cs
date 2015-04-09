using DynamicFieldsDemo.Code.Model;
using DynamicFieldsDemo.Code.Util;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace DynamicFieldsDemo.Code.DocRepo
{
    public class FieldContainerRepo
    {
        private const string DocName = @"DynamicFieldsDemo.App_Data.ConfigStorage.field-collection.json";

        public FieldContainer LoadFields()
        {
            using (var reader = new StreamReader(GetType().Assembly.GetManifestResourceStream(DocName)))
            {
                var strConfig = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<FieldContainer>(strConfig, new FieldConverter());
            }
        }
    }
}