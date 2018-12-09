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

    public class BannerService
    {
        private NewsContext _context;
        public BannerService(NewsContext context)
        {
            _context = context;
        }

        /// <summary>
        /// 新增banner
        /// </summary>
        /// <param name="banner"></param>
        /// <returns></returns>
        public ResponseModel AddBanner(Model.Request.AddBanner banner)
        {
            if (banner == null || string.IsNullOrWhiteSpace(banner.Title) || string.IsNullOrWhiteSpace(banner.Image) || string.IsNullOrWhiteSpace(banner.Url))
                return new ResponseModel { Code = 0, Msg = "数据不正确" };

            Model.Entity.Banner dbBanner = new Model.Entity.Banner()
            {
                Image = banner.Image,
                Title = banner.Title,
                Url = banner.Url,
                Remark = banner.Remark,
                Createtime = DateTime.Now,
                LastUpdateTime = DateTime.Now
            };
            _context.Banner.Add(dbBanner);
            int count = _context.SaveChanges();
            if (count > 0)
                return new ResponseModel { Code = 200, Msg = "banner添加成功" };
            else
                return new ResponseModel { Code = 0, Msg = "banner添加失败" };
        }

        /// <summary>
        /// 删除banner
        /// </summary>
        /// <param name="bannerId"></param>
        /// <returns></returns>
        public ResponseModel DeleteBanner(long bannerId)
        {
            if (bannerId <= 0)
                return new ResponseModel { Code = 0, Msg = "数据不正确" };

            Model.Entity.Banner dbBanner = _context.Banner.Find(bannerId);
            if (dbBanner == null)
                return new ResponseModel { Code = 0, Msg = "数据不正确" };

            _context.Banner.Remove(dbBanner);
            int count = _context.SaveChanges();
            if (count > 0)
                return new ResponseModel { Code = 200, Msg = "banner删除成功" };
            else
                return new ResponseModel { Code = 0, Msg = "banner删除失败" };
        }

        /// <summary>
        /// 更新banner
        /// </summary>
        /// <param name="banner"></param>
        /// <returns></returns>
        public ResponseModel EditBanner(Model.Request.EditBanner banner)
        {
            if (banner == null || banner.Id <= 0 || string.IsNullOrWhiteSpace(banner.Title) || string.IsNullOrWhiteSpace(banner.Image) || string.IsNullOrWhiteSpace(banner.Url))
                return new ResponseModel { Code = 0, Msg = "数据不正确" };

            Model.Entity.Banner dbBanner = _context.Banner.Find(banner.Id);
            if (dbBanner == null)
                return new ResponseModel { Code = 0, Msg = "数据不正确" };

            dbBanner.Image = banner.Image;
            dbBanner.Title = banner.Title;
            dbBanner.Url = banner.Url;
            dbBanner.Remark = banner.Remark;
            dbBanner.PreviousUpdateTime = dbBanner.LastUpdateTime;
            dbBanner.LastUpdateTime = DateTime.Now;

            _context.Banner.Update(dbBanner);
            int count = _context.SaveChanges();
            if (count > 0)
                return new ResponseModel { Code = 200, Msg = "banner编辑成功" };
            else
                return new ResponseModel { Code = 0, Msg = "banner编辑失败" };
        }

        /// <summary>
        /// 查询banner列表
        /// </summary>
        /// <returns></returns>
        public ResponseModel GetBannerList()
        {
            var banners = _context.Banner.ToList();
            var responseModel = new ResponseModel { Code = 200, Msg = "banner查询成功", Data = new List<BannerModel>() };
            foreach (var item in banners)
            {
                responseModel.Data.Add(new BannerModel
                {
                    Id = item.Id,
                    Title=item.Title,
                    Image = item.Image,
                    Url = item.Url,
                    Remark = item.Remark
                });
            }
            return responseModel;
        }

        /// <summary>
        /// 根据Id查询banner
        /// </summary>
        /// <param name="bannerId"></param>
        /// <returns></returns>
        public ResponseModel GetBannerOne(long bannerId)
        {
            if (bannerId <= 0)
                return new ResponseModel { Code = 0, Msg = "数据不正确" };

            Model.Entity.Banner dbBanner = _context.Banner.Find(bannerId);
            if (dbBanner == null)
                return new ResponseModel { Code = 0, Msg = "数据不正确" };

            var banners = _context.Banner.ToList();
            var responseModel = new ResponseModel { Code = 200, Msg = "banner查询成功", Data = dbBanner };
            return responseModel;
        }

        /// <summary>
        /// 查询指定的一个banner
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public Banner GetOneBanner(Expression<Func<Banner, bool>> where)
        {
            return _context.Banner.FirstOrDefault(where);
        }
    }
}
