using LoowooTech.Models.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoowooTech.Managers.Admin
{
    public class FlowManager:ManagerBase
    {
        public int Add(Flow flow)
        {
            DB.Flows.Add(flow);
            DB.SaveChanges();
            return flow.ID;
        }

        public bool Edit(Flow flow)
        {
            var model = DB.Flows.Find(flow.ID);
            if (model == null)
            {
                return false;
            }
            DB.Entry(model).CurrentValues.SetValues(flow);
            DB.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            var model = DB.Flows.Find(id);
            if (model == null)
            {
                return false;
            }
            model.Delete = true;
            DB.SaveChanges();
            return true;
        }

        public Flow Get(int id)
        {
            return DB.Flows.Find(id);
        }

        public List<Flow> GetList()
        {
            return DB.Flows.Where(e => e.Delete == false).OrderBy(e => e.ID).ToList();
        }
        
    }
}
