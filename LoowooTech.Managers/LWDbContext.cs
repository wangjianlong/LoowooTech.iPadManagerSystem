using LoowooTech.Models;
using LoowooTech.Models.Admin;
using LoowooTech.Models.Expense;
using System.Data.Entity;

namespace LoowooTech.Managers
{
    public class LWDbContext:DbContext
    {
        public LWDbContext() : base("name=DbConnection")
        {
            Database.SetInitializer<LWDbContext>(null);
        }


        public DbSet<User> Users { get; set; }
        public DbSet<LWSystem> LWSystems { get; set; }
        public DbSet<Power> Powers { get; set; }
        public DbSet<PowerItem> Items { get; set; }

        #region 便签
        public DbSet<Note> Notes { get; set; }
        #endregion

        #region  联系人
        public DbSet<Contact> Contacts { get; set; }//联系人
        #endregion

        #region  管理员
        public DbSet<Group> Groups { get; set; }//
        public DbSet<City> Citys { get; set; }//城市
        public DbSet<Company> Companys { get; set; }//单位  公司
        public DbSet<ProjectType> ProjectTypes { get; set; }//项目类型
        public DbSet<Flow> Flows { get; set; }//流程
        public DbSet<FlowNode> FlowNodes { get; set; }//流程节点
        public DbSet<Category> Categorys { get; set; }//种类


        #endregion

        #region  项目

        public DbSet<Models.Project.Project> Projects { get; set; }
        #endregion

        #region  报销
        public DbSet<Sheet> Sheets { get; set; }
        #endregion
    }
}
