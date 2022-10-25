using DHF_Viewer_WebApp.Models;

namespace DHF_Viewer_WebApp.Interfaces
{
    public interface IPayloadMaker
    {
        StringContent CreatePayload(DocumentModel documentData);
        StringContent CreatePayloadForUpdatedDocument(DocumentModel documentData);
    }
}