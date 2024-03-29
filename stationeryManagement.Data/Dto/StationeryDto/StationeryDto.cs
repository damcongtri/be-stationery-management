using System.ComponentModel.DataAnnotations;

namespace stationeryManagement.Data.Dto.StationeryDto
{
    public class StationeryDto
    {
        public int? StationeryId { get; set; }
        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }
        public string UnitPrice { get; set; }
        public int CategoryId { get; set; }

        public decimal Price { get; set; }
        public int Inventory { get; set; }
        public int? SupplierId { get; set; }
        [FileExtensions(Extensions = "jpg, jpeg, gif, png, webp", ErrorMessage = "The file extension is not allowed.")]
        public string? FileName => FileUpload?.FileName;
        public string? Image {  get; set; }
        public IFormFile? FileUpload { get; set; }
    }
}
