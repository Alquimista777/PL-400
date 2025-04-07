using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;

namespace first_plug_in
{
    public class CustomWebAPI : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            IPluginExecutionContext context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            ITracingService tracingService = (ITracingService)serviceProvider.GetService(typeof(ITracingService));

           if(context.MessageName.Equals("CustomAPIMessageExample"))
            {
                try 
                {
                    string input = (string)context.InputParameters["param1"];
                    if (!string.IsNullOrEmpty(input)) {
                        context.OutputParameters["response"] = new string(input.Reverse().ToArray());
                        tracingService.Trace($"CUSTOM API MESSAGE, PARAM1 {input}");
                    }
                }
                catch (Exception ex) {

                    tracingService.Trace("CustomAPIMessageExample: {0}", ex.ToString());
                    throw new InvalidPluginExecutionException("An error occurred in CustomAPIMessageExample.", ex);
                }
            }
        }
    }
}
