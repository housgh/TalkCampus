using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using OnlineAdvising.Data.Entities;
using OnlineAdvising.Data.ProcedureModels;

namespace OnlineAdvising.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

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
        public virtual DbSet<AdminDashboardPatient> AdminDashboardPatients { get; set; }
        public virtual DbSet<AdminDashboardPsychologist> AdminDashboardPsychologists { get; set; }
        public virtual DbSet<UserFile> UserFiles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(new List<Role>()
            {
                new() { Id = 1, Name = "Psychologist" },
                new() { Id = 2, Name = "Patient" },
                new() { Id = 3, Name = "Admin" }
            });

            modelBuilder.Entity<Status>().HasData(new List<Status>()
            {
                new() { Id = 1, Name = "Active" },
                new() { Id = 2, Name = "Declined" },
                new() { Id = 3, Name = "Pending" }
            });
            
            modelBuilder.Entity<User>().HasData(new List<User>()
            {
                new()
                {
                    Id = 1,
                    Email = "patient.user@email.com",
                    Username = "patient.user",
                    PasswordHash = "69E45613E534DAB256A77929D08C579E7025FC8CA2D48C86898556AF54E460FB",
                    FirstName = "Patient",
                    LastName = "User",
                    RoleId = 2,
                    AccountStatusId = 1,
                },
                new()
                {
                    Id = 2,
                    Email = "psychologist.user@email.com",
                    Username = "psychologist.user",
                    PasswordHash = "69E45613E534DAB256A77929D08C579E7025FC8CA2D48C86898556AF54E460FB",
                    FirstName = "Psychologist",
                    LastName = "User",
                    RoleId = 1,
                    AccountStatusId = 1,
                },
                new()
                {
                    Id = 3,
                    Email = "admin.user@email.com",
                    Username = "admin.user",
                    PasswordHash = "69E45613E534DAB256A77929D08C579E7025FC8CA2D48C86898556AF54E460FB",
                    FirstName = "Admin",
                    LastName = "User",
                    RoleId = 3,
                    AccountStatusId = 1,
                },
            });
            base.OnModelCreating(modelBuilder);
        }
    }
}