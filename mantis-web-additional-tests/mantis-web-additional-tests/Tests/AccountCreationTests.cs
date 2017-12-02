using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using System.IO;

namespace mantis_web_additional_tests 
{
    [TestFixture]
    class AccountCreationTests : TestBase
    {
        [TestFixtureSetUp]
        public void SetUpConfig()
        {
            app.FTP.BackupFile("/config_inc.php");
            using (Stream localfile = File.Open("Config/config_inc.php", FileMode.Open))
            {
                app.FTP.Upload("/config_inc.php", localfile);
            }
        }

        [Test]
        public void AccountRegistrationTest()
        {
            AccountData account = new AccountData("username10", "password")
            {
                Email = "username10@localhost.localdomain"
            };

            app.James.Delete(account);
            app.James.Add(account);

            app.Registration.Register(account);
        }

        [TestFixtureSetUp]
        public void RestoreConfig()
        {
            app.FTP.RestoreBackupFile("config_inc.php");
        }
    }
}
