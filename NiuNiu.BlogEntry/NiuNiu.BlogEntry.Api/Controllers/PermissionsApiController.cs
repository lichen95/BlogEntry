using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NiuNiu.BlogEntry.Entity;
using NiuNiu.BlogEntry.Entity.DTO;
using NiuNiu.BlogEntry.IService;

namespace NiuNiu.BlogEntry.Api.Controllers
{
    [Authorize]
    [EnableCors("AllowSameDomain")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PermissionsApiController : ControllerBase
    {
        public IPermissionsService iPermissionsService = null;

        public PermissionsApiController(IPermissionsService _iPermissionsService)
        {
            iPermissionsService = _iPermissionsService;
        }

        [HttpPost]
        public PageResult<Permissions> Query(int pageIndex = 1, int pageSize = 10)
        {
            PageResult<Permissions> page = new PageResult<Permissions>();
            page.pageIndex = pageIndex;
            page.pageSize = pageSize;
            var result = iPermissionsService.Query(page);
            return result;
        }

        [HttpPost]
        public bool Add(Permissions model)
        {
            var result = iPermissionsService.Add(model);
            return result;
        }
        [HttpPost]
        public bool Update(Permissions model)
        {
            var result = iPermissionsService.Update(model);
            return result;
        }
        [HttpPost]
        public bool Delete(string Ids)
        {
            var result = iPermissionsService.Delete(Ids);
            return result;
        }
        [HttpPost]
        public Permissions QueryById(int Id)
        {
            var result = iPermissionsService.QueryById(Id);
            return result;
        }
    }
}