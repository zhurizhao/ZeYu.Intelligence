using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ZeYu.Intelligence.WebAPI.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using ZeYu.Intelligence.WebAPI.Data;
using Microsoft.EntityFrameworkCore;
//using MySQL.Data.EntityFrameworkCore.Extensions;
using Pomelo.EntityFrameworkCore.MySql;
using Microsoft.AspNetCore.Mvc.Formatters;
using System.Buffers;
using Newtonsoft.Json.Serialization;
using Microsoft.AspNetCore.Authorization;

// 解决json序列化时的循环引用问题
//config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
// 对 JSON 数据使用混合大小写。驼峰式,但是是javascript 首字母小写形式.
//config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new  CamelCasePropertyNamesContractResolver();
// 对 JSON 数据使用混合大小写。跟属性名同样的大小.输出
//config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new DefaultContractResolver();

namespace ZeYu.Intelligence.WebAPI
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

            services.AddOptions();
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            string defaultConnectionString = Configuration.GetConnectionString("DefaultConnection");// 获取mysql连接配置
            string db0ConnectionString = Configuration.GetConnectionString("DB0Connection");
            string dbStarCloudConnectionString = Configuration.GetConnectionString("DBStarCloudConnection"); 
            services.AddDbContext<DefaultContext>(option => option.UseMySql(defaultConnectionString));
            services.AddDbContext<DB0Context>(option => option.UseMySql(db0ConnectionString));
            services.AddDbContext<DBStarCloudContext>(option => option.UseMySql(dbStarCloudConnectionString));

            AppSettings.RedisConnection = Configuration.GetConnectionString("RedisConnection");


            //services.AddMemoryCache();
            services.AddMvc();
            services.AddDistributedRedisCache(options =>
            {
                options.Configuration = Configuration.GetConnectionString("RedisConnection");
                options.InstanceName = "TT";
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme) // 
                                         // .AddCookie(cfg => cfg.SlidingExpiration = true) //如果同时采用COOKIE认证可以取消注释
                    .AddJwtBearer(options => {
                        options.RequireHttpsMetadata = false;
                        options.SaveToken = true;
                        options.TokenValidationParameters =
                             new TokenValidationParameters
                             {
                                 ValidateIssuer = true,
                                 ValidateAudience = true,
                                 ValidateLifetime = true,
                                 ValidateIssuerSigningKey = true,

                                 ValidIssuer = Configuration["AuthenticationToken:Issuer"], // 发行者
                                 ValidAudience = Configuration["AuthenticationToken:Audience"], // 接收者
                                 IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["AuthenticationToken:SigningKey"]))
                                 //IssuerSigningKey =JwtSecurityKey.Create("60ab2b97251e33ff")
                              };
                        options.Events = new JwtBearerEvents
                        {
                            OnAuthenticationFailed = context =>
                            {
                                Console.WriteLine("OnAuthenticationFailed: " + context.Exception.Message);
                                return Task.CompletedTask;
                            },
                            OnTokenValidated = context =>
                            {
                                Console.WriteLine("OnTokenValidated: " + context.SecurityToken);
                                return Task.CompletedTask;
                            }
                        };
                    });
            //services.AddScoped<IAuthorizationHandler, ValidJtiHandler>();
            /*
            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser()
                    //.AddRequirements(new ValidJtiRequirement()) // 添加上面的验证要求
                    .Build());
            });
            */
            // 注册验证要求的处理器，可通过这种方式对同一种要求添加多种验证
            //services.AddSingleton<IAuthorizationHandler, ValidJtiHandler>();




            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });

            services.Configure<MvcOptions>(options =>
            {
                options.Filters.Add(new CorsAuthorizationFilterFactory("AllowSpecificOrigin"));
            });

            services.AddMvc().AddJsonOptions(options => { options.SerializerSettings.ContractResolver = new DefaultContractResolver(); });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();

            app.UseCors("AllowSpecificOrigin");
            app.UseStaticFiles();
            //var physicalFileSystem = new PhysicalFileSystem(webPath);  
            var options = new FileServerOptions
            {
                EnableDefaultFiles = true,
            };
            options.StaticFileOptions.ServeUnknownFileTypes = true;
            options.DefaultFilesOptions.DefaultFileNames = new[] { "index.html", "Index.html", "default.html", "Default.html" };
            app.UseFileServer(options);
            app.UseMvc();

            
           
        }
    }

    
}

