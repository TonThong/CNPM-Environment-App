using Environmental_Monitoring.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

namespace Environmental_Monitoring.Model
{
    // =============================================
    // Junction table for SampleTemplates and Parameters
    // =============================================
    [Table("TemplateParameters")]
    public class TemplateParameter
    {
        [Key, Column(Order = 0)]
        public int TemplateID { get; set; }

        [Key, Column(Order = 1)]
        public int ParameterID { get; set; }

        // Navigation properties
        [ForeignKey("TemplateID")]
        public virtual SampleTemplate SampleTemplate { get; set; }

        [ForeignKey("ParameterID")]
        public virtual Parameter Parameter { get; set; }
    }
}
