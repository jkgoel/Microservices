﻿using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace JKTech.Common.Auth
{
    public class JwtHandler : IJwtHandler
    {
        private readonly JwtOptions _options;
        private readonly SecurityKey _issuerSecurityKey;
        private readonly SigningCredentials _signingCredentials;
        private readonly TokenValidationParameters _tokenValidationParameters;
        private readonly JwtHeader _jwtHeader;

        private  readonly  JwtSecurityTokenHandler _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

        public JwtHandler(IOptions<JwtOptions> options)
        {
            _options = options.Value;
            _issuerSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey));
            _signingCredentials = new SigningCredentials(_issuerSecurityKey,SecurityAlgorithms.HmacSha256);
            _jwtHeader = new JwtHeader(_signingCredentials);
            _tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidIssuer = _options.Issuer,
                IssuerSigningKey = _issuerSecurityKey
            };

        }
        public JsonWebToken CreateToken(Guid userId)
        {
            var nowUtc = DateTime.UtcNow;
            var expires = nowUtc.AddMinutes(_options.ExpireMinutes);
            var centuryBegin = new DateTime(1970,1,1).ToUniversalTime();
            var exp = (long) (new TimeSpan(expires.Ticks - centuryBegin.Ticks).TotalSeconds);
            var now = (long) (new TimeSpan(nowUtc.Ticks - centuryBegin.Ticks).TotalSeconds);
            var payload = new JwtPayload
            {
                {"sub", userId},
                {"iss", _options.Issuer},
                {"iat", now},
                {"exp", exp},
                {"unique_name", userId}
            };

            var jwt = new JwtSecurityToken(_jwtHeader,payload);
            var token = _jwtSecurityTokenHandler.WriteToken(jwt);

            return new JsonWebToken
            {
                Token = token,
                Expires = exp
            };

        }
    }
}