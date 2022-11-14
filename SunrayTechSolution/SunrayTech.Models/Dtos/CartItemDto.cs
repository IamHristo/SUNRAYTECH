using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SunrayTech.Models.Dtos
{
    public class CartItemDto
    {
        private int qty;

        public int Id { get; set; }
        public int ProductId { get; set; }
        public int CartId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public string ProductImageURL { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice { get; set; }
        public int AavailableQty { get; set; }
        public int Qty
        {
            get => qty;
            set
            {
                qty = value;
                TotalPrice = Price * Qty;
            }
        }

    }
}
