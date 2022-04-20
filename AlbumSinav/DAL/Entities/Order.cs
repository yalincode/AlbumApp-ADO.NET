using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlbumSinav.DAL.Entities
{
    public class Order
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string County { get; set; }
        public int UserId { get; set; }
    }
}
