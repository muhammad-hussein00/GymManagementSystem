using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.ViewModels.PlanViewModel
{
    internal class UpdatePlanViewModel
    {
        [Required(ErrorMessage = "Plan name is required.")]
        [StringLength(50, ErrorMessage = "The maximum length is 50 character.")]
        public string PlanName { get; set; } = null!;

        [Required(ErrorMessage = "Description is required.")]
        [StringLength(100,MinimumLength = 5, ErrorMessage = "Description must be between 5 and 100 character.")]
        public string Description { get; set; } = null!;

        [Required(ErrorMessage = "Duration Days is required.")]
        [Range(1, 365, ErrorMessage = "Duration must be between 1 and 365.")]
        public int DurationDays { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        [Range(0.1, 10000, ErrorMessage = "Price must be between 0.1 and 10000" )]
        public decimal Price { get; set; }
    }
}
