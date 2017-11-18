using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using NUnit.Framework;
using System.Collections.Generic;
using Newtonsoft.Json;
using Excel = Microsoft.Office.Interop.Excel;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactCreationTests : ContactTestBase
    {
        public static IEnumerable<ContactData> RandomContactDataProvider()
        {
            List<ContactData> contacts = new List<ContactData>();
            for (int i = 0; i < 5; i++)
            {
                contacts.Add(new ContactData(GenerateRandomString(15), GenerateRandomString(15))
                {
                    MiddleName = GenerateRandomString(15),
                    NickName = GenerateRandomString(15),

                    Company = GenerateRandomString(30),
                    Title = GenerateRandomString(10),
                    Address = GenerateRandomString(100),

                    HomePhone = GetRandomPhone(),
                    WorkPhone = GetRandomPhone(),
                    MobilePhone = GetRandomPhone(),
                    Fax = GetRandomPhone(),

                    Email = GenerateRandomEmail(15),
                    Email2 = GenerateRandomEmail(15),
                    Email3 = GenerateRandomEmail(15),
                    HomePage = GenerateRandomHomePage(10),
                    BirthDay = GenerateRandomDay(),
                    BirthMonth = GenerateRandomMonth(),
                    BirthYear = GenerateRandomYear(),

                    AnniDay = GenerateRandomDay(),
                    AnniMonth = GenerateRandomMonth(),
                    AnniYear = GenerateRandomYear(),

                    SecondaryAddress = GenerateRandomString(100),
                    SecondaryHomePhone = GetRandomPhone(),
                    Notes = GenerateRandomString(100)
                });
            }

            return contacts;
        }

        public static IEnumerable<ContactData> ContactDataFromCsvFile()
        {
            List<ContactData> contacts = new List<ContactData>();
            string[] lines = File.ReadAllLines(@"Files\ContactsTestData\contacts.csv");

            foreach (string line in lines)
            {
                string[] parts = line.Split('|');
                contacts.Add(new ContactData()
                {
                    FirstName = parts[0],
                    LastName = parts[1],
                    MiddleName = parts[2],
                    NickName = parts[3],

                    Company = parts[4],
                    Title = parts[5],
                    Address = parts[6],

                    HomePhone = parts[7],
                    WorkPhone = parts[8],
                    MobilePhone = parts[9],
                    Fax = parts[10],

                    Email = parts[11],
                    Email2 = parts[12],
                    Email3 = parts[13],
                    HomePage = parts[14],
                    BirthDay = parts[15],
                    BirthMonth = parts[16],
                    BirthYear = parts[17],

                    AnniDay = parts[18],
                    AnniMonth = parts[19],
                    AnniYear = parts[20],

                    SecondaryAddress = parts[21],
                    SecondaryHomePhone = parts[22],
                    Notes = parts[23]
                });
            }
            
            return contacts;
        }

        public static IEnumerable<ContactData> ContactDataFromXmlFile()
        {
            return (List<ContactData>)
                new XmlSerializer(typeof(List<ContactData>)).Deserialize(new StreamReader(@"Files\ContactsTestData\contacts.xml"));
        }

        public static IEnumerable<ContactData> ContactDataFromJsonFile()
        {
            return JsonConvert.DeserializeObject<List<ContactData>>(File.ReadAllText(@"Files\ContactsTestData\contacts.json"));
        }

        public static IEnumerable<ContactData> ContactDataFromExcelFile()
        {
            List<ContactData> contacts = new List<ContactData>();
            Excel.Application app = new Excel.Application();
            app.Visible = true;
            Excel.Workbook wb = app.Workbooks.Open(Path.Combine(Directory.GetCurrentDirectory(), @"Files\ContactsTestData\contacts.xlsx"));
            Excel.Worksheet sheet = wb.Sheets[1];
            Excel.Range range = sheet.UsedRange;

            for (int i = 1; i <= range.Rows.Count; i++)
            {
                contacts.Add(new ContactData()
                {
                    FirstName = range.Cells[i, 1].Value.ToString(),
                    LastName = range.Cells[i, 2].Value.ToString(),
                    MiddleName = range.Cells[i, 3].Value.ToString(),
                    NickName = range.Cells[i, 4].Value.ToString(),

                    Company = range.Cells[i, 5].Value.ToString(),
                    Title = range.Cells[i, 6].Value.ToString(),
                    Address = range.Cells[i, 7].Value.ToString(),

                    HomePhone = range.Cells[i, 8].Value.ToString(),
                    WorkPhone = range.Cells[i, 9].Value.ToString(),
                    MobilePhone = range.Cells[i, 10].Value.ToString(),
                    Fax = range.Cells[i, 11].Value.ToString(),

                    Email = range.Cells[i, 12].Value.ToString(),
                    Email2 = range.Cells[i, 13].Value.ToString(),
                    Email3 = range.Cells[i, 14].Value.ToString(),
                    HomePage = range.Cells[i, 15].Value.ToString(),
                    BirthDay = range.Cells[i, 16].Value.ToString(),
                    BirthMonth = range.Cells[i, 17].Value.ToString(),
                    BirthYear = range.Cells[i, 18].Value.ToString(),

                    AnniDay = range.Cells[i, 19].Value.ToString(),
                    AnniMonth = range.Cells[i, 20].Value.ToString(),
                    AnniYear = range.Cells[i, 21].Value.ToString(),

                    SecondaryAddress = range.Cells[i, 22].Value.ToString(),
                    SecondaryHomePhone = range.Cells[i, 23].Value.ToString(),
                    Notes = range.Cells[i, 24].Value.ToString(),
                });
            }

            wb.Close();
            app.Visible = false;
            app.Quit();

            return contacts;
        }

        [Test, TestCaseSource("ContactDataFromCsvFile")]
        public void ContactCreationTest(ContactData contact)
        {
            List<ContactData> oldContacts = ContactData.GetAll();

            app.Contacts.Create(contact);

            Assert.AreEqual(oldContacts.Count + 1, app.Contacts.GetContactCount());

            List<ContactData> newContacts = ContactData.GetAll();
            oldContacts.Add(contact);
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);
        }
    }
}
