using System.ComponentModel.DataAnnotations;

namespace OnlineAdvising.Data.ProcedureModels
{
    public class PsychologistDashboard
    {
        [Key]
        public int PsychologistId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Degree { get; set; }
        public string Biography { get; set; }
        public int? StudentsHelped { get; set; }
        public double? AverageRating { get; set; }
        public int? HoursServed { get; set; }
        public int AccountStatusId { get; set; }
    }
}