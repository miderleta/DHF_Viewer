using DHF_Viewer_WebApp.Models;
using DHF_Viewer_WebApp.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DHF_Viewer_WebApp.Pages.Categories
{
    public class CreateModel : PageModel
    {
        [BindProperty]
        public ReturnedData UserInput { get; set; }
        public DocumentModel NewDocumentData { get; set; }
        
        private readonly ICouchProcessor _couchProcessor;

        public CreateModel(ICouchProcessor couchProcessor)
        {
            _couchProcessor = couchProcessor;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            if(ModelState.IsValid)
            {
                NewDocumentData = new DocumentModel();

                NewDocumentData._id = UserInput._id;
                NewDocumentData._rev = "";
                NewDocumentData.Title = UserInput.Title;
                NewDocumentData.Revision = UserInput.Revision;
                NewDocumentData.Product = UserInput.Product;
                NewDocumentData.Type = UserInput.Type;

                //call API
                try
                {
                    var result = await _couchProcessor.CreateNewDocument(NewDocumentData);
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
