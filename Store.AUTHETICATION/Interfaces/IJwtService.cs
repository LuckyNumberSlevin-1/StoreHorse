using Store.DATA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.AUTHETICATION.Interfaces
{
    public interface IJwtService
    {
        Task<object> GenerateJwt(Customer user);
    }
}
