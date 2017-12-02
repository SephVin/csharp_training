using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System.Threading;

namespace mantis_web_tests
{
    public class ApplicationManager
    {
        protected IWebDriver driver;
        protected string baseURL;

        protected LoginHelper loginHelper;
        protected MenuManagmentHelper menuManagmentHelper;
        protected ProjectManagementHelper projectManagementHelper;
        protected SidebarHelper sidebarHelper;

        private static ThreadLocal<ApplicationManager> app = new ThreadLocal<ApplicationManager>();

        public ApplicationManager()
        {
            FirefoxOptions options = new FirefoxOptions();
            options.BrowserExecutableLocation = @"c:\Program Files\Mozilla Firefox ESR\firefox.exe";
            options.UseLegacyImplementation = true;
            driver = new FirefoxDriver(options);
            baseURL = "http://localhost/";

            loginHelper = new LoginHelper(this, baseURL);
            menuManagmentHelper = new MenuManagmentHelper(this, baseURL);
            projectManagementHelper = new ProjectManagementHelper(this);
            sidebarHelper = new SidebarHelper(this, baseURL);
        }

        public static ApplicationManager GetInstance()
        {
            if (!app.IsValueCreated)
            {
                ApplicationManager newInstance = new ApplicationManager();
                app.Value = newInstance;
                newInstance.Auth.OpenLoginPage();
            }

            return app.Value;
        }

        ~ApplicationManager()
        {
            try
            {
                driver.Quit();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
        }

        public IWebDriver Driver
        {
            get
            {
                return driver;
            }
        }

        public LoginHelper Auth
        {
            get
            {
                return loginHelper;
            }
        }

        public MenuManagmentHelper MenuManagment
        {
            get
            {
                return menuManagmentHelper;
            }
        }

        public ProjectManagementHelper ProjectManagement
        {
            get
            {
                return projectManagementHelper;
            }
        }

        public SidebarHelper Sidebar
        {
            get
            {
                return sidebarHelper;
            }
        }
    }
}
