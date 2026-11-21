## Step 05: Test ITIS using Console .Net 10

*Guide updated on November 21, 2026.*

---

### Objetive

Build app console test 

* ### Environment

  * .NET 10
  * Visual Studio 2026
  * MariaDB
  * The `testudines_itis` database (created in Step 03) and the `Testudines.Data.Itis` class library (scaffolded in Step 04)

### References

```
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net10.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
  </PropertyGroup>

	<ItemGroup>
		<PackageReference Include="Microsoft.Extensions.Configuration" Version="10.0.15" />
		<PackageReference Include="Microsoft.Extensions.Configuration.Binder" Version="10.0.15" />
		<PackageReference Include="Microsoft.Extensions.Configuration.Json" Version="10.0.15" />
		<PackageReference Include="Microsoft.Extensions.Configuration.EnvironmentVariables" Version="10.0.15" />
		<PackageReference Include="Microting.EntityFrameworkCore.MySql" Version="10.0.14" />
		<PackageReference Include="Microsoft.Extensions.DependencyInjection" Version="10.0.15" />

		<PackageReference Include="Microsoft.AspNetCore.Identity.EntityFrameworkCore" Version="10.0.15" />
		<PackageReference Include="Microsoft.EntityFrameworkCore" Version="10.0.15" />
		<PackageReference Include="Microsoft.Extensions.Logging.Console" Version="10.0.15" />
		<ProjectReference Include="..\..\src\Testudines.Data.Itis.csproj" />
	</ItemGroup>

	<ItemGroup>
	  <None Update="appsettings.json">
	    <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
	  </None>
	</ItemGroup>


</Project>
```



### Source

Need adjust :

User and password in _TestConnectionString

The database may be too changed

```c#
using Microsoft.EntityFrameworkCore;
using Testudines.Data.Itis;
using Testudines.Data.Itis.Context;

using Testudines.Data.Itis.TestBlazor.Components;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

//--------------------------------------
var itisConnection = builder.Configuration.GetConnectionString("ItisConnection")
    ?? throw new InvalidOperationException("Connection string 'ItisConnection' not found.");

builder.Services.AddDbContextFactory<ItisDbContext>(options =>
    options.UseMySql(itisConnection, ServerVersion.AutoDetect(itisConnection)));

//--------------------------------------


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();


```



### appsettings.json

You need ajust connection string in appsettings.json file

```json
{
  "ConnectionStrings": {
    "ItisConnection": "Server=localhost;Port=3306;Database=ITIS;User=your_user;Password=your_password;"
  },

  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}

```



### Result

![image-20261121174247821](D:\__Testudines_02\src\Testudines.Data.Itis\docs\assets\image-20261121174247821.png)

## Links Github project

* [Root project](https://github.com/V-Niclos/Testudines.Data.Itis.)
* [Download test source](https://github.com/V-Niclos/Testudines.Data.Itis./tree/master/tests)
* [Library Testudines.Data.Itis](https://github.com/V-Niclos/Testudines.Data.Itis./tree/master/src)