# ASP.NET Core output caching middleware

[![Build status](https://ci.appveyor.com/api/projects/status/033jspebqrwao5o4?svg=true)](https://ci.appveyor.com/project/madskristensen/webessentials-aspnetcore-serviceworker)
[![NuGet](https://img.shields.io/nuget/v/WebEssentials.AspNetCore.ServiceWorker.svg)](https://nuget.org/packages/WebEssentials.AspNetCore.ServiceWorker/)

Service worker support for any ASP.NET Core 2.0 app

## Register the service

Start by registering the service it in `Startup.cs` like so:

```c#
public void ConfigureServices(IServiceCollection services)
{
    services.AddMvc();
    services.AddServiceWorker();
}
```

## Configure
The options can be configured either in `Startup.cs`:

```c#
public void ConfigureServices(IServiceCollection services)
{
    services.AddMvc();
    services.AddServiceWorker(new ServiceWorkerOptions
    {
        Version = "v3",
        RoutesToPreCache = "foo.css, bar.js"
    });
}
```

...or in `appsettings.json`:

```json
{
  "serviceworker": {
    "version": "v1.0",
    "routesToPreCache": "foo.css, bar.js"
  }
}
```