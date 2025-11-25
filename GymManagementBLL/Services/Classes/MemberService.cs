using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels.MemberViewModels;
using GymManagementDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymManagementDAL.Repositories.Classes;
using GymManagementDAL.Repositories.Interfaces;
using System.Reflection.Metadata.Ecma335;
using GymManagementDAL.Models.Owned;
using GymManagementBLL.ViewModels.HealthRecordViewModels;
using GymManagementDAL.UnitOfWork;
using GymManagementDAL.Data.Models;

namespace GymManagementBLL.Services.Classes
{
    public class MemberService : IMemberService
    {
        private readonly IUnitOfWork _unitOfWork;

        public MemberService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IEnumerable<MemberViewModel> GetAllMembers()
        {
            var members = _unitOfWork.GetRepository<Member>().GetAll();
            if (members is null || members.Any() == false) return [];
            var memberViewModels = members.Select(x => new MemberViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                Email = x.Email,
                Phone = x.Phone,
                Photo = x.Photo,
                Gender = x.Gender.ToString()
            });
            return memberViewModels;
        }
        public MemberDetailsViewModel? GetMemberDetails(int id)
        {
            try
            {
                var member = _unitOfWork.GetRepository<Member>().GetById(id);
                if (member is null)
                    return null;


                var memberDetailsViewModel = new MemberDetailsViewModel()
                {
                    Id = member.Id,
                    Name = member.Name,
                    Email = member.Email,
                    Phone = member.Phone,
                    Photo = member.Photo,
                    Gender = member.Gender.ToString(),
                    Address = $"{member.Address.BuildingNumber} - {member.Address.Street} - {member.Address.City}",
                    DateOfBirth = member.DateOfBirth.ToShortDateString(),
                };
                var activeMemberPlan = _unitOfWork.GetRepository<Membership>().GetAll(x => x.MemberId == id && x.IsActive)
                                                            .FirstOrDefault();
                if (activeMemberPlan is not null)
                {
                    memberDetailsViewModel.MembershipStartDate = activeMemberPlan.CreatedAt.ToShortDateString();
                    memberDetailsViewModel.MembershipEndDate = activeMemberPlan.EndDate.ToShortDateString();

                    var plan = _unitOfWork.GetRepository<Plan>().GetById(activeMemberPlan.PlanId);
                    memberDetailsViewModel.PlanName = plan?.Name;
                }
                return memberDetailsViewModel;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public HealthRecordViewModel? GetMemberHealthDetails(int memberId)
        {
            var healthRecord = _unitOfWork.GetRepository<HealthRecord>().GetById(memberId);
            if (healthRecord is null)
                return null;

            return new HealthRecordViewModel()
            {
                BloodType = healthRecord.BloodType,
                Height = healthRecord.Height,
                Weight = healthRecord.Weight,
                Note = healthRecord.Note,
            };
        }
        public MemberToUpdateViewModel? GetMemberToUpdate(int memberId)
        {
            var member = _unitOfWork.GetRepository<Member>().GetById(memberId);
            if (member is null) return null;

            return new MemberToUpdateViewModel()
            {
                Name = member.Name,
                Email = member.Email,
                Photo = member.Photo,
                Phone = member.Phone,
                BuildingNumber = member.Address.BuildingNumber,
                City = member.Address.City,
            };
        }
        public bool CreateMember(CreateMemberViewModel createMemberViewModel)
        {
            //If member is exist
            var memberIsExist = _unitOfWork.GetRepository<Member>().GetAll(x => x.Email == createMemberViewModel.Email ||
                                                         x.Phone == createMemberViewModel.Phone).Any();
            //If email is exist or phone is exist return false
            if (memberIsExist) return false;
            //Adding member
            try
            {
                var member = new Member()
                {
                    Email = createMemberViewModel.Email,
                    Phone = createMemberViewModel.Phone,
                    Photo = createMemberViewModel.Photo,
                    Gender = createMemberViewModel.Gender,
                    DateOfBirth = createMemberViewModel.DateOfBirth,
                    Name = createMemberViewModel.Name,
                    Address = new Address()
                    {
                        BuildingNumber = createMemberViewModel.BuildingNumber,
                        Street = createMemberViewModel.Street,
                        City = createMemberViewModel.City
                    },
                    HealthRecord = new HealthRecord()
                    {
                        BloodType = createMemberViewModel.HealthRecordViewModel.BloodType,
                        Height = createMemberViewModel.HealthRecordViewModel.Height,
                        Weight = createMemberViewModel.HealthRecordViewModel.Weight,
                        Note = createMemberViewModel.HealthRecordViewModel.Note
                    }
                };
                _unitOfWork.GetRepository<Member>().Add(member);
                return _unitOfWork.SaveChanges() > 0;
            }
            catch
            {
                return false;
            }
        }
        public bool UpdateMember(MemberToUpdateViewModel memberToUpdateView, int memberId)
        {

            if (!MemberExists(memberId)
                ||EmailExists(memberToUpdateView.Email)
                ||PhoneExists(memberToUpdateView.Phone)) return false;

            var member = _unitOfWork.GetRepository<Trainer>().GetById(memberId);

            member!.Address.BuildingNumber = memberToUpdateView.BuildingNumber;
            member!.Address.Street = memberToUpdateView.Street;
            member!.Address.City = memberToUpdateView.City;
            member!.Phone = memberToUpdateView.Phone;
            member!.Email = memberToUpdateView.Email;
            member!.UpdatedAt = DateTime.Now;
            _unitOfWork.GetRepository<Trainer>().Update(member);
            return _unitOfWork.SaveChanges() > 0;
        }
        public bool TryDeleteMember(int memberId)
        {
            var memberRepo = _unitOfWork.GetRepository<Trainer>();
            var member = memberRepo.GetById(memberId);
            if(member == null) return false;
            var activeMemberBookedSessions = _unitOfWork.GetRepository<MemberSession>().GetAll(x => x.Id == memberId && x.Session.StartDate > DateTime.Now)
                                                               .Any();
            if(activeMemberBookedSessions == true) return false;
            
            var memberPlans = _unitOfWork.GetRepository<Membership>().GetAll(x => x.MemberId == memberId);
            try
            {
                if (memberPlans is not null)
                {
                    foreach (var memberPlan in memberPlans)
                    {
                        _unitOfWork.GetRepository<Membership>().Delete(memberPlan);
                    }
                }
                memberRepo.Delete(member);
                return _unitOfWork.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #region Helper methods
        private bool EmailExists (string email)
        {
            return _unitOfWork.GetRepository<Trainer>().GetAll(x => x.Email == email).Any();
        }
        private bool PhoneExists(string phone)
        {
            return _unitOfWork.GetRepository<Trainer>().GetAll(x => x.Phone == phone).Any();
        }
        private bool MemberExists(int memberId)
        {
            return _unitOfWork.GetRepository<Trainer>().GetAll(x => x.Id == memberId).Any();
        }
        #endregion
    }
}
