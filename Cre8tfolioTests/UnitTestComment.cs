using Cre8tfolioBLL.Dto;
using Cre8tfolioBLL.Interfaces;
using Cre8tfolioBLL.Services;
using Moq;

namespace Cre8tfolioTests
{
    [TestClass]
    public class UnitTestComment
    {
        private CommentService _service;
        private Mock<ICommentRepository> _mockRepository;
        

        [TestInitialize]
        public void Setup()
        {
            //Hier maak je eigenlijk weer een mock aan, je hebt met moq geen appart gemaakte mock nodig. Alleen zo
            _mockRepository = new Mock<ICommentRepository>();

            _mockRepository.Setup(repo => repo.GetCommentsByPostId(It.IsAny<int>()))
                .Returns((int id) => new List<CommentDTO>
                {
                    new CommentDTO { Id = 1, PortfolioPostId = id, Content = "Mock Comment 1", Author = "Author 1" },
                    new CommentDTO { Id = 2, PortfolioPostId = id, Content = "Mock Comment 2", Author = "Author 2" }
                });

            _mockRepository.Setup(repo => repo.AddComment(It.IsAny<CommentDTO>())).Verifiable();
            _mockRepository.Setup(repo => repo.DeleteComment(It.IsAny<int>())).Verifiable();

            _service = new CommentService(_mockRepository.Object);
        }

        [TestMethod]
        public void GetCommentsByPostId_ReturnsCorrectComments()
        {
            // Arrange
            int portfolioPostId = 1;

            // Act
            var result = _service.GetCommentsByPostId(portfolioPostId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(portfolioPostId, result[0].PortfolioPostId);
            Assert.AreEqual("Mock Comment 1", result[0].Content);
            _mockRepository.Verify(repo => repo.GetCommentsByPostId(portfolioPostId), Times.Once);
        }

        [TestMethod]
        public void GetCommentsByPostId_ReturnsEmptyList_WhenNoCommentsExist()
        {
            // Arrange
            int portfolioPostId = 999; 
            _mockRepository.Setup(repo => repo.GetCommentsByPostId(portfolioPostId)).Returns(new List<CommentDTO>());

            // Act
            var result = _service.GetCommentsByPostId(portfolioPostId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
            _mockRepository.Verify(repo => repo.GetCommentsByPostId(portfolioPostId), Times.Once);
        }

        [TestMethod]
        public void AddComment_ThrowsException_WhenCommentIsInvalid()
        {
            // Arrange
            var invalidComment = new CommentDTO { Content = "", Author = "Cass" }; // Invalid data

            // Act en Assert
            Assert.ThrowsException<System.ArgumentException>(() => _service.AddComment(invalidComment));
            _mockRepository.Verify(repo => repo.AddComment(It.IsAny<CommentDTO>()), Times.Never);
        }

        [TestMethod]
        public void AddComment_CallsAddMethod()
        {
            // Arrange
            var comment = new CommentDTO { PortfolioPostId = 1, Content = "New Comment", Author = "Author 3" };

            // Act
            _service.AddComment(comment);

            // Assert
            _mockRepository.Verify(repo => repo.AddComment(It.Is<CommentDTO>(c => c.PortfolioPostId == 1 && c.Content == "New Comment" && c.Author == "Author 3")), Times.Once);

        }
        [TestMethod]
        public void DeleteComment_RemovesCommentFromRepository()
        {
            // Arrange
            var comments = new List<CommentDTO>
            {
                new CommentDTO { Id = 1, PortfolioPostId = 1, Content = "Comment 1", Author = "Author 1" },
                new CommentDTO { Id = 2, PortfolioPostId = 1, Content = "Comment 2", Author = "Author 2" }
            };

            _mockRepository.Setup(repo => repo.GetCommentsByPostId(1)).Returns(comments);
            _mockRepository.Setup(repo => repo.DeleteComment(It.IsAny<int>()))
                .Callback<int>(id => comments.RemoveAll(c => c.Id == id));

            int commentIdToDelete = 1;

            // Act
            _service.DeleteComment(commentIdToDelete);

            // Assert
            Assert.IsFalse(comments.Any(c => c.Id == commentIdToDelete), "The comment should have been removed from the repository.");
            _mockRepository.Verify(repo => repo.DeleteComment(commentIdToDelete), Times.Once);
        }
        
        [TestMethod]
        public void DeleteComment_CallsDeleteMethod()
        {
            // Arrange
            int commentId = 1;

            // Act
            _service.DeleteComment(commentId);

            // Assert
            _mockRepository.Verify(repo => repo.DeleteComment(commentId), Times.Once);
        }
    }
}
