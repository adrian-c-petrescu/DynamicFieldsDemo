using DynamicFieldsDemo.Code.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DynamicFieldsDemo.Code.ViewModels
{
    public abstract class AbstractFieldViewModel
    {
        public string Type { get; set; }
        public string Key { get; set; }
        public string Label { get; set; }
        public string[] Validation { get; set; }
        public string ValidationMsg { get; set; }

        public Dictionary<string, string> Storage { get; set; }

        public string Value
        {
            get { return Storage[Key]; }
            set { Storage[Key] = value; }
        }

        public abstract string ViewName { get; }
        public abstract void LoadExtraViewData(IFieldDataVisitor fieldVisitor);
        public abstract bool Validate();
    }

    public class TextFieldViewModel : AbstractFieldViewModel
    {
        public override string ViewName { get { return "TextFieldView"; } }
        public override void LoadExtraViewData(IFieldDataVisitor fieldVisitor) { }

        public override bool Validate()
        {
            throw new NotImplementedException();
        }
    }

    public abstract class DropdownFieldViewModel : AbstractFieldViewModel
    {
        public string[] Options { get; set; }
        public override string ViewName { get { return "DropdownFieldView"; } }
    }

    public class DropdownBackendFieldViewModel : DropdownFieldViewModel
    {
        public string FactoryKey { get; set; }
        
        public override void LoadExtraViewData(IFieldDataVisitor fieldVisitor)
        {
            fieldVisitor.LoadBackendFieldData(this);
        }

        public override bool Validate()
        {
            throw new NotImplementedException();
        }
    }

    public class DropdownEdenFieldViewModel : DropdownFieldViewModel
    {
        public string WebserviceUrl { get; set; }

        public override void LoadExtraViewData(IFieldDataVisitor fieldVisitor)
        {
            fieldVisitor.LoadEdenFieldData(this);
        }

        public override bool Validate()
        {
            throw new NotImplementedException();
        }
    }
}