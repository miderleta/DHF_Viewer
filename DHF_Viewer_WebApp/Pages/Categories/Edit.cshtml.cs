using DHF_Viewer_WebApp.Interfaces;
using DHF_Viewer_WebApp.Utilities;
using DHF_Viewer_WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DHF_Viewer_WebApp.Pages.Categories
{
    public class EditModel : PageModel
    {
        [BindProperty]
        public ReturnedData UserInput { get; set; }
        
        public DocumentModel DocumentData { get; set; }
        public DocumentModel NewDocData { get; set; }

        private readonly ICouchProcessor _couchProcessor;

        public EditModel(ICouchProcessor couchProcessor)
        {
            _couchProcessor = couchProcessor;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            DocumentData = new DocumentModel();
            DocumentData._id = UserInput._id;

            //call API
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
                    return RedirectToPage("update", UserInput);
                }
            }
            catch
            {
                TempData["success"] = "The document ID you entered does not exist. Please check your entry";
            }
            return Page();
            //return RedirectToPage("update", UserInput);
        }
    }
}
