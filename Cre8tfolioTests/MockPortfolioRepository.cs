using Cre8tfolioBLL.Dto;
using Cre8tfolioDAL;
using Moq;
//first
namespace Cre8tfolioTests
{
    public class MockPortfolioRepository
    {
        public static Mock<PortfolioRepository> GetMock()
        {
            var mock = new Mock<PortfolioRepository>();

            mock.Setup(repo => repo.GetPortfolioPostById(It.IsAny<int>()))
             .Returns((int id) => new PortfolioPostDTO
             {
                 Id = id,
                 Title = $"Post {id}",
                 Description = $"Description {id}",
                 ImagePath = $"ImagePath{id}.jpg"
             });

            mock.Setup(repo => repo.GetPortfolioPostDTOs())
            .Returns(new List<PortfolioPostDTO>
            {
                new PortfolioPostDTO { Id = 1, Title = "Post 1", Description = "Description 1", ImagePath = "ImagePath1.jpg" },
                new PortfolioPostDTO { Id = 2, Title = "Post 2", Description = "Description 2", ImagePath = "ImagePath2.jpg" }
            });

            mock.Setup(repo => repo.CreatePost(It.IsAny<PortfolioPostDTO>()))
            .Verifiable();

            mock.Setup(repo => repo.EditPost(It.IsAny<PortfolioPostDTO>()))
            .Verifiable();

            mock.Setup(repo => repo.DeletePost(It.IsAny<int>()))
            .Verifiable();

            return mock;
        }
    } 
}
