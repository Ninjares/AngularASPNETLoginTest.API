using ASPNETAngularLogin.Data.Entities;
using ASPNETAngularLogin.Data.Entities.Base;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ASPNETAngularLogin.Data
{
    public class ASPNETAngularLoginDbContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public ASPNETAngularLoginDbContext(DbContextOptions<ASPNETAngularLoginDbContext> options) : base(options)
        { 
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            this.ConfigureUserIdentityRelations(builder); //??
        }

        private void ConfigureUserIdentityRelations(ModelBuilder builder)
            => builder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries().Where(e => e.State == EntityState.Added || e.State == EntityState.Modified))
            {
                if (entry.Entity is IBaseEntity entity)
                {
                    if (entry.State == EntityState.Added)
                    {
                        entity.CreatedOn = DateTime.UtcNow;
                    }
                    else
                    {
                        entity.ModifiedOn = DateTime.UtcNow;
                    }
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
