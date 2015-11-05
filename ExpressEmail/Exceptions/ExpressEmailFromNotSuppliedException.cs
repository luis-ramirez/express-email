using System;

namespace ExpressEmail.Exceptions
{
    public class ExpressEmailFromNotSuppliedException : Exception
    {
        public ExpressEmailFromNotSuppliedException() : base("From email was not configured")
        {
            
        }
    }
}
