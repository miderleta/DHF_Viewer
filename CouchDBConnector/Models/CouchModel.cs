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

        public string DB_Name { get; set; }
        public object Sizes { get; set; }

        public string Doc_Count { get; set; }

    }
}
