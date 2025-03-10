using Cre8tfolioBLL.Services;
using Cre8tfolioBLL.Dto;
using Moq;
using Cre8tfolioBLL.Interfaces;
using Microsoft.AspNetCore.Http;
//Je hoeft alleen maar de BLL te testen
namespace Cre8tfolioTests.Controllers
{
    [TestClass]
    public class PortfolioTests
    {
        private PortfolioService _service;
        private Mock<IPortfolioRepository> _mockRepository;

        [TestInitialize]
        public void Setup()
        {
            _mockRepository = new Mock<IPortfolioRepository>();

            _mockRepository.Setup(repo => repo.GetPortfolioPostDTOs())
                .Returns(new List<PortfolioPostDTO>
                {
                    new PortfolioPostDTO { Id = 1, Title = "Post 1", Description = "Description 1"},
                    new PortfolioPostDTO { Id = 2, Title = "Post 2", Description = "Description 2"}
                });

            _mockRepository.Setup(repo => repo.GetPortfolioPostById(It.IsAny<int>()))
                .Returns((int id) => new PortfolioPostDTO
                {
                    Id = id,
                    Title = $"Post {id}",
                    Description = $"Description {id}",
                });

            _mockRepository.Setup(repo => repo.CreatePost(It.IsAny<PortfolioPostDTO>())).Verifiable();
            _mockRepository.Setup(repo => repo.EditPost(It.IsAny<PortfolioPostDTO>())).Verifiable();
            _mockRepository.Setup(repo => repo.DeletePost(It.IsAny<int>())).Verifiable();

            _service = new PortfolioService(_mockRepository.Object);
        }
        [TestMethod]
        public void GetPost_ReturnsCorrectPost()
        {
            // Arrange
            var post = new PortfolioPostDTO { Id = 1, Title = "Post 1", Description = "Description 1", ImagePath = "image1.jpg" };
            _mockRepository.Setup(repo => repo.GetPortfolioPostById(1)).Returns(post);

            // Act
            var result = _service.GetPost(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Id);
            Assert.AreEqual("Post 1", result.Title);
            _mockRepository.Verify(repo => repo.GetPortfolioPostById(1), Times.Once);
        }

        [TestMethod]
        public void CreatePost_ReturnsError_MissingRequiredElements()
        {
            {
                // Arrange
                var newPost = new PortfolioPostDTO
                {
                    Title = "", 
                    Description = "", 
                    ImagePath = "newimage.jpg"
                };
                string fakeWebRootPath = Path.GetTempPath();
                Mock<IFormFile> mockFile = null; 

                // Act
                _service.CreatePost(newPost, mockFile?.Object, fakeWebRootPath);

                // Assert: Verwacht dat een Argument Exception wordt gegooid.
            }
        }
        [TestMethod]
        public void CreatePost_SavesPost_WithImage()
        {
            // Arrange
            var post = new PortfolioPostDTO { Title = "New Post", Description = "New Description" };
            var mockFile = new Mock<IFormFile>();
            mockFile.Setup(f => f.FileName).Returns("image.jpg");

            string fakeWebRootPath = Path.GetTempPath();
            string expectedPath = Path.Combine(fakeWebRootPath, "images");

            // Act
            _service.CreatePost(post, mockFile.Object, fakeWebRootPath);

            // Assert
            Assert.IsTrue(post.ImagePath.StartsWith("/images/"));
            _mockRepository.Verify(repo => repo.CreatePost(It.IsAny<PortfolioPostDTO>()), Times.Once);
            Assert.IsTrue(Directory.Exists(expectedPath));

            // Cleanup
            Directory.Delete(expectedPath, true);
        }
        [TestMethod]
        public void EditPost_UpdatesPost_WithImage()
        {
            // Arrange
            var post = new PortfolioPostDTO { Id = 1, Title = "Updated Post", Description = "Updated Description" };
            var mockFile = new Mock<IFormFile>();
            mockFile.Setup(f => f.FileName).Returns("updatedimage.jpg");

            string fakeWebRootPath = Path.GetTempPath();

            // Act
            _service.EditPost(post, mockFile.Object, fakeWebRootPath);

            // Assert
            _mockRepository.Verify(repo => repo.EditPost(It.Is<PortfolioPostDTO>(p => p.Id == 1 && p.Title == "Updated Post")), Times.Once);
        }
        [TestMethod]
        public void DeletePost_RemovesPost_AndDeletesImage()
        {
            // Arrange
            var post = new PortfolioPostDTO { Id = 1, ImagePath = "/images/testimage.jpg" };
            _mockRepository.Setup(repo => repo.GetPortfolioPostById(1)).Returns(post);

            string fakeWebRootPath = Path.GetTempPath();
            string imagePath = Path.Combine(fakeWebRootPath, "images", "testimage.jpg");
            Directory.CreateDirectory(Path.Combine(fakeWebRootPath, "images"));
            File.WriteAllText(imagePath, "Fake image content");

            // Act
            _service.DeletePost(1, fakeWebRootPath);

            // Assert
            _mockRepository.Verify(repo => repo.DeletePost(1), Times.Once);
            Assert.IsFalse(File.Exists(imagePath));

            // Cleanup
            Directory.Delete(Path.Combine(fakeWebRootPath, "images"), true);
        }
        [TestMethod]
        public void SaveImageIfExists_ShouldCreateDirectory_WhenImageIsProvided()
        {
            // Arrange
            var mockImage = new Mock<IFormFile>();
            var fileName = "testImage.jpg";
            var webRootPath = "C:\\Users\\ck825\\OneDrive\\Bureaublad\\Picture posts"; 

            mockImage.Setup(m => m.FileName).Returns(fileName);
            mockImage.Setup(m => m.CopyTo(It.IsAny<Stream>()));

            var uploadsFolder = Path.Combine(webRootPath, "images");

            // Act
            var result = _service.SaveImageIfExists(mockImage.Object, webRootPath);

            // Assert
            Assert.IsTrue(Directory.Exists(uploadsFolder), "De map 'images' zou aangemaakt moeten zijn.");
        }
    }
}
    
    