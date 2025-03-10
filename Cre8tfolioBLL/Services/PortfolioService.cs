using Cre8tfolioBLL.Dto;
using Cre8tfolioBLL.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Cre8tfolioBLL.Services

{
    public class PortfolioService
    {
        public IPortfolioRepository _repository;

        public PortfolioService(IPortfolioRepository repository)
        {
            _repository = repository;
        }

        public List<PortfolioPostDTO> GetAllPosts()
        {
            try
            {
                return _repository.GetPortfolioPostDTOs();
            }
            catch (Exception e)
            {
                throw new Exception("An error occurred while retrieving portfolio posts", e);
            }
        }

        public PortfolioPostDTO GetPost(int id)
        {
            try
            {
                return _repository.GetPortfolioPostById(id);
            }
            catch (Exception e)
            {
                throw new Exception("An error occurred while retrieving a portfolio post", e);
            }
        }

        public void EditPost(PortfolioPostDTO postDto, IFormFile? image, string webRootPath)
        {
            postDto.ImagePath = SaveImageIfExists(image, webRootPath);
            _repository.EditPost(postDto);
        }

        public void DeletePost(int id, string webRootPath)
        {
            var postDto = _repository.GetPortfolioPostById(id);

            if (!string.IsNullOrEmpty(postDto?.ImagePath))
            {
                string filePath = Path.Combine(webRootPath, postDto.ImagePath.TrimStart('/'));
                if (File.Exists(filePath)) File.Delete(filePath);
            }

            _repository.DeletePost(id);
        }
        public void CreatePost(PortfolioPostDTO postDto, IFormFile? image, string webRootPath)
        {
            postDto.ImagePath = SaveImageIfExists(image, webRootPath);
            _repository.CreatePost(postDto);
        }
        public string SaveImageIfExists(IFormFile? image, string webRootPath)
        {
            if (image == null) return null;

            string uploadsFolder = Path.Combine(webRootPath, "images");
            Directory.CreateDirectory(uploadsFolder);

            string uniqueFileName = $"{Guid.NewGuid()}_{image.FileName}";
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                image.CopyTo(fileStream);
            }

            return $"/images/{uniqueFileName}";
        }
    }
}


    
