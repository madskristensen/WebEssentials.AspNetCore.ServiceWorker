# ASP.NET Core output caching middleware

[![Build status](https://ci.appveyor.com/api/projects/status/033jspebqrwao5o4?svg=true)](https://ci.appveyor.com/project/madskristensen/webessentials-aspnetcore-serviceworker)
[![NuGet](https://img.shields.io/nuget/v/WebEssentials.AspNetCore.ServiceWorker.svg)](https://nuget.org/packages/WebEssentials.AspNetCore.ServiceWorker/)

[Service worker](https://developers.google.com/web/fundamentals/primers/service-workers/) support for any ASP.NET Core 2.0 apps in order to improve performance as well as provide offline support. It's a foundational piece of [Progressive Web Apps](https://developers.google.com/web/progressive-web-apps/). This NuGet package makes it super easy to work with. 

Service workers are [supported in most modern browsers](http://caniuse.com/#feat=serviceworkers) and are safe to use since they are ignored by older ones.

## Install
To add a service worker to your ASP.NET Core 2.0 application, simply add the NuGet package [WebEssentials.AspNetCore.ServiceWorker](https://www.nuget.org/packages/WebEssentials.AspNetCore.ServiceWorker/).

Either do that through Visual Studio's NuGet Package Manager or the command line like this:

```cmd
dotnet add package WebEssentials.AspNetCore.ServiceWorker
```

## Register the service
In your ASP.NET Core app, start by registering the service in `Startup.cs` like so:

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
        CacheId = "v3",
        RoutesToPreCache = "foo.css, bar.js"
    });
}
```

...or in `appsettings.json`:

```json
{
  "serviceworker": {
    "cacheId": "v1.0",
    "routesToPreCache": "foo.css, bar.js"
  }
}
```

## Caching strategies
Specify which caching strategy you want like this:

```c#
public void ConfigureServices(IServiceCollection services)
{
    services.AddMvc();
    services.AddServiceWorker(new ServiceWorkerOptions
    {
        Strategy = ServiceWorkerStrategy.CacheFirst
    });
}
```

...or in `appsettings.json`:

```json
{
  "serviceworker": {
    "strategy": "cacheFirst"
  }
}
```

The options are:

### CacheFirst
This strategy will add all requested resources to the service worker cache and serve it from the cache every time. If the cache doesn't have the requested resource it will fall back to the network and if that succeeds it will put the response in the cache.

### CacheFirstSafe (default)
This strategy adds only HTML files as well as resources with a `v` querystring parameter such as `site.css?v=8udsfsaufd09sud0809sd_ds` to the cache.

It will always attempt the network for HTML files and fall back to the cache when the user is offline. That way the user always gets the latest from the live Internet when online.

For the resources (the ones with a `v` querystring parameter) it will always try the cache first and fall back to the network.

### NetworkFirst
This strategy will always try the network first for all resources and then fall back to the cache when offline. When the network call succeeds, it will put the response in the cache.

This strategy is completely safe to use and is primarily useful for offline-only scenarios since it isn't giving any performance benefits.