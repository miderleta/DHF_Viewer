using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouchDBConnector
{
    public class TestAPICalling
    {
        HttpClient client = new HttpClient();
        Uri url = new Uri("http://userM:XCSirz12@127.0.0.1:5984/recipes");

        public string CallResult()
        {
            using (client)
            {
                var  result = client.GetAsync(url).Result;
                var json = result.Content.ReadAsStringAsync().Result;

                return json;
            }
        }

    }
}
