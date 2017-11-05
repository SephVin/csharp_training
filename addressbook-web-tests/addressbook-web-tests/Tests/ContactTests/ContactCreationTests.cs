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
        public static IEnumerable<ContactData> RandomContactDataProvider()
        {
            List<ContactData> contacts = new List<ContactData>();
            for (int i = 0; i < 5; i++)
            {
                contacts.Add(new ContactData(GenerateRandomString(15), GenerateRandomString(15))
                {
                    MiddleName = GenerateRandomString(15),
                    NickName = GenerateRandomString(15),

                    Company = GenerateRandomString(30),
                    Title = GenerateRandomString(10),
                    Address = GenerateRandomString(100),

                    HomePhone = GetRandomPhone(),
                    WorkPhone = GetRandomPhone(),
                    MobilePhone = GetRandomPhone(),
                    Fax = GenerateRandomString(15),

                    Email = GenerateRandomEmail(15),
                    Email2 = GenerateRandomEmail(15),
                    Email3 = GenerateRandomEmail(15),
                    HomePage = GenerateRandomHomePage(10),
                    BirthDay = GenerateRandomDay(),
                    BirthMonth = GenerateRandomMonth(),
                    BirthYear = GenerateRandomYear(),

                    AnniDay = GenerateRandomDay(),
                    AnniMonth = GenerateRandomMonth(),
                    AnniYear = GenerateRandomYear(),

                    SecondaryAddress = GenerateRandomString(100),
                    SecondaryHomePhone = GetRandomPhone(),
                    Notes = GenerateRandomString(100)
                });
            }
            return contacts;
        }

        [Test, TestCaseSource("RandomContactDataProvider")]
        public void ContactCreationTest(ContactData contact)
        {
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
