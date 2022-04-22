namespace OnlineAdvising.Core.Models
{
    public class UserFileModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
        public byte[] Value { get; set; }
    }
}