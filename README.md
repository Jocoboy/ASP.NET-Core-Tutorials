# AutoMapper

> AutoMapper是对象到对象的映射工具。在完成映射规则之后，AutoMapper可以将源对象转换为目标对象。
> 一般情况下，表现层与应用层之间是通过DTO(数据传输对象Data Transfer Object)来进行交互的，数据传输对象是没有行为的POCO对象(简单CLR对象Plain Old CLR Object)，他的目的是为了对领域对象进行数据封装，实现层与层之间的数据传递。为何不直接将领域对象进行数据传递？因为领域对象更注重领域，DTO更注重数据。由于“富领域模型”的特点，这样会直接将领域对象的行为暴露给表现层。
> DTO本身不是业务对象，它是根据UI需求进行设计的。简单来说Model面向业务，我们是通过业务来定义Model的。而DTO是面向UI，通过UI的需求来定义的，通过DTO我们实现了表现层与Model层之间的解耦，表现层不引用Model。如果开发过程中我们的模型变了，而界面没变，我们只需改Model而不需要去改动表现层。

## 使用步骤

### 安装注册

从NuGet程序包中搜索：AutoMapper，下载对应的包，安装即可。

在StartUp中的ConfigureServices方法中注册。

```c#
services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
```

### 创建Model和DTO对象

### 继承Profile类实现映射关系

> CreateMap创建映射规则。
> BeforeMap：在映射之前执行的方法。
> AfterMap：反之，映射之后执行的方法。
> 自动扁平化映射：AutoMapper会将类中的属性进行分割，或匹配“Get”开头的方法。
> ForMember：指定映射字段。

### Controller中注入IMapper

```c#
        readonly IMapper _map;
        public QBasicController(IMapper map)
        {
            _map = map;
        }
```

### 单个实体对象转单个DTO

```c#
var dto = _map.Map<ProjectTipDTO>(project);
```

### 多个实体对象转单个DTO

```c#
var projSubRegion = new ProjSubRegion();
var finalDto = _map.Map(projSubRegion, dto);
```

# 从3.1迁移到6.0

## Program.cs与Startup.cs合并

**net core 3.1**

```c#
//Startup.cs
public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

 
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddDbContext<MvcMovieContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString(".")));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }


//Program.cs 
public class Program
    {
        public static void Main(string[] args)
        {
            //IHostBuilder builder = CreateHostBuilder(args);
            //IHost host = builder.Build();
            //host.Run();
            CreateHostBuilder(args).Build().Run();
        }

        //public static IHostBuilder CreateHostBuilder(string[] args)
        //{
        //    IHostBuilder builder = Host.CreateDefaultBuilder(args);
        //    Action<IWebHostBuilder> configAction = delegate(IWebHostBuilder webBuilder) 
        //    {
        //                       webBuilder.UseStartup<Startup>();
        //     };
        //    builder = builder.ConfigureWebHostDefaults(configAction);
        //    return builder;
        //}
     
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
```

**net 6.0**

```c#
//Program.cs
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");

    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

```



# 参考

## ASP.NET Core API

### 中间件

#### CorsMiddlewareExtensions

##### [UseCors(string)](https://docs.microsoft.com/zh-cn/dotnet/api/microsoft.aspnetcore.builder.corsmiddlewareextensions.usecors?view=aspnetcore-3.1#Microsoft_AspNetCore_Builder_CorsMiddlewareExtensions_UseCors_Microsoft_AspNetCore_Builder_IApplicationBuilder_)

```c#
string MyAllowCrossdomain = "";
app.UseCors(MyAllowCrossdomain);
```

#### CorsServiceCollectionExtensions

##### [AddCors](https://docs.microsoft.com/zh-cn/dotnet/api/microsoft.extensions.dependencyinjection.corsservicecollectionextensions.addcors?view=aspnetcore-3.1#Microsoft_Extensions_DependencyInjection_CorsServiceCollectionExtensions_AddCors_Microsoft_Extensions_DependencyInjection_IServiceCollection_System_Action_Microsoft_AspNetCore_Cors_Infrastructure_CorsOptions__)

```c#
services.AddCors(options =>
{
    options.AddPolicy(MyAllowCrossdomain,
     p => p.AllowAnyOrigin()
    .AllowAnyHeader()
    .AllowAnyMethod());
});
```



#### SessionMiddlewareExtensions

##### [UseSession](https://docs.microsoft.com/zh-cn/dotnet/api/microsoft.aspnetcore.builder.sessionmiddlewareextensions.usesession?view=aspnetcore-3.1#Microsoft_AspNetCore_Builder_SessionMiddlewareExtensions_UseSession_Microsoft_AspNetCore_Builder_IApplicationBuilder_Microsoft_AspNetCore_Builder_SessionOptions_)

```c#
app.UseSession();
```

#### SessionServiceCollectionExtensions

##### [AddSession](https://docs.microsoft.com/zh-cn/dotnet/api/microsoft.extensions.dependencyinjection.sessionservicecollectionextensions.addsession?view=aspnetcore-3.1#Microsoft_Extensions_DependencyInjection_SessionServiceCollectionExtensions_AddSession_Microsoft_Extensions_DependencyInjection_IServiceCollection_System_Action_Microsoft_AspNetCore_Builder_SessionOptions__)

```c#
services.AddSession(option =>
{
    //TimeSpan.FromMinutes(30);
    option.IdleTimeout = TimeSpan.FromHours(2); 
});
```

### 服务

#### 创建服务

##### AddSingleton

> 创建一个Singleton服务，首次请求会创建服务，然后，所有后续的请求中都会使用相同的实例，整个应用程序生命周期都使用该单个实例

实例 - [基于IHttpContextAccessor实现系统级别身份标识](https://www.cnblogs.com/lex-wu/p/10528109.html)

```c#
services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
```

##### AddScoped

> 不同http清求，实例不同，同名谓词不同，也不行。例如httpget跟httppost,作用域是一定范围内，例如从同一个post请求的create方法，只能统计一次，每次请求都是新的实例

##### AddTransient

> 临时服务，每次请求时，都会创建一个新的Transient服务实例

### 其他

#### AuthAppBuilderExtensions

##### [UseAuthentication](https://docs.microsoft.com/zh-cn/dotnet/api/microsoft.aspnetcore.builder.authappbuilderextensions.useauthentication?view=aspnetcore-3.1#Microsoft_AspNetCore_Builder_AuthAppBuilderExtensions_UseAuthentication_Microsoft_AspNetCore_Builder_IApplicationBuilder_)

```c#
app.UseAuthentication();
```



## .NET CLI

### dotnet-new

```shell
dotnet new <TEMPLATE> [--dry-run] [--force] [-lang|--language {"C#"|"F#"|VB}]
    [-n|--name <OUTPUT_NAME>] [--no-update-check] [-o|--output <OUTPUT_DIRECTORY>] [Template options]

dotnet new -h|--help
```
