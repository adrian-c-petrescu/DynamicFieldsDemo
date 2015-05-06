using DynamicFieldsDemo.Code.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace DynamicFieldsDemo.Code.MvcExtensions
{
	public static class MvcExtensions
	{
		public static MvcHtmlString EditorWithCustomValidation(this HtmlHelper htmlHelper, AbstractFieldViewModel model)
		{
			return htmlHelper.TextBox(model.Key, model.Value, model.ValidationHtmlAttributes);
		}
	}
}