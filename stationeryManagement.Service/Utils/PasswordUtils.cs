using System.Security.Cryptography;
using System.Text;

namespace stationeryManagement.Service.Utils;

public static class PasswordUtils
{
    // Hàm tạo salt sử dụng BCrypt
    public static string GenerateSalt()
    {
        return BCrypt.Net.BCrypt.GenerateSalt();
    }

    // Hàm băm mật khẩu sử dụng BCrypt
    public static string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password, GenerateSalt());
    }

    // Hàm xác minh mật khẩu sử dụng BCrypt
    public static bool VerifyPassword(string enteredPassword, string storedHashedPassword)
    {
        return BCrypt.Net.BCrypt.Verify(enteredPassword, storedHashedPassword);
    }
}