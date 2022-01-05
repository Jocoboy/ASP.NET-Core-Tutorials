# DTO与ViewModel

## DTO

> DTO常见于接口输入参数，可以看做是接口传入参数的封装对象，同时参数中和数据库实体模型有一部分交集；比如需要提供一个用于修改一个学生类的姓名的API，那就可以定义一个StudentDTO，里面封装一个主键字段和姓名字段，将此DTO作为接口的传入参数。在比较简单的操作中，封装DTO体现不出优势(传入参数超过3个时，就要考虑封装成类，增加代码可读性)，但是当项目代码达到一定规模后，封装DTO可以规范代码的结构，便于项目的维护。

## ViewModel

> ViewModel 常见于MVC中 Contoller 返回的实体模型的命名。是对返回的数据库实体模型的封装，同时属性和数据库实体模型有一部分交集。其思想是为了封装接口暴露的对象，很像数据库开发中视图的使用场景。避免暴露部分字段，只提供需要的部分。

## ORM

> 对象关系映射（Object Relational Mapping，简称ORM）模式是一种为了解决面向对象与关系数据库存在的互不匹配的现象的技术。简单的说，ORM是通过使用描述对象和数据库之间映射的元数据，将程序中的对象自动持久化到关系数据库中。ORM在业务逻辑层和数据库层之间充当了桥梁的作用。

- 优点
  1. ORM解决的主要问题是对象和关系的映射。它通常把一个类和一个表一一对应，类的每个实例对应表中的一条记录，类的每个属性对应表中的每个字段。
  2. ORM提供了对数据库的映射，不用直接编写SQL代码，只需像操作对象一样从数据库操作数据。
  3. 让软件开发人员专注于业务逻辑的处理，提高了开发效率。

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

# JSON Web Token(JWT)

JWT是目前最流行的跨域身份验证解决方案。

## 应用场景

> 1. 授权：这是使用JWT的最常见方案。一旦用户登录，每个后续请求将包括JWT，允许用户访问该令牌允许的路由，服务和资源。Single Sign On是一种现在广泛使用JWT的功能，因为它的开销很小，并且能够在不同的域中轻松使用。
> 2. 信息交换：JSON Web令牌是在各方之间安全传输信息的好方法。因为JWT可以签名 - 例如，使用公钥/私钥对 - 您可以确定发件人是他们所说的人。此外，由于使用标头和有效负载计算签名，您还可以验证内容是否未被篡改。

## 优点

> #### 传统的身份校验方式
>
> 1. 用户向服务器发送用户名和密码。
> 2. 服务器验证通过后，在当前对话（session）里面保存相关数据，比如用户角色、登录时间等等。
> 3. 服务器向用户返回一个 session_id，写入用户的 Cookie。
> 4. 用户随后的每一次请求，都会通过 Cookie，将 session_id 传回服务器。
> 5. 服务器收到 session_id，找到前期保存的数据，由此得知用户的身份。
>
> 　　这种模式的问题在于，扩展性（scaling）不好。单机当然没有问题，如果是服务器集群，或者是跨域的服务导向架构，就要求 session 数据共享，每台服务器都能够读取 session。如果session存储的节点挂了，那么整个服务都会瘫痪，体验相当不好，风险也很高。
>
> 　　相比之下，JWT的实现方式是将用户信息存储在客户端，服务端不进行保存。每次请求都把令牌带上以校验用户登录状态，这样服务就变成了无状态的，服务器集群也很好扩展。

## 结构

JWT由"."分割的三部分组成：

- Header 头

```json
{
  "alg": "HS256", //正在使用的签名算法
  "typ": "JWT"	// 令牌的类型
}
```

- Payload 有效载荷

  1. 官方字段

  > iss (issuer)：签发人
  >
  > exp (expiration time)：过期时间
  >
  > sub (subject)：主题
  >
  > aud (audience)：受众
  >
  > nbf (Not Before)：生效时间
  >
  > iat (Issued At)：签发时间
  >
  > jti (JWT ID)：编号

  2. 私用字段

  ```json
  {
    "sub": "1234567890",
    "name": "John Doe",
    "admin": true
  }
  ```

- Signature 签名

