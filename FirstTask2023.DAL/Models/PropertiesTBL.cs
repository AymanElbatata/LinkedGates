﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FirstTask2023.DAL.Models
{
    public partial class PropertiesTBL
    {
        public PropertiesTBL()
        {
            DevicePropertyDetailTBL = new HashSet<DevicePropertyDetailTBL>();
            DevicePropertyTBL = new HashSet<DevicePropertyTBL>();
        }

        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        public string Name { get; set; }

        [InverseProperty("Property")]
        public virtual ICollection<DevicePropertyDetailTBL> DevicePropertyDetailTBL { get; set; }
        [InverseProperty("Property")]
        public virtual ICollection<DevicePropertyTBL> DevicePropertyTBL { get; set; }
    }
}