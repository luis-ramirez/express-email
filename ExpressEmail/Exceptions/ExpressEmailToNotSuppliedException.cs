using System;

namespace ExpressEmail.Exceptions
{
    public class ExpressEmailToNotSuppliedException : Exception
    {
        public ExpressEmailToNotSuppliedException() : base("MailMessage not contain any destinatary")
        {
            
        }
    }
}