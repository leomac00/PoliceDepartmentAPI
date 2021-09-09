namespace DesafioAPI.Models
{
    public class Perpetrator : Person
    {
        public int Id { get; set; }
        public bool Status { get; set; }
    }
    public class PerpetratorDTO : PersonDTO { }
}