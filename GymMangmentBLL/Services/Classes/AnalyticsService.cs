using GymMangmentBLL.Services.Interfaces;
using GymMangmentBLL.ViewModels.AnalyticsViewModels;
using GymMangmentDAL.Entities;
using GymMangmentDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymMangmentBLL.Services.Classes
{
    public class AnalyticsService : IAnalyticsService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AnalyticsService(IUnitOfWork unitOfWork ) {
           _unitOfWork = unitOfWork;
        }
        public AnalyticsViewModel GetAnalyticsData()
        {
            return new AnalyticsViewModel()
            {
                ActiveMembers = _unitOfWork.GetRepository<MemberShip>().GetAll(m => m.Status == "Active").Count(),
                TotalMembers = _unitOfWork.GetRepository<Member>().GetAll().Count(),
                TotalTrainers = _unitOfWork.GetRepository<Trainer>().GetAll().Count(),
                UpcomingSessions = _unitOfWork.SessionRepository.GetAll().Count(x=> x.StartDate> DateTime.Now),
                OngoingSessions = _unitOfWork.SessionRepository.GetAll().Count(x => x.StartDate <= DateTime.Now && x.EndDate >= DateTime.Now),
                ComplotedSessions = _unitOfWork.SessionRepository.GetAll().Count(x => x.EndDate < DateTime.Now),
            };
        }
    }
}
