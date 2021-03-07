using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestRazor.Model
{
    public class Order
    {
        public long Id { get; set; }
     //   public long BuyerId { get; set; }
        public long SellerId { get; set; }
        public string BuyerEmail { get; set; }
        public string SellerEmail { get; set; }
        public string BuyerTel { get; set; }
        public string SellerTel { get; set; }

        public string BuyerCountry { get; set; }
        public string BuyerCity { get; set; }
        public string Address { get; set; }

        public string Status { get; set; }
    }
}
