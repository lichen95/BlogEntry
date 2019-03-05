using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NiuNiu.BlogEntry.IService;

namespace NiuNiu.BlogEntry.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private IConfiguration _config;
        public IUsersService iUsersService = null;
        public TokenController(IConfiguration config, IUsersService _iUsersService)
        {
            _config = config;
            iUsersService = _iUsersService;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult CreateToken([FromBody]LoginModel login)
        {
            IActionResult response = Unauthorized();
            var user = Authenticate(login);

            if (user != null)
            {
                var tokenString = BuildToken(user);
                response = Ok(new { token = tokenString });
            }

            return response;
        }

        /// <summary>
        /// 根据用户信息生成token
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private string BuildToken(UserModel user)
        {
            //添加Claims信息
            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, user.Name),
                new Claim(JwtRegisteredClaimNames.Email, user.Password),
                new Claim(JwtRegisteredClaimNames.Birthdate, user.Birthdate.ToString("yyyy-MM-dd")),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              claims,//添加claims
              expires: DateTime.Now.AddMinutes(30),
              signingCredentials: creds);
            //一个典型的JWT 字符串由三部分组成:

            //header: 头部,meta信息和算法说明
            //payload: 负荷(Claims), 可在其中放入自定义内容, 比如, 用户身份等
            //signature: 签名, 数字签名, 用来保证前两者的有效性

            //三者之间由.分隔, 由Base64编码.根据Bearer 认证规则, 添加在每一次http请求头的Authorization字段中, 这也是为什么每次这个字段都必须以Bearer jwy - token这样的格式的原因.
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private UserModel Authenticate(LoginModel login)
        {
            UserModel user = null;

           var users= iUsersService.Login(login.Username, login.Password);

            if (users!=null)
            {
                user = new UserModel { Name = login.Username, Password = login.Password };
            }
            
            return user;
        }

        public class LoginModel
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }

        private class UserModel
        {
            public string Name { get; set; }
            public string Password { get; set; }
            public DateTime Birthdate { get; set; }
        }
    }
}