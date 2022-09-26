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
        PayloadModel? payloadData { get; set; }
        PayloadUpdateDocumentModel? updateDocumentPayload { get; set; }
        ICouchProcessor? Processor { get; set; }

        public StringContent CreatePayload(DocumentModel documentData)
        {
            payloadData = new PayloadModel()
            {
                Title = documentData.Title,
                Type = documentData.Type,
                Revision = documentData.Revision,
                Product = documentData.Product,
            };

            //code based on the YouTube video by Nick Proud 
            //avalible at https://www.youtube.com/watch?v=Yi-O-HBGPeU&t=924s&ab_channel=NickProud
            //convert model data to JSON and save it as payload
            var payloadDataJson = JsonConvert.SerializeObject(payloadData);
            var payload = new StringContent(payloadDataJson, Encoding.UTF8, "application/json");

            return payload;
        }

        public StringContent CreatePayloadForUpdatedDocument(DocumentModel documentData)
        {
            Processor = new CouchProcessor();

            //query the databse to retrive the _rev number of the document
            string couchDocRev = Processor.ReadDocumentData(documentData).Result._rev;

            //assign required info to payloadData variable
            updateDocumentPayload = new PayloadUpdateDocumentModel()
            {
                Title = documentData.Title,
                _rev = couchDocRev,
                Type = documentData.Type,
                Revision = documentData.Revision,
                Product = documentData.Product,
            };

            //convert model data to JSON and save it as payload
            var payloadDataJson = JsonConvert.SerializeObject(updateDocumentPayload);
            var payload = new StringContent(payloadDataJson, Encoding.UTF8, "application/json");

            return payload;
        }
    }
}
