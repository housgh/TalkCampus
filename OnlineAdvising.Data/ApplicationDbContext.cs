using Microsoft.EntityFrameworkCore;
using OnlineAdvising.Data.Entities;
using OnlineAdvising.Data.ProcedureModels;

namespace OnlineAdvising.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }
        
        public virtual DbSet<Appointment> Appointments { get; set; }
        public virtual DbSet<DayOfWeek> DaysOfWeek { get; set; }
        public virtual DbSet<PsychologistSchedule> PsychologistSchedules { get; set; }
        public virtual DbSet<Rate> Rates { get; set; }
        public virtual DbSet<Report> Reports { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<ScheduleDay> ScheduleDays { get; set; }
        public virtual DbSet<Status> Status { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Psychologist> Psychologists { get; set; }
        public virtual DbSet<Patient> Patients { get; set; }
        public virtual DbSet<Chat> Chats { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<PsychologistDashboard> PsychologistDashboard { get; set; }
        public virtual DbSet<UserFile> UserFiles { get; set; }
    }
}