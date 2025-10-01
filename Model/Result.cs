using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

namespace Environmental_Monitoring.Model
{
    // =============================================
    // Table for storing analysis Results
    // =============================================
    [Table("Results")]
    public class Result
    {
        [Key]
        public int ResultID { get; set; }

        public decimal? GiaTri { get; set; }

        public ResultStatus? TrangThaiPheDuyet { get; set; }

        public DateTime? NgayPhanTich { get; set; }

        // Foreign Keys
        public int? SampleID { get; set; }
        [ForeignKey("SampleID")]
        public virtual EnvironmentalSample EnvironmentalSample { get; set; }

        public int? ParameterID { get; set; }
        [ForeignKey("ParameterID")]
        public virtual Parameter Parameter { get; set; }
    }
}
