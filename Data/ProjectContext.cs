using Projects.Models;
using Microsoft.EntityFrameworkCore;

namespace Project.Data
{
    public class ProjectContext : DbContext
    {
        public ProjectContext(DbContextOptions<ProjectContext> options) : base(options)
        {

        }

        // tables in Projects database
        public DbSet<ProjectModel> Projects { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // seed data
            modelBuilder.Entity<ProjectModel>().HasData(
                new ProjectModel
                {
                    Id = 1,
                    Title = "Escape Room Game",
                    IsPublic = true,
                    Language = "Java",
                    About = "Text adventure game using OOP principles with save and load functionality.",
                    Description =
                        "Using the console, the player collects items you find along " +
                        "your journey as a way to figure out a way to freedom. " +
                        "Use the following commands to navigate within the game: " +
                        "use 'north', 'east', 'south', or 'west' to move around, " +
                        "'info' to display current room description " +
                        "'inventory' to view current items in inventory, " +
                        "'save' to save current game progress, " +
                        "'restart' to reset to the beginning of the game, " +
                        "or 'quit' to exit game & optionally save game progress.",
                    CreatedDate = new System.DateTime(2019, 11, 14),
                    ImageFileName = "Escape.jpg"
                },
                new ProjectModel
                {
                    Id = 2,
                    Title = "Project Tracking Application",
                    IsPublic = true,
                    Language = "C#",
                    About = "An application to track projects, tasks and the time employees spend working on each task.",
                    Description =
                        "Using a Multiple Doucument Interface (MDI) application, different forms can be displayed at once. " +
                        "There are four forms that the application can displayed. " +
                        "The Employees form allows you to add or update employee information. " +
                        "The Projects form allows you to add or update project information including tasks and employees working on the project. " +
                        "The Tasks form allows you to add or update task information. " +
                        "The Work form allows you to add or update the work an employee completed. ",
                    CreatedDate = new System.DateTime(2019, 6, 1),
                    ImageFileName = "Tracking.jpg"
                },
                new ProjectModel
                {
                    Id = 3,
                    Title = "Fight or Flight! Game",
                    IsPublic = true,
                    Language = "JavaScript",
                    About = "An adventure game where you fight monsters.",
                    Description =
                        "Using a random function, a player will collect items or fight a monster. " +
                        "The player can loot a dead adventurer for more items. " +
                        "Health potions can be used to restore health. " +
                        "Offering your items to the shrine can provide you with a powerful weapon. " +
                        "With a proper strategy the player can defeat all enemies.",
                    CreatedDate = new System.DateTime(2019, 6, 4),
                    ImageFileName = "Fight.jpg"
                }
            );
        }
    }
}
