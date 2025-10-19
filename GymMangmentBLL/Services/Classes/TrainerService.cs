using GymMangmentBLL.Services.Interfaces;
using GymMangmentBLL.ViewModels.TrainerViewModel;
using GymMangmentDAL.Entities;
using GymMangmentDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymMangmentBLL.Services.Classes
{
    internal class TrainerService : ITrainerService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TrainerService( IUnitOfWork unitOfWork) {
            _unitOfWork = unitOfWork;
        }
        public bool CreateTrainer(CreateTrainerViewModel createTrainer)
        {
            try
            {
                var Repo= _unitOfWork.GetRepository<Trainer>();
                if (IsEmailExists(createTrainer.Email) || IsPhoneExists(createTrainer.Phone))
                {
                    return false;
                }
                var trainer = new Trainer()
                {
                    Name = createTrainer.Name,
                    Email = createTrainer.Email,
                    Phone = createTrainer.Phone,
                    DateOfBirth = createTrainer.DateOfBirth,
                    Gender =createTrainer.Gender,
                    Specialties = createTrainer.Specialities,
                    Address = new Address()
                    {
                        BuildingNumber = createTrainer.BuildingNumber,
                        Street = createTrainer.Street,
                        City = createTrainer.City,
                    }
                    };
                Repo.Add(trainer);
                return _unitOfWork.SaveChanges() > 0;

            }
            catch  
            {
                return false;
            }
        }


        public IEnumerable<TrainerViewModel> GetAllTrainers()
        {
            var trainers = _unitOfWork.GetRepository<Trainer>().GetAll();
            if (trainers == null || !trainers.Any())
            {
                return [];
            }
            var trainerViewModels = trainers.Select(t => new TrainerViewModel
            {
                Name = t.Name,
                Email = t.Email,
                Phone = t.Phone,
                Specialization = t.Specialties.ToString(),
                Id = t.Id,

            });
            return trainerViewModels;
        }

        public TrainerViewModel? GetTrainerDetails(int trainerId)
        {
            var trainer = _unitOfWork.GetRepository<Trainer>().GetById(trainerId);
            if (trainer == null) return null;
            return new TrainerViewModel()
            {
                
                Name = trainer.Name,
                Email = trainer.Email,
                Phone = trainer.Phone,
                Specialization = trainer.Specialties.ToString(),
                DateOfBirth = trainer.DateOfBirth.ToString("yyyy/MM/dd"),
                Address = trainer.Address != null ? $"{trainer.Address.BuildingNumber}, {trainer.Address.Street}, {trainer.Address.City}" : null
            };

        }

        public TrainerToUpdateViewModel? GetTrainerToUpdate(int trainerId)
        {
             var trainer = _unitOfWork.GetRepository<Trainer>().GetById(trainerId);
            if (trainer == null) return null;
            return new TrainerToUpdateViewModel()
            {
                Name = trainer.Name,
                Email = trainer.Email,
                Phone = trainer.Phone,
                City = trainer.Address.City,
                Street = trainer.Address.Street,
                BuildingNumber = trainer.Address.BuildingNumber,
                Specialities = trainer.Specialties,

            };
        }


        public bool RemoveTrainer(int trainerId)
        {
            try
            {
                var repo = _unitOfWork.GetRepository<Trainer>();
                var trainer = repo.GetById(trainerId);
                if (trainer == null || HasActiveSessions(trainerId))
                {
                    return false;
                }
                repo.Delete(trainer);
                return _unitOfWork.SaveChanges() > 0;
            }
            catch
            {
                return false;
            }

        }

        public bool UpdateTrainerDetails(UpdateTrainerViewModel updatedTrainer, int trainerId)
        {
            var repo = _unitOfWork.GetRepository<Trainer>();
            var TrainerToUpdate = repo.GetById(trainerId);
            if (TrainerToUpdate is null || IsEmailExists(updatedTrainer.Email)|| IsPhoneExists(updatedTrainer.Phone)) return false;

          
            TrainerToUpdate.Email = updatedTrainer.Email;
            TrainerToUpdate.Phone = updatedTrainer.Phone;
            TrainerToUpdate.Name = updatedTrainer.Name;
            TrainerToUpdate.Specialties = updatedTrainer.Specialties;
            TrainerToUpdate.Address.BuildingNumber = updatedTrainer.BuildingNumber;
            TrainerToUpdate.Address.Street = updatedTrainer.Street;
            TrainerToUpdate.Address.City = updatedTrainer.City;
            TrainerToUpdate.UpdatedAt = DateTime.Now;
            repo.Update(TrainerToUpdate);
            return _unitOfWork.SaveChanges() > 0;
        }

        #region Helper functions
        private bool IsEmailExists(string email)
        {
            return _unitOfWork.GetRepository<Trainer>().GetAll(m => m.Email == email).Any();
        }
        private bool IsPhoneExists(string phone)
        {
            return _unitOfWork.GetRepository<Trainer>().GetAll(m => m.Phone == phone).Any();
        }

        private bool HasActiveSessions(int trainerId)
        {
            var activesessions = _unitOfWork.GetRepository<Session>().GetAll(s => s.TrainerId == trainerId && s.StartDate > DateTime.Now).Any();
            return activesessions;
        }
        #endregion
    }
}
