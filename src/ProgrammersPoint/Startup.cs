using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ProgrammersPoint.Interfaces;
using ProgrammersPoint.Models.MSSQLContext;
using ProgrammersPoint.Repositories;
using Swashbuckle.AspNetCore.Swagger;

namespace ProgrammersPoint
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("EditPolicy", policyBuilder =>
                {
                    policyBuilder.RequireAuthenticatedUser()
                        .RequireAssertion(context => context.User.HasClaim("Beheerder", "true"))
                        .Build();
                });
            });

            // Add framework services.
            services.AddMvc();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "ProgrammersPoint API", Version = "v1" });
            });

            //Instantieer een GebruikerMSSQLContext en geef deze door bij een aanvraag in een constructor
            services.AddScoped<IGebruikerContext, GebruikerMSSQLContext>();

            services.AddScoped<IPostContext, PostMSSQLContext>();

            services.AddScoped<ReviewMSSQLContext>();
            services.AddScoped<IReviewContext, ReviewRepository>();

            services.AddScoped<ReviewWaarderingMSSQLContext>();
            services.AddScoped<IReviewWaarderingContext, ReviewWaarderingRepository>();

            services.AddScoped<ScheldwoordMSSQLContext>();
            services.AddScoped<IScheldwoordContext, ScheldwoordRepository>();

            services.AddScoped<CategorieMSSQLContext>();
            services.AddScoped<ICategorieContext, CategorieRepository>();

            services.AddScoped<ReactieMSSQLContext>();
            services.AddScoped<IReactieContext, ReactieRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Errors");
                app.UseStatusCodePagesWithReExecute("/Errors/Error/{0}");
            }

            app.UseStaticFiles();

            app.UseCookieAuthentication(new CookieAuthenticationOptions()
            {
                AuthenticationScheme = "CookieAuthenticationScheme",
                LoginPath = new PathString("/Errors/Unauthorized/"),
                AccessDeniedPath = new PathString("/Errors/Forbidden/"),
                AutomaticAuthenticate = true,
                AutomaticChallenge = true
            });

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
