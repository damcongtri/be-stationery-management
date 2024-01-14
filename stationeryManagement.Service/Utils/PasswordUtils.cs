using System.Security.Cryptography;
using System.Text;

namespace stationeryManagement.Service.Utils;

public static class PasswordUtils
{
    public static string GenerateSalt()
    {
        byte[] saltBytes = new byte[16];
        using (var rng = new RNGCryptoServiceProvider())
        {
            rng.GetBytes(saltBytes);
        }
        return Convert.ToBase64String(saltBytes);
    }

    // Hàm mã hóa mật khẩu với salt
    public static string HashPassword(string password, string salt)
    {
        using (var sha256 = SHA256.Create())
        {
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password + salt);
            byte[] hashedBytes = sha256.ComputeHash(passwordBytes);
            return Convert.ToBase64String(hashedBytes) + salt;
        }
    }
    public static string HashPassword(string password)
    {
        using (var sha256 = SHA256.Create())
        {
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password + GenerateSalt());
            byte[] hashedBytes = sha256.ComputeHash(passwordBytes);
            return Convert.ToBase64String(hashedBytes) + GenerateSalt();
        }
    }

    // Hàm xác minh mật khẩu
    public static bool VerifyPassword(string enteredPassword, string storedHashedPassword)
    {
        // Trích xuất salt từ storedHashedPassword
        string salt = storedHashedPassword.Substring(storedHashedPassword.Length - 22);

        // Tái tạo hashed password và so sánh
        string enteredPasswordHash = HashPassword(enteredPassword, salt);
        return enteredPasswordHash == storedHashedPassword;
    }
}