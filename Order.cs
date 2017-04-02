using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class Order
    {
        public int ID { get; set; }
        public List<string> Route { get; set; }
        public DateTime TimeCall { get; set; }
        public DateTime TimeStart { get; set; }
        public DateTime TimeFinish { get; set; }
        public int Price { get; set; }
        public int DriverID { get; set; }
        public int ClientID { get; set; }
        public int StatusID { get; set; }


    }
}
