using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace addressbook_tests_white
{
    public class ContactData : IComparable<ContactData>, IEquatable<ContactData>
    {
        public ContactData()
        {
        }

        public ContactData(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CompanyName { get; set; }
        public string City { get; set; }
        public string Address { get; set; }

        public int CompareTo(ContactData other)
        {
            if (object.ReferenceEquals(other, null))
                return 1;

            if (FirstName != null && FirstName.CompareTo(other.FirstName) != 0)
                return FirstName.CompareTo(other.FirstName);
            else if (LastName != null && LastName.CompareTo(other.LastName) != 0)
                return LastName.CompareTo(other.LastName);
            else if (CompanyName != null && CompanyName.CompareTo(other.CompanyName) != 0)
                return CompanyName.CompareTo(other.CompanyName);
            else if (City != null && City.CompareTo(other.City) != 0)
                return City.CompareTo(other.City);
            else if (Address != null && Address.CompareTo(other.Address) != 0)
                return Address.CompareTo(other.Address);

            return 0;
        }

        public bool Equals(ContactData other)
        {
            if (object.ReferenceEquals(other, null))
                return false;
            if (object.ReferenceEquals(this, other))
                return true;

            return FirstName == other.FirstName && LastName == other.LastName && CompanyName == other.CompanyName
                   && City == other.City && Address == other.Address;
        }
    }
}
