using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Model
{
    public class Password
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string EncryptedPassword { get; set; }

    }


}
