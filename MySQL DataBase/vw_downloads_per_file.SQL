CREATE VIEW `simpledownloadcounter`.`vw_downloads_per_file` AS
SELECT	file_name, SUM(bytes_transfered) as bytes_transfered, SUM(file_size) as file_size, 
		TRUNCATE(SUM(total), 0) as total
FROM	vw_downloads_raw
GROUP BY file_name