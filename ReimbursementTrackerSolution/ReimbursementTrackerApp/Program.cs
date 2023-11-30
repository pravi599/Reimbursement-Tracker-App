using ReimbursementTrackerApp.Services;
using ReimbursementTrackerApp.Models;
using ReimbursementTrackerApp.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ReimbursementTrackerApp.Contexts;
using Microsoft.OpenApi.Models;
using ReimbursementTrackerApp.Interfaces;

namespace ReimbursementTrackerApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            #region Swagger
            builder.Services.AddSwaggerGen(opt =>
            {
                opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme."
                });

                opt.AddSecurityRequirement(new OpenApiSecurityRequirement
                 {
                     {
                           new OpenApiSecurityScheme
                             {
                                 Reference = new OpenApiReference
                                 {
                                     Type = ReferenceType.SecurityScheme,
                                     Id = "Bearer"
                                 }
                             },
                             new string[] {}

                     }
                 });
            });
            #endregion
            
            #region CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("reactApp", opts =>
                {
                    opts.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin();
                });
            });
            #endregion

            #region Uitility

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["SecretKey"])),
                        ValidateIssuerSigningKey = true
                    };
                });

            builder.Services.AddDbContext<RTAppContext>(opts =>
            {
                opts.UseSqlServer(builder.Configuration.GetConnectionString("reimbursementCon"));
            });

            builder.Logging.AddLog4Net();
            #endregion

            #region UserDefinedServices

            builder.Services.AddScoped<IRepository<string, User>, UserRepository>();
            builder.Services.AddScoped<IRepository<int, Request>, RequestRepository>();
            builder.Services.AddScoped<IRepository<int, Tracking>, TrackingRepository>();
            builder.Services.AddScoped<IRepository<int, UserProfile>, UserProfileRepository>();
            builder.Services.AddScoped<IRepository<int, PaymentDetails>, PaymentDetailsRepository>();


            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<ITokenService, TokenService>();
            builder.Services.AddScoped<IRequestService, RequestService>();
            builder.Services.AddScoped<ITrackingService ,TrackingService>();
            builder.Services.AddScoped<IUserProfileService, UserProfileService>();
            builder.Services.AddScoped<IPaymentDetailsService, PaymentDetailsService>();


            #endregion

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();

            }
            app.UseStaticFiles();

            app.UseRouting();


            app.UseCors("reactApp");
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
