using Microsoft.EntityFrameworkCore;
using System;

namespace SecurityDemo.Data
{
    public interface ISecurityDemoContext : IDisposable
    {
        DbSet<TEntity> Set<TEntity>()
            where TEntity : class;

        int SaveChanges();
    }
}
