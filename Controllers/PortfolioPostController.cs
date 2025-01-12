using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using PersonalProjectCre8tfolio.Models;
using Cre8tfolioBLL.Services;
using Cre8tfolioBLL.Dto;
using System.Data;
using static System.Net.Mime.MediaTypeNames;


namespace PersonalProjectCre8tfolio.Controllers
{
    
    public class PortfolioPostController : Controller
    {
        private readonly PortfolioService _portfolioService;
        private readonly IWebHostEnvironment _hostingEnvironment;


        public PortfolioPostController(PortfolioService portfolioService, IWebHostEnvironment hostingEnvironment)
        {
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

            //TODO: aanvragen aan repository om alle DTO's te geven

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
            PortfolioPost post = new PortfolioPost
            {
                Id = postDTO.Id,
                Title = postDTO.Title,
                Description = postDTO.Description,
                ImagePath = postDTO.ImagePath
            };
            return View(post);
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
        public ActionResult Create(PortfolioPost portfolioPost, IFormFile Image)
        {
            if (ModelState.IsValid)
            //TODO: Opzoeken waar je de regels kan defineren.
            {
                string uniqueFileName = null;
                if (Image != null)
                {
                    string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images");

                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder); // Create the folder if it doesn't exist
                    }

                    uniqueFileName = Guid.NewGuid().ToString() + "_" + Image.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        Image.CopyTo(fileStream);
                    }
                }

                var postDTO = new PortfolioPostDTO
                {
                    Title = portfolioPost.Title,
                    Description = portfolioPost.Description,
                    ImagePath = uniqueFileName != null ? "/images/" + uniqueFileName : null
                };

                _portfolioService.CreatePost(postDTO);
                return RedirectToAction(nameof(Index));
            }

            return View(portfolioPost);
        }



        //public IActionResult Create(SquadViewModel squadViewModel)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            CreateEditSquadDto squadDto = new CreateEditSquadDto()
        //            {
        //                Name = squadViewModel.Name,
        //                Description = squadViewModel.Description
        //            };

        //            _squadService.CreateSquad(squadDto);
        //            return RedirectToAction("Index");
        //        }
        //        return View(squadViewModel);
        //    }
        //    catch (DuplicateNameException ex)
        //    {
        //        ViewData["ErrorMessage"] = "Er bestaat al een squad met deze naam";
        //        return View(squadViewModel);
        //    }
        //    catch (Exception e)
        //    {
        //        return View("Create");
        //    }
        //}






        // GET: PortfolioPostController/Edit/5
        //Needs to Retrieve the data for editing, just like the details GET
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
        //Needs to Save the changes
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, PortfolioPost portfolioPost, IFormFile? Image)
        {
            if (ModelState.IsValid)
            {
                string imagePath = portfolioPost.ImagePath;

                if (Image != null)
                {
                    string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images");
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + Image.FileName;
                    imagePath = Path.Combine("images", uniqueFileName);
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        Image.CopyTo(fileStream);
                    }
                };


                var postDTO = new PortfolioPostDTO
                {
                    Id = portfolioPost.Id,
                    Title = portfolioPost.Title,
                    Description = portfolioPost.Description,
                    ImagePath = imagePath
                };

                _portfolioService.EditPost(postDTO);
                return RedirectToAction(nameof(Index));
            }

            return View(portfolioPost);
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
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                var postDTO = _portfolioService.GetPost(id);
                if (postDTO?.ImagePath != null)
                {
                    // Delete the associated image from "wwwroot/images"
                    string filePath = Path.Combine(_hostingEnvironment.WebRootPath, postDTO.ImagePath.TrimStart('/'));
                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }
                }

                _portfolioService.DeletePost(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");

                PortfolioPostDTO portfolioPostDTO = _portfolioService.GetPost(id);
                if (portfolioPostDTO == null)
                {
                    return NotFound();
                }

                PortfolioPost portfolioPost = new PortfolioPost
                {
                    Id = portfolioPostDTO.Id,
                    Title = portfolioPostDTO.Title,
                    Description = portfolioPostDTO.Description,
                    ImagePath = portfolioPostDTO.ImagePath
                };
                return View(portfolioPost);
            }
        }
    }





}
//using (SqlConnection con = new SqlConnection(Str))
//{
//    con.Open();
//    //TODO: met parameters gaan werken
//    string q = "insert into PortfolioPost (Title, Description) values('" + portfolioPost.Title + "','" + portfolioPost.Description + "')";
//    SqlCommand cmd = new SqlCommand(q, con);
//    cmd.ExecuteNonQuery();
//}
//return RedirectToAction(nameof(Index));