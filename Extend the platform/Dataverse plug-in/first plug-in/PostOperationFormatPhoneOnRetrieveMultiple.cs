using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;

namespace first_plug_in
{
    public class PostOperationFormatPhoneOnRetrieveMultiple : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            IPluginExecutionContext context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));

            if (context.MessageName.Equals("Retrieve")) 
            {
                if (context.OutputParameters.Contains("BusinessEntity") && context.OutputParameters["BusinessEntity"] is Entity)
                {
                    throw new InvalidPluginExecutionException("No business entity found");
                }
                
                Entity entity = (Entity)context.OutputParameters["BusinessEntity"];

                if (!entity.Attributes.Contains("company"))
                    return;
                if (!long.TryParse(entity["company"].ToString(), out long phoneNumber));
                var formattedNumber = String.Format("{0:(###) ###-####}", phoneNumber);
                entity["company"] = formattedNumber;

            }
           else if (context.MessageName.Equals("RetrieveMultiple")) 
            {
                if (!context.OutputParameters.Contains("BusinessEntityCollection") && context.OutputParameters["BusinessEntityCollection"] is EntityCollection)
                {
                    throw new InvalidPluginExecutionException("No business entity collection found");
                }

                EntityCollection entityCollection = (EntityCollection)context.OutputParameters["BusinessEntityCollection"];

                foreach (Entity entity in entityCollection.Entities) {
                    if (entity.Attributes.Contains("company") && entity["company"] != null)
                    {
                        if (long.TryParse(entity["company"].ToString(), out long phoneNumber))
                        {
                            var formattedNumber = String.Format("{0:(###) ###-####}", phoneNumber);
                            entity["company"] = formattedNumber;
                        }
                    }
                }
            }






   
        }
    }
}
