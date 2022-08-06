using CouchDBConnector;
using CouchDBConnector.Interfaces;
using DHF_Viewer_WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DHF_Viewer_WebApp.Pages.Categories
{
    public class CreateModel : PageModel
    {
        public Task<CouchModel>? receivedData;

        public void OnGet()
        {
            //Init HttpClient and CouchProcessor objects
            IApiHelper client = new ApiHelper();
            client.InitializeCouchClient();
            ICouchProcessor dataLoader = new CouchProcessor();

            //Get data from CouchDB
            receivedData = dataLoader.LoadCouchData();
        }
    }
}
