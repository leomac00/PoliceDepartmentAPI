using System.ComponentModel.DataAnnotations;

namespace DesafioAPI.Models
{
    public class PoliceDepartment
    {
        public int Id { get; set; }
        public Adress Adress { get; set; }
        public string PhoneNumber { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; }
    }
    public class PoliceDepartmentDTO
    {
        [Required(ErrorMessage = "Address´s ID is mandatory.")]
        public int AdressId { get; set; }


        [Required(ErrorMessage = "PD´s phone number is mandatory.")]
        [RegularExpression(@"(\(?\d{2}\)?\s)?(\d{4,5}\-\d{4})", ErrorMessage = "Invalid Phone Number.")]
        public string PhoneNumber { get; set; }


        [Required(ErrorMessage = "User´s Name is mandatory.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "User´s name should be beetwen {2} and {1} characters.")]
        public string Name { get; set; }
    }
}