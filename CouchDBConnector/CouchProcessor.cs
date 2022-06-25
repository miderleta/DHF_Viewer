using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CouchDBConnector;
using Newtonsoft.Json;
using CouchDBConnector.Models;

namespace CouchDBConnector
{
    public class CouchProcessor : ICouchProcessor
    {
        public const string DatabaseUrl = "http://127.0.0.1:5984/dhf_viewer";

        //this will be done with user's input
        DocumentModel documentData = new DocumentModel()
        {
            _id = "H001-001-004",
            _rev = "5-a6dd80bd6bc119576833d6b97d4bb236",
            Title = "Zelda URD",
            Type = "URD",
            Revision = "AA",
            Product = "360P"
        };

        public string getEndpointAddress()
        {
            return CouchProcessor.DatabaseUrl;
        }

        //calls CouchDB API and retrives init data 
        public async Task<CouchModel> LoadCouchData()
        {
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

        //create document
        public async Task<String> CreateNewDocument()
        {
            string url = this.getEndpointAddress();

            var payloadData = new PayloadModel()
            {
                Title = documentData.Title,
                Type = documentData.Type,
                Revison = documentData.Revision,
                Product = documentData.Product,
            };

            //crete databse URL with documentID
            string urlWithDocID = url + "/" + documentData._id;

            //convert model data to JSON and save it as payload
            var payloadDataJson = JsonConvert.SerializeObject(payloadData);
            var payload = new StringContent(payloadDataJson, Encoding.UTF8, "application/json");

            //make a call to the API using ApiClient (PUT)
            using (HttpResponseMessage response = await ApiHelper.ApiCouchClient.PutAsync(urlWithDocID, payload))
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

        //Read document data
        ////calls CouchDB API and retrives document Data 
        public async Task<DocumentModel> ReadDocumentData()
        {
            string url = this.getEndpointAddress();

            //crete databse URL with documentID
            string urlWithDocID = url + "/" + documentData._id;

            //make a call to the API using ApiClient
            using (HttpResponseMessage response = await ApiHelper.ApiCouchClient.GetAsync(urlWithDocID))
            {
                if (response.IsSuccessStatusCode)
                {
                    DocumentModel docData = await response.Content.ReadAsAsync<DocumentModel>();

                    return docData;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        //Update document
        //This is basic implementation of how to update the document
        //this will have to be amended to include user's input and reading it from the object.
        public async Task<String> UpdateDocument()
        {
            string url = this.getEndpointAddress();

            //crete documentID using documentNumber and pass it on in databaseURL
            string urlWithDocID = url + "/" + documentData._id;

            var payloadData = new UpdateDocumentModel()
            {
                Title = documentData.Title,
                _rev = documentData._rev,
                Type = documentData.Type,
                Revison = documentData.Revision,
                Product = documentData.Product,
            };

            //convert model data to JSON and save it as payload
            var payloadDataJson = JsonConvert.SerializeObject(payloadData);
            var payload = new StringContent(payloadDataJson, Encoding.UTF8, "application/json");

            //make a call to the API using ApiClient (POST)
            using (HttpResponseMessage response = await ApiHelper.ApiCouchClient.PutAsync(urlWithDocID, payload))
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

        //Delete document
        //This is basic implementation of how to delete a document
        //this will have to be amended to include user's input and reading it from the object.#
        public async Task<String> DeleteDocument()
        {
            string url = this.getEndpointAddress();

            //crete documentID using documentNumber and pass it on in databaseURL
            string urlWithDocID = url + "/" + documentData._id + "?rev=" + documentData._rev;

            //convert model data to JSON and save it as payload
            //var documentDataJson = JsonConvert.SerializeObject(documentData);
            //var payload = new StringContent(documentDataJson, Encoding.UTF8, "application/json");

            //make a call to the API using ApiClient (POST)
            using (HttpResponseMessage response = await ApiHelper.ApiCouchClient.DeleteAsync(urlWithDocID))
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
