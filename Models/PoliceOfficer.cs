using System.ComponentModel.DataAnnotations;

namespace DesafioAPI.Models
{
    public class PoliceOfficer : Person
    {
        public int Id { get; set; }
        public string Rank { get; set; }
        public string RegisterId { get; set; }
        public bool Status { get; set; }
    }
    public class PoliceOfficerDTO : PersonDTO
    {
        [Required(ErrorMessage = "Police Officer Register Number is mandatory.")]
        public string RegisterId { get; set; }

        [Required(ErrorMessage = "Rank Code is mandatory.")]
        [Range(0, 6, ErrorMessage = "Rank code should be: Police technician = 0; Police officer/patrol officer/police detective = 1; Police corporal = 2; Police sergeant = 3; Police lieutenant = 4; Police captain = 5; Chief of police = 6;")]
        public int RankCode { get; set; }

        public static string GetRank(int rankCode)
        {
            switch (rankCode)
            {
                case 0:
                    return "Police technician";
                case 1:
                    return "Police officer/patrol officer/police detective";
                case 2:
                    return "Police corporal";
                case 3:
                    return "Police sergeant";
                case 4:
                    return "Police lieutenant";
                case 5:
                    return "Police captain";
                case 6:
                    return "Chief of police";
                default:
                    return "Police technician";
            }
        }
    }
}
