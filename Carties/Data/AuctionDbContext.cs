using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Carties.Entities;
using Microsoft.EntityFrameworkCore;

namespace Carties.Data
{
    public class AuctionDbContext: DbContext
    {
        public AuctionDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Auction>Auctions { get; set; }
        
    }
}