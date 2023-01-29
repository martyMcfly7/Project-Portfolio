using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Projects.Models;
using Project.Repositories;
using Microsoft.CodeAnalysis;

namespace Projects.Controllers
{
    public class ProjectsController : Controller
    {
        private IProjectRepository _repository;
        private IWebHostEnvironment _environment;

        public ProjectsController(IProjectRepository repository, IWebHostEnvironment environment)
        {
            _repository = repository;
            _environment = environment;
        }

        public IActionResult Index()
        {
            return View(_repository.GetProjects());
        }

        public IActionResult Project(int projectNum)
        {
            ProjectModel project = _repository.GetProjectById(projectNum);
            if (project == null)
                return RedirectToAction("Index", "Projects");
            return View(project);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(ProjectModel project)
        {
            if (ModelState.IsValid)
            {
                _repository.CreateProject(project);
                return RedirectToAction(nameof(Index));
            }
            return View(project);
        }

        [HttpGet]
        public IActionResult Edit()
        {
            return View(_repository.GetProjects());
        }

        [HttpGet]
        public IActionResult EditProject(int projectNum)
        {
            ProjectModel projectToUpdate = _repository.GetProjectById(projectNum);
            if (projectToUpdate == null)
                return RedirectToAction("Edit", "Projects");
            return View(projectToUpdate);
        }

        [HttpPost]
        public IActionResult EditProject(ProjectModel project)
        {
            if (ModelState.IsValid)
            {
                _repository.EditProject(project);
                return RedirectToAction(nameof(Index));
            }
            return View(project);
        }

        [HttpGet]
        public IActionResult Remove()
        {
            return View(_repository.GetProjects());
        }

        [HttpGet]
        public IActionResult RemoveProject(int projectNum)
        {
            _repository.DeleteProject(projectNum);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult GetImage(string fileName)
        {
            if (!System.IO.File.Exists($@"wwwroot\images\{fileName}.jpg"))
                return File($@"images\Placeholder.jpg", "image/jpeg");
            return File($@"images\{fileName}.jpg", "image/jpeg");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
