using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ViewModel
{
    public class CheckoutVM
    {
        public Course Course { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
