﻿using System.ComponentModel.DataAnnotations;

namespace stationeryManagement.Data.Dto
{
    public class RoleDto
    {
        public Guid Id { get; set; }
        [Required]
        public string RoleName { get; set; }
        public decimal ThresholdAmountPerMonth { get; set; }
    }
}
