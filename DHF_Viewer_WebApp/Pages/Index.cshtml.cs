using DHF_Viewer_WebApp.Interfaces;
using DHF_Viewer_WebApp.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DHF_Viewer_WebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IApiHelper _apiHelper;
        private readonly ICouchProcessor _couchProcessor;
        public Task<CouchModel>? receivedData;

        public IndexModel(IApiHelper apiHelper, ICouchProcessor couchProcessor)
        {
            _apiHelper = apiHelper;
            _couchProcessor = couchProcessor;
        }

        public void OnGet()
        {
            //use dependency injection
            _apiHelper.InitializeCouchClient();
           
            //Get data from CouchDB
            receivedData = _couchProcessor.LoadCouchData();
        }
    }
}