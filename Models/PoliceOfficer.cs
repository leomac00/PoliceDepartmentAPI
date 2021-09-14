using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace DesafioAPI.Models
{
    public class PoliceOfficer : Person
    {
        public int Id { get; set; }
        public string Rank { get; set; }
        public string RegisterId { get; set; }
        public bool Status { get; set; }

        public static string GetRank(int rankCode)
        {
            var ranks = new Hashtable();

            ranks.Add(0, "Police technician");
            ranks.Add(1, "Police officer/patrol officer/police detective");
            ranks.Add(2, "Police corporal");
            ranks.Add(3, "Police sergeant");
            ranks.Add(4, "Police lieutenant");
            ranks.Add(5, "Chief of police");

            return ranks.ContainsKey(rankCode) ? (string)ranks[rankCode] : "Police technician";
        }
    }
    public class PoliceOfficerDTO : PersonDTO
    {
        [Required(ErrorMessage = "Police Officer Register Number is mandatory.")]
        public string RegisterId { get; set; }

        [Required(ErrorMessage = "Rank Code is mandatory.")]
        [Range(0, 5, ErrorMessage = "Rank code should be: Police technician = 0; Police officer/patrol officer/police detective = 1; Police corporal = 2; Police sergeant = 3; Police lieutenant = 4; Police captain = 5; Chief of police = 6;")]
        public int RankCode { get; set; }
    }
}
