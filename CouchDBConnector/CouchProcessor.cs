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
        string endpointAddress = "";

        public string getEndpointAddress()
        {
            return this.endpointAddress;
        }

        public void setEndpointAddress(string db_name)
        {
            //create endpoint url
            if (db_name != "")
            {
                this.endpointAddress = $"http://127.0.0.1:5984/{ db_name }";
            }
            else
            {
                this.endpointAddress = "http://127.0.0.1:5984/";
            }
        }

        //calls CouchDB API and retrives init data 
        public async Task<CouchModel> LoadCouchData() //I may want to use a database name here as a variable
        {
            //string url = "";

            //create endpoint url
            //if (db_name != "")
            //{
            //    url = $"http://127.0.0.1:5984/{ db_name }";
            //}
            //else
            //{
            //    url = "http://127.0.0.1:5984/";
            //}

            string url = this.getEndpointAddress();

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

        public async Task<String> CreateNewDocument()
        {
            //string url = "";
            //if (db_name != "")
            //{
            //    url = $"http://127.0.0.1:5984/{ db_name }";
            //}
            //else
            //{
            //    url = "http://127.0.0.1:5984/";
            //}
            string url = this.getEndpointAddress();
            var documentData = new NewDocumentModel()
            {
                documentNumber = "H001-001-004",
                documentDesc = "Yet Another new Document",
                documentRev = "AA",
                documentType = "Validation Plan"
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
