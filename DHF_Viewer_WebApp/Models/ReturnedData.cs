using System.ComponentModel.DataAnnotations;

namespace DHF_Viewer_WebApp.Models
{
    public class ReturnedData
    {
        public string? DB_Name { get; set; }
        public string? Doc_Count { get; set; }
        [StringLength(12)]
        [RegularExpression((@"^[A-Z]+[a-zA-Z0-9""'\s-]*$"), ErrorMessage = "Document ID must be in Format: HXXX-XXX-XXX or MXXXXXXXXXX")]
        public string? _id { get; set; }
        public string? _rev { get; set; }
        public string? Title { get; set; }
        public string? Type { get; set; }
        public string? Revision { get; set; }
        public string? Product { get; set; }

    }
}
