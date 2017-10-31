# ASP.NET Core output caching middleware

[![Build status](https://ci.appveyor.com/api/projects/status/rqp3tneiy0bi1697?svg=true)](https://ci.appveyor.com/project/madskristensen/webessentials-aspnetcore-outputcaching)
[![NuGet](https://img.shields.io/nuget/v/WebEssentials.AspNetCore.OutputCaching.svg)](https://nuget.org/packages/WebEssentials.AspNetCore.OutputCaching/)

Server-side caching middleware for ASP.NET 2.0

## Register the middleware

Start by registering the service it in `Startup.cs` like so:

```c#
public void ConfigureServices(IServiceCollection services)
{
    services.AddMvc();
    services.AddOutputCaching();
}
```

...and then register the middleware just before the call to `app.UseMvc(...)` like so:

```c#
public void Configure(IApplicationBuilder app, IHostingEnvironment env)
{
    app.UseOutputCaching();
    app.UseMvc(routes =>
    {
        routes.MapRoute(
            name: "default",
            template: "{controller=Home}/{action=Index}/{id?}");
    });
}
```

## Usage examples
There are various ways to use and customize the output caching. Here are some examples.

### Action filter
Use the `OutputCache` attribute on a controller action:

```c#
[OutputCache(Duration = 600, VaryByParam = "id")]
public IActionResult Product()
{
    return View();
}
```

### Programmatically

Using the `EnableOutputCaching` extension method on the `HttpContext` object in MVC, WebAPI or Razor Pages:

```c#
public IActionResult About()
{
    HttpContext.EnableOutputCaching(TimeSpan.FromMinutes(1));
    return View();
}
```

## Caching profiles
Set up cache profiles to reuse the same settings.

```c#
public void ConfigureServices(IServiceCollection services)
{
    services.AddMvc();
    services.AddOutputCaching(options =>
    {
        options.Profiles["default"] = new OutputCacheProfile
        {
            Duration = 600
        };
    });
}
```

Then use the profile from an action filter:

```c#
[OutputCache(Profile = "default")]
public IActionResult Index()
{
    return View();
}
```