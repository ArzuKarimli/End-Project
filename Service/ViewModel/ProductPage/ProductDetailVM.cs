using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ViewModel.ProductPage
{
    public class ProductDetailVM
    {
        public List<Product> Products { get; set; }
        public Product Product { get; set; }
        public List<ProductCategory> ProductCategories { get; set; }
        
    }
}
