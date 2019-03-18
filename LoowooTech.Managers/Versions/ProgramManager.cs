using LoowooTech.Models.Versions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoowooTech.Managers.Versions
{
    public class ProgramManager:ManagerBase
    {
        public int Add(Program program)
        {
            DB.Programs.Add(program);
            DB.SaveChanges();
            return program.ID;
        }


        public Program Get(int id)
        {
            return DB.Programs.Find(id);
        }

        public bool Edit(Program program)
        {
            var model = DB.Programs.Find(program.ID);
            if (model == null)
            {
                return false;
            }
            DB.Entry(model).CurrentValues.SetValues(program);
            DB.SaveChanges();
            return true;
        }

        public bool Delete(int id,bool flag=true)
        {
            var model = DB.Programs.Find(id);
            if (model == null)
            {
                return false;
            }
            model.Delete = flag;
            DB.SaveChanges();
            return true;
        }

        public List<Program> Search(ProgramParameter parameter)
        {
            var query = DB.Programs.AsQueryable();
            if (string.IsNullOrEmpty(parameter.Name) == false)
            {
                query = query.Where(e => e.Name.Contains(parameter.Name));
            }
            if (parameter.Delete.HasValue)
            {
                query = query.Where(e => e.Delete == parameter.Delete.Value);
            }

            if (parameter.UserId.HasValue)
            {
                query = query.Where(e => e.UserId == parameter.UserId.Value);
            }

            query = query.OrderByDescending(e => e.Time).SetPage(parameter.Page);

            return query.ToList();
        }

        public void AddFiles(List<ProgramFile> files)
        {
            DB.Program_Files.AddRange(files);
            DB.SaveChanges();
        }
        public bool DeleteFile(int id)
        {
            var model = DB.Program_Files.Find(id);
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
