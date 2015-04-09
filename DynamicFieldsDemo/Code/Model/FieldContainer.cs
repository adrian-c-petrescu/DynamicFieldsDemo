using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DynamicFieldsDemo.Code.Model
{
    public class FieldContainer
    {
        public AbstractField[] Fields { get; set; }
    }


    public abstract class AbstractField
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("label")]
        public string Label { get; set; }

        [JsonProperty("validation")]
        public string[] Validation { get; set; }

        [JsonProperty("validation-msg")]
        public string ValidationMsg { get; set; }

        //public abstract string ViewName { get; }
    }

    public class TextField : AbstractField
    {
        //public override string ViewName { get { return "TextFieldView"; } }
    }

    public class DropdownBackendField : AbstractField
    {
        [JsonProperty("factory-key")]
        public string FactoryKey { get; set; }

        //public override string ViewName { get { return "DropdownBackendFieldView"; } }
    }

    public class DropdownEdenField : AbstractField
    {
        [JsonProperty("webservice-url")]
        public string WebserviceUrl { get; set; }

        //public override string ViewName { get { return "DropdownEdenFieldView"; } }
    }
}
