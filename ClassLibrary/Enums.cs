using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelryStore.Models
{
    public enum OrderStatus
    {
        Placed = 0,
        Confirmed = 1,
        Payed = 2,
        Sent = 3,
        Delivered = 4,
        Canceled = 5
    }


}
