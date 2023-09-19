using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.DataBase;
using WebApi.Model;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConnectController : ControllerBase
    {
        private readonly GameServersDbContext _dbContext;

        public ConnectController(GameServersDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost("{code}")]
        public Server Post(string code)
        {
            try
            {
                return _dbContext.Servers.First(server => server.JoinCode == code);
            }
            catch (InvalidOperationException exception)
            {
                throw new InvalidOperationException("Server not found", exception);
            }
        }

        [HttpGet]
        public async Task<IEnumerable<Server>> GetServers()
        {
            return await _dbContext.Servers.ToListAsync();
        }
    }
}
