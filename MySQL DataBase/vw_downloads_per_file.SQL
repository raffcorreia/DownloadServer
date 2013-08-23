CREATE VIEW `simpledownloadcounter`.`vw_downloads_per_file` AS
SELECT	file_name, SUM(bytes_transfered) as bytes_transfered, SUM(file_size) as total_size,
		TRUNCATE((SUM(bytes_transfered) / file_size), 0) as total
FROM	tb_downloads
GROUP BY file_name