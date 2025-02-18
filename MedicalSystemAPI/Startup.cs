using System.Text;
using MedicalSystemAPI.Filters;
using MedicalSystemAPI.Middleware;
using MedicalSystemModule.Interfaces.Services;
using MedicalSystemModule.Services;
using MedicalSystemModule.Utilities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Unchase.Swashbuckle.AspNetCore.Extensions.Extensions;

namespace MedicalSystemAPI
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureDependencies(IServiceCollection services)
        {
            services.AddScoped<IClinicServices, ClinicServices>();
            services.AddScoped<IClinicServiceServises, ClinicServiceServises>();
            services.AddScoped<IDoctorServices, DoctorServices>();
            services.AddScoped<IServiceServices, ServiceServices>();
            services.AddScoped<IWorkingHoursServices, WorkingHoursServices>();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddControllers();

            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1.0.0", new OpenApiInfo { Title = "Medical API— API Documentation", Version = "v1.0.0" });
                c.EnableAnnotations();
                c.CustomSchemaIds(type => type.ToString());
                c.AddEnumsWithValuesFixFilters();
                c.ParameterFilter<QueryArrayParamFilter>();
                c.IncludeXmlCommentsFromInheritDocs(true, typeof(string));

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. "
                                  + "Enter the authorization token obtained from the authentication endpoint.",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
        {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header,

                    },
                    new List<string>()
                }
        });
            });


            // Add JWT Authentication
            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration["AppSettings:Issuer"],
                        ValidAudience = Configuration["AppSettings:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["AppSettings:Secret"]))
                    };
                });



            // Other service configurations...
            services.AddControllers();
            services.AddSwaggerGen();

            ConfigureDependencies(services);
            services.AddHttpContextAccessor();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("v1.0.0/swagger.json", "Medical API v1.0.0");
                    c.InjectStylesheet("/Storage/swagger-ui.css");
                });
            }

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(env.ContentRootPath, "Storage")),
                RequestPath = "/Storage"
            });

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .WithExposedHeaders("X-Pagination-Total-Count", "X-Pagination-Current-Page", "X-Pagination-Total-Pages"));

            app.UseMiddleware<JwtMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

    }

}
