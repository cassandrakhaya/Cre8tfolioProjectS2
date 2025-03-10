using Cre8tfolioBLL.Dto;
using Cre8tfolioBLL.Interfaces;


namespace Cre8tfolioBLL.Services
{
    public class CommentService
    {
        private readonly ICommentRepository _repository;

        public CommentService(ICommentRepository repository)
        {
            _repository = repository;
        }

        public List<CommentDTO> GetCommentsByPostId(int portfolioPostId)
        {
            var comments = _repository.GetCommentsByPostId(portfolioPostId);
            return comments.Select(c => new CommentDTO
            {
                Id = c.Id,
                PortfolioPostId = c.PortfolioPostId,
                Content = c.Content,
                Author = c.Author
            }).ToList();
        }

        public void AddComment(CommentDTO commentDTO)
        {
            var commentEntity = new CommentDTO
            {
                PortfolioPostId = commentDTO.PortfolioPostId,
                Content = commentDTO.Content,
                Author = commentDTO.Author
            };

            if (string.IsNullOrWhiteSpace(commentDTO.Content))
            {
                throw new ArgumentException("Content cannot be empty.", nameof(commentDTO.Content));
            }

            if (string.IsNullOrWhiteSpace(commentDTO.Author))
            {
                throw new ArgumentException("Author cannot be empty.", nameof(commentDTO.Author));
            }

            _repository.AddComment(commentEntity);
        }

        public void DeleteComment(int commentId)
        {
            _repository.DeleteComment(commentId);
        }
    }
}

