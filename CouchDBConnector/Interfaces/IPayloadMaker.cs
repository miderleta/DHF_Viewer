using CouchDBConnector.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouchDBConnector.Interfaces
{
    public interface IPayloadMaker
    {
        StringContent CreatePayload(DocumentModel documentData);
        StringContent CreatePayloadForUpdatedDocument(DocumentModel documentData);
    }
}
