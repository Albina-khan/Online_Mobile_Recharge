using Microsoft.EntityFrameworkCore;
using Online_Recharge.Models;

namespace Online_Recharge.Db
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }

        public DbSet<Login_info>  Login_info { get; set; }
        public DbSet<easyLoad> easyLoad { get; set; }
        public DbSet<dataPackages> dataPackages { get; set; }
        public DbSet<usersOrders> internetOrders { get; set; }
        public DbSet<feedback> feedback { get; set; }
        public DbSet<contact> contact { get; set; }

    }
}
