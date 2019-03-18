using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoowooTech.Managers.Versions
{
    public class VersionManager:ManagerBase
    {
        public int Add(Models.Versions.Version version)
        {
            DB.Versions.Add(version);
            DB.SaveChanges();
            return version.ID;
        }

        public bool Delete(int id,bool flag=true)
        {
            var model = DB.Versions.Find(id);
            if (model == null)
            {
                return false;
            }
            model.Delete = flag;
            DB.SaveChanges();
            return true;
        }

        public List<Models.Versions.Version> Search(Models.Versions.VersionParameter parameter)
        {
            var query = DB.Versions.AsQueryable();
            if (parameter.ProgramId.HasValue)
            {
                query = query.Where(e => e.ProgramId == parameter.ProgramId.Value);
            }

            if (parameter.UserId.HasValue)
            {
                query = query.Where(e => e.UserId == parameter.UserId.Value);
            }

            query = query.OrderByDescending(e => e.Time).SetPage(parameter.Page);
            return query.ToList();
        }
        
    }
}
