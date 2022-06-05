using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using PasswordManager.Model;
using PasswordManager.Repositories;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Services
{
    public class UserService
    {
        private const int SaltLength = 32;
        private const int KeyLength = 32;
        private const int IterationCount = 100000;
        private readonly byte[] Pepper;

        private UserRepository UserRepo;
        public UserService(UserRepository UserRepository)
        {
            UserRepo = UserRepository;
            Pepper = ConvertStringToPepper("Pepper Stegger");
        }
        public User Create(string userName, string password)
        {
            User newUser = new User()
            {
                Username = userName,
                Password = GenerateHash(password)
            };
            return UserRepo.Create(newUser);
        }

        public User Delete(int Id)
        {
            return UserRepo.Delete(Id);
        }

        public User FindUserWithID(int Id)
        {
            return UserRepo.FindUserWithID(Id);
        }

        public List<User> ReadUsers()
        {
            return UserRepo.ReadUsers();

        }

        public User Update(User UserUpdate)
        {
            return UserRepo.Update(UserUpdate);
        }

       /* public byte[] ConvertSpacedHexToByteArray(string hexString)
        {
            string[] hexValuesSplit = hexString.Split(' ');
            byte[] data = new byte[hexValuesSplit.Length];

            for (var index = 0; index < hexValuesSplit.Length; index++)
            {
                data[index] = byte.Parse(hexValuesSplit[index], NumberStyles.HexNumber, CultureInfo.InvariantCulture);
            }

            return data;
        }*/

        public byte[] ConvertStringToPepper(string input)
        {
            return Encoding.ASCII.GetBytes(input);
        }

        public void Login(string username, string password)
        {
            User user = UserRepo.GetUserByUserName(username);
            var result = ValidatePassword(user.Password, password);
        }

        public byte[] GenerateSalt()
        {
            return GenerateSalt(SaltLength);
        }
        //this parameterized version is for the flexibility at knowledgable hands.
        public byte[] GenerateSalt(int saltLength)
        {
            using (RNGCryptoServiceProvider saltCellar = new RNGCryptoServiceProvider())
            {
                byte[] salt = new byte[saltLength];
                saltCellar.GetBytes(salt);
                return salt;
            }
        }
        public string GenerateHash(string password)
        {
            byte[] salt = GenerateSalt();
            byte[] hash = GenerateHash(password, salt);

            return Convert.ToBase64String(salt) + ":" + Convert.ToBase64String(hash);
            //store salt and hash together with ':' as the separator, coded in Base64.
        }

        public byte[] GenerateHash(string password, byte[] salt)
        {
            //create an hmac hash of the password using the pepper value as the key
            using (var hmac = new HMACSHA512(Pepper))
            {
                byte[] initialHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

                //generate a key value using pbkdf2 that will serve as the password hash
                using (var pbkdf2 = new Rfc2898DeriveBytes(initialHash, salt, IterationCount)) 
                {
                    return pbkdf2.GetBytes(KeyLength);
                }
            }
        }
        public bool ValidatePassword(string password, string testPassword)
        {
            string[] hashParts = password.Split(':');
            byte[] salt = Convert.FromBase64String(hashParts[0]);
            byte[] hash = Convert.FromBase64String(hashParts[1]);
            byte[] testHash = GenerateHash(testPassword, salt);

            //IMPORTANT!!! The following is required to defend against timing attacks.
            //We dont want to exit at the first byte mismatch but test every byte no matter what
            //so that timing is the same for valid and invalid passwords and
            //we dont want to leak information to the attacker about upto which byte his guess is correct.
            uint differences = (uint)hash.Length ^ (uint)testHash.Length;
            for (int position = 0; position < Math.Min(hash.Length, testHash.Length); position++)
                differences |= (uint)(hash[position] ^ testHash[position]);
            return differences == 0;
        }
    }
}
