using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;

namespace first_plug_in
{
    public class WebAccessFromPlug_in : IPlugin
    {
        private string webAddress;

        public WebAccessFromPlug_in(string config)
        {
            if (string.IsNullOrEmpty(config)) 
            {
                webAddress = "https://www.google.com";
            }
            else
            {
                webAddress = config;
            }
        }
        public void Execute(IServiceProvider serviceProvider)
        {
            IPluginExecutionContext context = (IPluginExecutionContext) serviceProvider.GetService(typeof(IPluginExecutionContext));
            ITracingService tracingService = (ITracingService) serviceProvider.GetService(typeof(ITracingService));

            try {
                tracingService.Trace($"DONWLOAD the target {webAddress}");

                try {
                    using (WebClient client = new WebClient())
                    {
                        byte[] responseBytes = client.DownloadData(webAddress);
                        string response = Encoding.UTF8.GetString(responseBytes);
                        tracingService.Trace($"RESPONSE: {response}");
                        tracingService.Trace("WEB PLUG-IN COMPLETED SUCCESSFULLY");
                    }
                
                }
                catch(Exception ex) { }
            }
            catch(Exception ex) { }

        }
    }

    public class CustomWebClient: WebClient
    {
        protected override WebRequest GetWebRequest(Uri address)
        {
            HttpWebRequest request =  (HttpWebRequest) base.GetWebRequest(address);
            if (request != null) {
                request.Timeout = 15000;
                request.KeepAlive = false;
            }
            return request;
        }
    }
}
