using System;

namespace OnlineAdvising.Data.Entities
{
    public class Document
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public DateTime UploadedOn { get; set; }
    }
}