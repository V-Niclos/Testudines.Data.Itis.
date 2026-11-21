## Step 06: Test Blazor Integration Using .NET 10

*Guide updated on November 11, 2026.*

---

### Objective

Create a simple Blazor test application targeting .NET 10 that uses the `Testudines.Data.Itis` class library. The application will have a single page that displays a list of taxonomic units from the database.

### Environment

* Visual Studio 2026
* Blazor Web App targeting **.NET 10** (`net10.0`)
* MariaDB database: `testudines_itis` (created in **Step 03**; update the connection string if you used a different name)
* Class library: `Testudines.Data.Itis` (scaffolded in **Step 04**)

### 1. Add Project and NuGet References

In your Blazor project, add a project reference to `Testudines.Data.Itis`:

```xml
<ItemGroup>
  <ProjectReference Include="..\..\src\Testudines.Data.Itis\Testudines.Data.Itis.csproj" />
</ItemGroup>
```





### 2. Configure the Connection String

In `appsettings.json`, add a `ConnectionStrings` section and replace `your_user` and `your_password` with your MariaDB credentials:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=127.0.0.1;Port=3306;Database=testudines_itis;Uid=your_user;Pwd=your_password;"
  }
}
```

> **Note:** Keep database credentials out of source control. Use .NET User Secrets or environment variables for local development.

### 3. Register the Database Context Factory in `Program.cs`

Add the required namespaces at the top of `Program.cs`:

```csharp
using Microsoft.EntityFrameworkCore;
using Testudines.Data.Itis.Context;
```

Register `IDbContextFactory<ItisDbContext>` before `var app = builder.Build();`:

```csharp
// Register the database context factory for safe use in Blazor components.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException(
        "Connection string 'DefaultConnection' was not found.");

builder.Services.AddDbContextFactory<ItisDbContext>(options =>
    options.UseMySql(
        connectionString,
        ServerVersion.AutoDetect(connectionString)));
```



For more clear example: You can see full example program.cs below

```
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



### 4. Update the Home Page

Replace the contents of `Components/Pages/Home.razor` with the following code (limited to the first 100 records for fast rendering):

```razor
@page "/"
@rendermode InteractiveServer

@using Microsoft.EntityFrameworkCore
@using Testudines.Data.Itis.Context
@using Testudines.Data.Itis.Entities

@inject IDbContextFactory<ItisDbContext> DbFactory

<PageTitle>Home</PageTitle>

<h3>ITIS Taxonomic Units</h3>

@if (rows is null)
{
    <p>Loading...</p>
}
else
{
    <ul>
        @foreach (var item in rows)
        {
            <li>@item.tsn: @item.complete_name</li>
        }
    </ul>
}

@code {
    private List<taxonomic_units>? rows;

    protected override async Task OnInitializedAsync()
    {
        await using var context = await DbFactory.CreateDbContextAsync();

        rows = await context.taxonomic_units
            .AsNoTracking()
            .OrderBy(t => t.complete_name)
            .Take(100)
            .ToListAsync();
    }
}
```

### 5. Result

Run the Blazor application. The home page will connect to `testudines_itis` and display the first 100 taxonomic units ordered alphabetically by their complete name.



![image-20261121172138529](D:\__Testudines_02\src\Testudines.Data.Itis\docs\assets\image-20261121172138529.png)



## Links Github

* [Root project](https://github.com/V-Niclos/Testudines.Data.Itis.)
* [Download test source](https://github.com/V-Niclos/Testudines.Data.Itis./tree/master/tests)
* [Library Testudines.Data.Itis](https://github.com/V-Niclos/Testudines.Data.Itis./tree/master/src)
