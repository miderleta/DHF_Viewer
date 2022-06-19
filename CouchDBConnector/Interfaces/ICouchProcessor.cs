using CouchDBConnector.Models;

namespace CouchDBConnector
{
    public interface ICouchProcessor
    {
        Task<string> CreateNewDocument();
        Task<string> DeleteDocument();
        string getEndpointAddress();
        Task<CouchModel> LoadCouchData();
        Task<DocumentModel> ReadDocumentData();
        Task<string> UpdateDocument();
    }
}