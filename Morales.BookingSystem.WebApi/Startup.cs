using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.IServices;
using Core.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Morales.BookingSystem.Domain.IRepositories;
using Morales.BookingSystem.Domain.Services;
using Morales.BookingSystem.EntityFramework;
using Morales.BookingSystem.EntityFramework.Entities;
using Morales.BookingSystem.EntityFramework.Repositories;


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
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "Morales.BookingSystem.WebApi", Version = "v1"});
            });
            services.AddDbContext<MainDbContext>(opt =>
            {
                opt.UseSqlite("Data Source=main.db"); 
            });
            
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IAppointmentService, AppointmentService>();
            services.AddScoped<IAppointmentRepository, AppointmentRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, MainDbContext mainDbContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Morales.BookingSystem.WebApi v1"));

                #region Setup Contexts

                mainDbContext.Database.EnsureDeleted();
                mainDbContext.Database.EnsureCreated();
                mainDbContext.Accounts.Add(new AccountEntity
                {
                    Id = 1,
                    Email = "bob@bob.com",
                    Name = "Schwanz",
                    PhoneNumber = "3254566",
                    Sex = "Homie",
                    Type = "Customer"
                });
                mainDbContext.Accounts.Add(new AccountEntity
                {
                    Id = 2,
                    Email = "bob@schwanzmail.com",
                    Name = "Schwanz",
                    PhoneNumber = "32145487",
                    Sex = "Homie",
                    Type = "Customer"
                });
                mainDbContext.Accounts.Add(new AccountEntity
                {
                    Id = 3,
                    Email = "bob@swagmail.com",
                    Name = "Swag",
                    PhoneNumber = "71456588",
                    Sex = "Homie",
                    Type = "Owner"
                });
                mainDbContext.Accounts.Add(new AccountEntity
                {
                    Id = 4,
                    Email = "employee@bob.com",
                    Name = "Homie",
                    PhoneNumber = "658987",
                    Sex = "Homie",
                    Type = "Employee"
                });
                mainDbContext.Appointments.Add(new AppointmentEntity
                {
                    Id = 1, CustomerId = 1, EmployeeId = 4, Date = DateTime.Now, Duration = DateTime.UtcNow,
                    sex = "Yes Please"
                });
                mainDbContext.Appointments.Add(new AppointmentEntity
                {
                    Id = 2, CustomerId = 1, EmployeeId = 3, Date = DateTime.Now, Duration = DateTime.UtcNow,
                    sex = "Yes Please"
                });
                mainDbContext.SaveChanges();

                #endregion
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        
    }
    }
}