using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Home_A_Heaven.Models
{
    public class CategoryModel
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string ImageURL { get; set; }
        public HttpPostedFileBase ImageFile { get; set; }
    }
}