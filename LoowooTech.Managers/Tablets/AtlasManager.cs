using LoowooTech.Models.Tablets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoowooTech.Managers.Tablets
{
    public class AtlasManager:ManagerBase
    {
        public Atlas Get(int id)
        {
            return DB.Atlas.Find(id);
        }
        public int Add(Atlas atlas)
        {
            DB.Atlas.Add(atlas);
            DB.SaveChanges();
            return atlas.ID;
        }

        public bool Edit(Atlas atlas)
        {
            var model = DB.Atlas.Find(atlas.ID);
            if (model == null)
            {
                return false;
            }
            DB.Entry(model).CurrentValues.SetValues(atlas);
            DB.SaveChanges();

            return true;
        }
        public bool Delete(int id,bool flag=true)
        {
            var model = DB.Atlas.Find(id);
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
