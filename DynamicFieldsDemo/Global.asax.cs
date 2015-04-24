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
            DataAnnotationsModelValidatorProvider.AddImplicitRequiredAttributeForValueTypes = false;
            ModelValidatorProviders.Providers.Add(
                new FluentValidationModelValidatorProvider(new ValidatorFactory()));
        }

        private class ValidatorFactory : IValidatorFactory
        {

            public IValidator GetValidator(Type type)
            {
                if (type == typeof (DynamicFormViewModel))
                {
                    
                }

                throw new NotImplementedException();
            }

            public IValidator<T> GetValidator<T>()
            {
                return (IValidator<T>) GetValidator(typeof (T));
            }
        }
    }
}
