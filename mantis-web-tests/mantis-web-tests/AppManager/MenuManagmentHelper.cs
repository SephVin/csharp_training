using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace mantis_web_tests
{
    public class MenuManagmentHelper : HelperBase
    {
        private string baseURL;

        public MenuManagmentHelper(ApplicationManager manager, string baseURL) : base(manager)
        {
            this.baseURL = baseURL;
        }

        public void OpenManageProjectPage()
        {
            if (driver.Url == baseURL + "mantisbt-2.8.0/manage_proj_page.php")
            {
                return;
            }

            driver.FindElement(By.LinkText("Управление проектами")).Click();
        }

        public bool IsManageProjectPageAlreadyOpen()
        {
            if (driver.Url == baseURL + "mantisbt-2.8.0/manage_proj_page.php")
            {
                return true;
            }

            return false;
        }
    }
}
