using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;

namespace first_plug_in
{
    public class SharedVariables : IPlugin
    {
      
        public void Execute(IServiceProvider serviceProvider)
        {
           IPluginExecutionContext context = (IPluginExecutionContext) serviceProvider.GetService(typeof(IPluginExecutionContext));
           ITracingService tracingService = (ITracingService)serviceProvider.GetService(typeof(ITracingService));


            if (context.SharedVariables.Contains("sharedVariable")) // Post-OPeration
            {
                Guid guid = new Guid((string)context.SharedVariables["sharedVariable"]);
                tracingService.Trace("ShareVariable OBTAINED {0}", guid.ToString());

            }
            else // Pre-OPeration
            {
                Guid guid = new Guid("415060ae-0000-0000-0000-0022482b576f");

                context.SharedVariables.Add("sharedVariable", guid.ToString());
                tracingService.Trace("ShareVariable CREATED {0}", guid.ToString());


            }

        }
    }
}
