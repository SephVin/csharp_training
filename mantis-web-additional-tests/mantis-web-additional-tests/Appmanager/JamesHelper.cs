using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MinimalisticTelnet;

namespace mantis_web_additional_tests
{
    public class JamesHelper : HelperBase
    {
        public JamesHelper(ApplicationManager manager) : base(manager)
        {
        }

        public void Add(AccountData account)
        {
            if (Verify(account))
            {
                return;
            }

            TelnetConnection telnet = LoginToJames();
            telnet.WriteLine("adduser " + account.Username + " " + account.Password);
            Console.Out.WriteLine(telnet.Read());
        }

        public void Delete(AccountData account)
        {
            if (!Verify(account))
            {
                return;
            }

            TelnetConnection telnet = LoginToJames();
            telnet.WriteLine("deluser " + account.Username);
            Console.Out.WriteLine(telnet.Read());
        }

        public bool Verify(AccountData account)
        {
            TelnetConnection telnet = LoginToJames();
            telnet.WriteLine("verify " + account.Username);
            string s = telnet.Read();
            Console.Out.WriteLine(s);

            return !s.Contains("does not exist");
        }

        private TelnetConnection LoginToJames()
        {
            TelnetConnection telnet = new TelnetConnection("localhost", 4555);
            Console.Out.WriteLine(telnet.Read());
            telnet.WriteLine("root");
            Console.Out.WriteLine(telnet.Read());
            telnet.WriteLine("root");
            Console.Out.WriteLine(telnet.Read());
            return telnet;
        }
    }
}
