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
    public class HomeController : Controller
    {
        private BannerService _bannerService;
        private NewsService _newsService;
        private NewsClassifyService _newsClassifyService;
        public HomeController(BannerService bannerService, NewsClassifyService newsClassifyService, NewsService newsService)
        {
            _bannerService = bannerService;
            _newsClassifyService = newsClassifyService;
            _newsService = newsService;
        }
        public IActionResult Index()
        {
            var ResponseModel = _newsClassifyService.GetNewsClassifyList();
            return View(ResponseModel);
        }

        [HttpGet]
        public IActionResult GetBanners()
        {
            var ResponseModel = _bannerService.GetBannerList();
            return Json(ResponseModel);
        }

        public IActionResult GetNewsList()
        {
            var ResponseModel = _newsService.GetNewsList();
            return Json(ResponseModel);
        }

        public IActionResult GetTopNewsList()
        {
            var ResponseModel = _newsService.GetTopNewsList(c =>true, 5);
            return Json(ResponseModel);
        }
        

    }
}
