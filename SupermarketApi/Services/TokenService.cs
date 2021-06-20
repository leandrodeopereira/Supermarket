namespace SupermarketApi.Services
{
    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;
    using SupermarketApi.Configuration;
    using SupermarketApi.Entities.Identity;

    internal sealed class TokenService : ITokenService
    {
        private readonly TokenOptions token;
        private readonly SymmetricSecurityKey key;

        public TokenService(IOptions<TokenOptions> options)
        {
            this.token = options.Value;
            this.key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.token.Key!));
        }

        string ITokenService.CreateToken(AppUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.GivenName, user.DisplayName),
            };

            var credentials = new SigningCredentials(this.key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = credentials,
                Issuer = this.token.Issuer!,
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
