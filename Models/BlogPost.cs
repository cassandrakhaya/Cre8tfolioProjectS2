using System.ComponentModel.DataAnnotations;

namespace PersonalProjectCre8tfolio.Models
{
    public class BlogPost
    {
        public int Id { get; set; }
        [Required]
        public required string Title { get; set; }
        [Required]
        public required string Description { get; set; }
        public string? ImagePath { get; set; }

        //public int DateCreated { get; set; }
        //public int DateUpdated { get; set; }
    }
}
