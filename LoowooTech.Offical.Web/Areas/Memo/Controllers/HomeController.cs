using LoowooTech.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoowooTech.Offical.Web.Areas.Memo.Controllers
{
    public class HomeController : MemoControllerBase
    {
        // GET: Memo/Home
        public ActionResult Index()
        {
            var list = Core.NoteManager.GetList(Identity.UserId);
            ViewBag.List = list;
            return View();
        }

        public ActionResult Create(int id = 0)
        {
            var note = Core.NoteManager.Get(id);
            ViewBag.Note = note;
            return View();
        }

        [HttpPost]
        public ActionResult Save(Note note)
        {
            if (note.ID > 0)
            {
                if (!Core.NoteManager.Edit(note))
                {
                    return ErrorJsonResult("编辑失败！未找到相关编辑信息");
                }
            }
            else
            {
                var id = Core.NoteManager.Add(note);
                if (id <= 0)
                {
                    return ErrorJsonResult("新建便签失败！");
                }
            }
            return SuccessJsonResult();
        }

        public ActionResult Delete(int id)
        {
            if (!Core.NoteManager.Delete(id))
            {
                return ErrorJsonResult("删除失败，未找到相关便签信息！");
            }
            return SuccessJsonResult();
        }
    }
}