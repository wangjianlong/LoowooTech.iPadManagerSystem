using LoowooTech.Models.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoowooTech.Managers.Project
{
    public class InvoiceManager:ManagerBase
    {
        public Invoice Get(int id)
        {
            return DB.Invoices.Find(id);
        }

        public int Add(Invoice invoice)
        {
            DB.Invoices.Add(invoice);
            DB.SaveChanges();
            return invoice.ID;
        }

        public bool Edit(Invoice invoice)
        {
            var model = DB.Invoices.Find(invoice.ID);
            if (model == null)
            {
                return false;
            }
            DB.Entry(model).CurrentValues.SetValues(invoice);
            DB.SaveChanges();
            return true;
        }

        public List<Invoice> GetList(int projectId)
        {
            return DB.Invoices.Where(e => e.Delete == false && e.ProjectId == projectId).ToList();
        }
    }
}
