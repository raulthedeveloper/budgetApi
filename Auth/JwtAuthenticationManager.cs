using System.Collections.Generic;
using server.Models;
using server.DataAccessLayer;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace server.Auth
{
    public class JwtAuthenticationManager : IJwtAuthenicationManager
    {
       private UserDal db = new UserDal();

        private readonly string key;

        public JwtAuthenticationManager(string key)
        {
            this.key = key;
        }

#pragma warning disable CS8766 // Nullability of reference types in return type doesn't match implicitly implemented member (possibly because of nullability attributes).
        public string? AuthenticateUser(string email, string password)
#pragma warning restore CS8766 // Nullability of reference types in return type doesn't match implicitly implemented member (possibly because of nullability attributes).
        {
           Users user =  db.GetUser(email, password);

            if (user.Email != email && user.Password != password)
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();


            var tokenKey = Encoding.ASCII.GetBytes(key);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, email)
                }),
                Expires = DateTime.UtcNow.AddHours(1),

                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);


        }
    }
}
