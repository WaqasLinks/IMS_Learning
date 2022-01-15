
using IMSRepo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMSRepo.ViewModels
{
    public class SOVM
    {
        public MySO MySO { get; set; }
        public List<SOD> SODs { get; set; }
        public Customer Customer { get; set; }
        public List<Product> Products { get; set; }
    }
}
