using LoowooTech.Models.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoowooTech.Managers.Admin
{
    public class FlowNodeManager:ManagerBase
    {
        public int Add(FlowNode node)
        {
            DB.FlowNodes.Add(node);
            DB.SaveChanges();
            return node.ID;
        }

        public bool Edit(FlowNode node)
        {
            var model = DB.FlowNodes.Find(node.ID);
            if (model == null)
            {
                return false;
            }
            DB.Entry(model).CurrentValues.SetValues(node);
            DB.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            var current = DB.FlowNodes.Find(id);
            if (current == null)
            {
                return false;
            }
            var next = DB.FlowNodes.FirstOrDefault(e => e.PrevId == id);
            if (next != null)
            {
                next.PrevId = current.PrevId;
            }
            DB.FlowNodes.Remove(current);
            DB.SaveChanges();
            return true;
        }

        public FlowNode Get(int id)
        {
            return DB.FlowNodes.Find(id);
        }

        /// <summary>
        /// 作用：上移
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Prev(int id)
        {
            var current = DB.FlowNodes.Find(id);
            if (current == null)
            {
                return false;
            }
            var prev = DB.FlowNodes.Find(current.PrevId);
            if (prev == null)
            {
                return false;
            }
            var next = DB.FlowNodes.FirstOrDefault(e => e.PrevId == id);

            current.PrevId = prev.PrevId;
            prev.PrevId = current.ID;
            if (next != null)
            {
                next.PrevId = prev.ID;
            }
            DB.SaveChanges();
            return true;
        }
        /// <summary>
        /// 作用：下移
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Later(int id)
        {
            var current = DB.FlowNodes.Find(id);
            if (current == null)
            {
                return false;
            }
            var next = DB.FlowNodes.FirstOrDefault(e => e.PrevId == id);
            if (next == null)
            {
                return false;
            }
            return Prev(next.ID);
        }



        public class Node
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public int PrevID { get; set; }
        }


        public void Function()
        {
            var a = new Node { ID = 1, Name = "a", PrevID = 0 };
            var b = new Node { ID = 2, Name = "b", PrevID = 1 };
            var c = new Node { ID = 3, Name = "c", PrevID = 2 };
            var d = new Node { ID = 4, Name = "d", PrevID = 3 };
        }
    }
}
