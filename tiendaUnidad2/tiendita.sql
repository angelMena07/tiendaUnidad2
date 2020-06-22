-- phpMyAdmin SQL Dump
-- version 4.8.5
-- https://www.phpmyadmin.net/
--
-- Servidor: 127.0.0.1:3306
-- Tiempo de generación: 22-06-2020 a las 05:38:26
-- Versión del servidor: 5.7.26
-- Versión de PHP: 7.2.18

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de datos: `tiendita`
--

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `detalles`
--

DROP TABLE IF EXISTS `detalles`;
CREATE TABLE IF NOT EXISTS `detalles` (
  `Id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT,
  `ProductoId` int(10) UNSIGNED NOT NULL,
  `VentaId` int(10) UNSIGNED NOT NULL,
  `Subtotal` decimal(65,30) NOT NULL,
  `DetalleId` int(10) UNSIGNED DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_Detalles_DetalleId` (`DetalleId`),
  KEY `IX_Detalles_ProductoId` (`ProductoId`),
  KEY `IX_Detalles_VentaId` (`VentaId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `productos`
--

DROP TABLE IF EXISTS `productos`;
CREATE TABLE IF NOT EXISTS `productos` (
  `Id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT,
  `Nombre` longtext CHARACTER SET utf8mb4,
  `Descripcion` longtext CHARACTER SET utf8mb4,
  `Tamano` longtext CHARACTER SET utf8mb4,
  `Costo` decimal(65,30) NOT NULL,
  `Precio` decimal(65,30) NOT NULL,
  `Cantidad` decimal(65,30) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Volcado de datos para la tabla `productos`
--

INSERT INTO `productos` (`Id`, `Nombre`, `Descripcion`, `Tamano`, `Costo`, `Precio`, `Cantidad`) VALUES
(1, 'Tapabocas', 'Pal covid', 'Normal', '50.000000000000000000000000000000', '100.000000000000000000000000000000', '150.000000000000000000000000000000'),
(2, 'cereal', 'cereal', 'chico', '34.000000000000000000000000000000', '45.000000000000000000000000000000', '3.000000000000000000000000000000');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `usuarios`
--

DROP TABLE IF EXISTS `usuarios`;
CREATE TABLE IF NOT EXISTS `usuarios` (
  `idusuario` int(11) NOT NULL AUTO_INCREMENT,
  `username` varchar(45) COLLATE utf8mb4_unicode_ci NOT NULL,
  `password` varchar(80) COLLATE utf8mb4_unicode_ci NOT NULL,
  `tipo_usuario` varchar(50) COLLATE utf8mb4_unicode_ci NOT NULL,
  PRIMARY KEY (`idusuario`)
) ENGINE=InnoDB AUTO_INCREMENT=16 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Volcado de datos para la tabla `usuarios`
--

INSERT INTO `usuarios` (`idusuario`, `username`, `password`, `tipo_usuario`) VALUES
(9, 'admin', 'f865b53623', 'admin'),
(14, 'david', 'MQAyADMANAA=', 'admin'),
(15, 'angel', 'NAAzADIAMQA=', 'user');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `ventas`
--

DROP TABLE IF EXISTS `ventas`;
CREATE TABLE IF NOT EXISTS `ventas` (
  `Id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT,
  `Total` decimal(65,30) NOT NULL,
  `fecha` datetime(6) NOT NULL,
  `Cliente` longtext CHARACTER SET utf8mb4,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Volcado de datos para la tabla `ventas`
--

INSERT INTO `ventas` (`Id`, `Total`, `fecha`, `Cliente`) VALUES
(1, '500.000000000000000000000000000000', '2020-06-21 00:00:00.000000', 'pablo');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `__efmigrationshistory`
--

DROP TABLE IF EXISTS `__efmigrationshistory`;
CREATE TABLE IF NOT EXISTS `__efmigrationshistory` (
  `MigrationId` varchar(95) COLLATE utf8mb4_unicode_ci NOT NULL,
  `ProductVersion` varchar(32) COLLATE utf8mb4_unicode_ci NOT NULL,
  PRIMARY KEY (`MigrationId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Volcado de datos para la tabla `__efmigrationshistory`
--

INSERT INTO `__efmigrationshistory` (`MigrationId`, `ProductVersion`) VALUES
('20200620060238_Initial', '3.1.5');

--
-- Restricciones para tablas volcadas
--

--
-- Filtros para la tabla `detalles`
--
ALTER TABLE `detalles`
  ADD CONSTRAINT `FK_Detalles_Detalles_DetalleId` FOREIGN KEY (`DetalleId`) REFERENCES `detalles` (`Id`),
  ADD CONSTRAINT `FK_Detalles_Productos_ProductoId` FOREIGN KEY (`ProductoId`) REFERENCES `productos` (`Id`) ON DELETE CASCADE,
  ADD CONSTRAINT `FK_Detalles_Ventas_VentaId` FOREIGN KEY (`VentaId`) REFERENCES `ventas` (`Id`) ON DELETE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
