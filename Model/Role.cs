using Environmental_Monitoring.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Environmental_Monitoring.Model
{
    // =============================================
    // Table for managing user roles
    // =============================================
    [Table("Roles")]
    public class Role
    {
        [Key]
        public int RoleID { get; set; }

        [Required]
        [StringLength(50)]
        public string RoleName { get; set; }

        // Navigation property for one-to-many relationship
        public virtual ICollection<Employee> Employees { get; set; }

        public Role()
        {
            Employees = new HashSet<Employee>();
        }
    }
}
