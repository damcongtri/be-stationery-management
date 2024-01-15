using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using stationeryManagement.Data.Model;
using stationeryManagement.Service.Interface;

namespace stationeryManagement.Service;

public class AuthService : IAuthService
{
    private IConfiguration _config;

    public AuthService(IConfiguration config)
    {
        _config = config;
    }

    public string GenerateToken(User user)
    {
        //lấy key trong file cấu hình
        var key = _config["Jwt:Key"];
        //mã hóa secret key
        var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        //ký vào key đã mã hóa
        var signingCredential = new SigningCredentials(signingKey,
            SecurityAlgorithms.HmacSha256);
        //tạo claims chứa thông tin người dùng (nếu cần)
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Sid, user.UserId.ToString()),
            new Claim(ClaimTypes.Role, user.Role.RoleName),
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.Email, user.Email),
        };
        //tạo token với các thông số khớp với cấu hình trong startup để validate
        var token = new JwtSecurityToken
        (
            expires: DateTime.Now.AddHours(1),
            signingCredentials: signingCredential,
            claims: claims
        );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}