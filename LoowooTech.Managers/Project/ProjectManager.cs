using LoowooTech.Common;
using LoowooTech.Models.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoowooTech.Managers.Project
{
    public class ProjectManager:ManagerBase
    {
        public int Add(Models.Project.Project project)
        {
            DB.Projects.Add(project);
            DB.SaveChanges();
            return project.ID;
        }

        public bool Exist(string name)
        {
            return DB.Projects.Any(e => e.Name == name);
        }
        public bool Edit(Models.Project.Project project)
        {
            var model = DB.Projects.Find(project.ID);
            if (model == null)
            {
                return false;
            }
            DB.Entry(model).CurrentValues.SetValues(project);
            DB.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            var model = DB.Projects.Find(id);
            if (model == null)
            {
                return false;
            }
            model.Delete = true;
            DB.SaveChanges();
            return true;
        }

        public List<Models.Project.Project> Search(ProjectParameter parameter)
        {
            var query = DB.Projects.AsQueryable();
            if (parameter.UserId.HasValue)
            {
                query = query.Where(e => e.UserId == parameter.UserId.Value);
            }
            if (parameter.Delete.HasValue)
            {
                query = query.Where(e => e.Delete == parameter.Delete.Value);
            }
            if (!string.IsNullOrEmpty(parameter.Name))
            {
                query = query.Where(e => e.Name.Contains(parameter.Name));
            }

            if (parameter.IsDone.HasValue)
            {
                query = query.Where(e => e.IsDone == parameter.IsDone.Value);
            }

            switch (parameter.Order)
            {
                case Models.LWOrder.DeID:
                    query = query.OrderByDescending(e => e.ID);
                    break;
                case Models.LWOrder.DeTime:
                    query = query.OrderByDescending(e => e.CreateTime);
                    break;
                case Models.LWOrder.ID:
                    query = query.OrderBy(e => e.ID);
                    break;
                case Models.LWOrder.Time:
                    query = query.OrderBy(e => e.CreateTime);
                    break;
            }
            query = query.SetPage(parameter.Page);
            return query.ToList();
        }


        public Models.Project.Project Get(int id)
        {
            return DB.Projects.Find(id);
        }

        public void SaveProjectUser(List<ProjectUser> projectUsers,int projectId)
        {
            var old = DB.ProjectUsers.Where(e => e.ProjectId == projectId).ToList();
            foreach(var item in old)
            {
                var entry = projectUsers.FirstOrDefault(e => e.UserId == item.ID);
                if (entry == null)
                {
                    DB.ProjectUsers.Remove(item);
                }
            }
            DB.SaveChanges();
            foreach(var item in projectUsers)
            {
                var entry = DB.ProjectUsers.FirstOrDefault(e => e.ProjectId == projectId && e.UserId == item.UserId);
                if (entry == null)
                {
                    DB.ProjectUsers.Add(item);
                }
            }
            DB.SaveChanges();
        }

    }
}
