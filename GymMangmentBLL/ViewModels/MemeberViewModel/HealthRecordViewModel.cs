using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymMangmentBLL.ViewModels.MemeberViewModel
{
    internal class HealthRecordViewModel
    {
        [Required (ErrorMessage = "Height is Requied")]
        [Range(30, 300, ErrorMessage = "Height must be between 30 cm and 300 cm")]
        public decimal Height { get; set; }

        [Required(ErrorMessage = "Weight is Requied")]
        [Range(1, 500, ErrorMessage = "Weight must be between 1 kg and 500 kg")]
        public decimal Weight { get; set; }

        [Required(ErrorMessage = "Blood Type is Requied")]
        [StringLength(3, MinimumLength = 1, ErrorMessage = "Blood Type must be between 2 and 3 characters.")]
        public string BloodType { get; set; } = null!;

        public string? Note { get; set; } = null!;
    }
}
