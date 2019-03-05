using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NiuNiu.BlogEntry.Entity;
using NiuNiu.BlogEntry.Entity.DTO;

namespace NiuNiu.BlogEntry.UI.Controllers
{
   [PermissionRequiredAttribute]
    public class UsersController : Controller
    {
        #region 用户登录

        /// <summary>
        /// 获取token
        /// </summary>
        /// <returns></returns>
        public string GetToken()
        {
            var tokenStr = HttpContext.User.Claims.Last().Value;
            var token = JsonConvert.DeserializeObject<Token>(tokenStr);
            return token.token;
        }

        /// <summary>
        ///登录
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [NoPermissionRequired]
        [HttpPost]
        public string Login(string username, string password)
        {
            var json = HttpClientHelper.PostResponse("http://127.0.0.1:8088/api/usersapi/login?Name=" + username + "&Pwd=" + password, "");
            var user = JsonConvert.DeserializeObject<Users>(json);
            if (user != null)
            {
                LoginModel login = new LoginModel() { Username= user .UserName,Password=user.Password};
                var token = HttpClientHelper.PostResponse("http://127.0.0.1:8088/api/Token",JsonConvert.SerializeObject(login));
                
                //下面的变量claims是Claim类型的数组，Claim是string类型的键值对，所以claims数组中可以存储任意个和用户有关的信息，
                //不过要注意这些信息都是加密后存储在客户端浏览器cookie中的，所以最好不要存储太多特别敏感的信息，这里我们只存储了用户名到claims数组,
                //表示当前登录的用户是谁
                var claims = new[] { new Claim("User", json), new Claim("token", token) };

                var claimsIdentity = new ClaimsIdentity(
                claims,
                CookieAuthenticationDefaults.AuthenticationScheme);

                ClaimsPrincipal claimsPriincipal = new ClaimsPrincipal(claimsIdentity);

                Task.Run(async () =>
                {
                    //登录用户，相当于ASP.NET中的FormsAuthentication.SetAuthCookie
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPriincipal);
                    #region
                    //可以使用HttpContext.SignInAsync方法的重载来定义持久化cookie存储用户认证信息，例如下面的代码就定义了用户登录后60分钟内cookie都会保留在客户端计算机硬盘上，
                    //即便用户关闭了浏览器，60分钟内再次访问站点仍然是处于登录状态，除非调用Logout方法注销登录。
                    //注意其中的AllowRefresh属性，如果AllowRefresh为true，表示如果用户登录后在超过50%的ExpiresUtc时间间隔内又访问了站点，就延长用户的登录时间（其实就是延长cookie在客户端计算机硬盘上的保留时间），
                    //例如本例中我们下面设置了ExpiresUtc属性为60分钟后，那么当用户登录后在大于30分钟且小于60分钟内访问了站点，那么就将用户登录状态再延长到当前时间后的60分钟。但是用户在登录后的30分钟内访问站点是不会延长登录时间的，
                    //因为ASP.NET Core有个硬性要求，是用户在超过50%的ExpiresUtc时间间隔内又访问了站点，才延长用户的登录时间。
                    //如果AllowRefresh为false，表示用户登录后60分钟内不管有没有访问站点，只要60分钟到了，立马就处于非登录状态（不延长cookie在客户端计算机硬盘上的保留时间，60分钟到了客户端计算机就自动删除cookie）
                    /*
                    await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    user, new AuthenticationProperties()
                    {
                        IsPersistent = true,
                        ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(60),
                        AllowRefresh = true
                    });
                    */
                    #endregion
                }).Wait();
                return "200";
            }
            else
            {
                Task.Run(async () =>
                {
                    //注销登录的用户，相当于ASP.NET中的FormsAuthentication.SignOut  
                    await HttpContext.SignOutAsync();
                }).Wait();
                return "500";
            }
        }

        /// <summary>
        /// 该Action从Asp.Net Core中注销登录的用户
        /// </summary>
        public IActionResult Logout()
        {
            Task.Run(async () =>
            {
                //注销登录的用户，相当于ASP.NET中的FormsAuthentication.SignOut  
                await HttpContext.SignOutAsync();
            }).Wait();

            return Redirect("/users/login");
        }
        #endregion

        #region 页面

        /// <summary>
        /// 该Action判断用户是否已经登录，如果已经登录，那么读取登录用户的用户名
        /// </summary>
        public IActionResult Index()
        {
            ViewBag.token = GetToken();
            return View();
        }

        /// <summary>
        /// 该Action登录用户admin到Asp.Net Core
        /// </summary>
        [NoPermissionRequired]
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Welcome()
        {
            ViewBag.token = GetToken();
            return View();
        }

        public IActionResult P404()
        {
            return View();
        }

        public IActionResult Add()
        {
            ViewBag.token = GetToken();
            return View();
        }

        public IActionResult Cate()
        {
            ViewBag.token = GetToken();
            return View();
        }

        public IActionResult Edit()
        {
            ViewBag.token = GetToken();
            return View();
        }
        public IActionResult List()
        {
            ViewBag.token = GetToken();
            return View();
        }
        public IActionResult Role()
        {
            ViewBag.token = GetToken();
            return View();
        }
        public IActionResult Role_Add()
        {
            ViewBag.token = GetToken();
            return View();
        }
        public IActionResult PermissionList()
        {
            ViewBag.token = GetToken();
            return View();
        }

        #endregion
    }

    public class Token
    {
        public string token { get; set; }
    }
}