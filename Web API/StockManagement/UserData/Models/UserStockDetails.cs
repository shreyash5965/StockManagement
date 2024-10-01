using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace UserData.Models
{
    public class UserStockDetails
    {
        public int intuserid { get; set; }
        public int? intstockid { get; set; }
        public string strstockname { get; set; }
        public string intstockprice { get; set; }
        public int intquantity { get; set; }
        public string strshortname { get; set; }
        public string strdescription { get; set; }
        public string strtradetype { get; set; }
        public string strtrading { get; set; }
        public int intlowestprice { get; set; }
        public int inthighestprice { get; set; }
        public bool islisted { get; set; }
        public string dtcreatedon { get; set; }
    }

    public class TradingHistoryFilter
    {
        public string strtradetype { get; set; }
        public string strtrading { get; set; }
        public string intstockids { get; set; }
        public string fromdate { get; set; }
        public string todate { get; set; }
    }
}