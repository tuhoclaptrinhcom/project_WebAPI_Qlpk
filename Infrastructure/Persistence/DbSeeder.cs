using App_QLPK.Application.Interfaces.Services;
using Microsoft.EntityFrameworkCore;
using App_QLPK.Domain.Entities;


namespace App_QLPK.Infrastructure.Persistence;

/// <summary>
///     Tạo 1 tài khoản mặc định admin để đảm bảo có sẵn vai trò Admin (mật khẩu BCrypt) để đăng nhập thử, chỉ Insert khi chưa tồn tại . Gọi lúc khởi động ở môi trường Development
/// </summary> 

public static class DbSeeder
{
    // dữ liệu mặc định
    public const string AdminUsername = "admin";
    public const string AdminPassword = "Admin@123";


    // Hàm dùng để seed cho database    
    public static async Task SeedAsync(QlpkDbContext context , IPasswordHasher hasher, CancellationToken ct = default)
    {   
        // kiểm tra role Admin ? (truy cập vào bảng Role thông qua DbContext)
        var adminRole = await context.Roles.FirstOrDefaultAsync(r => r.Code == "ADMIN", ct); // cần dữ liệu dùng FirstOrDefaultAsync()
        if (adminRole ==  null)  
        {
            // nếu chưa có -> tạo mới
            adminRole = new Role
            {
                Name = "Quản trị viên",
                Code = "ADMIN", // mã định danh
                Description = "Toàn quyền",
                CreatedAt = DateTime.UtcNow,
            };
            context.Roles.Add(adminRole);
            await context.SaveChangesAsync(ct);
        }

        // kiểm tra admin đã tồn tại chưa ? -> true / false -> dùng AnyAsync()
        var hasAdmin = await context.Users.AnyAsync(u => u.Username == AdminUsername, ct); 
        if(!hasAdmin)
        {
            // nếu chưa tồn tại -> tạo mới
            context.Users.Add(new User
            {
                Username = AdminUsername,
                FullName = "Quản trị hệ thống",
                RoleId = adminRole.Id,
                Email = "admin@qlpk.local",
                PasswordHash = hasher.Hash(AdminPassword),
                IsActive = true,
                IsDeleted = false,
                CreatedAt = DateTime.UtcNow
            });
            await context.SaveChangesAsync(ct);
        }
    }
}