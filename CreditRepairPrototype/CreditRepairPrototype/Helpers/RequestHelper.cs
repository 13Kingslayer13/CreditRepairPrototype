using System;
using System.Linq;
using System.Collections.Generic;
using RestSharp.Portable.HttpClient;
using RestSharp.Portable;
using HtmlAgilityPack;

namespace CreditRepairPrototype
{
    public class RequestHelper : Singleton<RequestHelper>
    {     
        public async void SendRequest(string uri, string apiCommand, object data, Action<string> successCallback = null, Action<string> errorCallback = null)
        {            
            try
            {
                using (var client = new RestClient(uri)) {
					var request = apiCommand == "" ? new RestRequest() : new RestRequest(apiCommand);
                    request.AddBody(data);
					var response = await client.Execute(request);
                    if(successCallback != null)
                    {                       
                        successCallback(response.Content);
                    }
				}
            }
            catch(Exception ex)
            {
                if(errorCallback != null)
                    errorCallback(ex.Message);
            }
        }

        public async void SendRequest<T>(string uri, string apiCommand, object data, Action<T> successCallback, Action<string> errorCallback = null)
        {            
            try
            {
                using (var client = new RestClient(uri)) {
					var request = apiCommand == "" ? new RestRequest() : new RestRequest(apiCommand);
                    request.AddBody(data);
					var response = await client.Execute<T>(request);
                    successCallback(response.Data);                                         
                }  
            }
            catch(Exception ex)
            {
                if(errorCallback != null)
                    errorCallback(ex.Message);
            }
        }

		public async void ReadHTMLPage(string uri, string apiCommand, object data, ParseHtmlSettings settings, Action<List<ParsedHtmlNode>> successCallback, Action<string> errorCallback = null)
		{            
			try
			{				
				var htmlWeb = new HtmlWeb();
				var html = await htmlWeb.LoadFromWebAsync(uri);	

				var classes = html.DocumentNode.Descendants().Where(d => 
					d.Attributes.Contains(settings.Attribute) && (! settings.HasAttributeValue || d.Attributes[settings.Attribute].Value.Contains(settings.AttributeValue))
				);

				List<ParsedHtmlNode> nodes = new List<ParsedHtmlNode>();
				foreach(HtmlNode node in classes)
				{
					ParsedHtmlNode parsedNode = new ParsedHtmlNode(settings.Attribute, node.InnerText);
					if(!settings.HasInclusion || node.InnerText.Contains(settings.ContentInclusion))
					{						
						nodes.Add(parsedNode);
					}
				}

				if(successCallback != null)
				{
					successCallback(nodes);
				}		
			}
			catch(Exception ex)
			{
				if(errorCallback != null)
					errorCallback(ex.Message);
			}
		}
    }   
}

