# VSCode:Attempted to update project that is not loaded
## 日志

```
D:\common\Softwares\VS\2019\Community\MSBuild\Current\Bin\Microsoft.Common.CurrentVersion.targets(1177,5): Error: This project targets .NET 6.0 but the currently used MSBuild is not compatible with it - MSBuild 16.9+ is required. To solve this, if you have Visual Studio 2019 installed on your machine, make sure it is updated to version 16.9 or add 'omnisharp.json' to your project root with the setting { "msbuild": { "useBundledOnly": true } }.

[fail]: OmniSharp.MSBuild.ProjectManager
        Attempted to update project that is not loaded: d:\common\Documents\GithubRepo\Jocoboy\ASP.NET-Core-Tutorials\Create-a-web-API\ContosoPizza\ContosoPizza.csproj
```

## 解决方法

VS2019  version 16.7 升级到 16.11

# httprepl:The SSL connection could not be established, see inner exception

## 解决方法

```shell
dotnet dev-certs https --trust
```

[see more](https://stackoverflow.com/questions/54538216/the-ssl-connection-could-not-be-established-see-inner-exception)