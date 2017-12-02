using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace mantis_web_tests
{
    public class SidebarHelper : HelperBase
    {
        private string baseURL;

        public SidebarHelper(ApplicationManager manager, string baseURL) : base(manager)
        {
            this.baseURL = baseURL;
        }

        public void OpenManageOverviewPage()
        {
            if (driver.Url == baseURL + "mantisbt-2.8.0/manage_overview_page.php")
            {
                return;
            }

            driver.FindElement(By.Id("sidebar"))
                  .FindElements(By.TagName("li")).Last()
                  .FindElement(By.TagName("a")).Click();
        }
    }
}
