using LoowooTech.iPadManagerSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoowooTech.iPadManagerSystem.Manager
{
    public class ColorManager:ManagerBase
    {
        public List<Color> GetList()
        {
            return DB.Colors.ToList();

        }

        public int Add(Color color)
        {
            DB.Colors.Add(color);
            DB.SaveChanges();
            return color.ID;
        }


        public bool Edit(Color color)
        {
            var entry = DB.Colors.Find(color.ID);
            if (entry != null)
            {
                DB.Entry(entry).CurrentValues.SetValues(color);
                DB.SaveChanges();
                return true;
            }
            return false;
        }

        public bool Exist(Color color)
        {
            return DB.Colors.Any(e => e.Name.ToLower() == color.Name.ToLower());
        }

        public bool Delete(int id)
        {
            var entry = DB.Colors.Find(id);
            if (entry == null)
            {
                return false;
            }
            DB.Colors.Remove(entry);
            DB.SaveChanges();
            return true;
        }
    }
}