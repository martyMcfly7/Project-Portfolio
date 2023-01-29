using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Projects.Models;

namespace Project.Repositories
{
    public interface IProjectRepository
    {
        IEnumerable<ProjectModel> GetProjects();
        ProjectModel GetProjectById(int id);
        void CreateProject(ProjectModel project);
        void EditProject(ProjectModel project);
        void DeleteProject(int id);
    }
}
