using E_Commerce.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.Interfaces
{
    public interface IUnitOfWork:IDisposable
    {
        IUnitOfWork<TEnity> Repository<TEnity>() where TEnity:BaseEntity;
        Task<int> Complete();
    }
}
