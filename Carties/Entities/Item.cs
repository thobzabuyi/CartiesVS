using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Carties.Entities
{
    public class Item
    {
        public Guid Id {get; set;}
        public string Make { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public string ImageUrl { get; set; }
        public int Year { get; set; }
        public int Mileage { get; set; }

        
        public Auction Auction{get; set;}
         public Guid AuctionIdId {get; set;}

    }
}