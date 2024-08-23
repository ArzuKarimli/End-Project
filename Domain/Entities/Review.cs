using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Review:BaseEntity
    {
          
            public string Name { get; set; }
            public string Email { get; set; }
            public string Message { get; set; }
            public int ProductId { get; set; } 
            public Product Product { get; set; }
            public DateTime CreatedAt { get; set; }
        

    }
}
