using DynamicFieldsDemo.Code.Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace DynamicFieldsDemo.Code.Util
{
    public class FieldConverter : JsonCreationConverter<AbstractField>
    {
        protected override AbstractField Create(Type objectType, JObject jsonObject)
        {
            string typeName = jsonObject["Type"].Value<string>();

            if (typeName == "text")
            {
                return new TextField();
            }
            if (typeName == "dropdown-backend")
            {
                return new DropdownBackendField();
            }
            if (typeName == "dropdown-eden")
            {
                return new DropdownEdenField();
            }

            throw new SerializationException(string.Format("Field type {0} is not supported", typeName));
        }

    }
}