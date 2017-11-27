
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStack.White;
using TestStack.White.UIItems;
using TestStack.White.UIItems.WindowItems;

namespace addressbook_tests_white
{
    public class ApplicationManager
    {
        public static string WINTITLE = "Free Address Book";

        private ContactHelper contactHelper;

        public ApplicationManager()
        {
            Application app = Application.Launch(@"D:\Selenium\FreeAddressBookPortable\AddressBook.exe");
            MainWindow = app.GetWindow(WINTITLE);
            
            contactHelper = new ContactHelper(this);
        }

        public Window MainWindow { get; private set; }

        public ContactHelper Contacts
        {
            get
            {
                return contactHelper;
            }
        }

        public void Stop()
        {
            MainWindow.Get<Button>("uxExitAddressButton").Click();
        }
    }
}
