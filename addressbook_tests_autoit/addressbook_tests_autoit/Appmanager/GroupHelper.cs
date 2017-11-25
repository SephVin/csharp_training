using System;
using System.Collections.Generic;

namespace addressbook_tests_autoit
{
    public class GroupHelper : HelperBase
    {
        public static string GROUPWINTITLE = "Group editor";
        public static string DELETEGROUPWINTITLE = "Delete group";

        public GroupHelper(ApplicationManager manager) : base(manager) { }


        public void Add(GroupData group)
        {
            OpenGroupsDialog();

            InitGroupCreation();
            FillGroupNameAndSubmitGroupCreation(group);

            CloseGroupsDialog();
        }

        public void Remove(int index)
        {
            OpenGroupsDialog();

            SelectGroup(index);
            InitGroupRemoval();
            RemoveGroup();

            CloseGroupsDialog();
        }

        public void InitGroupCreation()
        {
            aux.ControlClick(GROUPWINTITLE, "", "WindowsForms10.BUTTON.app.0.2c908d53");
        }

        public void FillGroupNameAndSubmitGroupCreation(GroupData group)
        {
            aux.Send(group.Name);
            aux.Send("{ENTER}");
        }

        public void SelectGroup(int index)
        {
            aux.ControlTreeView(GROUPWINTITLE, "", "WindowsForms10.SysTreeView32.app.0.2c908d51",
                                "Select", "Contact groups|#" + index, "");
        }

        public void InitGroupRemoval()
        {
            aux.ControlClick(GROUPWINTITLE, "", "WindowsForms10.BUTTON.app.0.2c908d51");
            aux.WinWait(DELETEGROUPWINTITLE);
        }

        public void RemoveGroup()
        {
            aux.ControlClick(DELETEGROUPWINTITLE, "", "WindowsForms10.BUTTON.app.0.2c908d51");
            aux.ControlClick(DELETEGROUPWINTITLE, "", "WindowsForms10.BUTTON.app.0.2c908d53");
        }

        private void OpenGroupsDialog()
        {
            aux.ControlClick(WINTITLE, "", "WindowsForms10.BUTTON.app.0.2c908d512");
            aux.WinWait(GROUPWINTITLE);
        }

        private void CloseGroupsDialog()
        {
            aux.ControlClick(GROUPWINTITLE, "", "WindowsForms10.BUTTON.app.0.2c908d54");
        }

        public List<GroupData> GetGroupList()
        {
            List<GroupData> list = new List<GroupData>();

            OpenGroupsDialog();
            int count = GetRootGroupsCount();

            for (int i = 0; i < count; i++)
            {
                string item = aux.ControlTreeView(GROUPWINTITLE, "", "WindowsForms10.SysTreeView32.app.0.2c908d51",
                                                  "GetText", "#0|#" + i, "");
                list.Add(new GroupData()
                {
                    Name = item
                });
            }

            CloseGroupsDialog();
            return list;
        }

        public int GetRootGroupsCount()
        {
            return int.Parse(aux.ControlTreeView(GROUPWINTITLE, "", "WindowsForms10.SysTreeView32.app.0.2c908d51",
                                               "GetItemCount", "#0", ""));
        }

        public void AddIfNotExist()
        {
            OpenGroupsDialog();

            string count = aux.ControlTreeView(GROUPWINTITLE, "", "WindowsForms10.SysTreeView32.app.0.2c908d51",
                                               "GetItemCount", "#0", "");
            if (int.Parse(count) == 1)
            {
                Add(new GroupData()
                {
                    Name = "AddIfNotExist"
                });
            }

            CloseGroupsDialog();
        }
    }
}