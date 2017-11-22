using LoowooTech.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoowooTech.Managers
{
    public class NoteManager:ManagerBase
    {
        public List<Note> GetList(int userId)
        {
            return DB.Notes.Where(e => e.Delete == false && e.UserId == userId).OrderByDescending(e => e.Time).ToList();
        }

        public Note Get(int id)
        {
            return DB.Notes.Find(id);
        }

        public int Add(Note note)
        {
            DB.Notes.Add(note);
            DB.SaveChanges();
            return note.ID;
        }

        public bool Delete(int id)
        {
            var model = DB.Notes.Find(id);
            if (model == null)
            {
                return false;
            }
            model.Delete = true;
            DB.SaveChanges();
            return true;
        }

        public bool Edit(Note note)
        {
            var model = DB.Notes.Find(note.ID);
            if (model == null)
            {
                return false;
            }
            DB.Entry(model).CurrentValues.SetValues(note);
            DB.SaveChanges();
            return true;
        }
    }
}
