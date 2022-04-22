using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineAdvising.Data.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int? RoleId { get; set; }
        public int? AccountStatusId { get; set; }
        public DateTime JoinedDate { get; set; }
        
        [ForeignKey("RoleId")]
        public virtual Role Role { get; set; }
        [ForeignKey("AccountStatusId")]
        public virtual Status AccountStatus { get; set; }
    }
}