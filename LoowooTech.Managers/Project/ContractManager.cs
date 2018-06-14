using LoowooTech.Models.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoowooTech.Managers.Project
{
    public class ContractManager:ManagerBase
    {
        public List<Contract> GetList(int projectId)
        {
            return DB.Contracts.Where(e => e.Delete == false && e.ProjectId == projectId).OrderBy(e => e.ID).ToList();
        }

        public int Add(Contract contract)
        {
            DB.Contracts.Add(contract);
            DB.SaveChanges();
            return contract.ID;
        }

        public bool Edit(Contract contract)
        {
            var model = DB.Contracts.Find(contract.ID);
            if (model == null)
            {
                return false;
            }
            DB.Entry(model).CurrentValues.SetValues(contract);
            DB.SaveChanges();

            return true;
        }

        public bool Delete(int id,bool flag=true)
        {
            var model = DB.Contracts.Find(id);
            if (model == null)
            {
                return false;
            }
            model.Delete = flag;
            DB.SaveChanges();
            return true;
        }


        public void AddPayInfo(List<PayInfo> infos,int contractId)
        {
            var old = DB.PayInfos.Where(e => e.ContractId == contractId).ToList();
            if (old.Count > 0)
            {
                DB.PayInfos.RemoveRange(old);
                DB.SaveChanges();
            }
            DB.PayInfos.AddRange(infos);
            DB.SaveChanges();
        }
    }
}
