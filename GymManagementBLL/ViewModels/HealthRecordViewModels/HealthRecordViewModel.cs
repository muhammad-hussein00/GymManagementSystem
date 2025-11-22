using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.ViewModels.HealthRecordViewModels
{
    public class HealthRecordViewModel
    {
        [Required(ErrorMessage ="Height is required.")]
        [Range(40, 300, ErrorMessage = "Height must be between 40 and 300.")]
        public decimal Height { get; set; }

        [Required(ErrorMessage = "Weight is required.")]
        [Range(20, 500, ErrorMessage = "Weight must be between 20 and 500.")]
        public decimal Weight { get; set; }

        [Required(ErrorMessage = "Blood type is required.")]
        [StringLength(2, MinimumLength = 1,ErrorMessage = "Blood type must be between 1 and 2 character.")]
        public string BloodType { get; set; } = null!;

        public string? Note { get; set; } = null!;
    }
}
