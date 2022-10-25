using DHF_Viewer_WebApp.Models;
using System.Collections;

namespace DHF_Viewer_WebApp.Interfaces
{
    public interface ICouchProcessor
    {
        Task<string> CreateNewDocument(DocumentModel documentData);
        Task<string> DeleteDocument(DocumentModel documentData);
        string getEndpointAddress();
        Task<CouchModel> LoadCouchData();
        Task<DocumentModel> ReadDocumentData(DocumentModel documentData);
        Task<string> UpdateDocument(DocumentModel documentData);
        Task<ReportModel> ReportAllDocuments();
        Task<ReportModel> ReportByProduct(DocumentModel documentData);
        Task<ReportModel> SearchByDocumentName(DocumentModel documentData);
    }
}