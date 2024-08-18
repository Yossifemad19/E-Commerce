using E_Commerce.Core.Entities;
using E_Commerce.Core.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Formats.Tar;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private Hashtable _repositories;
        private readonly StoreContext _context;

        public UnitOfWork(StoreContext context)
        {
            _context = context;
        }
        public async Task<int> Complete()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public IUnitOfWork<TEnity> Repository<TEnity>() where TEnity : BaseEntity
        {
            if(_repositories==null) _repositories = new Hashtable();

           var type =typeof(TEnity).Name;

            if(!_repositories.ContainsKey(type)) { 
            
                var repoType = typeof(GenericRepository<>);

                var repo=Activator.CreateInstance(repoType.MakeGenericType(typeof(TEnity)),_context);

                _repositories.Add(type, repo);
            }

            return (IUnitOfWork<TEnity>) _repositories[type];
        }
    }
}
