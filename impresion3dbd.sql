-- --------------------------------------------------------
-- Host:                         127.0.0.1
-- Versión del servidor:         9.5.0 - MySQL Community Server - GPL
-- SO del servidor:              Win64
-- HeidiSQL Versión:             12.16.0.7229
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;


-- Volcando estructura de base de datos para db_impresiones3d
CREATE DATABASE IF NOT EXISTS `db_impresiones3d` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `db_impresiones3d`;

-- Volcando estructura para tabla db_impresiones3d.clientes
CREATE TABLE IF NOT EXISTS `clientes` (
  `id_cliente` int NOT NULL AUTO_INCREMENT,
  `id_persona` int NOT NULL,
  PRIMARY KEY (`id_cliente`),
  UNIQUE KEY `id_persona` (`id_persona`),
  CONSTRAINT `fk_clientes_personas` FOREIGN KEY (`id_persona`) REFERENCES `personas` (`id_persona`) ON DELETE RESTRICT ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Volcando datos para la tabla db_impresiones3d.clientes: ~0 rows (aproximadamente)

-- Volcando estructura para tabla db_impresiones3d.consumo_material
CREATE TABLE IF NOT EXISTS `consumo_material` (
  `id_consumo_material` int NOT NULL AUTO_INCREMENT,
  `id_trabajo` int NOT NULL,
  `id_material` int NOT NULL,
  `cantidad` decimal(5,2) NOT NULL,
  PRIMARY KEY (`id_consumo_material`),
  KEY `fk_consumo_trabajo` (`id_trabajo`),
  KEY `fk_consumo_material` (`id_material`),
  CONSTRAINT `fk_consumo_material` FOREIGN KEY (`id_material`) REFERENCES `materiales` (`id_material`) ON DELETE RESTRICT ON UPDATE CASCADE,
  CONSTRAINT `fk_consumo_trabajo` FOREIGN KEY (`id_trabajo`) REFERENCES `trabajo_impresion` (`id_trabajo_impresion`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Volcando datos para la tabla db_impresiones3d.consumo_material: ~0 rows (aproximadamente)

-- Volcando estructura para tabla db_impresiones3d.detalle_pedido
CREATE TABLE IF NOT EXISTS `detalle_pedido` (
  `id_detalle_pedido` int NOT NULL AUTO_INCREMENT,
  `id_pedido` int NOT NULL,
  `id_producto` int NOT NULL,
  `cantidad` int NOT NULL,
  `precio_unitario` decimal(10,2) NOT NULL,
  `subtotal` decimal(10,2) NOT NULL,
  PRIMARY KEY (`id_detalle_pedido`),
  KEY `fk_detalle_pedido_pedido` (`id_pedido`),
  KEY `fk_detalle_pedido_producto` (`id_producto`),
  CONSTRAINT `fk_detalle_pedido_pedido` FOREIGN KEY (`id_pedido`) REFERENCES `pedidos` (`id_pedido`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fk_detalle_pedido_producto` FOREIGN KEY (`id_producto`) REFERENCES `productos` (`id_producto`) ON DELETE RESTRICT ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Volcando datos para la tabla db_impresiones3d.detalle_pedido: ~0 rows (aproximadamente)

-- Volcando estructura para tabla db_impresiones3d.estado_impresion
CREATE TABLE IF NOT EXISTS `estado_impresion` (
  `id_estado_impresion` int NOT NULL AUTO_INCREMENT,
  `nombre` varchar(50) COLLATE utf8mb4_unicode_ci NOT NULL,
  PRIMARY KEY (`id_estado_impresion`),
  UNIQUE KEY `nombre` (`nombre`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Volcando datos para la tabla db_impresiones3d.estado_impresion: ~4 rows (aproximadamente)
INSERT INTO `estado_impresion` (`id_estado_impresion`, `nombre`) VALUES
	(1, 'Pendiente'),
	(2, 'Imprimiendo'),
	(3, 'Finalizada'),
	(4, 'Fallida');

-- Volcando estructura para tabla db_impresiones3d.estado_pedido
CREATE TABLE IF NOT EXISTS `estado_pedido` (
  `id_estado_pedido` int NOT NULL AUTO_INCREMENT,
  `nombre` varchar(50) COLLATE utf8mb4_unicode_ci NOT NULL,
  PRIMARY KEY (`id_estado_pedido`),
  UNIQUE KEY `nombre` (`nombre`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Volcando datos para la tabla db_impresiones3d.estado_pedido: ~4 rows (aproximadamente)
INSERT INTO `estado_pedido` (`id_estado_pedido`, `nombre`) VALUES
	(1, 'Pendiente'),
	(2, 'En proceso'),
	(3, 'Finalizado'),
	(4, 'Cancelado');

-- Volcando estructura para tabla db_impresiones3d.impresora_material
CREATE TABLE IF NOT EXISTS `impresora_material` (
  `id_impresora` int NOT NULL,
  `id_material` int NOT NULL,
  `observacion` varchar(255) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  PRIMARY KEY (`id_impresora`,`id_material`),
  KEY `fk_impresora_material_material` (`id_material`),
  CONSTRAINT `fk_impresora_material_impresora` FOREIGN KEY (`id_impresora`) REFERENCES `impresoras` (`id_impresora`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fk_impresora_material_material` FOREIGN KEY (`id_material`) REFERENCES `materiales` (`id_material`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Volcando datos para la tabla db_impresiones3d.impresora_material: ~0 rows (aproximadamente)

-- Volcando estructura para tabla db_impresiones3d.impresoras
CREATE TABLE IF NOT EXISTS `impresoras` (
  `id_impresora` int NOT NULL AUTO_INCREMENT,
  `nombre` varchar(50) COLLATE utf8mb4_unicode_ci NOT NULL,
  `marca` varchar(50) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `modelo` varchar(50) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `volumen_x` decimal(5,2) DEFAULT NULL,
  `volumen_y` decimal(5,2) DEFAULT NULL,
  `volumen_z` decimal(5,2) DEFAULT NULL,
  `tecnologia` varchar(50) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `estado` tinyint(1) NOT NULL DEFAULT '1',
  `fecha_alta` date NOT NULL,
  PRIMARY KEY (`id_impresora`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Volcando datos para la tabla db_impresiones3d.impresoras: ~0 rows (aproximadamente)

-- Volcando estructura para tabla db_impresiones3d.materiales
CREATE TABLE IF NOT EXISTS `materiales` (
  `id_material` int NOT NULL AUTO_INCREMENT,
  `nombre` varchar(50) COLLATE utf8mb4_unicode_ci NOT NULL,
  `tipo` varchar(50) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `unidad_medida` varchar(50) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `color` varchar(50) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `diametro` decimal(5,2) DEFAULT NULL,
  `activo` tinyint(1) NOT NULL DEFAULT '1',
  PRIMARY KEY (`id_material`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Volcando datos para la tabla db_impresiones3d.materiales: ~0 rows (aproximadamente)

-- Volcando estructura para tabla db_impresiones3d.modelo3d
CREATE TABLE IF NOT EXISTS `modelo3d` (
  `id_modelo3d` int NOT NULL AUTO_INCREMENT,
  `nombre` varchar(50) COLLATE utf8mb4_unicode_ci NOT NULL,
  `archivo` varchar(255) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `formato` varchar(50) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `descripcion` varchar(255) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `fecha_creacion` date NOT NULL,
  PRIMARY KEY (`id_modelo3d`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Volcando datos para la tabla db_impresiones3d.modelo3d: ~0 rows (aproximadamente)

-- Volcando estructura para tabla db_impresiones3d.movimiento_material
CREATE TABLE IF NOT EXISTS `movimiento_material` (
  `id_movimiento_material` int NOT NULL AUTO_INCREMENT,
  `id_material` int NOT NULL,
  `tipo_movimiento` varchar(50) COLLATE utf8mb4_unicode_ci NOT NULL,
  `cantidad` decimal(5,2) NOT NULL,
  `fecha_actualizacion` date NOT NULL,
  `observacion` text COLLATE utf8mb4_unicode_ci,
  PRIMARY KEY (`id_movimiento_material`),
  KEY `fk_movimiento_material_material` (`id_material`),
  CONSTRAINT `fk_movimiento_material_material` FOREIGN KEY (`id_material`) REFERENCES `materiales` (`id_material`) ON DELETE RESTRICT ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Volcando datos para la tabla db_impresiones3d.movimiento_material: ~0 rows (aproximadamente)

-- Volcando estructura para tabla db_impresiones3d.pedidos
CREATE TABLE IF NOT EXISTS `pedidos` (
  `id_pedido` int NOT NULL AUTO_INCREMENT,
  `id_cliente` int NOT NULL,
  `fecha_hora` datetime NOT NULL,
  `fecha_entrega_estimada` date DEFAULT NULL,
  `fecha_entrega_real` date DEFAULT NULL,
  `id_estado` int NOT NULL,
  `total_estimado` decimal(10,2) NOT NULL DEFAULT '0.00',
  `subtotal` decimal(10,2) NOT NULL DEFAULT '0.00',
  `descuento` decimal(10,2) NOT NULL DEFAULT '0.00',
  `total` decimal(10,2) NOT NULL DEFAULT '0.00',
  PRIMARY KEY (`id_pedido`),
  KEY `fk_pedidos_cliente` (`id_cliente`),
  KEY `fk_pedidos_estado` (`id_estado`),
  CONSTRAINT `fk_pedidos_cliente` FOREIGN KEY (`id_cliente`) REFERENCES `clientes` (`id_cliente`) ON DELETE RESTRICT ON UPDATE CASCADE,
  CONSTRAINT `fk_pedidos_estado` FOREIGN KEY (`id_estado`) REFERENCES `estado_pedido` (`id_estado_pedido`) ON DELETE RESTRICT ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Volcando datos para la tabla db_impresiones3d.pedidos: ~0 rows (aproximadamente)

-- Volcando estructura para tabla db_impresiones3d.personas
CREATE TABLE IF NOT EXISTS `personas` (
  `id_persona` int NOT NULL AUTO_INCREMENT,
  `dni` varchar(10) COLLATE utf8mb4_unicode_ci NOT NULL,
  `nombre` varchar(50) COLLATE utf8mb4_unicode_ci NOT NULL,
  `apellido` varchar(50) COLLATE utf8mb4_unicode_ci NOT NULL,
  `telefono` varchar(20) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `email` varchar(100) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `direccion` varchar(100) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `activo` tinyint(1) NOT NULL DEFAULT '1',
  `fecha_alta` date NOT NULL,
  PRIMARY KEY (`id_persona`),
  UNIQUE KEY `dni` (`dni`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Volcando datos para la tabla db_impresiones3d.personas: ~0 rows (aproximadamente)

-- Volcando estructura para tabla db_impresiones3d.producto_material
CREATE TABLE IF NOT EXISTS `producto_material` (
  `id_producto` int NOT NULL,
  `id_material` int NOT NULL,
  `cantidad_estimada` int NOT NULL,
  PRIMARY KEY (`id_producto`,`id_material`),
  KEY `fk_producto_material_material` (`id_material`),
  CONSTRAINT `fk_producto_material_material` FOREIGN KEY (`id_material`) REFERENCES `materiales` (`id_material`) ON DELETE RESTRICT ON UPDATE CASCADE,
  CONSTRAINT `fk_producto_material_producto` FOREIGN KEY (`id_producto`) REFERENCES `productos` (`id_producto`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Volcando datos para la tabla db_impresiones3d.producto_material: ~0 rows (aproximadamente)

-- Volcando estructura para tabla db_impresiones3d.productos
CREATE TABLE IF NOT EXISTS `productos` (
  `id_producto` int NOT NULL AUTO_INCREMENT,
  `id_modelo3d` int DEFAULT NULL,
  `nombre` varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  `descripcion` text COLLATE utf8mb4_unicode_ci,
  `precio_venta` decimal(10,2) NOT NULL DEFAULT '0.00',
  `activo` tinyint(1) NOT NULL DEFAULT '1',
  `fecha_alta` date NOT NULL,
  PRIMARY KEY (`id_producto`),
  KEY `fk_productos_modelo3d` (`id_modelo3d`),
  CONSTRAINT `fk_productos_modelo3d` FOREIGN KEY (`id_modelo3d`) REFERENCES `modelo3d` (`id_modelo3d`) ON DELETE SET NULL ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Volcando datos para la tabla db_impresiones3d.productos: ~0 rows (aproximadamente)

-- Volcando estructura para tabla db_impresiones3d.roles
CREATE TABLE IF NOT EXISTS `roles` (
  `id_rol` int NOT NULL AUTO_INCREMENT,
  `nombre` varchar(50) COLLATE utf8mb4_unicode_ci NOT NULL,
  `descripcion` varchar(255) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  PRIMARY KEY (`id_rol`),
  UNIQUE KEY `nombre` (`nombre`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Volcando datos para la tabla db_impresiones3d.roles: ~0 rows (aproximadamente)

-- Volcando estructura para tabla db_impresiones3d.trabajo_impresion
CREATE TABLE IF NOT EXISTS `trabajo_impresion` (
  `id_trabajo_impresion` int NOT NULL AUTO_INCREMENT,
  `id_pedido` int NOT NULL,
  `id_producto` int NOT NULL,
  `id_impresora` int NOT NULL,
  `id_modelo` int NOT NULL,
  `id_material` int NOT NULL,
  `cantidad` int NOT NULL,
  `id_estado` int NOT NULL,
  `fecha_inicio` date DEFAULT NULL,
  `fecha_fin` date DEFAULT NULL,
  `tiempo_estimado` time DEFAULT NULL,
  `peso_estimado` decimal(5,2) DEFAULT NULL,
  `observacion` text COLLATE utf8mb4_unicode_ci,
  PRIMARY KEY (`id_trabajo_impresion`),
  KEY `fk_trabajo_pedido` (`id_pedido`),
  KEY `fk_trabajo_producto` (`id_producto`),
  KEY `fk_trabajo_impresora` (`id_impresora`),
  KEY `fk_trabajo_modelo` (`id_modelo`),
  KEY `fk_trabajo_material` (`id_material`),
  KEY `fk_trabajo_estado` (`id_estado`),
  CONSTRAINT `fk_trabajo_estado` FOREIGN KEY (`id_estado`) REFERENCES `estado_impresion` (`id_estado_impresion`) ON DELETE RESTRICT ON UPDATE CASCADE,
  CONSTRAINT `fk_trabajo_impresora` FOREIGN KEY (`id_impresora`) REFERENCES `impresoras` (`id_impresora`) ON DELETE RESTRICT ON UPDATE CASCADE,
  CONSTRAINT `fk_trabajo_material` FOREIGN KEY (`id_material`) REFERENCES `materiales` (`id_material`) ON DELETE RESTRICT ON UPDATE CASCADE,
  CONSTRAINT `fk_trabajo_modelo` FOREIGN KEY (`id_modelo`) REFERENCES `modelo3d` (`id_modelo3d`) ON DELETE RESTRICT ON UPDATE CASCADE,
  CONSTRAINT `fk_trabajo_pedido` FOREIGN KEY (`id_pedido`) REFERENCES `pedidos` (`id_pedido`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fk_trabajo_producto` FOREIGN KEY (`id_producto`) REFERENCES `productos` (`id_producto`) ON DELETE RESTRICT ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Volcando datos para la tabla db_impresiones3d.trabajo_impresion: ~0 rows (aproximadamente)

-- Volcando estructura para tabla db_impresiones3d.usuario_rol
CREATE TABLE IF NOT EXISTS `usuario_rol` (
  `id_rol` int NOT NULL,
  `id_usuario` int NOT NULL,
  PRIMARY KEY (`id_rol`,`id_usuario`),
  KEY `fk_usuario_rol_usuario` (`id_usuario`),
  CONSTRAINT `fk_usuario_rol_rol` FOREIGN KEY (`id_rol`) REFERENCES `roles` (`id_rol`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fk_usuario_rol_usuario` FOREIGN KEY (`id_usuario`) REFERENCES `usuarios` (`id_usuario`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Volcando datos para la tabla db_impresiones3d.usuario_rol: ~0 rows (aproximadamente)

-- Volcando estructura para tabla db_impresiones3d.usuarios
CREATE TABLE IF NOT EXISTS `usuarios` (
  `id_usuario` int NOT NULL AUTO_INCREMENT,
  `id_persona` int NOT NULL,
  `usuario` varchar(50) COLLATE utf8mb4_unicode_ci NOT NULL,
  `password_hash` varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  `activo` tinyint(1) NOT NULL DEFAULT '1',
  `fecha_creacion` date NOT NULL,
  `ultimo_acceso` datetime DEFAULT NULL,
  PRIMARY KEY (`id_usuario`),
  UNIQUE KEY `usuario` (`usuario`),
  KEY `fk_usuarios_personas` (`id_persona`),
  CONSTRAINT `fk_usuarios_personas` FOREIGN KEY (`id_persona`) REFERENCES `personas` (`id_persona`) ON DELETE RESTRICT ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Volcando datos para la tabla db_impresiones3d.usuarios: ~0 rows (aproximadamente)

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
