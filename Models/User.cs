namespace ActivityTracker.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UniqueID { get; set; }
        public string Email { get; set; }
        public string Photo { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Nickname { get; set; }
    }
}