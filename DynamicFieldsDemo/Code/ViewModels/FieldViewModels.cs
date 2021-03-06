﻿using DynamicFieldsDemo.Code.Model;
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

        public abstract string ViewName { get; }
        public abstract void LoadExtraViewData(IFieldDataVisitor fieldVisitor);
    }

    public class TextFieldViewModel : AbstractFieldViewModel
    {
        public override string ViewName { get { return "TextFieldView"; } }
        
        public override void LoadExtraViewData(IFieldDataVisitor fieldVisitor) { }
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
    }

    public class DropdownEdenFieldViewModel : DropdownFieldViewModel
    {
        public string WebserviceUrl { get; set; }

        public override void LoadExtraViewData(IFieldDataVisitor fieldVisitor)
        {
            fieldVisitor.LoadEdenFieldData(this);
        }
    }
}