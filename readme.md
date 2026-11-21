## ITIS_Branch_UTF8 

A lightweight tool to clone the ITIS database, normalize its encoding to UTF-8, and extract specific taxonomic branches (e.g., Reptilia, Aves, Testudines) into a clean, ready-to-use database.

*Updated 2026-11-20*



## Objectives

1. **Local Clone:** Mirror the full original ITIS (*Integrated Taxonomic Information System*) database locally using MySQL / MariaDB (preserving its original Latin-2 encoding).

2. **UTF-8 Migration:** Generate a clean copy of the cloned database converted to `utf8mb4` encoding.

3. **Taxonomic Distillation:** Extract a lightweight, distilled version of the ITIS database filtered down to a specific taxonomic branch.

   

## Testing & Sample Implementations

* To validate the resulting `utf8mb4` distilled database, this repository includes working test projects built with **.NET 10**:
* **.NET 10 Data Access Library:** Entity Framework Core `DbContext` and entity models generated via database-first scaffolding.
* **.NET 10 Console App:** Command-line test utility to query and verify taxonomic hierarchy integrity and UTF-8 character rendering.
* **.NET 10 Blazor App:** Interactive web interface demonstrating real-world browsing and filtering of the distilled taxonomic data.

####  Important Notes

Before running the scripts or applications, make sure to review and adjust the following default parameters to match your environment:

1. **Database Credentials:** Replace the placeholder credentials (`user_name` and `user_password`) with your actual MySQL / MariaDB user and password.
2. **Database Host:** The default server address is configured to `localhost`.
3. **Target Taxonomic Branch:** The distillation scripts are currently pre-configured to extract the ***Testudines*** (turtles) taxonomic unit. Update this parameter if you wish to target a different branch (e.g., *Reptilia*, *Aves*).
4. **Storage Engine (`InnoDB`):** Ensure all tables use the **InnoDB** storage engine before converting to `utf8mb4`. Legacy engines (such as MyISAM) can cause unexpected column length and index size errors when expanding from single-byte (`latin1`/`latin2`) to multi-byte (`utf8mb4`) character sets.

## Environment

While this setup can be easily adapted to newer versions, the scripts and test applications in this guide were verified using the following environment:

- **Database Server:** MariaDB `11.8.6` (`11.8.6-MariaDB-5ubuntu0.1` on Ubuntu)
- **Development Stack:**
  - .NET 10
  - Visual Studio 2026 Community
- **NuGet Packages Used (Test Projects):**
  - `Microsoft.EntityFrameworkCore.Design` (`10.0.15`)
  - `Microsoft.EntityFrameworkCore.SqlServer` (`10.0.15`)
  - `Microsoft.EntityFrameworkCore.Tools` (`10.0.15`)
  - `Microting.EntityFrameworkCore.MySql` (`10.0.14`)



## Step-by-Step Guide (Table of Contents)



### Step 1: Local Installation of the Official ITIS Clone
Set up a local mirror of the official ITIS (*Integrated Taxonomic Information System*) database.  
👉 [Go to Step 1](docs/step_01_dowload_install.md) 



### Step 2: Migrate ITIS Clone to `utf8mb4` (Optional)
Generate a full clone of the ITIS database encoded in `utf8mb4` (using the **InnoDB** engine to avoid column/index length issues during multi-byte conversion) and resolve legacy encoding problems.

👉 [Go to Step 2](docs/step_02_utf8mb4_encoding.md)

###  Step 3: Extract a Specific Taxonomic Branch (Optional)
Create a lightweight clone filtered to a single taxonomic branch. This guide uses turtles (*Testudines*, `TSN: 948936`) as an example, though you can target any other clade such as reptiles (*Reptilia*, `TSN: 173747`), mammals (*Mammalia*, `TSN: 179913`), or birds (*Aves*, `TSN: 174371`). Look up other TSN values on [itis.gov](https://itis.gov/).  
👉 [Go to Step 3](docs/step_03_branch.md)

### Step 4: Data Model Library via Scaffolding (Optional)
Build a .NET data access library using database-first scaffolding with EF Core, Pomelo, and MySqlConnector.  
👉 [Go to Step 4]docs/step_04_scaffolding.md)

### Step 5: Console Test Application (Optional)
Create a command-line test application using the data model library generated in Step 4.  
👉 [Go to Step 5](docs/step_05_test_console.md)

### Step 6: Blazor Web Test Application (Optional)
Build an interactive Blazor web app to browse the distilled database using the data model from Step 4.  
👉 [Go to Step 6](docs/step_06_test_blazor.md)
