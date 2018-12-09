using Microsoft.EntityFrameworkCore;
using News.Model.Entity;
using News.Model.Request;
using News.Model.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace News.Service
{

    public class NewsService
    {
        private NewsContext _context;
        public NewsService(NewsContext context)
        {
            _context = context;
        }

        /// <summary>
        /// 新增新闻
        /// </summary>
        /// <param name="News"></param>
        /// <returns></returns>
        public ResponseModel AddNews(Model.Request.AddNews news)
        {
            if (news == null || news.NewsClassifyId <= 0 || news.Sort <= 0 || string.IsNullOrWhiteSpace(news.Title) || string.IsNullOrWhiteSpace(news.Content) || string.IsNullOrWhiteSpace(news.Image))
                return new ResponseModel { Code = 0, Msg = "数据不正确" };

            Model.Entity.News dbNews = new Model.Entity.News()
            {
                Title = news.Title,
                Content = news.Content,
                Image = news.Image,
                Sort = news.Sort,
                NewsClassifyId = news.NewsClassifyId,
                Createtime = DateTime.Now,
                LastUpdateTime = DateTime.Now
            };
            _context.News.Add(dbNews);
            int count = _context.SaveChanges();
            if (count > 0)
                return new ResponseModel { Code = 200, Msg = "新闻添加成功" };
            else
                return new ResponseModel { Code = 0, Msg = "新闻添加失败" };
        }

        /// <summary>
        /// 删除新闻
        /// </summary>
        /// <param name="NewsId"></param>
        /// <returns></returns>
        public ResponseModel DeleteNews(long NewsId)
        {
            if (NewsId <= 0)
                return new ResponseModel { Code = 0, Msg = "数据不正确" };

            Model.Entity.News dbNews = _context.News.Find(NewsId);
            if (dbNews == null)
                return new ResponseModel { Code = 0, Msg = "数据不正确" };

            _context.News.Remove(dbNews);
            int count = _context.SaveChanges();
            if (count > 0)
                return new ResponseModel { Code = 200, Msg = "新闻删除成功" };
            else
                return new ResponseModel { Code = 0, Msg = "新闻删除失败" };
        }

        /// <summary>
        /// 更新新闻
        /// </summary>
        /// <param name="News"></param>
        /// <returns></returns>
        public ResponseModel EditNews(Model.Request.EditNews news)
        {
            if (news == null || news.Id <= 0 || news.NewsClassifyId <= 0 || news.Sort <= 0 || string.IsNullOrWhiteSpace(news.Title) || string.IsNullOrWhiteSpace(news.Content) || string.IsNullOrWhiteSpace(news.Image))
                return new ResponseModel { Code = 0, Msg = "数据不正确" };

            Model.Entity.News dbNews = _context.News.Find(news.Id);
            if (dbNews == null)
                return new ResponseModel { Code = 0, Msg = "数据不正确" };

            dbNews.Title = news.Title;
            dbNews.Content = news.Content;
            dbNews.Image = news.Image;
            dbNews.Sort = news.Sort;
            dbNews.NewsClassifyId = news.NewsClassifyId;
            dbNews.PreviousUpdateTime = dbNews.LastUpdateTime;
            dbNews.LastUpdateTime = DateTime.Now;

            _context.News.Update(dbNews);
            int count = _context.SaveChanges();
            if (count > 0)
                return new ResponseModel { Code = 200, Msg = "新闻编辑成功" };
            else
                return new ResponseModel { Code = 0, Msg = "新闻编辑失败" };
        }

        /// <summary>
        /// News列表
        /// </summary>
        /// <returns></returns>
        public ResponseModel GetNewsList()
        {
            var newss = _context.News.Include("NewsClassify").Include("Comments").OrderByDescending(x => x.Createtime).ToList();
            var responseModel = new ResponseModel { Code = 200, Msg = "新闻查询成功", Data = new List<NewsModel>() };
            for (int i = 0; i < newss.Count; i++)
            {
                responseModel.Data.Add(new NewsModel
                {
                    Id = newss[i].Id,
                    Title = newss[i].Title,
                    Content = newss[i].Content.Length > 100 ? newss[i].Content.Substring(0, 100) : newss[i].Content,
                    Image = newss[i].Image,
                    Sort = newss[i].Sort,
                    Count = newss[i].Comments.Count,
                    Createtime = newss[i].Createtime.ToString("yyyy-MM-dd")
                });
            }
            return responseModel;
        }

        /// <summary>
        /// 最新评论新闻列表
        /// </summary>
        /// <returns></returns>
        public ResponseModel GetTopNewsList(Expression<Func<News.Model.Entity.News, bool>> where, int topCount)
        {
           // var comments = _context.Comment.GroupBy(x => x.NewsId).Select(g => new { NewsId = g.Key, Createtime = g.Max(x => x.Createtime) }).OrderByDescending(x => x.Createtime).Take(topCount).ToList();
            //var newsIds = comments.Select(s => s.NewsId).ToList();
            var newsIds = _context.Comment.OrderByDescending(c => c.Createtime).GroupBy(c => c.NewsId).Select(c => c.Key).Take(topCount).ToList();
            var newss = _context.News.Include("NewsClassify").Include("Comments").Where(c => newsIds.Contains(c.Id)).Where(where).OrderByDescending(c => c.Createtime).ToList();
            var responseModel = new ResponseModel { Code = 200, Msg = "新闻查询成功", Data = new List<NewsModel>() };
            for (int i = 0; i < newss.Count; i++)
            {
                responseModel.Data.Add(new NewsModel
                {
                    Id = newss[i].Id,
                    Title = newss[i].Title,
                    Content = newss[i].Content.Length > 100 ? newss[i].Content.Substring(0, 100) : newss[i].Content,
                    Image = newss[i].Image,
                    Sort = newss[i].Sort,
                    Count = newss[i].Comments.Count,
                    Createtime = newss[i].Createtime.ToString("yyyy-MM-dd")
                });
            }
            return responseModel;

            //return null;
        }

        /// <summary>
        /// 根据Id查询新闻
        /// </summary>
        /// <param name="NewsId"></param>
        /// <returns></returns>
        public ResponseModel GetNewsOne(long NewsId)
        {
            if (NewsId <= 0)
                return new ResponseModel { Code = 0, Msg = "数据不正确" };

            Model.Entity.News dbNews = _context.News.Include("NewsClassify").FirstOrDefault(x => x.Id == NewsId);
            if (dbNews == null)
                return new ResponseModel { Code = 0, Msg = "数据不正确" };

            var Newss = _context.News.ToList();
            var responseModel = new ResponseModel { Code = 200, Msg = "新闻查询成功", Data = dbNews };
            return responseModel;
        }

        /// <summary>
        /// 查询指定的News
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public News.Model.Entity.News GetOneNews(Expression<Func<News.Model.Entity.News, bool>> where)
        {
            return _context.News.FirstOrDefault(where);
        }
    }
}
