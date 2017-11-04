using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WebAddressbookTests
{
    public class ContactData : IEquatable<ContactData>, IComparable<ContactData>
    {
        private string allPhones;
        private string allEmails;
        private List<string> contactDetails;

        public ContactData(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public string Id { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string NickName { get; set; }

        public string Title { get; set;}

        public string Company { get; set; }

        public string Address { get; set; }

        public string HomePhone { get; set; }

        public string MobilePhone { get; set; }

        public string WorkPhone { get; set; }

        public string Fax { get; set; }

        public string Email { get; set; }

        public string Email2 { get; set; }

        public string Email3 { get; set; }

        public string HomePage { get; set; }

        public string BirthDay { get; set; }

        public string BirthMonth { get; set; }

        public string BirthYear { get; set; }

        public string AnniDay { get; set; }

        public string AnniMonth { get; set; }

        public string AnniYear { get; set; }

        public string SecondaryAddress { get; set; }

        public string SecondaryHomePhone { get; set; }

        public string Notes { get; set; }

        public string AllEmails
        {
            get
            {
                if (allEmails != null)
                {
                    return allEmails;
                }
                else
                {
                    return (CleanUpEmail(Email) + CleanUpEmail(Email2) + CleanUpEmail(Email3)).Trim();
                }
            }
            set
            {
                allEmails = value;
            }
        }

        public string AllPhones
        {
            get
            {
                if (allPhones != null)
                {
                    return allPhones;
                }
                else
                {
                    return (CleanUpPhone(HomePhone) + CleanUpPhone(MobilePhone) + CleanUpPhone(WorkPhone) + CleanUpPhone(SecondaryHomePhone)).Trim();
                }
            }
            set
            {
                allPhones = value;
            }
        }

        public List<string> ContactDetails
        {
            get
            {
                if (contactDetails != null)
                {
                    return contactDetails;
                }
                else
                {
                    List<string> result = new List<string>();
                    StringBuilder str = new StringBuilder();

                    AddContactDetailsToOneString(str, FirstName);
                    AddContactDetailsToOneString(str, MiddleName);
                    AddContactDetailsToOneString(str, LastName);
                    CleanUpFioStringAndAddToResult(str, result);

                    AddContactDetailsToResult(result, NickName);                      

                    AddContactDetailsToResult(result, Title);
                    AddContactDetailsToResult(result, Company);
                    AddContactDetailsToResult(result, Address);

                    AddContactDetailsToResult(result, HomePhone);
                    AddContactDetailsToResult(result, MobilePhone);
                    AddContactDetailsToResult(result, WorkPhone);
                    AddContactDetailsToResult(result, Fax);

                    AddContactDetailsToResult(result, Email);
                    AddContactDetailsToResult(result, Email2);
                    AddContactDetailsToResult(result, Email3);
                    AddContactDetailsToResult(result, HomePage);

                    AddContactDetailsToOneString(str, BirthDay);
                    AddContactDetailsToOneString(str, BirthMonth);
                    AddContactDetailsToOneString(str, BirthYear);
                    CleanUpBirthDateAndAddToResult(str, result);

                    AddContactDetailsToOneString(str, AnniDay);
                    AddContactDetailsToOneString(str, AnniMonth);
                    AddContactDetailsToOneString(str, AnniYear);
                    CleanUpAnniDateAndAddToResult(str, result);

                    AddContactDetailsToResult(result, SecondaryAddress);
                    AddContactDetailsToResult(result, SecondaryHomePhone);
                    AddContactDetailsToResult(result, Notes);

                    if (result.Count == 0)
                    {
                        result.Add("");
                    }

                    return result;
                }
            }
            set
            {
                contactDetails = value;
            }
        }

        private void AddContactDetailsToOneString(StringBuilder str, string property)
        {
            if (property == FirstName || property == LastName || property == MiddleName)
            {
                if (!string.IsNullOrEmpty(property))
                {
                    str.Append(property + " ");
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(property) && (!property.Equals("0") && !property.Equals("-")))
                {
                    if (property == BirthDay || property == AnniDay)
                        str.Append(property + ". ");
                    if (property == BirthMonth || property == AnniMonth)
                        str.Append(property + " ");
                    if (property == BirthYear || property == AnniYear)
                    {
                        str.Append(property + " ");
                        str.Append(string.Format("({0})", (DateTime.Now.Year - int.Parse(property))));
                    }
                }
            }
        }

        private void AddContactDetailsToResult(List<string> result, string property)
        {
            if (!string.IsNullOrEmpty(property))
            {
                if (property == HomePhone)
                    result.Add("H: " + property);
                else if (property == MobilePhone)
                    result.Add("M: " + property);
                else if (property == WorkPhone)
                    result.Add("W: " + property);
                else if (property == Fax)
                    result.Add("F: " + property);
                else if (property == SecondaryHomePhone)
                    result.Add("P: " + property);
                else if (property == Email || property == Email2 || property == Email3)
                    result.Add(property.Trim());
                else if (property == HomePage)
                {
                    result.Add("Homepage:");
                    result.Add(property.Replace("http://", ""));
                }
                else
                    result.Add(property);
            }
        }

        private string CleanUpEmail(string email)
        {
            if (email == null || email == "")
            {
                return "";
            }

            return email.Trim() + "\r\n";
        }

        private string CleanUpPhone(string phone)
        {
            if (phone == null || phone == "")
            {
                return "";
            }

            return Regex.Replace(phone, "[() -]", "") + "\r\n";
        }

        private void CleanUpFioStringAndAddToResult(StringBuilder str, List<string> result)
        {
            if (str.Length > 1)
            {
                result.Add(str.ToString().Trim());
            }
            str.Clear();
        }

        private void CleanUpBirthDateAndAddToResult(StringBuilder str, List<string> result)
        {
            if (str.Length > 1)
            {
                result.Add("Birthday " + str.ToString().Trim());
            }
            str.Clear();
        }

        private void CleanUpAnniDateAndAddToResult(StringBuilder str, List<string> result)
        {
            if (str.Length > 1)
            {
                result.Add("Anniversary " + str.ToString().Trim());
            }
        }

        public bool Equals(ContactData other)
        {
            if (object.ReferenceEquals(other, null))
            {
                return false;
            }
            if (object.ReferenceEquals(this, other))
            {
                return true;
            }

            return LastName == other.LastName && FirstName == other.FirstName;
        }

        public override int GetHashCode()
        {
            return (LastName + FirstName).GetHashCode();
        }

        public int CompareTo(ContactData other)
        {
            if (object.ReferenceEquals(other, null))
            {
                return 1;
            }

            if (LastName.CompareTo(other.LastName) != 0)
            {
                return LastName.CompareTo(other.LastName);
            }
            else if (FirstName.CompareTo(other.FirstName) != 0)
            {
                return FirstName.CompareTo(other.FirstName);
            }

            return 0;
        }

        public override string ToString()
        {
            return string.Format("LastName: {0}, FirstName: {1}", LastName, FirstName);
        }
    }
}
