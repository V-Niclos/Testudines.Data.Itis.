## Step 04: Scaffold a MySQL/MariaDB Database Using .NET 10 and Mircroting.EnstityFrameCore.MySql

*Guide updated on November 21, 2026.*

---

This guide provides instructions for generating  Entity Framework Core models from the  MariaDB or Mysql database using Database-First (reverse engineering) scaffolding.

### 1 Objective:  

Create .Net 10 library with context and models.
    
1. Prerequisites

* Visual Studio 2026 Community

* .NET 10 SDK 

* The `testudines_itis` database created in **Step 03** (or your target MariaDB database),

  In the commands you can need change this parameters

  | Parameter         | Value                | Your value | Check |
  | ----------------- | -------------------- | ---------- | ----- |
  | Database Server   | localhost            |            | ☐     |
  | Database          | testudines_itis      |            | ☐     |
  | User              | your_user            |            | ☐     |
  | Password          | Your_pasword         |            | ☐     |
  | Library to create | Testudines_Data.Itis |            | ☐     |

  

### Required NuGet Packages

* Microsoft.EntityFrameworkCore.Design Version=10.0.14
* Microsoft.EntityFrameworkCore.Tools Version=10.0.14
* Microting.EntityFrameworkCore.MySql Version=10.0.14
* `Microsoft.EntityFrameworkCore` Version `10.0.14`
* Windows SDK (versión 10.0.x)

  In console here are the commans to add

> ```
> dotnet add package Microsoft.EntityFrameworkCore --version 10.0.14
> dotnet add package Microsoft.EntityFrameworkCore.Design --version 10.0.14
> dotnet add package Microsoft.EntityFrameworkCore.Tools --version 10.0.14
> dotnet add package Microting.EntityFrameworkCore.MySql --version 10.0.14
> ```
>
> 

### 1.1. Verify the .NET 10 SDK

Open a PowerShell terminal and check the installed SDK versions:

```bash
dotnet --list-sdks
```

You need find in reply list result some like 10.0.404 [C:\Program Files\dotnet\sdk] 

### 1.2. Pin the SDK Version (`global.json`)

To ensure consistent SDK selection across development environments, create a `global.json` file in your solution or project root directory:

```bash
dotnet new globaljson --sdk-version 10.0.404 --roll-forward latestFeature
```

This configuration selects the specified .NET 9 SDK feature band and allows compatible updates within that band. Verify that the generated `global.json` file matches the following structure:

```json
{
  "sdk": {
    "rollForward": "latestFeature",
    "version": "10.0.404"
  }
}
```

> **Note:** The specified SDK version (or a newer patch within the `10.0.404` band) must be installed on your machine.

### 1.3. Install the Entity Framework Core CLI

Install the global `dotnet-ef` command-line tool matching your EF Core package version:

If you need install in windows:

```bash
dotnet tool install --global dotnet-ef --version 10.0.404

```

If you need install in Linux ( script oficial ofMicrosoft )

```
curl -sSL https://dot.net/v1/dotnet-install.sh | bash /dev/stdin --version 10.0.404

```

Verify the installation:

```bash
dotnet ef --version
```

---

## 2. Create the Class Library Project

In Visual Studio (or via the CLI), create a **Class Library** project targeting **.NET 9** named **`Testudines.Data.Itis`** or your app name preference



> **Important:** The class library must target **.NET 10** (`net9.0`) to remain compatibility

Ensure that the following NuGet packages are added to `Testudines.Data.Itis.csproj`:

```xml
<ItemGroup>
  <PackageReference Include="Microsoft.EntityFrameworkCore" Version="10.0.14" />
  <PackageReference Include="Microsoft.EntityFrameworkCore.Design" Version="10.0.14" />
  <PackageReference Include="Microsoft.EntityFrameworkCore.Tools" Version="10.0.14" />
  <PackageReference Include="Microting.EntityFrameworkCore.MySql" Version="10.0.14" />

</ItemGroup>
```

