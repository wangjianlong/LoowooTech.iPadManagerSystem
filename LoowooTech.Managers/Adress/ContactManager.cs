using LoowooTech.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoowooTech.Managers
{
    public class ContactManager:ManagerBase
    {
        public int Add(Contact contact)
        {
            DB.Contacts.Add(contact);
            DB.SaveChanges();
            return contact.ID;
        }
        public bool Edit(Contact contact)
        {
            var model = DB.Contacts.Find(contact.ID);
            if (model == null)
            {
                return false;
            }
            DB.Entry(model).CurrentValues.SetValues(contact);
            DB.SaveChanges();
            return true;
        }
        public bool Delete(int id)
        {
            var model = DB.Contacts.Find(id);
            if (model == null)
            {
                return false;
            }
            model.Delete = true;
            DB.SaveChanges();
            return true;
        }

        public List<Contact> GetList()
        {
            return DB.Contacts.Where(e => e.Delete == false).OrderByDescending(e => e.Time).ToList();
        }

        public Contact Get(int id)
        {
            return DB.Contacts.Find(id);
        }
    }
}
