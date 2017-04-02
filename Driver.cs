using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class Driver : User
    {
        public int CarNumber { get; set; }
        public int Balance { get; set; }
    }
}
