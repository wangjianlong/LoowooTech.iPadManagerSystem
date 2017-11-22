using LoowooTech.Models.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoowooTech.Managers.Project
{
    public class ProjectTypeManager:ManagerBase
    {
        public int Add(ProjectType projectType)
        {
            DB.ProjectTypes.Add(projectType);
            DB.SaveChanges();
            return projectType.ID;
        }

        public bool Edit(ProjectType projectType)
        {
            var model = DB.ProjectTypes.Find(projectType.ID);
            if (model == null)
            {
                return false;
            }
            DB.Entry(model).CurrentValues.SetValues(projectType);
            DB.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            var model = DB.ProjectTypes.Find(id);
            if (model == null)
            {
                return false;
            }
            model.Delete = true;
            DB.SaveChanges();
            return true;
        }

        public ProjectType Get(int id)
        {
            return DB.ProjectTypes.Find(id);
        }

        public List<ProjectType> GetTree()
        {
            var list = DB.ProjectTypes.Where(e => e.Delete == false&&e.ParentId==null).OrderBy(e => e.ID).ToList();
            foreach(var item in list)
            {
                item.Children = GetTree(item.ID);
            }
            return list;
        }

        public List<ProjectType> GetTree(int parentId)
        {
            var list = DB.ProjectTypes.Where(e => e.Delete == false && e.ParentId.HasValue && e.ParentId.Value == parentId).OrderBy(e => e.ID).ToList();
            foreach(var item in list)
            {
                item.Children = GetTree(item.ID);
            }

            return list;
        }
    }
}
