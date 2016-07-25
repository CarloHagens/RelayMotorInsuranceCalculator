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
    public class Driver
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        // Sql("ALTER TABLE [dbo].[Driver] ADD [FullName] AS [FirstName] + [LastName]");
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public string FullName { get; set; }
        [Required]
        [MaxLength(30)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(30)]
        public string LastName { get; set; }
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
