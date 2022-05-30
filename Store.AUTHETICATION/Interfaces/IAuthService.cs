using Store.DATA.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.AUTHETICATION.Interfaces
{
    public interface IAuthService
    {
        Task<object> Login(string log, string pass);

        Task<object> Register(CustomerDto dto);
    }
}
