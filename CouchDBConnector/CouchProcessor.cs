using System.Text;
using Newtonsoft.Json;
using CouchDBConnector.Models;
using CouchDBConnector.Interfaces;

namespace CouchDBConnector
{
    public class CouchProcessor : ICouchProcessor
    {
        public string DatabaseUrl = "http://127.0.0.1:5984/dhf_viewer";
       
        public string getEndpointAddress()
        {
            return this.DatabaseUrl;
        }

        public string createApiCallUrl(DocumentModel documentData)
        {
            string apiCallUrl = this.getEndpointAddress() + "/" + documentData._id;

            return apiCallUrl;
        }

        //calls CouchDB API and retrives init data 
        public async Task<CouchModel> LoadCouchData()
        {
            string apiCallUrl = this.getEndpointAddress();

            //make a call to the API using ApiClient
            using (HttpResponseMessage response = await ApiHelper.ApiCouchClient.GetAsync(apiCallUrl))
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
        //Function takes user input (as parameter), creates the payload and makes an API call
        //to create a new document.
        public async Task<String> CreateNewDocument(DocumentModel documentData)
        {
            //create URL for the API call
            string apiCallUrl = createApiCallUrl(documentData);

            IPayloadMaker payloadMaker = new PayloadMaker();
            StringContent payload = payloadMaker.CreatePayload(documentData);

            //make a call to the API using ApiClient (PUT)
            using (HttpResponseMessage response = await ApiHelper.ApiCouchClient.PutAsync(apiCallUrl, payload))
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
        //calls CouchDB API and retrives document Data 
        public async Task<DocumentModel> ReadDocumentData(DocumentModel documentData)
        {
            //create URL for the API call
            string apiCallUrl = createApiCallUrl(documentData);

            //make a call to the API using ApiClient
            using HttpResponseMessage response = await ApiHelper.ApiCouchClient.GetAsync(apiCallUrl);
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

        //UPDATE DOCUMENT
        //Functions first queries the DB using document number entered by the user to retrieve
        //the document revision, and then supplies updated document data as payload
        //for the API call.
        public async Task<String> UpdateDocument(DocumentModel documentData)
        {
            //create URL for the API call
            string apiCallUrl = createApiCallUrl(documentData);

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
            using HttpResponseMessage response = await ApiHelper.ApiCouchClient.PutAsync(apiCallUrl, payload);
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

        //DELETE DOCUMENT
        //Function queries the DB to retrieve document ID and makes a call
        //to the API using the document ID and revision to delete it.
        public async Task<String> DeleteDocument(DocumentModel documentData)
        {
            //query the databse to retrive the _rev number of the document
            var couchDocRev = ReadDocumentData(documentData).Result._rev;

            //create URL for the API call
            string apiCallUrl = createApiCallUrl(documentData) + "?rev=" + couchDocRev;

            //make a call to the API using ApiClient (POST)
            using HttpResponseMessage response = await ApiHelper.ApiCouchClient.DeleteAsync(apiCallUrl);
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
        //this function will call a specified COuchDB view
        //based on the Product chosen by the user. All documents related to chosen product
        //will be displayed
        public async Task<ReportModel> ReportByProduct(DocumentModel documentData)
        {
            string url = this.getEndpointAddress();
            string viewUrl = "";
            string product = documentData.Product;

            //create view URL based on chosen product
            switch (product)
            {
                case "Product A":
                    viewUrl = url + "/_design/by_product/_view/Product_A";
                    break;
                case "Product B":
                    viewUrl = url + "/_design/by_product/_view/Product_B";
                    break;
                case "Product C":
                    viewUrl = url + "/_design/by_product/_view/Product_C";
                    break;
                case "Product D":
                    viewUrl = url + "/_design/by_product/_view/Product_D";
                    break;
                case "Product E":
                    viewUrl = url + "/_design/by_product/_view/Product_E";
                    break;
                case "Product F":
                    viewUrl = url + "/_design/by_product/_view/Product_F";
                    break;
                default:
                    viewUrl = url + "";
                    break;
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

        //BY DOCUMENT NAME
        //this function will call all_docs with a parameter coresponding to a document title
        //entered bu the user and retur the document data or error
        public async Task<ReportModel> SearchByDocumentName(DocumentModel documentData)
        {
            string url = this.getEndpointAddress();
            string docTitle = documentData.Title;
            string modifiedTitle = "\"" + docTitle + "\"";

            //URL to access all_docs view
            string viewUrl = url + "/_design/by_product/_view/all_products?key=" + modifiedTitle;

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
