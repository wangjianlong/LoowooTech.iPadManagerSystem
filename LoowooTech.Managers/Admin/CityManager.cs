using LoowooTech.Models.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoowooTech.Managers.Admin
{
    public class CityManager:ManagerBase
    {
        public int Add(City city)
        {
            DB.Citys.Add(city);
            DB.SaveChanges();
            return city.ID;
        }

        public bool Edit(City city)
        {
            var model = DB.Citys.Find(city.ID);
            if (model == null)
            {
                return false;
            }
            DB.Entry(model).CurrentValues.SetValues(city);
            DB.SaveChanges();
            return true;
        }


        public bool Delete(int id)
        {
            var model = DB.Citys.Find(id);
            if (model == null)
            {
                return false;
            }
            model.Delete = true;
            DB.SaveChanges();
            return true;
        }


        public City Get(int id)
        {
            var city= DB.Citys.Find(id);
            if (city != null && city.ParentId.HasValue)
            {
                city.Parent = Get(city.ParentId.Value);
            }
            return city;
        }
        public List<City> GetList(int[] Ids)
        {
            var list = new List<City>();
            foreach(var id in Ids)
            {
                var model = Get(id);
                list.Add(Get(id));
            }
            return list;
        }

        /// <summary>
        /// 作用：获取一级城市级别
        /// </summary>
        /// <returns></returns>
        public List<City> GetList()
        {
            return DB.Citys.Where(e => e.Delete == false&&e.ParentId==null).OrderBy(e => e.ID).ToList();
        }


        public List<City> GetTree()
        {
            var list = DB.Citys.Where(e => e.Delete == false && e.ParentId == null).OrderBy(e => e.Code).ToList();
            foreach(var item in list)
            {
                item.Children = GetTree(item.ID);
            }
            return list;
        }
        public List<City> GetTree(int id)
        {
            var list = DB.Citys.Where(e => e.Delete == false && e.ParentId == id).OrderBy(e => e.Code).ToList();
            foreach(var item in list)
            {
                item.Children = GetTree(item.ID);
            }
            return list;
        }
    }
}
