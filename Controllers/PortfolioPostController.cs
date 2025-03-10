using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using PersonalProjectCre8tfolio.Models;
using Cre8tfolioBLL.Services;
using Cre8tfolioBLL.Dto;
using System.Data;

namespace PersonalProjectCre8tfolio.Controllers
{

    public class PortfolioPostController : Controller
    {
        private readonly CommentService _commentService;
        private readonly PortfolioService _portfolioService;
        private readonly IWebHostEnvironment _hostingEnvironment;


        public PortfolioPostController(CommentService commentService, PortfolioService portfolioService, IWebHostEnvironment hostingEnvironment)
        {
            _commentService = commentService;
            _portfolioService = portfolioService;
            _hostingEnvironment = hostingEnvironment;

        }

        public ActionResult Index()
        {
            List<PortfolioPostDTO> postDTOs = _portfolioService.GetAllPosts();

            List<PortfolioPost> posts = postDTOs.Select(dto => new PortfolioPost
            {
                Id = dto.Id,
                Title = dto.Title,
                Description = dto.Description,
                ImagePath = dto.ImagePath
            }).ToList();


            return View(posts);
        }



        // GET: PortfolioPostController/Details/5
        public ActionResult Details(int id)
        {
            PortfolioPostDTO postDTO = _portfolioService.GetPost(id);
            if (postDTO == null)
            {
                return NotFound();  
            }

            var comments = _commentService.GetCommentsByPostId(id);

            PortfolioPost post = new PortfolioPost
            {
                Id = postDTO.Id,
                Title = postDTO.Title,
                Description = postDTO.Description,
                ImagePath = postDTO.ImagePath,
                Comments = comments
            };

            return View(post);  
        }

        [HttpPost]
        public IActionResult AddComment(int portfolioPostId, string content, string author)
        {
            if (!string.IsNullOrWhiteSpace(content) && !string.IsNullOrWhiteSpace(author))
            {
                _commentService.AddComment(new CommentDTO
                {
                    PortfolioPostId = portfolioPostId,
                    Content = content,
                    Author = author
                });
            }

            return RedirectToAction("Details", new { id = portfolioPostId });
        }

        [Authorize]
        [HttpPost]
        public IActionResult DeleteComment(int commentId, int portfolioPostId)
        {
            _commentService.DeleteComment(commentId);
            return RedirectToAction("Details", new { id = portfolioPostId });
        }

        // GET: PortfolioPostController/Create
        [Authorize]
        public ActionResult Create()
        {
            return View(new PortfolioPost());
        }

        // POST: PortfolioPostController/Create
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PortfolioPost portfolioPost, IFormFile? image)
        {
            if (!ModelState.IsValid) return View(portfolioPost);

            var postDTO = new PortfolioPostDTO
            {
                Title = portfolioPost.Title,
                Description = portfolioPost.Description
            };

            _portfolioService.CreatePost(postDTO, image, _hostingEnvironment.WebRootPath);
            return RedirectToAction(nameof(Index));
        }


        // GET: PortfolioPostController/Edit/5
        [Authorize]
        public ActionResult Edit(int id)
        {
            var postDTO = _portfolioService.GetPost(id);
            if (postDTO == null)
            {
                return NotFound();
            }

            var post = new PortfolioPost
            {
                Id = postDTO.Id,
                Title = postDTO.Title,
                Description = postDTO.Description,
                ImagePath = postDTO.ImagePath

            };
            return View(post);
        }


        // POST: PortfolioPostController/Edit/5
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, PortfolioPost portfolioPost, IFormFile? image)
        {
            if (!ModelState.IsValid) return View(portfolioPost);

            var postDTO = new PortfolioPostDTO
            {
                Id = portfolioPost.Id,
                Title = portfolioPost.Title,
                Description = portfolioPost.Description,
                ImagePath = portfolioPost.ImagePath
            };

            _portfolioService.EditPost(postDTO, image, _hostingEnvironment.WebRootPath);
            return RedirectToAction(nameof(Index));
        }


        // GET: PortfolioPostController/Delete/5
        [Authorize]
        public ActionResult Delete(int id)
        {
            PortfolioPostDTO postDTO = _portfolioService.GetPost(id);
            if (postDTO == null)
            {
                return NotFound();
            }

            PortfolioPost post = new PortfolioPost
            {
                Id = postDTO.Id,
                Title = postDTO.Title,
                Description = postDTO.Description,
                ImagePath = postDTO.ImagePath

            };
            return View(post);
        }

        // POST: PortfolioPostController/Delete/5
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(PortfolioPost portfolioPost, int id)
        {
            try
            {
                _portfolioService.DeletePost(id, _hostingEnvironment.WebRootPath);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

    }  
}
