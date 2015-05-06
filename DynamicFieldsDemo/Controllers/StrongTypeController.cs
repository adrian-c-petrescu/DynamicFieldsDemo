using DynamicFieldsDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DynamicFieldsDemo.Controllers
{
    public class StrongTypeController : Controller
    {
        //
        // GET: /StrongType/
        public ActionResult Index()
        {
			return View(new StrongViewModel { Name = "", Value = "" });
        }

		[HttpPost, ActionName("Index")]
		public ActionResult Post(StrongViewModel model)
		{
			if (ModelState.IsValid == false)
			{
				return View(model);
			}

			var content = model.ToString();

			return View("Result", (object) content);
		}
	}
}