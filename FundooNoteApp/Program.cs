using FundooManager.Interface;
using FundooManager.Manager;
using FundooRepository.Interface;
using FundooRepository.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using NLog.Web;
using NLog;

namespace FundooNoteApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
            logger.Debug("in it main");
            try
            {
                var builder = WebApplication.CreateBuilder(args);
                // Add services to the container.

                builder.Services.AddControllers();
                builder.Services.AddTransient<IUserRepository, UserRepository>();
                builder.Services.AddTransient<IUserManager, UserManager>();
                builder.Services.AddTransient<INotesRepository, NotesRepository>();
                builder.Services.AddTransient<INotesManager, NotesManager>();
                builder.Services.AddTransient<ILabelManager, LabelManager>();
                builder.Services.AddTransient<ILabelRepository, LabelRepository>();
                builder.Services.AddTransient<ICollaboratorManager, CollaboratorManager>();
                builder.Services.AddTransient<ICollaboratorRepository, CollaboratorRepository>();
                // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddSwaggerGen(s =>
                {
                    s.SwaggerDoc("v1", new OpenApiInfo { Title = "Welcome to FundoNotes" });
                    var jwtSecurityScheme = new OpenApiSecurityScheme
                    {
                        Scheme = "bearer",
                        BearerFormat = "JWT",
                        Name = "JWT Authentication",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.Http,
                        Description = "Enter JWT Bearer Token in Textbox For Authorization",

                        Reference = new OpenApiReference
                        {
                            Id = JwtBearerDefaults.AuthenticationScheme,
                            Type = ReferenceType.SecurityScheme
                        }
                    };
                    s.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

                    s.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                    { jwtSecurityScheme, Array.Empty<string>() },
                    });
                });
                var tokenKey = builder.Configuration.GetValue<string>("Jwt:key");
                var key = Encoding.ASCII.GetBytes(tokenKey);
                builder.Services.AddAuthentication(a =>
                {
                    a.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    a.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                }).AddJwtBearer(j =>
                {
                    j.RequireHttpsMetadata = false;
                    j.SaveToken = true;
                    j.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                    };
                });
                builder.Logging.ClearProviders();
                builder.Host.UseNLog();
                var app = builder.Build();

                // Configure the HTTP request pipeline.
                if (app.Environment.IsDevelopment())
                {
                    app.UseSwagger();
                    app.UseSwaggerUI();
                }
                if (!app.Environment.IsDevelopment())
                {
                    app.UseExceptionHandler("/Home/Error");
                    app.UseHsts();
                }
                app.UseAuthentication();

                app.UseHttpsRedirection();
                app.UseStaticFiles();

                app.UseRouting();

                app.UseAuthorization();

                app.MapControllers();
                app.MapControllerRoute(
                    name: "Admin",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                app.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                app.Run();
            }
            catch (Exception ex)
            {
                logger.Error(ex,"Program Stopped Beacuse of Exception");
                throw;
            }
            finally
            {
                NLog.LogManager.Shutdown();
            }
        }
    }
}