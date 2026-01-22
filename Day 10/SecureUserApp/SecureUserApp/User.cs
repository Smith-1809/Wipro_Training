using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecureUserApp
{
    public class User
    {
        public string Username { get; set; }
        public string HashedPassword { get; set; }
        public string EncryptedEmail { get; set; } 

        private static List<User> _database = new List<User>();
        private readonly SecurityService _security = new SecurityService();

        public bool Register(string username, string password, string email)
        {
            try
            {
                var newUser = new User
                {
                    Username = username,
                    HashedPassword = _security.HashPassword(password),
                    EncryptedEmail = _security.EncryptData(email)
                };
                _database.Add(newUser);
                LoggerService.Log($"User {username} registered successfully.");
                return true;
            }
            catch (Exception ex)
            {
                LoggerService.Log($"Error during registration: {ex.Message}");
                return false;
            }
        }

        public bool Login(string username, string password)
        {
            try
            {
                var user = _database.Find(u => u.Username == username);
                if (user != null)
                {
                    string loginHash = _security.HashPassword(password);
                    if (user.HashedPassword == loginHash)
                    {
                        LoggerService.Log($"User {username} logged in.");
                        return true;
                    }
                }
                LoggerService.Log($"Login failed for user: {username}");
                return false;
            }
            catch (Exception ex)
            {
                LoggerService.Log($"Login Error: {ex.Message}");
                return false;
            }
        }
    }
}
