using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelayMotorInsuranceCalculator.DAL.Entities
{
    public class Policy
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime StartDate { get; set; }
        public virtual ICollection<Driver> Drivers { get; set; }
    }
}
