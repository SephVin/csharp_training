using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactSearchTests : AuthTestBase
    {
        [Test]
        public void ContactSearchTest()
        {
            Console.Out.Write(app.Contacts.GetNumberOfSearchResults());
        }
    }
}
