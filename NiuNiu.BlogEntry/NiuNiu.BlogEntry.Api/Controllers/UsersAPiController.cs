using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NiuNiu.BlogEntry.Api.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Cors;
    using NiuNiu.BlogEntry.Entity;
    using NiuNiu.BlogEntry.Entity.DTO;
    using NiuNiu.BlogEntry.IService;
    using System.Security.Claims;

    [Authorize]
    [EnableCors("AllowSameDomain")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsersAPiController : ControllerBase
    {
        public IUsersService iUsersService = null;

        public UsersAPiController(IUsersService _iUsersService)
        {
            iUsersService = _iUsersService;
        }

        [HttpPost]
        public PageResult<Users> Query(int pageIndex = 1, int pageSize = 10)
        {
            PageResult<Users> page = new PageResult<Users>();
            page.pageIndex = pageIndex;
            page.pageSize = pageSize;
           var result= iUsersService.Query(page);
            return result;
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Pwd"></param>
        /// <returns></returns>
        [HttpPost, AllowAnonymous]
        public Users Login(string Name,string Pwd)
        {
            var result = iUsersService.Login(Name, Pwd);
            return result;
        }
        [HttpPost]
        public bool Add(Users model)
        {
            var result = iUsersService.Add(model);
            return result;
        }
        [HttpPost]
        public bool Update(Users model)
        {
            var result = iUsersService.Update(model);
            return result;
        }
        [HttpPost]
        public bool Delete(string Ids)
        {
            var result = iUsersService.Delete(Ids);
            return result;
        }
        [HttpPost]
        public Users QueryById(int Id)
        {
            var result = iUsersService.QueryById(Id);
            return result;
        }

        [HttpGet]//启用jwt验证
        public IEnumerable<Book> Get()
        {
            var currentUser = HttpContext.User;
            int userAge = 0;
            var resultBookList = new Book[] {
      new Book { Author = "Ray Bradbury", Title = "Fahrenheit 451", AgeRestriction = false },
      new Book { Author = "Gabriel García Márquez", Title = "One Hundred years of Solitude", AgeRestriction = false },
      new Book { Author = "George Orwell", Title = "1984", AgeRestriction = false },
      new Book { Author = "Anais Nin", Title = "Delta of Venus", AgeRestriction = true }
    };
            //取出token中的claim信息并验证, 如果用户年龄<18岁, 则去掉一些R级内容?哈哈
            //if (currentUser.HasClaim(c => c.Type == ClaimTypes.DateOfBirth))
            //{
            //    DateTime birthDate = DateTime.Parse(currentUser.Claims.FirstOrDefault(c => c.Type == ClaimTypes.DateOfBirth).Value);
            //    userAge = DateTime.Today.Year - birthDate.Year;
            //}

            //if (userAge < 18)
            //{
            //    resultBookList = resultBookList.Where(b => !b.AgeRestriction).ToArray();
            //}

            return resultBookList;
        }

        public class Book
        {
            public string Author { get; set; }
            public string Title { get; set; }
            public bool AgeRestriction { get; set; }
        }
    }
}