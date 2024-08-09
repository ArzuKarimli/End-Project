
using Microsoft.AspNetCore.Identity;
using Service.ViewModel.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface IAccountService
    {
        Task<SignInResult> Login(SignInVM request);
        Task Logout();
    }
}
