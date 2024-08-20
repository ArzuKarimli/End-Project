using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ViewModel.Contact
{
    public class ContactVM
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
        public Dictionary<string, string> Settings { get; set; }
    }
}
