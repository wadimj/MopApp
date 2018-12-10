using System.Collections.Generic;

namespace WebApplication1.Models.User
{
    public class User
    {
        public int Id { get; set; }
        public string Forename { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        
        public ICollection<UserRole> UserRoles { get; } = new List<UserRole>();
    }
}