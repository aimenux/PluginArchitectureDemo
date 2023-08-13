[![.NET](https://github.com/aimenux/PluginArchitectureDemo/actions/workflows/ci.yml/badge.svg?branch=main)](https://github.com/aimenux/PluginArchitectureDemo/actions/workflows/ci.yml)

# PluginArchitectureDemo
```  
Using various ways to illustrate plugin architecture  
```  

> In this demo, i m using various ways in order to illustrate the use of plugin architecture.
>
> :one: `Example01` use [default ioc](https://learn.microsoft.com/en-us/dotnet/core/extensions/dependency-injection) and some reflection in order to load plugins
>
> :two: `Example02` use [default ioc](https://learn.microsoft.com/en-us/dotnet/core/extensions/dependency-injection) and [scrutor](https://github.com/khellang/Scrutor) in order to load plugins
>
> :three: `Example03` use [autofac](https://github.com/autofac/Autofac) in order to load plugins
>
> :four: `Example04` use [simple injector](https://github.com/simpleinjector/SimpleInjector) in order to load plugins
>
> :bulb: Plugins are copied on `plugins` folder on post build events and loaded at startup by the application.
>

**`Tools`** : vs22, net 6.0, autofac, simple-injector, scrutor, fluent-validation