using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace mantis_web_tests
{
    public class ProjectManagementHelper : HelperBase
    {
        public ProjectManagementHelper(ApplicationManager manager) : base(manager)
        {
        }

        public ProjectManagementHelper CreateProject(ProjectData project)
        {
            if (manager.MenuManagment.IsManageProjectPageAlreadyOpen() == false)
            {
                manager.Sidebar.OpenManageOverviewPage();
                manager.MenuManagment.OpenManageProjectPage();
            }
                      
            InitCreatingProject();
            FillProjectCreationForm(project);
            SubmitProjectCreation();

            return this;
        }

        public ProjectManagementHelper RemoveProject(int index)
        {
            if (manager.MenuManagment.IsManageProjectPageAlreadyOpen() == false)
            {
                manager.Sidebar.OpenManageOverviewPage();
                manager.MenuManagment.OpenManageProjectPage();
            }

            InitRemovalProject(index);
            SubmitRemovalProject();

            return this;
        }

        public ProjectManagementHelper InitCreatingProject()
        {
            driver.FindElement(By.XPath("//button[.='создать новый проект']")).Click();

            return this;
        }
        public ProjectManagementHelper FillProjectCreationForm(ProjectData project)
        {
            Type(By.Name("name"), project.Name);
            Type(By.Name("description"), project.Description);
            SelectElement(By.Id("project-status"), project.Status);
            SelectElement(By.Id("project-view-state"), project.ViewState);

            return this;
        }

        public ProjectManagementHelper SubmitProjectCreation()
        {
            driver.FindElement(By.XPath("//input[@value='Добавить проект']")).Click();
            driver.FindElement(By.LinkText("Продолжить")).Click();

            return this;
        }

        public ProjectManagementHelper InitRemovalProject(int index)
        {
            driver.FindElement(By.ClassName("widget-box"))
                  .FindElement(By.TagName("Table"))
                  .FindElement(By.TagName("tbody"))
                  .FindElements(By.TagName("tr"))[index]
                  .FindElement(By.TagName("a")).Click();

            return this;
        }

        public ProjectManagementHelper SubmitRemovalProject()
        {
            driver.FindElement(By.XPath("//input[@value='Удалить проект']")).Click();
            driver.FindElement(By.XPath("//input[@value='Удалить проект']")).Click();

            return this;
        }

        public List<ProjectData> GetProjectsList()
        {
            List<ProjectData> list = new List<ProjectData>();

            if (manager.MenuManagment.IsManageProjectPageAlreadyOpen() == false)
            {
                manager.Sidebar.OpenManageOverviewPage();
                manager.MenuManagment.OpenManageProjectPage();
            }

            ICollection<IWebElement> tableRows = driver.FindElement(By.ClassName("widget-box"))
                                                   .FindElement(By.TagName("Table"))
                                                   .FindElement(By.TagName("tbody"))
                                                   .FindElements(By.TagName("tr"));

            if (tableRows.Count == 0)
            {
                return list;
            }

            foreach (IWebElement row in tableRows)
            {
                IList<IWebElement> cells = row.FindElements(By.TagName("td"));

                string name = cells[0].Text;
                string status = cells[1].Text;
                string enabled = "";
                if (cells[2].Text == " ")
                    enabled = "false";
                else
                    enabled = "true";
                string viewState = cells[3].Text;
                string description = cells[4].Text;

                list.Add(new ProjectData(name)
                {
                    Status = status,
                    Enabled = enabled,
                    ViewState = viewState,
                    Description = description
                });
            }

            return list;
        }

        public void DeleteProjectIfAlreadyExist(ProjectData project)
        {
            List<ProjectData> list = GetProjectsList();

            if (list.Count == 0)
                return;

            var indexOfExistProject = list.FindIndex(x => x.Name == project.Name);

            if (indexOfExistProject != -1)
            {
                RemoveProject(indexOfExistProject);
            }
        }

        public int GetProjectsCount()
        {
            if (manager.MenuManagment.IsManageProjectPageAlreadyOpen() == false)
            {
                manager.Sidebar.OpenManageOverviewPage();
                manager.MenuManagment.OpenManageProjectPage();
            }

            var tableRows = driver.FindElement(By.ClassName("widget-box"))
                              .FindElement(By.TagName("Table"))
                              .FindElement(By.TagName("tbody"))
                              .FindElements(By.TagName("tr"));

            return tableRows.Count;
        }

        public void CreateIfNotExist()
        {
            if (GetProjectsCount() == 0)
            {
                CreateProject(new ProjectData("Тест"));
            }
        }
    }
}
