USE `db_impresiones3d`;

-- ========================================================
-- STORED PROCEDURES - MÓDULO 1: PERSONAS
-- ========================================================

DROP PROCEDURE IF EXISTS `sp_personas_obtener_todas`;
DROP PROCEDURE IF EXISTS `sp_personas_obtener_por_id`;
DROP PROCEDURE IF EXISTS `sp_personas_obtener_por_dni`;
DROP PROCEDURE IF EXISTS `sp_personas_existe_dni`;
DROP PROCEDURE IF EXISTS `sp_personas_existe_email`;
DROP PROCEDURE IF EXISTS `sp_personas_crear`;
DROP PROCEDURE IF EXISTS `sp_personas_buscar`;
DROP PROCEDURE IF EXISTS `sp_personas_eliminar`;
DROP PROCEDURE IF EXISTS `sp_personas_actualizar`;
DROP PROCEDURE IF EXISTS `sp_personas_cambiar_estado`;

DELIMITER //

CREATE PROCEDURE `sp_personas_obtener_todas`()
BEGIN
    SELECT id_persona, dni, nombre, apellido, telefono, email, direccion, activo, fecha_alta
    FROM personas;
END //

CREATE PROCEDURE `sp_personas_obtener_por_id`(
    IN p_id_persona INT
)
BEGIN
    SELECT id_persona, dni, nombre, apellido, telefono, email, direccion, activo, fecha_alta
    FROM personas
    WHERE id_persona = p_id_persona;
END //

CREATE PROCEDURE `sp_personas_obtener_por_dni`(
    IN p_dni VARCHAR(10)
)
BEGIN
    SELECT id_persona, dni, nombre, apellido, telefono, email, direccion, activo, fecha_alta
    FROM personas
    WHERE dni = p_dni;
END //

CREATE PROCEDURE `sp_personas_existe_dni`(
    IN p_dni VARCHAR(10)
)
BEGIN
    SELECT EXISTS(SELECT 1 FROM personas WHERE dni = p_dni);
END //

CREATE PROCEDURE `sp_personas_existe_email`(
    IN p_email VARCHAR(100)
)
BEGIN
    SELECT EXISTS(SELECT 1 FROM personas WHERE email = p_email);
END //

CREATE PROCEDURE `sp_personas_crear`(
    IN p_dni VARCHAR(10),
    IN p_nombre VARCHAR(50),
    IN p_apellido VARCHAR(50),
    IN p_telefono VARCHAR(20),
    IN p_email VARCHAR(100),
    IN p_direccion VARCHAR(100),
    IN p_activo TINYINT(1),
    IN p_fecha_alta DATE
)
BEGIN
    INSERT INTO personas (dni, nombre, apellido, telefono, email, direccion, activo, fecha_alta)
    VALUES (p_dni, p_nombre, p_apellido, p_telefono, p_email, p_direccion, p_activo, p_fecha_alta);
END //

CREATE PROCEDURE `sp_personas_buscar`(
    IN p_buscar VARCHAR(100)
)
BEGIN
    SELECT id_persona, dni, nombre, apellido, telefono, email, direccion, activo, fecha_alta
    FROM personas
    WHERE nombre LIKE CONCAT('%', p_buscar, '%')
       OR apellido LIKE CONCAT('%', p_buscar, '%')
       OR dni LIKE CONCAT('%', p_buscar, '%');
END //

CREATE PROCEDURE `sp_personas_eliminar`(
    IN p_id_persona INT
)
BEGIN
    DELETE FROM personas WHERE id_persona = p_id_persona;
END //

CREATE PROCEDURE `sp_personas_actualizar`(
    IN p_id_persona INT,
    IN p_dni VARCHAR(10),
    IN p_nombre VARCHAR(50),
    IN p_apellido VARCHAR(50),
    IN p_telefono VARCHAR(20),
    IN p_email VARCHAR(100),
    IN p_direccion VARCHAR(100),
    IN p_activo TINYINT(1),
    IN p_fecha_alta DATE
)
BEGIN
    UPDATE personas
    SET dni = p_dni,
        nombre = p_nombre,
        apellido = p_apellido,
        telefono = p_telefono,
        email = p_email,
        direccion = p_direccion,
        activo = p_activo,
        fecha_alta = p_fecha_alta
    WHERE id_persona = p_id_persona;
END //

CREATE PROCEDURE `sp_personas_cambiar_estado`(
    IN p_id_persona INT,
    IN p_activo TINYINT(1)
)
BEGIN
    UPDATE personas
    SET activo = p_activo
    WHERE id_persona = p_id_persona;
END //

-- ========================================================
-- STORED PROCEDURES - MÓDULO 1: CLIENTES, ROLES, USUARIOS
-- ========================================================

