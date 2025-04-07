using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;

namespace first_plug_in
{
    public class OrganizationService : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            IPluginExecutionContext context = (IPluginExecutionContext) serviceProvider.GetService(typeof(IPluginExecutionContext));
            ITracingService tracingService = (ITracingService) serviceProvider.GetService(typeof(ITracingService));

            IOrganizationServiceFactory organizationServiceFactory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
            IOrganizationService organizationService = organizationServiceFactory.CreateOrganizationService(context.UserId);

            try 
            {
                Entity task = new Entity("task");
                task["subject"] = "Plug-in Organization service";
                task["category"] = context.PrimaryEntityName;
    

                if(context.OutputParameters.Contains("id")) 
                {
                    Guid accountId = new Guid(context.OutputParameters["id"].ToString());
                    string objectType = "account";

                    task["regardingobjectid"] = new EntityReference(objectType, accountId);

                }
                    tracingService.Trace("Creating a Task related to Account");
                    organizationService.Create(task);
       
            }
            catch (FaultException<OrganizationServiceFault> ex)
            {
                throw new InvalidPluginExecutionException("An error ocurred in Task", ex);
            }
            catch (Exception ex) 
            {
                tracingService.Trace("Task {0}", ex.ToString());
            }
        }
    }
}
