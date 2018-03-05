-- phpMyAdmin SQL Dump
-- version 4.5.1
-- http://www.phpmyadmin.net
--
-- Gép: 127.0.0.1
-- Létrehozás ideje: 2018. Már 05. 18:19
-- Kiszolgáló verziója: 10.1.19-MariaDB
-- PHP verzió: 5.6.28
SET FOREIGN_KEY_CHECKS=0;

--
-- Adatbázis: `yacht_club`
--

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `loan_device`
--

DROP TABLE IF EXISTS `loan_device`;
CREATE TABLE IF NOT EXISTS `loan_device` (
  `loan_id` int(4) NOT NULL,
  `who` int(6) NOT NULL,
  `for_who` int(6) NOT NULL,
  `from_time` datetime NOT NULL,
  `finish_time` datetime NOT NULL,
  `device_id` int(4) NOT NULL,
  PRIMARY KEY (`loan_id`),
  KEY `who` (`who`),
  KEY `for_who` (`for_who`),
  KEY `device_id` (`device_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `loan_yacht`
--

DROP TABLE IF EXISTS `loan_yacht`;
CREATE TABLE IF NOT EXISTS `loan_yacht` (
  `loan_id` int(8) NOT NULL,
  `who` int(6) NOT NULL,
  `for_who` int(6) NOT NULL,
  `from_time` datetime NOT NULL,
  `finish_time` datetime NOT NULL,
  `yacht_id` int(4) NOT NULL,
  PRIMARY KEY (`loan_id`),
  KEY `who` (`who`),
  KEY `for_who` (`for_who`),
  KEY `yacht_id` (`yacht_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `transport_device`
--

DROP TABLE IF EXISTS `transport_device`;
CREATE TABLE IF NOT EXISTS `transport_device` (
  `device_id` int(4) NOT NULL,
  `ower` int(6) NOT NULL,
  `now_ower` int(6) NOT NULL,
  `size` int(4) NOT NULL,
  `busy` int(1) DEFAULT '0',
  PRIMARY KEY (`device_id`),
  KEY `ower` (`ower`),
  KEY `now_ower` (`now_ower`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `travels`
--

DROP TABLE IF EXISTS `travels`;
CREATE TABLE IF NOT EXISTS `travels` (
  `yacht_id` int(4) NOT NULL,
  `yacht_ower` int(6) NOT NULL,
  `where_old_time` datetime NOT NULL,
  `where_is` varchar(50) NOT NULL,
  `where_old` varchar(50) NOT NULL,
  PRIMARY KEY (`yacht_id`,`yacht_ower`,`where_old_time`),
  KEY `yacht_ower` (`yacht_ower`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `yachts`
--

DROP TABLE IF EXISTS `yachts`;
CREATE TABLE IF NOT EXISTS `yachts` (
  `yacht_id` int(4) NOT NULL,
  `name` varchar(30) NOT NULL,
  `image` int(2) DEFAULT NULL,
  `ower` int(6) NOT NULL,
  `now_ower` int(6) NOT NULL,
  `seats` int(4) DEFAULT NULL,
  `busy` int(1) DEFAULT '0',
  PRIMARY KEY (`yacht_id`),
  KEY `ower` (`ower`),
  KEY `now_ower` (`now_ower`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `yacht_club_tags`
--

DROP TABLE IF EXISTS `yacht_club_tags`;
CREATE TABLE IF NOT EXISTS `yacht_club_tags` (
  `tag_id` int(6) NOT NULL,
  `name` varchar(55) NOT NULL,
  `image` int(2) DEFAULT NULL,
  `zip_code` int(4) NOT NULL,
  `home_adress` varchar(50) NOT NULL,
  `birthday` datetime NOT NULL,
  PRIMARY KEY (`tag_id`),
  KEY `zip_code` (`zip_code`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `yacht_login`
--

DROP TABLE IF EXISTS `yacht_login`;
CREATE TABLE IF NOT EXISTS `yacht_login` (
  `login_id` int(10) NOT NULL,
  `club_tag_id` int(6) NOT NULL,
  `password` varchar(65) NOT NULL,
  `name` varchar(25) NOT NULL,
  `access_level` int(1) NOT NULL,
  `last_login` datetime NOT NULL,
  PRIMARY KEY (`login_id`),
  KEY `club_tag_id` (`club_tag_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `zip_codes`
--

DROP TABLE IF EXISTS `zip_codes`;
CREATE TABLE IF NOT EXISTS `zip_codes` (
  `zip_code` int(4) NOT NULL,
  `city` varchar(30) DEFAULT NULL,
  PRIMARY KEY (`zip_code`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Megkötések a kiírt táblákhoz
--

--
-- Megkötések a táblához `loan_device`
--
ALTER TABLE `loan_device`
  ADD CONSTRAINT `loan_device_ibfk_1` FOREIGN KEY (`who`) REFERENCES `yacht_club_tags` (`tag_id`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `loan_device_ibfk_2` FOREIGN KEY (`for_who`) REFERENCES `yacht_club_tags` (`tag_id`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `loan_device_ibfk_3` FOREIGN KEY (`device_id`) REFERENCES `transport_device` (`device_id`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Megkötések a táblához `loan_yacht`
--
ALTER TABLE `loan_yacht`
  ADD CONSTRAINT `loan_yacht_ibfk_1` FOREIGN KEY (`who`) REFERENCES `yacht_club_tags` (`tag_id`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `loan_yacht_ibfk_2` FOREIGN KEY (`for_who`) REFERENCES `yacht_club_tags` (`tag_id`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `loan_yacht_ibfk_3` FOREIGN KEY (`yacht_id`) REFERENCES `yachts` (`yacht_id`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Megkötések a táblához `transport_device`
--
ALTER TABLE `transport_device`
  ADD CONSTRAINT `transport_device_ibfk_1` FOREIGN KEY (`ower`) REFERENCES `yacht_club_tags` (`tag_id`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `transport_device_ibfk_2` FOREIGN KEY (`now_ower`) REFERENCES `yacht_club_tags` (`tag_id`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Megkötések a táblához `travels`
--
ALTER TABLE `travels`
  ADD CONSTRAINT `travels_ibfk_1` FOREIGN KEY (`yacht_ower`) REFERENCES `yacht_club_tags` (`tag_id`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `travels_ibfk_2` FOREIGN KEY (`yacht_id`) REFERENCES `yachts` (`yacht_id`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Megkötések a táblához `yachts`
--
ALTER TABLE `yachts`
  ADD CONSTRAINT `yachts_ibfk_1` FOREIGN KEY (`ower`) REFERENCES `yacht_club_tags` (`tag_id`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `yachts_ibfk_2` FOREIGN KEY (`now_ower`) REFERENCES `yacht_club_tags` (`tag_id`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Megkötések a táblához `yacht_club_tags`
--
ALTER TABLE `yacht_club_tags`
  ADD CONSTRAINT `yacht_club_tags_ibfk_2` FOREIGN KEY (`zip_code`) REFERENCES `zip_codes` (`zip_code`);

--
-- Megkötések a táblához `yacht_login`
--
ALTER TABLE `yacht_login`
  ADD CONSTRAINT `yacht_login_ibfk_1` FOREIGN KEY (`club_tag_id`) REFERENCES `yacht_club_tags` (`tag_id`) ON DELETE CASCADE ON UPDATE CASCADE;
