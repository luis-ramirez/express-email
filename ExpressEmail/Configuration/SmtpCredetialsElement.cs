using System;
using System.Configuration;

namespace ExpressEmail.Configuration
{
    public class SmtpCredetialsElement : ConfigurationElement
    {
        [ConfigurationProperty("userName", IsRequired = true, DefaultValue = "")]
        public String UserName
        {
            get { return (String) this["userName"]; }
            set { this["UserName"] = value; }
        }
        
        [ConfigurationProperty("password", IsRequired = true, DefaultValue = "")]
        public String Password
        {
            get { return (String)this["password"]; }
            set { this["password"] = value; }
        }
    }
}