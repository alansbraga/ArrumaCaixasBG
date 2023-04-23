using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ArrumaCaixasBG.Blazor;
using ArrumaCaixasBG.Blazor.Codigo;
using Radzen;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddTransient<CaixasTelaServico>();
builder.Services.AddScoped<DialogService>();
builder.Services.AddScoped<NotificationService>();
builder.Services.AddScoped<TooltipService>();
builder.Services.AddScoped<ContextMenuService>();
builder.Services.AddDadosCSVWeb(builder.Configuration);
builder.Services.AddArrumaCaixasInterativas();
builder.Services.AddSolucoesComoCaixa();
builder.Services.AddSolucoesSardine(builder.Configuration);
await builder.Build().RunAsync();
