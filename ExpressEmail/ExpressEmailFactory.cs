using ExpressEmail.Configuration;

namespace ExpressEmail
{
    public class ExpressEmailFactory
    {
        public ExpressEmail Create()
        {
            return new ExpressEmail();
        }

        public ExpressEmail Create(ExpressEmailConfiguration configuration)
        {
            return new ExpressEmail(configuration);
        }
    }
}
