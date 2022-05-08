using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouchDBConnector
{
    public class NewDocumentModel
    {
        public string documentNumber { get; set; }
        public string documentDesc { get; set; }
        public string documentRev { get; set; }
        public string documentType { get; set; }
    }
}
