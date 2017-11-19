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

        public ContactHelper Modify(ContactData oldData, ContactData newData)
        {
            manager.Navigator.OpenHomePage();

            InitContactModification(oldData.Id);
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

        public ContactHelper Remove(ContactData contact)
        {
            manager.Navigator.OpenHomePage();

            SelectContact(contact.Id);
            RemoveContact();

            return this;
        }

        public ContactHelper AddContactToGroup(ContactData contact, GroupData group)
        {
            manager.Navigator.OpenHomePage();

            ClearGroupFilter();
            SelectContact(contact.Id);
            SelectGroupToAdd(group.Name);
            CommitAddingContactToGroup();
            new WebDriverWait(driver, TimeSpan.FromSeconds(10))
                .Until(d => d.FindElements(By.CssSelector("div.msgbox")).Count > 0);

            return this;
        }

        public ContactHelper RemoveContactFromGroup(ContactData contact, GroupData group)
        {
            manager.Navigator.OpenHomePage();
            
            SelectGroupFromFilter(group.Name);
            SelectContact(contact.Id);
            CommitRemovingContactFromGroup();
            new WebDriverWait(driver, TimeSpan.FromSeconds(10))
                .Until(d => d.FindElements(By.CssSelector("div.msgbox")).Count > 0);


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

        public ContactHelper InitContactModification(string id)
        {
            driver.FindElement(By.XPath(string.Format("//a[@href='edit.php?id={0}']", id))).Click();

            return this;
        }

        public ContactHelper FillContactForm(ContactData contact)
        {
            Type(By.Name("firstname"), contact.FirstName);
            Type(By.Name("middlename"), contact.MiddleName);
            Type(By.Name("lastname"), contact.LastName);
            Type(By.Name("nickname"), contact.NickName);

            Type(By.Name("company"), contact.Company);
            Type(By.Name("title"), contact.Title);
            Type(By.Name("address"), contact.Address);

            Type(By.Name("home"), contact.HomePhone);
            Type(By.Name("mobile"), contact.MobilePhone);
            Type(By.Name("work"), contact.WorkPhone);
            Type(By.Name("fax"), contact.Fax);

            Type(By.Name("email"), contact.Email);
            Type(By.Name("email2"), contact.Email2);
            Type(By.Name("email3"), contact.Email3);
            Type(By.Name("homepage"), contact.HomePage);

            SelectElement(By.Name("bday"), contact.BirthDay);
            SelectElement(By.Name("bmonth"), contact.BirthMonth);
            Type(By.Name("byear"), contact.BirthYear);

            SelectElement(By.Name("aday"), contact.AnniDay);
            SelectElement(By.Name("amonth"), contact.AnniMonth);
            Type(By.Name("ayear"), contact.AnniYear);

            Type(By.Name("address2"), contact.SecondaryAddress);
            Type(By.Name("phone2"), contact.SecondaryHomePhone);
            Type(By.Name("notes"), contact.Notes);

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

        public ContactHelper SelectContact(string id)
        {
            driver.FindElement(By.XPath(string.Format("//input[@name='selected[]' and @id={0}]", id))).Click();

            return this;
        }

        public ContactHelper RemoveContact()
        {
            driver.FindElement(By.XPath("//input[@value='Delete']")).Click();
            driver.SwitchTo().Alert().Accept();
            contactCache = null;

            return this;
        }

        public ContactHelper SelectGroupFromFilter(string groupName)
        {
            new SelectElement(driver.FindElement(By.Name("group"))).SelectByText(groupName);
            return this;
        }

        public ContactHelper SelectGroupToAdd(string name)
        {
            new SelectElement(driver.FindElement(By.Name("to_group"))).SelectByText(name);
            return this;
        }

        public ContactHelper CommitAddingContactToGroup()
        {
            driver.FindElement(By.Name("add")).Click();
            return this;
        }

        public ContactHelper CommitRemovingContactFromGroup()
        {
            driver.FindElement(By.Name("remove")).Click();
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
                    MiddleName = "Sergeevich",
                    NickName = "SephVin",

                    Company = "SKB Kontur",
                    Title = "TitleName",
                    Address = "г.Екатеринбург, ул.Ленина, д.29, кв.7",

                    HomePhone = "+7(952) 321-43-65",
                    WorkPhone = "8(34365) 2 48 52",
                    MobilePhone = "7 952 543 12 54",
                    Fax = "666 777",

                    Email = "   email@bk.ru ",
                    Email2 = "testemail@test.ru",
                    Email3 = "anotheremail@test.ru",
                    HomePage = "http://testpage.ru",

                    BirthDay = "5",
                    BirthMonth = "February",
                    BirthYear = "1991",

                    AnniDay = "5",
                    AnniMonth = "February",
                    AnniYear = "1991",

                    SecondaryAddress = "г.Асбест, ул.Мира, д.8, кв.88",
                    SecondaryHomePhone = "нет",
                    Notes = "MyNotes"
                };

                Create(data);
            }

            return this;
        }

        public ContactHelper AddContactToGroupIfLinkNotExist(GroupData group)
        {
            List<ContactData> contactsInGroup = group.GetContacts();

            if (contactsInGroup.Count == 0)
                AddContactToGroup(GetContactList()[0], group);

            return this;
        }
        public ContactHelper CreateContactLinkToGroupIfNotExist(int index)
        {
            manager.Navigator.OpenHomePage();

            GroupData group = GroupData.GetAll()[index];
            List<ContactData> oldContactsInGroupList = group.GetContacts();
            if (oldContactsInGroupList.Count == 0)
            {
                AddContactToGroup(GetContactList()[index], group);
                oldContactsInGroupList = group.GetContacts();
            }

            return this;
        }


        public ContactHelper ClearGroupFilter()
        {
            new SelectElement(driver.FindElement(By.Name("group"))).SelectByText("[all]");
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
            string middleName = driver.FindElement(By.Name("middlename")).GetAttribute("value");
            string lastName = driver.FindElement(By.Name("lastname")).GetAttribute("value");
            string nickName = driver.FindElement(By.Name("nickname")).GetAttribute("value");

            string title = driver.FindElement(By.Name("title")).GetAttribute("value");
            string company = driver.FindElement(By.Name("company")).GetAttribute("value");
            string address = driver.FindElement(By.Name("address")).GetAttribute("value");

            string homePhone = driver.FindElement(By.Name("home")).GetAttribute("value");
            string mobilePhone = driver.FindElement(By.Name("mobile")).GetAttribute("value");
            string workPhone = driver.FindElement(By.Name("work")).GetAttribute("value");
            string fax = driver.FindElement(By.Name("fax")).GetAttribute("value");

            string email = driver.FindElement(By.Name("email")).GetAttribute("value");
            string email2 = driver.FindElement(By.Name("email2")).GetAttribute("value");
            string email3 = driver.FindElement(By.Name("email3")).GetAttribute("value");
            string homePage = driver.FindElement(By.Name("homepage")).GetAttribute("value");

            string bDay = driver.FindElement(By.XPath("//select[@name='bday']/option[@selected='selected']")).GetAttribute("value");
            string bMonth = driver.FindElement(By.XPath("//select[@name='bmonth']/option[@selected='selected']")).GetAttribute("value");
            string bYear = driver.FindElement(By.Name("byear")).GetAttribute("value");

            string aDay = driver.FindElement(By.XPath("//select[@name='aday']/option[@selected='selected']")).GetAttribute("value");
            string aMonth = driver.FindElement(By.XPath("//select[@name='amonth']/option[@selected='selected']")).GetAttribute("value");
            string aYear = driver.FindElement(By.Name("ayear")).GetAttribute("value");

            string secondaryAddress = driver.FindElement(By.Name("address2")).GetAttribute("value");
            string secondaryHomePhone = driver.FindElement(By.Name("phone2")).GetAttribute("value");
            string notes = driver.FindElement(By.Name("notes")).GetAttribute("value");

            return new ContactData(firstName, lastName)
            {
                MiddleName = middleName,
                NickName = nickName,

                Company = company,
                Title = title,
                Address = address,

                HomePhone = homePhone,
                MobilePhone = mobilePhone,
                WorkPhone = workPhone,

                Fax = fax,
                Email = email,
                Email2 = email2,
                Email3 = email3,
                HomePage = homePage,

                BirthDay = bDay,
                BirthMonth = bMonth,
                BirthYear = bYear,

                AnniDay = aDay,
                AnniMonth = aMonth,
                AnniYear = aYear,

                SecondaryAddress = secondaryAddress,
                SecondaryHomePhone = secondaryHomePhone,
                Notes = notes
            };
        }

        public ContactData GetContactInformationFromDetailsForm(int index)
        {
            manager.Navigator.OpenHomePage();

            driver.FindElements(By.Name("entry"))[index]
                  .FindElements(By.TagName("td"))[6]
                  .FindElement(By.TagName("a")).Click();

            string contactDetails = driver.FindElement(By.CssSelector("div#content")).Text;

            return new ContactData(null, null)
            {
                ContactDetails = contactDetails
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
