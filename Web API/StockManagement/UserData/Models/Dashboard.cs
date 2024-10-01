using System.Data;

namespace UserData.Models
{
    public class Dashboard
    {
        public DataTable profit_loss_data { get; set; }
        public DataTable investment_data { get; set; }
        public DataTable history_data { get; set; }
    }
}
