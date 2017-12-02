using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mantis_web_additional_tests
{
    public class NavigationHelper : HelperBase
    {
        private string baseURL;

        public NavigationHelper(ApplicationManager manager, string baseURL) : base(manager)
        {
            this.baseURL = baseURL;
        }

        public void OpenLoginPage()
        {
            if (driver.Url == baseURL + "mantisbt-2.8.0/login_page.php")
            {
                return;
            }

            driver.Navigate().GoToUrl(baseURL + "mantisbt-2.8.0/login_page.php");
        }
    }
}
