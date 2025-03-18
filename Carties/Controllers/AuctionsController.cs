using AutoMapper;
using Carties.Data;
using Carties.DTOs;
using Carties.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Carties.Controllers
{
    [ApiController]
    [Route("api/auctions")]
    public class AuctionsController : ControllerBase
    {
        private readonly AuctionDbContext _context;
        
        private readonly IMapper _mapper;
        public AuctionsController(AuctionDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<AuctionDTO>>> GetAuctions()
        {
            var auctions = await _context.Auctions
                .Include(x => x.Item)
                .OrderBy(x => x.Item.Make)
                .ToListAsync();

            return _mapper.Map<List<AuctionDTO>>(auctions);

        }

        [HttpGet ("{id}")]
        public async Task<ActionResult<AuctionDTO>> GetAuctionById(Guid Id)
        {
            var auction = await _context.Auctions
                .Include(x => x.Item)
                .FirstOrDefaultAsync(x => x.Id == Id);

            if(auction == null) return NotFound();

            return _mapper.Map<AuctionDTO>(auction);

        }

        [HttpPost]
        public async Task<ActionResult<AuctionDTO>> CreateAuction( CreateAuctionDto auctionDto)
        {
            var auction = _mapper.Map<Auction>(auctionDto);
            auction.Seller = "test";

            _context.Auctions.Add(auction);

            var result = await _context.SaveChangesAsync() > 0;
             if(!result) return BadRequest("No changes to be saved in the db.");
             return CreatedAtAction(nameof(GetAuctionById), new {auction.Id}, _mapper.Map<AuctionDTO>(auction));

        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAuction(Guid Id, UpdateAuctionDto updateAuctionDto)
        {
            var auction = await _context.Auctions.Include(x => x.Item)
                .FirstOrDefaultAsync(x => x.Id == Id);

            if(auction==null) return NotFound();

            auction.Item.Make = updateAuctionDto.Make ?? auction.Item.Make;
            auction.Item.Model = updateAuctionDto.Model ?? auction.Item.Model;
            auction.Item.Color = updateAuctionDto.Color ?? auction.Item.Color;
            auction.Item.Mileage = updateAuctionDto.Mileage ?? auction.Item.Mileage;
            auction.Item.Year = updateAuctionDto.Year ?? auction.Item.Year;
          
            var result = await _context.SaveChangesAsync() > 0;
            if(result) return Ok();
            return BadRequest("Couldn't save changes");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAuction(Guid Id)
        {
            var auction = await _context.Auctions.FindAsync(Id);

            if(auction==null) return NotFound();

            _context.Auctions.Remove(auction);
          
            var result = await _context.SaveChangesAsync() > 0;
            if(result) return Ok();
            return BadRequest("Couldn't save changes");
        }
       
}
}