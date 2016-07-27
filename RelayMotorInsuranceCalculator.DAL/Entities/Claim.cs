using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelayMotorInsuranceCalculator.DAL.Entities
{
    public class Claim : BaseEntity
    {
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime ClaimDate { get; set; } 
        [ForeignKey("Driver")]
        public Guid DriverId { get; set; }
        public virtual Driver Driver { get; set; }
    }
}
