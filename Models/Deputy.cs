using System.ComponentModel.DataAnnotations;

namespace DesafioAPI.Models
{
    public class Deputy : Person
    {
        public int Id { get; set; }
        public PoliceDepartment PoliceDepartment { get; set; }
        public string Shift { get; set; } //0 = morning; 1 = evening; 2 = night
        public string RegisterId { get; set; }
        public bool Status { get; set; }
    }
    public class DeputyDTO : PersonDTO
    {
        [Required(ErrorMessage = "PD´s ID is mandatory.")]
        public int PoliceDepartmentId { get; set; }



        [Required(ErrorMessage = "Shift Code is mandatory.")]
        [Range(0, 2, ErrorMessage = "Shift code should be: 0 = Morning; 1 = Evening; 2 = Night;")]
        public int ShiftCode { get; set; } //0 = morning; 1 = evening; 2 = night



        [Required(ErrorMessage = "Deputy Register Number is mandatory.")]
        public string RegisterId { get; set; }

        public static string GetShift(int shiftCode)
        {
            switch (shiftCode)
            {
                case 0:
                    return "Morning";
                case 1:
                    return "Evening";
                case 2:
                    return "Night";
                default:
                    return "Morning";
            }
        }
    }
}