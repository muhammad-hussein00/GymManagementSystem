using GymManagementDAL.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.ViewModels.TrainerViewModels
{
    internal class TrainerToUpdateViewModel
    {
        public string Name { get; set; } = null!;


        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")] //Validation
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Email must be valid.")]
        [DataType(DataType.EmailAddress)] //UI hint
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Email must be between 3 and 100.")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Phone is required.")]
        [Phone(ErrorMessage = "Invalid phone format.")]
        [RegularExpression(@"^(011|012|010|015)\d{8}$", ErrorMessage = "Phone must be valid egyptian phone number.")]
        [DataType(DataType.PhoneNumber)]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "Phone must be 11 digit.")]
        public string Phone { get; set; } = null!;

        [Required(ErrorMessage = "Building number is required.")]
        [Range(1, 9999, ErrorMessage = "Building number must be between 1 and 9999.")]
        public int BuildingNumber { get; set; }

        [Required(ErrorMessage = "Street is required.")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Street must be between 2 and 30 character.")]
        public string Street { get; set; } = null!;

        [Required(ErrorMessage = "City is required.")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "City must be between 2 and 30 character.")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "City can contain character and spaces only.")]
        public string City { get; set; } = null!;

        [Required(ErrorMessage = "Specialization is required.")]
        public Specialty Specialization { get; set; }
    }
}
