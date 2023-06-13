using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Spg.GammaShop.Infrastructure;

namespace Spg.GammaShop.API.Controllers.V2
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("2.0")]
    public class SeedController : ControllerBase
    {
        private GammaShopContext _context;

        public SeedController(GammaShopContext context)
        {
            _context = context;
        }

        [HttpGet("seed")]
        public IActionResult Seed()
        {
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
            _context.Seed();
            _context.SaveChanges();
            return Ok();
        }
    }
}