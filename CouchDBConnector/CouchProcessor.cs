using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CouchDBConnector;
using Newtonsoft.Json;
using CouchDBConnector.Models;
using System.Collections;

namespace CouchDBConnector
{
    public class CouchProcessor : ICouchProcessor
    {
        public string DatabaseUrl = "http://127.0.0.1:5984/dhf_viewer";
        public Object Documents { get; set; }
       
        public string getEndpointAddress()
        {
            return this.DatabaseUrl;
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

        //CREATE DOCUMENT
        public async Task<String> CreateNewDocument(DocumentModel documentData)
        {
            string url = this.getEndpointAddress();

            var payloadData = new PayloadModel()
            {
                Title = documentData.Title,
                Type = documentData.Type,
                Revision = documentData.Revision,
                Product = documentData.Product,
            };

            //crete databse URL with documentID
            string urlWithDocID = url + "/" + documentData._id;

            //code based on the YouTube video by Nick Proud 
            //avalible at https://www.youtube.com/watch?v=Yi-O-HBGPeU&t=924s&ab_channel=NickProud
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

        //READ DOCUMENT DATA
        ////calls CouchDB API and retrives document Data 
        public async Task<DocumentModel> ReadDocumentData(DocumentModel documentData)
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

        //UPDATE DOCUMENT
        public async Task<String> UpdateDocument(DocumentModel documentData)
        {
            string url = this.getEndpointAddress();

            //crete documentID using documentNumber and pass it on in databaseURL
            string urlWithDocID = url + "/" + documentData._id;

            //query the databse to retrive the _rev number of the document
            var couchDocRev = ReadDocumentData(documentData).Result._rev;

            //assign required info to payloadData variable
            var payloadData = new UpdateDocumentModel()
            {
                Title = documentData.Title,
                _rev = couchDocRev,
                Type = documentData.Type,
                Revision = documentData.Revision,
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

        //DELETE DOCUMENT
        public async Task<String> DeleteDocument(DocumentModel documentData)
        {
            string url = this.getEndpointAddress();

            //query the databse to retrive the _rev number of the document
            var couchDocRev = ReadDocumentData(documentData).Result._rev;

            //crete targetURL with document _id and couchdb revision
            string urlWithDocID = url + "/" + documentData._id + "?rev=" + couchDocRev;

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

        //REPORTS
        //ALL DOCUMENTS
        //this function will call all_docs view in couchDB and return JSON with data for all documents
        public async Task<ReportModel> ReportAllDocuments()
        {
            string url = this.getEndpointAddress();

            //URL to access all_docs view
            string viewUrl = url + "/_design/by_product/_view/all_products";

            //make a call to the API using ApiClient
            using (HttpResponseMessage response = await ApiHelper.ApiCouchClient.GetAsync(viewUrl))
            {
                if (response.IsSuccessStatusCode)
                {
                    ReportModel docData = await response.Content.ReadAsAsync<ReportModel>();
                    return docData;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        //BY PRODUCT
        //this function will call all_docs view in couchDB and return JSON with data for all documents
        public async Task<ReportModel> ReportByProduct(DocumentModel documentData)
        {
            string url = this.getEndpointAddress();
            string viewUrl = "";
            string product = documentData.Product;

            //research case statemnts for c#
            if (product == null)
            {
                //URL to access by_product view
                viewUrl = url + "";
            }

            if (product == "Product A")
            {
                //URL to access by_product view
                viewUrl = url + "/_design/by_product/_view/Product_A";
            }

            if (product == "Product B")
            {
                //URL to access by_product view
                viewUrl = url + "/_design/by_product/_view/Product_B";
            }

            if (product == "Product C")
            {
                //URL to access by_product view
                viewUrl = url + "/_design/by_product/_view/Product_C";
            }

            if (product == "Product D")
            {
                //URL to access by_product view
                viewUrl = url + "/_design/by_product/_view/Product_D";
            }

            if (product == "Product E")
            {
                //URL to access by_product view
                viewUrl = url + "/_design/by_product/_view/Product_E";
            }

            if (product == "Product F")
            {
                //URL to access by_product view
                viewUrl = url + "/_design/by_product/_view/Product_F";
            }

            //make a call to the API using ApiClient
            using (HttpResponseMessage response = await ApiHelper.ApiCouchClient.GetAsync(viewUrl))
            {
                if (response.IsSuccessStatusCode)
                {
                    ReportModel docData = await response.Content.ReadAsAsync<ReportModel>();
                    return docData;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

    }
}
