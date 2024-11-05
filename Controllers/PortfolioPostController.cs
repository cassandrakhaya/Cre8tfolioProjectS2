using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using PersonalProjectCre8tfolio.Models;

namespace PersonalProjectCre8tfolio.Controllers
{
    public class PortfolioPostController : Controller
    {
        private string Str = @"Data Source=CASCENNDRA\SQLEXPRESS01;Initial Catalog=CRE8TFOLIO;Integrated Security=True;";
        //private string Str = @"Data Source=CASCENNDRA\SQLEXPRESS01;Initial Catalog=CRE8TFOLIO;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False";
        // GET: PortfolioPostController
        public ActionResult Index()
        {
            //TODO: Maak een list van portfolioposts in plaats van een datatable
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(Str))
            {
                con.Open();
                //TODO: Maak duidelijk welke data je ophaalt (welke kolommen) in plaats van *
                string q = "Select * from PortfolioPost";
                SqlDataAdapter da = new SqlDataAdapter(q, con);
                da.Fill(dt);
            }
            return View(dt);
        }

        // GET: PortfolioPostController/Details/5
        public ActionResult Details(int id)
        {
            return View();
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
            try
            {
                using (SqlConnection con = new SqlConnection(Str))
                {
                    con.Open();
                    //TODO: met parameters gaan werken
                    string q = "insert into PortfolioPost (Title, Description) values('" + portfolioPost.Title + "','" + portfolioPost.Description + "')";
                    SqlCommand cmd = new SqlCommand(q, con);
                    cmd.ExecuteNonQuery();
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(portfolioPost);
            }
        }

        // GET: PortfolioPostController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PortfolioPostController/Edit/5
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

        // GET: PortfolioPostController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PortfolioPostController/Delete/5
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
