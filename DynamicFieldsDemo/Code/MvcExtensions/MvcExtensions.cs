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
		public static MvcHtmlString EditorWithCustomValidation(
			this HtmlHelper htmlHelper,
			AbstractFieldViewModel model)
		{
			return EditorWithCustomValidation(htmlHelper, model, new { });
		}

		public static MvcHtmlString EditorWithCustomValidation(
			this HtmlHelper htmlHelper,
			AbstractFieldViewModel model,
			object htmlAttributes)
		{
			var htmlAttrDictionary = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);

			return htmlHelper.TextBox(
				model.FieldMetadata.Key,
				model.Value,
				model.GetValidationHtmlAttributes()
					.Concat(htmlAttrDictionary)
					.ToDictionary(kvp => kvp.Key, kvp => kvp.Value));
		}

		public static MvcHtmlString DropdownListWithCustomValidation(
			this HtmlHelper htmlHelper,
			DropdownFieldViewModel model)
		{
			return DropdownListWithCustomValidation(htmlHelper, model, new { });
		}

		public static MvcHtmlString DropdownListWithCustomValidation(
			this HtmlHelper htmlHelper,
			DropdownFieldViewModel model, object htmlAttributes)
		{
			var htmlAttr = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);

			return htmlHelper.DropDownList(
				model.FieldMetadata.Key,
				model.SelectListItems,
				model.GetValidationHtmlAttributes()
					.Concat(htmlAttr)
					.ToDictionary(kvp => kvp.Key, kvp => kvp.Value));
		}
	}
}