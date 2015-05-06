using DynamicFieldsDemo.Code.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace DynamicFieldsDemo.Code.MvcExtensions
{
	public static class MvcExtensions
	{
		public static MvcHtmlString EditorWithCustomValidation(this HtmlHelper htmlHelper, AbstractFieldViewModel model)
		{
			return htmlHelper.TextBox(model.FieldMetadata.Key, model.Value, model.GetValidationHtmlAttributes());
		}

		public static MvcHtmlString DropdownListWithCustomValidation(this HtmlHelper htmlHelper, DropdownFieldViewModel model)
		{
			return htmlHelper.DropDownList(model.FieldMetadata.Key, model.SelectListItems, model.GetValidationHtmlAttributes());
		}
	}
}