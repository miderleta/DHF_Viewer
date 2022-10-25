using DHF_Viewer_WebApp.Interfaces;
using DHF_Viewer_WebApp.Utilities;
using DHF_Viewer_WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DHF_Viewer_WebApp.Pages.Categories
{
    public class UpdateModel : PageModel
    {
        [BindProperty]
        public ReturnedData UserInput { get; set; }
        public DocumentModel CouchDBConnectorDocumentModel { get; set; }
        public ReturnedData MyData { get; set; }

        public void OnGet(ReturnedData UserInput)
        {
            this.UserInput = UserInput;
        }

        public async Task<IActionResult> OnPost()
        {
            CouchDBConnectorDocumentModel = new DocumentModel();
            CouchDBConnectorDocumentModel._rev = Request.Form["UserInput._rev"];
            CouchDBConnectorDocumentModel._id = Request.Form["UserInput._id"];
            CouchDBConnectorDocumentModel.Title = Request.Form["UserInput.Title"];
            CouchDBConnectorDocumentModel.Revision = Request.Form["UserInput.Revision"];
            CouchDBConnectorDocumentModel.Product = Request.Form["UserInput.Product"];
            CouchDBConnectorDocumentModel.Type = Request.Form["UserInput.Type"];

            //Init HttpClient and CouchProcessor objects
            IApiHelper client = new ApiHelper();
            client.InitializeCouchClient();
            ICouchProcessor dataLoader = new CouchProcessor();

            //call API
            try
            {
                var result = await dataLoader.UpdateDocument(CouchDBConnectorDocumentModel);
                if (result != null)
                {
                    TempData["success"] = "Document Updated successfully";
                    return RedirectToPage("edit");
                }
            }
            catch
            {
                TempData["success"] = "We have an error. Please check your entry";
            }
            return Page();
            
        }
    }
}
