using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Store.DATA.Repositories;
using Store.DATA.Models;
using Store.CORE.EF_dbContext;
using Store.CORE.Repo_App;
using Store.AUTHETICATION.Services;
using Store.AUTHETICATION.Interfaces;
using Swashbuckle.AspNetCore.Swagger;
//using Swashbuckle.Swagger;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;

namespace Store.API.Config
{
    public static class ServiceConfiguration
    {
        //Подключаем репозитории и их реализацию
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services
                .AddTransient<IUserRepository, UserRepository>()
                .AddTransient<ICustomerRepository, CustomerRepository>()
                .AddTransient<IItemRepository, ItemRepository>()
                .AddTransient<IOrderRepository, OrderRepository>()
                .AddTransient<IOrderItemRepository, OrderItemRepository>();

            return services;
        }

        //CORS запросы, чтобы можно было слать запросы со стороннего хоста
        public static IServiceCollection AddCorsConfig(this IServiceCollection services)
        {
            services.AddCors(config =>
            {
                config.AddPolicy("AllowAll", new Microsoft.AspNetCore.Cors.Infrastructure.CorsPolicyBuilder()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    //.AllowAnyOrigin()
                    .AllowCredentials()
                    .SetIsOriginAllowed(hostName => true)
                    .Build());
            });
            return services;
        }

        //Swager connect
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "StoreHorse.API", Version = "v1" });
            });
            return services;
        }

        //Подлкючим сервисы аутентификации
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services
                .AddTransient<IJwtService, JwtGenerator>()
                .AddTransient<IAuthService, AuthService>();

            return services;
        }

        //Настройки Identity
        public static IServiceCollection ConfigureIdentity(this IServiceCollection services)
        {
            //более щадящие требования к паролю
            services.AddIdentity<Customer, IdentityRole<Guid>>(opt =>
            {
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireLowercase = false;
            }).AddEntityFrameworkStores<ApplicationContext>();

            return services;
        }
    }
}
