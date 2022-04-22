using AutoMapper;
using OnlineAdvising.Core.Models;
using OnlineAdvising.Data.Entities;

namespace OnlineAdvising.Core.Mappings
{
    public class EntityToModelMapping : Profile
    {
        public EntityToModelMapping()
        {
            CreateMap<Appointment, AppointmentModel>();
            CreateMap<Chat, ChatModel>();
            CreateMap<Document, DocumentModel>();
            CreateMap<Message, MessageModel>();
            CreateMap<Psychologist, PsychologistModel>();
            CreateMap<Rate, RateModel>();
            CreateMap<PsychologistSchedule, ScheduleModel>();
            CreateMap<User, UserModel>();
        }
    }
}