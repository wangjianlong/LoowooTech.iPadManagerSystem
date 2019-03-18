using LoowooTech.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoowooTech.Offical.Web.Common
{
    public static class FilePost
    {
        private static string _filesDir = "FILES";

        private static string[] _pictures = { ".bmp", ".png", ".jpg", ".jpeg", ".tiff" };
        private static string[] _atlas = { ".apk", ".ipa" };

        public static bool IsPictures(string ext)
        {
            return _pictures.Contains(ext);
        }

        public static bool IsAtlas(string ext)
        {
            return _atlas.Contains(ext);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="file"></param>
        /// <param name="folder"></param>
        /// <param name="userId"></param>
        /// <param name="flag">true：如果服务器中存在相同的文件，则会覆盖替换文件  false: 存在相同文件，会保留原始的文件  </param>
        /// <returns></returns>
        public static FileInfo Upload(HttpPostedFileBase file,string folder,int userId,bool flag=false)
        {
            if (file.ContentLength == 0) return null;
            var currentDir = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _filesDir, folder,DateTime.Now.ToLongDateString());
            if (!System.IO.Directory.Exists(currentDir))
            {
                System.IO.Directory.CreateDirectory(currentDir);
            }
            var saveFile = currentDir +"\\"+ file.FileName;
            //var saveFile = System.IO.Path.Combine(currentDir, file.FileName);
            if (System.IO.File.Exists(saveFile))
            {
                if (flag == true)
                {
                    System.IO.File.Delete(saveFile);
                }
                else
                {
                    var newfile = System.IO.Path.GetFileNameWithoutExtension(saveFile) + "-" + DateTime.Now.Ticks.ToString();
                    if (newfile.Length > 256)
                    {
                        newfile = newfile.Substring(255);
                    }
                    newfile = currentDir+"\\"+ newfile + System.IO.Path.GetExtension(saveFile);
                    saveFile = newfile;
                }
               
            }
            file.SaveAs(saveFile);
            var path = saveFile.Replace(AppDomain.CurrentDomain.BaseDirectory, "");
            try
            {
                System.IO.FileInfo info = new System.IO.FileInfo(saveFile);
                return new FileInfo
                {
                    UserId = userId,
                    FileName = file.FileName,
                    Path = path,
                    Size = info.Length
                };
            }
            catch
            {

            }
            return new FileInfo
            {
                UserId = userId,
                FileName = file.FileName,
                Path = path
            };
            
        }
    }
}