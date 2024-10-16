﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Product:BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public ProductCategory Category { get; set; }
        public decimal Rating { get; set; }
        
        public decimal Weight { get; set; } 
        public string Material { get; set; }
        public int SalesCount { get; set; } = 0;
        public decimal Price { get; set; }
        public ICollection<ProductImage> ProductImages { get; set; }
        public ICollection<Review>? Reviews { get; set; }

    }
}
