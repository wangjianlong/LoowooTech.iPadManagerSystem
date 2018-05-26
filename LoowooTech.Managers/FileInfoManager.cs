using LoowooTech.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoowooTech.Managers
{
    public class FileInfoManager:ManagerBase
    {
        public int Add(FileInfo info)
        {
            DB.Files.Add(info);
            DB.SaveChanges();
            return info.ID;
        }
    }
}
