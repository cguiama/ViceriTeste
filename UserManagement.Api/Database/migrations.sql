Build started...
Build succeeded.
warn: 25/05/2024 03:20:03.579 CoreEventId.SensitiveDataLoggingEnabledWarning[10400] (Microsoft.EntityFrameworkCore.Infrastructure) 
      Sensitive data logging is enabled. Log entries and exception messages may include sensitive application data; this mode should only be enabled during development.
CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" TEXT NOT NULL CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY,
    "ProductVersion" TEXT NOT NULL
);

BEGIN TRANSACTION;

CREATE TABLE "Clients" (
    "Id" TEXT NOT NULL CONSTRAINT "PK_Clients" PRIMARY KEY,
    "Nome" TEXT NOT NULL,
    "Mail" TEXT NOT NULL,
    "Senha" TEXT NOT NULL,
    "Cpf" TEXT NOT NULL,
    "Nasc" TEXT NOT NULL
);

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20240525060055_Initial', '8.0.5');

COMMIT;


