using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Environmental_Monitoring.Model
{
    // =============================================
    // Table for storing Customer/Company information
    // =============================================
    [Table("Customers")]
    public class Customer
    {
        [Key]
        public int CustomerID { get; set; }

        [Required]
        [StringLength(255)]
        public string TenDoanhNghiep { get; set; }

        [StringLength(50)]
        public string KyHieuDoanhNghiep { get; set; }

        [StringLength(255)]
        public string DiaChi { get; set; }

        [StringLength(100)]
        public string TenNguoiDaiDien { get; set; }

        // Navigation property
        public virtual ICollection<Contract> Contracts { get; set; }

        public Customer()
        {
            Contracts = new HashSet<Contract>();
        }
    }
}
