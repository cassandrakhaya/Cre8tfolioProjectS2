using Cre8tfolioBLL.Dto;
namespace Cre8tfolioBLL.Interfaces
{
    public interface IPortfolioRepository 
    {
        List<PortfolioPostDTO> GetPortfolioPostDTOs();
        PortfolioPostDTO GetPortfolioPostById(int id);
        void CreatePost(PortfolioPostDTO portfolioPost);
        void EditPost(PortfolioPostDTO portfolioPost);
        void DeletePost(int id);
    }
}
