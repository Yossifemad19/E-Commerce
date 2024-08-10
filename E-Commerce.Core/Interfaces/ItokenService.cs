using E_Commerce.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.Interfaces
{
    public interface ItokenService
    {
        String GetToken(AppUser user);
    }
}
