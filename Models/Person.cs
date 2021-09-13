using System.ComponentModel.DataAnnotations;

namespace DesafioAPI.Models
{
    public abstract class Person
    {
        public string Name { get; set; }
        public string CPF { get; set; }
    }
    public abstract class PersonDTO
    {
        [Required(ErrorMessage = "User´s Name is mandatory.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "User´s name should be beetwen {2} and {1} characters.")]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage = "Characters are not allowed.")]
        public string Name { get; set; }


        [Required(ErrorMessage = "User´s CPF is mandatory.")]
        [RegularExpression(@"([0-9]{2}[\.]?[0-9]{3}[\.]?[0-9]{3}[\/]?[0-9]{4}[-]?[0-9]{2})|([0-9]{3}[\.]?[0-9]{3}[\.]?[0-9]{3}[-]?[0-9]{2})", ErrorMessage = "Invalid CPF. Please use the following format '000.000.000-00'.")]
        public string CPF { get; set; }
    }

}