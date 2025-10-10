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

namespace GymManagementBLL.Services.Classes
{
    internal class MemberService : IMemberService
    {
        private readonly IGenericRepository<Member> _memberRepository;
        private readonly IGenericRepository<MemberPlan> _memberPlanRepository;
        private readonly IPlanRepository _planRepository;
        private readonly IGenericRepository<HealthRecord> _healthRecordRepository;

        public MemberService(IGenericRepository<Member> memberRepository,
                             IGenericRepository<MemberPlan> memberPlanRepository,
                             IPlanRepository planRepository,
                             IGenericRepository<HealthRecord> healthRecordRepository)
        {
            _memberRepository = memberRepository;
            this._memberPlanRepository = memberPlanRepository;
            this._planRepository = planRepository;
            this._healthRecordRepository = healthRecordRepository;
        }
        public IEnumerable<MemberViewModel> GetAllMembers()
        {
            var members = _memberRepository.GetAll();
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
                var member = _memberRepository.GetById(id);
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
                var activeMemberPlan = _memberPlanRepository.GetAll(x => x.MemberId == id && x.IsActive)
                                                            .FirstOrDefault();
                if (activeMemberPlan is not null)
                {
                    memberDetailsViewModel.MembershipStartDate = activeMemberPlan.CreatedAt.ToShortDateString();
                    memberDetailsViewModel.MembershipEndDate = activeMemberPlan.EndDate.ToShortDateString();

                    var plan = _planRepository.GetById(activeMemberPlan.PlanId);
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
            var healthRecord = _healthRecordRepository.GetById(memberId);
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
            var member = _memberRepository.GetById(memberId);
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

        public bool TryCreateMember(CreateMemberViewModel createMemberViewModel)
        {
            //If member is exist
            var memberIsExist = _memberRepository.GetAll(x => x.Email == createMemberViewModel.Email ||
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
                return _memberRepository.Add(member) > 0;
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

            var member = _memberRepository.GetById(memberId);

            member!.Address.BuildingNumber = memberToUpdateView.BuildingNumber;
            member!.Address.Street = memberToUpdateView.Street;
            member!.Address.City = memberToUpdateView.City;
            member!.Phone = memberToUpdateView.Phone;
            member!.Email = memberToUpdateView.Email;
            member!.UpdatedAt = DateTime.Now;
            return _memberRepository.Update(member) > 0;
        }


        #region Helper methods

        private bool EmailExists (string email)
        {
            return _memberRepository.GetAll(x => x.Email == email).Any();
        }
        private bool PhoneExists(string phone)
        {
            return _memberRepository.GetAll(x => x.Phone == phone).Any();
        }
        private bool MemberExists(int memberId)
        {
            return _memberRepository.GetAll(x => x.Id == memberId).Any();
        }

        #endregion
    }
}
