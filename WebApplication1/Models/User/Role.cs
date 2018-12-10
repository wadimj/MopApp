using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models.User
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        //public ICollection<UserRole> UserRoles { get; } = new List<UserRole>();
    }
}