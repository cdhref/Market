using Market.Util;
using Microsoft.EntityFrameworkCore;

namespace Market.Models.Context
{
    public class MarketDBContext:DbContext
    {
        public virtual DbSet<CompanyModel> Company { get; set; }
        public virtual DbSet<CategoryModel> Category { get; set; }
        public virtual DbSet<ProductModel> Product { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            Config config = new Config();
            string dataSource = config.Read("DataSource", "SQLite");
            optionsBuilder.UseSqlite(string.Format("Data Source={0}", dataSource));
        }
        
    }
}
