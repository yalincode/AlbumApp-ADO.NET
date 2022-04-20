﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlbumSinav.DAL.VM
{
    public class OrderDetailVM
    {
        public int OrderDetailId { get; set; }
        public int AlbumName { get; set; }
        public int SingerName { get; set; }
        public int OrderId { get; set; }
        public int AlbumId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
    }
}
