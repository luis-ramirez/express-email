using System;

namespace ExpressEmail.Exceptions
{
    public class ExpressEmailMailMessageNotSuppliedException : Exception
    {
        public ExpressEmailMailMessageNotSuppliedException() : base("Mail Message not configured.")
        {
            
        }
    }
}
