using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    public class TestBase
    {
        protected ApplicationManager app;

        [SetUp]
        public void SetupApplicationManager()
        {
            app = ApplicationManager.GetInstance();
        }

        public static Random rnd = new Random();
        private static string[] suffix = new string[] { ".com", ".de", ".net", ".uk", ".org", ".cn", ".info", ".nl", ".ru", ".eu" };
        private static List<char> characters = new List<char>() {'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k',
                                                      'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v',
                                                      'w', 'x', 'y', 'z', 'A', 'B', 'C', 'D', 'E', 'F', 'G',
                                                      'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R',
                                                      'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', '-', '_'};

        public static string GenerateRandomString(int max)
        {
            int l = Convert.ToInt32(rnd.NextDouble() * max);
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < l; i++)
            {
                builder.Append(Convert.ToChar(32 + Convert.ToInt32(rnd.NextDouble() * 223)));
            }

            return builder.ToString();
        }

        public static string GenerateRandomDay()
        {
            return rnd.Next(1, 32).ToString();
        }

        public static string GenerateRandomYear()
        {
            return rnd.Next(1000, 10000).ToString();
        }

        public static string GenerateRandomMonth()
        {
            string[] months = new string[] { "January", "February", "March", "April", "May", "June", "July",
                                             "August", "September", "October", "November", "December"};
            return months[rnd.Next(0, 12)];
        }

        public static string GenerateRandomHomePage(int max)
        {
            int l = Convert.ToInt32(rnd.NextDouble() * max);
            StringBuilder builder = new StringBuilder();

            builder.Append("http://");
            builder = AddRandomCharacterOrNumber(builder, l);

            return builder.Append(suffix[rnd.Next(0, suffix.Length)]).ToString();
        }

        public static string GetRandomPhone()
        {
            StringBuilder builder = new StringBuilder(12);

            for (int i = 0; i < 3; i++)
            {
                builder.Append(rnd.Next(0, 10));
            }

            builder.Append(" ");
            builder.Append(string.Format("{0:D3}",rnd.Next(0, 1000)));
            builder.Append("-");
            builder.Append(string.Format("{0:D4}", rnd.Next(0, 1000)));

            return builder.ToString();
        }

        public static string GenerateRandomEmail(int max)
        {
            int l = Convert.ToInt32(rnd.NextDouble() * max);
            StringBuilder builder = new StringBuilder();

            builder = AddRandomCharacterOrNumber(builder, l);
            builder.Append("@");
            builder = AddRandomCharacterOrNumber(builder, l);

            return builder.Append(suffix[rnd.Next(0, suffix.Length)]).ToString();
        }

        private static StringBuilder AddRandomCharacterOrNumber(StringBuilder builder, int l)
        {
            for (int i = 0; i < l; i++)
            {
                int random = rnd.Next(1, 3);
                if (random == 1)
                {
                    builder.Append(rnd.Next(10));
                }
                else if (random == 2)
                {
                    builder.Append(characters[rnd.Next(0, characters.Count)]);
                }
            }

            return builder;
        }
    }
}
