using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ControleContas.Repository.Interfaces;
using ControleContas.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleContas.Presentation
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
            //definir o padrão de navegação do projeto (CONTROLLER/VIEW)
            services.AddControllersWithViews();

            //Mapear o tipo de autenticação que será utilizado no projeto (Authentication Scheme)
            //Autenticação por meio de Cookies
            services.Configure<CookiePolicyOptions>(options => { options.MinimumSameSitePolicy = SameSiteMode.None; });
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();

            //ler a connectionstring mapeada no arquivo /appsettings.json
            var connectionstring = Configuration.GetConnectionString("ProjetoContas");

            //configurando uma injeção de dependência (inicialização automática)
            services.AddTransient<IContasRepository, ContasRepository>
                (map => new ContasRepository(connectionstring));

            services.AddTransient<IUsuarioRepository, UsuarioRepository>
                (map => new UsuarioRepository(connectionstring));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                //definir o caminho da página inicial do projeto
                endpoints.MapControllerRoute(
                        name: "default", //página inicial
                        pattern: "{controller=Account}/{action=Login}" //caminho
                    );
            });
        }
    }
}