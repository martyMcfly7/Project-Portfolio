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
        public string Language { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "About the Project: ")]
        public string About { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Project Description: ")]
        public string Description { get; set; }
        
        [DataType(DataType.Date)]
        [Display(Name = "Created Date: ")]
        public DateTime? CreatedDate { get; set; }

        [Display(Name = "Image Name: ")]
        public string ImageName { get; set; }
    }
}
