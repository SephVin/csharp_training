using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAddressbookTests;
using Newtonsoft.Json;
using Excel = Microsoft.Office.Interop.Excel;

namespace addressbook_test_data_generators
{
    class Program
    {
        static void Main(string[] args)
        {
            string type = args[0];
            int count = Convert.ToInt32(args[1]);
            string filename = args[2];
            string format = args[3];

            List<ContactData> contacts = new List<ContactData>();
            List<GroupData> groups = new List<GroupData>();

            if (type == "contacts")
            {
                for (int i = 0; i < count; i++)
                {
                    contacts.Add(new ContactData(TestBase.GenerateRandomString(15), TestBase.GenerateRandomString(15))
                    {
                        MiddleName = TestBase.GenerateRandomString(15),
                        NickName = TestBase.GenerateRandomString(15),

                        Company = TestBase.GenerateRandomString(30),
                        Title = TestBase.GenerateRandomString(10),
                        Address = TestBase.GenerateRandomString(100),

                        HomePhone = TestBase.GetRandomPhone(),
                        WorkPhone = TestBase.GetRandomPhone(),
                        MobilePhone = TestBase.GetRandomPhone(),
                        Fax = TestBase.GetRandomPhone(),

                        Email = TestBase.GenerateRandomEmail(15),
                        Email2 = TestBase.GenerateRandomEmail(15),
                        Email3 = TestBase.GenerateRandomEmail(15),
                        HomePage = TestBase.GenerateRandomHomePage(10),
                        BirthDay = TestBase.GenerateRandomDay(),
                        BirthMonth = TestBase.GenerateRandomMonth(),
                        BirthYear = TestBase.GenerateRandomYear(),

                        AnniDay = TestBase.GenerateRandomDay(),
                        AnniMonth = TestBase.GenerateRandomMonth(),
                        AnniYear = TestBase.GenerateRandomYear(),

                        SecondaryAddress = TestBase.GenerateRandomString(100),
                        SecondaryHomePhone = TestBase.GetRandomPhone(),
                        Notes = TestBase.GenerateRandomString(100)
                    });
                }
            }

            else if (type == "groups")
            {
                for (int i = 0; i < count; i++)
                {
                    groups.Add(new GroupData(TestBase.GenerateRandomString(10))
                    {
                        Header = TestBase.GenerateRandomString(100),
                        Footer = TestBase.GenerateRandomString(100)
                    });
                }
            }

            else
                Console.Out.Write("Unrecognized type " + type);

            if (format == "excel")
            {
                if (type == "contacts")
                    WriteContactsToExcelFile(contacts, filename);
                else if (type == "groups")
                    WriteGroupsToExcelFile(groups, filename);
            }
                
            else
            {
                StreamWriter writer = new StreamWriter(filename);

                if (format == "csv")
                {
                    if (type == "contacts")
                        WriteContactsToCsvFile(contacts, writer);
                    else if (type == "groups")
                        WriteGroupsToCsvFile(groups, writer);
                }
                else if (format == "xml")
                {
                    if (type == "contacts")
                        WriteContactsToXmlFile(contacts, writer);
                    else if (type == "groups")
                        WriteGroupsToXmlFile(groups, writer);
                }
                else if (format == "json")
                {
                    if (type == "contacts")
                        WriteContactsToJsonFile(contacts, writer);
                    else if (type == "groups")
                        WriteGroupsToJsonFile(groups, writer);
                }
                else
                    Console.Out.Write("Unrecongnized format " + format);

                writer.Close();
            }    
        }

        static void WriteContactsToCsvFile(List<ContactData> contacts, StreamWriter writer)
        {
            foreach (ContactData contact in contacts)
            {
                writer.WriteLine("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12}," +
                                 "{13},{14},{15},{16},{17},{18},{19},{20},{21},{22},{23}",
                                 contact.FirstName, contact.LastName, contact.MiddleName, contact.NickName,                                 
                                 contact.Company, contact.Title, contact.Address,
                                 contact.HomePhone, contact.WorkPhone, contact.MobilePhone, contact.Fax,
                                 contact.Email, contact.Email2, contact.Email3, contact.HomePage,
                                 contact.BirthDay, contact.BirthMonth, contact.BirthYear,
                                 contact.AnniDay, contact.AnniMonth, contact.AnniYear,
                                 contact.SecondaryAddress, contact.SecondaryHomePhone, contact.Notes);
            }
        }

