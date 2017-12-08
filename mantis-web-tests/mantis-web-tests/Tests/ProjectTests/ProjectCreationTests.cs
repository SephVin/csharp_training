using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;

namespace mantis_web_tests
{
    [TestFixture]
    public class ProjectCreationTests : TestBase
    {
        [SetUp]
        public void Init()
        {
            app.API.DeleteProjectIfAlreadyExist(account, new ProjectData("test"));
        }

        [Test]
        public void ProjectCreationTest()
        {     
            List<ProjectData> oldProjectsList = app.API.GetProjectsList(account);

            ProjectData project = new ProjectData("test1")
            {     
                Status = "development",
                ViewState = "public",
                Description = "Это тест",
                Enabled = "True"
            };

            app.API.CreateProject(account, project);

            List<ProjectData> newProjectsList = app.API.GetProjectsList(account);

            Assert.AreEqual(oldProjectsList.Count + 1, app.API.GetProjectsCount(account));

            oldProjectsList.Add(project);
            oldProjectsList.Sort();
            newProjectsList.Sort();
            Assert.AreEqual(oldProjectsList, newProjectsList);
        }
    }
}
