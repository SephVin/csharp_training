using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace mantis_web_tests
{
    [TestFixture]
    public class ProjectRemovalTests : TestBase
    {
        [SetUp]
        public void Init()
        {
            app.API.CreateIfNotExist(account);
        }

        [Test]
        public void RemoveProject()
        {
            List<ProjectData> oldProjectsList = app.API.GetProjectsList(account);

            app.API.RemoveProject(account, 0);

            List<ProjectData> newProjectsList = app.API.GetProjectsList(account);

            Assert.AreEqual(oldProjectsList.Count - 1, app.API.GetProjectsCount(account));

            oldProjectsList.RemoveAt(0);
            oldProjectsList.Sort();
            newProjectsList.Sort();
            Assert.AreEqual(oldProjectsList, newProjectsList);
        }
    }
}
