using CouchDBConnector;
using CouchDBConnector.Interfaces;
using CouchDBConnector.Models;
using DHF_Viewer_WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DHF_Viewer_WebApp.Pages.Categories
{
    public class DeleteModel : PageModel
    {
        [BindProperty]
        public ReturnedData UserInput { get; set; }

        public DocumentModel CouchDBConnectorDocumentModel { get; set; }
        public String? info { get; set; }

        public void OnGet()
        {
            UserInput = new ReturnedData();
        }

        public async Task<IActionResult> OnPost()
        {
            CouchDBConnectorDocumentModel = new DocumentModel();
            //UserInput._id = Request.Form["UserInput._id"];
            var documentNumber = UserInput._id;
            CouchDBConnectorDocumentModel._id = documentNumber;

            //Init HttpClient and CouchProcessor objects
            IApiHelper client = new ApiHelper();
            client.InitializeCouchClient();
            ICouchProcessor dataLoader = new CouchProcessor();

            //call API
            try
            {
                //Retrive doc data for disply to user
                var docData = await dataLoader.ReadDocumentData(CouchDBConnectorDocumentModel);
                if (docData != null)
                {
                    UserInput._rev = docData._rev;
                    UserInput._id = docData._id;
                    UserInput.Title = docData.Title;
                    UserInput.Revision = docData.Revision;
                    UserInput.Product = docData.Product;
                    UserInput.Type = docData.Type;
                }
                
                var result = await dataLoader.DeleteDocument(CouchDBConnectorDocumentModel);
                if (result != null)
                {
                    TempData["success"] = "Document Deleted successfully";
                    //info = "Document " + UserInput._id + " has been deleted.";
                    info = "Below doument has been deleted:";
                    //return RedirectToPage("delete", UserInput);
                    return Page();
                }
            }
            catch
            {
                TempData["success"] = "No Document found!. Please check your entry";
            }
            //return RedirectToPage("document_data", UserInput);
            return Page();
        }
    }
}

