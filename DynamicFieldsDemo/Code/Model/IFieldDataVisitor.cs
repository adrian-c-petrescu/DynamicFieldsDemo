using DynamicFieldsDemo.Code.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicFieldsDemo.Code.Model
{
    public interface IFieldDataVisitor
    {
        void LoadBackendFieldData(DropdownBackendFieldViewModel dropdownBackendViewModel);
        void LoadEdenFieldData(DropdownEdenFieldViewModel dropdownEdenViewModel);
    }
}
