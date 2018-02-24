using LoowooTech.Models.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoowooTech.Managers.Project
{
    public class WorkScheduleManager:ManagerBase
    {
        public WorkSchedule Get(int id)
        {
            return DB.Schedules.Find(id);
        }

        public int Add(WorkSchedule schedule)
        {
            DB.Schedules.Add(schedule);
            DB.SaveChanges();
            return schedule.ID;
        }

        public bool Edit(WorkSchedule schedule)
        {
            var model = DB.Schedules.Find(schedule.ID);
            if (model == null)
            {
                return false;
            }

            DB.Entry(model).CurrentValues.SetValues(schedule);
            DB.SaveChanges();
            return true;
        }

        public bool Delete(int id,bool flag)
        {
            var model = DB.Schedules.Find(id);
            if (model == null)
            {
                return false;
            }

            model.Delete = flag;
            DB.SaveChanges();
            return true;
        }
    }
}
