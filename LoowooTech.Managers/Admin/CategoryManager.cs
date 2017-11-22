using LoowooTech.Models.Expense;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoowooTech.Managers.Admin
{
    public class CategoryManager:ManagerBase
    {
        public int Add(Category category)
        {
            DB.Categorys.Add(category);
            DB.SaveChanges();
            return category.ID;
        }

        public bool Edit(Category category)
        {
            var model = DB.Categorys.Find(category.ID);
            if (model == null)
            {
                return false;
            }
            DB.Entry(model).CurrentValues.SetValues(category);
            DB.SaveChanges();
            return true;
        }


        public bool Delete(int id)
        {
            var model = DB.Categorys.Find(id);
            if (model == null)
            {
                return false;
            }
            model.Delete = true;
            DB.SaveChanges();
            return true;
        }

        public Category Get(int id)
        {
            return DB.Categorys.Find(id);
        }

        public List<Category> GetTree()
        {
            var list = DB.Categorys.Where(e => e.Delete == false && e.CategoryId == 0).OrderBy(e => e.ID).ToList();
            foreach(var item in list)
            {
                item.Children = GetTree(item.ID);
            }

            return list;
        }
        public List<Category> GetTree(int id)
        {
            var list = DB.Categorys.Where(e => e.Delete == false && e.CategoryId == id).OrderBy(e => e.ID).ToList();
            foreach(var item in list)
            {
                item.Children = GetTree(item.ID);
            }
            return list;
        }
    }
}
