using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace UserData.Models
{
    public class UserModel
    {
        public int? intuserid { get; set; }
        public string strusername { get; set; }
        public string strpassword { get; set; }
        public string strfirstname { get; set; }
        public string strmiddlename { get; set; }
        public string strlastname { get; set; }
        public string stremailid { get; set; }
        public string strcontactno { get; set; }
        public string dtdob { get; set; }
    }
}