using DHF_Viewer_WebApp.Interfaces;
using DHF_Viewer_WebApp.Utilities;
using DHF_Viewer_WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DHF_Viewer_WebApp.Pages.Categories
{
    public class DeleteModel : PageModel
    {
        private readonly ICouchProcessor _couchProcessor;
        [BindProperty]
        public ReturnedData UserInput { get; set; }
        public DocumentModel DocumentData { get; set; }
        public String? info { get; set; }

        public DeleteModel(ICouchProcessor couchProcessor)
        {
            _couchProcessor = couchProcessor;
        }

        public void OnGet()
        {
            UserInput = new ReturnedData();
        }

        public async Task<IActionResult> OnPost()
        {
            DocumentData = new DocumentModel();
            DocumentData._id = UserInput._id;

            //Init HttpClient and CouchProcessor objects
            //IApiHelper client = new ApiHelper();
            //client.InitializeCouchClient();
            //ICouchProcessor dataLoader = new CouchProcessor();

            //call API
            try
            {
                //Retrive doc data for disply to user
                var docData = await _couchProcessor.ReadDocumentData(DocumentData);
                if (docData != null)
                {
                    UserInput._rev = docData._rev;
                    UserInput._id = docData._id;
                    UserInput.Title = docData.Title;
                    UserInput.Revision = docData.Revision;
                    UserInput.Product = docData.Product;
                    UserInput.Type = docData.Type;
                }
                
                var result = await _couchProcessor.DeleteDocument(DocumentData);
                if (result != null)
                {
                    TempData["success"] = "Document Deleted successfully";
                    info = "Below doument has been deleted:";
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

