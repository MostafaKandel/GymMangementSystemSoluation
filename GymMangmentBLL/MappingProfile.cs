using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using GymMangmentBLL.ViewModels.MemeberViewModel;
using GymMangmentBLL.ViewModels.SessionViewModel;
using GymMangmentDAL.Entities;

namespace GymMangmentBLL
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
          MapSession();
            MapMember();
          

        }

        
        private void MapSession()
        {
            CreateMap<Session, SessionViewModel>()
             .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.SessionCategory.CategoryName))
             .ForMember(dest => dest.TrainerName, opt => opt.MapFrom(src => src.SessionTrainer.Name))
             .ForMember(dest => dest.AvilableSlots, opt => opt.Ignore());

            CreateMap<CreateSessionViewModel, Session>().ReverseMap();
            CreateMap<Session, UpdateSessionViewModel>().ReverseMap();
            CreateMap<Category, CategorySelectViewModel>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.CategoryName));
            CreateMap<Trainer, TrainerSelectViewModel>();
             
        }
 
        
        private void MapMember()
        {
            CreateMap<CreateMemberViewModel, Member>()
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => new Address()
                {
                    BuildingNumber = src.BuildingNumber,
                    Street = src.Street,
                    City = src.City
                }))
                .ForMember(dest => dest.HealthRecord, opt => opt.MapFrom(src => new HealthRecord
                {
                    Weight = src.HealthRecordViewModel.Weight,
                    Height = src.HealthRecordViewModel.Height,
                    BloodType = src.HealthRecordViewModel.BloodType,
                    Note = src.HealthRecordViewModel.Note
                }));

            CreateMap<Member, MemberViewModel>()
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender.ToString()))
                .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth.ToShortDateString()))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => $"{src.Address.BuildingNumber}, {src.Address.Street}, {src.Address.City}"));

            CreateMap<HealthRecord, HealthRecordViewModel>();
            CreateMap<Member, MemberToUpdateViewModel> ()
                .ForMember(dest => dest.BuildingNumber, opt => opt.MapFrom(src => src.Address.BuildingNumber))
                .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Address.Street))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Address.City));

            CreateMap<MemberToUpdateViewModel, Member> ()
                .ForMember(dest=> dest.Name, opt=> opt.Ignore())
                .ForMember(dest=> dest.Photo, opt=> opt.Ignore())
                .AfterMap((src, dest) =>
                {
                    
                    dest.Address.BuildingNumber = src.BuildingNumber;
                    dest.Address.Street = src.Street;
                    dest.Address.City = src.City;
                    dest.UpdatedAt = DateTime.Now;
                });
        }   
    }
}
