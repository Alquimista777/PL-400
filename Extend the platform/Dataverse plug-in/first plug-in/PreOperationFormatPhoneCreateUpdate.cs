using Microsoft.Xrm.Sdk;
using System;
using System.Text.RegularExpressions;

namespace first_plug_in
{
    public class PreOperationFormatPhoneCreateUpdate : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            IPluginExecutionContext contenxt = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));


            if (!contenxt.InputParameters.ContainsKey("Target"))
                throw new InvalidPluginExecutionException("No target found");

            Entity entity = contenxt.InputParameters["Target"] as Entity;

            if (!entity.Attributes.Contains("telephone1"))
                return;

            string phoneNumber = (string)entity["telephone1"];
            var formattedNumber = Regex.Replace(phoneNumber, @"[^\d]", "");

            entity["telephone1"] = $"0000000{formattedNumber}";

        }
    }
}
