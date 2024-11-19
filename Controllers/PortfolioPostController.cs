using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using PersonalProjectCre8tfolio.Models;
//using Cre8tfolioBLL.Services;
using Cre8tfolioBLL.Services;
using Cre8tfolioBLL.Dto;


namespace PersonalProjectCre8tfolio.Controllers
{
    
    public class PortfolioPostController : Controller
    {
        private readonly PortfolioService _portfolioService;
        //PortfolioRepository portfolioRepository = new PortfolioRepository();

        public PortfolioPostController(PortfolioService portfolioService)
        {
            _portfolioService = portfolioService;
        }

        public ActionResult Index()
        {
            //PortfolioRepository portfolioRepository = new PortfolioRepository();
            List<PortfolioPostDTO> postDTOs = _portfolioService.GetAllPosts();

            List<PortfolioPost> posts = postDTOs.Select(dto => new PortfolioPost
            {
                Id = dto.Id,
                Title = dto.Title,
                Description = dto.Description,
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
                        Description = portfolioPost.Description
                    };
                    //PortfolioRepository portfolioRepository = new PortfolioRepository();
                    
                    _portfolioService.CreatePost(postDto);
                    //portfolioRepository.CreatePost(postDto);

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
                    //PortfolioRepository portfolioRepository = new PortfolioRepository();
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