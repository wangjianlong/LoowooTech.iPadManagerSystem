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
        public List<Contract> Get(int projectId)
        {
            return DB.Contracts.Where(e => e.Delete == false && e.ProjectId == projectId).OrderBy(e => e.ID).ToList();
        }
    }
}
