using Domain.Entities;
using Service.ViewModel.Contact;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface IContactService
    {
        Task CreateMessage(Contact contact);
    }
}