DROP PROCEDURE IF EXISTS `sp_clientes_obtener_todos`;
DROP PROCEDURE IF EXISTS `sp_clientes_crear`;
DROP PROCEDURE IF EXISTS `sp_roles_obtener_todos`;
DROP PROCEDURE IF EXISTS `sp_usuarios_login`;
DROP PROCEDURE IF EXISTS `sp_usuarios_crear`;
DROP PROCEDURE IF EXISTS `sp_usuarios_cambiar_estado`;
DROP PROCEDURE IF EXISTS `sp_usuario_obtener_por_usuario`;
DROP PROCEDURE IF EXISTS `sp_usuario_rol_asignar`;
DROP PROCEDURE IF EXISTS `sp_usuario_rol_obtener_por_usuario`;

CREATE PROCEDURE `sp_clientes_obtener_todos`()
BEGIN
    SELECT id_cliente, id_persona FROM clientes;
END //

CREATE PROCEDURE `sp_clientes_crear`(
    IN p_id_persona INT
)
BEGIN
    INSERT INTO clientes (id_persona) VALUES (p_id_persona);
END //

CREATE PROCEDURE `sp_roles_obtener_todos`()
BEGIN
    SELECT id_rol, nombre, descripcion FROM roles;
END //

CREATE PROCEDURE `sp_usuarios_login`(
    IN p_usuario VARCHAR(50),
    IN p_password_hash VARCHAR(255)
)
BEGIN
    SELECT id_usuario, id_persona, usuario, password_hash, activo, fecha_creacion, ultimo_acceso
    FROM usuarios
    WHERE usuario = p_usuario AND password_hash = p_password_hash;
END //

CREATE PROCEDURE `sp_usuarios_crear`(
    IN p_id_persona INT,
    IN p_usuario VARCHAR(50),
    IN p_password_hash VARCHAR(255),
    IN p_activo TINYINT(1),
    IN p_fecha_creacion DATE,
    IN p_ultimo_acceso DATETIME
)
BEGIN
    INSERT INTO usuarios (id_persona, usuario, password_hash, activo, fecha_creacion, ultimo_acceso)
    VALUES (p_id_persona, p_usuario, p_password_hash, p_activo, p_fecha_creacion, p_ultimo_acceso);
END //

CREATE PROCEDURE `sp_usuarios_cambiar_estado`(
    IN p_id_usuario INT,
    IN p_activo TINYINT(1)
)
BEGIN
    UPDATE usuarios SET activo = p_activo WHERE id_usuario = p_id_usuario;
END //

CREATE PROCEDURE `sp_usuario_obtener_por_usuario`(
    IN p_usuario VARCHAR(50)
)
BEGIN
    SELECT id_usuario, id_persona, usuario, password_hash, activo, fecha_creacion, ultimo_acceso
    FROM usuarios
    WHERE usuario = p_usuario;
END //

CREATE PROCEDURE `sp_usuario_rol_asignar`(
    IN p_id_usuario INT,
    IN p_id_rol INT
)
BEGIN
    INSERT IGNORE INTO usuario_rol (id_usuario, id_rol)
    VALUES (p_id_usuario, p_id_rol);
END //

CREATE PROCEDURE `sp_usuario_rol_obtener_por_usuario`(
    IN p_id_usuario INT
)
BEGIN
    SELECT id_rol, id_usuario
    FROM usuario_rol
    WHERE id_usuario = p_id_usuario;
END //

-- ========================================================
-- STORED PROCEDURES - MÓDULO 2: PRODUCTOS E INVENTARIO
-- ========================================================

DROP PROCEDURE IF EXISTS `sp_productos_obtener_todos`;
DROP PROCEDURE IF EXISTS `sp_productos_obtener_por_id`;
DROP PROCEDURE IF EXISTS `sp_productos_crear`;
DROP PROCEDURE IF EXISTS `sp_productos_actualizar`;
DROP PROCEDURE IF EXISTS `sp_productos_cambiar_estado`;
DROP PROCEDURE IF EXISTS `sp_producto_material_obtener_por_producto`;
DROP PROCEDURE IF EXISTS `sp_producto_material_asignar`;
DROP PROCEDURE IF EXISTS `sp_producto_material_actualizar`;
DROP PROCEDURE IF EXISTS `sp_producto_material_eliminar`;
DROP PROCEDURE IF EXISTS `sp_movimiento_material_obtener_todos`;
DROP PROCEDURE IF EXISTS `sp_movimiento_material_obtener_por_id`;
DROP PROCEDURE IF EXISTS `sp_movimiento_material_crear`;
DROP PROCEDURE IF EXISTS `sp_movimiento_material_actualizar`;
DROP PROCEDURE IF EXISTS `sp_movimiento_material_eliminar`;

CREATE PROCEDURE `sp_productos_obtener_todos`()
BEGIN
    SELECT id_producto, id_modelo3d, nombre, descripcion, precio_venta, activo, fecha_alta
    FROM productos;
END //

CREATE PROCEDURE `sp_productos_obtener_por_id`(
    IN p_id_producto INT
)
BEGIN
    SELECT id_producto, id_modelo3d, nombre, descripcion, precio_venta, activo, fecha_alta
    FROM productos
    WHERE id_producto = p_id_producto;
