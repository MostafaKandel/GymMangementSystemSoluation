using AutoMapper;
using GymMangmentBLL.Services.Interfaces;
using GymMangmentBLL.ViewModels.SessionViewModel;
using GymMangmentDAL.Entities;
using GymMangmentDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymMangmentBLL.Services.Classes
{
    internal class SessionService : ISessionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SessionService(IUnitOfWork unitOfWork, IMapper mapper )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public IEnumerable<SessionViewModel> GetAllSessions()
        {
            var Session = _unitOfWork.SessionRepository.GetAllSessionsWithTrainerAndCategories();
            if (Session == null || !Session.Any()) return [];

            #region Manual Mapping

            //return Session.Select(session => new SessionViewModel()
            //{
            //    Id = session.Id,
            //    Description = session.Description,
            //    StartDate = session.StartDate,
            //    EndDate = session.EndDate,
            //    Capacity = session.Capacity,
            //    CategoryName = session.SessionCategory.CategoryName,
            //    AvilableSlots = session.Capacity - _unitOfWork.SessionRepository.GetCountOfBookedSlots(session.Id),
            //    TrainerName = session.SessionTrainer.Name,
            //});
            #endregion
            #region AutoMapper Mapping

            var MappedSessions= _mapper.Map<IEnumerable<Session>, IEnumerable<SessionViewModel>>(Session);
            return MappedSessions;

            #endregion
        }

        public SessionViewModel? GetSessionById(int id)
        {
             var Session= _unitOfWork.SessionRepository.GetSessionByIdWithTrainerAndCategories(id);
            if (Session == null) return null;
            var MappedSession= _mapper.Map<Session, SessionViewModel>(Session);
            return MappedSession;
        }

        public bool CreateSession(CreateSessionViewModel createSession)
        {
            try
            {
                if (!IsTrainerExist(createSession.TrainerId)) return false;
                if (!IsCategoryExist(createSession.CategoryId)) return false;
                if (!IsValidDateRange(createSession.StartDate, createSession.EndDate)) return false;

                var MappedSession = _mapper.Map<CreateSessionViewModel, Session>(createSession);
                _unitOfWork.SessionRepository.Add(MappedSession);

                return _unitOfWork.SaveChanges() > 0;
            }
            catch
            {
                return false;
            }

        }


        public UpdateSessionViewModel? GetSessionToUpdate(int id)
        {
            var Session = _unitOfWork.GetRepository<Session>().GetById(id);
            if (!IsSessionAvilableForUpdating(Session!)) return null;
            return _mapper.Map<Session, UpdateSessionViewModel>(Session!);

        }

        public bool UpdateSession(UpdateSessionViewModel updateSession, int id)
        {
            try
            {
                var Session = _unitOfWork.GetRepository<Session>().GetById(id);
                if (!IsSessionAvilableForUpdating(Session!)) return false;
                if (!IsTrainerExist(updateSession.TrainerId)) return false;
                if (!IsValidDateRange(updateSession.StartDate, updateSession.EndDate)) return false;
                _mapper.Map(updateSession, Session);
                Session!.UpdatedAt = DateTime.Now;
                _unitOfWork.GetRepository<Session>().Update(Session);
                return _unitOfWork.SaveChanges() > 0;
            }
            catch
            {
                return false;
            }

        }


        public bool RemoveSession(int id)
        {
            try
            {
                var Sesson = _unitOfWork.SessionRepository.GetById(id);

                if (!IsSessionAvilableForRemoving(Sesson)) return false;

                _unitOfWork.SessionRepository.Delete(Sesson);

                return _unitOfWork.SaveChanges() > 0;
            }
            catch { return false; }

        }


        #region Helper

        private bool IsTrainerExist(int id) { 
            return _unitOfWork.GetRepository<Trainer>().GetById(id) != null;
        }

        private bool IsCategoryExist(int id) { 
            return _unitOfWork.GetRepository<Category>().GetById(id) != null;
        }

        private bool IsValidDateRange(DateTime StartDate,  DateTime EndDate)
        {
            return StartDate < EndDate && StartDate > DateTime.Now;
        }

        private bool IsSessionAvilableForUpdating(Session session)
        {
            if (session == null) return false;
            if (session.EndDate< DateTime.Now) return false;

            if(session.StartDate<= DateTime.Now) return false;

            var HasActiveBookings = _unitOfWork.SessionRepository.GetCountOfBookedSlots(session.Id) > 0;

            if (HasActiveBookings) return false;

            return true;



        }


        private bool IsSessionAvilableForRemoving(Session session)
        {
            if (session == null) return false;
            if (session.StartDate > DateTime.Now) return false;

            if (session.StartDate <= DateTime.Now && session.EndDate> DateTime.Now) return false;

            var HasActiveBookings = _unitOfWork.SessionRepository.GetCountOfBookedSlots(session.Id) > 0;

            if (HasActiveBookings) return false;

            return true;



        }



        #endregion
    }
}
