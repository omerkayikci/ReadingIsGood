using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ReadingIsGood.Common.ExceptionHandling;
using ReadingIsGood.Core.Options;
using ReadingIsGood.Core.Repositories.Abstractions;
using ReadingIsGood.Core.Request;
using ReadingIsGood.Core.Services.Abstractions;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ReadingIsGood.Application.Service
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly ILogger<AuthenticationService> logger;
        private readonly IUserRepository userRepository;
        private readonly AuthenticationOptions authOptions;
        public AuthenticationService(
            ILogger<AuthenticationService> logger,
            IUserRepository userRepository,
            IOptions<AuthenticationOptions> options)
        {
            this.logger = logger;
            this.userRepository = userRepository;
            authOptions = options.Value;
        }

        public async Task<string> GenerateToken(AuthRequest request)
        {
            if (await IsValidUserAsync(request))
            {
                var someClaims = new Claim[]{

                    //TODO:Örnek vermek gerekirse claime e adres bilgisi eklenerek token bazlı filtereleme yapılabilir.
                    new Claim(JwtRegisteredClaimNames.UniqueName,request.Username),
                };

                var token = CreateJwtBearer(someClaims);

                this.logger.LogInformation("JWT Token Created");

                return new JwtSecurityTokenHandler().WriteToken(token);
            }

            this.logger.LogError("UserName Or Password Wrongly Entered");

            throw new ReadingIsGoodException("UserName And Password Not Valid", HttpStatusCode.BadRequest, logLevel: LogLevel.Warning);
        }

        private JwtSecurityToken CreateJwtBearer(Claim[] someClaims)
        {

            SecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.authOptions.SecurityKey));
            JwtSecurityToken token = new JwtSecurityToken(
                issuer: this.authOptions.Issuer,
                audience: this.authOptions.Audience,
                claims: someClaims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256)
            );

            return token;
        }

        private async Task<bool> IsValidUserAsync(AuthRequest user)
        {
            if (await this.userRepository.IsValidUserAsync(user.Username, user.Password))
            {
                return true;
            }

            return false;
        }
    }
}
