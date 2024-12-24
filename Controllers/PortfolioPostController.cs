using Microsoft.AspNetCore.Mvc;
using PersonalProjectCre8tfolio.Models;
using Cre8tfolioBLL.Services;
using Cre8tfolioBLL.Dto;
using System.Data;


namespace PersonalProjectCre8tfolio.Controllers
{
    
    public class PortfolioPostController : Controller
    {
        private readonly PortfolioService _portfolioService;

        public PortfolioPostController(PortfolioService portfolioService)
        {
            _portfolioService = portfolioService;
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
        public ActionResult Create()
        {
            return View(new PortfolioPost());
        }

        // POST: PortfolioPostController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PortfolioPost portfolioPost)
        {
            if (ModelState.IsValid)
            //TODO: Opzoeken waar je de regels kan defineren.
            {
                try
                {
                    PortfolioPostDTO postDto = new PortfolioPostDTO
                    {
                        Title = portfolioPost.Title,
                        Description = portfolioPost.Description,
                        ImagePath = portfolioPost.ImagePath
                    };

                    _portfolioService.CreatePost(postDto);

                    return RedirectToAction(nameof(Index));
                }
                catch (InvalidOperationException ex)
                {
                    ViewData["ErrorMessage"] = "Er bestaat al een Post met deze naam";
                    Console.WriteLine("Exception caught: " + ex.Message);
                    return View(portfolioPost);
                }
                //    catch (Exception)
                //    {
                //        ModelState.AddModelError(string.Empty, "An unexpected error occurred.");
                //        return View(portfolioPost);
                //    }
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
        public ActionResult Edit(int id)
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
            };
            return View(post);
        }


        // POST: PortfolioPostController/Edit/5
        //Needs to Save the changes
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, PortfolioPost portfolioPost)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    PortfolioPostDTO postDTO = new PortfolioPostDTO
                    {
                        Id = portfolioPost.Id,
                        Title = portfolioPost.Title,
                        Description = portfolioPost.Description
                    };
                    _portfolioService.EditPost(postDTO);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return View(portfolioPost);
                }
            }
            return View(portfolioPost);
        }


        // GET: PortfolioPostController/Delete/5
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
            };
            return View(post);
        }

        // POST: PortfolioPostController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                _portfolioService.DeletePost(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");

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
                };
                return View(post);
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