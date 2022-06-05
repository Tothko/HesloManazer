using PasswordManager.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Repositories
{
    public class UserRepository
    {
        readonly PasswordManagerDbContext context;

        public UserRepository(PasswordManagerDbContext ctx)
        {
            context = ctx;
        }

        public User Create(User User)
        {
            context.Users.Add(User);
            context.SaveChanges();
            return context.Users.Find(User.Id);
        }

        public User Delete(int Id)
        {
            context.Users.Remove(FindUserWithID(Id));
            context.SaveChanges();
            return null;
        }

        public User FindUserWithID(int Id)
        {
            return context.Users
                .AsNoTracking()
                .FirstOrDefault(p => p.Id == Id);

        }

        public List<User> ReadUsers()
        {
            return context.Users
                .AsNoTracking().ToList();
        }

        public User Update(User UserUpdate)
        {
            context.Users.Attach(UserUpdate);
            context.SaveChanges();
            return context.Users.Find(FindUserWithID(UserUpdate.Id));
        }

        public User GetUserByUserName(string username)
        {
            return context.Users
                .AsNoTracking()
                .FirstOrDefault(p => p.Username == username);
        }
    }
}
