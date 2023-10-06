using Microsoft.AspNetCore.Mvc;
using WebApi.DataBase;
using WebApi.Model;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [RestrictToLocalhost]
    public class RemoveServerController : ControllerBase
    {
        private readonly GameServersDbContext _dbContext;

        public RemoveServerController(GameServersDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost("{containerId}")]
        public void Post(string containerId)
        {
            try
            {
                var server = _dbContext.Servers.First(server => server.ContainerId == containerId);
                _dbContext.Servers.Remove(server);
                _dbContext.SaveChanges();
            }
            catch (InvalidOperationException exception)
            {
                throw new InvalidOperationException($"Server with container id {containerId} not found", exception);
            }

            _ = new BashCommand($"docker stop {containerId} | xargs docker rm").Execute();
        }
    }
}
