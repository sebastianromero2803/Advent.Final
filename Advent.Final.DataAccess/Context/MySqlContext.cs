using Advent.Final.Entities.Entities;
using Microsoft.EntityFrameworkCore;

namespace Advent.Final.DataAccess.Context
{
    public class MySqlContext : DbContext
    {
        private string _connectionString;
        public MySqlContext()
        {
            _connectionString = "server=localhost;uid=root;pwd=chachan2803;database=AdventFinal";
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL(_connectionString);
        }

        public MySqlContext(DbContextOptions<MySqlContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }

    }
}
