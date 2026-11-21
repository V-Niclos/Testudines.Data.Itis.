###  Step 6: Test de la librería con tu aplicación Blazor

Actualizado: 2026/11/21  [Git source](https://github.com/V-Niclos/Testudines.Data.Itis.)



### Objetivo

Crear una applicacion  test, tipo web .Net 10 usando blazor, integrando la libreria Testudines.Itis creada en el paso 4



### Entorno:

* Microsoft .Net 10
* MySql / MariaDB en WSL
* Visual studio 2026
* Libreria .Net 10 Testudines.Data.Itis creada en paso 4



### Referencias



### Creacion del proyecto

* Elige tipo de proyecto "Applicacion Web Blazor"

* Selecciona añadir paginas de ejemplo (Para simplificar)

* Agrega **Referencia el proyecto.** En el Explorador de soluciones, haz clic derecho sobre **Dependencias** dentro de tu proyecto **Blazor**, elige **Agregar referencia de proyecto** y marca **`Testudines.Data.Iis`**.

- **Añade la cadena de conexión.** Abre `appsettings.json` en tu proyecto Blazor y añade la conexión 

  json

  ```
  {
  "ConnectionStrings": {
    "ItisConnection": "Server=localhost;Port=3306;Database=testudines_itis;User=your_user;Password=your_password;"
  },
  ...
  ```

  Usa el código con precaución.

- **Registra `ItisDbContext` en `Program.cs`.** Añade estas líneas en tu proyecto Blazor (recuerda poner adaptar la clase context, `<ItisDbContext>` 

  csharp

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

  Usa el código con precaución.

*(Nota: Usar `AddDbContextFactory` en lugar de `AddDbContext` es la mejor práctica en Blazor Server porque evita errores de concurrencia cuando varios componentes consultan tablas de `ITIS` al mismo tiempo).*

------

### 6. Ejemplo de uso en una página Blazor

En cualquier componente `.razor` de tu aplicación, ya puedes inyectar la factoría y consultar tus tablas existentes de `ITIS`:

Ejemplo minimo de injeccion del context

razor

```
@using Microsoft.EntityFrameworkCore
@using Itis.Data.Context
@using Itis.Data.Models
@inject IDbContextFactory< ItisDbContext > DbFactory

@code {
    protected override async Task OnInitializedAsync()
    {
        await using var context = await DbFactory.CreateDbContextAsync();
        // Ejemplo: var datos = await context.TuTablaDeItis.Take(20).ToListAsync();
    }
}
```



Ejemplo mas completo, listando en pantalla un pequeña muerstra de unidades taxonomicas



```csharp
@page "/home"
@page "/"
@using Microsoft.EntityFrameworkCore
@using Testudines.Data.Itis.Context
@using Testudines.Data.Itis.Identities;
@inject IDbContextFactory< ItisDbContext > DbFactory


<h3>Unidades Taxonómicas (ITIS) limit take 20</h3>

@if (isLoading)
{
    <div class="d-flex align-items-center my-3">
        <div class="spinner-border text-primary me-2" role="status"></div>
        <span>Cargando nombres taxonómicos...</span>
    </div>
}
else if (!string.IsNullOrEmpty(errorMessage))
{
    <div class="alert alert-danger" role="alert">
        @errorMessage
    </div>
}
else if (completeNames is null || completeNames.Count == 0)
{
    <p class="text-muted">No se encontraron registros.</p>
}
else
{
    <ul class="list-group">
        @foreach (var name in completeNames)
        {
            <li class="list-group-item">@name</li>
        }
    </ul>
}

@code {
    private List<string>? completeNames;
    private bool isLoading = true;
    private string? errorMessage;

    protected override async Task OnInitializedAsync()
    {
        try
        {
            isLoading = true;

            // Crea una instancia aislada del DbContext y la libera al terminar
            await using var context = await DbFactory.CreateDbContextAsync();

            // Nota: Verifica si tu DbSet se llama TaxonomicUnits o TaxonomicUnit
            completeNames = await context.TaxonomicUnits
                .AsNoTracking()
                .Where(u => u.CompleteName != null)
                .Select(u => u.CompleteName!)
                .OrderBy(name => name)
                .Take(20) // Límite preventivo por el gran tamaño de ITIS
                .ToListAsync();
        }
        catch (Exception ex)
        {
            errorMessage = $"Error al consultar la base de datos: {ex.Message}";
        }
        finally
        {
            isLoading = false;
        }
    }
}
```

### Result

![image-20261121172759856](D:\__Testudines_02\src\Testudines.Data.Itis\assets\image-20261121172759856.png)

## Links Github

* [Root project](https://github.com/V-Niclos/Testudines.Data.Itis.)
* [Download test source](https://github.com/V-Niclos/Testudines.Data.Itis./tree/master/tests)
* [Library Testudines.Data.Itis](https://github.com/V-Niclos/Testudines.Data.Itis./tree/master/src)

