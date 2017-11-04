using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using System.Collections.Generic;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactCreationTests : AuthTestBase
    {
        [Test]
        public void ContactCreationTest()
        {
            ContactData contact = new ContactData("Evgeniy", "Ivanov")
            {
                Address = "Неизвестный адрес",
                HomePhone = "+7(952) 321-43-65",
                WorkPhone = "8(34365) 2 48 52",
                MobilePhone = "7 952 543 12 54",
                Email = "   email@bk.ru ",
                Email2 = "testemail@test.ru",
                Email3 = "anotheremail@test.ru"
            };


            List<ContactData> oldContacts = app.Contacts.GetContactList();

            app.Contacts.Create(contact);

            Assert.AreEqual(oldContacts.Count + 1, app.Contacts.GetContactCount());

            List<ContactData> newContacts = app.Contacts.GetContactList();
            oldContacts.Add(contact);
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);
        }
    }
}
