using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using RelayMotorInsuranceCalculator.DAL.Entities;

namespace RelayMotorInsuranceCalculator.DAL.DAL
{
    /// <summary>
    /// Serves as the database context for this application.
    /// </summary>
    public class RelayMotorInsuranceCalculatorDbContext : DbContext
    {
        /// <summary>
        /// The policies that are accepted and saved.
        /// </summary>
        public DbSet<Policy> Policies { get; set; }
        /// <summary>
        /// The drivers that belong to a certain policy.
        /// </summary>
        public DbSet<Driver> Drivers { get; set; }
        /// <summary>
        /// The claims that a certain driver has.
        /// </summary>
        public DbSet<Claim> Claims { get; set; }
        public RelayMotorInsuranceCalculatorDbContext() : base("default")
        {
            Configuration.LazyLoadingEnabled = false;
        }

        public static RelayMotorInsuranceCalculatorDbContext Create()
        {
            return new RelayMotorInsuranceCalculatorDbContext();
        }
    }
}
