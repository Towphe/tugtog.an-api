using Microsoft.EntityFrameworkCore;
using tugtog_an.data.Repo;
using tugtog_an.domain;
using DotNetEnv;
using DotNetEnv.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddDotNetEnv("../.env", LoadOptions.TraversePath()).Build();

builder.Services.AddHttpsRedirection(opts => {
    opts.HttpsPort = 44350;
});

builder.Services.AddDbContext<TugtoganContext>(opts => {
    opts.UseNpgsql();
});

var app = builder.Build();

app.UseHttpsRedirection();

app.MapGet("/", () => "Hello world!");
    
app.Run();
