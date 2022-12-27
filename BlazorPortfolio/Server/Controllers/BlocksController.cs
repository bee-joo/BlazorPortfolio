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

        private RoleManager<IdentityRole> roleManager;
        private UserManager<ApplicationUser> userManager;

        public BlocksController(ApplicationDbContext context, RoleManager<IdentityRole> roleMgr, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _blocks = context.Blocks;

            roleManager = roleMgr;
            this.userManager = userManager;
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

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Block>> AddBlock([FromBody] Block block)
        {
            var result = _blocks.Add(block);
            await _context.SaveChangesAsync();
            return result.Entity;
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
