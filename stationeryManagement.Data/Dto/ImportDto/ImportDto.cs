using stationeryManagement.Data.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace stationeryManagement.Data.Dto
{
    public class ImportDto
    {
        public DateTime ImportDate { get; set; } = DateTime.Now;
        public List<ImportDetailDto>  ImportDetails { get; set; }
    }
}
