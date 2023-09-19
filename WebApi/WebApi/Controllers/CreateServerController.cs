using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using WebApi.DataBase;
using WebApi.Model;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CreateServerController : ControllerBase
    {
        private readonly GameServersDbContext _dbContext;

        public CreateServerController(GameServersDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        public string Post()
        {
            var containerId = new BashCommand("docker compose -f /root/compose.yaml run --service-ports -d gameserver").Execute();
            containerId = containerId.Replace("\n", string.Empty);
            var ports = new BashCommand($"docker port {containerId}").Execute();

            var port7777 = Regex.Match(ports, @"7777/tcp -> \S*:(\d*)").Groups[1].Value;
            var port7778 = Regex.Match(ports, @"7778/tcp -> \S*:(\d*)").Groups[1].Value;
            string joinCode = string.Empty;

            do
                joinCode = new JoinCode(6).CreateNew();
            while (_dbContext.Servers.Any(server => server.JoinCode == joinCode));

            _dbContext.Servers.Add(new Server()
            {
                JoinCode = joinCode,
                IpAddress = "localhost",
                Port7777 = port7777,
                Port7778 = port7778,
                ContainerId = containerId,
            });

            _dbContext.SaveChanges();

            return joinCode;
        }
    }
}