using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace InvestmentTrackerApiClient
{
    public partial class InvestmentTrackerApiClient
    {
        public string Token {get;set;}

        partial void PrepareRequest(System.Net.Http.HttpClient client, System.Net.Http.HttpRequestMessage request, string url)
        { 
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", Token);
        }
        //void ProcessResponse(System.Net.Http.HttpClient client, System.Net.Http.HttpResponseMessage response)
        //{ 
           
        //}
    }
}