        static void WriteGroupsToCsvFile(List<GroupData> groups, StreamWriter writer)
        {
            foreach (GroupData group in groups)
            {
                writer.WriteLine(string.Format("{0},{1},{2}",
                    group.Name, group.Header, group.Footer));
            }
        }

        static void WriteContactsToXmlFile(List<ContactData> contacts, StreamWriter writer)
        {
            new XmlSerializer(typeof(List<ContactData>)).Serialize(writer, contacts);
        }

        static void WriteGroupsToXmlFile(List<GroupData> groups, StreamWriter writer)
        {
            new XmlSerializer(typeof(List<GroupData>)).Serialize(writer, groups);
        }

        static void WriteContactsToJsonFile(List<ContactData> contacts, StreamWriter writer)
        {
            writer.Write(JsonConvert.SerializeObject(contacts, Newtonsoft.Json.Formatting.Indented));
        }

        static void WriteGroupsToJsonFile(List<GroupData> groups, StreamWriter writer)
        {
            writer.Write(JsonConvert.SerializeObject(groups, Newtonsoft.Json.Formatting.Indented));
        }

        static void WriteContactsToExcelFile(List<ContactData> contacts, string filename)
        {
            Excel.Application app = new Excel.Application();
            app.Visible = true;
            Excel.Workbook wb = app.Workbooks.Add();
            Excel.Worksheet sheet = app.ActiveSheet;

            int row = 1;
            {
                foreach (ContactData  contact in contacts)
                {
                    sheet.Cells[row, 1] = contact.FirstName;
                    sheet.Cells[row, 2] = contact.LastName;
                    sheet.Cells[row, 3] = contact.MiddleName;
                    sheet.Cells[row, 4] = contact.NickName;

                    sheet.Cells[row, 5] = contact.Company;
                    sheet.Cells[row, 6] = contact.Title;
                    sheet.Cells[row, 7] = contact.Address;

                    sheet.Cells[row, 8] = contact.HomePhone;
                    sheet.Cells[row, 9] = contact.WorkPhone;
                    sheet.Cells[row, 10] = contact.MobilePhone;
                    sheet.Cells[row, 11] = contact.Fax;

                    sheet.Cells[row, 12] = contact.Email;
                    sheet.Cells[row, 13] = contact.Email2;
                    sheet.Cells[row, 14] = contact.Email3;
                    sheet.Cells[row, 15] = contact.HomePage;
                    sheet.Cells[row, 16] = contact.BirthDay;
                    sheet.Cells[row, 17] = contact.BirthMonth;
                    sheet.Cells[row, 18] = contact.BirthYear;

                    sheet.Cells[row, 19] = contact.AnniDay;
                    sheet.Cells[row, 20] = contact.AnniMonth;
                    sheet.Cells[row, 21] = contact.AnniYear;

                    sheet.Cells[row, 22] = contact.SecondaryAddress;
                    sheet.Cells[row, 23] = contact.SecondaryHomePhone;
                    sheet.Cells[row, 24] = contact.Notes;

                    row++;
                }
            }

            string fullPath = Path.Combine(Directory.GetCurrentDirectory(), filename);
            File.Delete(fullPath);
            wb.SaveAs(fullPath);

            wb.Close();
            app.Visible = false;
            app.Quit();
        }

        static void WriteGroupsToExcelFile(List<GroupData> groups, string filename)
        {
            Excel.Application app = new Excel.Application();
            app.Visible = true;
            Excel.Workbook wb = app.Workbooks.Add();
            Excel.Worksheet sheet = wb.ActiveSheet;

            int row = 1;
            foreach (GroupData group in groups)
            {
                sheet.Cells[row, 1] = group.Name;
                sheet.Cells[row, 2] = group.Header;
                sheet.Cells[row, 3] = group.Footer;

                row++;
            }

            string fullPath = Path.Combine(Directory.GetCurrentDirectory(), filename);
            File.Delete(fullPath);
            wb.SaveAs(fullPath);

            wb.Close();
            app.Visible = false;
            app.Quit();
        }
    }
}
