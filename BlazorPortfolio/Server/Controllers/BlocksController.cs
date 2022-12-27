using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BlazorPortfolio.Server.Data;
using BlazorPortfolio.Server.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Data;

namespace BlazorPortfolio.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlocksController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Block> _blocks;

        public BlocksController(ApplicationDbContext context)
        {
            _context = context;
            _blocks = context.Blocks;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Block>>> GetBlocks()
        {
            if (_blocks == null)
            {
                return NotFound();
            }
            return await _blocks.ToListAsync();
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Block>> GetBlockById(long id)
        {
            var block = await _blocks.FirstOrDefaultAsync(b => b.Id == id);
            if (block == null)
            {
                return NotFound();
            }
            return block;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Block>> AddBlock([FromBody] Block block)
        {
            var result = _blocks.Add(block);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<bool>> EditBlock(long id, [FromBody] Block block)
        {
            var data = await _blocks.FirstOrDefaultAsync(b => b.Id == id);
            if (data != null)
            {
                data.Title = block.Title;
                data.Link = block.Link;

                _blocks.Update(data);
                await _context.SaveChangesAsync();

                return true;
            }
            else
            {
                return false;
            } 
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<bool> DeleteBlock(long id)
        {
            var block = await _blocks.FirstOrDefaultAsync(b => b.Id == id);
            if (block == null)
            {
                return false;
            }
            _blocks.Remove(block);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
