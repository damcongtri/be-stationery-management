using stationeryManagement.Data.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace stationeryManagement.Data.Dto
{
    public class ImportDto
    {
        public int ImportId { get; set; }
        public Guid UserCreateId { get; set; }
        public DateTime ImportDate { get; set; } = DateTime.Now;

    }
}
