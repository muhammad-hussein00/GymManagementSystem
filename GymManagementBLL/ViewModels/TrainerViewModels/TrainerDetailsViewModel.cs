using GymManagementDAL.Models.Owned;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.ViewModels.TrainerViewModels
{
    internal class TrainerDetailsViewModel
    {
        public string Name { get; set; } = null!;
        public string Specialization { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public DateOnly DateOfBirth { get; set; }
        public int BuildingNumber { get; set; }
        public string Street { get; set; } = null!;
        public string City { get; set; } = null!;

        #region Properties to display
        public string Address => $"{BuildingNumber} - {Street} - {City}";
        public string Speciality => $"{Specialization} Trainer"; 
        #endregion
    }
}
