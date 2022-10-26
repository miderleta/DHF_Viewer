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
        public DocumentModel DocumentData { get; set; }
        public ReturnedData MyData { get; set; }

        private readonly ICouchProcessor _couchProcessor;

        public UpdateModel(ICouchProcessor couchProcessor)
        {
            _couchProcessor = couchProcessor;
        }

        public void OnGet(ReturnedData UserInput)
        {
            this.UserInput = UserInput;
        }

        public async Task<IActionResult> OnPost()
        {
            DocumentData = new DocumentModel();
            DocumentData._rev = Request.Form["UserInput._rev"];
            DocumentData._id = Request.Form["UserInput._id"];
            DocumentData.Title = Request.Form["UserInput.Title"];
            DocumentData.Revision = Request.Form["UserInput.Revision"];
            DocumentData.Product = Request.Form["UserInput.Product"];
            DocumentData.Type = Request.Form["UserInput.Type"];

            //call API
            try
            {
                var result = await _couchProcessor.UpdateDocument(DocumentData);
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
