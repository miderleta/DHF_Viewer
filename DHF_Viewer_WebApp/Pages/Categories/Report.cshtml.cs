using DHF_Viewer_WebApp.Interfaces;
using DHF_Viewer_WebApp.Utilities;
using DHF_Viewer_WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DHF_Viewer_WebApp.Pages.Categories
{
    public class ReportModel : PageModel
    {
        [BindProperty]
        public ReturnedData UserInput { get; set; }
        public DocumentModel CouchDBConnectorDocumentModel { get; set; }
        public List<DocumentModel> Results { get; set; }
        public String? output { get; set; }
        public void OnGet()
        {
            Results = new List<DocumentModel>();
        }

        public async Task<IActionResult> OnPost()
        {
            CouchDBConnectorDocumentModel = new DocumentModel();
            CouchDBConnectorDocumentModel.Product = UserInput.Product;

            //Init HttpClient and CouchProcessor objects
            IApiHelper client = new ApiHelper();
            client.InitializeCouchClient();
            ICouchProcessor dataLoader = new CouchProcessor();

            if (CouchDBConnectorDocumentModel.Product == "all_products")
            {
                try
                {
                    var result = await dataLoader.ReportAllDocuments();
                    Results = new List<DocumentModel>();
                    for (var i = 0; i < result.Rows.Count; i++)
                    {
                        Results.Add(result.Rows[i].value);
                    }
                    TempData["success"] = "Data Retrieved successfully";
                }
                catch
                {
                    TempData["success"] = "We have an error. Please check your entry";
                }
                //return RedirectToPage("update", UserInput);
                return Page();

            }
            else
            {
                //call API
                try
                {
                    var result = await dataLoader.ReportByProduct(CouchDBConnectorDocumentModel);
                    Results = new List<DocumentModel>();
                    for (var i = 0; i < result.Rows.Count; i++)
                    {
                        Results.Add(result.Rows[i].value);
                    }

                    if (result != null)
                    {
                        TempData["success"] = "Data Retrieved successfully";
                    }
                }
                catch
                {
                    TempData["success"] = "We have an error. Please check your entry";
                }
                //return RedirectToPage("update", UserInput);
                return Page();
            }
        }
    }
}
