using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Encodings;
using System.Threading.Tasks;

namespace CursoNetCore.Infrestuctura
{
    public class Criptography
    {
        public static bool ValidacionPassword(string password, byte[] HashPassword, byte[] saltPassword)
        {
            using (var hmac = new HMACSHA512(saltPassword))
            {
                var Computed = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < Computed.Length; i++)
                {
                    if (Computed[i] != HashPassword[i]) return false;
                    
                }
            }
            return true;
        }

        public static void CrearPasswordEncriptado(string Password, out byte[] HashPassword, out byte[] saltPassword)
        {
            using (var hmac = new HMACSHA512())
            {
                saltPassword = hmac.Key;
                HashPassword = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(Password));

            }

        }
    }
}
