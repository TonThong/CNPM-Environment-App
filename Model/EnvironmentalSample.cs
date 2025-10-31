using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Contracts;

namespace Environmental_Monitoring.Model
{
    // =============================================
    // Table for specific Environmental Samples
    // =============================================
    [Table("EnvironmentalSamples")]
    public class EnvironmentalSample
    {
        [Key]
        public int SampleID { get; set; }

        [Required]
        [StringLength(50)]
        public string MaMau { get; set; }

        // Foreign Keys
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
        // Navigation property
        public virtual ICollection<Result> Results { get; set; }

        public EnvironmentalSample()
        {
            Results = new HashSet<Result>();
        }
    }
}
