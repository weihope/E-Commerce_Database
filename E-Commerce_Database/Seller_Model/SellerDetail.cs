using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Database.Seller
{
    public class SellerDetail
    {
        [Key]
        public int SellerId { get; set; }
        public string SellerEmail { get; set; }
        public string SellerPhoneNumber { get; set; }
        public string SellerStoreName { get; set; }
        public string SellerPassword { get; set; }
        public int LoginTry { get; set; }
        public string? Status { get; set; }
        public DateTime LastLoginTime { get; set; }
        public int AddressId { get; set; }
        public virtual ICollection<Products>? products { get; set; }
        public virtual ICollection<SellerAddress>? sellerAddress { get; set; }



    }
}