> Signature 部分是对前两部分的签名，防止数据篡改。
>
> 首先，需要指定一个密钥（secret）。这个密钥只有服务器才知道，不能泄露给用户。
>
> 然后，使用 Header 里面指定的签名算法（默认是 HMAC SHA256），按照下面的公式产生签名。
>
> ```c#
> HMACSHA256(
>   base64UrlEncode(header) + "." +
>   base64UrlEncode(payload),
>   secret)
> ```

## 工作流程

每当用户想要访问受保护的路由或资源时，用户代理应该使用承载模式发送JWT，通常在Authorization标头中。

如果在标Authorization头中发送令牌，则跨域资源共享（CORS）将不会成为问题，因为它不使用cookie。

获取JWT并用于访问API或资源过程：

> 1. 应用程序向授权服务器请求授权
> 2. 校验用户身份，校验成功，返回token
> 3. 应用程序使用访问令牌访问受保护的资源

## 在ASP.NET中集成

### Startup添加JWT验证的相关配置

```c#
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Const.SecurityKey)),
                    ValidIssuer = "wanpengjiaoyu",
                    ValidAudience = "WebApi",
                    ValidateIssuer = true,
                    ValidateAudience = true
                };
            });
```

### Tool中添加模拟登陆生成Token的api

```c#
        public static string GetToken(string id, string loginname, string rolecode, string username)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Sid,id),
                new Claim(ClaimTypes.Name,loginname),
                new Claim(ClaimTypes.Role,rolecode),
                new Claim(ClaimTypes.Anonymous,username)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Const.SecurityKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var jwtToken = new JwtSecurityToken(
                issuer:"wanpengjiaoyu", 
                audience:"WebApi",
                claims: claims, 
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials
                );


            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }
```

### Controller中模拟调用

# Swagger

REST APIs文档生成工具。使用 OpenAPI 3.0 规范写一份文档，该文档描述了 API 的各种状态。

## 使用步骤

### 安装

从NuGet程序包中搜索：Swashbuckle，下载对应的包，安装即可。

```shell
PM> Install-Package Swashbuckle.AspNetCore
```

### 注册服务

```c#
       services.AddSwaggerGen(s =>
       {
                s.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
        }
```

- 在Swagger中添加JWT认证功能

```c#
                s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Description = "在下框中输入请求头中需要添加Jwt授权Token：Bearer {Token}",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });

                s.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                            Type = ReferenceType.SecurityScheme,
                                            Id = "Bearer"
                                }
                             }, new string[] { }
                        }
                    });
```



### 添加使用

```c#
          app.UseSwagger();
          app.UseSwaggerUI(s =>
          {
                s.SwaggerEndpoint("/swagger/v1/swagger.json", "My API v1");
          });
```

# 依赖注入（DI)

## 服务生命周期

### AddSingleton

> 创建一个Singleton服务，首次请求会创建服务，然后，所有后续的请求中都会使用相同的实例，整个应用程序生命周期都使用该单个实例

