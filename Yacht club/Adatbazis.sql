DROP TABLE IF EXISTS `endevice`;
CREATE TABLE IF NOT EXISTS `endevice` (
  `device_id` int(4) NOT NULL,
  `type` varchar(20) NOT NULL,
  `hire` int(1) NOT NULL DEFAULT '1',
  `busy` int(1) NOT NULL DEFAULT '0',
  `daly_price` int(20) DEFAULT NULL,
  `max_width` int(3) NOT NULL,
  `max_lenght` int(3) NOT NULL,
  `max_height` int(3) NOT NULL,
  `max_weight` int(3) NOT NULL,
  `ower` int(4) NOT NULL,
  `renter` int(4) DEFAULT NULL,
  PRIMARY KEY (`device_id`),
  UNIQUE KEY `render` (`renter`),
  KEY `ower` (`ower`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

DROP TABLE IF EXISTS `endevice_rent`;
CREATE TABLE IF NOT EXISTS `endevice_rent` (
  `rent_id` int(4) NOT NULL,
  `start_date` datetime NOT NULL,
  `end_date` datetime NOT NULL,
  `device_id` int(4) NOT NULL,
  `renter_id` int(4) NOT NULL,
  PRIMARY KEY (`rent_id`),
  KEY `render_id` (`renter_id`),
  KEY `device_id` (`device_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

DROP TABLE IF EXISTS `enlogin`;
CREATE TABLE IF NOT EXISTS `enlogin` (
  `login_id` int(4) NOT NULL,
  `login_name` varchar(25) NOT NULL,
  `password` varchar(500) NOT NULL,
  `email` varchar(30) NOT NULL,
  `theme` int(1) NOT NULL DEFAULT '1',
  `admin` int(1) NOT NULL,
  `last_login` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`login_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

INSERT INTO `enlogin` (`login_id`, `login_name`, `password`, `email`, `theme`, `admin`, `last_login`) VALUES
(0, 'admin', 'LnrqcycsIw70smaFQ20SMP3ChHdqCICcpn5C6RrA1BfciQ19Jb8MqnI7/9pJ+v2SfXiDlDL8r06Y7JXzxNby8e7BlDPZKQwvjgcP7Sv6p7BkGo692Y+jMU0CNDrbXkBEoR31BFdphKT4e3X/YATVJleMVbi2G/4kZK2pZbDM7Vk=', 'YachtClub@Infok.hu', 1, 1, '2018-04-10 20:58:07'),
(1, 'jacjozs', 'zd7WLm0bocg/yC9T2YURaSpinxsgU33jd/V2OOFpFdSFJQCw55J1inc+eBtHYr74O4EjVUWlqBC7tprfwA5lYdlMBqYs1judsaEnBrDgsLTGWzdhAzm7Xmlfy9LB6YCdhfwVUVPD+B1warXJ5aBvjMbsexrRrksjcfmIiPCNNV4=', 'jacjozs@gmail.com', 1, 0, '2018-04-11 22:24:51');

DROP TABLE IF EXISTS `enmessage`;
CREATE TABLE IF NOT EXISTS `enmessage` (
  `message_id` int(4) NOT NULL,
  `date` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `sender` int(4) NOT NULL,
  `addressee` int(4) NOT NULL,
  `yacht_id` int(4) DEFAULT NULL,
  `device_id` int(4) DEFAULT NULL,
  `start_date` datetime NOT NULL,
  `end_date` datetime NOT NULL,
  `from_port` int(4) NOT NULL,
  `to_port` int(4) NOT NULL,
  `accept` int(1) DEFAULT NULL,
  `new` int(1) NOT NULL DEFAULT '1',
  `back` int(1) DEFAULT '0',
  PRIMARY KEY (`message_id`),
  KEY `sender` (`sender`),
  KEY `addressee` (`addressee`),
  KEY `yacht_id` (`yacht_id`),
  KEY `device_id` (`device_id`),
  KEY `from_port` (`from_port`),
  KEY `to_port` (`to_port`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

DROP TABLE IF EXISTS `enport`;
CREATE TABLE IF NOT EXISTS `enport` (
  `port_id` int(4) NOT NULL,
  `name` varchar(30) NOT NULL,
  `country` varchar(70) NOT NULL,
  PRIMARY KEY (`port_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

INSERT INTO `enport` (`port_id`, `name`, `country`) VALUES
(0, 'Ismeretlen', 'Ismeretlen'),
(1, 'Csepeli Szabadkikötő', 'Magyarország'),
(2, 'Bajai ÁTI DEPO kikötő', 'Magyarország'),
(3, 'Bajai OKK RO-RO kikötő', 'Magyarország'),
(4, 'Dunaferr kikötő', 'Magyarország'),
(5, 'Dunavecse kikötő', 'Magyarország'),
(6, 'Fadd-Dombori kikötő', 'Magyarország'),
(7, 'FERROPORT', 'Magyarország'),
(8, 'Győr-Gönyű kikötő', 'Magyarország'),
(9, 'Hárosi hajókikötő', 'Magyarország'),
(10, 'MAHART Gabonatárház', 'Magyarország'),
(11, 'Mahart Container Center', 'Magyarország'),
(12, 'Mohács kikötő', 'Magyarország'),
(13, 'Paks kikötő', 'Magyarország'),
(14, 'Szászhalombatta kikötő', 'Magyarország'),
(15, 'Rotterdam', 'Hollandia'),
(16, 'Dubai', 'Egyesült Arab Emirátusok'),
(17, 'Qingdao', 'Kína'),
(18, 'Guangzhou', 'Kína'),
(19, 'Ningbo', 'Kína'),
(20, 'Busan', 'Dél-Korea'),
(21, 'Shenzen', 'Kína'),
(22, 'Hongkong', 'Kína'),
(23, 'Szingapúr', 'Szingapúr'),
(24, 'Shanghai', 'Kína');

DROP TABLE IF EXISTS `entravel`;
CREATE TABLE IF NOT EXISTS `entravel` (
  `travel_id` int(4) NOT NULL,
  `date` datetime NOT NULL,
  `yacht_rent_id` int(4) NOT NULL,
  `port_id` int(4) NOT NULL,
  PRIMARY KEY (`travel_id`),
  KEY `yacht_rent_id` (`yacht_rent_id`),
  KEY `port_id` (`port_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

DROP TABLE IF EXISTS `enyacht`;
CREATE TABLE IF NOT EXISTS `enyacht` (
  `yacht_id` int(4) NOT NULL,
  `name` varchar(30) NOT NULL,
  `producer` varchar(30) NOT NULL,
  `image` longblob,
  `ower` int(4) NOT NULL,
  `renter` int(4) DEFAULT NULL,
  `seats` int(4) DEFAULT NULL,
  `hire` int(1) DEFAULT '1',
  `busy` int(1) DEFAULT '0',
  `daly_price` int(20) DEFAULT NULL,
  `width` float NOT NULL,
  `lenght` float NOT NULL,
  `dive` float NOT NULL,
  `speed` int(5) NOT NULL,
  `port_id` int(4) DEFAULT '0',
  PRIMARY KEY (`yacht_id`),
  UNIQUE KEY `renter` (`renter`),
  KEY `ower` (`ower`),
  KEY `port_id` (`port_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

INSERT INTO `enyacht` (`yacht_id`, `name`, `producer`, `image`, `ower`, `renter`, `seats`, `hire`, `busy`, `daly_price`, `width`, `lenght`, `dive`, `speed`, `port_id`) VALUES
(1, 'ASHA', 'Maiora', 0x30, 1, NULL, 12, 0, 0, 1000, 6.3, 27.4, 1.7, 23, 0),
(2, 'AA ABSOLUTE', 'Heesen', NULL, 1, NULL, 14, 1, 0, NULL, 7.15, 36.75, 1.6, 33, 0),
(3, 'AB 116', 'AB Yachts', NULL, 1, NULL, 16, 1, 0, NULL, 7.5, 36.25, 1.6, 38, 0);

DROP TABLE IF EXISTS `enyacht_club_tag`;
CREATE TABLE IF NOT EXISTS `enyacht_club_tag` (
  `member_id` int(4) NOT NULL,
  `login_id` int(4) NOT NULL,
  `nickname` varchar(25) NOT NULL,
  `first_name` varchar(25) NOT NULL,
  `last_name` varchar(25) NOT NULL,
  `image` longblob,
  `zip_code` int(4) NOT NULL,
  `address` varchar(50) NOT NULL,
  `country` varchar(50) NOT NULL,
  `birthday` datetime NOT NULL,
  PRIMARY KEY (`member_id`),
  KEY `zip_code` (`zip_code`),
  KEY `login_id` (`login_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

INSERT INTO `enyacht_club_tag` (`member_id`, `login_id`, `nickname`, `first_name`, `last_name`, `image`, `zip_code`, `address`, `country`, `birthday`) VALUES
(0, 0, 'Admin', 'admin', 'admin', NULL, 6000, 'admin', 'admin', '2018-04-01 00:00:00'),
(1, 1, 'Hikaru', 'Jaczina', 'József', NULL, 6000, 'Valahol', 'Magyaroszág', '1997-04-29 00:00:00');

DROP TABLE IF EXISTS `enyacht_rent`;
CREATE TABLE IF NOT EXISTS `enyacht_rent` (
  `rent_id` int(4) NOT NULL,
  `start_date` datetime NOT NULL,
  `end_date` datetime NOT NULL,
  `yacht_id` int(4) NOT NULL,
  `renter_id` int(4) NOT NULL,
  `from` int(4) NOT NULL,
  `to` int(4) NOT NULL,
  PRIMARY KEY (`rent_id`),
  KEY `render_id` (`renter_id`),
  KEY `yacht_id` (`yacht_id`),
  KEY `from` (`from`),
  KEY `to` (`to`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

DROP TABLE IF EXISTS `enzipcode`;
CREATE TABLE IF NOT EXISTS `enzipcode` (
  `zip_code` int(4) NOT NULL,
  `city` varchar(30) DEFAULT NULL,
  PRIMARY KEY (`zip_code`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

INSERT INTO `enzipcode` (`zip_code`, `city`) VALUES
(6000, 'Kecskemét');

ALTER TABLE `endevice`
  ADD CONSTRAINT `enDevice_ibfk_1` FOREIGN KEY (`ower`) REFERENCES `enyacht_club_tag` (`member_id`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `enDevice_ibfk_2` FOREIGN KEY (`renter`) REFERENCES `enyacht_club_tag` (`member_id`) ON DELETE SET NULL ON UPDATE CASCADE;

ALTER TABLE `endevice_rent`
  ADD CONSTRAINT `enDevice_rent_ibfk_1` FOREIGN KEY (`renter_id`) REFERENCES `enyacht_club_tag` (`member_id`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `enDevice_rent_ibfk_2` FOREIGN KEY (`device_id`) REFERENCES `endevice` (`device_id`) ON DELETE CASCADE ON UPDATE CASCADE;

ALTER TABLE `enmessage`
  ADD CONSTRAINT `enMessage_ibfk_1` FOREIGN KEY (`sender`) REFERENCES `enyacht_club_tag` (`member_id`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `enMessage_ibfk_2` FOREIGN KEY (`addressee`) REFERENCES `enyacht_club_tag` (`member_id`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `enMessage_ibfk_3` FOREIGN KEY (`yacht_id`) REFERENCES `enyacht` (`yacht_id`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `enMessage_ibfk_4` FOREIGN KEY (`device_id`) REFERENCES `endevice` (`device_id`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `enMessage_ibfk_5` FOREIGN KEY (`from_port`) REFERENCES `enport` (`port_id`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `enMessage_ibfk_6` FOREIGN KEY (`to_port`) REFERENCES `enport` (`port_id`) ON DELETE CASCADE ON UPDATE CASCADE;

ALTER TABLE `entravel`
  ADD CONSTRAINT `enTravel_ibfk_1` FOREIGN KEY (`yacht_rent_id`) REFERENCES `enyacht_rent` (`rent_id`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `enTravel_ibfk_2` FOREIGN KEY (`port_id`) REFERENCES `enport` (`port_id`) ON DELETE CASCADE ON UPDATE CASCADE;

ALTER TABLE `enyacht`
  ADD CONSTRAINT `enYacht_ibfk_1` FOREIGN KEY (`ower`) REFERENCES `enyacht_club_tag` (`member_id`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `enYacht_ibfk_2` FOREIGN KEY (`port_id`) REFERENCES `enport` (`port_id`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `enYacht_ibfk_3` FOREIGN KEY (`renter`) REFERENCES `enyacht_club_tag` (`member_id`) ON DELETE SET NULL ON UPDATE CASCADE;

ALTER TABLE `enyacht_club_tag`
  ADD CONSTRAINT `enYacht_Club_Tag_ibfk_1` FOREIGN KEY (`zip_code`) REFERENCES `enzipcode` (`zip_code`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `enYacht_Club_Tag_ibfk_2` FOREIGN KEY (`login_id`) REFERENCES `enlogin` (`login_id`) ON DELETE CASCADE ON UPDATE CASCADE;

ALTER TABLE `enyacht_rent`
  ADD CONSTRAINT `enYacht_rent_ibfk_1` FOREIGN KEY (`renter_id`) REFERENCES `enyacht_club_tag` (`member_id`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `enYacht_rent_ibfk_2` FOREIGN KEY (`yacht_id`) REFERENCES `enyacht` (`yacht_id`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `enYacht_rent_ibfk_3` FOREIGN KEY (`from`) REFERENCES `enport` (`port_id`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `enYacht_rent_ibfk_4` FOREIGN KEY (`to`) REFERENCES `enport` (`port_id`) ON DELETE CASCADE ON UPDATE CASCADE;
