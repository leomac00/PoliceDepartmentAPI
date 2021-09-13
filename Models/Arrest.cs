using System;
using System.ComponentModel.DataAnnotations;

namespace DesafioAPI.Models
{
    public class Arrest
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public Crime Crime { get; set; }
        public Deputy Deputy { get; set; }
        public PoliceOfficer Officer { get; set; }
        public Perpetrator Perpetrator { get; set; }
        public bool Status { get; set; }
    }
    public class ArrestDTO
    {
        [Required(ErrorMessage = "Police Officer´s ID is mandatory.")]
        public int OfficerId { get; set; }


        [Required(ErrorMessage = "Perpetrator´s ID is mandatory.")]
        public int PerpetratorId { get; set; }


        [Required(ErrorMessage = "Deputy´s ID is mandatory.")]
        public int DeputyId { get; set; }


        [Required(ErrorMessage = "Crime´s ID is mandatory.")]
        public int CrimeId { get; set; }


        [Required(ErrorMessage = "Autopsy´s Date is mandatory.")]
        [RegularExpression(@"(0[1-9]|[12][0-9]|3[01])/(0[1-9]|1[012])/(19|20)\d{2}", ErrorMessage = "Invalid Date Format.")]
        public string Date { get; set; }
    }
}