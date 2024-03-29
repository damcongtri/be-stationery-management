using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using stationeryManagement.Data.Enum;

namespace stationeryManagement.Data.Model;

public class Request
{
    [Key]
    public int RequestId { get; set; }
    public Guid UserId { get; set; }
    public DateTime RequestDate { get; set; } = DateTime.Now;
    public RequestStatus ApprovalStatus { get; set; } = RequestStatus.Pending;
    public DateTime? CancellationDate { get; set; }
    public DateTime? WithdrawalDate { get; set; }
    public Guid? ApprovedId { get; set; }
    
    [ForeignKey("UserId")]
    public virtual User User { get; set; }
    public virtual ICollection<RequestDetail> RequestDetails { get; set; }
    [ForeignKey("ApprovedId")]
    public virtual User? Approver { get; set; }
}