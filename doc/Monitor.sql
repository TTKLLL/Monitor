/*
MySQL Backup
Database: lc
Backup Time: 2019-08-02 09:56:20
*/

SET FOREIGN_KEY_CHECKS=0;
DROP TABLE IF EXISTS `lc`.`baseinfo`;
DROP TABLE IF EXISTS `lc`.`data`;
DROP TABLE IF EXISTS `lc`.`device`;
DROP TABLE IF EXISTS `lc`.`linkattr`;
DROP TABLE IF EXISTS `lc`.`menue`;
DROP TABLE IF EXISTS `lc`.`module`;
DROP TABLE IF EXISTS `lc`.`point`;
DROP TABLE IF EXISTS `lc`.`pointinfo`;
DROP TABLE IF EXISTS `lc`.`td`;
DROP TABLE IF EXISTS `lc`.`xmport`;
CREATE TABLE `baseinfo` (
  `name` varchar(255) DEFAULT NULL,
  `value` varchar(255) DEFAULT NULL,
  `id` int(11) NOT NULL AUTO_INCREMENT,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=gbk;
CREATE TABLE `data` (
  `sno` varchar(15) DEFAULT NULL,
  `cycle` varchar(15) DEFAULT NULL,
  `time` datetime DEFAULT NULL,
  `tdno` varchar(10) DEFAULT NULL,
  `res` float(10,3) DEFAULT NULL,
  `dy` float DEFAULT NULL,
  `port` int(11) DEFAULT NULL,
  `pointName` varchar(20) DEFAULT NULL,
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `number` varchar(20) DEFAULT NULL,
  `dataType` varchar(50) DEFAULT NULL,
  `deep` float(255,3) DEFAULT NULL,
  `Unit` varchar(50) DEFAULT NULL,
  `valueOne` double(255,3) DEFAULT NULL,
  `valueTwo` double(255,3) DEFAULT NULL,
  `valueThree` double(255,3) DEFAULT NULL,
  `xmno` int(11) unsigned zerofill DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=4992 DEFAULT CHARSET=gbk;
CREATE TABLE `device` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `deviceId` varchar(100) DEFAULT NULL,
  `deviceInfo` varchar(255) DEFAULT NULL,
  `company` varchar(255) DEFAULT NULL,
  `type` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=21 DEFAULT CHARSET=gbk;
CREATE TABLE `linkattr` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `uid` varchar(20) DEFAULT NULL,
  `pwd` varchar(100) DEFAULT NULL,
  `linkAttrName` varchar(255) DEFAULT NULL,
  `server` varchar(255) DEFAULT NULL,
  `port` varchar(255) DEFAULT NULL,
  `xmno` int(11) DEFAULT NULL,
  `database` varchar(20) DEFAULT NULL,
  `driver` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=gbk;
CREATE TABLE `menue` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(50) DEFAULT NULL,
  `parentId` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=gbk;
CREATE TABLE `module` (
  `mkno` varchar(50) NOT NULL,
  `port` int(10) DEFAULT NULL,
  PRIMARY KEY (`mkno`)
) ENGINE=InnoDB DEFAULT CHARSET=gbk;
CREATE TABLE `point` (
  `tdno` varchar(10) NOT NULL,
  `sno` varchar(15) NOT NULL,
  `pointId` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`sno`,`tdno`)
) ENGINE=InnoDB DEFAULT CHARSET=gbk;
CREATE TABLE `pointinfo` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `pointName` varchar(255) DEFAULT NULL,
  `tdno` varchar(20) DEFAULT NULL,
  `t0` float(255,3) DEFAULT NULL,
  `k0` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=gbk;
CREATE TABLE `td` (
  `tdno` varchar(20) NOT NULL,
  `mkno` varchar(50) NOT NULL,
  `deviceId` varchar(100) DEFAULT NULL,
  `pointName` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`tdno`,`mkno`)
) ENGINE=InnoDB DEFAULT CHARSET=gbk;
CREATE TABLE `xmport` (
  `port` int(11) NOT NULL,
  `xmno` varchar(15) DEFAULT NULL,
  `dataType` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`port`)
) ENGINE=InnoDB DEFAULT CHARSET=gbk;
BEGIN;
LOCK TABLES `lc`.`baseinfo` WRITE;
DELETE FROM `lc`.`baseinfo`;
INSERT INTO `lc`.`baseinfo` (`name`,`value`,`id`) VALUES ('ip', '192.168.168.96', 2);
UNLOCK TABLES;
COMMIT;
BEGIN;
LOCK TABLES `lc`.`data` WRITE;
DELETE FROM `lc`.`data`;
INSERT INTO `lc`.`data` (`sno`,`cycle`,`time`,`tdno`,`res`,`dy`,`port`,`pointName`,`id`,`number`,`dataType`,`deep`,`Unit`,`valueOne`,`valueTwo`,`valueThree`,`xmno`) VALUES ('NF-SY02', '0000000001', '2019-07-11 16:55:03', '12', NULL, 3.43, 2000, '测点1', 4980, '12', '渗压', NULL, NULL, 0.008, NULL, NULL, 00000000123),('JMZX-489AT', '0000000001', '2019-07-11 16:54:54', '11', NULL, 3.43, 2000, '测点1', 4981, '11', '钢筋', NULL, NULL, -145.203, NULL, NULL, 00000000123),('JMZX-488AT', '0000000001', '2019-07-11 16:54:46', '10', NULL, 3.43, 2000, '测点3', 4982, '10', '钢筋', NULL, NULL, -147.476, NULL, NULL, 00000000123),('NF-BM30', '0000000001', '2019-07-11 16:54:38', '9', NULL, 3.43, 2000, '测点3', 4983, '9', '表面应变', NULL, NULL, 3264.519, NULL, NULL, 00000000123),('NF-MS3S50T', '0000000001', '2019-07-11 16:54:29', '8', NULL, 3.43, 2000, '测点3', 4984, '8', '锚索-三弦', NULL, NULL, 0.000, NULL, NULL, 00000000123),('NF-MS3S50T', '0000000001', '2019-07-11 16:54:21', '7', NULL, 3.43, 2000, '测点3', 4985, '7', '锚索-三弦', NULL, NULL, 0.000, NULL, NULL, 00000000123),('NF-MS3S50T', '0000000001', '2019-07-11 16:54:13', '6', NULL, 3.43, 2000, '测点3', 4986, '6', '锚索-三弦', NULL, NULL, 0.000, NULL, NULL, 00000000123),('NFZL-30007', '0000000001', '2019-07-11 16:54:06', '5', NULL, 3.43, 2000, '测点2', 4987, '5', '轴力-三弦', NULL, NULL, -4955.080, NULL, NULL, 00000000123),('NFZL-30007', '0000000001', '2019-07-11 16:53:58', '4', NULL, 3.43, 2000, '测点2', 4988, '4', '轴力-三弦', NULL, NULL, -4793.678, NULL, NULL, 00000000123),('NFZL-30007', '0000000001', '2019-07-11 16:53:50', '3', NULL, 3.43, 2000, '测点2', 4989, '3', '轴力-三弦', NULL, NULL, -4956.763, NULL, NULL, 00000000123),('NF-TY04', '0000000001', '2019-07-11 16:53:41', '2', NULL, 3.43, 2000, '测点1', 4990, '2', '土压', NULL, NULL, -0.002, NULL, NULL, 00000000123),('NF-ZL300T', '0000000001', '2019-07-11 16:53:33', '1', NULL, 3.43, 2000, '测点1', 4991, '1', '轴力-单弦', NULL, NULL, -7633.968, NULL, NULL, 00000000123);
UNLOCK TABLES;
COMMIT;
BEGIN;
LOCK TABLES `lc`.`device` WRITE;
DELETE FROM `lc`.`device`;
INSERT INTO `lc`.`device` (`id`,`deviceId`,`deviceInfo`,`company`,`type`) VALUES (12, 'NF-ZL300T', 'ZL30-2018380026', '一芯科技', '轴力-单弦'),(13, 'NF-TY04', 'TY06 1919-TY0001-02', '一芯科技', '土压'),(14, 'NFZL-30007', 'ZL30-2019050001', '一芯科技', '轴力-三弦'),(15, 'NF-MS3S50T', 'B20180928002-0097', '一芯科技', '锚索-三弦'),(16, 'NF-BM30', 'BM30-2019100579', '一芯科技', '表面应变'),(17, 'JMZX-488AT', '330517', '金码', '钢筋'),(18, 'JMZX-489AT', '340846', '金码', '钢筋'),(19, 'NF-SY02', 'K402-2019080040', '一芯科技', '渗压');
UNLOCK TABLES;
COMMIT;
BEGIN;
LOCK TABLES `lc`.`linkattr` WRITE;
DELETE FROM `lc`.`linkattr`;
UNLOCK TABLES;
COMMIT;
BEGIN;
LOCK TABLES `lc`.`menue` WRITE;
DELETE FROM `lc`.`menue`;
UNLOCK TABLES;
COMMIT;
BEGIN;
LOCK TABLES `lc`.`module` WRITE;
DELETE FROM `lc`.`module`;
INSERT INTO `lc`.`module` (`mkno`,`port`) VALUES ('zx0060', 2000);
UNLOCK TABLES;
COMMIT;
BEGIN;
LOCK TABLES `lc`.`point` WRITE;
DELETE FROM `lc`.`point`;
UNLOCK TABLES;
COMMIT;
BEGIN;
LOCK TABLES `lc`.`pointinfo` WRITE;
DELETE FROM `lc`.`pointinfo`;
INSERT INTO `lc`.`pointinfo` (`id`,`pointName`,`tdno`,`t0`,`k0`) VALUES (7, '测点1', NULL, NULL, NULL),(8, '测点2', NULL, NULL, NULL),(9, '测点3', NULL, NULL, NULL);
UNLOCK TABLES;
COMMIT;
BEGIN;
LOCK TABLES `lc`.`td` WRITE;
DELETE FROM `lc`.`td`;
INSERT INTO `lc`.`td` (`tdno`,`mkno`,`deviceId`,`pointName`) VALUES ('1', 'zx0060', 'NF-ZL300T', '测点1'),('10', 'zx0060', 'JMZX-488AT', '测点3'),('11', 'zx0060', 'JMZX-489AT', '测点1'),('12', 'zx0060', 'NF-SY02', '测点1'),('2', 'zx0060', 'NF-TY04', '测点1'),('3', 'zx0060', 'NFZL-30007', '测点2'),('4', 'zx0060', 'NFZL-30007', '测点2'),('5', 'zx0060', 'NFZL-30007', '测点2'),('6', 'zx0060', 'NF-MS3S50T', '测点3'),('7', 'zx0060', 'NF-MS3S50T', '测点3'),('8', 'zx0060', 'NF-MS3S50T', '测点3'),('9', 'zx0060', 'NF-BM30', '测点3');
UNLOCK TABLES;
COMMIT;
BEGIN;
LOCK TABLES `lc`.`xmport` WRITE;
DELETE FROM `lc`.`xmport`;
INSERT INTO `lc`.`xmport` (`port`,`xmno`,`dataType`) VALUES (2000, '123', '');
UNLOCK TABLES;
COMMIT;
