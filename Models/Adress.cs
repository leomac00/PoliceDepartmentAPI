using System.ComponentModel.DataAnnotations;

namespace DesafioAPI.Models
{
    public class Adress
    {
        public int Id { get; set; }
        public string ZIPCode { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public bool Status { get; set; }
    }
    public class AdressDTO
    {
        [Required(ErrorMessage = "Street´s name is mandatory.")]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "Street´s name should be beetwen {2} and {1} characters.")]
        public string Street { get; set; }


        [Required(ErrorMessage = "Number is mandatory.")]
        [StringLength(100, ErrorMessage = "Address´s number should be less than {1} characters.")]
        public string Number { get; set; }


        [Required(ErrorMessage = "City is mandatory.")]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "City´s name should be beetwen {2} and {1} characters.")]
        public string City { get; set; }


        [Required(ErrorMessage = "State is mandatory.")]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "State´s name should be beetwen {2} and {1} characters.")]
        public string State { get; set; }


        [Required(ErrorMessage = "ZIP Code is mandatory.")]
        [RegularExpression(@"^\d{5}-\d{3}$", ErrorMessage = "Invalid ZIP Code. Please use the following format '00000-000'.")]
        public string ZIPCode { get; set; }
    }
}