using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;

namespace CouchDBConnector
{
    public class ApiHelper
    {
        public static HttpClient ApiClient { get; set; }
        public static HttpClient ApiCouchClient { get; set; }

        public static void InitializeClient()
        {
            ApiClient = new HttpClient();
            //ApiCLient.BaseAddress = new Uri("http://127.0.0.1:5984/");
            ApiClient.DefaultRequestHeaders.Accept.Clear();
            ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        //CouchDB client
        public static void InitializeCouchClient()
        {
            var byteArray = Encoding.ASCII.GetBytes("userM:XCSirz12");
            ApiCouchClient = new HttpClient();
            //ApiCouchClient.BaseAddress = new Uri("http://127.0.0.1:5984/");
            ApiCouchClient.DefaultRequestHeaders.Accept.Clear();
            ApiCouchClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            ApiCouchClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
        }
    }
}
