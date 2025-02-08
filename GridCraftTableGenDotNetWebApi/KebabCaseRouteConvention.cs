using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace GridCraftTableGenDotNetWebApi
{
    public class KebabCaseRouteConvention : IControllerModelConvention
    {
        public void Apply(ControllerModel controller)
        {
            // Convert "GridGeneration" to "grid-generation"
            string kebabCaseName = ConvertToKebabCase(controller.ControllerName);

            // Override the default route template
            foreach (var selector in controller.Selectors)
            {
                if (selector.AttributeRouteModel != null)
                {
                    selector.AttributeRouteModel.Template = $"api/{kebabCaseName}";
                }
            }
        }

        private string ConvertToKebabCase(string input)
        {
            return Regex.Replace(input, "([a-z0-9])([A-Z])", "$1-$2").ToLower();
        }
    }
}