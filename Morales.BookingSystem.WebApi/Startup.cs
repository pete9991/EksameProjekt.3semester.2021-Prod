using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.IServices;
using Core.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Morales.BookingSystem.Domain.IRepositories;
using Morales.BookingSystem.Domain.Services;
using Morales.BookingSystem.EntityFramework;
using Morales.BookingSystem.EntityFramework.Entities;
using Morales.BookingSystem.EntityFramework.Repositories;
using Morales.BookingSystem.Middleware;
using Morales.BookingSystem.PolicyHandlers;
using Morales.BookingSystem.Security;
using Morales.BookingSystem.Security.Models;
using Morales.BookingSystem.Security.Services;


namespace Morales.BookingSystem
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
            services.AddControllers();
            services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc("v1", new OpenApiInfo {Title = "Morales.BookingSystem.WebApi", Version = "v1"});
                opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description =
                        "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\""
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
                        new string[] { }
                    }
                });
            });
            services.AddDbContext<MainDbContext>(opt =>
            {
                opt.UseSqlite("Data Source=main.db"); 
            });
            services.AddDbContext<AuthDbContext>(opt =>
            {
                opt.UseSqlite("Data Source=auth.db");
            });
            
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IAppointmentService, AppointmentService>();
            services.AddScoped<IAppointmentRepository, AppointmentRepository>();
            services.AddScoped<ITreatmentService, TreatmentService>();
            services.AddScoped<ITreatmentRepository, TreatmentRepository>();
            services.AddScoped<IAuthService, AuthService>();

            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    ValidAudience = Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                };
            });
            services.AddSingleton<IAuthorizationHandler, AdminHandler>();
            services.AddSingleton<IAuthorizationHandler, EmployeeHandler>();
            services.AddSingleton<IAuthorizationHandler, CustomerHandler>();
            services.AddAuthorization(options =>
            {
                options.AddPolicy(nameof(AdminHandler),
                    policy => policy.Requirements.Add(new AdminHandler()));
                options.AddPolicy(nameof(EmployeeHandler),
                    policy => policy.Requirements.Add(new EmployeeHandler()));
                options.AddPolicy(nameof(CustomerHandler),
                    policy => policy.Requirements.Add(new CustomerHandler()));

            });
            services.AddCors(opt => opt
                .AddPolicy("dev-policy", policy =>
                {
                    policy
                        .AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                }));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, MainDbContext mainDbContext, AuthDbContext authDbContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Morales.BookingSystem.WebApi v1"));
                app.UseCors("dev-policy");

                #region Setup Contexts

                mainDbContext.Database.EnsureDeleted();
                mainDbContext.Database.EnsureCreated();
                authDbContext.Database.EnsureDeleted();
                authDbContext.Database.EnsureCreated();

                #region AuthSeeding

                authDbContext.LoginUsers.Add(new LoginUser
                {
                    Id = 1,
                    UserName = "88888888",
                    HashedPassword = "mj2aUQ9UDGTm/aFdMVWoNIIVg0IwYent+qMo9aPt3Bc=",
                    AccountId = 1,
                    Salt = "test1234"
                });
                authDbContext.LoginUsers.Add(new LoginUser
                {
                    Id = 2,
                    UserName = "11111111",
                    HashedPassword = "PBz+Tp8l33n1+lD8a2fUpGDB5Fp4y+EBDvej3uChvEA=",
                    AccountId = 2,
                    Salt = "Test4321"
                });
                authDbContext.LoginUsers.Add(new LoginUser
                {
                    Id = 3,
                    UserName = "58889999",
                    HashedPassword = "oB/g944gOnJFXFkZy/DXtjAzwziEyOoM4HJ8c2pWwJM=",
                    AccountId = 3,
                    Salt = "OUFOIUNXHP"
                });
                authDbContext.Permissions.AddRange(new Permission
                {
                    Id = 1,
                    Name = "Owner"
                }, new Permission
                {
                    Id = 2,
                    Name = "Employee"
                }, new Permission
                {
                    Id = 3,
                    Name = "Customer"
                });
                authDbContext.UserPermissions.Add(new UserPermission
                {
                    UserID = 1,
                    PermissionId = 3
                });
                authDbContext.UserPermissions.Add(new UserPermission
                {
                    UserID = 2,
                    PermissionId = 1
                });
                authDbContext.UserPermissions.Add(new UserPermission
                {
                    UserID = 2,
                    PermissionId = 2
                });
                authDbContext.UserPermissions.Add(new UserPermission
                {
                    UserID = 2,
                    PermissionId = 3
                });
                authDbContext.UserPermissions.Add(new UserPermission
                {
                    UserID = 3,
                    PermissionId = 3
                });
                authDbContext.SaveChanges();

                #endregion

                #region Account Seeding
                mainDbContext.Accounts.Add(new AccountEntity
                {
                    Id = 1,
                    Email = "bob@bob.com",
                    Name = "Schwanz",
                    PhoneNumber = "88888888",
                    Sex = "Male",
                    Type = "Customer"
                });
                mainDbContext.Accounts.Add(new AccountEntity
                {
                    Id = 2,
                    Email = "Karl@schwanzmail.com",
                    Name = "Karl",
                    PhoneNumber = "11111111",
                    Sex = "Male",
                    Type = "Employee"
                });
                mainDbContext.Accounts.Add(new AccountEntity
                {
                    Id = 3,
                    Email = "bob@bob.com",
                    Name = "Lise",
                    PhoneNumber = "58889999",
                    Sex = "Female",
                    Type = "Customer"
                });
                #endregion

                #region Treatment Seeding

                mainDbContext.Treatments.Add(new TreatmentEntity {Id = 1, Duration = new TimeSpan(0,30,0), Name = "Herre Klip",Price = 275});
                mainDbContext.Treatments.Add(new TreatmentEntity {Id = 2, Duration = new TimeSpan(0,30,0), Name = "Dame Klip",Price = 345});
                mainDbContext.Treatments.Add(new TreatmentEntity {Id = 3, Duration = new TimeSpan(0,30,0), Name = "Børneklip",Price = 225});
                mainDbContext.Treatments.Add(new TreatmentEntity {Id = 4, Duration = new TimeSpan(0,30,0), Name = "Balayage inkl.toning",Price = 1800});
                mainDbContext.Treatments.Add(new TreatmentEntity {Id = 5, Duration = new TimeSpan(0,30,0), Name = "Babylights",Price = 1300});
                mainDbContext.Treatments.Add(new TreatmentEntity {Id = 6, Duration = new TimeSpan(0,30,0), Name = "Toning",Price = 500});
                mainDbContext.Treatments.Add(new TreatmentEntity {Id = 7, Duration = new TimeSpan(0,30,0), Name = "Dame kli og farve",Price = 925});
                mainDbContext.Treatments.Add(new TreatmentEntity {Id = 8, Duration = new TimeSpan(0,30,0), Name = "Dame Reflekser og klip",Price = 1050});
                mainDbContext.Treatments.Add(new TreatmentEntity {Id = 9, Duration = new TimeSpan(0,30,0), Name = "Bryn og Vipper",Price = 275});
                mainDbContext.Treatments.Add(new TreatmentEntity {Id = 10, Duration = new TimeSpan(0,30,0), Name = "Olaplex behandling",Price = 550});
                mainDbContext.Treatments.Add(new TreatmentEntity {Id = 11, Duration = new TimeSpan(0,30,0), Name = "Kurbehandling ved farve",Price = 75});
                

                #endregion

                #region appointmentSeeding
                mainDbContext.Appointments.Add(new AppointmentEntity
                {
                    Id = 1, CustomerId = 1, EmployeeId = 2, Date = DateTime.Now.AddHours(1), Duration = new TimeSpan(1,0,0),
                    AppointmentEnd = DateTime.Now.AddHours(2), 
                    
                });
                mainDbContext.Appointments.Add(new AppointmentEntity
                {
                    Id = 2, CustomerId = 1, EmployeeId = 2, Date = DateTime.Now.AddHours(22), Duration = new TimeSpan(0,30,0),
                    AppointmentEnd = DateTime.Now.AddHours(24)
                });
                mainDbContext.Appointments.Add(new AppointmentEntity
                {
                    Id = 3, CustomerId = 1, EmployeeId = 2, Date = DateTime.Now.AddHours(40), Duration = new TimeSpan(0,30,0),
                    AppointmentEnd = DateTime.Now.AddHours(42)
                });
                mainDbContext.Appointments.Add(new AppointmentEntity
                {
                    Id = 4, CustomerId = 3, EmployeeId = 2, Date = DateTime.Now.AddHours(2), Duration = new TimeSpan(0,30,0),
                    AppointmentEnd = DateTime.Now.AddHours(3)
                });
                mainDbContext.SaveChanges();
                #endregion

                #region appointmentTreatment Seeding

                mainDbContext.Add(new AppointmentTreatmentEntity()
                {
                    AppointmentId = 1,
                    TreatmentId = 1
                });
                mainDbContext.Add(new AppointmentTreatmentEntity()
                {
                    AppointmentId = 3,
                    TreatmentId = 1
                });

                mainDbContext.SaveChanges();

                #endregion

                #endregion
            }
            else
            {
                 #region Setup Contexts

                mainDbContext.Database.EnsureDeleted();
                mainDbContext.Database.EnsureCreated();
                authDbContext.Database.EnsureDeleted();
                authDbContext.Database.EnsureCreated();

                #region AuthSeeding

                authDbContext.LoginUsers.Add(new LoginUser
                {
                    Id = 1,
                    UserName = "88888888",
                    HashedPassword = "mj2aUQ9UDGTm/aFdMVWoNIIVg0IwYent+qMo9aPt3Bc=",
                    AccountId = 1,
                    Salt = "test1234"
                });
                authDbContext.LoginUsers.Add(new LoginUser
                {
                    Id = 2,
                    UserName = "11111111",
                    HashedPassword = "PBz+Tp8l33n1+lD8a2fUpGDB5Fp4y+EBDvej3uChvEA=",
                    AccountId = 2,
                    Salt = "Test4321"
                });
                authDbContext.LoginUsers.Add(new LoginUser
                {
                    Id = 3,
                    UserName = "58889999",
                    HashedPassword = "oB/g944gOnJFXFkZy/DXtjAzwziEyOoM4HJ8c2pWwJM=",
                    AccountId = 3,
                    Salt = "OUFOIUNXHP"
                });
                authDbContext.Permissions.AddRange(new Permission
                {
                    Id = 1,
                    Name = "Owner"
                }, new Permission
                {
                    Id = 2,
                    Name = "Employee"
                }, new Permission
                {
                    Id = 3,
                    Name = "Customer"
                });
                authDbContext.UserPermissions.Add(new UserPermission
                {
                    UserID = 1,
                    PermissionId = 3
                });
                authDbContext.UserPermissions.Add(new UserPermission
                {
                    UserID = 2,
                    PermissionId = 1
                });
                authDbContext.UserPermissions.Add(new UserPermission
                {
                    UserID = 2,
                    PermissionId = 2
                });
                authDbContext.UserPermissions.Add(new UserPermission
                {
                    UserID = 2,
                    PermissionId = 3
                });
                authDbContext.UserPermissions.Add(new UserPermission
                {
                    UserID = 3,
                    PermissionId = 3
                });
                authDbContext.SaveChanges();

                #endregion

                #region Account Seeding
                mainDbContext.Accounts.Add(new AccountEntity
                {
                    Id = 1,
                    Email = "bob@bob.com",
                    Name = "Schwanz",
                    PhoneNumber = "88888888",
                    Sex = "Male",
                    Type = "Customer"
                });
                mainDbContext.Accounts.Add(new AccountEntity
                {
                    Id = 2,
                    Email = "Karl@schwanzmail.com",
                    Name = "Karl",
                    PhoneNumber = "11111111",
                    Sex = "Male",
                    Type = "Employee"
                });
                mainDbContext.Accounts.Add(new AccountEntity
                {
                    Id = 3,
                    Email = "bob@bob.com",
                    Name = "Lise",
                    PhoneNumber = "58889999",
                    Sex = "Female",
                    Type = "Customer"
                });
                #endregion

                #region Treatment Seeding

                mainDbContext.Treatments.Add(new TreatmentEntity {Id = 1, Duration = new TimeSpan(0,30,0), Name = "Herre Klip",Price = 275});
                mainDbContext.Treatments.Add(new TreatmentEntity {Id = 2, Duration = new TimeSpan(0,30,0), Name = "Dame Klip",Price = 345});
                mainDbContext.Treatments.Add(new TreatmentEntity {Id = 3, Duration = new TimeSpan(0,30,0), Name = "Børneklip",Price = 225});
                mainDbContext.Treatments.Add(new TreatmentEntity {Id = 4, Duration = new TimeSpan(0,30,0), Name = "Balayage inkl.toning",Price = 1800});
                mainDbContext.Treatments.Add(new TreatmentEntity {Id = 5, Duration = new TimeSpan(0,30,0), Name = "Babylights",Price = 1300});
                mainDbContext.Treatments.Add(new TreatmentEntity {Id = 6, Duration = new TimeSpan(0,30,0), Name = "Toning",Price = 500});
                mainDbContext.Treatments.Add(new TreatmentEntity {Id = 7, Duration = new TimeSpan(0,30,0), Name = "Dame kli og farve",Price = 925});
                mainDbContext.Treatments.Add(new TreatmentEntity {Id = 8, Duration = new TimeSpan(0,30,0), Name = "Dame Reflekser og klip",Price = 1050});
                mainDbContext.Treatments.Add(new TreatmentEntity {Id = 9, Duration = new TimeSpan(0,30,0), Name = "Bryn og Vipper",Price = 275});
                mainDbContext.Treatments.Add(new TreatmentEntity {Id = 10, Duration = new TimeSpan(0,30,0), Name = "Olaplex behandling",Price = 550});
                mainDbContext.Treatments.Add(new TreatmentEntity {Id = 11, Duration = new TimeSpan(0,30,0), Name = "Kurbehandling ved farve",Price = 75});
                

                #endregion

                #region appointmentSeeding
                mainDbContext.Appointments.Add(new AppointmentEntity
                {
                    Id = 1, CustomerId = 1, EmployeeId = 2, Date = DateTime.Now.AddHours(1), Duration = new TimeSpan(1,0,0),
                    AppointmentEnd = DateTime.Now.AddHours(2), 
                    
                });
                mainDbContext.Appointments.Add(new AppointmentEntity
                {
                    Id = 2, CustomerId = 1, EmployeeId = 2, Date = DateTime.Now.AddHours(22), Duration = new TimeSpan(0,30,0),
                    AppointmentEnd = DateTime.Now.AddHours(24)
                });
                mainDbContext.Appointments.Add(new AppointmentEntity
                {
                    Id = 3, CustomerId = 1, EmployeeId = 2, Date = DateTime.Now.AddHours(40), Duration = new TimeSpan(0,30,0),
                    AppointmentEnd = DateTime.Now.AddHours(42)
                });
                mainDbContext.Appointments.Add(new AppointmentEntity
                {
                    Id = 4, CustomerId = 3, EmployeeId = 2, Date = DateTime.Now.AddHours(2), Duration = new TimeSpan(0,30,0),
                    AppointmentEnd = DateTime.Now.AddHours(3)
                });
                mainDbContext.SaveChanges();
                #endregion

                #region appointmentTreatment Seeding

                mainDbContext.Add(new AppointmentTreatmentEntity()
                {
                    AppointmentId = 1,
                    TreatmentId = 1
                });
                mainDbContext.Add(new AppointmentTreatmentEntity()
                {
                    AppointmentId = 3,
                    TreatmentId = 1
                });
                mainDbContext.Add(new AppointmentTreatmentEntity()
                {
                    AppointmentId = 2,
                    TreatmentId = 1
                });
                mainDbContext.Add(new AppointmentTreatmentEntity()
                {
                    AppointmentId = 2,
                    TreatmentId = 2
                });

                mainDbContext.SaveChanges();

                #endregion

                #endregion
            }


            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseMiddleware<JWTMiddleware>();
            
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        
    }
    }
}