using DHF_Viewer_WebApp.Interfaces;
using DHF_Viewer_WebApp.Utilities;
using DHF_Viewer_WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DHF_Viewer_WebApp.Pages.Categories
{
    public class SearchModel : PageModel
    {
        [BindProperty]
        public ReturnedData UserInput { get; set; }

        public DocumentModel DocumentData { get; set; }
        public List<DocumentModel> Results { get; set; }

        private readonly ICouchProcessor _couchProcessor;

        public SearchModel(ICouchProcessor couchProcessor)
        {
            _couchProcessor = couchProcessor;
        }

        public void OnGet()
        {
            Results = new List<DocumentModel>();
        }

        public async Task<IActionResult> OnPost()
        {
            DocumentData = new DocumentModel();
            DocumentData._id = UserInput._id;
            DocumentData.Title = UserInput.Title;

            //call API
            if (DocumentData._id != null)
            {
                try
                {
                    var result = await _couchProcessor.ReadDocumentData(DocumentData);
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
                    var result = await _couchProcessor.SearchByDocumentName(DocumentData);
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

