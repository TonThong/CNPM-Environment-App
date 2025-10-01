using Environmental_Monitoring.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Environmental_Monitoring.Model
{
    // =============================================
    // Table for managing Contracts
    // =============================================
    [Table("Contracts")]
    public class Contract
    {
        [Key]
        public int ContractID { get; set; }

        [Required]
        [StringLength(50)]
        public string MaDon { get; set; }

        public DateTime? NgayKy { get; set; }

        public DateTime? NgayTraKetQua { get; set; }

        public ContractType? ContractType { get; set; }

        public ContractStatus? Status { get; set; }

        // Foreign Keys
        public int? CustomerID { get; set; }
        [ForeignKey("CustomerID")]
        public virtual Customer Customer { get; set; }

        public int? EmployeeID { get; set; }
        [ForeignKey("EmployeeID")]
        public virtual Employee Employee { get; set; }

        // Navigation properties
        public virtual ICollection<EnvironmentalSample> EnvironmentalSamples { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }

        public Contract()
        {
            EnvironmentalSamples = new HashSet<EnvironmentalSample>();
            Notifications = new HashSet<Notification>();
        }
    }
}
