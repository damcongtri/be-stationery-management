using stationeryManagement.Data.Model;
using System.ComponentModel.DataAnnotations;

namespace stationeryManagement.Data.Dto
{
    public class SupplierDto
    {
        public int? SupplierId { get; set; }
        [Required]
        public string Name { get; set; }
        public string ContactInfo { get; set; }
        public ICollection<Supplier>? Stationery { get; set; }
    }
}
