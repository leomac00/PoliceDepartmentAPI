using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace DesafioAPI.Models
{
    public class User : Person
    {
        public int Id { get; set; }
        public string RegisterId { get; set; } //OAB number
        public string UserRole { get; set; } //0 = Judge; 1 = Lawyer - Left as int for further user´s types (scaling)
        public string Password { get; set; }
        public bool Status { get; set; }

        public static string GetRole(int roleCode)
        {
            var roles = new Hashtable();
            roles.Add(0, "Judge");
            roles.Add(1, "Lawyer");

            return roles.ContainsKey(roleCode) ? (string)roles[roleCode] : "Lawyer";
        }
    }

    public class UserDTO : PersonDTO
    {
        [Required(ErrorMessage = "User´s Registration Number is mandatory.")]
        public string RegisterId { get; set; }


        [Required(ErrorMessage = "User´s Type is mandatory.")]
        [Range(0, 1, ErrorMessage = "User Type should be: 0 = Judge; 1 = Lawyer;")]
        public int UserRole { get; set; }

        [Required(ErrorMessage = "User´s Password is mandatory.")]
        public string Password { get; set; }
    }
    public class UserCredentials
    {
        [Required(ErrorMessage = "User´s Registration Number is mandatory.")]
        public string RegisterId { get; set; }

        [Required(ErrorMessage = "User´s Password is mandatory.")]
        public string Password { get; set; }
    }

    public class UserInfo : Person
    {
        public int Id { get; set; }
        public string UserRole { get; set; } //0 = Judge; 1 = Lawyer - Left as int for further user´s types (scaling)
        public string RegisterId { get; set; } //OAB number
    }
}