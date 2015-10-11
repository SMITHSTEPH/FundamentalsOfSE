using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class Verifcation
    {

        public int PasswordCheck(String password)
        {

            string possiblePassword = Convert.ToString(password);

            if (possiblePassword.Length < 6)
            {
                return 1;
            }
            else if (!possiblePassword.Any(c => IsDigit(c)) ||
                    !possiblePassword.Any(c => IsSymbol(c)) ||
                    !possiblePassword.Any(c => IsLetterLower(c)) ||
                    !possiblePassword.Any(c => IsLetterHigher(c)))
            {
                return 2;
            }
            else
            {
                return 0;
            }
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

        public Boolean IsNullMember(Member Table)
        {
            if (IsNull(Table.UserName) ||
                     IsNull(Table.Email) ||
                     IsNull(Table.Password) ||
                     IsNull(Table.Address) ||
                     IsNull(Table.Birthdate) ||
                     IsNull(Table.Gender) ||
                     IsNull(Table.FirstName) ||
                     IsNull(Table.LastName) ||
                     IsNull(Table.PhoneNumber)){

                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsNull(string input)
        {
            if(input == null)
            {
                return true;
            }
            return false;
            
        }

    }
}