## Step 02: Create a Clone of the ITIS Database with `utf8mb4` Encoding

*Guide updated on November 21, 2026.*

## 1. Objective

Create **`ITIS_utf8mb4`**, a working clone where the modifications, cleanup, and transformations needed to use the database in multilingual projects will be carried out.

The original `ITIS` database installed in the previous step will retain its source structure and encoding. A new clone named `ITIS_utf8mb4` will be created using the `InnoDB` storage engine and `utf8mb4` character encoding.

## 2. Environment

- **Database Server:** MariaDB `11.8.6-MariaDB-5ubuntu0.1`
- **OS:** Ubuntu on WSL (Windows 11)
- **Administration Tool:** MariaDB CLI console

Default database engine configuration:

```ini
default_storage_engine = InnoDB
```

### Important Notes

The following issues were identified during testing:

1. Using GUI tools like HeidiSQL for certain import processes caused character encoding errors.
2. Opening and saving SQL dump files with some text editors altered their original encoding.
3. Using `MyISAM` caused index length issues when converting columns to `utf8mb4`.
4. The working database is converted to `utf8mb4` for use in modern, multilingual environments.
5. `utf8mb4_bin` is used as the collation to preserve exact binary string comparisons.

### Check the Environment

In MariaDB, run:

```sql
SELECT VERSION();
SHOW VARIABLES LIKE 'default_storage_engine';
SHOW VARIABLES LIKE 'innodb_default_row_format';
```

**Expected results:**

- **Default engine:** `InnoDB`
- **InnoDB default row format:** `dynamic`

---

## 3. Prerequisites: Original `ITIS` Database

To follow these steps, the original `ITIS` database must already be installed on the server. If it is not, complete [Step 1: Local Installation of ITIS](./01_Instalacion_ITIS.md) before continuing.

---

## 4. Create the `ITIS_utf8mb4` Working Database

`ITIS_utf8mb4` will be a second copy of the original database. It will initially be imported using the original `latin1` encoding and then converted to `utf8mb4`.

### 4.1. Create the Database

In MariaDB, run:

```sql
DROP DATABASE IF EXISTS ITIS_utf8mb4;

CREATE DATABASE ITIS_utf8mb4
  CHARACTER SET latin1
  COLLATE latin1_bin;
```

Verify the database encoding:

```sql
SELECT
    SCHEMA_NAME,
    DEFAULT_CHARACTER_SET_NAME,
    DEFAULT_COLLATION_NAME
FROM information_schema.SCHEMATA
WHERE SCHEMA_NAME = 'ITIS_utf8mb4';
```

**Expected result:**

```text
latin1
latin1_bin
```

### 4.2. Import the Data

Exit MariaDB:

```sql
EXIT;
```

From the WSL terminal, navigate to the download folder:

```bash
cd /mnt/d/itis_download
ls
```

Run the import:

```bash
mariadb -u your_user_id -p \
  --default-character-set=latin1 \
  ITIS_utf8mb4 < ITIS.sql
```

The import may take a few minutes.

### 4.3. Verify the Installation

Connect to MariaDB:

```bash
mariadb -u your_user_id -p
```

Run:

```sql
USE ITIS_utf8mb4;
SHOW TABLES;
```

Verify that all expected tables (~25) are present.

---

## 5. Check Special Characters

This step prevents silent corruption of special characters, such as:

```text
209402|(Gnther, 1999)|2019-09-27|5|Gnther 1999
75288|(Gnther, 1867)|2004-01-21|5|Gnther 1867
208783|(Gnther, 2009)|2019-09-27|5|Gnther 2009
```

Before changing the encoding, verify that special characters were imported properly. For example, inspect author names containing accented characters or umlauts:

```sql
SELECT
    shortauthor,
    HEX(shortauthor)
FROM ITIS_utf8mb4.strippedauthor
WHERE shortauthor LIKE '%G%nther%'
   OR shortauthor LIKE '%Dum%ril%'
LIMIT 30;
```

Inspect the results visually. The `HEX` column allows you to examine the raw hexadecimal byte representation of the characters (for example, `FC` for `ü` and `E9` for `é` in `latin1`) and detect any import corruption.

> **Warning:** Do not continue if you find corrupted or replacement characters (``).

---

## 6. Convert the Tables to `InnoDB`

This step is critical. The official SQL dump may explicitly create `MyISAM` tables, even if `InnoDB` is configured as the default storage engine in MariaDB (an explicit `ENGINE=` clause in a `CREATE TABLE` statement overrides the server default).

Therefore, check the table engines and convert them to `InnoDB` before altering the character set.

### 6.1. Check the Storage Engines in Use

