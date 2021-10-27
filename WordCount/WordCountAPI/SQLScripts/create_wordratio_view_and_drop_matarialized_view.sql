DROP materialized view wordratios;

CREATE VIEW wordratios AS
SELECT appearsin.id,
       appearsin.wordname,
       appearsin.amount,
       filelist.articletitle,
       filelist.filepath,
       filelist.totalwordsinarticle,
       filelist.sourceid,
       externalsources.sourcename,
       round(appearsin.amount::numeric / filelist.totalwordsinarticle::numeric * 100::numeric, 2) AS percent
FROM filelist
         JOIN appearsin ON filelist.filepath = appearsin.filepath AND filelist.articletitle = appearsin.articletitle
         JOIN externalsources ON filelist.sourceid = externalsources.id
ORDER BY (round(appearsin.amount::numeric / filelist.totalwordsinarticle::numeric * 100::numeric, 2)) DESC;