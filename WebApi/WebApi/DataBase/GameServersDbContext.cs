using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using WebApi.Model;

namespace WebApi.DataBase
{
    public class GameServersDbContext : DbContext
    {
        public GameServersDbContext(DbContextOptions<GameServersDbContext> options)
            : base(options)
        {
            if (Database.GetService<IDatabaseCreator>() is not RelationalDatabaseCreator dataBaseCreator)
                throw new NullReferenceException(nameof(dataBaseCreator));

            if (dataBaseCreator.CanConnect() == false)
                dataBaseCreator.Create();

            if (dataBaseCreator.HasTables() == false)
                dataBaseCreator.CreateTables();
        }

        public DbSet<Server> Servers { get; set; }
    }
}
