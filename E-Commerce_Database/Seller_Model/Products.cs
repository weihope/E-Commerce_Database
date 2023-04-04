using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Commerce_Database.Seller_Model;

namespace E_Commerce_Database.Seller
{
    public class Products
    {
        [Key]
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public int SellerId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public decimal DiscountPrice { get; set; }
        public int Stock { get; set; }
        public string? ProductSKU { get; set; }
        public string Description { get; set; } = string.Empty;
        public string ProductStatus { get; set; }

        public Category? Category { get; set; }
        public SellerDetail? Seller { get; set; }

    }
}
