using LoowooTech.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoowooTech.Managers.Admin
{
    public class GroupManager:ManagerBase
    {
        public int Add(Group group)
        {
            DB.Groups.Add(group);
            DB.SaveChanges();
            return group.ID;
        }

        public bool Edit(Group group)
        {
            var model = DB.Groups.Find(group.ID);
            if (model == null)
            {
                return false;
            }
            DB.Entry(model).CurrentValues.SetValues(group);
            DB.SaveChanges();

            return true;
        }

        public Group Get(int id)
        {
            return DB.Groups.Find(id);
        }

        public bool Delete(int id)
        {
            var model = DB.Groups.Find(id);
            if (model == null)
            {
                return false;
            }
            model.Delete = true;
            DB.SaveChanges();
            return true;
        }

        public List<Group> GetList()
        {
            return DB.Groups.Where(e => e.Delete == false).OrderBy(e => e.Order).ToList();
        }
    }
}
