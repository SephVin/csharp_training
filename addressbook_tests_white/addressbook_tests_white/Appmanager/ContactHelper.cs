using System.Collections.Generic;
using System.Windows.Automation;
using TestStack.White.UIItems;
using TestStack.White.UIItems.Finders;
using TestStack.White.UIItems.WindowItems;
using TestStack.White.UIItems.TableItems;

namespace addressbook_tests_white
{
    public class ContactHelper : HelperBase
    {
        private static string CONTACT_EDITOR_WINTITLE = "Contact Editor";

        public ContactHelper(ApplicationManager manager) : base(manager) { }
        
        public void Add(ContactData contact)
        {
            Window contactEditorDialogue =  InitContactCreation();
            FillContactForm(contactEditorDialogue, contact);
            SubmitContactCreation(contactEditorDialogue);
        }

        public void Remove(int index)
        {
            SelectContact(index);
            Window questionDialogue = InitContactRemoval();
            SubmitContactRemoval(questionDialogue);
        }

        public Window InitContactCreation()
        {
            manager.MainWindow.Get<Button>("uxNewAddressButton").Click();
            return manager.MainWindow.ModalWindow(CONTACT_EDITOR_WINTITLE);
        }

        public void FillContactForm(Window contactEditorDialogue, ContactData contact)
        {
            TextBox FirstName = contactEditorDialogue.Get<TextBox>("ueFirstNameAddressTextBox");
            FirstName.Enter(contact.FirstName);

            TextBox LastName = contactEditorDialogue.Get<TextBox>("ueLastNameAddressTextBox");
            LastName.Enter(contact.LastName);

            contactEditorDialogue.Get<CheckBox>("uxIsCompanyGasCheckBox").Click();

            TextBox CompanyName = contactEditorDialogue.Get<TextBox>("ueCompanyAddressTextBox");
            CompanyName.Enter(contact.CompanyName);

            TextBox City = contactEditorDialogue.Get<TextBox>("ueCityAddressTextBox");
            City.Enter(contact.City);

            TextBox Address = contactEditorDialogue.Get<TextBox>("ueAddressAddressTextBox");
            Address.Enter(contact.Address);
        }

        public void SubmitContactCreation(Window contactEditorDialogue)
        {
            contactEditorDialogue.Get<Button>("uxSaveAddressButton").Click();
        }

        public void SelectContact(int index)
        {
            Table contactTable = (Table)manager.MainWindow.Get(SearchCriteria.ByControlType(ControlType.Table));
            contactTable.Rows[index].Click();
        }

        public Window InitContactRemoval()
        {
            manager.MainWindow.Get<Button>("uxDeleteAddressButton").Click();
            return manager.MainWindow.ModalWindow(SearchCriteria.ByControlType(ControlType.Window));
        }

        public void SubmitContactRemoval(Window questionDialogue)
        {
            questionDialogue.Get<Button>(SearchCriteria.ByText("Yes")).Click();
        }

        public List<ContactData> GetContactsList()
        {
            List<ContactData> contactsList = new List<ContactData>();
            List<string> cellsList = new List<string>();

            Table contactsTable = (Table)manager.MainWindow.Get(SearchCriteria.ByControlType(ControlType.Table));

            foreach (var row in contactsTable.Rows)
            {
                for (int i = 0; i < 5; i++)
                {
                    if (row.Cells[i].Value.ToString() != "(не определено)")
                        cellsList.Add(row.Cells[i].Value.ToString());
                    else
                        cellsList.Add(null);
                }

                ContactData contact = new ContactData()
                {
                    FirstName = cellsList[0],
                    LastName = cellsList[1],
                    CompanyName = cellsList[2],
                    City = cellsList[3],
                    Address = cellsList[4]
                };

                contactsList.Add(contact);
                cellsList.Clear();
            }

            return contactsList;
        }

        public int GetContactsCount()
        {
            Table contactsTable = (Table)manager.MainWindow.Get(SearchCriteria.ByControlType(ControlType.Table));
            return contactsTable.Rows.Count;
        }

        public void CreateIfNotExist()
        {
            if (GetContactsCount() == 0)
                Add(new ContactData("Petr", "Petrov"));
        }
    }
}
