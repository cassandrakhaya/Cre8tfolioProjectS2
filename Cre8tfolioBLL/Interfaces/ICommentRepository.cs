using Cre8tfolioBLL.Dto;
using System.Collections.Generic;

namespace Cre8tfolioBLL.Interfaces
{
    public interface ICommentRepository
    {
        public List<CommentDTO> GetCommentsByPostId(int portfolioPostId);
        public void AddComment(CommentDTO comment);
        public void DeleteComment(int commentId);
    }
}
