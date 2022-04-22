using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineAdvising.Data.Entities
{
    public class UserDocument
    {
        public int Id { get; set; }
        public int? DocumentId { get; set; }
        public int? UserId { get; set; }
        
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        [ForeignKey("DocumentId")]
        public virtual Document Document { get; set; }
        
    }
}