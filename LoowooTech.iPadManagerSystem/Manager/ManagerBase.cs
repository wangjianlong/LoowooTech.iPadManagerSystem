using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoowooTech.iPadManagerSystem.Manager
{
    public class ManagerBase
    {
        protected ManagerCore Core { get { return ManagerCore.Instance; } }
        protected DataContext GetDbContext()
        {
            return new DataContext();
        }

        protected DataContext DB
        {
            get
            {
                return HttpDbContextContainer.GetDbContext<DataContext>() ?? new DataContext();
            }
        }
    }
}