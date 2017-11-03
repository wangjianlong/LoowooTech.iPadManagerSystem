using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace LoowooTech.Managers
{
    public static class OneContext
    {
        private static string _contextName { get; set; }
        
        public static LWDbContext Current
        {
            get { return HttpContext.Current.Items[_contextName] as LWDbContext; }
        }

        static OneContext()
        {
            _contextName = "LoowooTechContext";
        }

        public static void Begin()
        {
            HttpContext.Current.Items[_contextName] = new LWDbContext();
        }

        public static void End()
        {
            var entity = HttpContext.Current.Items[_contextName] as LWDbContext;
            if (entity != null)
            {
                entity.Dispose();
            }
        }
    }
}
