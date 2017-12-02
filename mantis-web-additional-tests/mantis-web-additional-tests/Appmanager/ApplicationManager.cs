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

namespace mantis_web_additional_tests
{
    public class ApplicationManager
    {
        protected IWebDriver driver;
        protected string baseURL;

        protected RegistrationHelper registrationHelper;
        protected FtpHelper ftpHelper;
        protected JamesHelper jamesHelper;
        protected MailHelper mailHelper;
        protected NavigationHelper navigationHelper;

        private static ThreadLocal<ApplicationManager> app = new ThreadLocal<ApplicationManager>();

        public ApplicationManager()
        {
            FirefoxOptions options = new FirefoxOptions();
            options.BrowserExecutableLocation = @"c:\Program Files\Mozilla Firefox ESR\firefox.exe";
            options.UseLegacyImplementation = true;
            driver = new FirefoxDriver(options);
            baseURL = "http://localhost/";

            registrationHelper = new RegistrationHelper(this);
            ftpHelper = new FtpHelper(this);
            jamesHelper = new JamesHelper(this);
            mailHelper = new MailHelper(this);
            navigationHelper = new NavigationHelper(this, baseURL);
        }

        public static ApplicationManager GetInstance()
        {
            if (!app.IsValueCreated)
            {
                ApplicationManager newInstance = new ApplicationManager();
                app.Value = newInstance;
                newInstance.Navigator.OpenLoginPage();
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

        public RegistrationHelper Registration
        {
            get
            {
                return registrationHelper;
            }
        }

        public FtpHelper FTP
        {
            get
            {
                return ftpHelper;
            }
        }

        public JamesHelper James
        {
            get
            {
                return jamesHelper;
            }
        }

        public MailHelper Mail
        {
            get
            {
                return mailHelper;
            }
        }

        public NavigationHelper Navigator
        {
            get
            {
                return navigationHelper;
            }
        }
    }
}
