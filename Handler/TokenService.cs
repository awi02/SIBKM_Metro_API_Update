using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.Handler
{
    public interface ITokenService
    {
        string GenerateToken(IEnumerable<Claim> claims);//Payload
    }
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(IEnumerable<Claim> claims)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]!));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);//method untuk memangggil creedensial, lalu dengan algoritma HmacSha256

            var tokenOptions = new JwtSecurityToken(
                                                    issuer: _configuration["JWT:Issuer"],
                                                    audience: _configuration["JWT:Audience"],
                                                    claims: claims,//key dari method jws
                                                    expires: DateTime.Now.AddMinutes(5),//masa kadaluarsa
                                                    signingCredentials: signinCredentials
                                                   );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);//generete token
            return tokenString;//return
        }
    }
}
