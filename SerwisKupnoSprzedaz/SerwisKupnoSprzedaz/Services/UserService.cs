using Microsoft.AspNetCore.Connections;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SerwisKupnoSprzedaz.Data;
using SerwisKupnoSprzedaz.Models;

namespace SerwisKupnoSprzedaz.Services
{
    public class UserService : IUserService
    {
        public IEnumerable<User> allUsers { get; set; }
        
        private readonly AppDBContext appDBContext;

        public UserService(IEnumerable<User> allUsers)
        {
            this.allUsers = allUsers;
        }

        public UserService(AppDBContext context)
        {
            appDBContext = context;
        }

        public IEnumerable<User> GetAllUsers()
        {
            return allUsers;
        }

        public void AddUser(User user)
        {

        }

        public void RemoveUser(int ID) {
            
        }

        public void EditUser(int ID, User user)
        {

        }

        public bool isValidRegister(string username, string password, string password2, string adress)
        {
            if (username == null ||
                adress == null   ||
                password == null ||
                password2 == string.Empty||
                password != password2
                ) return false;

            if (password.Length < 5) return false;
           
            return true;

        }

    }
}
