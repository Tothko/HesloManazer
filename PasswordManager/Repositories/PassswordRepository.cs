using PasswordManager.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Repositories
{
    public class PassswordRepository
    {
        readonly PasswordManagerDbContext context;

        public PassswordRepository(PasswordManagerDbContext ctx)
        {
            context = ctx;
        }

        public Password Create(Password Password)
        {
            context.Passwords.Add(Password);
            context.SaveChanges();
            return context.Passwords.Find(Password.Id);
        }

        public Password Delete(int Id)
        {
            context.Passwords.Remove(FindPasswordWithID(Id));
            context.SaveChanges();
            return null;
        }

        public Password FindPasswordWithID(int Id)
        {
            return context.Passwords
                .AsNoTracking()
                .FirstOrDefault(p => p.Id == Id);

        }

        public List<Password> ReadPasswords()
        {
            return context.Passwords
                .AsNoTracking().ToList();
        }

        public Password Update(Password PasswordUpdate)
        {
            context.Passwords.Attach(PasswordUpdate);
            context.SaveChanges();
            return context.Passwords.Find(FindPasswordWithID(PasswordUpdate.Id));
        }
    }
}

