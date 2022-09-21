using CouchDBConnector.Interfaces;
using CouchDBConnector.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouchDBConnector
{
    internal class PayloadMaker : IPayloadMaker
    {
        PayloadModel payloadData { get; set; }

        public StringContent CreatePayload(DocumentModel documentData)
        {
            payloadData = new PayloadModel();

            payloadData.Title = documentData.Title;
            payloadData.Type = documentData.Type;
            payloadData.Revision = documentData.Revision;
            payloadData.Product = documentData.Product;

            //code based on the YouTube video by Nick Proud 
            //avalible at https://www.youtube.com/watch?v=Yi-O-HBGPeU&t=924s&ab_channel=NickProud
            //convert model data to JSON and save it as payload
            var payloadDataJson = JsonConvert.SerializeObject(payloadData);
            var payload = new StringContent(payloadDataJson, Encoding.UTF8, "application/json");

            return payload;
        }
    }
}
