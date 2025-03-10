
namespace Cre8tfolioDAL.Entities
{
    public class PortfolioEntity
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }

        public int DateCreated { get; set; }

        public int DateUpdated { get; set; }   

        public string? ImagePath { get; set; }
        public List<CommentEntity> Comments { get; set; }
    }
}
