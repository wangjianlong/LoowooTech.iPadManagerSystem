using LoowooTech.Models.Voucher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoowooTech.Managers.Voucher
{
    public class EInvoiceManager:ManagerBase
    {
        public EInvoice Get(int id)
        {
            return DB.EInvoices.Find(id);
        }

        public bool Edit(EInvoice invoice)
        {
            var model = DB.EInvoices.Find(invoice.ID);
            if (model == null)
            {
                return false;
            }
            DB.Entry(model).CurrentValues.SetValues(invoice);
            DB.SaveChanges();

            return true;
        }


        public bool Delete(int id)
        {
            var model = DB.EInvoices.Find(id);
            if (model == null)
            {
                return false;
            }
            model.Delete = true;
            DB.SaveChanges();
            return true;
        }
    }
}
