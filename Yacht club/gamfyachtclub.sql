

--
-- Adatbázis: `gamfyachtclub`
--
CREATE DATABASE IF NOT EXISTS `gamfyachtclub` DEFAULT CHARACTER SET latin1 COLLATE latin1_swedish_ci;
USE `gamfyachtclub`;

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `endevice`
--

DROP TABLE IF EXISTS `endevice`;
CREATE TABLE IF NOT EXISTS `endevice` (
  `device_id` int(4) NOT NULL,
  `type` varchar(20) NOT NULL,
  `hire` int(1) NOT NULL DEFAULT '1',
  `busy` int(1) NOT NULL DEFAULT '0',
  `daly_price` int(20) DEFAULT NULL,
  `max_lenght` varchar(3) NOT NULL,
  `max_weight` int(3) NOT NULL,
  `ower` int(4) NOT NULL,
  `renter` int(4) DEFAULT NULL,
  `hide` int(1) DEFAULT '0'
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- TÁBLA KAPCSOLATAI `endevice`:
--   `ower`
--       `enyacht_club_tag` -> `member_id`
--   `renter`
--       `enyacht_club_tag` -> `member_id`
--

--
-- A tábla adatainak kiíratása `endevice`
--

INSERT INTO `endevice` (`device_id`, `type`, `hire`, `busy`, `daly_price`, `max_lenght`, `max_weight`, `ower`, `renter`, `hide`) VALUES
(1, 'VCHJ 150', 1, 0, NULL, '0', 15, 1, NULL, 0),
(2, 'JHB 10', 1, 0, NULL, '0', 15, 1, NULL, 0),
(3, 'BNMV 123', 1, 1, NULL, '0', 20, 1, 5, 0),
(4, 'BHJF 25', 1, 0, NULL, '0', 25, 1, NULL, 0),
(5, 'BHJF 25', 1, 0, NULL, '0', 25, 5, NULL, 0),
(6, 'BHJF 25', 1, 0, NULL, '0', 25, 2, NULL, 0),
(7, 'BHJF 25', 1, 0, NULL, '0', 25, 4, NULL, 0),
(8, 'BHJF 25', 1, 0, NULL, '0', 25, 3, NULL, 0),
(9, 'NGKJ 120', 1, 0, NULL, '0', 12, 0, NULL, 0),
(10, 'MNV 12', 1, 0, NULL, '0', 120, 0, NULL, 0),
(11, 'BVS 30', 1, 1, NULL, '0', 30, 0, 3, 0),
(12, 'DDFR 30', 1, 0, NULL, '0', 15, 0, NULL, 0),
(13, 'BVFS 85', 1, 1, NULL, '0', 30, 5, 1, 0),
(14, 'BVFS 85', 1, 0, NULL, '0', 30, 4, NULL, 0),
(15, 'BVFS 85', 1, 0, NULL, '0', 30, 2, NULL, 0),
(16, 'BVFS 85', 1, 0, NULL, '0', 30, 3, NULL, 0);

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `enlogin`
--

DROP TABLE IF EXISTS `enlogin`;
CREATE TABLE IF NOT EXISTS `enlogin` (
  `login_id` int(4) NOT NULL,
  `login_name` varchar(25) NOT NULL,
  `password` varchar(500) NOT NULL,
  `email` varchar(30) NOT NULL,
  `theme` int(1) NOT NULL DEFAULT '1',
  `admin` int(1) NOT NULL,
  `last_login` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- TÁBLA KAPCSOLATAI `enlogin`:
--

--
-- A tábla adatainak kiíratása `enlogin`
--

INSERT INTO `enlogin` (`login_id`, `login_name`, `password`, `email`, `theme`, `admin`, `last_login`) VALUES
(0, 'admin', 'MxDbc2/naanT/2TMBka31I3E0kg91GVVwayjqDNeP5oXB5d3C5ydc0A2PdBkNP445HGeXaGKa2Po/POPqfxl1ZDbpd8+eL3zj+7lelQAnM+xZQHxywERAxVThD3gEM51qc1NmiLapq03zoO9txMXjUBCc+Eit6UcgmmV4jsnYlM=', 'YachtClub@Infok.hu', 1, 1, '2018-04-10 20:58:07'),
(1, 'jacjozs', 'zd7WLm0bocg/yC9T2YURaSpinxsgU33jd/V2OOFpFdSFJQCw55J1inc+eBtHYr74O4EjVUWlqBC7tprfwA5lYdlMBqYs1judsaEnBrDgsLTGWzdhAzm7Xmlfy9LB6YCdhfwVUVPD+B1warXJ5aBvjMbsexrRrksjcfmIiPCNNV4=', 'jacjozs@gmail.com', 1, 0, '2018-04-11 22:24:51'),
(2, 'andris', 'cm0M7aTCoNYRsPhzJxFX8MLYbnRXppdbaJYJPmN/OHSyRmAoeXOzDCt9ARJp/sOOIkrB6VuENZ08mp7O0FcEku3lmFcFjMiL+U/4PNqO6G3nZhXyKVBpzydKrXmBW6WZJlpvsobCkOtRbYJZ5ik5yMpE/wmNwDIk6xOz9rqLkqk=', 'andris@gmail.com', 1, 0, '2018-04-15 20:52:47'),
(3, 'zoli', 'zHuTu6TbbDzdgQZbrbZMv+8Rn5B0eUEg1FNeTBfC2RDeZULmldv+FfiaxVJ1CK4cm5RnfufNCSgWgCAG3eRLaIengHZcwqjuYm1dYHgo2fd7vGNRQ9SDkY7ayqO/r5TMhEmIM7UuBwxvENJYh0YfUQPTyz+UicP9HgAfk2+iOBo=', 'Zoli@gmail.com', 1, 0, '2018-04-15 20:54:00'),
(4, 'benji', 'eQUcHglQ2k4Rq6LiMJQXX+IiDqAxSYYqSKDh2D8RLNSSpaZu+rC4lKMd3OUVlh36p/rvwdthwDZMHw5aW/bVYYnoiscisVJugCNlLEOlJvxQyEvLBiQKBOFPhfkekHy9eODyJPL4IqS2tkrxRFcJ24Dhzc4rBilnuZRSIOZ+5pU=', 'benji@gmail.com', 1, 0, '2018-04-15 20:54:40'),
(5, 'szilvia', 'Ec7xQ7gpBsvGTFseAMA8UmMv0uhsO9SJ8bCgS50RAjM2ZUSEjKIM4gvMyatzSQmo839fcyyZsoE93e5V472cJW8LH6j+8kV42wcwDcp93bBtN2G0KzPE0vktOWQBQimhEiSEItl0ovQ0N7y8oCxE/jDZ7VUAnJzY1Y3Zh3d5o+s=', 'szilvia@gmail.com', 1, 0, '2018-04-15 20:55:31');

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `enmessage`
--

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
  `from_port` int(4) DEFAULT NULL,
  `to_port` int(4) DEFAULT NULL,
  `accept` int(1) DEFAULT NULL,
  `new` int(1) NOT NULL DEFAULT '1',
  `back` int(1) DEFAULT '0'
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- TÁBLA KAPCSOLATAI `enmessage`:
--   `sender`
--       `enyacht_club_tag` -> `member_id`
--   `addressee`
--       `enyacht_club_tag` -> `member_id`
--   `yacht_id`
--       `enyacht` -> `yacht_id`
--   `device_id`
--       `endevice` -> `device_id`
--   `from_port`
--       `enport` -> `port_id`
--   `to_port`
--       `enport` -> `port_id`
--

--
-- A tábla adatainak kiíratása `enmessage`
--

INSERT INTO `enmessage` (`message_id`, `date`, `sender`, `addressee`, `yacht_id`, `device_id`, `start_date`, `end_date`, `from_port`, `to_port`, `accept`, `new`, `back`) VALUES
(1, '2018-04-15 22:21:16', 1, 5, NULL, 13, '2018-04-15 00:00:00', '2018-04-15 00:00:00', NULL, NULL, 1, 0, 0),
(2, '2018-04-15 22:34:21', 5, 1, 2, NULL, '2018-04-15 00:00:00', '2018-04-15 00:00:00', 5, 13, 0, 0, 1),
(3, '2018-04-15 22:34:35', 5, 1, NULL, 3, '2018-04-15 00:00:00', '2018-04-15 00:00:00', NULL, NULL, 1, 0, 0),
(4, '2018-04-15 22:36:05', 2, 4, NULL, 7, '2018-04-15 00:00:00', '2018-04-15 00:00:00', NULL, NULL, NULL, 1, 0),
(5, '2018-04-15 22:36:23', 2, 0, 18, NULL, '2018-04-15 00:00:00', '2018-05-01 00:00:00', 5, 2, 1, 0, 1),
(6, '2018-04-15 22:37:17', 4, 3, 9, NULL, '2018-04-15 00:00:00', '2018-05-01 00:00:00', 2, 12, 0, 0, 1),
(7, '2018-04-15 22:37:41', 4, 5, NULL, 5, '2018-04-15 00:00:00', '2018-04-15 00:00:00', NULL, NULL, NULL, 1, 0),
(8, '2018-04-15 22:38:08', 3, 5, 5, NULL, '2018-04-15 00:00:00', '2018-04-15 00:00:00', 8, 3, NULL, 1, 0),
(9, '2018-04-15 22:38:14', 3, 0, NULL, 11, '2018-04-15 00:00:00', '2018-04-15 00:00:00', NULL, NULL, NULL, 1, 0);

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `enport`
--

DROP TABLE IF EXISTS `enport`;
CREATE TABLE IF NOT EXISTS `enport` (
  `port_id` int(4) NOT NULL,
  `name` varchar(30) NOT NULL,
  `country` varchar(70) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- TÁBLA KAPCSOLATAI `enport`:
--

--
-- A tábla adatainak kiíratása `enport`
--

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

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `enyacht`
--

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
  `hide` int(1) DEFAULT '0'
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- TÁBLA KAPCSOLATAI `enyacht`:
--   `ower`
--       `enyacht_club_tag` -> `member_id`
--   `port_id`
--       `enport` -> `port_id`
--   `renter`
--       `enyacht_club_tag` -> `member_id`
--

--
-- A tábla adatainak kiíratása `enyacht`
--

INSERT INTO `enyacht` (`yacht_id`, `name`, `producer`, `image`, `ower`, `renter`, `seats`, `hire`, `busy`, `daly_price`, `width`, `lenght`, `dive`, `speed`, `port_id`, `hide`) VALUES
(1, 'ASHA', 'Maiora', NULL, 1, NULL, 12, 0, 0, 100000, 6.3, 27.4, 1.7, 23, 0, 0),
(2, 'AA ABSOLUTE', 'Heesen', NULL, 1, NULL, 14, 1, 0, NULL, 7.15, 36.75, 1.6, 33, 0, 0),
(3, 'AB 116', 'AB Yachts', NULL, 1, NULL, 16, 1, 0, NULL, 7.5, 36.25, 1.6, 38, 0, 0),
(4, 'AFRICA DREAM', 'Arno', NULL, 5, NULL, 8, 1, 0, NULL, 5.35, 23, 1.6, 25, 0, 0),
(5, 'AZIMUT 46', 'Azimut', NULL, 5, NULL, 5, 1, 0, NULL, 4.42, 14.93, 1, 30, 0, 0),
(6, 'BLUE CHIP', 'Canados', NULL, 2, NULL, 11, 1, 0, NULL, 6.45, 26.48, 1.8, 24, 0, 1),
(7, 'BLUE SHADOW C', ' CN Apaunia / Lurssen', NULL, 2, NULL, 24, 1, 0, NULL, 8.35, 50.5, 2.7, 12, 0, 0),
(8, 'CARRARA', 'Amer', NULL, 2, NULL, 12, 1, 0, NULL, 6.1, 26.21, 1.95, 22, 0, 0),
(9, 'CHAMPNEYS', 'Sunseeker', NULL, 3, NULL, 12, 1, 0, NULL, 6, 25, 1.38, 26, 0, 0),
(10, 'CLARITY', 'Leight Notika', NULL, 3, NULL, 13, 1, 0, NULL, 7.4, 31.6, 1.9, 20, 0, 0),
(11, 'COCTAILS', 'Harqrave', NULL, 3, NULL, 12, 1, 0, NULL, 6.4, 30.48, 1.98, 19, 0, 0),
(12, 'DAKOTA', 'Codecasa', NULL, 3, NULL, 23, 1, 0, NULL, 9.5, 49.9, 3.2, 17, 0, 0),
(13, 'EOL-B', ' Arno Leopard', NULL, 4, NULL, 8, 1, 0, NULL, 7.35, 34, 2, 34, 0, 0),
(14, 'FAMOUS', 'Falcon', NULL, 4, NULL, 11, 1, 0, NULL, 5.75, 24.8, 1.65, 22, 0, 0),
(15, 'FELICITA WEST', ' Perini Navi', NULL, 4, NULL, 21, 1, 0, NULL, 12.7, 64, 3.8, 12, 0, 0),
(16, 'FREE-SPIRIT', ' Azimut', NULL, 0, NULL, 8, 1, 0, NULL, 6.6, 30.4, 1.65, 15, 0, 1),
(17, 'G-FORCE', 'Heesen', NULL, 0, NULL, 13, 1, 0, NULL, 7.5, 37.3, 2.2, 12, 0, 0),
(18, 'GLADIUS', 'Cantieri di Pisa', NULL, 0, 2, 17, 1, 1, NULL, 7.5, 38.7, 1.85, 21, 0, 0);

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `enyacht_club_tag`
--

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
  `birthday` datetime NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- TÁBLA KAPCSOLATAI `enyacht_club_tag`:
--   `zip_code`
--       `enzipcode` -> `zip_code`
--   `login_id`
--       `enlogin` -> `login_id`
--

--
-- A tábla adatainak kiíratása `enyacht_club_tag`
--

INSERT INTO `enyacht_club_tag` (`member_id`, `login_id`, `nickname`, `first_name`, `last_name`, `image`, `zip_code`, `address`, `country`, `birthday`) VALUES
(0, 0, 'Admin', 'Yacht Club Zrt', '', NULL, 6000, 'Valahol', 'Magyarorszag', '2018-04-01 00:00:00'),
(1, 1, 'Hikaru', 'Jaczina', 'József', NULL, 6000, 'Valahol', 'Magyarország', '1997-04-29 00:00:00'),
(2, 2, 'Andris', 'Makkos', 'András', NULL, 6000, 'Valahol', 'Magyarország', '1990-01-01 00:00:00'),
(3, 3, 'Zoli', 'Kugelman', 'Zoltán', NULL, 6000, 'Valahol', 'Magyarország', '1990-01-01 00:00:00'),
(4, 4, 'Benji', 'Kiss', 'Benji', NULL, 6000, 'Valahol', 'Magyarország', '1990-01-01 00:00:00'),
(5, 5, 'Szilvia', 'Halczman', 'Szilvia', NULL, 6000, 'Valahol', 'Magyarország', '1990-01-01 00:00:00');

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `enzipcode`
--

DROP TABLE IF EXISTS `enzipcode`;
CREATE TABLE IF NOT EXISTS `enzipcode` (
  `zip_code` int(4) NOT NULL,
  `city` varchar(30) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- TÁBLA KAPCSOLATAI `enzipcode`:
--

--
-- A tábla adatainak kiíratása `enzipcode`
--

INSERT INTO `enzipcode` (`zip_code`, `city`) VALUES
(2067, 'Szárliget'),
(2087, 'Pilisszentiván'),
(3467, 'Ároktő'),
(3580, 'Tiszaújváros'),
(3896, 'Telkibánya'),
(4361, 'Nyírbogát'),
(4440, 'Tiszavasvári'),
(4474, 'Tiszabercel'),
(5052, 'Újszász'),
(6000, 'Kecskemét'),
(6041, 'Kerekegyháza'),
(6087, 'Dunavecse'),
(6333, 'Dunaszentbenedek'),
(6726, 'Szeged'),
(10000, 'Zágráb'),
(10381, 'Bedenica'),
(34138, 'Trieszt ');

--
-- Indexek a kiírt táblákhoz
--

--
-- A tábla indexei `endevice`
--
ALTER TABLE `endevice`
  ADD PRIMARY KEY (`device_id`),
  ADD UNIQUE KEY `render` (`renter`),
  ADD KEY `ower` (`ower`);

--
-- A tábla indexei `enlogin`
--
ALTER TABLE `enlogin`
  ADD PRIMARY KEY (`login_id`);

--
-- A tábla indexei `enmessage`
--
ALTER TABLE `enmessage`
  ADD PRIMARY KEY (`message_id`),
  ADD KEY `sender` (`sender`),
  ADD KEY `addressee` (`addressee`),
  ADD KEY `yacht_id` (`yacht_id`),
  ADD KEY `device_id` (`device_id`),
  ADD KEY `from_port` (`from_port`),
  ADD KEY `to_port` (`to_port`);

--
-- A tábla indexei `enport`
--
ALTER TABLE `enport`
  ADD PRIMARY KEY (`port_id`);

--
-- A tábla indexei `enyacht`
--
ALTER TABLE `enyacht`
  ADD PRIMARY KEY (`yacht_id`),
  ADD UNIQUE KEY `renter` (`renter`),
  ADD KEY `ower` (`ower`),
  ADD KEY `port_id` (`port_id`);

--
-- A tábla indexei `enyacht_club_tag`
--
ALTER TABLE `enyacht_club_tag`
  ADD PRIMARY KEY (`member_id`),
  ADD KEY `zip_code` (`zip_code`),
  ADD KEY `login_id` (`login_id`);

--
-- A tábla indexei `enzipcode`
--
ALTER TABLE `enzipcode`
  ADD PRIMARY KEY (`zip_code`);

--
-- Megkötések a kiírt táblákhoz
--

--
-- Megkötések a táblához `endevice`
--
ALTER TABLE `endevice`
  ADD CONSTRAINT `enDevice_ibfk_1` FOREIGN KEY (`ower`) REFERENCES `enyacht_club_tag` (`member_id`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `enDevice_ibfk_2` FOREIGN KEY (`renter`) REFERENCES `enyacht_club_tag` (`member_id`) ON DELETE SET NULL ON UPDATE CASCADE;

--
-- Megkötések a táblához `enmessage`
--
ALTER TABLE `enmessage`
  ADD CONSTRAINT `enMessage_ibfk_1` FOREIGN KEY (`sender`) REFERENCES `enyacht_club_tag` (`member_id`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `enMessage_ibfk_2` FOREIGN KEY (`addressee`) REFERENCES `enyacht_club_tag` (`member_id`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `enMessage_ibfk_3` FOREIGN KEY (`yacht_id`) REFERENCES `enyacht` (`yacht_id`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `enMessage_ibfk_4` FOREIGN KEY (`device_id`) REFERENCES `endevice` (`device_id`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `enMessage_ibfk_5` FOREIGN KEY (`from_port`) REFERENCES `enport` (`port_id`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `enMessage_ibfk_6` FOREIGN KEY (`to_port`) REFERENCES `enport` (`port_id`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Megkötések a táblához `enyacht`
--
ALTER TABLE `enyacht`
  ADD CONSTRAINT `enYacht_ibfk_1` FOREIGN KEY (`ower`) REFERENCES `enyacht_club_tag` (`member_id`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `enYacht_ibfk_2` FOREIGN KEY (`port_id`) REFERENCES `enport` (`port_id`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `enYacht_ibfk_3` FOREIGN KEY (`renter`) REFERENCES `enyacht_club_tag` (`member_id`) ON DELETE SET NULL ON UPDATE CASCADE;

--
-- Megkötések a táblához `enyacht_club_tag`
--
ALTER TABLE `enyacht_club_tag`
  ADD CONSTRAINT `enYacht_Club_Tag_ibfk_1` FOREIGN KEY (`zip_code`) REFERENCES `enzipcode` (`zip_code`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `enYacht_Club_Tag_ibfk_2` FOREIGN KEY (`login_id`) REFERENCES `enlogin` (`login_id`) ON DELETE CASCADE ON UPDATE CASCADE;
