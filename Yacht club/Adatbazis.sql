
SET FOREIGN_KEY_CHECKS=0;

--
-- Adatbázis: `yacht_club`
--

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `enDevice_rent`
--

DROP TABLE IF EXISTS `enDevice_rent`;
CREATE TABLE IF NOT EXISTS `enDevice_rent` (
  `rent_id` int(4) NOT NULL,
  `start_date` datetime NOT NULL,
  `end_date` datetime NOT NULL,
  `device_id` int(4) NOT NULL,
  `render_id` int(4) NOT NULL,
  PRIMARY KEY (`rent_id`),
  KEY `render_id` (`render_id`),
  KEY `device_id` (`device_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `loan_yacht`
--

DROP TABLE IF EXISTS `enYacht_rent`;
CREATE TABLE IF NOT EXISTS `enYacht_rent` (
  `rent_id` int(4) NOT NULL,
  `start_date` datetime NOT NULL,
  `end_date` datetime NOT NULL,
  `yacht_id` int(4) NOT NULL,
  `render_id` int(4) NOT NULL,
  `from` int(4) NOT NULL,
  `to` int(4) NOT NULL,
  PRIMARY KEY (`rent_id`),
  KEY `render_id` (`render_id`),
  KEY `yacht_id` (`yacht_id`),
  KEY `from` (`from`),
  KEY `to` (`to`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `transport_device`
--

DROP TABLE IF EXISTS `enDevice`;
CREATE TABLE IF NOT EXISTS `enDevice` (
  `device_id` int(4) NOT NULL,
  `type` varchar(20) NOT NULL,
  `hire` int(1) NOT NULL default '1',
  `busy` int(1) NOT NULL default '0',
  `max_width` int(4) NOT NULL,
  `max_lenght` int(4) NOT NULL,
  `max_height` int(4) NOT NULL,
  `max_weight` int(4) NOT NULL,
  `ower` int(4) not null,
  PRIMARY KEY (`device_id`),
  KEY `ower` (`ower`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `travels`
--

DROP TABLE IF EXISTS `enTravel`;
CREATE TABLE IF NOT EXISTS `enTravel` (
  `travel_id` int(4) NOT NULL,
  `date` datetime NOT NULL,
  `yacht_rent_id` int(4) NOT NULL,
  `port_id` int(4) NOT NULL,
  PRIMARY KEY (`travel_id`),
  KEY `yacht_rent_id` (`yacht_rent_id`),
  KEY `port_id` (`port_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `yachts`
--

DROP TABLE IF EXISTS `enYacht`;
CREATE TABLE IF NOT EXISTS `enYacht` (
  `yacht_id` int(4) NOT NULL,
  `name` varchar(30) NOT NULL,
  `type` varchar(30) NOT NULL,
  `image` int(2) DEFAULT NULL,
  `ower` int(6) NOT NULL,
  `seats` int(4) DEFAULT NULL,
  `hire` int(1) DEFAULT '1',
  `busy` int(1) DEFAULT '0',
  `daly_price` int(20) not null,
  `width` int(4) NOT NULL,
  `lenght` int(4) NOT NULL,
  `height` int(4) NOT NULL,
  `weight` int(4) NOT NULL,
  `port_id` int(4) NOT NULL,
  PRIMARY KEY (`yacht_id`),
  KEY `ower` (`ower`),
  KEY `port_id` (`port_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `yacht_club_tags`
--

DROP TABLE IF EXISTS `enYacht_Club_Tag`;
CREATE TABLE IF NOT EXISTS `enYacht_Club_Tag` (
  `member_id` int(4) NOT NULL,
  `login_id` int(4) NOT NULL,
  `nickname` varchar(25) NOT NULL,
  `first_name` varchar(25) NOT NULL,
  `last_name` varchar(25) NOT NULL,
  `image` int(2) DEFAULT NULL,
  `zip_code` int(4) NOT NULL,
  `adress` varchar(50) NOT NULL,
  `country` varchar(50) NOT NULL,
  `birthday` datetime NOT NULL,
  PRIMARY KEY (`member_id`),
  KEY `zip_code` (`zip_code`),
  KEY `login_id` (`login_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `yacht_login`
--

DROP TABLE IF EXISTS `enLogin`;
CREATE TABLE IF NOT EXISTS `enLogin` (
  `login_id` int(4) NOT NULL,
  `login_name` varchar(25) NOT NULL,
  `password` varchar(65) NOT NULL,
  `email` varchar(30) NOT NULL,
  `admin` int(1) NOT NULL,
  `last_login` datetime NOT NULL,
  PRIMARY KEY (`login_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

DROP TABLE IF EXISTS `enMessage`;
CREATE TABLE IF NOT EXISTS `enMessage` (
  `message_id` int(4) NOT NULL,
  `date` datetime NOT NULL,
  `sender` int(4) NOT NULL,
  `addressee` int(4) NOT NULL,
  `yacht_id` int(4) Default null,
  `device_id` int(4) Default null,
  `accept` int(1) NOT NULL,
  `new` int(1) NOT NULL,
  PRIMARY KEY (`message_id`),
  KEY `sender` (`sender`),
  KEY `addressee` (`addressee`),
  KEY `yacht_id` (`yacht_id`),
  KEY `device_id` (`device_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `zip_codes`
--

DROP TABLE IF EXISTS `enZipcode`;
CREATE TABLE IF NOT EXISTS `enZipcode` (
  `zip_code` int(4) NOT NULL,
  `city` varchar(30) DEFAULT NULL,
  PRIMARY KEY (`zip_code`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;


DROP TABLE IF EXISTS `enPort`;
CREATE TABLE IF NOT EXISTS `enPort` (
  `port_id` int(4) NOT NULL,
  `name` varchar(30) NOT NULL,
  PRIMARY KEY (`port_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Megkötések a kiírt táblákhoz
--

--
-- Megkötések a táblához `enDevice_rent`
--
ALTER TABLE `enDevice_rent`
  ADD CONSTRAINT `enDevice_rent_ibfk_1` FOREIGN KEY (`render_id`) REFERENCES `enYacht_Club_Tag` (`member_id`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `enDevice_rent_ibfk_2` FOREIGN KEY (`device_id`) REFERENCES `enDevice` (`device_id`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Megkötések a táblához `loan_yacht`
--
ALTER TABLE `enYacht_rent`
  ADD CONSTRAINT `enYacht_rent_ibfk_1` FOREIGN KEY (`render_id`) REFERENCES `enYacht_Club_Tag` (`member_id`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `enYacht_rent_ibfk_2` FOREIGN KEY (`yacht_id`) REFERENCES `enYacht` (`yacht_id`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `enYacht_rent_ibfk_3` FOREIGN KEY (`from`) REFERENCES `enPort` (`port_id`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `enYacht_rent_ibfk_4` FOREIGN KEY (`to`) REFERENCES `enPort` (`port_id`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Megkötések a táblához `enDevice`
--
ALTER TABLE `enDevice`
  ADD CONSTRAINT `enDevice_ibfk_1` FOREIGN KEY (`ower`) REFERENCES `enYacht_Club_Tag` (`member_id`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Megkötések a táblához `travels`
--
ALTER TABLE `enTravel`
  ADD CONSTRAINT `enTravel_ibfk_1` FOREIGN KEY (`yacht_rent_id`) REFERENCES `enYacht_rent` (`rent_id`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `enTravel_ibfk_2` FOREIGN KEY (`port_id`) REFERENCES `enPort` (`port_id`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Megkötések a táblához `yachts`
--
ALTER TABLE `enYacht`
  ADD CONSTRAINT `enYacht_ibfk_1` FOREIGN KEY (`ower`) REFERENCES `enYacht_Club_Tag` (`member_id`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `enYacht_ibfk_2` FOREIGN KEY (`port_id`) REFERENCES `enPort` (`port_id`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Megkötések a táblához `enYacht_Club_Tag`
--
ALTER TABLE `enYacht_Club_Tag`
  ADD CONSTRAINT `enYacht_Club_Tag_ibfk_1` FOREIGN KEY (`zip_code`) REFERENCES `enZipcode` (`zip_code`),
  
  
  
ALTER TABLE `enMessage`
  ADD CONSTRAINT `enMessage_ibfk_1` FOREIGN KEY (`sender`) REFERENCES `enYacht_Club_Tag` (`member_id`),
  ADD CONSTRAINT `enMessage_ibfk_2` FOREIGN KEY (`addressee`) REFERENCES `enYacht_Club_Tag` (`member_id`),
  ADD CONSTRAINT `enMessage_ibfk_3` FOREIGN KEY (`yacht_id`) REFERENCES `enYacht` (`yacht_id`),
  ADD CONSTRAINT `enMessage_ibfk_4` FOREIGN KEY (`device_id`) REFERENCES `enDevice` (`device_id`);