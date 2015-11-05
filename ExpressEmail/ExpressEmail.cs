using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using ExpressEmail.Configuration;
using ExpressEmail.Exceptions;
using ExpressEmail.Extensions;

namespace ExpressEmail
{
    public class ExpressEmail
    {
        private SmtpClient _smtpClient;
        private MailMessage _mailMessage;
        private readonly ExpressEmailConfiguration _emailConfiguration;
        private bool _useAlternativeSmtpConfiguration;

        public ExpressEmail()
        {
            var defaultConfiguration = (ExpressEmailConfiguration)ConfigurationManager.GetSection("emailConfiguration");

            if (defaultConfiguration == null || defaultConfiguration.MainEmailConfiguration == null)
                throw new InvalidOperationException("can't create ExpressEmail without email configuration, please check web.config or app.config file");

            _emailConfiguration = defaultConfiguration;

            ConfigSmtpClientWithMainEmailConfiguration();

            ConfigMailMessage();
        }

        public ExpressEmail(ExpressEmailConfiguration configuration)
        {
            _emailConfiguration = configuration;
            ConfigSmtpClientWithMainEmailConfiguration();
            ConfigMailMessage();
        }

        public ExpressEmail From(string emailAddress, string name)
        {
            if (emailAddress.IsEmail())
                _mailMessage.From = new MailAddress(emailAddress, name);

            return this;
        }

        public ExpressEmail From(MailAddress mailAddress)
        {
            if (mailAddress != null && mailAddress.Address.IsEmail())
                _mailMessage.From = mailAddress;

            return this;
        }

        public ExpressEmail To(string address, string name)
        {
            if (!address.IsEmail())
                return this;

            var mailAddress = new MailAddress(address, name);
            return To(mailAddress);
        }

        public ExpressEmail To(string addresses, EmailAddressSeparator emailChar, string name)
        {
            foreach (var add in addresses.Split(GetSplitAddress(emailChar).ToCharArray()).Where(add => add.IsEmail()))
            {
                _mailMessage.To.Add(new MailAddress(add, name));
            }

            return this;
        }
        public ExpressEmail To(MailAddress mailAddress)
        {
            if (mailAddress != null && mailAddress.Address.IsEmail())
                _mailMessage.To.Add(mailAddress);

            return this;
        }

        public ExpressEmail To(IEnumerable<MailAddress> mailAddresses)
        {
            foreach (var mailAddress in mailAddresses.Where(mailAddress => mailAddress.Address.IsEmail()))
            {
                _mailMessage.To.Add(mailAddress);
            }

            return this;
        }

        public ExpressEmail IsHtmlBody()
        {
            _mailMessage.IsBodyHtml = true;

            return this;
        }

        public ExpressEmail WithSubject(string subject)
        {
            if (string.IsNullOrWhiteSpace(_mailMessage.Subject))
                _mailMessage.Subject = subject;

            return this;
        }

        public ExpressEmail WithBody(string body)
        {
            _mailMessage.Body += body;

            return this;
        }

        public ExpressEmail WithAttachment(string fileName)
        {
            if (!File.Exists(fileName))
                return this;

            var attachment = new Attachment(fileName);

            return WithAttachment(attachment);
        }

        public ExpressEmail WithAttachment(Stream contenStream, string name, string mediaType = null)
        {
            if (contenStream == null)
                return this;

            var attachment = new Attachment(contenStream, name, mediaType);

            return WithAttachment(attachment);
        }

        public ExpressEmail WithAttachment(Attachment attachment)
        {
            if (attachment.ContentStream != null)
                _mailMessage.Attachments.Add(attachment);

            return this;
        }

        public ExpressEmail WithAttachment(IEnumerable<Attachment> attachments)
        {
            foreach (var attachment in attachments.Where(attachment => attachment.ContentStream != null))
            {
                _mailMessage.Attachments.Add(attachment);
            }

            return this;
        }

        public ExpressEmail WithAlternativeEmailConfiguration(ExpressEmailConfigurationElement alternativeEmailConfiguration)
        {
            if (alternativeEmailConfiguration == null || alternativeEmailConfiguration.SmtpConfiguration == null)
                throw new Exception("this alternative email configuration have invalid configuration, please check.");

            _emailConfiguration.AlternativeEmailConfiguration = alternativeEmailConfiguration;
            _useAlternativeSmtpConfiguration = true;

            return this;
        }

        public ExpressEmail UseAlternativeSmtpConfiguration()
        {
            if (_emailConfiguration.AlternativeEmailConfiguration == null)
                throw new Exception("alternative email configuration not configured in app.config or web.config.");

            _useAlternativeSmtpConfiguration = true;
            return this;
        }

        public void Send()
        {
            try
            {
                ValidateMailMessage();
                _smtpClient.Send(_mailMessage);
            }
            catch (ExpressEmailFromNotSuppliedException)
            {
                throw;
            }
            catch (ExpressEmailMailMessageNotSuppliedException)
            {
                throw;
            }
            catch (ExpressEmailToNotSuppliedException)
            {
                throw;
            }
            catch (Exception)
            {
                if (_useAlternativeSmtpConfiguration)
                {
                    ConfigSmtpClient(_emailConfiguration.AlternativeEmailConfiguration.SmtpConfiguration);
                    _smtpClient.Send(_mailMessage);
                }
                else
                    throw;
            }
        }

        private void ValidateMailMessage()
        {
            if (_mailMessage == null)
                throw new ExpressEmailMailMessageNotSuppliedException();

            if (_mailMessage.From == null)
                throw new ExpressEmailFromNotSuppliedException();

            if (!_mailMessage.To.Any())
                throw new ExpressEmailToNotSuppliedException();
        }

        public static ExpressEmailFactory Factory
        {
            get { return new ExpressEmailFactory(); }
        }

        private void ConfigSmtpClientWithMainEmailConfiguration()
        {
            ConfigSmtpClient(_emailConfiguration.MainEmailConfiguration.SmtpConfiguration);
        }

        private void ConfigMailMessage()
        {
            _mailMessage = new MailMessage();

            if (!_emailConfiguration.MainEmailConfiguration.FromEmail.IsNullOrWhiteSpace())
            {
                _mailMessage.From =
                    new MailAddress(_emailConfiguration.MainEmailConfiguration.FromEmail,
                        _emailConfiguration.MainEmailConfiguration.FromName);
            }

            if (!_emailConfiguration.MainEmailConfiguration.ToEmail.IsNullOrWhiteSpace())
            {
                _mailMessage.To.Add(new MailAddress(_emailConfiguration.MainEmailConfiguration.ToEmail,
                    _emailConfiguration.MainEmailConfiguration.ToName));
            }
        }

        private void ConfigSmtpClient(SmtpConfigurationElement smtpConfiguration)
        {
            _smtpClient = new SmtpClient
            {
                Host = smtpConfiguration.Host,
                Port = smtpConfiguration.Port,
                EnableSsl = smtpConfiguration.EnableSsl,
                UseDefaultCredentials = smtpConfiguration.UseDefaultCredentials,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials =
                    new NetworkCredential(smtpConfiguration.Credentials.UserName, smtpConfiguration.Credentials.Password)
            };
        }


        private string GetSplitAddress(EmailAddressSeparator emailAddressSeparator)
        {
            switch (emailAddressSeparator)
            {
                case EmailAddressSeparator.Colon:
                    return ",";

                case EmailAddressSeparator.Pipe:
                    return "|";

                case EmailAddressSeparator.SemiColon:
                    return ";";
            }

            return null;
        }

        public enum EmailAddressSeparator
        {
            Pipe,
            SemiColon,
            Colon
        }
    }
}
