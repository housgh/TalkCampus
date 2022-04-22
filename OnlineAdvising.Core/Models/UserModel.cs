using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace OnlineAdvising.Core.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public bool HasAppointment { get; set; }
        public string Biography { get; set; }
        public int UpcomingChatId { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int? RoleId { get; set; }
        public int? AccountStatusId { get; set; }
        public DateTime JoinedDate { get; set; }
    }
}