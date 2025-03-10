namespace PersonalProjectCre8tfolio.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public int PortfolioPostId { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
