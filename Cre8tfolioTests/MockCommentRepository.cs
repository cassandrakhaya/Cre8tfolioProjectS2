using Cre8tfolioBLL.Dto;
using Cre8tfolioDAL;
using Moq;

namespace Cre8tfolioTests
{
    public class MockCommentRepository
    {
        public static Mock<CommentRepository> GetMock()
        {
            var mock = new Mock<CommentRepository>();

            mock.Setup(repo => repo.GetCommentsByPostId(It.IsAny<int>()))
                .Returns<int>(id => new List<CommentDTO>
                {
                    new CommentDTO { Id = 1, PortfolioPostId = id, Content = "Great post!", Author = "Cassandra Khaya"},
                    new CommentDTO { Id = 2, PortfolioPostId = id, Content = "Thanks for sharing!", Author = "Anonymous" }
                });
            mock.Setup(repo => repo.AddComment(It.IsAny<CommentDTO>()))
               .Verifiable();

            mock.Setup(repo => repo.DeleteComment(It.IsAny<int>()))
                .Verifiable();

            return mock;
        }
    }
}


