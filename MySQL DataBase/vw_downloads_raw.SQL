CREATE VIEW `simpledownloadcounter`.`vw_downloads_raw` AS

SELECT	file_name, SUM(bytes_transfered) as bytes_transfered, SUM(file_size) as file_size, 
		(SUM(file_size) / SUM(bytes_transfered)) as total
FROM 	vw_downloads_uncompleted
GROUP BY file_name

UNION

SELECT	file_name, SUM(bytes_transfered) as bytes_transfered, SUM(file_size) as file_size, 
		COUNT(file_name) as total
FROM 	vw_downloads_completed
GROUP BY file_name