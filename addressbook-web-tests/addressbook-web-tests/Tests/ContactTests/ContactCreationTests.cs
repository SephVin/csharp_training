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
                MiddleName = "Sergeevich",
                NickName = "SephVin",

                Company = "SKB Kontur",
                Title = "TitleName",                
                Address = "г.Екатеринбург, ул.Ленина, д.29, кв.7",

                HomePhone = "+7(952) 321-43-65",
                WorkPhone = "8(34365) 2 48 52",
                MobilePhone = "7 952 543 12 54",
                Fax = "666 777",

                Email = "   email@bk.ru ",
                Email2 = "testemail@test.ru",
                Email3 = "anotheremail@test.ru",
                HomePage = "http://testpage.ru",

                BirthDay = "5",
                BirthMonth = "February",
                BirthYear = "1991",

                AnniDay = "5",
                AnniMonth = "February",
                AnniYear = "1991",

                SecondaryAddress = "г.Асбест, ул.Мира, д.8, кв.88",
                SecondaryHomePhone = "нет",
                Notes = "MyNotes"
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
