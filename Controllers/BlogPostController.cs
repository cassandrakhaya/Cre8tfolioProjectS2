using Cre8tfolioBLL.Dto;
using Cre8tfolioBLL.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonalProjectCre8tfolio.Models;

namespace Cre8tfolioPL.Controllers
{
    public class BlogPostController : Controller
    {
        private readonly BlogService _blogService;

        public BlogPostController(BlogService blogService)
        {
            _blogService = blogService;
        }

        // GET: BlogPostController
        public ActionResult Index()
        {
            List<BlogPostDTO> BpostDTOs = _blogService.GetAllPosts();

            List<BlogPost> bposts = BpostDTOs.Select(dto => new BlogPost
            {
                Id = dto.Id,
                Title = dto.Title,
                Description = dto.Description,
            }).ToList();

            return View(bposts);
        }

        // GET: BlogPostController/Details/5
        public ActionResult Details(int id)
        {
            var postDTO = _blogService.GetPost(id);
            if (postDTO == null)
            {
                return NotFound();
            }

            var post = new BlogPost
            {
                Id = postDTO.Id,
                Title = postDTO.Title,
                Description = postDTO.Description,
            };

            return View(post);
        }


        // GET: BlogPostController/Create
        // GET: BlogPostController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BlogPostController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BlogPost blogPost)
        {
            if (ModelState.IsValid)
            {
                var postDTO = new BlogPostDTO
                {
                    Title = blogPost.Title,
                    Description = blogPost.Description,
                };

                _blogService.CreatePost(postDTO);
                return RedirectToAction(nameof(Index));
            }

            return View(blogPost);
        }


        // GET: BlogPostController/Edit/5
        // GET: BlogPostController/Edit/5
        public ActionResult Edit(int id)
        {
            var postDTO = _blogService.GetPost(id);
            if (postDTO == null)
            {
                return NotFound();
            }

            var post = new BlogPost
            {
                Id = postDTO.Id,
                Title = postDTO.Title,
                Description = postDTO.Description,
            };

            return View(post);
        }

        // POST: BlogPostController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, BlogPost blogPost)
        {
            if (ModelState.IsValid)
            {
                var postDTO = new BlogPostDTO
                {
                    Id = blogPost.Id,
                    Title = blogPost.Title,
                    Description = blogPost.Description,
                };

                _blogService.EditPost(postDTO);
                return RedirectToAction(nameof(Index));
            }

            return View(blogPost);
        }


        // GET: BlogPostController/Delete/5
        public ActionResult Delete(int id)
        {
            BlogPostDTO postDTO = _blogService.GetPost(id);
            if (postDTO == null)
            {
                return NotFound();
            }

            BlogPost posts = new BlogPost
            {
                Id = postDTO.Id,
                Title = postDTO.Title,
                Description = postDTO.Description,
            };

            return View(posts);
        }

        //POST: BlogPostController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                _blogService.DeletePost(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");

                BlogPostDTO blogPostDTO = _blogService.GetPost(id);
                if (blogPostDTO == null)
                {
                    return NotFound();
                }

                BlogPost blogPost = new BlogPost
                {
                    Id = blogPostDTO.Id,
                    Title = blogPostDTO.Title,
                    Description = blogPostDTO.Description,
                };
                return View(blogPost);
            }

        }

    }
}


//public ActionResult Index()
//{
//    List<BlogPostDTO> postDTOs = _blogService.GetAllPosts();
//    List<BlogPost> posts = postDTOs.Select(dto => new BlogPost
//    {
//        Id = dto.Id,
//        Title = dto.Title,
//        Description = dto.Description,
//    }).ToList();

//    return View(posts);

//}

//// GET: BlogPostController/Details/5
//public ActionResult Details(int id)
//{
//    BlogPostDTO postDTO = _blogService.GetPost(id);
//    if (postDTO == null)
//    {
//        return NotFound();
//    }
//    BlogPost post = new BlogPost
//    {
//        Id = postDTO.Id,
//        Title = postDTO.Title,
//        Description = postDTO.Description,
//    };
//    return View(post);
//}



//// GET: BlogPostController/Create
//public ActionResult Create()
//{
//    return View(new BlogPost());
//}

//// POST: BlogPostController/Create
//[HttpPost]
//[ValidateAntiForgeryToken]
//public ActionResult Create(BlogPost blogPost)
//{
//    if (ModelState.IsValid)
//    //TODO: Opzoeken waar je de regels kan defineren.
//    {
//        try
//        {
//            BlogPostDTO postDto = new BlogPostDTO
//            {
//                Title = blogPost.Title,
//                Description = blogPost.Description
//            };

//            _blogService.CreatePost(postDto);

//            return RedirectToAction(nameof(Index));
//        }
//        catch (Exception ex)
//        {
//            Console.WriteLine(ex.Message);
//            return View(blogPost);
//        }
//    }
//    return View(blogPost);
//}




//// GET: BlogPostController/Edit/5
////Needs to Retrieve the data for editing, just like the details GET
//public ActionResult Edit(int id)
//{
//    BlogPostDTO postDTO = _blogService.GetPost(id);
//    if (postDTO == null)
//    {
//        return NotFound();
//    }

//    BlogPost post = new BlogPost
//    {
//        Id = postDTO.Id,
//        Title = postDTO.Title,
//        Description = postDTO.Description,
//    };
//    return View(post);
//}


//// POST: BlogPostController/Edit/5
////Needs to Save the changes
//[HttpPost]
//[ValidateAntiForgeryToken]
//public ActionResult Edit(int id, BlogPost blogPost)
//{
//    if (ModelState.IsValid)
//    {
//        try
//        {
//            BlogPostDTO postDTO = new BlogPostDTO
//            {
//                Id = blogPost.Id,
//                Title = blogPost.Title,
//                Description = blogPost.Description
//            };
//            _blogService.EditPost(postDTO);
//            return RedirectToAction(nameof(Index));
//        }
//        catch (Exception ex)
//        {
//            Console.WriteLine(ex.Message);
//            return View(blogPost);
//        }
//    }
//    return View(blogPost);
//}


//// GET: BlogPostController/Delete/5
//public ActionResult Delete(int id)
//{
//    BlogPostDTO postDTO = _blogService.GetPost(id);
//    if (postDTO == null)
//    {
//        return NotFound();
//    }

//    BlogPost post = new BlogPost
//    {
//        Id = postDTO.Id,
//        Title = postDTO.Title,
//        Description = postDTO.Description,
//    };
//    return View(post);
//}

//// POST: BlogPostController/Delete/5
//[HttpPost]
//[ValidateAntiForgeryToken]
//public ActionResult Delete(int id, IFormCollection collection)
//{
//    try
//    {
//        _blogService.DeletePost(id);
//        return RedirectToAction(nameof(Index));
//    }
//    catch (Exception ex)
//    {
//        Console.WriteLine($"{ex.Message}");

//        BlogPostDTO postDTO = _blogService.GetPost(id);
//        if (postDTO == null)
//        {
//            return NotFound();
//        }

//        BlogPost post = new BlogPost
//        {
//            Id = postDTO.Id,
//            Title = postDTO.Title,
//            Description = postDTO.Description,
//        };
//        return View(post);
//    }
//}
