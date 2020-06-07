using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace Preludio.AspNetCore
{
    public class CqsModelConvention : IApplicationModelConvention
    {
        private const string Query = "QueryController";
        public void Apply(ApplicationModel application)
        {
            foreach (var controller in application.Controllers.Where(a => a.ControllerType.Name.EndsWith(Query, StringComparison.OrdinalIgnoreCase)))
            {
                foreach (var selectorModel in controller.Selectors.Where(x => x.AttributeRouteModel != null).ToList())
                {
                    selectorModel.AttributeRouteModel =
                        new AttributeRouteModel
                        {
                            Template = "api/" +
                                       controller.ControllerType.Name.Replace(Query, "", StringComparison.OrdinalIgnoreCase)
                        };
                }
            }
        }
    }
}
