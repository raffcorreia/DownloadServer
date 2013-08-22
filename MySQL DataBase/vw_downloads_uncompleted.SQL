CREATE VIEW `simpledownloadcounter`.`vw_downloads_uncompleted` AS
SELECT file_name, SUM(bytes_transfered) as bytes_transfered, file_size
FROM Tb_Downloads
WHERE bytes_transfered > 0
GROUP BY file_name, SessionID, file_size
HAVING (SUM(bytes_transfered) / file_size) < 1