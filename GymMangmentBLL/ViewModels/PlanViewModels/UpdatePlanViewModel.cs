using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymMangmentBLL.ViewModels.PlanViewModels
{
    internal class UpdatePlanViewModel
    {
         
        public string PlanName { get; set; } = null!;

        [Required(ErrorMessage = "Description is Required")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Plan Description Name must be between 5 and 50 characters.")]
        public string Description { get; set; } = null!;

        [Range(1,365, ErrorMessage = "Duration Days between 1 and 365")]
        public int DurationDays {get; set;}

        [Range(0.1, 10000, ErrorMessage = "Price Between o.1 and 10000")]
        public decimal Price { get; set;}
    }
}
