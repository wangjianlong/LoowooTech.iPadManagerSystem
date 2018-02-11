using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoowooTech.Models.Expense
{
    [Table("traffic")]
    public class Traffic
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Vehicles Vehicles { get; set; }
        /// <summary>
        /// 金额
        /// </summary>
        public double Cost { get; set; }
        /// <summary>
        /// 公里数
        /// </summary>
        public double? KiloMeter { get; set; }
        /// <summary>
        /// 过路费
        /// </summary>
        public double? Toll { get; set; }
        /// <summary>
        /// 油费
        /// </summary>
        public double? Petrol { get; set; }
        /// <summary>
        /// 司机
        /// </summary>
        public string Driver { get; set; }
        /// <summary>
        /// 车补
        /// </summary>
        public double? CarPetty { get; set; }
        /// <summary>
        /// 车牌
        /// </summary>
        public string Plate { get; set; }
        public int EvectionId { get; set; }

        [NotMapped]
        public string Description
        {
            get
            {
                var sb = new StringBuilder();
                sb.AppendFormat("{0} 元", Cost);
                if (Vehicles == Vehicles.Personal || Vehicles == Vehicles.Company)
                {
                    sb.AppendFormat("其中里程：{0}公里，过路费：{1}元，油费：{2}元，司机：{3}，车补：{4}，车牌：{5}",
                        KiloMeter == null ? 0 : KiloMeter.Value,
                        Toll.HasValue ? Toll.Value : 0,
                        Petrol.HasValue ? Petrol.Value : 0,
                        Driver,
                        CarPetty.HasValue ? CarPetty.Value : 0,
                        Plate);
                }
                return sb.ToString();
            }
        }

        public static List<Traffic> Generate(int evectionId, Vehicles[] vehicles,double[] cost,double[] kiloMeter,double[] carPetty, string[] plate, string[] driver, double[] petrol, double[] toll)
        {
            if (vehicles == null)
            {
                return null;
            }
            var i = 0;
            var list = new List<Traffic>();
            foreach(Vehicles ve in Enum.GetValues(typeof(Vehicles)))
            {
                if (vehicles.Contains(ve))
                {
                    var entry = new Traffic
                    {
                        Vehicles = ve,
                        Cost = cost[i],
                        EvectionId=evectionId
                    };

                    if (ve == Vehicles.Company)
                    {
                        entry.KiloMeter = kiloMeter[0];
                        entry.CarPetty = carPetty[0];
                        if (plate != null)
                        {
                            entry.Plate = plate[0];
                        }
                        if (driver != null)
                        {
                            entry.Driver = driver[0];
                        }
                       
                        entry.Petrol = petrol[0];
                        entry.Toll = toll[0];
                    }
                    else if (ve == Vehicles.Personal)
                    {
                        entry.KiloMeter = kiloMeter[1];
                        entry.CarPetty = carPetty[1];
                        if (plate != null)
                        {
                            entry.Plate = plate[1];
                        }
                        if (driver != null)
                        {
                            entry.Driver = driver[1];
                        }
                        entry.Petrol = petrol[1];
                        entry.Toll = toll[1];
                    }
                    list.Add(entry);
                }
                i++;    
            }
            return list;
        }
    }

    public enum Vehicles
    {
        [Description("飞机")]
        Plane=0,
        [Description("火车")]
        Train,
        [Description("客运大巴")]
        Bus,
        [Description("出租车")]
        Taxi,
        [Description("公交车")]
        PublicBus,
        [Description("地铁")]
        Metro,
        [Description("船")]
        Ship,
        [Description("互联网用车（企业支付）")]
        InternetCar,
        [Description("互联网用车（个人支付）")]
        InternetCarPersonal,
        [Description("公司车")]
        Company=50,
        [Description("自备车")]
        Personal,
    }
}
