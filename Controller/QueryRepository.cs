using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Environmental_Monitoring.Controller
{
    // Lớp tĩnh chứa tất cả các chuỗi truy vấn SQL
    public static class QueryRepository
    {
        // === ResultContent Queries ===

        public const string LoadContractResults = @"
        SELECT 
            c.MaDon, c.NgayKy, c.NgayTraKetQua, c.Status,
            cu.TenDoanhNghiep, cu.TenNguoiDaiDien,
            e.HoTen AS TenNhanVien,
            s.SampleID, 
            CONCAT(s.MaMau, ' - ', t.TenMau) AS MauKiemNghiem,
            p.ParameterID, p.TenThongSo, p.DonVi, p.GioiHanMin, p.GioiHanMax,
            p.ONhiem,
            r.GiaTri, r.TrangThaiPheDuyet
        FROM Contracts c
        JOIN Customers cu ON c.CustomerID = cu.CustomerID
        JOIN Employees e ON c.EmployeeID = e.EmployeeID
        JOIN EnvironmentalSamples s ON s.ContractID = c.ContractID
        JOIN SampleTemplates t ON s.TemplateID = t.TemplateID
        JOIN TemplateParameters tp ON t.TemplateID = tp.TemplateID
        JOIN Parameters p ON tp.ParameterID = p.ParameterID
        LEFT JOIN Results r ON r.SampleID = s.SampleID AND r.ParameterID = p.ParameterID
        WHERE c.ContractID = @contractId
        ORDER BY c.MaDon, s.MaMau, p.TenThongSo";

        public const string GetContractHeaderInfo = @"
            SELECT c.MaDon, cu.TenDoanhNghiep, cu.KyHieuDoanhNghiep, cu.TenNguoiDaiDien
            FROM Contracts c
            JOIN Customers cu ON c.CustomerID = cu.CustomerID
            WHERE c.ContractID = @ContractID";

        public const string GetCustomerEmail = @"
            SELECT cu.Email FROM Customers cu
            JOIN Contracts c ON c.CustomerID = cu.CustomerID
            WHERE c.ContractID = @contractId";

        public const string GetMaDon = "SELECT MaDon FROM Contracts WHERE ContractID = @id";

        public const string ResetResultsOnRequest = @"
            UPDATE Results r
            JOIN EnvironmentalSamples es ON r.SampleID = es.SampleID
            SET r.TrangThaiPheDuyet = NULL 
            WHERE es.ContractID = @contractId";

        public const string UpdateContractTienTrinh = "UPDATE Contracts SET TienTrinh = @tienTrinh WHERE ContractID = @contractId";

        public const string ApproveAllResults = @"
            UPDATE Results r
            JOIN EnvironmentalSamples es ON r.SampleID = es.SampleID
            SET r.TrangThaiPheDuyet = 1 
            WHERE es.ContractID = @contractId";

        public const string CompleteContractStatus = "UPDATE Contracts SET Status = 'Completed' WHERE ContractID = @contractId";

        public const string GetPopupContracts = "SELECT ContractID, MaDon, NgayKy, NgayTraKetQua, Status FROM Contracts";
    }
}
