using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CouchDBConnector;


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
    }
}
