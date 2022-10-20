using Nest;

namespace Sample.Elasticsearch.Core;

public class Startup {
    public Startup(IConfiguration configuration) => Configuration = configuration;

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services) {
        services.AddControllersWithViews();

        // use AddElastic or wrap ElasticClient instance in ProfiledElasticClient
        services.AddMiniProfiler().AddElastic();
        services.AddSingleton<IElasticClient>(_ => {
            var node = new Uri("http://localhost:9200");
            var connectionSettings = new ConnectionSettings(node).DefaultIndex("elasticsearch-sample");
            return new ElasticClient(connectionSettings);
        });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
        if (env.IsDevelopment()) {
            app.UseDeveloperExceptionPage();
        } else {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();

        app.UseMiniProfiler();
        app.UseEndpoints(endpoints => {
            endpoints.MapDefaultControllerRoute();
        });
    }
}
