using BlazorServer.Experiments.Web;
using BlazorServer.Experiments.Web.Application;
using BlazorServer.Experiments.Web.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
    

var searchSpecificationUrlMarshaller = new SearchSpecificationUrlMarshaller();

builder.Services.AddSingleton<ISearchService, PredictableNonsenseSearchService>();
builder.Services.AddSingleton<ISearchSpecificationFormatter>(searchSpecificationUrlMarshaller);
builder.Services.AddSingleton<ISearchSpecificationParser>(searchSpecificationUrlMarshaller);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();