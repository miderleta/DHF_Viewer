using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using CouchDBConnector.Interfaces;

namespace CouchDBConnector
{
    public class ApiHelper : IApiHelper
    {
        public static HttpClient ApiCouchClient { get; set; }

        //CouchDB client
        public void InitializeCouchClient()
        {
            //code based on https://stackoverflow.com/questions/56931657/calling-api-with-header-username-and-password-in-c-sharp
            var byteArray = Encoding.ASCII.GetBytes("userM:XCSirz12");
            ApiCouchClient = new HttpClient();
            ApiCouchClient.DefaultRequestHeaders.Accept.Clear();
            ApiCouchClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            ApiCouchClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
        }
    }
}
