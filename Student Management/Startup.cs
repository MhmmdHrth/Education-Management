using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Student_Management.AutoMapper;
using Student_Management.Data;
using Student_Management.Repository;
using Student_Management.Repository.IRepository;

namespace Student_Management
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddDbContext<ApplicationDbContext>
                (options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            services.AddScoped<ITeacherRepository, TeacherRepository>();
            services.AddScoped<IStudentRepository, StudentRepository>();

            services.AddAutoMapper(typeof(ManagementMapper));

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("EducationSystemManagement", new Microsoft.OpenApi.Models.OpenApiInfo()
                {
                    Title = "Education Management",
                    Version = "1",
                    Description = "The easier and simply way to manage education system",
                    Contact = new Microsoft.OpenApi.Models.OpenApiContact()
                    {
                        Name = "Muhammad Harith",
                        Email = "mhmmdhrth99@gmail.com",
                        Url = new Uri("https://linkedin.com/in/harith-jamdil-a500b5190")
                    }
                });

                var xmlCommentFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlCommentFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentFile);

                options.IncludeXmlComments(xmlCommentFullPath);
            });
        }

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
                options.SwaggerEndpoint("/swagger/EducationSystemManagement/swagger.json", "EducationSystemManagement");
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
