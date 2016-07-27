using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RelayMotorInsuranceCalculator.DAL.Entities.Enums;

namespace RelayMotorInsuranceCalculator.DAL.Entities
{
    public class Driver : BaseEntity
    {
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        [Required]
        public Occupation Occupation { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime DateOfBirth { get; set; }
        public virtual ICollection<Claim> Claims { get; set; }
        [ForeignKey("Policy")]
        public Guid PolicyId { get; set; }
        public virtual Policy Policy { get; set; }

    }
}
