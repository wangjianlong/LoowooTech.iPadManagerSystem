using LoowooTech.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoowooTech.Managers
{
    public class LWSystemManager:ManagerBase
    {
        public int Add(LWSystem system)
        {
            DB.LWSystems.Add(system);
            DB.SaveChanges();
            return system.ID;
        }

        public bool Delete(int id)
        {
            var model = DB.LWSystems.Find(id);
            if (model != null)
            {
                model.Delete = true;
                DB.SaveChanges();
                return true;
            }

            return false;
        }

        public bool Edit(LWSystem system)
        {
            var entry = DB.LWSystems.Find(system.ID);
            if (entry == null)
            {
                return false;
            }
            DB.Entry(entry).CurrentValues.SetValues(system);
            DB.SaveChanges();
            return true;
        }

        /// <summary>
        /// 作用：获取未删除的子系统列表
        /// </summary>
        /// <returns></returns>
        public List<LWSystem> Get()
        {
            return DB.LWSystems.Where(e => e.Delete == false).OrderByDescending(e => e.Order).ToList();
        }

        /// <summary>
        /// 作用：获取上线子系统列表
        /// </summary>
        /// <returns></returns>
        public List<LWSystem> GetList()
        {
            return DB.LWSystems.Where(e => e.Delete == false&&e.IsOnline==true).OrderByDescending(e => e.Order).ToList();
        }

        public LWSystem Get(int id)
        {
            return DB.LWSystems.Find(id);
        }
    }
}
