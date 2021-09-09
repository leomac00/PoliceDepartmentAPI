using System.ComponentModel.DataAnnotations;

namespace DesafioAPI.Models
{
    public class Coroner : Person
    {
        public int Id { get; set; }
        public string RegisterId { get; set; } //CRM register number
        public bool Status { get; set; }
    }
    public class CoronerDTO : PersonDTO
    {
        [Required(ErrorMessage = "Coroner Register Number [CRM] is mandatory.")]
        public string RegisterId { get; set; } //CRM register number
    }
}