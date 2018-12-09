using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using News.Models;
using News.Service;

namespace News.Controllers
{
    public class NewsController : Controller
    {
        private NewsClassifyService _newsClassifyService;
        private NewsService _newsService;
        public NewsController(NewsService newsService, NewsClassifyService newsClassifyService)
        {
            _newsClassifyService = newsClassifyService;
            _newsService = newsService;
        }
        public IActionResult Index()
        {
            var ResponseModel = _newsClassifyService.GetNewsClassifyList();
            return View(ResponseModel);
        }

        public IActionResult Detail()
        {
            var ResponseModel = _newsClassifyService.GetNewsClassifyList();
            return View(ResponseModel);
        }

        public IActionResult GetNewsList()
        {
            var ResponseModel = _newsService.GetNewsList();
            return Json(ResponseModel);
        }

    }
}
