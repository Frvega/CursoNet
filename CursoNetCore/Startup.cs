using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CursoNetCore.Infrestuctura;
using CursoNetCore.Mapper;
using CursoNetCore.Repository;
using CursoNetCore.Repository.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace CursoNetCore
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
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddDbContext<CatalogoContext>(Options => Options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddAutoMapper(typeof(CategoriaMapper));
            services.AddAutoMapper(typeof(UsuarioMapper));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.GetSection("AppSettings:TokenKey").Value)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                };
            });

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("CatalogosApi", new OpenApiInfo()
                {
                    Title = "Catalogos Generales",
                    Description = "Contiene los acatalogos genericos de aplicaciones",
                    Version = "1.0",
                    Contact = new Microsoft.OpenApi.Models.OpenApiContact()
                    {
                        Email = "email",
                        Name = "nombre",

                    },
                    License = new Microsoft.OpenApi.Models.OpenApiLicense()
                    {
                        Name = "licencia",
                        Url = new Uri("http://bsd.axsis.com")
                    }
                });
                var XMLComentarios = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var ApiRutaComentarios = Path.Combine(AppContext.BaseDirectory, XMLComentarios);
                options.IncludeXmlComments(ApiRutaComentarios);
            });



            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/CatalogosApi/swagger.json", "API Usuarios");
                options.RoutePrefix = string.Empty;

            });

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
