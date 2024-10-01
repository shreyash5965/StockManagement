namespace UserData.Models
{
    public class TradingHistory
    {
        public int? intstockid { get;set;}
        public string strstockname { get;set;}
        public string strshortname { get;set;}
        public int intuserid { get;set;}
        public string strusername { get;set;}
        public int intquantity { get;set;}
        public int intaverageboughtprice { get;set;}
        public int totalamount { get;set;}
        public string strtradetype { get;set;}
        public string strtrading { get;set;}
        public string intstockprice { get;set;}
        public string dtetradedon { get;set;}
    }
}
