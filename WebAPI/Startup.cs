using Application.Courses.Commands;
using Application.Interfaces;
using AutoMapper;
using Domain;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Persistence;
using Persistence.Dapper;
using Persistence.Dapper.Teacher;
using Security;
using System.Text;
using WebAPI.Middleware;

namespace WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddDbContext<CoursesContext> (options => {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddOptions();
            services.Configure<ConnectionConfig>(Configuration.GetSection("ConnectionStrings"));

            services.AddMediatR(typeof(Query.Handler).Assembly);
            services.AddControllers(opt => {
                var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                opt.Filters.Add(new AuthorizeFilter(policy));
            })
                .AddFluentValidation(
                config => config.RegisterValidatorsFromAssemblyContaining<Create>());

            //Auth con Identity
            var builder = services.AddIdentityCore<User>();
            var identityBuilder = new IdentityBuilder(builder.UserType, builder.Services);
            identityBuilder.AddEntityFrameworkStores<CoursesContext>();
            identityBuilder.AddSignInManager<SignInManager<User>>();
            services.TryAddSingleton<ISystemClock, SystemClock>();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 4;
            });

            //Mapper
            services.AddAutoMapper(typeof(Query.Handler));
            // JWT
            services.AddScoped<IJwtGenerator, JwtGenerator>();
            services.AddScoped<IUserSession, UserSession>();
            // Repository 
            services.AddTransient<IFactoryConnection, FactoryConnection>();
            services.AddScoped<ITeacherRepository, TeacherRepository>();

            services.AddSwaggerGen(opt => {
                opt.SwaggerDoc("v1", new OpenApiInfo {
                    Title = "Servicios para mantenimiento de cursos",
                    Version = "v1"
                });
                // Toma las clases que reciben los datos desde los controladores junto con el namespace
                // para no confundirse al encontrar clases con el mismo nombre
                opt.CustomSchemaIds(s => s.FullName);
            });

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Mi llave secreta"));
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt => {
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = key,
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseMiddleware<ExceptionHandlerMiddleware>();

            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(opt => {
                opt.SwaggerEndpoint("v1/swagger.json", "Cursos online v2");
            });
        }
    }
}
