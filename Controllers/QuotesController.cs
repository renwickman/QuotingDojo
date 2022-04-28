using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using QuotingDojo.Models;

namespace QuotingDojo.Controllers
{
    public class QuotesController : Controller
    {
        [HttpGet("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("quotes")]
        public IActionResult Quotes()
        {
            List<Dictionary<string, object>> AllQuotes = DbConnector.Query($"SELECT * FROM quotes ORDER BY created_at");
            ViewBag.Quotes = AllQuotes;
            return View();
        }

        [HttpPost("quotes")]
        public IActionResult Create(Quote quote)
        {
            if (ModelState.IsValid)
            {
                string sql = $@"INSERT INTO quotes (author, content, created_at) VALUES ('{quote.Author}', '{quote.Content}', Now())";
                DbConnector.Execute(sql);
                return RedirectToAction("Quotes");
            }
            return View("Index");
        }
    }
}
