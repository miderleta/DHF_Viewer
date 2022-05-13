using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouchDBConnector.Models
{
    public class DocumentModel
    {
        public string? _id { get; set; }
        public string? _rev { get; set; }
        public string? Title { get; set; }
        public string? Type { get; set; }
        public string? Revision { get; set; }
        public string? Product { get; set; }
    }
}
