using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Home_A_Heaven.Models
{
    public class OrderModel
    {
        public int Id { get; set; }
        public System.DateTime OrderDate { get; set; }
        public int UserId { get; set; }
        public string SubTotal { get; set; }
    }
}