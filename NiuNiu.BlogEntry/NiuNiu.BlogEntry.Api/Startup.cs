using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using NiuNiu.BlogEntry.Api;
using Microsoft.AspNetCore.Http;
using System.Text;

namespace NiuNiu.BlogEntry.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IConfiguration>(Configuration);
            services.AddMemoryCache();//添加基于内存的缓存支持

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,//validate the server
                ValidateAudience = true,//ensure that the recipient of the token is authorized to receive it 
                ValidateLifetime = true,//check that the token is not expired and that the signing key of the issuer is valid 
                ValidateIssuerSigningKey = true,//verify that the key used to sign the incoming token is part of a list of trusted keys
                ValidIssuer = Configuration["Jwt:Issuer"],//appsettings.json文件中定义的Issuer
                ValidAudience = Configuration["Jwt:Issuer"],//appsettings.json文件中定义的Audience
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
            };//appsettings.json文件中定义的JWT Key
        });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddScored();//使用DI注入

            //配置跨域处理，允许所有来源：
            //services.AddCors(options =>
            //options.AddPolicy("AllowSameDomain",
            //p => p.AllowAnyOrigin())
            //);
            //允许一个或多个具体来源:
            services.AddCors(options =>
            {
                // Policy 名稱 CorsPolicy 是自訂的，可以自己改
                //跨域规则的名称
                options.AddPolicy("AllowSameDomain", policy =>
                {
                    // 設定允許跨域的來源，有多個的話可以用 `,` 隔開
                    policy.WithOrigins("http://localhost:62249", "http://127.0.0.1:62249")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    //.AllowAnyOrigin()//允许所有来源的主机访问
                    .AllowCredentials();
                });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseAuthentication();//配置授权
            //                        //处理异常
            //app.UseStatusCodePages(new StatusCodePagesOptions()
            //{
            //    HandleAsync = (context) =>
            //    {
            //        if (context.HttpContext.Response.StatusCode == 401)
            //        {
            //            using (System.IO.StreamWriter sw = new System.IO.StreamWriter(context.HttpContext.Response.Body))
            //            {
            //                sw.Write(Newtonsoft.Json.JsonConvert.SerializeObject(new
            //                {
            //                    status = 401,
            //                    message = "access denied!",
            //                }));
            //            }
            //        }
            //        return System.Threading.Tasks.Task.Delay(0);
            //    }
            //});




            app.UseCors("AllowSameDomain");//必须位于UserMvc之前 
            app.UseMvc();
        }
    }
}
