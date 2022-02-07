using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Buffers.Binary;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Weelo.Abstrations.Repositories;
using Weelo.Abstrations.Types.Authentication;
using Weelo.Common.Generics;

namespace Weelo.Repository.SqlServer.Services
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private IConfiguration _configuration { get; }

        public AuthenticationRepository(IConfiguration configuration) => _configuration = configuration;
        public Task<string> Generate(IAuth auth)
        {
            try
            {
                byte[] key = Encoding.ASCII.GetBytes(_configuration["JwtParameters:SecretKey"].ToString());
                SecurityTokenDescriptor tokenDescriptor = new()
                {
                    Issuer = JsonConvert.SerializeObject(auth),
                    Subject = new ClaimsIdentity(new[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, auth.IdentificationNumber),
                        new Claim(ClaimTypes.Role, auth.BirthDay),
                        new Claim(ClaimTypes.Actor, auth.ActualDate + auth.IdentificationNumber)
                    }),
                    NotBefore = DateTime.UtcNow,
                    Audience = _configuration["JwtParameters:Audience"],
                    IssuedAt = DateTime.UtcNow,
                    Expires = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["JwtParameters:TimeOut"].ToString())),
                    SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
                };
                JwtSecurityTokenHandler tokenHandler = new();
                SecurityToken securityToken = tokenHandler.CreateToken(tokenDescriptor);
                return Task.Run(() => tokenHandler.WriteToken(securityToken));
            }
            catch
            {
                return Task.Run(() => Constants.DefaultMessage);
            }
        }
        private string GenerateKey(string request)
        {
            RSA rsa = RSA.Create();
            byte[] key = Encoding.UTF8.GetBytes(Convert.ToBase64String(rsa.ExportRSAPrivateKey()).Substring(DateTime.UtcNow.Minute, 32));
            byte[] bytes = Encoding.UTF8.GetBytes(request);
            int nonceSize = AesGcm.NonceByteSizes.MaxSize;
            int tagSize = AesGcm.TagByteSizes.MaxSize;
            int cipherSize = bytes.Length;
            int dataLength = 4 + nonceSize + 4 + tagSize + cipherSize;
            Span<byte> resultData = dataLength < 1024 ? stackalloc byte[dataLength] : new byte[dataLength].AsSpan();
            BinaryPrimitives.WriteInt32LittleEndian(resultData.Slice(0,4), nonceSize);
            BinaryPrimitives.WriteInt32LittleEndian(resultData.Slice(4+nonceSize,4), nonceSize);
            Span<byte> nonce = resultData.Slice(4, nonceSize);
            RandomNumberGenerator.Fill(nonce);
            using AesGcm aes = new AesGcm(key);
            aes.Encrypt(nonce, bytes.AsSpan(), resultData.Slice(4 + nonceSize + 4 + tagSize, cipherSize), resultData.Slice(4 + nonceSize + 4, tagSize));
            return Convert.ToBase64String(resultData);
        }
    }
}
