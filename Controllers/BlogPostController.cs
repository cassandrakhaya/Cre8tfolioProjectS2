using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cre8tfolioPL.Controllers
{
    public class BlogPostController : Controller
    {
        // GET: BlogPostController
        public ActionResult Index()
        {
            return View();
        }

        // GET: BlogPostController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: BlogPostController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BlogPostController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BlogPostController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: BlogPostController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BlogPostController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: BlogPostController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
