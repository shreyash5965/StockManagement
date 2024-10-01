using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace UserData.Models
{
    public class StockMasterModel
    {
        public int? intstockid { get; set; }
        public string strstockname { get; set; }
        public string strshortname { get; set; }
        public string strdescription { get; set; }
        public string intlowestprice { get; set; }
        public string inthighestprice { get; set; }
        public bool islisted { get; set; }
        public string dtecreatedon { get; set; }
    }
}