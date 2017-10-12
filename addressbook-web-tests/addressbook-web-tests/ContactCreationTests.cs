using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactCreationTests : TestBase
    {
        [Test]
        public void ContactCreationTest()
        {
            OpenHomePage();
            Login(new AccountData("admin", "secret"));
            InitContactCreation();
            FillContactForm(new ContactData("Evgeniy", "Ivanov"));
            SubmitContactCreation();
            ReturnToContactsPage();
            Logout();
        }
    }
}
