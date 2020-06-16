using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using CORE.API.Helpers;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using CORE.API.Persistence;
using Microsoft.EntityFrameworkCore;
using CORE.API.Core.IRepository;
using CORE.API.Persistence.Repository;
using AutoMapper;
using CORE.API.Core.Validator;
using FluentValidation.AspNetCore;
using FluentValidation;
using CORE.API.Core.Models;

namespace CORE.API
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
            services.AddCors();

            services.AddDbContext<AppDbContext>(x => x.UseSqlServer(Configuration.GetConnectionString("DbConnection")));

            services.AddAutoMapper(typeof(Startup));

            services.AddMvc().AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<DepartmentValidation>());

            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            services.AddScoped<IFileListRepository, FileListRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddControllers();
        }


        // services.AddMvc()
        // disable reference looping
        // .AddJsonOptions(opt =>
        // {
        //     opt.SerializerSettings.ReferenceLoopHandling =
        //     Newtonsoft.Json.ReferenceLoopHandling.Ignore;
        // })





        // net core compatibility setting
        // .SetCompatibilityVersion(CompatibilityVersion.Version_3_1);



        // // repository import and mapping
        // 

        // 
        // 

        // add jwt authentication
        // services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        //     .AddJwtBearer(options =>
        //     {
        //         options.TokenValidationParameters = new TokenValidationParameters
        //         {
        //             ValidateIssuerSigningKey = true,
        //             IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII
        //                       .GetBytes(Configuration.GetSection("AppSettings:Token").Value)),
        //             ValidateIssuer = false,
        //             ValidateAudience = false
        //         };
        //     });

        // register the Swagger generator, defining 1 or more Swagger documents
        // services.AddSwaggerGen(c =>
        // {
        //     c.SwaggerDoc("v1", new OpenApiInfo { Title = "CORE API", Version = "v1" });

        // });


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
























        // public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        // {
        //     if (env.EnvironmentName == "Development")
        //     {
        //         app.UseDeveloperExceptionPage();
        //     }
        //     else
        //     {
        //         // app.UseExceptionHandler("/Home/Error");
        //         // app.UseHsts();

        //         app.UseExceptionHandler(builder =>
        //         {
        //             builder.Run(async context =>
        //                 {
        //                     context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        //                     var error = context.Features.Get<IExceptionHandlerFeature>();
        //                     if (error != null)
        //                     {
        //                         context.Response.AddApplicationError(error.Error.Message);
        //                         await context.Response.WriteAsync(error.Error.Message);
        //                     }
        //                 });
        //         });
        //     }

        //     app.UseRouting();


        //     // add database error notification
        //     // app.UseDatabaseErrorPage();

        //     // enable middleware to serve generated Swagger as a JSON endpoint.
        //     // app.UseSwagger();

        //     // enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
        //     // specifying the Swagger JSON endpoint.
        //     // app.UseSwaggerUI(c =>
        //     // {
        //     //     c.SwaggerEndpoint("/swagger/v1/swagger.json", "CORE API end point v1");
        //     //     c.RoutePrefix = "docs";
        //     // });

        //     // app.UseHttpsRedirection();
        //     app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

        //     // use JWT authentication
        //     // app.UseAuthentication();

        //     app.UseEndpoints(endpoints =>
        //     {
        //         endpoints.MapRazorPages();
        //         endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
        //     });
        // }
    }
}
