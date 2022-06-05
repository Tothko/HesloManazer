using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager
{
    public class PasswordManagerDbInitializer
    {
        public static void SeedDB(PasswordManagerDbContext ctx)
        {
            ctx.Database.Exists();

        }
    }
}
