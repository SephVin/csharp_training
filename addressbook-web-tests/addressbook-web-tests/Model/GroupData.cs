using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAddressbookTests
{
    public class GroupData : IEquatable<GroupData>, IComparable<GroupData>
    {
        public GroupData()
        {
        }

        public GroupData(string name)
        {
            Name = name;
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public string Header { get; set; }

        public string Footer { get; set; }

        public bool Equals(GroupData other)
        {
            if (object.ReferenceEquals(other, null))
            {
                return false;
            }
            if (object.ReferenceEquals(this, other))
            {
                return true;
            }

            return Name == other.Name;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("Name: {0}\nHeader: {1}\nFooter: {2}", Name, Header, Footer);
        }

        public int CompareTo(GroupData other)
        {
            if (object.ReferenceEquals(other, null))
            {
                return 1;
            }

            return Name.CompareTo(other.Name);
        }
    }
}
