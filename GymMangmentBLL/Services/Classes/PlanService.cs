using GymMangmentBLL.Services.Interfaces;
using GymMangmentBLL.ViewModels.PlanViewModels;
using GymMangmentDAL.Entities;
using GymMangmentDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymMangmentBLL.Services.Classes
{
 public class PlanService : IPlanService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PlanService( IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IEnumerable<PlanViewModel> GetAllPlans()
        {
            var Plans = _unitOfWork.GetRepository<Plan>().GetAll();
            if (Plans == null || !Plans.Any()) return [];

            return Plans.Select(plan => new PlanViewModel()
            {
                Id = plan.Id,
                Name = plan.Name,
                Description = plan.Description,
                Price = plan.Price,
                DurationDays = plan.DurationDays,
                IsActive = plan.IsActive
            });

        }

        public PlanViewModel? GetPlanDetails(int planId)
        {
             var Plan = _unitOfWork.GetRepository<Plan>().GetById(planId);
            if (Plan == null) return null;
            return new PlanViewModel()
            {
                Id = Plan.Id,
                Name = Plan.Name,
                Description = Plan.Description,
                Price = Plan.Price,
                DurationDays = Plan.DurationDays,
                IsActive = Plan.IsActive
            };
        }

        public UpdatePlanViewModel? GetPlanToUpdate(int planId)
        {
            var Plan= _unitOfWork.GetRepository<Plan>().GetById(planId);
            if (Plan == null || Plan.IsActive== false || HasActiveMemberShips(planId)) return null;
            return new UpdatePlanViewModel()
            {
                PlanName = Plan.Name,
                Description = Plan.Description,
                DurationDays = Plan.DurationDays,
                Price = Plan.Price
            };
            
        }

        public bool ToggleStatus(int planId)
        {
            var Repo = _unitOfWork.GetRepository<Plan>();
            var Plan = Repo.GetById(planId);
            if (Plan == null || HasActiveMemberShips(planId)) return false;
            Plan.IsActive = Plan.IsActive == true ? false : true;
            try
            {
                Repo.Update(Plan);
                return _unitOfWork.SaveChanges() > 0;
            }
            catch
            {
                return false;

            }
        }

        public bool UpdatePlan(int planId, UpdatePlanViewModel UpdatedPlan)
        {
            try {
                var Repo = _unitOfWork.GetRepository<Plan>();
                var Plan = Repo.GetById(planId);
                if (Plan == null || HasActiveMemberShips(planId)) return false;
                (Plan.Description, Plan.DurationDays, Plan.Price, Plan.UpdatedAt)
                    = (UpdatedPlan.Description, UpdatedPlan.DurationDays, UpdatedPlan.Price, DateTime.Now);

                Repo.Update(Plan);
                return _unitOfWork.SaveChanges() > 0;
            }
            catch
            {
                return false;
            }

        }

        #region Helper Methods

        private bool HasActiveMemberShips(int planId)
        {
            return _unitOfWork.GetRepository<MemberShip>().GetAll(x => x.Id == planId && x.Status == "Active").Any();
        }

        #endregion
    }
}
