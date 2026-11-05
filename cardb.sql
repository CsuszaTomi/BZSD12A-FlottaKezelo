-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Gép: 127.0.0.1
-- Létrehozás ideje: 2026. Nov 01. 12:56
-- Kiszolgáló verziója: 10.4.32-MariaDB
-- PHP verzió: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Adatbázis: `cardb`
--
CREATE DATABASE IF NOT EXISTS `cardb` DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_hungarian_ci;
USE `cardb`;

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `car`
--

DROP TABLE IF EXISTS `car`;
CREATE TABLE `car` (
  `ID` int(16) NOT NULL,
  `PlateNumb` varchar(16) NOT NULL,
  `ChassNum` varchar(64) NOT NULL,
  `ModelId` int(11) NOT NULL,
  `UserID` int(16) NOT NULL,
  `CarYear` date NOT NULL,
  `Consumption` decimal(10,2) NOT NULL,
  `RanKM` varchar(255) NOT NULL,
  `FuelType` varchar(255) NOT NULL,
  `BrandId` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_hungarian_ci;

--
-- A tábla adatainak kiíratása `car`
--

INSERT INTO `car` (`ID`, `PlateNumb`, `ChassNum`, `ModelId`, `UserID`, `CarYear`, `Consumption`, `RanKM`, `FuelType`, `BrandId`) VALUES
(1, 'KLM-482', 'TMBJG7NE5J0123456', 8, 1, '2019-05-14', 5.4 , '142000', 'Dízel', 8),
(2, 'PQR-135', 'WVWZZZAUZHW234567', 2, 2, '2017-09-02', 5.9 , '168500', 'Benzin', 2),
(3, 'STU-907', 'SB1ZS3BE40E345678', 1, 3, '2021-03-20', 4.6, '76400', 'Hibrid', 1),
(4, 'NBA-264', 'W0VBD6EC5K1456789', 7, 4, '2016-11-08', 6.1, '189300', 'Benzin', 7),
(5, 'RJD-518', 'WF0EXXGCDEJ567890', 6, 5, '2018-06-25', 5.2, '154800', 'Dízel', 6),
(6, 'MTK-739', 'TSMNZC22S00678901', 9, 6, '2020-01-17', 5.0 l, '61200', 'Benzin', 9),
(7, 'BKS-346', 'WBA8E11050K789012', 3, 7, '2015-04-30', 5.7, '214600', 'Dízel', 3),
(8, 'GHF-871', 'TMAH3512ALJ890123', 13, 8, '2022-08-12', 6.3, '38700', 'Benzin', 13),
(9, 'CZT-092', 'U5YH2512AKL901234', 14, 9, '2019-10-05', 5.5, '97300', 'Benzin', 14),
(10, 'LPD-653', 'VF15R0A0H59012345', 11, 10, '2014-02-21', 4.9, '231000', 'Dízel', 11),
(11, 'UEA-417', 'WAUZZZF48JA123450', 4, 11, '2018-07-19', 5.8, '176200', 'Dízel', 4),
(12, 'FYN-285', 'VF3LBHNYHKS234501', 12, 12, '2020-12-03', 4.8, '88900', 'Dízel', 12),
(13, 'HDV-764', 'SHHFK7830KU345012', 10, 13, '2021-05-28', 6.0, '54100', 'Benzin', 10),
(14, 'WRX-309', 'JMZKF2W7A00456123', 15, 14, '2019-09-14', 7.2, '121700', 'Benzin', 15),
(15, 'SAB-156', 'TMBJF7NE8L0567234', 8, 1, '2023-02-09', 5.9, '29800', 'Benzin', 8),
(16, 'TNM-823', 'ZFA31200003678345', 17, 15, '2013-06-11', 5.6, '187400', 'Benzin', 17),
(17, 'KJH-540', 'SJNFAAJ11U2789456', 16, 16, '2017-03-27', 6.4, '149500', 'Benzin', 16),
(18, 'VCX-671', 'WVWZZZAUZLW890567', 2, 3, '2022-11-30', 4.7, '45300', 'Hibrid', 2),
(19, 'DMP-298', 'YV1UZA2MCL1901678', 19, 17, '2020-04-16', 7.1, '112600', 'Dízel', 19),
(20, 'EZR-935', 'WDD2050421R012789', 5, 18, '2016-08-22', 5.3, '203800', 'Dízel', 5);

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `damagereport`
--

DROP TABLE IF EXISTS `damagereport`;
CREATE TABLE `damagereport` (
  `ID` int(11) NOT NULL,
  `CarID` int(11) NOT NULL,
  `Description` text NOT NULL,
  `Date` date NOT NULL,
  `InsuranceStatus` tinyint(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_hungarian_ci;

--
-- A tábla adatainak kiíratása `damagereport`
--

INSERT INTO `damagereport` (`ID`, `CarID`, `Description`, `Date`, `InsuranceStatus`) VALUES
(1, 2, 'Hátsó lökhárító karcolás parkolás közben', '2026-03-14', 1),
(2, 7, 'Szélvédő kővel megrepedt', '2026-01-19', 1),
(3, 10, 'Bal oldali visszapillantó tükör letörött', '2025-08-23', 0);

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `maintenance`
--

DROP TABLE IF EXISTS `maintenance`;
CREATE TABLE `maintenance` (
  `ID` int(11) NOT NULL,
  `CarID` int(11) NOT NULL,
  `ServiceDate` date NOT NULL,
  `Description` text NOT NULL,
  `Cost` int(11) NOT NULL,
  `RanKm` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_hungarian_ci;

--
-- A tábla adatainak kiíratása `maintenance`
--

INSERT INTO `maintenance` (`ID`, `CarID`, `ServiceDate`, `Description`, `Cost`, `RanKm`) VALUES
(1, 1, '2026-02-20', 'Olajcsere, olaj- és levegőszűrő csere', 38000, 140500),
(2, 2, '2026-03-05', 'Első fékbetét csere', 42000, 167000),
(3, 3, '2026-04-12', 'Éves szerviz', 52000, 74800),
(4, 4, '2025-12-10', 'Vezérműszíj és vízpumpa csere', 145000, 187500),
(5, 5, '2026-01-25', 'Futómű javítás, lengéscsillapító csere', 96000, 153000),
(6, 7, '2026-06-02', 'Kuplung és ékszíj csere', 210000, 213000);

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `manufacturers`
--

DROP TABLE IF EXISTS `manufacturers`;
CREATE TABLE `manufacturers` (
  `ID` int(11) NOT NULL,
  `BrandName` varchar(32) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_hungarian_ci;

--
-- A tábla adatainak kiíratása `manufacturers`
--

INSERT INTO `manufacturers` (`ID`, `BrandName`) VALUES
(4, 'Audi'),
(3, 'BMW'),
(20, 'Citroen'),
(17, 'Fiat'),
(6, 'Ford'),
(10, 'Honda'),
(13, 'Hyundai'),
(14, 'Kia'),
(15, 'Mazda'),
(5, 'Mercedes-Benz'),
(16, 'Nissan'),
(7, 'Opel'),
(12, 'Peugeot'),
(11, 'Renault'),
(18, 'Seat'),
(8, 'Skoda'),
(9, 'Suzuki'),
(1, 'Toyota'),
(2, 'Volkswagen'),
(19, 'Volvo');

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `models`
--

DROP TABLE IF EXISTS `models`;
CREATE TABLE `models` (
  `ID` int(11) NOT NULL,
  `ModelName` varchar(32) NOT NULL,
  `ModelMaker` varchar(32) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_hungarian_ci;

--
-- A tábla adatainak kiíratása `models`
--

INSERT INTO `models` (`ID`, `ModelName`, `ModelMaker`) VALUES
(1, 'Corolla', 'Toyota'),
(2, 'Golf', 'Volkswagen'),
(3, '320d', 'BMW'),
(4, 'A4', 'Audi'),
(5, 'C 220', 'Mercedes-Benz'),
(6, 'Focus', 'Ford'),
(7, 'Astra', 'Opel'),
(8, 'Octavia', 'Skoda'),
(9, 'Swift', 'Suzuki'),
(10, 'Civic', 'Honda'),
(11, 'Clio', 'Renault'),
(12, '308', 'Peugeot'),
(13, 'i30', 'Hyundai'),
(14, 'Ceed', 'Kia'),
(15, 'CX-5', 'Mazda'),
(16, 'Qashqai', 'Nissan'),
(17, 'Panda', 'Fiat'),
(18, 'Leon', 'Seat'),
(19, 'XC60', 'Volvo'),
(20, 'C3', 'Citroen');

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `refueling`
--

DROP TABLE IF EXISTS `refueling`;
CREATE TABLE `refueling` (
  `ID` int(11) NOT NULL,
  `CarID` int(11) NOT NULL,
  `Date` datetime NOT NULL,
  `Liters` decimal(10,2) NOT NULL,
  `TotalCost` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_hungarian_ci;

--
-- A tábla adatainak kiíratása `refueling`
--

INSERT INTO `refueling` (`ID`, `CarID`, `Date`, `Liters`, `TotalCost`) VALUES
(1, 1, '2026-09-03 07:45:00', 44.62, 28557),
(2, 2, '2026-09-05 17:20:00', 39.85, 24707),
(3, 3, '2026-09-08 12:10:00', 34.71, 21520),
(4, 4, '2026-09-10 18:05:00', 37.48, 23238),
(5, 5, '2026-09-12 08:30:00', 41.93, 26835),
(6, 6, '2026-09-14 16:40:00', 32.56, 20187),
(7, 7, '2026-09-17 06:55:00', 49.37, 31597),
(8, 8, '2026-09-19 13:15:00', 35.82, 22208),
(9, 9, '2026-09-21 19:00:00', 40.64, 25197),
(10, 10, '2026-09-23 07:10:00', 38.76, 24806),
(11, 11, '2026-09-25 11:35:00', 47.21, 30214),
(12, 12, '2026-09-27 15:50:00', 43.58, 27891),
(13, 13, '2026-09-29 09:25:00', 36.94, 22903),
(14, 14, '2026-10-01 18:45:00', 51.43, 31887),
(15, 15, '2026-10-04 10:00:00', 39.27, 24347),
(16, 16, '2026-10-06 14:20:00', 29.68, 18402),
(17, 17, '2026-10-09 08:05:00', 45.35, 28117),
(18, 18, '2026-10-12 17:30:00', 31.89, 19772),
(19, 19, '2026-10-15 12:45:00', 54.12, 34637),
(20, 20, '2026-10-18 07:20:00', 46.74, 29914);

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `user`
--

DROP TABLE IF EXISTS `user`;
CREATE TABLE `user` (
  `ID` int(64) NOT NULL,
  `Name` varchar(32) NOT NULL,
  `Age` int(32) NOT NULL,
  `Role` varchar(64) NOT NULL,
  `IdentityCardNum` varchar(64) NOT NULL,
  `Password` varchar(64) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_hungarian_ci;

--
-- A tábla adatainak kiíratása `user`
--

INSERT INTO `user` (`ID`, `Name`, `Age`, `Role`, `IdentityCardNum`, `Password`) VALUES
(1, 'Kovács Péter', 34, 'user', '218745AB', 'Piros#Kocsi41'),
(2, 'Nagy Anna', 28, 'user', '305912CD', 'Kek!Hold518'),
(3, 'Szabó Gábor', 45, 'user', '447120EF', 'Mokus$Fa2041'),
(4, 'Tóth Katalin', 39, 'user', '129384GH', 'Tigris#Ut902'),
(5, 'Horváth Balázs', 31, 'user', '561903IJ', 'Szel@Malom63'),
(6, 'Varga Eszter', 26, 'user', '782014KL', 'Alma!Torta77'),
(7, 'Kiss László', 52, 'user', '893165MN', 'Vihar#Part305'),
(8, 'Molnár Réka', 29, 'user', '264057OP', 'Ezust$Hid184'),
(9, 'Németh Dávid', 23, 'user', '375428QR', 'Medve!Domb92'),
(10, 'Farkas Zsófia', 36, 'user', '486239ST', 'Csillag#Ut26'),
(11, 'Balogh Márk', 41, 'user', '597340UV', 'Folyo@Kavics8'),
(12, 'Papp Nóra', 33, 'user', '608451WX', 'Hegyi$Szel57'),
(13, 'Takács Ádám', 27, 'user', '719562YZ', 'Narancs!Fa430'),
(14, 'Juhász Orsolya', 48, 'user', '820673AC', 'Tuzes#Mad116'),
(15, 'Mészáros Tamás', 38, 'user', '931784BD', 'Havas$Csucs9'),
(16, 'Simon Bence', 24, 'user', '142895CE', 'Bator!Roka73'),
(17, 'Fehér Viktória', 30, 'user', '253906DF', 'Fenyo#Erdo25'),
(18, 'Rácz Gergő', 35, 'user', '364017EG', 'Zold@Tavasz6'),
(19, 'Szűcs Márta', 44, 'admin', '475128FH', 'Vedett!Admin26'),
(20, 'Oláh Csaba', 37, 'admin', '586239GJ', 'Szerver$Kulcs88');

--
-- Indexek a kiírt táblákhoz
--

--
-- A tábla indexei `car`
--
ALTER TABLE `car`
  ADD PRIMARY KEY (`ID`),
  ADD KEY `UserID` (`UserID`),
  ADD KEY `ModelId` (`ModelId`,`BrandId`),
  ADD KEY `BrandId` (`BrandId`);

--
-- A tábla indexei `damagereport`
--
ALTER TABLE `damagereport`
  ADD PRIMARY KEY (`ID`),
  ADD KEY `CarID` (`CarID`);

--
-- A tábla indexei `maintenance`
--
ALTER TABLE `maintenance`
  ADD PRIMARY KEY (`ID`),
  ADD KEY `CarID` (`CarID`);

--
-- A tábla indexei `manufacturers`
--
ALTER TABLE `manufacturers`
  ADD PRIMARY KEY (`ID`),
  ADD UNIQUE KEY `BrandName` (`BrandName`);

--
-- A tábla indexei `models`
--
ALTER TABLE `models`
  ADD PRIMARY KEY (`ID`),
  ADD KEY `ModelName` (`ModelMaker`) USING BTREE;

--
-- A tábla indexei `refueling`
--
ALTER TABLE `refueling`
  ADD PRIMARY KEY (`ID`),
  ADD KEY `CarID` (`CarID`);

--
-- A tábla indexei `user`
--
ALTER TABLE `user`
  ADD PRIMARY KEY (`ID`);

--
-- A kiírt táblák AUTO_INCREMENT értéke
--

--
-- AUTO_INCREMENT a táblához `car`
--
ALTER TABLE `car`
  MODIFY `ID` int(16) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=21;

--
-- AUTO_INCREMENT a táblához `damagereport`
--
ALTER TABLE `damagereport`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT a táblához `maintenance`
--
ALTER TABLE `maintenance`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;

--
-- AUTO_INCREMENT a táblához `manufacturers`
--
ALTER TABLE `manufacturers`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=21;

--
-- AUTO_INCREMENT a táblához `models`
--
ALTER TABLE `models`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=21;

--
-- AUTO_INCREMENT a táblához `refueling`
--
ALTER TABLE `refueling`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=21;

--
-- AUTO_INCREMENT a táblához `user`
--
ALTER TABLE `user`
  MODIFY `ID` int(64) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=21;

--
-- Megkötések a kiírt táblákhoz
--

--
-- Megkötések a táblához `car`
--
ALTER TABLE `car`
  ADD CONSTRAINT `car_ibfk_1` FOREIGN KEY (`UserID`) REFERENCES `user` (`ID`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `car_ibfk_2` FOREIGN KEY (`ModelId`) REFERENCES `models` (`ID`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `car_ibfk_3` FOREIGN KEY (`BrandId`) REFERENCES `manufacturers` (`ID`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Megkötések a táblához `damagereport`
--
ALTER TABLE `damagereport`
  ADD CONSTRAINT `damagereport_ibfk_1` FOREIGN KEY (`CarID`) REFERENCES `car` (`ID`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Megkötések a táblához `maintenance`
--
ALTER TABLE `maintenance`
  ADD CONSTRAINT `maintenance_ibfk_1` FOREIGN KEY (`CarID`) REFERENCES `car` (`ID`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Megkötések a táblához `models`
--
ALTER TABLE `models`
  ADD CONSTRAINT `models_ibfk_1` FOREIGN KEY (`ModelMaker`) REFERENCES `manufacturers` (`BrandName`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Megkötések a táblához `refueling`
--
ALTER TABLE `refueling`
  ADD CONSTRAINT `refueling_ibfk_1` FOREIGN KEY (`CarID`) REFERENCES `car` (`ID`) ON DELETE CASCADE ON UPDATE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
