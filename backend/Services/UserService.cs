using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Caps.Models;
using Caps.Helpers;

namespace Caps.Services
{
    public interface IUserService
    {
        User Authenticate(string username, string password);
        IEnumerable<User> GetAll();
    }

    public class UserService : IUserService
    {
        // users hardcoded for simplicity, store in a db with hashed passwords in production applications
        private List<User> _users = new List<User>
        { 
            new User { Id = 1, FirstName = "Enver", LastName = "Yilmaz", Username = "enver.yilmaz", Password = "p@ssw0rd" },
            new User { Id = 2, FirstName = "Salim", LastName = "Serdar", Username = "salim.serdar", Password = "p@ssw0rd" },
            new User { Id = 3, FirstName = "Selami", LastName = "Kul", Username = "selami.kul", Password = "p@ssw0rd" },
            new User { Id = 4, FirstName = "Ender", LastName = "Yilmaz", Username = "ender.yilmaz", Password = "p@ssw0rd" },
            new User { Id = 5, FirstName = "Mesut", LastName = "Boztas", Username = "mesut.boztas", Password = "p@ssw0rd" },
            new User { Id = 6, FirstName = "Tufan", LastName = "Bilge", Username = "tufan.bilge", Password = "p@ssw0rd" },
            new User { Id = 7, FirstName = "Celal", LastName = "Unver", Username = "celal.unver", Password = "p@ssw0rd" } 
        };

        private readonly AppSettings _appSettings;

        public UserService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public User Authenticate(string username, string password)
        {
            var user = _users.SingleOrDefault(x => x.Username == username && x.Password == password);

            // return null if user not found
            if (user == null)
                return null;

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] 
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            // remove password before returning
            user.Password = null;

            return user;
        }

        public IEnumerable<User> GetAll()
        {
            // return users without passwords
            return _users.Select(x => {
                x.Password = null;
                return x;
            });
        }
    }
}