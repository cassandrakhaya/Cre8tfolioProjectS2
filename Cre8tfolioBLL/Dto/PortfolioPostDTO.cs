

namespace Cre8tfolioBLL.Dto
{
    public class PortfolioPostDTO
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }

        public string? ImagePath { get; set; }
        public List<CommentDTO> Comments { get; set; }
        //public int DateCreated { get; set; }
        //public int DateUpdated { get; set; }



    }
}
