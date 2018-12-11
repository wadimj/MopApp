using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using WebApplication1.Models.User;

namespace WebApplication1.DAL
{
    public class UserRepository : IUserRepository, IDisposable
    {
        private MopContext _context;
        private bool _disposed = false;

        public UserRepository(MopContext context)
        {
            _context = context;
        }
        
        public async Task<User> Authenticate(string username, string password)
        {
            var user = await Task.Run(() => _context.Users.SingleOrDefault(x => x.Username == username && x.Password == password));
            return user ?? null;
        }
        
        public IEnumerable<User> GetUsers()
        {
            return _context.Users
                .Include(user => user.UserRoles)
                .ThenInclude(userRole => userRole.Role)
                .ToList();
        }

        public User GetUserById(int userId)
        {
            return _context.Users.Find(userId);
        }

        public void InsertUser(User user)
        {
            _context.Users.Add(user);
        }

        public void DeleteUser(int userId)
        {
            _context.Users.Remove(GetUserById(userId));
        }

        public void UpdateUser(User user)
        {
            _context.Entry(user).State = EntityState.Modified;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}