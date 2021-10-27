CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
                                                       "MigrationId" character varying(150) NOT NULL,
                                                       "ProductVersion" character varying(32) NOT NULL,
                                                       CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

START TRANSACTION;

CREATE TABLE "JsonSchema" (
                              "SchemaName" text NOT NULL,
                              "JsonString" jsonb NULL,
                              CONSTRAINT "PK_JsonSchema" PRIMARY KEY ("SchemaName")
);

CREATE TABLE "Publisher" (
                             "Id" bigint GENERATED BY DEFAULT AS IDENTITY,
                             "PublisherName" text NULL,
                             CONSTRAINT "PK_Publisher" PRIMARY KEY ("Id")
);


CREATE TABLE "Article" (
                           "Id" bigint GENERATED BY DEFAULT AS IDENTITY,
                           "FilePath" text NULL,
                           "Title" text NULL,
                           "TotalWords" integer NOT NULL,
                           "PublisherId" bigint NULL,
                           CONSTRAINT "PK_Article" PRIMARY KEY ("Id"),
                           CONSTRAINT "FK_Article_Publisher_PublisherId" FOREIGN KEY ("PublisherId") REFERENCES "Publisher" ("Id") ON DELETE RESTRICT
);

CREATE TABLE "Term" (
                        "ArticleId" bigint NOT NULL,
                        "Word" text NOT NULL,
                        "Count" integer NOT NULL,
                        CONSTRAINT "PK_Term" PRIMARY KEY ("ArticleId", "Word"),
                        CONSTRAINT "FK_Term_Article_ArticleId" FOREIGN KEY ("ArticleId") REFERENCES "Article" ("Id") ON DELETE CASCADE
);

CREATE INDEX "IX_Article_PublisherId" ON "Article" ("PublisherId");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20211027132951_v2', '5.0.10');

COMMIT;