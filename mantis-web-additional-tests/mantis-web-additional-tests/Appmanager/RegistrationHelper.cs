using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System.Text.RegularExpressions;

namespace mantis_web_additional_tests
{
    public class RegistrationHelper : HelperBase
    {
        public RegistrationHelper(ApplicationManager manager) : base(manager)
        {
        }

        public void Register(AccountData account)
        {
            manager.Navigator.OpenLoginPage();

            OpenRegistationForm();
            FillRegistrationForm(account);
            SubmitRegistration();

            string url = GetConfirmationUrl(account);
            FillPasswordForm(url, account);
            SubmitPasswordForm();
        }

        public RegistrationHelper OpenRegistationForm()
        {
            driver.FindElement(By.CssSelector("div.toolbar"))
                  .FindElement(By.TagName("a")).Click();

            return this;
        }

        public RegistrationHelper SubmitRegistration()
        {
            driver.FindElement(By.XPath("//input[@value='Зарегистрироваться']")).Click();

            return this;
        }

        public RegistrationHelper FillRegistrationForm(AccountData account)
        {
            driver.FindElement(By.Name("username")).SendKeys(account.Username);
            driver.FindElement(By.Name("email")).SendKeys(account.Email);

            return this;
        }

        public string GetConfirmationUrl(AccountData account)
        {
            string message = manager.Mail.GetLastMail(account);
            Match match = Regex.Match(message, @"http://\S*");

            return match.Value;
        }

        public RegistrationHelper FillPasswordForm(string url, AccountData account)
        {
            driver.Url = url;

            driver.FindElement(By.Name("password")).SendKeys(account.Password);
            driver.FindElement(By.Name("password_confirm")).SendKeys(account.Password);

            return this;
        }

        public RegistrationHelper SubmitPasswordForm()
        {
            driver.FindElement(By.XPath("//button[@type='submit']")).Click();

            return this;
        }
    }
}
