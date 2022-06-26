using CouchDBConnector.Models;

namespace CouchDBConnector
{
    public interface ICouchProcessor
    {
        Task<string> CreateNewDocument(DocumentModel documentData);
        Task<string> DeleteDocument(DocumentModel documentData);
        string getEndpointAddress();
        Task<CouchModel> LoadCouchData();
        Task<DocumentModel> ReadDocumentData(DocumentModel documentData);
        Task<string> UpdateDocument(DocumentModel documentData);
    }
}