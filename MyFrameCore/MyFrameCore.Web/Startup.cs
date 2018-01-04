using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyFrameCore.BLL;
using MyFrameCore.Model;
using Microsoft.AspNetCore.Diagnostics;
using System.Text;

namespace MyFrameCore.Web
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration; 
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            //注入
            services.AddScoped<Sys_UserBLL>();
            services.AddScoped<Sys_DictionaryBLL>();
            services.AddScoped<Sys_MenuBLL>();
            services.AddScoped<Sys_RoleBLL>();
            services.AddScoped<Sys_ButtonBLL>();
            services.AddOptions();
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
            //Session服务
            services.AddSession();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();//注入
            

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //开发环境异常处理
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                //生产环境异常处理
                app.UseExceptionHandler("/Content/HttpError/500.html");
            }

            //使用StatusCodePagesMiddleware 指定状态对应的显示页面
            Func<StatusCodeContext, Task> handler = async context =>
            {
                var resp = context.HttpContext.Response;
                resp.ContentType = "text/plain;charset=utf-8";
                if (resp.StatusCode == 404)
                {
                    //需要 using Microsoft.AspNetCore.Http;
                    await resp.WriteAsync("404，此页面路径无效！");
                }
            };
            app.UseStatusCodePages(handler);
            //使用静态文件
            app.UseStaticFiles();
            //使用Session中间件
            app.UseSession();
            //mvc路由规则
            app.UseMvc(routes =>
            {
                //区域
                routes.MapRoute(
                    name: "area",
                    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                //默认
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
