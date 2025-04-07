using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;

namespace first_plug_in
{
    public class PreEntityImagesAndPostEntityImages : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            IPluginExecutionContext context = (IPluginExecutionContext) serviceProvider.GetService(typeof(IPluginExecutionContext));
            ITracingService tracingService = (ITracingService) serviceProvider.GetService(typeof(ITracingService));

            if(context.MessageName.Equals("Update"))
            {
                try 
                {
                    Entity preImage = new Entity();

                    if(context.PreEntityImages.Contains("pre_account_image"))
                    {
                        preImage = context.PreEntityImages["pre_account_image"];
                        string accountName = preImage.GetAttributeValue<string>("name");

                        tracingService.Trace("PreEntityImage: AccountName {0}", accountName);
                    }
                }
                catch (Exception ex)
                {
                    tracingService.Trace("PreEntityImage: {0}", ex.ToString());
                    throw;
                }
            }
        }
    }
}
