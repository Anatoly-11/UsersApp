using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SQLite;
using System.Data.Entity;
using System.Configuration;
using System.Text;
using System.Threading.Tasks;

namespace UsersApp {
    public class AppDbContext : DbContext {
        public DbSet<User> Users{ get; set; }
        public AppDbContext() : base("myConn") {

        }
    }
}
