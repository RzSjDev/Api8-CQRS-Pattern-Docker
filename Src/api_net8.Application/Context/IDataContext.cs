using Microsoft.EntityFrameworkCore;
using Src.api_net8.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Src.api_net8.Application.Context
{
    public interface IDataContext
    {
        public DbSet<Factor> Factors { get; set; }

        public DbSet<FactorDetail> FactorDetails { get; set; }

        public DbSet<Product> Products { get; set; }

        int SaveChanges(bool acceptAllChangesOnSuccess);
        int SaveChanges();
        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken());
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());
    }
}
