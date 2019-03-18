using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoowooTech.Common
{
    public static class AtlasExtension
    {
        public static void Save(string folder,string name,string number,string fullPath)
        {
            var fileName = System.IO.Path.GetFileName(fullPath);//文件名
            var newPath = System.IO.Path.Combine(folder, fileName);//新保存的程序路径
            if (System.IO.File.Exists(newPath))
            {
                System.IO.File.Delete(newPath);
            }
            System.IO.File.Copy(fullPath, newPath);

            var plist = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "manifest.plist");
            var newplist = System.IO.Path.Combine(folder, string.Format("{0}.plist", name));
            using (var fs1=new FileStream(newplist, FileMode.OpenOrCreate, FileAccess.Write))
            {
                using (var sr1=new StreamWriter(fs1))
                {
                    using (var fs = new FileStream(plist, FileMode.Open, FileAccess.Read))
                    {
                        using (var sr = new StreamReader(fs))
                        {
                            var str = sr.ReadLine();
                            while(str != null)
                            {
                                str = str.Replace("{NAME}", fileName).Replace("{NUMBER}", number);
                                sr1.WriteLine(str);
                                str = sr.ReadLine();
                            }
                           
                           
                        }
                    }
                }
               
            }
           

        }
    }
}
