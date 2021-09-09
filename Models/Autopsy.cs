using System;
using System.ComponentModel.DataAnnotations;

namespace DesafioAPI.Models
{
    public class Autopsy
    {
        public int Id { get; set; }
        public Victim Victim { get; set; }
        public Coroner Coroner { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
    }
    public class AutopsyDTO
    {
        [Required(ErrorMessage = "Victim´s ID is mandatory.")]
        public int VictimId { get; set; }


        [Required(ErrorMessage = "Coroner´s ID is mandatory.")]
        public int CoronerId { get; set; }


        [Required(ErrorMessage = "Autopsy´s Date is mandatory.")]
        [RegularExpression(@"(0[1-9]|[12][0-9]|3[01])/(0[1-9]|1[012])/(19|20)\d{2}", ErrorMessage = "Invalid Date Format.")]
        public string Date { get; set; }


        [Required(ErrorMessage = "Autopsy´s Description is mandatory.")]
        public string Description { get; set; }
    }
}