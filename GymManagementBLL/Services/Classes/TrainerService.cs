using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels.TrainerViewModels;
using GymManagementDAL.Models;
using GymManagementDAL.Models.Owned;
using GymManagementDAL.Repositories.Interfaces;
using GymManagementDAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.Services.Classes
{
    internal class TrainerService : ITrainService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TrainerService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public bool CreateTrainer(CreateTrainerViewModel createdTrainer)///////////////////////////////////////////////////////////////////
        {
            try
            {
                if (createdTrainer == null || EmailExists(createdTrainer.Email) || PhoneExists(createdTrainer.Phone))
                    return false;

                var trainer = new Trainer()
                {
                    Name = createdTrainer.Name,
                    Phone = createdTrainer.Phone,
                    Email = createdTrainer.Email,
                    DateOfBirth = createdTrainer.DateOfBirth,
                    Gender = createdTrainer.Gender,
                    CreatedAt = DateTime.Now,
                    Specialty = createdTrainer.Specializations,
                    Address = new Address()
                    {
                        BuildingNumber = createdTrainer.BuildingNumber,
                        Street = createdTrainer.Street,
                        City = createdTrainer.City
                    }
                };
                _unitOfWork.GetRepository<Trainer>().Add(trainer);
                return _unitOfWork.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Create failed. {ex}");
                return false;
            }
        }

        public bool DeleteTrainer(int trainerId)
        {
            if (!TrainerExists(trainerId)) return false;

            var trainer = _unitOfWork.TrainerRepository.GetById(trainerId);
            var trainerFutureSessins = _unitOfWork.TrainerRepository.GetTrainerWithSessions(trainerId)!
                                                                    .Sessions.Select(s => s.StartDate >=  DateTime.Now).Any();
            if (trainerFutureSessins) return false;

            _unitOfWork.TrainerRepository.Delete(trainer!);
            return _unitOfWork.SaveChanges() > 0;
        }

        public IEnumerable<TrainerViewModel> GetAllTrainers()
        {
            var trainers = _unitOfWork.GetRepository<Trainer>().GetAll();
            if (trainers == null)
                return Enumerable.Empty<TrainerViewModel>();

            var trainerViewModels = trainers.Select(x => new TrainerViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                Email = x.Email,
                Phone = x.Phone,
                Specialization = x.Specialty.ToString()
            });
            return trainerViewModels;
        }

        public TrainerDetailsViewModel? GetTrainerDetails(int trainerId)
        {
            if (!TrainerExists(trainerId))
                return null;

            var trainerWithAddress = _unitOfWork.TrainerRepository.GetTrainerDetailsWithAddress(trainerId);

            if (trainerWithAddress == null)
                return null;

            return new TrainerDetailsViewModel()
            {
                Name = trainerWithAddress.Name,
                Email = trainerWithAddress.Email,
                Phone = trainerWithAddress.Phone,
                DateOfBirth = trainerWithAddress.DateOfBirth,
                Specialization = trainerWithAddress.Specialty.ToString(),
                BuildingNumber = trainerWithAddress.Address.BuildingNumber,
                Street = trainerWithAddress.Address.Street,
                City = trainerWithAddress.Address.City,
            };
        }

        public TrainerToUpdateViewModel? GetTrainerToUpdate(int trainerId)
        {
            var trainer = _unitOfWork.TrainerRepository.GetTrainerDetailsWithAddress(trainerId);

            if (trainer == null)
                return null;

            return new TrainerToUpdateViewModel()
            {
                Name = trainer.Name,
                Email = trainer.Email,
                Phone = trainer.Phone,
                Specialization = trainer.Specialty,
                BuildingNumber = trainer.Address.BuildingNumber,
                Street = trainer.Address.Street,
                City = trainer.Address.City
            };
        }

        public bool UpdateTrainer(int trainerId, TrainerToUpdateViewModel trainerUpdated)
        {
            try
            {
                var trainer = _unitOfWork.TrainerRepository.GetTrainerDetailsWithAddress(trainerId);
                if (trainerUpdated == null
                   || trainer == null
                   || EmailExists(trainerUpdated.Email, trainerId)
                   || PhoneExists(trainerUpdated.Phone, trainerId))
                    return false;

                trainer.Email = trainerUpdated.Email;
                trainer.Phone = trainerUpdated.Phone;
                trainer.Address.BuildingNumber = trainerUpdated.BuildingNumber;
                trainer.Address.Street = trainerUpdated.Street;
                trainer.Address.City = trainerUpdated.City;
                trainer.UpdatedAt = DateTime.Now;
                trainer.Specialty = trainerUpdated.Specialization;
                _unitOfWork.TrainerRepository.Update(trainer);
                return _unitOfWork.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Update failed. {ex}");
                return false;
            }
        }

        #region Helper methods
        private bool EmailExists(string email, int id)
        {
            return _unitOfWork.GetRepository<Trainer>().GetAll(x => x.Email == email && x.Id != id).Any();
        }
        private bool EmailExists(string email)
        {
            return _unitOfWork.GetRepository<Trainer>().GetAll(x => x.Email == email).Any();
        }
        private bool PhoneExists(string phone, int id)
        {
            return _unitOfWork.GetRepository<Trainer>().GetAll(x => x.Phone == phone && x.Id != id).Any();
        }
        private bool PhoneExists(string phone)
        {
            return _unitOfWork.GetRepository<Trainer>().GetAll(x => x.Phone == phone).Any();
        }
        private bool TrainerExists(int trainerId)
        {
            return _unitOfWork.GetRepository<Trainer>().GetById(trainerId) != null;
        }
        #endregion
    }
}
