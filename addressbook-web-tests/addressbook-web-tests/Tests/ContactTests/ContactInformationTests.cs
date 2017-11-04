using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactInformationTests : AuthTestBase
    {
        [SetUp]
        public void Init()
        {
            app.Contacts.CreateIfNotExist();
        }

        [Test]
        public void ContactInformationTest()
        {
            ContactData fromTable = app.Contacts.GetContactInformationFromTable(0);
            ContactData fromEditForm = app.Contacts.GetContactInformationFromEditForm(0);

            Assert.AreEqual(fromTable, fromEditForm);
            Assert.AreEqual(fromTable.Address, fromEditForm.Address);
            Assert.AreEqual(fromTable.AllEmails, fromEditForm.AllEmails);
            Assert.AreEqual(fromTable.AllPhones, fromEditForm.AllPhones);
        }

        [Test]
        public void ContactDetailsInformationTest()
        {
            ContactData fromEditForm = app.Contacts.GetContactInformationFromEditForm(0);
            ContactData fromDetailsForm = app.Contacts.GetContactInformationFromDetailsForm(0);

            Assert.AreEqual(fromDetailsForm.ContactDetails, fromEditForm.ContactDetails);
        }
    }
}
