using System;
using RestSharp.Portable.HttpClient;
using RestSharp.Portable;

namespace CreditRepairPrototype
{
    public class RequestHelper : Singleton<RequestHelper>
    {     
        public async void SendRequest(string uri, string apiCommand, object data, Action<string> successCallback = null, Action<string> errorCallback = null)
        {            
            try
            {
                using (var client = new RestClient(uri)) {
                    var request = new RestRequest(apiCommand);
                    request.AddBody(data);
                    var result = await client.Execute(request);
                    if(successCallback != null)
                    {                       
                        successCallback(result.Content);
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
                    var request = new RestRequest(apiCommand);
                    request.AddBody(data);
                    var result = await client.Execute<T>(request);
                    successCallback(result.Data);                                         
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

