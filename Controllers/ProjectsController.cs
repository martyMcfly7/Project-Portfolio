using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Projects.Models;
using Project.Repositories;
using System.IO;
using System;
using System.Linq;
using Microsoft.AspNetCore.Razor.Language;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.CodeAnalysis;

namespace Projects.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly IProjectRepository _repository;
        private readonly IWebHostEnvironment _environment;

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
                return RedirectToAction("Index");

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
                IFormFile imageFile = Request.Form.Files.First();

                if (imageFile != null)
                    project = UploadImage(project, imageFile);

                _repository.CreateProject(project);

                return RedirectToAction("Index");
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
                return RedirectToAction("Edit");

            return View(projectToUpdate);
        }

        [HttpPost]
        public IActionResult EditProject(ProjectModel project)
        {
            if (ModelState.IsValid)
            {
                IFormFile imageFile = Request.Form.Files.First();

                if (imageFile != null)
                    project = UploadImage(project, imageFile);

                _repository.EditProject(project);

                return RedirectToAction("Index");
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

            return RedirectToAction("Index");
        }

        public ProjectModel UploadImage(ProjectModel project, IFormFile imageFile)
        {
            var imagesFolder = Path.Combine(_environment.WebRootPath, "images");
            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);

            using (FileStream stream = new FileStream(Path.Combine(imagesFolder, fileName), FileMode.Create))
            {
                imageFile.CopyTo(stream);
                project.ImageFileName = fileName;
            }

            return project;
        }

        public IActionResult GetImage(string imageFileName)
        {
            string fileName = "";
            string mimeType = "";

            if (imageFileName != null)
            {
                if (imageFileName.EndsWith(".jpg"))
                {
                    fileName = imageFileName;
                    mimeType = "image/jpeg";
                }

                if (imageFileName.EndsWith(".png"))
                {
                    fileName = imageFileName;
                    mimeType = "image/png";
                }

                if (imageFileName.EndsWith(".gif"))
                {
                    fileName = imageFileName;
                    mimeType = "image/gif";
                }
            }
            else
            {
                fileName = "Placeholder.jpg";
                mimeType = "image/jpeg";
            }

            return File($@"images\" + fileName, mimeType);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
