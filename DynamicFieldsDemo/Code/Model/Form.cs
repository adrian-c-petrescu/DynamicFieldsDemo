using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DynamicFieldsDemo.Code.Model
{
    public class Form
    {
        [JsonProperty("field-keys")]
        public string[] FieldKeys { get; set; }
    }
}