﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelayMotorInsuranceCalculator.DAL.Entities
{
    public class Policy : BaseEntity
    {
        [DataType(DataType.DateTime)]
        public DateTime StartDate { get; set; }
        public virtual ICollection<Driver> Drivers { get; set; }
    }
}
