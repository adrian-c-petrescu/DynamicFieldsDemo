using DynamicFieldsDemo.Code.ViewModels;
using FluentValidation;
using FluentValidation.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace DynamicFieldsDemo
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //setup validation
            //DataAnnotationsModelValidatorProvider.AddImplicitRequiredAttributeForValueTypes;
			//ModelValidatorProviders.Providers.Add(rovider(new ValidatorFactory())
			//	new CustomModelValidatorProvider()
			//	//new FluentValidationModelValidatorP
                
			//	);
        }


		//private class CustomModelValidatorProvider : ModelValidatorProvider
		//{
		//	public override IEnumerable<ModelValidator> GetValidators(ModelMetadata metadata, ControllerContext context)
		//	{
		//		return new ModelValidator[] { new DynamicFormValidator(metadata, context) };
		//	}
		//}

		//private class DynamicFormValidator : ModelValidator
		//{
		//	public DynamicFormValidator(
		//		ModelMetadata metadata,
		//		ControllerContext controllerContext)
		//		: base(metadata, controllerContext)
		//	{
		//	}

		//	public override IEnumerable<ModelValidationResult> Validate(
		//		object container)
		//	{
		//		throw new NotImplementedException();
		//	}
		//}


    }
}
