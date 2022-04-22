using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineAdvising.Core.Models;
using OnlineAdvising.Core.Results;

namespace OnlineAdvising.Core.Interfaces
{
    public interface IAppointmentService
    {
        Task<Result<AppointmentModel>> GetAsync(int id);
        Task<Result<List<AppointmentModel>>> GetPerUserAsync(int userId);
        Task<Result<int>> UpdateAsync(AppointmentModel model);
        Task<Result<int>> AddAsync(AppointmentModel model);
        Task<Result<int>> GetHoursServed(int psychologistId);
        Task<Result<int>> GetAppointmentsCount(int psychologistId);
        Task<Result<int>> DeclineAppointment(int id);
        Task<Result<int>> EndAppointment(int id);
    }
}