using LoowooTech.Models.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoowooTech.Offical.Web.Areas.Admin.Controllers
{
    public class CityController : AdminControllerBase
    {
        // GET: Admin/City
        public ActionResult Index()
        {
            var tree = Core.CityManager.GetTree();
            ViewBag.Tree = tree;
            return View();
        }

        public ActionResult Create(int id = 0,int cityId=0)
        {
            var city = Core.CityManager.Get(id);
            ViewBag.City = city;
            ViewBag.Parent = Core.CityManager.Get(cityId);
            return View();
        }

        public ActionResult Save(City city)
        {
            if (city.ID > 0)
            {
                if (!Core.CityManager.Edit(city))
                {
                    return ErrorJsonResult("编辑失败，未找到需要编辑的城市信息！");
                }
            }
            else
            {
                var id = Core.CityManager.Add(city);
                if (id <= 0)
                {
                    return ErrorJsonResult("添加城市失败！");
                }
            }
            return SuccessJsonResult();
        }

        public ActionResult Delete(int id)
        {
            if (!Core.CityManager.Delete(id))
            {
                return ErrorJsonResult("删除城市失败！未找到相关诚信信息！");
            }
            return SuccessJsonResult();
        }


   
  
    }
}