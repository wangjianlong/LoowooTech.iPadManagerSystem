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
            
            return project.ID;
        }
    }
}
