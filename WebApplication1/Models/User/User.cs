using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models.User
{
    public class User
    {
        public int Id { get; set; }
        public string Forename { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        
        //[InverseProperty("User")]
        public ICollection<UserRole> UserRoles { get; } = new List<UserRole>();
    }
}