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
    public class LoginHelper : HelperBase
    {
        private string baseURL;

        public LoginHelper(ApplicationManager manager, string baseURL) : base(manager)
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

        public void Login(AccountData account)
        {
            if (IsLoggedIn())
            {
                if (IsLoggedIn(account))
                {
                    return;
                }

                Logout();
            }

            Type(By.Name("username"), account.Username);
            driver.FindElement(By.XPath("//input[@value='Войти']")).Click();

            Type(By.Name("password"), account.Password);
            driver.FindElement(By.XPath("//input[@value='Войти']")).Click();
        }

        private bool IsLoggedIn()
        {
            return IsElementPresent(By.CssSelector("span.user-info"));
        }

        private bool IsLoggedIn(AccountData account)
        {
            return IsLoggedIn() && GetLoggedUserName() == account.Username;
        }

        private string GetLoggedUserName()
        {
            return driver.FindElement(By.CssSelector("span.user-info")).Text;
        }

        private void Logout()
        {
            driver.FindElement(By.CssSelector("span.user-info")).Click();
            driver.FindElement(By.LinkText("выход")).Click();
        }        
    }
}
