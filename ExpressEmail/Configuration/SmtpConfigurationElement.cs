using System;
using System.Configuration;

namespace ExpressEmail.Configuration
{
    public class SmtpConfigurationElement : ConfigurationElement
    {
        [ConfigurationProperty("host", IsRequired = true)]
        [StringValidator(InvalidCharacters = "~!@#$%^&*()[]{}/;'\"|\\")]
        public String Host
        {
            get { return (String) this["host"]; }
            set { this["host"] = value; }
        }

        [ConfigurationProperty("enableSsl", IsRequired = true)]
        public Boolean EnableSsl
        {
            get { return (bool)this["enableSsl"]; }
            set { this["enableSsl"] = value; }
        }

        [ConfigurationProperty("port", IsRequired = true)]
        public Int32 Port
        {
            get { return (int) this["port"]; }
            set { this["port"] = value; }
        }

        [ConfigurationProperty("useDefaultCredentials", IsRequired = true)]
        public Boolean UseDefaultCredentials
        {
            get { return (bool) this["useDefaultCredentials"]; }
            set { this["useDefaultCredentials"] = value; }
        }

        [ConfigurationProperty("credentials", IsRequired = true)]
        public SmtpCredetialsElement Credentials
        {
            get { return (SmtpCredetialsElement) this["credentials"]; }
            set { this["credentials"] = value; }
        }

        public override string ToString()
        {
            return String.Format("host: {0} port:{1} enableSsl:{2} useDefaultCredentials:{3} userName:{4} password={5}", Host, Port, EnableSsl, UseDefaultCredentials, Credentials.UserName, Credentials.Password);
        }
    }
}