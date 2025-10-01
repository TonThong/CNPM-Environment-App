using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Environmental_Monitoring.Model
{
    // =============================================
    // Enums based on your database ENUM types
    // =============================================

    public enum ContractType
    {
        Quy,
        [Display(Name = "6 Thang")]
        Thang6
    }

    public enum ContractStatus
    {
        Active,
        Expired,
        Completed
    }

    public enum ResultStatus
    {
        Pending,
        Approved,
        Rejected
    }

    public enum NotificationType
    {
        QuaHan,
        SapHetHan
    }
}