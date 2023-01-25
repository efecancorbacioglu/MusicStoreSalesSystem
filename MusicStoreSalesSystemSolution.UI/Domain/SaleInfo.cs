using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStoreSalesSystemSolution.UI.Domain
{
    public class SaleInfo
    {
        public SaleInfo()
        {
            ActiveSales = new List<ActiveSale>();
        }
        public string CustomerFirstName { get; set; }
        public string CustomerLastName { get; set; }
        public string EmployeeFirstName { get; set; }
        public string EmployeeLastName { get; set; }
        public DateTime SaleDate { get; set; }
        public decimal TotalAmountPrice { get; set; }

        public List<ActiveSale> ActiveSales { get; set; }
    }
}
