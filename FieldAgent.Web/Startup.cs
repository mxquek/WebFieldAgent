using FieldAgent.Core.Interfaces.DAL;
using FieldAgent.DAL;
using FieldAgent.DAL.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace FieldAgent.Web
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940

        static ConfigProvider cp = new ConfigProvider();

        //CHANGE TO PROD LATER
        DbFactory dbFactory = new DbFactory(cp.Config, FactoryMode.TEST);
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
              .AddJwtBearer(options =>
              {
                  options.TokenValidationParameters = new TokenValidationParameters
                  {
                      ValidateIssuer = true,
                      ValidateAudience = true,
                      ValidateLifetime = true,
                      ValidateIssuerSigningKey = true,

                      ValidIssuer = "http://localhost:2000",
                      ValidAudience = "http://localhost:2000",
                      IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("KeyForSignInSecret@1234"))
                  };
                  services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
              });


            services.AddControllers().AddXmlDataContractSerializerFormatters();

            services.AddTransient<IAgentRepository>(x => new AgentRepository(dbFactory));
            services.AddTransient<IMissionRepository>(x => new MissionRepository(dbFactory));
            services.AddTransient<IAliasRepository>(x => new AliasRepository(dbFactory));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
