using System.Configuration;

namespace ExpressEmail.Configuration
{
    public class ExpressEmailConfiguration : ConfigurationSection
    {
        [ConfigurationProperty("mainEmailConfiguration", IsRequired = true)]
        public ExpressEmailConfigurationElement MainEmailConfiguration
        {
            get { return (ExpressEmailConfigurationElement) this["mainEmailConfiguration"]; }
            set { this["mainEmailConfiguration"] = value; }
        }

        [ConfigurationProperty("alternativeEmailConfiguration", IsRequired = true)]
        public ExpressEmailConfigurationElement AlternativeEmailConfiguration
        {
            get { return (ExpressEmailConfigurationElement)this["alternativeEmailConfiguration"]; }
            set { this["alternativeEmailConfiguration"] = value; }
        }
    }
}