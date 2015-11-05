using System.Runtime.InteropServices;
using NUnit.Framework;

namespace ExpressEmail.Tests
{
    [TestFixture]
    public class ExpressEmailTests
    {
        [Test]
        public void Can_Send_Email_With_Config_File()
        {
            Assert.DoesNotThrow(() =>
            {
                ExpressEmail.Factory.Create()
                    .From("email@example", "Luis Ramirez")
                    .To("email@example", "Luis Ramirez")
                    .WithBody("TEST EMAIL")
                    .Send();
            });
        }
    }
}