---

## 3. Generate Entity Framework Core Models

Be sure this

* Your are in directory of project
* if you work in visual studio, the project is set to initial project
* Change parameters: server, port, Database, UserId, Password
* Recommended -o parameter to Entities foldes, and reserve Models for your custom models

Open a terminal in the `Testudines.Data.Itis` project directory and run the following command. Update the connection string with your server address, database name, username, and password:

```bash
dotnet ef dbcontext scaffold "Server=127.0.0.1;Port=3306;Database=testudines_itis;User Id=your_user_id;Password=your_password;" Pomelo.EntityFrameworkCore.MySql -o Entities -c ItisDbContext --context-dir Context --no-onconfiguring --no-pluralize --use-database-names --data-annotations
```

### 3.1. Command Parameters

| Parameter | Description |
| :--- | :--- |
| `-o Entities` | Output directory for the generated entity classes. |
| `-c ItisDbContext` | Name of the generated `DbContext` class. |
| `--context-dir Context` | Output directory for the `DbContext` class. |
| `--no-onconfiguring` | Prevents the connection string from being hardcoded inside `ItisDbContext`. |
| `--no-pluralize` | Keeps table names as-is without automatic English pluralization. |
| `--use-database-names` | Preserves exact database table and column names. |
| `--data-annotations` | Uses data annotations where supported instead of Fluent API configuration. |
| --force | Overwrite existing files |

### 3.2. Generated Project Structure

All tables in the target database are scaffolded by default into the `Context` and `Entities` folders:

```text
Testudines.Data.Itis
│
├───Context
│       ItisDbContext.cs
│
├───Entities
│       change_comments.cs
│       change_operations.cs
│       change_tracks.cs
│       chg_operation_lkp.cs
│       comments.cs
│       experts.cs
│       geographic_div.cs
│       hierarchy.cs
│       HierarchyToRank.cs
│       jurisdiction.cs
│       kingdoms.cs
│       longnames.cs
│       nodc_ids.cs
│       other_sources.cs
│       publications.cs
│       reference_links.cs
│       reviews.cs
│       strippedauthor.cs
│       synonym_links.cs
│       taxonomic_units.cs
│       taxon_authors_lkp.cs
│       taxon_unit_types.cs
│       tu_comments_links.cs
│       vernaculars.cs
│       vern_ref_links.cs
│
└───Testudines.Data.Itis.csproj
```

### 3.3. Recommended Folder Organization for Custom Code

If you need to add custom models, DTOs, or business logic, place them in separate folders (such as `Models` and `Services`) rather than modifying files inside `Context` or `Entities`:

```text
Testudines.Data.Itis
│
├───Context
│       ItisDbContext.cs
├───Entities
├───Models
└───Services
```

---

## 4. Regenerating the Models

If the database schema changes and you need to regenerate the models, run the scaffolding command again with the `--force` flag:

```bash
dotnet ef dbcontext scaffold "Server=localhost;Port=3306;Database=testudines_itis;User Id=your_user_id;Password=your_password;" Pomelo.EntityFrameworkCore.MySql -o Entities -c ItisDbContext --context-dir Context --no-onconfiguring --no-pluralize --use-database-names --data-annotations --force
```

> [!WARNING]
> The `--force` flag overwrites all existing files in `Context` and `Entities`. Any manual edits made directly to generated files will be lost. Always keep custom code in separate folders or `partial` classes.

---

## Next Step

Proceed to **Step 05: Test the ITIS Database Using a .NET 10 Console App** to verify the database connection and query data using `Testudines.Data.Itis`.

## Links Github project

* [Root project](https://github.com/V-Niclos/Testudines.Data.Itis.)
* [Download test source](https://github.com/V-Niclos/Testudines.Data.Itis./tree/master/tests)
* [Library Testudines.Data.Itis](https://github.com/V-Niclos/Testudines.Data.Itis./tree/master/src)