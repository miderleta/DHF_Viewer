using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CouchDBConnector;
using Newtonsoft.Json;

namespace CouchDBConnector
{
    public class CouchProcessor
    {
        //calls CouchDB API and retrives init data 
        public async Task<CouchModel> LoadCouchData(string db_name) //I may want to use a database name here as a variable
        {
            string url = "";
            //const string userName = "userM";
            //const string password = "XCSirz12";

            if (db_name != "")
            {
                url = $"http://127.0.0.1:5984/{ db_name }";
            }
            else
            {
                url = "http://127.0.0.1:5984/";
            }

            //make a call to the API using ApiClient
            using (HttpResponseMessage response = await ApiHelper.ApiCouchClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    CouchModel couchData = await response.Content.ReadAsAsync<CouchModel>();

                    return couchData;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public async Task<String> CreateNewDocument(string db_name)
        {
            string url = "";
            if (db_name != "")
            {
                url = $"http://127.0.0.1:5984/{ db_name }";
            }
            else
            {
                url = "http://127.0.0.1:5984/";
            }

            var documentData = new NewDocumentModel()
            {
                documentNumber = "H001-001-003",
                documentDesc = "Another new Document",
                documentRev = "AA",
                documentType = "Test Plan"
            };

            var documentDataJson = JsonConvert.SerializeObject(documentData);
            var payload = new StringContent(documentDataJson, Encoding.UTF8, "application/json");

            //make a call to the API using ApiClient
            using (HttpResponseMessage response = await ApiHelper.ApiCouchClient.PostAsync(url, payload))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;

                    return result; 
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }

        }
    }
}
