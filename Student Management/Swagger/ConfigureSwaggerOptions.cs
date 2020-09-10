using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.IO;
using System.Reflection;

namespace Student_Management.Swagger
{
    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider provider;

        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider) => this.provider = provider;

        public void Configure(SwaggerGenOptions options)
        {
            foreach (var desc in provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(
                    desc.GroupName, new Microsoft.OpenApi.Models.OpenApiInfo()
                    {
                        Title = "Education Management",
                        Version = "1",
                        Description = "The easier and simply way to manage education system",
                        Contact = new Microsoft.OpenApi.Models.OpenApiContact()
                        {
                            Name = "Muhammad Harith",
                            Email = "mhmmdhrth99@gmail.com",
                            Url = new Uri("https://linkedin.com/in/harith-jamdil-a500b5190")
                        },
                    });

                var xmlCommentFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlCommentFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentFile);

                options.IncludeXmlComments(xmlCommentFullPath);
            }
        }
    }
}