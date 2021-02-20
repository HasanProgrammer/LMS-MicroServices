using System;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using DataAccess.CustomRepositories;
using DataModel;
using DataService.RoleServices;
using DataService.UserServices;
using Hangfire;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using WebFramework.Exceptions;

namespace WebFramework.Extensions
{
    public static class IServiceCollectionExtension
    {
        public static IServiceCollection AddDatabaseContainer(this IServiceCollection service, IConfiguration configuration)
        {
            string ConnectionString = configuration.GetConnectionString("SQL Server");
            return service.AddDbContext<DatabaseContext>(Config => Config.UseSqlServer(ConnectionString));
        }
        
        public static IServiceCollection AddIocContainer(this IServiceCollection service)
        {
            //User's Service
            service.AddScoped<UserRepository<User>, UserService>();
            //User's Service
            
            //Role's Service
            service.AddScoped<RoleRepository<Role>, RoleService>();
            //Role's Service

            return service;
        }

        public static IServiceCollection AddAllServiceContainers(this IServiceCollection service, IConfiguration configuration)
        {
            //Global's Filter
            //service.AddScoped<ModelValidation>();
            
            /*-------------------------------------------------------Admin's Filter-------------------------------------------------------*/

            //User's Controller
            // service.AddScoped<Filters.Areas.Admin.UserController.CheckPassword>();
            // service.AddScoped<Filters.Areas.Admin.UserController.CheckUniqueEmail>();
            // service.AddScoped<Filters.Areas.Admin.UserController.CheckUniqueUserName>();
            // service.AddScoped<Filters.Areas.Admin.UserController.CheckUser>();
            // service.AddScoped<Filters.Areas.Admin.UserController.ImageUploader>();
            // service.AddScoped<Filters.Areas.Admin.UserController.RegisterHandling>();
            //User's Controller
            
            /*-------------------------------------------------------Admin's Filter-------------------------------------------------------*/

            return service;
        }

        public static IServiceCollection AddIdentityContainer(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddIdentity<User, Role>(options =>
            {
                options.Password.RequireDigit     = configuration.GetValue<bool>("Password:RequireDigit");
                options.Password.RequiredLength   = configuration.GetValue<int> ("Password:RequiredLength");
                options.Password.RequireLowercase = configuration.GetValue<bool>("Password:RequireLowercase");
                options.Password.RequireUppercase = configuration.GetValue<bool>("Password:RequireUppercase");
            })
            .AddEntityFrameworkStores<DatabaseContext>()
            .AddDefaultTokenProviders();
            
            return service;
        }

        public static IServiceCollection AddPureApiVersion(this IServiceCollection service)
        {
            service.AddApiVersioning();

            return service;
        }

        public static IServiceCollection AddJWTContainer(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddAuthentication().AddJwtBearer(Config =>
            {
                /*در این قسمت موقع ارسال درخواست به سرور ، موارد ( اطلاعات ) موجود در سرور با اطلاعات ارسالی ( توکن ) بررسی می گردد*/
                /*در صورت وجود هر گونه تفاوتی بین داده های تنظیم شده در سرور با اطلاعات ارسالی از سمت کاربر ( توکن ) ؛ اعتبارسنجی کاربر نامعتبر خواهد شد*/
                Config.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer      = configuration.GetValue<string>("JWT:Issuer"),   /*صادر کننده*/
                    ValidAudience    = configuration.GetValue<string>("JWT:Audience"), /*مصرف کننده*/
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes( configuration.GetValue<string>("JWT:Key") )),
                    
                    ValidateIssuer           = true,
                    ValidateAudience         = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime         = true
                };
            
                /*در این قسمت بررسی می شود که در صورت بروز هر گونه خطایی از سمت سرور ، چه واکنش مناسبی به کلاینت ( کاربر ) ارسال گردد*/
                Config.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        /*در این قسمت ؛ صحت توکن ارسالی کاربر بررسی می گردد و در صورت نادرست بودن توکن ارسالی ؛ خطای مناسب برای کاربر صادر می گردد*/
                        if (context.Exception.GetType() == typeof(SecurityTokenSignatureKeyNotFoundException)) throw new TokenNotValidException();
                            
                        /*در این قسمت ؛ دلیل عدم موفقیت آمیز بودن احراز هویت ، منقضی شدن زمان توکن می باشد که باید در این صورت خطای مناسب به کاربر صادر گردد*/
                        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException)) throw new TokenExpireException();
                        
                        return Task.CompletedTask;
                    }
                    
                    ,
                    
                    /*این قسمت مربوط به سطوح دسترسی یا همان ACL می باشد*/
                    OnForbidden = context => throw new UnAuthorizedException()
                    
                    ,
                    
                    OnChallenge = delegate(JwtBearerChallengeContext context)
                    {
                        if (context.AuthenticateFailure != null) throw new AuthenticationFaildException();
                        return Task.CompletedTask;
                    }
                };
            });
            
            return service;
        }
        
        public static IServiceCollection AddTaskSchedulingContainer(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddHangfire
            (
                config => config.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                                .UseSimpleAssemblyNameTypeSerializer()
                                .UseRecommendedSerializerSettings()
                                .UseSqlServerStorage(configuration.GetConnectionString("SQL Server"))
            );

            service.AddHangfireServer();

            return service;
        }
        
        public static IServiceCollection AddCorsContainer(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddCors(Option =>
            {
                Option.AddPolicy("CORS", Builder =>
                {
                    Builder.WithOrigins(configuration.GetValue<string>("ClientURL:HomeSite"))
                           .WithOrigins(configuration.GetValue<string>("ClientURL:AdminPanel"))
                           .AllowAnyHeader()
                           .AllowAnyMethod()
                           /*.WithExposedHeaders("Content-Disposition")*/ /*این یک Header استاندارد است که برای دانلود فایل مورد استفاده قرار میگیرد*/
                           .WithExposedHeaders("X-Video-Name")            /*این یک Header شخصی است که برای ارسال نام فایلی که قرار است دانلود گردد مورد استفاده قرار می گیرد*/
                           .WithExposedHeaders("X-Pagination");           /*این یک Header شخصی است که برای ارسال داده ها به شکل صفحه بندی شده مورد استفاده قرار می گیرد*/
                });
            });
            
            return service;
        }

        public static IServiceCollection AddSessionContainer(this IServiceCollection service, IConfiguration configuration)
        {
            // service.AddDistributedMemoryCache();
            // service.AddSession(Config =>
            // {
            //     Config.IdleTimeout = TimeSpan.FromMinutes(configuration.GetValue<int>("Session:Timer"));
            // });
            // service.AddMvc();

            return service;
        }

        public static IServiceCollection AddConfigurationsContainer(this IServiceCollection service, IConfiguration configuration)
        {
            // service.Configure<Config.StatusCode>(config: configuration.GetSection("StatusCode"))
            //        .Configure<Config.Messages>  (config: configuration.GetSection("Messages"))
            //        .Configure<Config.JWT>       (config: configuration.GetSection("JWT"))
            //        .Configure<Config.Password>  (config: configuration.GetSection("Password"))
            //        .Configure<Config.File>      (config: configuration.GetSection("File"))
            //        .Configure<Config.ZarinPal>  (config: configuration.GetSection("ZarinPal"))
            //        .Configure<Config.AdminData> (config: configuration.GetSection("AdminData"))
            //        .Configure<Config.ClientURL> (config: configuration.GetSection("ClientURL"))
            //        .Configure<Config.Mail>      (config: configuration.GetSection("SMTP"));
            
            return service;
        }
    }
}