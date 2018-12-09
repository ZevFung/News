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

    public class NewsClassifyService
    {
        private NewsContext _context;
        public NewsClassifyService(NewsContext context)
        {
            _context = context;
        }

        /// <summary>
        /// 新增类别
        /// </summary>
        /// <param name="newsClassify"></param>
        /// <returns></returns>
        public ResponseModel AddNewsClassify(Model.Request.AddNewsClassify newsClassify)
        {
            if (newsClassify == null || string.IsNullOrWhiteSpace(newsClassify.Name))
                return new ResponseModel { Code = 0, Msg = "数据不正确" };

            Model.Entity.NewsClassify dbNewsClassify = new Model.Entity.NewsClassify()
            {
                Name = newsClassify.Name,
                Sort = newsClassify.Sort,
                Createtime = DateTime.Now,
                LastUpdateTime = DateTime.Now
            };
            _context.NewsClassify.Add(dbNewsClassify);
            int count = _context.SaveChanges();
            if (count > 0)
                return new ResponseModel { Code = 200, Msg = "类别添加成功" };
            else
                return new ResponseModel { Code = 0, Msg = "类别添加失败" };
        }

        /// <summary>
        /// 删除类别
        /// </summary>
        /// <param name="newsClassifyId"></param>
        /// <returns></returns>
        public ResponseModel DeleteNewsClassify(long newsClassifyId)
        {
            if (newsClassifyId <= 0)
                return new ResponseModel { Code = 0, Msg = "数据不正确" };

            Model.Entity.NewsClassify dbNewsClassify = _context.NewsClassify.Find(newsClassifyId);
            if (dbNewsClassify == null)
                return new ResponseModel { Code = 0, Msg = "数据不正确" };

            _context.NewsClassify.Remove(dbNewsClassify);
            int count = _context.SaveChanges();
            if (count > 0)
                return new ResponseModel { Code = 200, Msg = "类别删除成功" };
            else
                return new ResponseModel { Code = 0, Msg = "类别删除失败" };
        }

        /// <summary>
        /// 更新分类
        /// </summary>
        /// <param name="newsClassify"></param>
        /// <returns></returns>
        public ResponseModel EditNewsClassify(Model.Request.EditNewsClassify newsClassify)
        {
            if (newsClassify == null || newsClassify.Sort <= 0 || string.IsNullOrWhiteSpace(newsClassify.Name))
                return new ResponseModel { Code = 0, Msg = "数据不正确" };

            Model.Entity.NewsClassify dbNewsClassify = _context.NewsClassify.Find(newsClassify.Id);
            if (dbNewsClassify == null)
                return new ResponseModel { Code = 0, Msg = "数据不正确" };

            dbNewsClassify.Name = newsClassify.Name;
            dbNewsClassify.Sort = newsClassify.Sort;
            dbNewsClassify.PreviousUpdateTime = dbNewsClassify.LastUpdateTime;
            dbNewsClassify.LastUpdateTime = DateTime.Now;

            _context.NewsClassify.Update(dbNewsClassify);
            int count = _context.SaveChanges();
            if (count > 0)
                return new ResponseModel { Code = 200, Msg = "类别编辑成功" };
            else
                return new ResponseModel { Code = 0, Msg = "类别编辑失败" };
        }

        /// <summary>
        /// 查询newsClassify列表
        /// </summary>
        /// <returns></returns>
        public ResponseModel GetNewsClassifyList()
        {
            var newsClassifys = _context.NewsClassify.ToList();
            var responseModel = new ResponseModel { Code = 200, Msg = "类别查询成功", Data = new List<NewsClassifyModel>() };
            foreach (var item in newsClassifys)
            {
                responseModel.Data.Add(new NewsClassifyModel
                {
                    Id = item.Id,
                    Name = item.Name,
                    Sort = item.Sort
                });
            }
            return responseModel;
        }

        /// <summary>
        /// 根据Id查询分类
        /// </summary>
        /// <param name="newsClassifyId"></param>
        /// <returns></returns>
        public ResponseModel GetNewsClassifyOne(long newsClassifyId)
        {
            if (newsClassifyId <= 0)
                return new ResponseModel { Code = 0, Msg = "数据不正确" };

            Model.Entity.NewsClassify dbNewsClassify = _context.NewsClassify.Find(newsClassifyId);
            if (dbNewsClassify == null)
                return new ResponseModel { Code = 0, Msg = "数据不正确" };

            var newsClassifys = _context.NewsClassify.ToList();
            var responseModel = new ResponseModel { Code = 200, Msg = "类别查询成功", Data = dbNewsClassify };
            return responseModel;
        }

        /// <summary>
        /// 查询指定的一个newsClassify
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public NewsClassify GetOneNewsClassify(Expression<Func<NewsClassify, bool>> where)
        {
            return _context.NewsClassify.FirstOrDefault(where);
        }
    }
}
