using LoowooTech.Models.Tablets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoowooTech.Managers.Tablets
{
    public class AuthorityManager:ManagerBase
    {

        public void AddRange(List<Authority> list)
        {
            DB.Authorities.AddRange(list);
            DB.SaveChanges();
        }
        public List<Authority> Search(AuthorityParameter parameter)
        {
            var query = DB.Authorities.AsQueryable();
            if (parameter.StartTime.HasValue)
            {
                query = query.Where(e => e.Time >= parameter.StartTime.Value);
            }
            if (parameter.EndTime.HasValue)
            {
                query = query.Where(e => e.Time <= parameter.EndTime.Value);
            }
            if (string.IsNullOrEmpty(parameter.Name) == false)
            {
                query = query.Where(e => e.Name.Contains(parameter.Name));
            }
            if (parameter.Delete.HasValue)
            {
                query = query.Where(e => e.Delete == parameter.Delete.Value);
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
