using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projects.Models
{
    public class ProjectModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [Display(Name = "Project Title: ")]
        [Required(ErrorMessage = "You must have a project title!")]
        public string ProjectTitle { get; set; }
        
        [Display(Name = "Public Project: ")]
        public bool PublicProject { get; set; }
        
        [Display(Name = "Languages Used: ")]
        [Required(ErrorMessage = "You must list a language!")]
        public string Language { get; set; }
        
        [Display(Name = "About the Project: ")]
        [Required(ErrorMessage = "You must have an about!")]
        public string About { get; set; }
        
        [Display(Name = "Project Description: ")]
        [Required(ErrorMessage = "You must have a description!")]
        public string Description { get; set; }
        
        [DataType(DataType.Date)]
        [Display(Name = "Created Date: ")]
        [Required(ErrorMessage = "You must have a created date!")]
        public DateTime? CreatedDate { get; set; }

        [Display(Name = "Image Name: ")]
        [Required(ErrorMessage = "You must have an image filename!")]
        public string ImageName { get; set; }
    }
}
