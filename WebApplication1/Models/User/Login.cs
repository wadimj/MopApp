using System;

namespace WebApplication1.Models.User
{
    public class Login
    {
        public int Id { get; set; }
        public string UserAgent { get; set; }
        public DateTime Date { get; set; }
        
        public User User { get; }
    }
}