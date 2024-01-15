﻿using System.ComponentModel.DataAnnotations;

namespace stationeryManagement.Data.Dto
{
    public class StationeryDto
    {
        public int StationeryId { get; set; }
        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }
        public string UnitPrice { get; set; }
       
        public decimal Price { get; set; }
        public int Inventory { get; set; }
        public int ReorderLevel { get; set; }
        public int? SupplierId { get; set; }
        public string? Image {  get; set; }
        public IFormFile? ImageFile { get; set; }
    }
}