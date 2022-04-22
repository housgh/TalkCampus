using AutoMapper;
using OnlineAdvising.Core.Models;
using OnlineAdvising.Data.Entities;

namespace OnlineAdvising.Core.Mappings
{
    public class ModelToEntityMapping : Profile
    {
        public ModelToEntityMapping()
        {
            CreateMap<AppointmentModel, Appointment>();
            CreateMap<ChatModel, Chat>();
            CreateMap<DocumentModel, Document>();
            CreateMap<MessageModel, Message>();
            CreateMap<PsychologistModel, Psychologist>();
            CreateMap<RateModel, Rate>();
            CreateMap<ScheduleModel, PsychologistSchedule>();
            CreateMap<UserModel, User>();
            CreateMap<UserModel, Psychologist>();
            CreateMap<UserModel, Patient>();
            CreateMap<UserFileModel, UserFile>();
        }
    }
}