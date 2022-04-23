using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using OnlineAdvising.Data.Entities;

namespace OnlineAdvising.Data.ProcedureModels
{

    public class AdminDashboardPatient
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public int AppointmentsCount { get; set; }
        public int TimesReported { get; set; }
    }
    public class AdminDashboardPsychologist
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Degree { get; set; }
        public int HoursServed { get; set; }
        public int StudentsHelped { get; set; }
        public double Rating { get; set; }
        public int TimesReported { get; set; }
        public int Status { get; set; }
        [NotMapped]
        public virtual List<UserFile> Files { get; set; }
    }
    
    public class AdminDashboard
    {
        public List<AdminDashboardPatient> Patients { get; set; }
        public List<AdminDashboardPsychologist> Psychologists { get; set; }
    }
}