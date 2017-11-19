using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    public class RemovingContactFromGroupTests : AuthTestBase
    {
        [SetUp]
        public void Init()
        {
            app.Contacts.CreateIfNotExist();
            app.Groups.CreateIfNotExist();
            app.Contacts.CreateContactLinkToGroupIfNotExist(0);
        }

        [Test]
        public void RemovingContactFromGroupTest()
        {     
            GroupData group = GroupData.GetAll()[0];
            List<ContactData> oldContactsInGroupList = group.GetContacts();
            ContactData toBeRemoved = oldContactsInGroupList[0];

            app.Contacts.RemoveContactFromGroup(toBeRemoved, group);

            List<ContactData> newContactsInGroupList = group.GetContacts();

            Assert.AreEqual(oldContactsInGroupList.Count - 1, newContactsInGroupList.Count());

            oldContactsInGroupList.RemoveAt(0);
            oldContactsInGroupList.Sort();
            newContactsInGroupList.Sort();
            Assert.AreEqual(oldContactsInGroupList, newContactsInGroupList);

            foreach (ContactData contact in newContactsInGroupList)
            {
                Assert.AreNotEqual(contact.Id, toBeRemoved.Id);
            }
        }
    }
}
