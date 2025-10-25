using GymMangmentBLL.ViewModels.TrainerViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymMangmentBLL.Services.Interfaces
{
   public interface ITrainerService
    {
        IEnumerable<TrainerViewModel> GetAllTrainers();
        bool CreateTrainer(CreateTrainerViewModel trainerViewModel);
        bool UpdateTrainerDetails(TrainerToUpdateViewModel updatedTrainer, int trainerId);

        TrainerToUpdateViewModel ? GetTrainerToUpdate(int trainerId);

        bool RemoveTrainer(int trainerId);

        TrainerViewModel? GetTrainerDetails(int trainerId);
    }
}