```sql
SELECT
    ENGINE,
    COUNT(*) AS TotalTables
FROM information_schema.TABLES
WHERE TABLE_SCHEMA = 'ITIS_utf8mb4'
  AND TABLE_TYPE = 'BASE TABLE'
GROUP BY ENGINE;
```

The output shows how many tables currently use each storage engine.

### 6.2. Generate the Conversion Statements

Generate the required `ALTER TABLE` statements dynamically:

```sql
SELECT
    CONCAT(
        'ALTER TABLE `ITIS_utf8mb4`.`',
        TABLE_NAME,
        '` ENGINE=InnoDB, ROW_FORMAT=DYNAMIC;'
    ) AS Script
FROM information_schema.TABLES
WHERE TABLE_SCHEMA = 'ITIS_utf8mb4'
  AND TABLE_TYPE = 'BASE TABLE'
  AND ENGINE <> 'InnoDB'
ORDER BY TABLE_NAME;
```

Copy and execute the generated statements to convert all non-`InnoDB` tables.

### 6.3. Verify the Conversion

```sql
SELECT
    ENGINE,
    COUNT(*) AS TotalTables
FROM information_schema.TABLES
WHERE TABLE_SCHEMA = 'ITIS_utf8mb4'
  AND TABLE_TYPE = 'BASE TABLE'
GROUP BY ENGINE;
```

**Expected result:**

```text
InnoDB | 25
```

*(Note: The exact count depends on the tables included in your ITIS release.)*

#### Verify the Row Format

```sql
SELECT
    NAME,
    ROW_FORMAT
FROM information_schema.INNODB_SYS_TABLES
WHERE NAME LIKE 'ITIS_utf8mb4/%'
  AND ROW_FORMAT <> 'Dynamic';
```

If this query returns any rows, inspect those tables and convert them manually:

```sql
ALTER TABLE ITIS_utf8mb4.table_name
  ROW_FORMAT = DYNAMIC;
```

---

## 7. Convert `ITIS_utf8mb4` Encoding to `utf8mb4`

Once you have confirmed that all tables use `InnoDB` with `DYNAMIC` row format, proceed with the character set conversion.

### 7.1. Update the Database Default Encoding

```sql
ALTER DATABASE ITIS_utf8mb4
  CHARACTER SET utf8mb4
  COLLATE utf8mb4_bin;
```

> **Important:** `ALTER DATABASE` only updates the default settings for *new* tables; it does not automatically convert existing tables or columns. Each table must be converted explicitly.

### 7.2. Generate the Table Conversion Statements

Run:

```sql
SELECT
    CONCAT(
        'ALTER TABLE `ITIS_utf8mb4`.`',
        TABLE_NAME,
        '` CONVERT TO CHARACTER SET utf8mb4 COLLATE utf8mb4_bin;'
    ) AS Script
FROM information_schema.TABLES
WHERE TABLE_SCHEMA = 'ITIS_utf8mb4'
  AND TABLE_TYPE = 'BASE TABLE'
ORDER BY TABLE_NAME;
```

Copy and execute all the generated statements (one `ALTER TABLE` statement per table).

**Example output:**

```sql
ALTER TABLE `ITIS_utf8mb4`.`change_comments`
  CONVERT TO CHARACTER SET utf8mb4 COLLATE utf8mb4_bin;

ALTER TABLE `ITIS_utf8mb4`.`change_operations`
  CONVERT TO CHARACTER SET utf8mb4 COLLATE utf8mb4_bin;

ALTER TABLE `ITIS_utf8mb4`.`hierarchy`
  CONVERT TO CHARACTER SET utf8mb4 COLLATE utf8mb4_bin;
```

---

## 8. Verify the Character Set Conversion

### 8.1. Check for Unconverted Columns

Run:

```sql
SELECT
    TABLE_NAME,
    COLUMN_NAME,
    CHARACTER_SET_NAME,
    COLLATION_NAME,
    COLUMN_TYPE
FROM information_schema.COLUMNS
WHERE TABLE_SCHEMA = 'ITIS_utf8mb4'
  AND CHARACTER_SET_NAME IS NOT NULL
  AND (
      CHARACTER_SET_NAME <> 'utf8mb4'
      OR COLLATION_NAME <> 'utf8mb4_bin'
  )
ORDER BY TABLE_NAME, ORDINAL_POSITION;
```

**Expected result:** No rows returned (empty set). If any rows appear, those columns still need to be converted.

### 8.2. Inspect All Converted Text Columns

```sql
SELECT
    TABLE_NAME,
    COLUMN_NAME,
    CHARACTER_SET_NAME,
    COLLATION_NAME,
    COLUMN_TYPE
FROM information_schema.COLUMNS
WHERE TABLE_SCHEMA = 'ITIS_utf8mb4'
  AND CHARACTER_SET_NAME IS NOT NULL
