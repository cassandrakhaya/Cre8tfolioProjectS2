

namespace Cre8tfolioBLL.Dto
{
    public class CommentDTO
    {
        public int Id { get; set; }
        public int PortfolioPostId { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
        //public DateTime CreatedAt { get; set; }
    }
}
