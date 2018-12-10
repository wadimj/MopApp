using System;
using System.Collections.Generic;
using WebApplication1.Models;
using WebApplication1.Models.User;

namespace WebApplication1.DAL
{
    public interface IUserRepository : IDisposable
    {
        IEnumerable<User> GetUsers();
        User GetUserById(int userId);
        
        void InsertUser(User user);
        void DeleteUser(int userId);
        void UpdateUser(User user);
        void Save();
    }
}