ORDER BY TABLE_NAME, ORDINAL_POSITION;
```

All character-based columns should display:

```text
CHARACTER_SET_NAME = utf8mb4
COLLATION_NAME     = utf8mb4_bin
```

*(Numeric, date, and other non-text columns do not have a character encoding and will not appear in this result.)*

### 8.3. Verify the Database Default Encoding

```sql
SELECT
    SCHEMA_NAME,
    DEFAULT_CHARACTER_SET_NAME,
    DEFAULT_COLLATION_NAME
FROM information_schema.SCHEMATA
WHERE SCHEMA_NAME = 'ITIS_utf8mb4';
```

**Expected result:**

```text
utf8mb4
utf8mb4_bin
```

---

## 9. Final Engine Verification

Check the storage engines again:

```sql
SELECT
    ENGINE,
    COUNT(*) AS TotalTables
FROM information_schema.TABLES
WHERE TABLE_SCHEMA = 'ITIS_utf8mb4'
  AND TABLE_TYPE = 'BASE TABLE'
GROUP BY ENGINE;
```

**Expected result:**

```text
InnoDB | 25
```

There should be no `MyISAM` tables remaining.

Also verify that the original `ITIS` database retains its original storage engines by comparing both installations side by side:

```sql
SELECT
    TABLE_SCHEMA,
    ENGINE,
    COUNT(*) AS TotalTables
FROM information_schema.TABLES
WHERE TABLE_SCHEMA IN ('ITIS', 'ITIS_utf8mb4')
  AND TABLE_TYPE = 'BASE TABLE'
GROUP BY TABLE_SCHEMA, ENGINE
ORDER BY TABLE_SCHEMA, ENGINE;
```

---

## 10. Back Up `ITIS_utf8mb4`

After completing all conversions and verifications, create a clean backup of the converted database.

From the WSL terminal, run:

```bash
mariadb-dump -u your_user_id -p \
  --default-character-set=utf8mb4 \
  --databases ITIS_utf8mb4 > ITIS_utf8mb4_full.sql
```

Store this file in a safe location. It will serve as your clean baseline for future database filtering and transformations without having to repeat the download and conversion steps.

---

## 11. Configure `InnoDB` as MariaDB's Default Engine

This section is optional, though recommended. The goal is to prevent new tables from being created with `MyISAM` when no storage engine is explicitly specified.

### 11.1. Locate the Configuration File

On Ubuntu, the server configuration file is usually located at:

```text
/etc/mysql/mariadb.conf.d/50-server.cnf
```

Open it for editing:

```bash
sudo nano /etc/mysql/mariadb.conf.d/50-server.cnf
```

### 11.2. Modify the `[mysqld]` Section

Within the `[mysqld]` section, add or update the following directive:

```ini
[mysqld]
default-storage-engine = InnoDB
```

You can also optionally add:

```ini
enforce_storage_engine = InnoDB
```

This second directive prevents tables from being created with any other storage engine, although its behavior when another engine is explicitly specified also depends on the `NO_ENGINE_SUBSTITUTION` SQL mode.

> **Caution:** Enabling `enforce_storage_engine` is **not recommended** if you plan to import third-party SQL dumps that explicitly declare other storage engines, as it may cause the import to fail.

### 11.3. Restart MariaDB

```bash
sudo systemctl restart mariadb
```

### 11.4. Verify the Configuration

Check that the service is running properly:

```bash
sudo systemctl status mariadb
```

Connect to MariaDB:

```bash
mariadb -u your_user_id -p
```

Run:

```sql
SHOW VARIABLES LIKE 'default_storage_engine';
SHOW VARIABLES LIKE 'enforce_storage_engine';
```

`default_storage_engine` should report `InnoDB`.

---

## 12. Final State

Once this procedure is complete, you will have two independent databases on your server:

| Feature            | `ITIS`                 | `ITIS_utf8mb4`   |
| :----------------- | :--------------------- | :--------------- |
| **Source**         | Official ITIS download | Clone of `ITIS`  |
| **Encoding**       | `latin1`               | `utf8mb4`        |
| **Collation**      | `latin1_bin`           | `utf8mb4_bin`    |
| **Storage Engine** | Original               | `InnoDB`         |
| **Row Format**     | Original               | `Dynamic`        |
| **Modified**       | No                     | Yes              |
| **Purpose**        | Immutable reference    | Working database |

### Summary of Results

- **`ITIS`** preserves an untouched reference copy of the official data.
- **`ITIS_utf8mb4`** is normalized and ready for taxonomic filtering, cleanup, and application development.
- You have a clean backup (`ITIS_utf8mb4_full.sql`) allowing you to reset or repeat downstream steps at any time without re-downloading or re-converting the original dump.