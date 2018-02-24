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
    }
}
