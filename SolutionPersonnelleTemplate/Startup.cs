using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SolutionPersonnelleTemplate.Data;
using SolutionPersonnelleTemplate.Models;
using SolutionPersonnelleTemplate.Services;
using SolutionPersonnelleTemplate.Models.BLL.Interfaces;
using SolutionPersonnelleTemplate.Models.BLL.Managers;
using SolutionPersonnelleTemplate.Models.BO;
using ReflectionIT.Mvc.Paging;
using PaulMiami.AspNetCore.Mvc.Recaptcha;

namespace SolutionPersonnelleTemplate
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            //Add mail methode personnelle
            services.Configure<EmailSettings>(Configuration.GetSection("EmailSettings"));
            // add utilisateur
            services.Configure<Utilisateur>(Configuration);

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();
            //Add pour le Captcha !  faut installer un nouveau package : PaulMiami.AspNetCore.Mvc.Recaptcha
            services.AddRecaptcha(new RecaptchaOptions
            {
                SiteKey = Configuration["Recaptcha:SiteKey"],
                SecretKey = Configuration["Recaptcha:SecretKey"],
                ValidationMessage = "Are you a robot?"
            });
            //Add utilisateur services
            services.AddScoped<IUtilisateurInterface, UtilisateurManager>();
            //Add application services for fichier
            services.AddScoped<IRepositoryFichier, FichierRepository>();
            //Add application services for histoire
            services.AddScoped<IRepositoryHistoire, HistoireRepository>();
            //Add application services for message
            services.AddScoped<IRepositoryMessage, MessageRepository>();

            services.AddMvc();
            //add pour la pagination
            services.AddPaging();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
