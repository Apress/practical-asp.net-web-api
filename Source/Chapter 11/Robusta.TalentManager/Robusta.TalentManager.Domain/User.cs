using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Robusta.TalentManager.Domain
{
    public class User : IIdentifiable
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public byte[] Password { get; set; }
        public byte[] Salt { get; set; }

        public bool IsAuthentic(string password)
        {
            byte[] storedPassword = this.Password;
            byte[] storedSalt = this.Salt;

            var pbkdf2 = new Rfc2898DeriveBytes(password, storedSalt);
            pbkdf2.IterationCount = 1000;
            byte[] computedPassword = pbkdf2.GetBytes(32);

            return storedPassword.SequenceEqual(computedPassword);
        }
    }
}
