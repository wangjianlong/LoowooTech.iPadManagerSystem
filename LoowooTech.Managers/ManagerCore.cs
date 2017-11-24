using LoowooTech.Managers.Admin;
using LoowooTech.Managers.Expense;
using LoowooTech.Managers.Project;

namespace LoowooTech.Managers
{
    public class ManagerCore
    {
        public static readonly ManagerCore Instance = new ManagerCore();
        private UserManager _userManager { get; set; }
        public UserManager UserManager { get { return _userManager == null ? _userManager = new UserManager() : _userManager; } }
        private LWSystemManager _lwSystemManager { get; set; }
        public LWSystemManager LWSystemManager { get { return _lwSystemManager == null ? _lwSystemManager = new LWSystemManager() : _lwSystemManager; } }

        #region  便签
        private NoteManager _noteManager { get; set; }
        public NoteManager NoteManager { get { return _noteManager == null ? _noteManager = new NoteManager() : _noteManager; } }
        #endregion

        #region 联系人
        private ContactManager _contactManager { get; set; }
        public ContactManager ContactManager { get { return _contactManager == null ? _contactManager = new ContactManager() : _contactManager; } }
        #endregion

        #region  管理员
        private GroupManager _groupManager { get; set; }// 部门/组  
        public GroupManager GroupManager { get { return _groupManager == null ? _groupManager = new GroupManager() : _groupManager; } }
        private CityManager _cityManager { get; set; }//城市列表
        public CityManager CityManager { get { return _cityManager == null ? _cityManager = new CityManager() : _cityManager; } }
        private CompanyManager _companyManager { get; set; }//单位  公司
        public CompanyManager CompanyManager { get { return _companyManager == null ? _companyManager = new CompanyManager() : _companyManager; } }
        private ProjectTypeManager _projectTypeManager { get; set; }//项目类型
        public ProjectTypeManager ProjectTypeManager { get { return _projectTypeManager == null ? _projectTypeManager = new ProjectTypeManager() : _projectTypeManager; } }
        private FlowManager _flowManager { get; set; }//流程
        public FlowManager FlowManager { get { return _flowManager == null ? _flowManager = new FlowManager() : _flowManager; } }
        private FlowNodeManager _flowNodeManager { get; set; }//流程节点
        public FlowNodeManager FlowNodeManager { get { return _flowNodeManager == null ? _flowNodeManager = new FlowNodeManager() : _flowNodeManager; } }
        private CategoryManager _categoryManager { get; set; }//种类
        public CategoryManager CategoryManager { get { return _categoryManager == null ? _categoryManager = new CategoryManager() : _categoryManager; } }

        #endregion

        #region 项目
        private ProjectManager _projectManager { get; set; }
        public ProjectManager ProjectManager { get { return _projectManager == null ? _projectManager = new ProjectManager() : _projectManager; } }
        #endregion

        #region 报销
        private SheetManager _sheetManager { get; set; }
        public SheetManager SheetManager { get { return _sheetManager == null ? _sheetManager = new SheetManager() : _sheetManager; } }
        #endregion
    }
}