实例 - [基于IHttpContextAccessor实现系统级别身份标识](https://www.cnblogs.com/lex-wu/p/10528109.html)REST APIs文档生成工具

```c#
services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
```

### AddScoped

> 不同http清求，实例不同，同名谓词不同，也不行。例如httpget跟httppost,作用域是一定范围内，例如从同一个post请求的create方法，只能统计一次，每次请求都是新的实例

### AddTransient

> 临时服务，每次请求时，都会创建一个新的Transient服务实例

## 批量注入

### DIRegisterService

```c#
        /// <summary>
        /// 依赖注入仓储与服务
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        public static IServiceCollection DIRegisterService(this IServiceCollection service)
```

### RegisterAssembly

```c#
        /// <summary>
        /// 用DI批量注入接口程序集中对应的实现类。
        /// <para>
        /// 需要注意的是，这里有如下约定：
        /// IUserService --> UserService, IUserRepository --> UserRepository.
        /// </para>
        /// </summary>
        /// <param name="service"></param>
        /// <param name="AssemblyName">实现程序集的名称（不包含文件扩展名）</param>
        /// <param name="serviceLifetime">生命周期</param>
        /// <returns></returns>
        public static IServiceCollection RegisterAssembly(this IServiceCollection service,string AssemblyName, ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
```

# 自定义WebApi模型验证

## 应用场景

> 在我们的真实开发中，当我们碰到参数验证没通过400错误时，我们希望的是后台返回一个可理解的Json结果返回，而不是直接在页面返回400错误。所以我们需要替换掉默认的BadRequest响应结果，把结果换成我们想要的Json结果返回。

## 配置ApiBehaviorOptions

推荐的做法是使用InvalidModelStateResponseFactory，来自定义验证错误引发的响应。

InvalidModelStateResponseFactory是一个参数为ActionContext，返回值为IActionResult的委托。

```c#
      services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                   
                    var errors = actionContext.ModelState
                    .Where(e => e.Value.Errors.Count > 0)
                    .Select(e => e.Value.Errors.First().ErrorMessage)
                    .ToList();
                    var str = string.Join("|", errors);
                    var result = new MessageDTO<string>
                    {
                        Res = -1,
                        Msg = str,
                        Data = ""
                    };
                    return new BadRequestObjectResult(result);
                };
            });
```



上面的代码是覆盖ModelState管理的默认行为（ApiBehaviorOptions），当数据模型验证失败时，程序会执行这段代码。没通过验证的ModelState，把它抛出的错误信息通过MessageDTO打包返回给客户端。

# 自定义Tool类

## 验证码相关

### 生成验证码

#### RndomStr（手机六位验证码）

```c#
        /// <summary>  
        /// 生成指定长度的随机字符串 
        /// </summary>  
        /// <param name="codeLength">字符串的长度</param>  
        /// <returns>返回随机数字符串</returns>  
        public static string RndomStr(int codeLength)
```

#### CreateValiCode（图片四位验证码）

```c#
        /// <summary>  
        /// 将生成的字符串写入图像文件
        /// </summary>  
        /// <param name="code">验证码字符串</param>
        /// <param name="length">生成位数（默认4位）</param>  

        public static MemoryStream CreateValiCode(out string code, int length = 4)
```

### 校验验证码

#### GetCacheValue

```c#
        /// <summary>
        /// 获取缓存值
        /// </summary>
        /// <param name="key">缓存的键</param>
        /// <returns>返回缓存的值</returns>
        public static object GetCacheValue(string key, IMemoryCache cache)
```



#### ValiCode

```c#
    	/// <summary>
        /// 校验验证码是否正确
        /// </summary>
        /// <param name="key"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public static bool ValiCode(string key, string code, IMemoryCache cache)
```

## 短信相关

### 发送短信

#### HttpPost

```c#
       	/// <summary>  
        /// POST请求。返回值： 1，成功。0，失败。1001，连接接口失败。1002，获取接口返回信息失败。
        /// </summary>  
        public static string HttpPost(string Url, string postDataStr)
```

#### SMSMessage

```c#
        /// <summary>
        /// 发送短信
        /// </summary>
        /// <param name="url"></param>
        /// <param name="dto"></param>
        public static void SMSMessage(string url, RegMessageDTO<SmsDataDTO> dto)
```

### 重新发送短信

#### GetSmsArrState

```c#
        /// <summary>
        ///重新发送短信
        /// </summary>
        /// <param name="url"></param>
        /// <param name="dto"></param>
        public static SMSReport GetSmsArrState(string url, string clientId, List<SMSInfo> info)
```

## 安全性

### MD5字符串加密

#### GenerateMD5

```c#
        /// <summary>
        /// MD5字符串加密
        /// </summary>
        /// <param name="txt"></param>
        /// <returns>加密后字符串</returns>
        public static string GenerateMD5(string txt)
```




# Filter

MVC Filter是典型的AOP（面向切面编程）应用，在ASP.NET MVC中的4个过滤器类型，如下：

|     过滤器类型      |       **接口**       |       默认实现        |                        描述                        |
| :-----------------: | :------------------: | :-------------------: | :------------------------------------------------: |
|       Action        |    IActionFilter     | ActionFilterAttribute |              在动作方法之前及之后运行              |
|       Result        |    IResultFilter     | ActionFilterAttribute |           在动作结果被执行之前和之后运行           |
| AuthorizationFilter | IAuthorizationFilter |  AuthorizeAttribute   |      首先运行，在任何其它过滤器或动作方法之前      |
|      Exception      |   IExceptionFilter   | HandleErrorAttribute  | 只在另一个过滤器、动作方法、动作结果弹出异常时运行 |

默认实现它们的过滤器只有三种，分别是ActionFilter（方法），Authorize（授权），HandleError（错误处理）。

## 自定义过滤器

### MyActionFilter

```c#
    public class MyActionFilterAttribute : Attribute, IActionFilter
    {
        public  void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //TODO
        }
        public  void OnActionExecuted(ActionExecutedContext filterContext)
        {
            //TODO
        }
    }
```

### MyResultFilter

```c#
    public class MyResultFilterAttribute : Attribute, IResultFilter
    {
        public void OnResultExecuted(ResultExecutedContext context)
        {
            //TODO
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            //TODO
        }
    }
```

### MyAuthorizationFilter

```c#
    public class MyAuthorizationFilter : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            //TODO
        }
    }
```

### MyExceptionFilter

```c#
    public class MyExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            base.OnException(context);
        }
    }
```

## 作用域

### 局部拦截器

```c#
      [MyAuthorizationFilter]
        public IActionResult RegMessage()
        {
           return View();
        }
```

### 类控制器拦截器

```c#
[MyActionFilter]
[MyResultFilter]
public class QSystemController : Controller
   {
       
   }
```

### 全局拦截器

```c#
 public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews(configure =>
            {
                configure.Filters.Add<MyExceptionFilter>();
            });
        }
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

## MemoryCacheHelper

### 基本方法(CRUD)

#### Set\<T>

```c#
        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expire">过期时间，默认5分钟</param>
        public static void Set<T>(string key, T value, int expire = 5)
```

#### T Get\<T>

```c#
        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T Get<T>(string key)
```


#### Remove(string)

```c#
        /// <summary>
        /// 移除缓存
        /// </summary>
        /// <param name="key"></param>
        public static void Remove(string key)
```

#### Exists

```c#
        /// <summary>
        /// 是否存在缓存
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool Exists(string key)
```

#### GetCacheKeys

```c#
        /// <summary>
        /// 获取所有缓存键
        /// </summary>
        /// <returns></returns>
        public static List<string> GetCacheKeys()
```

#### ClearCache

```c#
        /// <summary>
        /// 移除所有缓存
        /// </summary>
        public static void ClearCache()
```



### 扩展方法

#### T GetOrSet\<T> 

Set\<T> ，T Get\<T>组合方法

```c#
        /// <summary>
        /// 获取或设置缓存
        /// </summary>
        /// <typeparam name="T">缓存类型</typeparam>
        /// <param name="cacheKey">缓存Key</param>
        /// <param name="getValue">获取要设置的缓存值的Lambda表达式</param>
        /// <param name="expire">过期时间，默认5分钟</param>
        /// <returns></returns>
        public static T GetOrSet<T>(string cacheKey, Func<T> getValue, int expire = 5)
```

#### Remove(string[])

Exists，Remove组合方法

```c#
        /// <summary>
        /// 移除缓存
        /// </summary>
        /// <param name="keys"></param>
        public static void Remove(params string[] keys)
```



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

## Transact-SQL for SQL Server

> Transact-SQL又简称T-SQL，它是微软公司在SQL Server数据库管理系统中对标准SQL的实现和扩展，是使用SQL Server的核心，所有与SQL Server实例通信的应用程序，其实都是通过发送T-SQL语句到服务器来完成对数据库的操作的。
> T-SQL与标准SQL稍有不同，SQL是结构化查询语言（Structured Query Language），是目前关系型数据库管理系统中使用得最广泛的查询语言。T-SQL是在SQL上发展而来的，T-SQL在SQL的基础上添加了变量、运算符、函数、注释和流程控制等，是标准SQL语言的扩展。因此，标准SQL是几乎所有关系型数据库都支持的语言，而T-SQL是Microsoft SQL Server支持的语言。

### 数据库对象的基本操作

#### Create

##### 创建数据库
> 在单个批处理中提交多条语句时，可以用关键字 GO 分隔各语句。 
>
> 当批处理只包含一条语句时，GO 是可选的。

```sql
CREATE DATABASE TestData  
GO
```

##### 创建表

> 数据库引擎 可安装为区分大小写或不区分大小写。 如果 数据库引擎 区分大小写进行安装，则对象名必须始终具有相同的大小写。 例如，名为 OrderData 的表与名为 ORDERDATA 的表是不同的表。 如果 数据库引擎 按不区分大小写进行安装，则这两个表名被视为同一个表，而且该名称只能使用一次。

```sql
-- 在新表中创建外键
USE TestData  
GO
CREATE TABLE dbo.Products  
   (ProductID int PRIMARY KEY NOT NULL,  
   ProductName varchar(25) NOT NULL,  
   Price money NULL,  
   ProductDescription varchar(max) NULL,
   AddressID int NULL,
   CONSTRAINT FK_Products_Address FOREIGN KEY (AddressID)
      REFERENCES dbo.Address (AddressID))  
GO

-- 在现有表中创建外键
USE TestData  
GO
CREATE TABLE dbo.Products  
   (ProductID int PRIMARY KEY NOT NULL,  
   ProductName varchar(25) NOT NULL,  
   Price money NULL,  
   ProductDescription varchar(max) NULL,
   AddressID int NULL)  
GO

ALTER TABLE dbo.Products 
   ADD CONSTRAINT FK_Products_Address FOREIGN KEY (AddressID)
      REFERENCES dbo.Address (AddressID)
      ON DELETE CASCADE
      ON UPDATE CASCADE
;
```

##### 创建视图

```sql
CREATE VIEW vw_Names  
   AS  
   SELECT ProductName, Price FROM Products;  
GO
```

##### 创建存储过程

```sql
CREATE PROCEDURE pr_Names @VarPrice money  
   AS  
   BEGIN  
      -- The print statement returns text to the user  
      PRINT 'Products less than ' + CAST(@VarPrice AS varchar(10));  
      -- A second statement starts here  
      SELECT ProductName, Price FROM vw_Names  
            WHERE Price < @varPrice;  
   END  
GO
```




#### Query

##### 查询所有数据库

```sql
SELECT *
FROM sys.databases;
```



##### 查询表中数据

```sql
-- The basic syntax for reading data from a single table  
SELECT ProductID, ProductName, Price, ProductDescription  
    FROM dbo.Products  
GO

-- Returns only two of the records in the table  
SELECT ProductID, ProductName, Price, ProductDescription  
    FROM dbo.Products  
    WHERE ProductID < 60  
GO

-- Returns ProductName and the Price including a 7% tax  
-- Provides the name CustomerPays for the calculated column  
SELECT ProductName, Price * 1.07 AS CustomerPays  
    FROM dbo.Products  
GO
```

##### 查询视图

```sql
SELECT * FROM vw_Names;  
GO
```



##### 查询存储过程

```sql
EXECUTE pr_Names 10.00;  
GO
```

#### Insert

##### 插入数据到表

```sql
-- Standard syntax 
INSERT dbo.Products (ProductID, ProductName, Price, ProductDescription)  
    VALUES (1, 'Clamp', 12.48, 'Workbench clamp')  
GO

-- Changing the order of the columns  
INSERT dbo.Products (ProductName, ProductID, Price, ProductDescription)  
    VALUES ('Screwdriver', 50, 3.17, 'Flat head')  
GO

-- Skipping the column list, but keeping the values in order  
INSERT dbo.Products  
    VALUES (75, 'Tire Bar', NULL, 'Tool for changing tires.')  
GO

-- Dropping the optional dbo and dropping the ProductDescription column  
INSERT Products (ProductID, ProductName, Price)  
    VALUES (3000, '3 mm Bracket', 0.52)  
GO
```

#### Update

##### 更新表中数据

```sql
UPDATE dbo.Products  
    SET ProductName = 'Flat Head Screwdriver'  
    WHERE ProductID = 50  
GO
```
#### Delete

##### 删除数据库

```sql
USE MASTER;  
GO  
DROP DATABASE TestData;  
GO
```

##### 删除表

```sql
DROP TABLE Products;  
GO
```

##### 删除表中所有数据

```sql
-- 使用 DELETE 语句删除 Products 表中的所有行
DELETE FROM Products;  
GO
-- 使用 TRUNCATE 语句删除 Products 表中的所有行
TRUNCATE TABLE TestData.dbo.Products;
GO
```

##### 删除视图

```sql
DROP VIEW vw_Names;  
GO
```

##### 删除存储过程

```sql
DROP PROC pr_Names;  
GO
```



## 数据库对象的权限配置

#### 创建和删除SQL Server实例的访问权限（LOGIN)

```sql
-- 创建
CREATE LOGIN [DESKTOP-Q4ORSFH\Mary]  
    FROM WINDOWS  
    WITH DEFAULT_DATABASE = [TestData];  
GO
-- 删除
DROP LOGIN [DESKTOP-Q4ORSFH\Mary];  
GO
```

#### 创建和删除数据库访问权限（USER)

```sql
USE [TestData];  
GO  
-- 创建
CREATE USER [Mary] FOR LOGIN [DESKTOP-Q4ORSFH\Mary];  
GO
-- 删除
DROP USER Mary;  
GO
```

#### 使用GRANT授予权限

> 必须具有 EXECUTE 权限才能执行存储过程。 必须具有 SELECT、INSERT、UPDATE 和 DELETE 权限才能访问和更改数据。

```sql
GRANT EXECUTE ON pr_Names TO Mary;  
GO
```

#### 使用REVOKE撤销权限

```sql
REVOKE EXECUTE ON pr_Names FROM Mary;  
GO
```

## 事务

> 事务是单个工作单元。 如果某一事务成功，则在该事务中进行的所有数据修改均会提交，成为数据库中的永久组成部分。 如果事务遇到错误且必须取消或回滚，则所有数据修改均被清除。
>
> SQL Server 以下列事务模式运行：
>
> 自动提交事务
> 每条单独的语句都是一个事务。
>
> 显式事务
> 每个事务均以 BEGIN TRANSACTION 语句显式开始，以 COMMIT 或 ROLLBACK 语句显式结束。
>
> 隐式事务
> 在前一个事务完成时新事务隐式启动，但每个事务仍以 COMMIT 或 ROLLBACK 语句显式完成。
>
> 批处理级事务
> 只能应用于多个活动结果集 (MARS)，在 MARS 会话中启动的 Transact-SQL 显式或隐式事务变为批处理级事务。 当批处理完成时没有提交或回滚的批处理级事务自动由 SQL Server 进行回滚。

### 使用显示事务

标记一个显式本地事务的起始点。 显式事务以 BEGIN TRANSACTION 语句开始，并以 COMMIT 或 ROLLBACK 语句结束。

```sql
-- 以COMMIT结束
BEGIN TRANSACTION;  
DELETE FROM dbo.Products 
    WHERE ProductID = 1;  
COMMIT;

-- 以ROLLBACK结束
CREATE TABLE ValueTable (id INT);  
BEGIN TRANSACTION;  
       INSERT INTO ValueTable VALUES(1);  
       INSERT INTO ValueTable VALUES(2);  
ROLLBACK;
```

### 事务结束标记

#### COMMIT TRANSACTION

标志一个成功的隐性事务或显式事务的结束。 如果 @@TRANCOUNT 为 1，COMMIT TRANSACTION 会使自事务开始以来的所有数据修改都成为数据库的永久部分，释放事务的资源，并将 @@TRANCOUNT 减少到 0。 如果 @@TRANCOUNT 大于 1，则 COMMIT TRANSACTION 使 @@TRANCOUNT 按 1 递减并且事务将保持活动状态。

```sql
BEGIN TRANSACTION;  
DELETE FROM dbo.Products 
    WHERE ProductID = 1;  
COMMIT TRANSACTION;   
```

#### ROLLBACK TRANSACTION

将显式事务或隐性事务回滚到事务的起点或事务内的某个保存点。 可以使用 ROLLBACK TRANSACTION 清除自事务的起点或到某个保存点所做的所有数据修改。 它还释放由事务控制的资源。

```sql
USE TestData;  
GO  
CREATE TABLE ValueTable ([value] INT);  
GO  
  
DECLARE @TransactionName VARCHAR(20) = 'Transaction1';  
  
BEGIN TRAN @TransactionName  
       INSERT INTO ValueTable VALUES(1), (2);  
ROLLBACK TRAN @TransactionName;  
  
INSERT INTO ValueTable VALUES(3),(4);  
  
SELECT [value] FROM ValueTable;  
  
DROP TABLE ValueTable;
```

