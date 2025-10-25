using AutoMapper;
using GymMangmentBLL.Services.Interfaces;
using GymMangmentBLL.ViewModels.MemeberViewModel;
using GymMangmentDAL.Entities;
using GymMangmentDAL.Repositories.Classes;
using GymMangmentDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymMangmentBLL.Services.Classes
{
   public class MemberService : IMemberService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MemberService( IUnitOfWork unitOfWork, IMapper mapper ) {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

         

        public bool CreateMember(CreateMemberViewModel createMember)
        {
            try {

                if (IsEmailExists(createMember.Email) || IsPhoneExists(createMember.Phone))
                {
                    return false;
                }

                // createMemeberViewModel to Memeber -> Mapping manuall
                #region manual mapping
                //var member = new Member()
                //{
                //    Name = createMember.Name,
                //    Email = createMember.Email,
                //    Phone = createMember.Phone,
                //    DateOfBirth = createMember.DateOfBirth,
                //    Gender = createMember.Gender,
                //    Address = new Address()
                //    {
                //        BuildingNumber = createMember.BuildingNumber,
                //        Street = createMember.Street,
                //        City = createMember.City,
                //    },
                //    HealthRecord = new HealthRecord()
                //    {
                //        Weight = createMember.HealthRecordViewModel.Weight,
                //        Height = createMember.HealthRecordViewModel.Height,
                //        BloodType = createMember.HealthRecordViewModel.BloodType,
                //        Note = createMember.HealthRecordViewModel.Note,

                //    }
                //};
                #endregion

                var MappedMember = _mapper.Map<CreateMemberViewModel,Member>(createMember);
                _unitOfWork.GetRepository<Member>().Add(MappedMember);
                return _unitOfWork.SaveChanges() > 0;

            }
            catch 
            {
                 return false;
            }
           
        }


        public IEnumerable<MemberViewModel> GetAllMembers()
        {
            var Memebers =_unitOfWork.GetRepository<Member>().GetAll();
            if (Memebers == null || !Memebers.Any())
            {
                return [];
            }


            #region method 1 manuall mapping
            //var MemberViewModels = new List<MemberViewModel>();
            //foreach (var member in Memebers)
            //{
            //    var memberViewModel = new MemberViewModel()

            //    {
            //        Id = member.Id,
            //        Name = member.Name,
            //        Email = member.Email,
            //        Phone = member.Phone,
            //        Photo = member.Photo,
            //        Gender = member.Gender.ToString()
            //    };
            //    MemberViewModels.Add(memberViewModel);
            //}
            //return MemberViewModels;
            #endregion

            #region method 2 using linq
            //var MemberViewModels = Memebers.Select(member => new MemberViewModel()
            //{
            //    Id = member.Id,
            //    Name = member.Name,
            //    Email = member.Email,
            //    Phone = member.Phone,
            //    Photo = member.Photo,
            //    Gender = member.Gender.ToString(),

            //}
            //);
            //return MemberViewModels;
            #endregion
        
            var MemberViewModels = _mapper.Map<IEnumerable<Member>, IEnumerable<MemberViewModel>>(Memebers);
            return MemberViewModels;

        }

        public MemberViewModel? GetMemberDetails(int MemberId)
        {
             var member = _unitOfWork.GetRepository<Member>().GetById(MemberId);
            if (member == null) return null;
            // Member to MemberViewModel -> Mapping
            var  viewModel = _mapper.Map<Member, MemberViewModel>(member);

            var ActivememberShip =_unitOfWork.GetRepository<MemberShip>().GetAll(ms => ms.MemberId == MemberId && ms.Status == "Active").FirstOrDefault();
            if (ActivememberShip != null)
            {
              
                viewModel.MemberShipStratDate = ActivememberShip.CreatedAt.ToShortDateString();
                viewModel.MemberShipEndDate = ActivememberShip.EndDate.ToShortDateString();
                var plan = _unitOfWork.GetRepository<Plan>().GetById(ActivememberShip.PlanId);
                viewModel.PlanName= plan?.Name;
            }
            return viewModel;
        }

        public HealthRecordViewModel? GetMemberHealthRecordDetails(int MemberId)
        {
            var MemberHealthRecord =_unitOfWork.GetRepository<HealthRecord>().GetById(MemberId);

            if (MemberHealthRecord == null) return null;

            // HealthRecord to HealthRecordViewModel -> Mapping
            return  _mapper.Map<HealthRecord, HealthRecordViewModel>(MemberHealthRecord);
        }

        public MemberToUpdateViewModel? GetMemberToUpdate(int MemberId)
        {
            var member = _unitOfWork.GetRepository<Member>().GetById(MemberId);
            if (member == null) return null;
            // Member to MemberToUpdateViewModel -> Mapping
            return _mapper.Map<Member, MemberToUpdateViewModel>(member);
        }

        public bool RemoveMember(int MemberId)
        {
            try
            {
                var member =_unitOfWork.GetRepository<Member>().GetById(MemberId);
                if (member == null) return false;
                // check if member active in session or not 
                var HasActiveMemberSessions = _unitOfWork.GetRepository<MemberSession>().GetAll(X=>X.MemberId == MemberId && X.Session.StartDate> DateTime.Now).Any();
                if (HasActiveMemberSessions) return false;

                var MemberShips= _unitOfWork.GetRepository<MemberShip>().GetAll(ms => ms.MemberId == MemberId);
                if ( MemberShips.Any())
                {
                    foreach (var memberShip in MemberShips)
                        _unitOfWork.GetRepository<MemberShip>().Delete(memberShip);
                    
                }

                 _unitOfWork.GetRepository<Member>().Delete(member);
                return _unitOfWork.SaveChanges() > 0;



            }
            catch
            {
                return false;
            }
        }

        public bool UpdateMemberDetails(int MemberId, MemberToUpdateViewModel memberToUpdate)
        {
            try
            {
                var emailExit= _unitOfWork.GetRepository<Member>()
                    .GetAll(x=> x.Email == memberToUpdate.Email && x.Id != MemberId).Any();
                var phoneExit = _unitOfWork.GetRepository<Member>()
                    .GetAll(x => x.Phone == memberToUpdate.Phone && x.Id != MemberId).Any();

                if(emailExit || phoneExit) return false;

                var member =_unitOfWork.GetRepository<Member>().GetById(MemberId);
                if (member == null) return false;

                _mapper.Map(memberToUpdate, member);

                _unitOfWork.GetRepository<Member>().Update(member);
                return _unitOfWork.SaveChanges() > 0;

            }
            catch
            {
                return false;
            }
        }


        #region Helper functions
        private bool IsEmailExists(string email)
        {
            return _unitOfWork.GetRepository<Member>().GetAll(m => m.Email == email).Any();
        }
        private bool IsPhoneExists(string phone)
        {
            return _unitOfWork.GetRepository<Member>().GetAll(m => m.Phone == phone).Any();
        }

       
        #endregion
    }
}
