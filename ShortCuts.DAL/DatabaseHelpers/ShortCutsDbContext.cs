using ShortCuts.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortCuts.DAL.DatabaseHelpers
{
    public class ShortCutsDbContext : DbContext
    {
        #region Constructor
        public ShortCutsDbContext() : base("ShortCutsDbContext")
        {
             
        }
        #endregion

        #region Public Properties
        public DbSet<ProductTypeModel> ProductTypes { get; set; }
        public DbSet<ProductModel> Products { get; set; }
        #endregion
    }
}
