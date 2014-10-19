using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.EntityModel.PayPal
{
    public class PayPalPaymentModel
    {
        public string ProductName { get; set; }
        public string Quantity { get; set; }
        public string Price { get; set; }
        public string Description { get; set; }
        public string Currency { get; set; }
    }
}
