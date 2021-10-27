CREATE VIEW WordRatio AS
SELECT "ArticleId", "Word", "Count", "Title", "FilePath", "TotalWords","PublisherId", "PublisherName",
       round("Count"::numeric / "TotalWords"::numeric * 100::numeric, 2) AS Percent

FROM "Article"
         JOIN "Term" ON "ArticleId" = "Article"."Id"
         JOIN "Publisher" ON "Article"."PublisherId" = "PublisherId"
