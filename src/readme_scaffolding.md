###

Command scaffolding

```c#
Scaffold-DbContext "Server=localhost;Port=3306;Database=testudines_itis;User=your_user;Password=your_password;" Microting.EntityFrameworkCore.MySql -OutputDir Identity -ContextDir Context -Context ItisDbContext -DataAnnotations -Force
```

powershell
go to your proyect folder

````bash
 cd D:\__Testudines\Git\src\Apps\Microting\src\Microting.Data.Itis
````


### 1. Crear la Biblioteca de Clases (.NET 10)

- **Añade el proyecto a tu solución.** En el **Explorador de soluciones**, haz clic derecho sobre la **Solución** (arriba del todo) y selecciona **Agregar > Nuevo proyecto**.
- **Elige la plantilla.** Busca **Biblioteca de clases** (*Class Library* para C#), nómbrala por ejemplo **`Itis.Data`** y pulsa **Siguiente**.
- **Selecciona el framework.** Elige **.NET 10.0** y haz clic en **Crear**.
- **Limpia el archivo por defecto.** Elimina el archivo `Class1.cs` que se genera automáticamente.

### 2. Cliente Dot Net

Verificacion

Comandos 

```
dotnet --info
dotnet --version
dotnet --list-sdks
```



Opcional: Instalar dotnet en linux

```bash
sudo apt update
sudo apt install -y dotnet-sdk-10.0
```



Opcional: Si necesitas istalar dotnet en windows, desde consola powershell ejecuta

```
winget install Microsoft.DotNet.SDK.10

```





------

### 2. Instalar los paquetes de Microting y Diseño en `Itis.Data`

El comando `Scaffold-DbContext` requiere el paquete `Microsoft.EntityFrameworkCore.Design` en el proyecto de inicio (tu app Blazor) y los paquetes de Microting/Tools en tu librería `Itis.Data`.

Abre la **Consola del Administrador de paquetes**, asegúrate de seleccionar **`Itis.Data`** en el desplegable superior **Proyecto predeterminado** y ejecuta:

Debes situarte en la carpeta de tu proyecto

```bash
dotnet add package Microting.EntityFrameworkCore.MySql --version 10.0.14
dotnet add package Microsoft.EntityFrameworkCore.Tools --version 10.0.14
dotnet add package Microsoft.EntityFrameworkCore.Design --version 10.0.14
```



------

### 3. Ejecutar el Scaffolding de la base de datos `ITIS`

Vuelve a seleccionar **`Itis.Data`** en el desplegable **Proyecto predeterminado** de la Consola del Administrador de paquetes y ejecuta el siguiente comando sustituyendo `tu_usuario` y `tu_contraseña` por tus credenciales de MySQL:



- **`-OutputDir Models`.** Crea una carpeta `*Identities*` con una clase C# por cada tabla de `ITIS`.
- **`-ContextDir Context -Context ItisDbContext`.** Crea el contexto `ItisDbContext.cs` dentro de una carpeta `Context`.
- **`-DataAnnotations`.** Añade atributos como `[Key]`, `[Required]` y `[StringLength]` sobre las propiedades.
- **`-Force`.** Sobrescribe los archivos si vuelves a lanzar el comando tras modificar alguna tabla en MySQL.



* Notas:

  * Es aconsejable configurar el parametro **-OutputDir Identities** y no a Model, de esta forma un nuevo Scaffolding sobrescribirá solo ese directorio, y podemos añadir el directorio Models para nuestros modelos personalizados
  * Si generamos adiciones a dbcontext, mejor en otro fichero separado en el directorio

  * Configura los parámetros Database, User, Password

Comando para ejecutar en consola de Visual Estudio 2026

```
Scaffold-DbContext "Server=localhost;Port=3306;Database=ITIS;User=your_user;Password=your_password;" Microting.EntityFrameworkCore.MySql -OutputDir Identities -ContextDir Context -Context ItisDbContext -DataAnnotations -Force
```



------

### 4. Limpiar la cadena de conexión en `ItisDbContext.cs`

Por seguridad, el *scaffolding* deja la contraseña escrita dentro del método `OnConfiguring` de `ItisDbContext.cs`.

Abre `Itis.Data/Context/ItisDbContext.cs` y borra o comenta el método `OnConfiguring` completo para que la librería tome siempre la cadena de conexión desde tu aplicación Blazor:

csharp

```
// Borra o comenta este bloque dentro de ItisDbContext.cs:
/*
protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    => optionsBuilder.UseMySql("server=localhost...", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.x-mysql"));
*/
```

Usa el código con precaución.

------

### 5. Test  Conectar la librería con aplicación Blazor

- **Referencia el proyecto.** En el Explorador de soluciones, haz clic derecho sobre **Dependencias** dentro de tu proyecto **Blazor**, elige **Agregar referencia de proyecto** y marca **`Itis.Data`**.

- **Añade la segunda cadena de conexión.** Abre `appsettings.json` en tu proyecto Blazor y añade la conexión a `ITIS` junto a la de Identity:

  json

  ```
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Port=3306;Database=blazor_identity_db;User=root;Password=tu_contraseña;",
    "ItisConnection": "Server=localhost;Port=3306;Database=ITIS;User=root;Password=tu_contraseña;"
  }
  ```

  Usa el código con precaución.

- **Registra `ItisDbContext` en `Program.cs`.** Añade estas líneas en tu proyecto Blazor (recuerda poner `<ItisDbContext>` a mano si el navegador oculta los corchetes):

  csharp

  ```
  var itisConnection = builder.Configuration.GetConnectionString("ItisConnection")
      ?? throw new InvalidOperationException("Connection string 'ItisConnection' not found.");
  
  builder.Services.AddDbContextFactory<ItisDbContext>(options =>
      options.UseMySql(itisConnection, ServerVersion.AutoDetect(itisConnection)));
  ```

  Usa el código con precaución.

*(Nota: Usar `AddDbContextFactory` en lugar de `AddDbContext` es la mejor práctica en Blazor Server porque evita errores de concurrencia cuando varios componentes consultan tablas de `ITIS` al mismo tiempo).*

------

### 6. Ejemplo de uso en una página Blazor

En cualquier componente `.razor` de tu aplicación, ya puedes inyectar la factoría y consultar tus tablas existentes de `ITIS`:

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

Usa el código con precaución.

Ejecuta el comando de **`Scaffold-DbContext`** en la consola y dime si te genera todas las tablas de **`ITIS`** sin errores o si alguna tabla (por ejemplo, una vista o tabla sin clave primaria) te lanza alguna advertencia.
