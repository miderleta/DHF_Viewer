using CouchDBConnector;
using CouchDBConnector.Interfaces;
using CouchDBConnector.Models;
using DHF_Viewer_WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DHF_Viewer_WebApp.Pages.Categories
{
    public class EditModel : PageModel
    {
        [BindProperty]
        public ReturnedData UserInput { get; set; }
        
        public DocumentModel CouchDBConnectorDocumentModel { get; set; }

        public void OnGet()
        {
            //UserInput = new ReturnedData();
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
                var result = await dataLoader.ReadDocumentData(CouchDBConnectorDocumentModel);
                if (result != null)
                {
                    UserInput._rev = result._rev;
                    UserInput._id = result._id;
                    UserInput.Title = result.Title;
                    UserInput.Revision = result.Revision;
                    UserInput.Product = result.Product;
                    UserInput.Type = result.Type;
                    TempData["success"] = "Document Retrieved successfully";
                }
            }
            catch
            {
                TempData["success"] = "We have an error. Please check your entry";
            }
            return RedirectToPage("update", UserInput);
        }
    }
}