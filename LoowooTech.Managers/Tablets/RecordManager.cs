using LoowooTech.Models.Tablets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoowooTech.Managers.Tablets
{
    public class RecordManager:ManagerBase
    {
        public Record Get(int id)
        {
            return DB.Records.Find(id);
        }
        public Revert GetRevert(int id)
        {
            return DB.Reverts.Find(id);
        }

        public bool Delete(int id,bool flag = true)
        {
            var model = DB.Records.Find(id);
            if (model == null)
            {
                return false;
            }
            model.Delete = flag;
            DB.SaveChanges();

            return true;
        }

        public bool Edit(Record record)
        {
            var model = DB.Records.Find(record.ID);
            if (model == null)
            {
                return false;
            }
            DB.Entry(model).CurrentValues.SetValues(record);
            DB.SaveChanges();
            return true;
        }

        public int Add(Record record)
        {
            DB.Records.Add(record);
            DB.SaveChanges();
            return record.ID;
        }

        public int Add(Revert revert)
        {
            DB.Reverts.Add(revert);
            DB.SaveChanges();
            return revert.ID;
        }

        public void Revert(int recordId,int[] tabletIds,int revertId)
        {
            var list = DB.TabletRecords.Where(e => e.RecordId == recordId && e.RevertId == null);
            foreach(var item in tabletIds)
            {
                var entry = list.FirstOrDefault(e => e.TabletId == item);
                if (entry != null)
                {
                    entry.RevertId = revertId;
                }
            }
            DB.SaveChanges();
        }
        public List<Record> Search(RecordParameter parameter)
        {
            var query = DB.Records.AsQueryable();
            if (parameter.ProjectId.HasValue)
            {
                query = query.Where(e => e.ProjectId == parameter.ProjectId.Value);
            }
            if (parameter.CityId.HasValue)
            {
                query = query.Where(e => e.CityId == parameter.CityId.Value);
            }
            query = query.OrderByDescending(e => e.Time).SetPage(parameter.Page);
            return query.ToList();
        }

        public void UpdateTablets(List<TabletRecord> list,int recordId)
        {
            var old = DB.TabletRecords.Where(e => e.RecordId == recordId).ToList();
            var removes = new List<TabletRecord>();
            foreach (var item in old)
            {
                var entry = list.FirstOrDefault(e => e.TabletId == item.TabletId);
                if (entry != null)
                {
                    list.Remove(entry);
                }
                else
                {
                    removes.Add(item);
                }
            }

            DB.TabletRecords.RemoveRange(removes);

            DB.TabletRecords.AddRange(list);
            DB.SaveChanges();
   
        }

        public bool UpdateFile(int id,int fileId)
        {
            var model = DB.Records.Find(id);
            if (model == null)
            {
                return false;
            }
            model.FileId = fileId;
            DB.SaveChanges();
            return true;
        }
    }
}
