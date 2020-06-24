DROP TABLE IF EXISTS public."AspNetUserTokens" CASCADE;
DROP TABLE IF EXISTS public."AspNetUsers" CASCADE;
DROP TABLE IF EXISTS public."AspNetUserRoles" CASCADE;
DROP TABLE IF EXISTS public."AspNetUserLogins" CASCADE;
DROP TABLE IF EXISTS public."AspNetUserClaims" CASCADE;
DROP TABLE IF EXISTS public."AspNetRoles" CASCADE;
DROP TABLE IF EXISTS public."AspNetRoleClaims" CASCADE;
DROP TABLE IF EXISTS public."__EFMigrationsHistory" CASCADE;
DROP TABLE IF EXISTS public."Posts" CASCADE;

CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory"
(
    "MigrationId"    character varying(150) NOT NULL,
    "ProductVersion" character varying(32)  NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

CREATE TABLE "AspNetRoles"
(
    "Id"               integer                NOT NULL GENERATED BY DEFAULT AS IDENTITY,
    "Name"             character varying(256) NULL,
    "NormalizedName"   character varying(256) NULL,
    "ConcurrencyStamp" text                   NULL,
    CONSTRAINT "PK_AspNetRoles" PRIMARY KEY ("Id")
);

CREATE TABLE "AspNetUsers"
(
    "Id"                   integer                     NOT NULL GENERATED BY DEFAULT AS IDENTITY,
    "UserName"             character varying(256)      NULL,
    "NormalizedUserName"   character varying(256)      NULL,
    "Email"                character varying(256)      NULL,
    "NormalizedEmail"      character varying(256)      NULL,
    "EmailConfirmed"       boolean                     NOT NULL,
    "PasswordHash"         text                        NULL,
    "SecurityStamp"        text                        NULL,
    "ConcurrencyStamp"     text                        NULL,
    "PhoneNumber"          text                        NULL,
    "PhoneNumberConfirmed" boolean                     NOT NULL,
    "TwoFactorEnabled"     boolean                     NOT NULL,
    "LockoutEnd"           timestamp with time zone    NULL,
    "LockoutEnabled"       boolean                     NOT NULL,
    "AccessFailedCount"    integer                     NOT NULL,
    "FirstName"            text                        NOT NULL,
    "LastName"             text                        NOT NULL,
    "Birthdate"            timestamp without time zone NOT NULL,
    "Country"              text                        NOT NULL,
    "Genres"               text                        NOT NULL,
    CONSTRAINT "PK_AspNetUsers" PRIMARY KEY ("Id")
);

CREATE TABLE "AspNetRoleClaims"
(
    "Id"         integer NOT NULL GENERATED BY DEFAULT AS IDENTITY,
    "RoleId"     integer NOT NULL,
    "ClaimType"  text    NULL,
    "ClaimValue" text    NULL,
    CONSTRAINT "PK_AspNetRoleClaims" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_AspNetRoleClaims_AspNetRoles_RoleId" FOREIGN KEY ("RoleId") REFERENCES "AspNetRoles" ("Id") ON DELETE CASCADE
);

CREATE TABLE "AspNetUserClaims"
(
    "Id"         integer NOT NULL GENERATED BY DEFAULT AS IDENTITY,
    "UserId"     integer NOT NULL,
    "ClaimType"  text    NULL,
    "ClaimValue" text    NULL,
    CONSTRAINT "PK_AspNetUserClaims" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_AspNetUserClaims_AspNetUsers_UserId" FOREIGN KEY ("UserId") REFERENCES "AspNetUsers" ("Id") ON DELETE CASCADE
);

CREATE TABLE "AspNetUserLogins"
(
    "LoginProvider"       text    NOT NULL,
    "ProviderKey"         text    NOT NULL,
    "ProviderDisplayName" text    NULL,
    "UserId"              integer NOT NULL,
    CONSTRAINT "PK_AspNetUserLogins" PRIMARY KEY ("LoginProvider", "ProviderKey"),
    CONSTRAINT "FK_AspNetUserLogins_AspNetUsers_UserId" FOREIGN KEY ("UserId") REFERENCES "AspNetUsers" ("Id") ON DELETE CASCADE
);

CREATE TABLE "AspNetUserRoles"
(
    "UserId" integer NOT NULL,
    "RoleId" integer NOT NULL,
    CONSTRAINT "PK_AspNetUserRoles" PRIMARY KEY ("UserId", "RoleId"),
    CONSTRAINT "FK_AspNetUserRoles_AspNetRoles_RoleId" FOREIGN KEY ("RoleId") REFERENCES "AspNetRoles" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_AspNetUserRoles_AspNetUsers_UserId" FOREIGN KEY ("UserId") REFERENCES "AspNetUsers" ("Id") ON DELETE CASCADE
);

CREATE TABLE "AspNetUserTokens"
(
    "UserId"        integer NOT NULL,
    "LoginProvider" text    NOT NULL,
    "Name"          text    NOT NULL,
    "Value"         text    NULL,
    CONSTRAINT "PK_AspNetUserTokens" PRIMARY KEY ("UserId", "LoginProvider", "Name"),
    CONSTRAINT "FK_AspNetUserTokens_AspNetUsers_UserId" FOREIGN KEY ("UserId") REFERENCES "AspNetUsers" ("Id") ON DELETE CASCADE
);

CREATE INDEX "IX_AspNetRoleClaims_RoleId" ON "AspNetRoleClaims" ("RoleId");

CREATE UNIQUE INDEX "RoleNameIndex" ON "AspNetRoles" ("NormalizedName");

CREATE INDEX "IX_AspNetUserClaims_UserId" ON "AspNetUserClaims" ("UserId");

CREATE INDEX "IX_AspNetUserLogins_UserId" ON "AspNetUserLogins" ("UserId");

CREATE INDEX "IX_AspNetUserRoles_RoleId" ON "AspNetUserRoles" ("RoleId");

CREATE INDEX "EmailIndex" ON "AspNetUsers" ("NormalizedEmail");

CREATE UNIQUE INDEX "UserNameIndex" ON "AspNetUsers" ("NormalizedUserName");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20200525095951_InitialMigration', '3.1.4');

ALTER TABLE "AspNetUsers"
    ADD "RefreshToken" text NOT NULL DEFAULT '';

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20200605131837_AddRefreshToken', '3.1.4');

ALTER TABLE "AspNetUsers"
    ADD "ProfilePicture" text NULL;

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20200615154326_AddProfilePicture', '3.1.4');

CREATE TABLE "Posts"
(
    "Id"      integer NOT NULL GENERATED BY DEFAULT AS IDENTITY,
    "Title"   text    NULL,
    "Content" text    NULL,
    "UserId"  integer NOT NULL,
    CONSTRAINT "PK_Posts" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_Posts_AspNetUsers_UserId" FOREIGN KEY ("UserId") REFERENCES "AspNetUsers" ("Id") ON DELETE CASCADE
);

CREATE INDEX "IX_Posts_UserId" ON "Posts" ("UserId");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20200617130343_AddPosts', '3.1.4');

ALTER TABLE "Posts"
    ADD "DateOfCreation" timestamp without time zone NOT NULL DEFAULT TIMESTAMP '0001-01-01 00:00:00';

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20200623043228_AddDateOfCreationOfPost', '3.1.4');

ALTER TABLE "Posts"
    ALTER COLUMN "Title" TYPE text;
ALTER TABLE "Posts"
    ALTER COLUMN "Title" SET NOT NULL;
ALTER TABLE "Posts"
    ALTER COLUMN "Title" DROP DEFAULT;

ALTER TABLE "Posts"
    ALTER COLUMN "Content" TYPE text;
ALTER TABLE "Posts"
    ALTER COLUMN "Content" SET NOT NULL;
ALTER TABLE "Posts"
    ALTER COLUMN "Content" DROP DEFAULT;

ALTER TABLE "Posts"
    ADD "Edited" timestamp without time zone NOT NULL DEFAULT TIMESTAMP '0001-01-01 00:00:00';

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20200624080721_AddConstraintsToPostModel', '3.1.4');

ALTER TABLE "Posts"
    DROP COLUMN "Edited";

ALTER TABLE "Posts"
    ADD "DateOfEdit" timestamp without time zone NULL;

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20200624163021_MakeDateOfEditNullable', '3.1.4');
