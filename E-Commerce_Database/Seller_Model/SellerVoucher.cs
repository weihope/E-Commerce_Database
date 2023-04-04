using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Database.Seller_Model
{
    public class SellerVoucher
    {
        [Key]
        public int SellerVoucherId { get; set; }
        public int SellerId { get; set; }
    }
}
