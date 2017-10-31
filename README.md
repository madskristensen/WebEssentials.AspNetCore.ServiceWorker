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