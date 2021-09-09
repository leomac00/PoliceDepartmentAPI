using System;
using System.ComponentModel.DataAnnotations;

namespace DesafioAPI.Models
{
    public class Crime
    {
        public int Id { get; set; }
        public Perpetrator Perpetrator { get; set; }
        public Victim Victim { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public Adress Adress { get; set; }
        public bool Status { get; set; }
    }
    public class CrimeDTO
    {

        [Required(ErrorMessage = "Perpetrator´s ID is mandatory.")]
        public int PerpetratorId { get; set; }



        [Required(ErrorMessage = "Victim´s ID is mandatory.")]
        public int VictimId { get; set; }


        [Required(ErrorMessage = "Crime´s Date is mandatory.")]
        [RegularExpression(@"(0[1-9]|[12][0-9]|3[01])/(0[1-9]|1[012])/(19|20)\d{2}", ErrorMessage = "Invalid Date Format.")]
        public string Date { get; set; }


        [Required(ErrorMessage = "Crime´s Description is mandatory.")]
        public string Description { get; set; }


        [Required(ErrorMessage = "Adsress´s ID where crime happened is mandatory.")]
        public int AdressId { get; set; }
    }
}