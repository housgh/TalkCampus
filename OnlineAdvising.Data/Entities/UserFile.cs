using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineAdvising.Data.Entities
{
    public class UserFile
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
        public byte[] Value { get; set; }
        
        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}