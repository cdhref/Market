using Market.Util;
using System.Data.Entity;

namespace Market.Models.Context
{
    public class MarketDBContext:DbContext
    {
        public MarketDBContext() : base("Market") { }

        public virtual DbSet<CompanyModel> Company { get; set; }
        public virtual DbSet<CategoryModel> Category { get; set; }
        public virtual DbSet<ProductModel> Product { get; set; }
    }
}
