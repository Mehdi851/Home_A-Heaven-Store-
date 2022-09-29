using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Home_A_Heaven.Models
{
    public class ProductModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Old_Price { get; set; }
        [Required]
        public string New_Price { get; set; }
       
        public string Discount { get; set; }
        [Required]
        public string Matrial { get; set; }
        [Required]
        public string Color { get; set; }
        [Required]
        public string Height { get; set; }
        [Required]
        public string Discription { get; set; }
        [Required]
        public string Size { get; set; }
        [Required]
        public string Selling { get; set; }
        [Required]
        public System.DateTime InsertDate { get; set; }
        [Required]
        public string Status { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public int SubCategoryId { get; set; }

    }
}