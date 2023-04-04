using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Database.Seller
{
    public  class SellerAddress
    {
        [Key]
        public int SellerAddressId { get; set; }
        public int SellerId { get; set; }
        public string WareHouseAddress_PickUp { get; set; }
        public string CustomerReturnAddress { get; set; }
        public SellerDetail? sellerDetail { get; set; }

    }
}
