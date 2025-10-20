using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using GymMangmentBLL.ViewModels.SessionViewModel;
using GymMangmentDAL.Entities;

namespace GymMangmentBLL
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
             CreateMap<Session, SessionViewModel>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.SessionCategory.CategoryName))
                .ForMember(dest => dest.TrainerName, opt => opt.MapFrom(src => src.SessionTrainer.Name))
                .ForMember(dest => dest.AvilableSlots, opt => opt.Ignore());

            CreateMap<CreateSessionViewModel, Session>();
            CreateMap<Session, UpdateSessionViewModel >().ReverseMap();
             
        }
    }
}
