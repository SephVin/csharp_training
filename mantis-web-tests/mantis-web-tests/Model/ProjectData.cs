using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mantis_web_tests
{
    public class ProjectData: IEquatable<ProjectData>, IComparable<ProjectData>
    {
        public ProjectData(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string Enabled { get; set; }
        public string ViewState { get; set; }

        public bool Equals(ProjectData other)
        {
            if (object.ReferenceEquals(other, null))
                return false;
            if (object.ReferenceEquals(this, other))
                return true;

            return Name == other.Name && Description == other.Description && Status == other.Status 
                                      && Enabled == other.Enabled && ViewState == other.ViewState;
        }

        public int CompareTo(ProjectData other)
        {
            if (object.ReferenceEquals(other, null))
                return 1;

            if (Name != null && Name.CompareTo(other.Name) != 0)
                return Name.CompareTo(other.Name);
            else if (Description != null && Description.CompareTo(other.Description) != 0)
                return Description.CompareTo(other.Description);
            else if (Status.CompareTo(other.Status) != 0)
                return Status.CompareTo(other.Status);
            else if (Enabled.CompareTo(other.Enabled) != 0)
                return Enabled.CompareTo(other.Enabled);
            else if (ViewState.CompareTo(other.ViewState) != 0)
                return ViewState.CompareTo(other.ViewState);

            return 0;
        }
    }
}
