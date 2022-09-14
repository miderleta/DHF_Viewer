using CouchDBConnector;
using CouchDBConnector.Interfaces;
using CouchDBConnector.Models;
using DHF_Viewer_WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DHF_Viewer_WebApp.Pages.Categories
{
    public class SearchModel : PageModel
    {
        [BindProperty]
        public ReturnedData UserInput { get; set; }

        public DocumentModel CouchDBConnectorDocumentModel { get; set; }
        public List<DocumentModel> Results { get; set; }

        public void OnGet()
        {
            Results = new List<DocumentModel>();
        }

        public async Task<IActionResult> OnPost()
        {
            CouchDBConnectorDocumentModel = new DocumentModel();
            //UserInput._id = Request.Form["UserInput._id"];
            //var documentNumber = UserInput._id;
            CouchDBConnectorDocumentModel._id = UserInput._id;
            CouchDBConnectorDocumentModel.Title = UserInput.Title;

            //Init HttpClient and CouchProcessor objects
            IApiHelper client = new ApiHelper();
            client.InitializeCouchClient();
            ICouchProcessor dataLoader = new CouchProcessor();

            //call API
            if (CouchDBConnectorDocumentModel._id != null)
            {
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
                        return RedirectToPage("document_data", UserInput);
                    }
                }
                catch
                {
                    TempData["success"] = "No Document found!. Please check your entry";
                }
            }
            else
            {
                //code to search by doc name
                try
                {
                    var result = await dataLoader.SearchByDocumentName(CouchDBConnectorDocumentModel);
                    Results = new List<DocumentModel>();
                    for (var i = 0; i < result.Rows.Count; i++)
                    {
                        Results.Add(result.Rows[i].value);
                    }
                    UserInput._rev = Results[0]._rev;
                    UserInput._id = Results[0]._id;
                    UserInput.Title = Results[0].Title;
                    UserInput.Revision = Results[0].Revision;
                    UserInput.Product = Results[0].Product;
                    UserInput.Type = Results[0].Type;
                    TempData["success"] = "Document Retrieved successfully";
                    return RedirectToPage("document_data", UserInput);
                }
                catch
                {
                    TempData["success"] = "We have an error. Please check your entry";
                }
                //return RedirectToPage("update", UserInput);
                return Page();

            }
            //return RedirectToPage("document_data", UserInput);
            return Page();
        }
    }
}

