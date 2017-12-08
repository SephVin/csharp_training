using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mantis_web_tests
{
    public class ApiHelper : HelperBase
    {
        public ApiHelper(ApplicationManager manager) : base(manager) { }

        Mantis.MantisConnectPortTypeClient client = new Mantis.MantisConnectPortTypeClient();

        public void CreateProject(AccountData account, ProjectData projectData)
        {
            Mantis.ProjectData project = new Mantis.ProjectData();
            project.name = projectData.Name;
            project.description = projectData.Description;
            project.status = new Mantis.ObjectRef();
            project.status.name = projectData.Status;
            project.enabled = projectData.Enabled == "True" ? true : false;
            project.view_state = new Mantis.ObjectRef();
            project.view_state.name = projectData.ViewState;

            client.mc_project_add(account.Username, account.Password, project);
        }

        public void RemoveProject(AccountData account, int index)
        {
            List<ProjectData> projectsList = GetProjectsList(account);

            var id = projectsList[0].Id;
            client.mc_project_delete(account.Username, account.Password, id);
        }

        public void DeleteProjectIfAlreadyExist(AccountData account, ProjectData projectData)
        {
            List<ProjectData> projectsList = GetProjectsList(account);

            if (projectsList.Count == 0)
                return;

            var indexOfExistProject = projectsList.FindIndex(x => x.Name == projectData.Name);

            if (indexOfExistProject != -1)
            {
                var id = projectsList.Find(x => x.Name == projectData.Name).Id;
                client.mc_project_delete(account.Username, account.Password, id);
            }
        }

        public void CreateIfNotExist(AccountData account)
        {
            if (GetProjectsList(account).Count == 0)
            {
                Mantis.ProjectData project = new Mantis.ProjectData();
                project.name = new ProjectData("test").Name;

                client.mc_project_add(account.Username, account.Password, project);
            }
        }

        public List<ProjectData> GetProjectsList(AccountData account)
        {
            List<ProjectData> projectsList = new List<ProjectData>();

            var projects = client.mc_projects_get_user_accessible(account.Username, account.Password);

            foreach (var project in projects)
            {
                projectsList.Add(new ProjectData()
                {
                    Id = project.id,
                    Name = project.name,
                    Description = project.description,
                    Status = project.status.name,
                    Enabled = project.enabled.ToString(),
                    ViewState = project.view_state.name
                });
            }

            return projectsList;
        }

        public int GetProjectsCount(AccountData account)
        {
            return GetProjectsList(account).Count;
        }
    }
}
