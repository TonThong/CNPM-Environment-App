using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
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

        public string ContractType { get; set; }

        public string Status { get; set; }

        public int? TienTrinh { get; set; }
        public int? CustomerID { get; set; }
        [ForeignKey("CustomerID")]
        public virtual Customer Customer { get; set; } 

        public int? EmployeeID { get; set; }
        [ForeignKey("EmployeeID")]
        public virtual Employee Employee { get; set; } 

        public virtual ICollection<EnvironmentalSample> EnvironmentalSamples { get; set; } 
        public virtual ICollection<Notification> Notifications { get; set; } 

        public Contract()
        {
            EnvironmentalSamples = new HashSet<EnvironmentalSample>();
            Notifications = new HashSet<Notification>();
        }

        public Contract(DataRow row)
        {
            ContractID = Convert.ToInt32(row["ContractID"]);
            MaDon = row["MaDon"].ToString();

            CustomerID = row["CustomerID"] != DBNull.Value ? (int?)Convert.ToInt32(row["CustomerID"]) : null;
            EmployeeID = row["EmployeeID"] != DBNull.Value ? (int?)Convert.ToInt32(row["EmployeeID"]) : null;

            NgayKy = row["NgayKy"] != DBNull.Value ? (DateTime?)row["NgayKy"] : null;
            NgayTraKetQua = row["NgayTraKetQua"] != DBNull.Value ? (DateTime?)row["NgayTraKetQua"] : null;

            ContractType = row["ContractType"].ToString();
            Status = row["Status"].ToString();

            TienTrinh = row["TienTrinh"] != DBNull.Value ? (int?)Convert.ToInt32(row["TienTrinh"]) : null;
        }
    }
}