﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouchDBConnector.Models
{
    public class UpdateDocumentModel
    {
        public string? Title { get; set; }
        public string? _rev { get; set; }
        public string? Type { get; set; }
        public string? Revison { get; set; }
        public string? Product { get; set; }
    }
}
