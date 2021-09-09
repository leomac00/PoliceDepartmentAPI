namespace DesafioAPI.Models
{
    public class Victim : Person
    {
        public int Id { get; set; }
        public bool Status { get; set; }
    }
    public class VictimDTO : PersonDTO { }
}