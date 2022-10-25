using DHF_Viewer_WebApp.Models;
using DHF_Viewer_WebApp.Interfaces;
using DHF_Viewer_WebApp.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DHF_Viewer_WebApp.Pages.Categories
{
    public class CreateModel : PageModel
    {
       
        [BindProperty]
        public ReturnedData UserInput { get; set; }
        public DocumentModel CouchDBConnectorDocumentModel { get; set; }
        public String? output { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            if(ModelState.IsValid)
            {
                output = UserInput._id + " " + UserInput.Title + " " +
                    UserInput.Revision + " " + UserInput.Type;

                CouchDBConnectorDocumentModel = new DocumentModel();

                CouchDBConnectorDocumentModel._id = UserInput._id;
                CouchDBConnectorDocumentModel._rev = "";
                CouchDBConnectorDocumentModel.Title = UserInput.Title;
                CouchDBConnectorDocumentModel.Revision = UserInput.Revision;
                CouchDBConnectorDocumentModel.Product = UserInput.Product;
                CouchDBConnectorDocumentModel.Type = UserInput.Type;

                //Init HttpClient and CouchProcessor objects
                IApiHelper client = new ApiHelper();
                client.InitializeCouchClient();
                ICouchProcessor dataLoader = new CouchProcessor();

                //call API
                try
                {
                    var result = await dataLoader.CreateNewDocument(CouchDBConnectorDocumentModel);
                    if(result != null)
                    {
                        TempData["success"] = "New Document created successfully";
                    }
                }
                catch
                {
                    TempData["success"] = "We have an error. Please check your entry";
                }                
            }
            return Page();

            
        }
    }
}
