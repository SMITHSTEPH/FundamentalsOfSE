using System;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace WebApplication2.Models
{
    public class Verifcation
    {
        public string PasswordCheck(string possiblePassword)
        {

            if (possiblePassword.Length < 6) return "Length";

            else if (!possiblePassword.Any(c => IsDigit(c)) ||
                    !possiblePassword.Any(c => IsSymbol(c)) ||
                    !possiblePassword.Any(c => IsLetterLower(c)) ||
                    !possiblePassword.Any(c => IsLetterHigher(c)))
                return "Char";

            else return "Valid";
        }

        static bool IsDigit(char c)
        {
            return c >= '0' && c <= '9';
        }
        static bool IsLetterLower(char c)
        {
            return (c >= 'a' && c <= 'z');
        }
        static bool IsLetterHigher(char c)
        {
            return (c >= 'A' && c <= 'Z');
        }
        static bool IsSymbol(char c)
        {
            return c > 32 && c < 127 && !IsDigit(c) && !IsLetterHigher(c) && !IsLetterLower(c);
        }

        public bool IsNull(string input)
        {
            if(input == null) return true;
            else return false;
            
        }

        public string Encrypt(string clearText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }

        public string Decrypt(string cipherText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }

    }
}