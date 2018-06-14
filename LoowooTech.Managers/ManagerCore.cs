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
        private UserCompanyManager _userCompanyManager { get; set; }//用户可查看企业相关
        public UserCompanyManager UserCompanyManager { get { return _userCompanyManager == null ? _userCompanyManager = new UserCompanyManager() : _userCompanyManager; } }
        private LWSystemManager _lwSystemManager { get; set; }
        public LWSystemManager LWSystemManager { get { return _lwSystemManager == null ? _lwSystemManager = new LWSystemManager() : _lwSystemManager; } }
        private FileInfoManager _fileInfoManager { get; set; }
        public FileInfoManager FileInfoManager { get { return _fileInfoManager == null ? _fileInfoManager = new FileInfoManager() : _fileInfoManager; } }

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
        private FlowDataManager _flowDataManager { get; set; }
        public FlowDataManager FlowDataManager { get { return _flowDataManager == null ? _flowDataManager = new FlowDataManager() : _flowDataManager; } }
        private FlowNodeDataManager _flowNodeDataManager { get; set; }
        public FlowNodeDataManager FlowNodeDataManager { get { return _flowNodeDataManager == null ? _flowNodeDataManager = new FlowNodeDataManager() : _flowNodeDataManager; } }

        #endregion

        #region 项目
        private ProjectManager _projectManager { get; set; }
        public ProjectManager ProjectManager { get { return _projectManager == null ? _projectManager = new ProjectManager() : _projectManager; } }
        private WorkScheduleManager _workScheduleManager { get; set; }
        public WorkScheduleManager WorkScheduleManager { get { return _workScheduleManager == null ? _workScheduleManager = new WorkScheduleManager() : _workScheduleManager; } }
        private ContractManager _contractManager { get; set; }
        public ContractManager ContractManager { get { return _contractManager == null ? _contractManager = new ContractManager() : _contractManager; } }
        private InvoiceManager _invoiceManager { get; set; }
        public InvoiceManager InvoiceManager { get { return _invoiceManager == null ? _invoiceManager = new InvoiceManager() : _invoiceManager; } }

        #endregion

        #region 报销
        private SheetManager _sheetManager { get; set; }
        public SheetManager SheetManager { get { return _sheetManager == null ? _sheetManager = new SheetManager() : _sheetManager; } }
        private SheetViewManager _sheetViewManager { get; set; }
        public SheetViewManager SheetViewManager { get { return _sheetViewManager == null ? _sheetViewManager = new SheetViewManager() : _sheetViewManager; } }
        private SheetFlowDataViewManager _sheetFlowDataViewManager { get; set; }
        public SheetFlowDataViewManager SheetFlowDataViewManager { get { return _sheetFlowDataViewManager == null ? _sheetFlowDataViewManager = new SheetFlowDataViewManager() : _sheetFlowDataViewManager; } }
        private EvectionManager _evectionManager { get; set; }
        public EvectionManager EvectionManager { get { return _evectionManager == null ? _evectionManager = new EvectionManager() : _evectionManager; } }
        private TrafficManager _trafficManager { get; set; }
        public TrafficManager TrafficManager { get { return _trafficManager == null ? _trafficManager = new TrafficManager() : _trafficManager; } }
        private ErrandManager _errandManager { get; set; }
        public ErrandManager ErrandManager { get { return _errandManager == null ? _errandManager = new ErrandManager() : _errandManager; } }
        private DailyManager _dailyManager { get; set; }
        public DailyManager DailyManager { get { return _dailyManager == null ? _dailyManager = new DailyManager() : _dailyManager; } }
        private SubstanceManager _substanceManager { get; set; }
        public SubstanceManager SubstanceManager { get { return _substanceManager == null ? _substanceManager = new SubstanceManager() : _substanceManager; } }
        private ReceptionManager _receptionManager { get; set; }
        public ReceptionManager ReceptionManager { get { return _receptionManager == null ? _receptionManager = new ReceptionManager() : _receptionManager; } }
        private ReceptionItemManager _receptionItemManager { get; set; }
        public ReceptionItemManager ReceptionItemManager { get { return _receptionItemManager == null ? _receptionItemManager = new ReceptionItemManager() : _receptionItemManager; } }
        #endregion
    }
}
