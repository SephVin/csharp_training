using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System.Text.RegularExpressions;

namespace WebAddressbookTests
{
    public class ContactHelper : HelperBase
    {
        public ContactHelper(ApplicationManager manager) : base (manager)
        {
        }

        public ContactHelper Create(ContactData contact)
        {
            manager.Navigator.OpenHomePage();

            InitContactCreation();
            FillContactForm(contact);
            SubmitContactCreation();
            ReturnToContactsPage();

            return this;
        }

        public ContactHelper Modify(int index, ContactData newData)
        {
            manager.Navigator.OpenHomePage();

            InitContactModification(index);
            FillContactForm(newData);
            SubmitContactModification();
            ReturnToContactsPage();

            return this;
        }

        public ContactHelper Remove(int index)
        {
            manager.Navigator.OpenHomePage();

            SelectContact(index);
            RemoveContact();

            return this;
        }

        public ContactHelper InitContactCreation()
        {
            driver.FindElement(By.LinkText("add new")).Click();
            return this;
        }

        public ContactHelper InitContactModification(int index)
        {
            driver.FindElements(By.Name("entry"))[index]
                  .FindElements(By.TagName("td"))[7]
                  .FindElement(By.TagName("a")).Click();

            return this;
        }

        public ContactHelper FillContactForm(ContactData contact)
        {
            Type(By.Name("firstname"), contact.FirstName);
            Type(By.Name("lastname"), contact.LastName);

            Type(By.Name("home"), contact.HomePhone);
            Type(By.Name("mobile"), contact.MobilePhone);
            Type(By.Name("work"), contact.WorkPhone);

            Type(By.Name("email"), contact.Email);
            Type(By.Name("email2"), contact.Email2);
            Type(By.Name("email3"), contact.Email3);

            return this;
        }

        public ContactHelper SubmitContactCreation()
        {
            driver.FindElement(By.Name("submit")).Click();
            contactCache = null;

            return this;
        }

        public ContactHelper SubmitContactModification()
        {
            driver.FindElement(By.Name("update")).Click();
            contactCache = null;

            return this;
        }

        public ContactHelper ReturnToContactsPage()
        {
            driver.FindElement(By.LinkText("home page")).Click();
            return this;
        }

        public ContactHelper SelectContact(int index)
        {
            driver.FindElements(By.Name("entry"))[index]
                  .FindElements(By.TagName("td"))[0]
                  .FindElement(By.Name("selected[]")).Click();

            return this;
        }

        public ContactHelper RemoveContact()
        {
            driver.FindElement(By.XPath("//input[@value='Delete']")).Click();
            driver.SwitchTo().Alert().Accept();
            contactCache = null;

            return this;
        }

        public bool IsContactExist()
        {
            return IsElementPresent(By.Name("entry"));
        }

        public ContactHelper CreateIfNotExist()
        {
            manager.Navigator.OpenHomePage();

            if (IsContactExist() == false)
            {
                ContactData data = new ContactData("Sergei", "Sidorov")
                {
                    Address = "г.Екатеринбург, ул.Ленина, д.29, кв.7",
                    HomePhone = "+7(952) 321-43-65",
                    WorkPhone = "8(34365) 2 48 52",
                    MobilePhone = "7 952 543 12 54",
                    Email = "   email@bk.ru ",
                    Email2 = "testemail@test.ru",
                    Email3 = "anotheremail@test.ru"
                };

                Create(data);
            }

            return this;
        }

        private List<ContactData> contactCache = null;

        public List<ContactData> GetContactList()
        {
            if (contactCache == null)
            {
                contactCache = new List<ContactData>();
                manager.Navigator.OpenHomePage();

                List<ContactData> contacts = new List<ContactData>();
                ICollection<IWebElement> elements = driver.FindElements(By.Name("entry"));
                foreach (IWebElement element in elements)
                {
                    IList<IWebElement> cells = element.FindElements(By.TagName("td"));
                    string id = cells[0].FindElement(By.Name("selected[]")).GetAttribute("value");
                    string lastName = cells[1].Text;
                    string firstName = cells[2].Text;

                    contactCache.Add(new ContactData(firstName, lastName)
                    {
                        Id = id
                    });
                }
            }    

            return new List<ContactData>(contactCache);
        }

        public int GetContactCount()
        {
            manager.Navigator.OpenHomePage();
            return driver.FindElements(By.Name("entry")).Count;
        }

        public ContactData GetContactInformationFromTable(int index)
        {
            manager.Navigator.OpenHomePage();

            IList<IWebElement> cells = driver.FindElements(By.Name("entry"))[index]
                                             .FindElements(By.TagName("td"));
            string lastName = cells[1].Text;
            string firstName = cells[2].Text;
            string address = cells[3].Text;
            string allEmails = cells[4].Text;
            string allPhones = cells[5].Text;

            return new ContactData(firstName, lastName)
            {
                Address = address,
                AllEmails = allEmails,
                AllPhones = allPhones
            };
        }

        public ContactData GetContactInformationFromEditForm(int index)
        {
            manager.Navigator.OpenHomePage();
            InitContactModification(index);

            string firstName = driver.FindElement(By.Name("firstname")).GetAttribute("value");
            string lastName = driver.FindElement(By.Name("lastname")).GetAttribute("value");
            string address = driver.FindElement(By.Name("address")).GetAttribute("value");

            string homePhone = driver.FindElement(By.Name("home")).GetAttribute("value");
            string mobilePhone = driver.FindElement(By.Name("mobile")).GetAttribute("value");
            string workPhone = driver.FindElement(By.Name("work")).GetAttribute("value");

            string email = driver.FindElement(By.Name("email")).GetAttribute("value");
            string email2 = driver.FindElement(By.Name("email2")).GetAttribute("value");
            string email3 = driver.FindElement(By.Name("email3")).GetAttribute("value");

            return new ContactData(firstName, lastName)
            {
                Address = address,
                Email = email,
                Email2 = email2,
                Email3 = email3,
                HomePhone = homePhone,
                MobilePhone = mobilePhone,
                WorkPhone = workPhone
            };
        }

        public int GetNumberOfSearchResults()
        {
            manager.Navigator.OpenHomePage();

            string text = driver.FindElement(By.TagName("label")).Text;
            Match m = new Regex(@"\d+").Match(text);

            return int.Parse(m.Value);
        }
    }
}
