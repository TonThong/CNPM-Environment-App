using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Environmental_Monitoring.Model
{
    [Table("EnvironmentalSamples")]
    public class EnvironmentalSample
    {
        [Key]
        public int SampleID { get; set; }

        [Required]
        [StringLength(50)]
        public string MaMau { get; set; }

        // --- CÁC TRƯỜNG MỚI THÊM ---
        [StringLength(50)]
        public string KyHieuMau { get; set; } // Ví dụ: KXQ.01

        [StringLength(500)]
        public string ViTriLayMau { get; set; } // Ví dụ: Cổng số 1

        [StringLength(50)]
        public string ToaDoX { get; set; }

        [StringLength(50)]
        public string ToaDoY { get; set; }
        // ---------------------------

        public int? ContractID { get; set; }
        [ForeignKey("ContractID")]
        public virtual Contract Contract { get; set; }

        public int? TemplateID { get; set; }
        [ForeignKey("TemplateID")]
        public virtual SampleTemplate SampleTemplate { get; set; }

        public int? AssignedToHT { get; set; }
        [ForeignKey("AssignedToHT")]
        public virtual Employee EmployeeHT { get; set; }

        public int? AssignedToPTN { get; set; }
        [ForeignKey("AssignedToPTN")]
        public virtual Employee EmployeePTN { get; set; }

        public int? ONhiem { get; set; }

        public virtual ICollection<Result> Results { get; set; }

        public EnvironmentalSample()
        {
            Results = new HashSet<Result>();
        }
    }
}