using System;

namespace OnlineAdvising.Core.Models
{
    public class DocumentModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public DateTime UploadedOn { get; set; }
    }
}