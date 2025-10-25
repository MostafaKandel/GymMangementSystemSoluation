using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymMangmentBLL.ViewModels.MemeberViewModel
{
    public class MemberToUpdateViewModel
    {
        public string Name { get; set; } = null!;


        [Required(ErrorMessage = "Email is Requied")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Email must be between 5 and 100 characters.")]
        [EmailAddress(ErrorMessage = "Invalid Email Format")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Phone is Requied")]
        [Phone(ErrorMessage = "Invalid Phone Number")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^(010|011|012|015)\d{8}$", ErrorMessage = "Phone number must be Egyption number")]
        public string Phone { get; set; } = null!;


        [Required(ErrorMessage = "Building Number is Requied")]
        [Range(1, 1000, ErrorMessage = "Building Number must be between 1 and 1000")]
        public int BuildingNumber { get; set; }

        [Required(ErrorMessage = "Street is Requied")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Street must be between 2 and 30 characters.")]
        public string Street { get; set; } = null!;

        [Required(ErrorMessage = "City is Requied")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "City must be between 2 and 30 characters.")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "City can only contain letters and spaces.")]
        public string City { get; set; } = null!;

        public string? Photo { get; set; }
    }
}
