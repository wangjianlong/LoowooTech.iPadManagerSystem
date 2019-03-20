using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace LoowooTech.OLDTONEW
{
    class Program
    {
        static void Main(string[] args)
        {
            StreamWriter sw = new StreamWriter(@"C:\ConsoleOutput.txt");
            Console.SetOut(sw);

            Console.WriteLine("Here is the result:");
            Console.WriteLine("Processing......");
            Console.WriteLine("OK!");

            Console.WriteLine("开始运行");

            List<iPad> olds = null;
            List<iPadRegister> registers = null;
            using(var db=new OLDDbContext())
            {
                olds = db.iPads.ToList();
                registers = db.Registers.ToList();
                foreach(var item in registers)
                {
                    item.Register_iPads = db.Register_IPads.Where(e => e.RID == item.ID && e.Revert == 0&&e.Relation==0).ToList();
                }
            }
            using(var db=new NewDbContext())
            {
                var users = db.users.ToList();
                var admin = users.FirstOrDefault(e => e.Name == "管理员");
                if (admin != null)
                {
                    Dictionary<int, int> dict = new Dictionary<int, int>();
                    #region  平板信息录入
                    foreach (var item in olds)
                    {
                        var tableType = new TabletType
                        {
                            Company = "苹果",
                            Year = 2019,
                            Size = 7.9,
                            UserId=admin.ID,
                            Remark="OLD"
                        };
                        switch (item.Color)
                        {
                            case 0:
                                tableType.Color = "银色";
                                break;
                            case 1:
                                tableType.Color = "金色";
                                break;
                            case 2:
                                tableType.Color = "深空灰";
                                break;
                            case 3:
                                tableType.Color = "玫瑰金";
                                break;
                            case 4:
                                tableType.Color = "亮黑色";
                                break;
   
                        }
                        switch (item.Type)
                        {
                            case 0:
                                tableType.Name = "iPad Mini2";
                                break;
                            case 1:
                                tableType.Name = "iPad Mini4";
                                break;
                            case 2:
                                tableType.Name = "iPad Air";
                                break;
                            case 3:
                                tableType.Name = "iPad Air2";
                                break;
                            case 4:
                                tableType.Name = "iPad Pro(9.7英寸)";
                                break;
                            case 5:
                                tableType.Name = "iPad Pro(12.9英寸)";
                                break;
                            case 6:
                                tableType.Name = "iPad(9.7英寸)";
                                break;
                            case 7:
                                tableType.Name = "iPad Pro(10.5英寸)";
                                break;
                            case 8:
                                tableType.Name = "iPad Pro(11英寸)";
                                break;

                        }
                        switch (item.Space)
                        {
                            case 0:
                                tableType.Space = 16;
                                break;
                            case 1:
                                tableType.Space = 32;
                                break;
                            case 2:
                                tableType.Space = 64;
                                break;
                            case 3:
                                tableType.Space = 128;
                                break;
                            case 4:
                                tableType.Space = 256;
                                break;
                        }
                        switch (item.Way)
                        {
                            case 0:
                                tableType.InternetWay = "WIFI";
                                break;
                            case 1:
                                tableType.InternetWay = "WIFI+Cellular";
                                break;
                        }
                        var entry = db.Tablet_Types.FirstOrDefault(e => e.Name == tableType.Name && e.Company == tableType.Company && e.Year == tableType.Year && e.Size == tableType.Size && e.Color == tableType.Color && e.Space == tableType.Space && e.SpaceUnit == tableType.SpaceUnit && e.InternetWay == tableType.InternetWay);
                        if (entry == null)
                        {
                            db.Tablet_Types.Add(tableType);
                            db.SaveChanges();
                            entry = tableType;
                        }
                        var tablet = new Tablet
                        {
                            SerialNumber = item.SerialNumber,
                            Code = item.AuthCode,
                            BuyTime = item.Time,
                            UserId = admin.ID,
                            Remark = "OLD",
                            TypeId=entry.ID
                        };
                        var buyer = users.FirstOrDefault(e => e.Name == item.Buyer);
                        if (buyer != null)
                        {
                            tablet.BuyerId = buyer.ID;
                        }
                        db.Tablets.Add(tablet);
                        db.SaveChanges();
                        dict.Add(item.ID, tablet.ID);
                    }
                   

                    foreach(var item in registers)
                    {
                        var record = new Record
                        {
                            Purpose = item.Mark,
                            
                            Company = "【旧系统】" + item.Name,
                            UseTime = item.BorrowTime,
                            UseName = item.Borrower,
                            Remark = "【OLD】"+ item.Remark,
                            UserId=admin.ID,
                            CityId=1,
                            ProjectId=69
                        };
                        if (item.Register_iPads != null)
                        {
                            var temp = new List<TabletRecord>();
                            foreach(var ee in item.Register_iPads)
                            {
                                if (dict.ContainsKey(ee.IID))
                                {
                                    temp.Add(new TabletRecord
                                    {
                                        TabletId = dict[ee.IID],
                                        Remark = "OLD"
                                    });
                                }
                            }
                            record.TabletRecords = temp;
                        }
                        db.Records.Add(record);
                    }
                    db.SaveChanges();
                    #endregion

                }

            }
           

            sw.Flush();
            sw.Close();
        }

    }
}
