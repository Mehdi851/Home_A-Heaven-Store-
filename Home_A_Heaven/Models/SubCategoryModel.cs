using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Home_A_Heaven.Models
{
    public class SubCategoryModel
    {
        public int Id { get; set; }
        public string SubCateName { get; set; }
        public int MainCateId { get; set; }
        public string ImageUrl { get; set; }
        public HttpPostedFileBase ImageFile { get; set; }
    }
}