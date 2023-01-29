using System.Linq;
using System.Collections.Generic;
using Project.Data;
using Projects.Models;

namespace Project.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private ProjectContext _context;

        public ProjectRepository(ProjectContext context)
        {
            _context = context;
        }

        public IEnumerable<ProjectModel> GetProjects()
        {
            return _context.Projects.ToList();
        }

        public ProjectModel GetProjectById(int id)
        {
            return _context.Projects.SingleOrDefault(p => p.Id == id);
        }

        public void CreateProject(ProjectModel project)
        {
            _context.Projects.Add(project);
            _context.SaveChanges();
        }

        public void EditProject(ProjectModel project)
        {
            _context.Projects.Update(project);
            _context.SaveChanges();
        }

        public void DeleteProject(int id)
        {
            ProjectModel project = _context.Projects.SingleOrDefault(p => p.Id == id);
            _context.Projects.Remove(project);
            _context.SaveChanges();
        }
    }
}
