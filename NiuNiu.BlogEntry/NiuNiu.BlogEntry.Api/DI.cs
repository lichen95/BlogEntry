using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NiuNiu.BlogEntry.Api
{
    using IService;
    using Service;
    public static class DI
    {
        public static void AddScored(this IServiceCollection service)
        {
            //DAL层注入
            service.AddScoped<IUsersService, UsersService>();
            service.AddScoped<IPermissionsService, PermissionsService>();
        }
    }
}
