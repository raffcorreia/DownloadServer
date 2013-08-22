CREATE TABLE `tb_downloads` (

  `id` int(11) NOT NULL AUTO_INCREMENT,

  `data` datetime NOT NULL,

  `file_name` varchar(50) NOT NULL,

  `file_size` bigint NOT NULL,

  `bytes_transfered` bigint NOT NULL,

  `range_bytes_begin` bigint NOT NULL,

  `range_bytes_end` bigint NOT NULL,

  `status_code` int(11) NOT NULL,

  `is_only_head` bit NOT NULL,

  `SessionID` varchar(50) NOT NULL,

  `REMOTE_ADDR` varchar(20) DEFAULT NULL,

  `REMOTE_HOST` varchar(20) DEFAULT NULL,

  `HTTP_ACCEPT_LANGUAGE` varchar(50) DEFAULT NULL,

  `HTTP_HOST` varchar(50) DEFAULT NULL,

  `HTTP_USER_AGENT` varchar(500) DEFAULT NULL,

  PRIMARY KEY (`id`)

) AUTO_INCREMENT=0