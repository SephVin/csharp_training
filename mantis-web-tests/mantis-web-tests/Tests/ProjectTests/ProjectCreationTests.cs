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
    public class ProjectCreationTests : AuthTestBase
    {
        [SetUp]
        public void Init()
        {
            app.ProjectManagement.DeleteProjectIfAlreadyExist(new ProjectData("test"));
        }

        [Test]
        public void ProjectCreationTest()
        {
            List<ProjectData> oldProjectsList = app.ProjectManagement.GetProjectsList();

            ProjectData project = new ProjectData("test")
            {                
                Status = "в разработке",
                ViewState = "публичная",
                Description = "Это тест",
                Enabled = "true"
            };

            app.ProjectManagement.CreateProject(project);

            List<ProjectData> newProjectsList = app.ProjectManagement.GetProjectsList();

            Assert.AreEqual(oldProjectsList.Count + 1, app.ProjectManagement.GetProjectsCount());

            project.ViewState = "публичный"; // на списке проектов окончание "ый"
            oldProjectsList.Add(project);
            oldProjectsList.Sort();
            newProjectsList.Sort();
            Assert.AreEqual(oldProjectsList, newProjectsList);
        }
    }
}
