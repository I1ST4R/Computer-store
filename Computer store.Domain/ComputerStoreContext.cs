using Computer_store.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerStore.Domain
{
    public class ComputerStoreContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public ComputerStoreContext(DbContextOptions options) : base(options) { }
    }
}
