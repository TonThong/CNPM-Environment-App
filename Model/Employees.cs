using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Diagnostics.Contracts;

namespace Environmental_Monitoring.Model 
{
    // =============================================
    // Table for storing Employee information
    // =============================================
    [Table("Employees")]
    public class Employee
    {
        [Key]
        public int EmployeeID { get; set; }

        [Required]
        [StringLength(20)]
        public string MaNhanVien { get; set; }

        [Required]
        [StringLength(100)]
        public string HoTen { get; set; }

        public int? NamSinh { get; set; } // YEAR can be mapped to int

        [StringLength(100)]
        public string PhongBan { get; set; }

        [StringLength(255)]
        public string DiaChi { get; set; }

        [StringLength(15)]
        public string SoDienThoai { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [StringLength(255)]
        public string PasswordHash { get; set; }

        public byte[] FaceIDData { get; set; } // BLOB is mapped to byte array

        // Foreign Key
        public int? RoleID { get; set; }
        [ForeignKey("RoleID")]
        public virtual Role Role { get; set; }

        // Navigation properties
        public virtual ICollection<Contract> Contracts { get; set; }

        [InverseProperty("EmployeeHT")]
        public virtual ICollection<EnvironmentalSample> SamplesAssignedHT { get; set; }

        [InverseProperty("EmployeePTN")]
        public virtual ICollection<EnvironmentalSample> SamplesAssignedPTN { get; set; }

        public Employee()
        {
            Contracts = new HashSet<Contract>();
            SamplesAssignedHT = new HashSet<EnvironmentalSample>();
            SamplesAssignedPTN = new HashSet<EnvironmentalSample>();
        }
    }
}
