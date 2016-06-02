using System;
using System.Text;
using System.Linq;
using System.Net.Http;
using System.Collections.Generic;
using HtmlAgilityPack;

using Newtonsoft.Json;

namespace CreditRepairPrototype
{
	public enum RequestHTTPMethod
	{
		GET, POST, PUT, DELETE
	}

    public class RequestHelper : Singleton<RequestHelper>
    {    
		#region Simple Requests
		public async void SendRequestNative(string uri, RequestHTTPMethod method, Action<string> successCallback = null, Action<string> errorCallback = null)
		{ 
			SendRequestNative(uri, method, null, null, successCallback, errorCallback);
		}

		public async void SendRequestNative(string uri, RequestHTTPMethod method, Dictionary<string, string> requestHeaders, Action<string> successCallback = null, Action<string> errorCallback = null)
		{ 
			SendRequestNative(uri, method, null, requestHeaders, successCallback, errorCallback);
		}

		public async void SendRequestNative(string uri, RequestHTTPMethod method, string data, Dictionary<string, string> requestHeaders = null, Action<string> successCallback = null, Action<string> errorCallback = null)
		{            
			HttpClient client = new HttpClient();
			// Add a new Request Message
			HttpRequestMessage requestMessage = new HttpRequestMessage(ConvertToHttpMethod(method), uri);
			// Add our custom headers
			if (requestHeaders != null)
			{
				foreach (var item in requestHeaders)
				{

					requestMessage.Headers.Add(item.Key, item.Value);

				}
			}
			// Add request body
			if (data != null )
			{				
				byte[] array = Encoding.Unicode.GetBytes(data);
				requestMessage.Content = new ByteArrayContent(array);
			}
			// Send the request to the server
			try
			{					
				using(HttpResponseMessage response = await client.SendAsync(requestMessage))
				{
					using(HttpContent content = response.Content)
					{
						string result = await content.ReadAsStringAsync();
						//T dataObj = JsonConvert.DeserializeObject<T>(loadedData);
						if(successCallback != null)
						{                       
							successCallback(result);
						}
					}		
				}
			}
			catch(Exception ex)
			{
				if(errorCallback != null)
					errorCallback(ex.Message);
			}
			finally
			{
				client.Dispose();
			}
		}
		#endregion

		#region Generic Object Request
		public async void SendRequestNative<T>(string uri, RequestHTTPMethod method, Action<T> successCallback, Action<string> errorCallback = null)
		{
			SendRequestNative<T> (uri, method, null, null, successCallback, errorCallback);
		}

		public async void SendRequestNative<T>(string uri, RequestHTTPMethod method, string data, Action<T> successCallback, Action<string> errorCallback = null)
		{
			SendRequestNative<T> (uri, method, data, null, successCallback, errorCallback);
		}

		public async void SendRequestNative<T>(string uri, RequestHTTPMethod method, string data, Dictionary<string, string> requestHeader, Action<T> successCallback, Action<string> errorCallback = null)
		{            
			HttpClient client = new HttpClient();
			// Add a new Request Message
			HttpRequestMessage requestMessage = new HttpRequestMessage(ConvertToHttpMethod(method), uri);
			// Add our custom headers
			if (requestHeader != null)
			{
				foreach (var item in requestHeader)
				{

					requestMessage.Headers.Add(item.Key, item.Value);

				}
			}
			// Add request body
			if (data != null )
			{				
				byte[] array = Encoding.Unicode.GetBytes(data);
				requestMessage.Content = new ByteArrayContent(array);
			}
			// Send the request to the server
			try
			{					
				using(HttpResponseMessage response = await client.SendAsync(requestMessage))
				{
					using(HttpContent content = response.Content)
					{
						string result = await content.ReadAsStringAsync();
						T dataObj = JsonConvert.DeserializeObject<T>(result);
						if(successCallback != null)
						{                       
							successCallback(dataObj);
						}
					}		
				}
			}
			catch(Exception ex)
			{
				if(errorCallback != null)
					errorCallback(ex.Message);
			}
			finally
			{
				client.Dispose();
			}
		}
		#endregion

		#region Html Parsing
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
		#endregion

		public HttpMethod ConvertToHttpMethod(RequestHTTPMethod method){
			switch (method) {
			case RequestHTTPMethod.GET:
				return HttpMethod.Get;
			case RequestHTTPMethod.DELETE:
				return HttpMethod.Delete;
			case RequestHTTPMethod.POST:
				return HttpMethod.Post;
			case RequestHTTPMethod.PUT:
				return HttpMethod.Put;
			}
			return HttpMethod.Get;
		}       
    }   
}

