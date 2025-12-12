using GymManagementBLL.ViewModels.TrainerViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.Services.Interfaces
{
    public interface ITrainerService
    {
        IEnumerable<TrainerViewModel>? GetAllTrainers();
        bool CreateTrainer(CreateTrainerViewModel createTrainerViewModel);
        TrainerDetailsViewModel? GetTrainerDetails(int trainerId); 
        
        TrainerToUpdateViewModel? GetTrainerToUpdate(int trainerId);
        bool UpdateTrainer(int trainerId, TrainerToUpdateViewModel trainerToUpdate);
        bool DeleteTrainer(int trainerId);
    }
}
