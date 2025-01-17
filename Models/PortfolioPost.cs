using Cre8tfolioBLL.Dto;
using System.ComponentModel.DataAnnotations;

namespace PersonalProjectCre8tfolio.Models
{
    public class PortfolioPost
    {
        public int Id { get; set; }
        [Required]
        public string? Title { get; set; }
        [Required]
        public string? Description { get; set; }
        public string? ImagePath { get; set; }
        public List<CommentDTO> Comments { get; set; }
    }
}
