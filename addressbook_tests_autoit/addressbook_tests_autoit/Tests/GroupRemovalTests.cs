using System.Collections.Generic;
using NUnit.Framework;

namespace addressbook_tests_autoit.Tests
{
    [TestFixture]
    class GroupRemovalTests : Testbase
    {
        [SetUp]
        public void Init()
        {
            app.Groups.AddIfNotExist();
        }

        [Test]
        public void GroupRemovalTest()
        {
            List<GroupData> oldGroups = app.Groups.GetGroupList();

            app.Groups.Remove(0);

            Assert.AreEqual(oldGroups.Count - 1, app.Groups.GetRootGroupsCount());

            List<GroupData> newGroups = app.Groups.GetGroupList();
            oldGroups.RemoveAt(0);
            oldGroups.Sort();
            newGroups.Sort();

            Assert.AreEqual(oldGroups, newGroups);
        }
    }
}