END //

CREATE PROCEDURE `sp_productos_crear`(
    IN p_id_modelo3d INT,
    IN p_nombre VARCHAR(255),
    IN p_descripcion TEXT,
    IN p_precio_venta DECIMAL(10,2),
    IN p_activo TINYINT(1),
    IN p_fecha_alta DATE
)
BEGIN
    INSERT INTO productos (id_modelo3d, nombre, descripcion, precio_venta, activo, fecha_alta)
    VALUES (p_id_modelo3d, p_nombre, p_descripcion, p_precio_venta, p_activo, p_fecha_alta);
END //

CREATE PROCEDURE `sp_productos_actualizar`(
    IN p_id_producto INT,
    IN p_id_modelo3d INT,
    IN p_nombre VARCHAR(255),
    IN p_descripcion TEXT,
    IN p_precio_venta DECIMAL(10,2),
    IN p_activo TINYINT(1),
    IN p_fecha_alta DATE
)
BEGIN
    UPDATE productos
    SET id_modelo3d = p_id_modelo3d,
        nombre = p_nombre,
        descripcion = p_descripcion,
        precio_venta = p_precio_venta,
        activo = p_activo,
        fecha_alta = p_fecha_alta
    WHERE id_producto = p_id_producto;
END //

CREATE PROCEDURE `sp_productos_cambiar_estado`(
    IN p_id_producto INT,
    IN p_activo TINYINT(1)
)
BEGIN
    UPDATE productos
    SET activo = p_activo
    WHERE id_producto = p_id_producto;
END //

CREATE PROCEDURE `sp_producto_material_obtener_por_producto`(
    IN p_id_producto INT
)
BEGIN
    SELECT id_producto, id_material, cantidad_estimada
    FROM producto_material
    WHERE id_producto = p_id_producto;
END //

CREATE PROCEDURE `sp_producto_material_asignar`(
    IN p_id_producto INT,
    IN p_id_material INT,
    IN p_cantidad_estimada INT
)
BEGIN
    INSERT INTO producto_material (id_producto, id_material, cantidad_estimada)
    VALUES (p_id_producto, p_id_material, p_cantidad_estimada);
END //

CREATE PROCEDURE `sp_producto_material_actualizar`(
    IN p_id_producto INT,
    IN p_id_material INT,
    IN p_cantidad_estimada INT
)
BEGIN
    UPDATE producto_material
    SET cantidad_estimada = p_cantidad_estimada
    WHERE id_producto = p_id_producto AND id_material = p_id_material;
END //

CREATE PROCEDURE `sp_producto_material_eliminar`(
    IN p_id_producto INT,
    IN p_id_material INT
)
BEGIN
    DELETE FROM producto_material
    WHERE id_producto = p_id_producto AND id_material = p_id_material;
END //

CREATE PROCEDURE `sp_movimiento_material_obtener_todos`()
BEGIN
    SELECT id_movimiento_material, id_material, tipo_movimiento, cantidad, fecha_actualizacion, observacion
    FROM movimiento_material;
END //

CREATE PROCEDURE `sp_movimiento_material_obtener_por_id`(
    IN p_id_movimiento_material INT
)
BEGIN
    SELECT id_movimiento_material, id_material, tipo_movimiento, cantidad, fecha_actualizacion, observacion
    FROM movimiento_material
    WHERE id_movimiento_material = p_id_movimiento_material;
END //

CREATE PROCEDURE `sp_movimiento_material_crear`(
    IN p_id_material INT,
    IN p_tipo_movimiento VARCHAR(50),
    IN p_cantidad DECIMAL(5,2),
    IN p_fecha_actualizacion DATETIME,
    IN p_observacion TEXT
)
BEGIN
    INSERT INTO movimiento_material (id_material, tipo_movimiento, cantidad, fecha_actualizacion, observacion)
    VALUES (p_id_material, p_tipo_movimiento, p_cantidad, DATE(p_fecha_actualizacion), p_observacion);
    SELECT LAST_INSERT_ID() AS id_movimiento_material;
END //

CREATE PROCEDURE `sp_movimiento_material_actualizar`(
    IN p_id_movimiento_material INT,
    IN p_id_material INT,
    IN p_tipo_movimiento VARCHAR(50),
    IN p_cantidad DECIMAL(5,2),
    IN p_fecha_actualizacion DATETIME,
    IN p_observacion TEXT
)
BEGIN
    UPDATE movimiento_material
    SET id_material = p_id_material,
        tipo_movimiento = p_tipo_movimiento,
        cantidad = p_cantidad,
        fecha_actualizacion = DATE(p_fecha_actualizacion),
        observacion = p_observacion
    WHERE id_movimiento_material = p_id_movimiento_material;
END //

CREATE PROCEDURE `sp_movimiento_material_eliminar`(
    IN p_id_movimiento_material INT
)
BEGIN
    DELETE FROM movimiento_material
    WHERE id_movimiento_material = p_id_movimiento_material;
END //

DELIMITER ;
