using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;
using System.Text;

namespace SalesRepDAL.Attributes
{
    public class EmailValidationAttribute: ValidationAttribute
    {
        public const string message = "Invalid email!";
        public override bool IsValid(object Email)
        {
            string email = Email as string;
            var trimmedEmail = email.Trim();
            if (trimmedEmail.EndsWith("."))
            {
                throw new Exception(message);
            }
            try
            {
                var emailaddress = new MailAddress(email);
                return emailaddress.Address == trimmedEmail;
            }
            catch
            {
                throw new Exception(message);
            }
        }
    }
}
