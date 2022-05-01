using FieldAgent.Core.Interfaces.DAL;
using FieldAgent.DAL;
using FieldAgent.DAL.Repositories;

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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
