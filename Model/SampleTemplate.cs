using Environmental_Monitoring.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Environmental_Monitoring.Model
{
    // =============================================
    // Table for Environmental Sample Templates
    // =============================================
    [Table("SampleTemplates")]
    public class SampleTemplate
    {
        [Key]
        public int TemplateID { get; set; }

        [Required]
        [StringLength(100)]
        public string TenMau { get; set; }

        // Navigation properties
        public virtual ICollection<TemplateParameter> TemplateParameters { get; set; }
        public virtual ICollection<EnvironmentalSample> EnvironmentalSamples { get; set; }

        public SampleTemplate()
        {
            TemplateParameters = new HashSet<TemplateParameter>();
            EnvironmentalSamples = new HashSet<EnvironmentalSample>();
        }
    }
}
