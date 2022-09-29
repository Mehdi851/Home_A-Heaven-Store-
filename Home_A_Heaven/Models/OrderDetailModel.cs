using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Home_A_Heaven.Models
{
    public class OrderDetailModel
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public string UnitPrice { get; set; }
        public string Total { get; set; }
    }
}