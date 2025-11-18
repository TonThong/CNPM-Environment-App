using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Diagnostics.Contracts;

namespace Environmental_Monitoring.Model
{
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

        // --- ĐÃ THAY ĐỔI ---
        public DateTime? NamSinh { get; set; } // Đổi từ int? sang DateTime?

        public int TruongBoPhan { get; set; }

        [StringLength(255)]
        public string DiaChi { get; set; }

        [StringLength(15)]
        public string SoDienThoai { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [StringLength(255)]
        public string PasswordHash { get; set; }

        public byte[] FaceIDData { get; set; }

        public int? RoleID { get; set; }
        [ForeignKey("RoleID")]
        public virtual Role Role { get; set; }

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
            TruongBoPhan = 0;
        }

        public Employee(DataRow row)
        {
            EmployeeID = Convert.ToInt32(row["EmployeeID"]);
            MaNhanVien = row["MaNhanVien"].ToString();
            HoTen = row["HoTen"].ToString();

            // --- ĐÃ THAY ĐỔI ---
            if (row["NamSinh"] != DBNull.Value)
                NamSinh = Convert.ToDateTime(row["NamSinh"]); // Đổi từ ToInt32 sang ToDateTime
            else
                NamSinh = null;

            if (row.Table.Columns.Contains("TruongBoPhan") && row["TruongBoPhan"] != DBNull.Value)
                TruongBoPhan = Convert.ToInt32(row["TruongBoPhan"]);
            else
                TruongBoPhan = 0;

            DiaChi = row["DiaChi"].ToString();
            SoDienThoai = row["SoDienThoai"].ToString();
            Email = row["Email"].ToString();
            PasswordHash = row["PasswordHash"].ToString();

            if (row["RoleID"] != DBNull.Value)
                RoleID = Convert.ToInt32(row["RoleID"]);
            else
                RoleID = null;

            if (row.Table.Columns.Contains("RoleName") && row["RoleName"] != DBNull.Value)
            {
                this.Role = new Role
                {
                    RoleID = this.RoleID ?? 0,
                    RoleName = row["RoleName"].ToString()
                };
            }

            Contracts = new HashSet<Contract>();
            SamplesAssignedHT = new HashSet<EnvironmentalSample>();
            SamplesAssignedPTN = new HashSet<EnvironmentalSample>();
        }
    }
}