using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouchDBConnector
{
    public class CouchModel
    {
        public string? CouchDB { get; set; }
        public string Version { get; set; }

        //for recipes db
        public string DB_Name { get; set; }
        public object Sizes { get; set; }

        //for dhf_viewer db
        public string Doc_Count { get; set; }

    }
}
