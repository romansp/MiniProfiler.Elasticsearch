using Nest;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
services.AddControllersWithViews();
services.AddMiniProfiler().AddElastic();
services.AddSingleton<IElasticClient>(x => {
    var node = new Uri("http://localhost:9200");
    var connectionSettings = new ConnectionSettings(node).DefaultIndex("elasticsearch-sample");
    return new ElasticClient(connectionSettings);
});

services.Configure<RouteOptions>(options => {
   options.LowercaseUrls = true;
});

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseMiniProfiler();
app.UseEndpoints(endpoints => {
    endpoints.MapDefaultControllerRoute();
});

app.Run();
