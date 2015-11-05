using System;
using System.Configuration;

namespace ExpressEmail.Configuration
{
    public class ExpressEmailConfigurationElement : ConfigurationElement
    {
        [ConfigurationProperty("fromEmail", IsRequired = false)]
        public String FromEmail
        {
            get { return (String)this["fromEmail"]; }
            set { this["fromEmail"] = value; }
        }

        [ConfigurationProperty("fromName", IsRequired = false)]
        public String FromName
        {
            get { return (String) this["fromName"]; }
            set { this["fromName"] = value; }
        }

        [ConfigurationProperty("toEmail", IsRequired = false)]
        public String ToEmail
        {
            get { return (String)this["toEmail"]; }
            set { this["toEmail"] = value; }
        }

        [ConfigurationProperty("toName", IsRequired = false)]
        public String ToName
        {
            get { return (String)this["toName"]; }
            set { this["toName"] = value; }
        }

        [ConfigurationProperty("smtp", IsRequired = true)]
        public SmtpConfigurationElement SmtpConfiguration
        {
            get { return (SmtpConfigurationElement)this["smtp"]; }
            set { this["smtp"] = value; }
        }
    }
}
