using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Environmental_Monitoring.Model
{
    // =============================================
    // Table for managing system Notifications
    // =============================================
    [Table("Notifications")]
    public class Notification
    {
        [Key]
        public int NotificationID { get; set; }

        [Required]
        public NotificationType LoaiThongBao { get; set; }

        public string NoiDung { get; set; } // TEXT maps to string

        public DateTime ThoiGianGui { get; set; }

        public bool DaGui { get; set; }

        // Foreign Key
        public int? ContractID { get; set; }
        [ForeignKey("ContractID")]
        public virtual Contract Contract { get; set; }
    }
}
