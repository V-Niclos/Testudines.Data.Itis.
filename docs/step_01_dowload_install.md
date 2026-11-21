# Step 1. Download ITIS and Install MariaDB / MySQL

*Guide updated on November 11, 2026.*

## 1. Objective

Install a complete local copy of the ITIS database:

- **ITIS:** The original, immutable clone of the database downloaded from ITIS. It will be kept as a baseline reference for queries and verification, retaining its source structure and original encoding.

## 2. Environment

- **Database Server:** MariaDB `11.8.6-MariaDB-5ubuntu0.1`
- **OS:** Ubuntu on WSL (Windows 11)
- **Administration Tool:** MariaDB CLI console

### Important Notes

The following issues were identified during testing:

1. Using GUI tools like HeidiSQL for certain import processes caused character encoding errors.
2. Opening and saving SQL dump files with some text editors altered their original encoding.
3. Using the `MyISAM` storage engine caused index length errors during later multi-byte conversions, while `latin1` caused character representation issues.
4. The next step in this tutorial creates a `utf8mb4` clone on `InnoDB` for modern, multilingual environments, avoiding all of the issues listed above.

### Check the MariaDB Environment

Open a PowerShell / WSL terminal, log in to MariaDB, and run the following commands one by one:

```bash
sudo mariadb -p
```

```sql
SELECT VERSION();
SHOW VARIABLES LIKE 'default_storage_engine';
SHOW VARIABLES LIKE 'innodb_default_row_format';
SHOW ENGINES;
```

**Expected results summary:**

1. `VERSION()`: `11.8.6-MariaDB-5ubuntu0.1`
2. `default_storage_engine`: `InnoDB`
3. `innodb_default_row_format`: `dynamic`
4. `SHOW ENGINES;` summary:

| Engine     | Support | Comment     | Transactions | XA   | Savepoints |
| ---------- | ------- | ----------- | ------------ | ---- | ---------- |
| **InnoDB** | DEFAULT | Supports... | YES          | YES  | YES        |

---

## 3. Download the Database from ITIS

Download the **Full ITIS Data Set (MySQL bulk load)** (`itisMySQLBulk.zip`) from the official source at [itis.gov/downloads](https://www.itis.gov/downloads/) to ensure you have the latest release.

Save the archive to a short path to simplify the next steps. For example:

```text
D:\itis_download
```

Extract the archive. The `ITIS.sql` file contains all the table creation and data insertion scripts.

Make sure `ITIS.sql` is located directly at:

```text
D:\itis_download\ITIS.sql
```

We will use this file location for the import process.

> **Important:** Do not modify or edit the original `ITIS.sql` file.

---

## 4. Install the Original ITIS Clone

This clone will remain immutable and will be used exclusively as a reference for verification and comparison with your working database.

### 4.1. Open MariaDB

Open PowerShell, enter WSL, and connect to MariaDB:

```bash
mariadb -u your_user_id -p
# or
sudo mariadb -p
```

### 4.2. Create the `ITIS` Database

If an `ITIS` database already exists, back it up first.

Create the database using its original encoding:

```sql
DROP DATABASE IF EXISTS ITIS;
CREATE DATABASE ITIS
  CHARACTER SET latin1
  COLLATE latin1_bin;
```

#### Verify the Database Encoding

```sql
SELECT
    SCHEMA_NAME,
    DEFAULT_CHARACTER_SET_NAME,
    DEFAULT_COLLATION_NAME
FROM information_schema.SCHEMATA
WHERE SCHEMA_NAME = 'ITIS';
```

**Expected result:**

```text
latin1
latin1_bin
```

### 4.3. Import the Data Tables

Exit the MariaDB prompt:

```sql
EXIT;
```

From the WSL terminal, navigate to the directory containing `ITIS.sql` (in WSL, the Windows `D:` drive is mounted under `/mnt/d/`):

```bash
cd /mnt/d/itis_download
ls
```

Verify that `ITIS.sql` appears in the file listing, then run the import:

```bash
mariadb -u your_user_id -p \
  --default-character-set=latin1 \
  ITIS < ITIS.sql
```

The `--default-character-set=latin1` flag is essential so that the legacy characters in the dump are interpreted accurately.

### 4.4. Verify the Installation

Connect to MariaDB again:

```bash
mariadb -u your_user_id -p
```

Run:

```sql
USE ITIS;
SHOW TABLES;
```

Approximately 25 tables should be listed.

The reference `ITIS` database is now installed.

> **Warning:** From this point onward, do not modify the `ITIS` database.