using System.Collections.Generic;
using NUnit.Framework;

namespace addressbook_tests_white
{
    [TestFixture]
    public class ContactCreationTests : TestBase
    {
        [Test]
        public void ContactCreationTest()
        {
            List<ContactData> oldContacts = app.Contacts.GetContactsList();

            ContactData contact = new ContactData("Evgeniy", "Ivanov")
            {
                CompanyName = "SKB Kontur",
                City = "Ekb",
                Address = "Maloprudnaya, 5" 
            };

            app.Contacts.Add(contact);

            List<ContactData> newContacts = app.Contacts.GetContactsList();

            Assert.AreEqual(oldContacts.Count + 1, app.Contacts.GetContactsCount());

            oldContacts.Add(contact);
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);            
        }
    }
}
