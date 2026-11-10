USE `db_impresiones3d`;

-- ========================================================
-- STORED PROCEDURES - MÓDULO 4: ESTADO_PEDIDO
-- ========================================================

DROP PROCEDURE IF EXISTS `sp_estado_pedido_crear`;
DROP PROCEDURE IF EXISTS `sp_estado_pedido_obtener_todos`;
DROP PROCEDURE IF EXISTS `sp_estado_pedido_obtener_por_id`;
DROP PROCEDURE IF EXISTS `sp_estado_pedido_actualizar`;
DROP PROCEDURE IF EXISTS `sp_estado_pedido_eliminar`;

DELIMITER //

CREATE PROCEDURE `sp_estado_pedido_crear`(
    IN p_nombre VARCHAR(50)
)
BEGIN
    INSERT INTO estado_pedido (nombre)
    VALUES (p_nombre);
    SELECT LAST_INSERT_ID() AS id_estado_pedido;
END //

CREATE PROCEDURE `sp_estado_pedido_obtener_todos`()
BEGIN
    SELECT id_estado_pedido, nombre
    FROM estado_pedido;
END //

CREATE PROCEDURE `sp_estado_pedido_obtener_por_id`(
    IN p_id INT
)
BEGIN
    SELECT id_estado_pedido, nombre
    FROM estado_pedido
    WHERE id_estado_pedido = p_id;
END //

CREATE PROCEDURE `sp_estado_pedido_actualizar`(
    IN p_id INT,
    IN p_nombre VARCHAR(50)
)
BEGIN
    UPDATE estado_pedido
    SET nombre = p_nombre
    WHERE id_estado_pedido = p_id;
END //

CREATE PROCEDURE `sp_estado_pedido_eliminar`(
    IN p_id INT
)
BEGIN
    DELETE FROM estado_pedido
    WHERE id_estado_pedido = p_id;
END //

-- ========================================================
-- STORED PROCEDURES - MÓDULO 4: PEDIDOS
-- ========================================================

DROP PROCEDURE IF EXISTS `sp_pedidos_crear`;
DROP PROCEDURE IF EXISTS `sp_pedidos_obtener_todos`;
DROP PROCEDURE IF EXISTS `sp_pedidos_obtener_por_id`;
DROP PROCEDURE IF EXISTS `sp_pedidos_obtener_por_cliente`;
DROP PROCEDURE IF EXISTS `sp_pedidos_actualizar`;
DROP PROCEDURE IF EXISTS `sp_pedidos_eliminar`;

CREATE PROCEDURE `sp_pedidos_crear`(
    IN p_id_cliente INT,
    IN p_fecha_hora DATETIME,
    IN p_fecha_entrega_estimada DATE,
    IN p_fecha_entrega_real DATE,
    IN p_id_estado INT,
    IN p_total_estimado DECIMAL(10,2),
    IN p_subtotal DECIMAL(10,2),
    IN p_descuento DECIMAL(10,2),
    IN p_total DECIMAL(10,2)
)
BEGIN
    INSERT INTO pedidos (
        id_cliente, fecha_hora, fecha_entrega_estimada, fecha_entrega_real,
        id_estado, total_estimado, subtotal, descuento, total
    ) VALUES (
        p_id_cliente, p_fecha_hora, p_fecha_entrega_estimada, p_fecha_entrega_real,
        p_id_estado, p_total_estimado, p_subtotal, p_descuento, p_total
    );
    SELECT LAST_INSERT_ID() AS id_pedido;
END //

CREATE PROCEDURE `sp_pedidos_obtener_todos`()
BEGIN
    SELECT 
        id_pedido, id_cliente, fecha_hora, fecha_entrega_estimada, fecha_entrega_real,
        id_estado, total_estimado, subtotal, descuento, total
    FROM pedidos;
END //

CREATE PROCEDURE `sp_pedidos_obtener_por_id`(
    IN p_id INT
)
BEGIN
    SELECT 
        id_pedido, id_cliente, fecha_hora, fecha_entrega_estimada, fecha_entrega_real,
        id_estado, total_estimado, subtotal, descuento, total
    FROM pedidos
    WHERE id_pedido = p_id;
END //

CREATE PROCEDURE `sp_pedidos_obtener_por_cliente`(
    IN p_id_cliente INT
)
BEGIN
    SELECT 
        id_pedido, id_cliente, fecha_hora, fecha_entrega_estimada, fecha_entrega_real,
        id_estado, total_estimado, subtotal, descuento, total
    FROM pedidos
    WHERE id_cliente = p_id_cliente;
END //

CREATE PROCEDURE `sp_pedidos_actualizar`(
    IN p_id INT,
    IN p_id_cliente INT,
    IN p_fecha_hora DATETIME,
    IN p_fecha_entrega_estimada DATE,
    IN p_fecha_entrega_real DATE,
    IN p_id_estado INT,
    IN p_total_estimado DECIMAL(10,2),
    IN p_subtotal DECIMAL(10,2),
    IN p_descuento DECIMAL(10,2),
    IN p_total DECIMAL(10,2)
)
BEGIN
    UPDATE pedidos
    SET 
        id_cliente = p_id_cliente,
        fecha_hora = p_fecha_hora,
        fecha_entrega_estimada = p_fecha_entrega_estimada,
        fecha_entrega_real = p_fecha_entrega_real,
        id_estado = p_id_estado,
        total_estimado = p_total_estimado,
        subtotal = p_subtotal,
        descuento = p_descuento,
        total = p_total
    WHERE id_pedido = p_id;
END //

CREATE PROCEDURE `sp_pedidos_eliminar`(
    IN p_id INT
)
BEGIN
    DELETE FROM pedidos WHERE id_pedido = p_id;
END //

-- ========================================================
-- STORED PROCEDURES - MÓDULO 4: DETALLE_PEDIDO
-- ========================================================

DROP PROCEDURE IF EXISTS `sp_detalle_pedido_crear`;
DROP PROCEDURE IF EXISTS `sp_detalle_pedido_obtener_todos`;
DROP PROCEDURE IF EXISTS `sp_detalle_pedido_obtener_por_id`;
DROP PROCEDURE IF EXISTS `sp_detalle_pedido_obtener_por_pedido`;
DROP PROCEDURE IF EXISTS `sp_detalle_pedido_actualizar`;
DROP PROCEDURE IF EXISTS `sp_detalle_pedido_eliminar`;

CREATE PROCEDURE `sp_detalle_pedido_crear`(
    IN p_id_pedido INT,
    IN p_id_producto INT,
    IN p_cantidad INT,
    IN p_precio_unitario DECIMAL(10,2),
    IN p_subtotal DECIMAL(10,2)
)
BEGIN
    INSERT INTO detalle_pedido (id_pedido, id_producto, cantidad, precio_unitario, subtotal)
    VALUES (p_id_pedido, p_id_producto, p_cantidad, p_precio_unitario, p_subtotal);
    SELECT LAST_INSERT_ID() AS id_detalle_pedido;
END //

CREATE PROCEDURE `sp_detalle_pedido_obtener_todos`()
BEGIN
    SELECT id_detalle_pedido, id_pedido, id_producto, cantidad, precio_unitario, subtotal
    FROM detalle_pedido;
END //

CREATE PROCEDURE `sp_detalle_pedido_obtener_por_id`(
    IN p_id INT
)
BEGIN
    SELECT id_detalle_pedido, id_pedido, id_producto, cantidad, precio_unitario, subtotal
    FROM detalle_pedido
    WHERE id_detalle_pedido = p_id;
END //

CREATE PROCEDURE `sp_detalle_pedido_obtener_por_pedido`(
    IN p_id_pedido INT
)
BEGIN
    SELECT id_detalle_pedido, id_pedido, id_producto, cantidad, precio_unitario, subtotal
    FROM detalle_pedido
    WHERE id_pedido = p_id_pedido;
END //

CREATE PROCEDURE `sp_detalle_pedido_actualizar`(
    IN p_id INT,
    IN p_id_pedido INT,
    IN p_id_producto INT,
    IN p_cantidad INT,
    IN p_precio_unitario DECIMAL(10,2),
    IN p_subtotal DECIMAL(10,2)
)
BEGIN
    UPDATE detalle_pedido
    SET id_pedido = p_id_pedido,
        id_producto = p_id_producto,
        cantidad = p_cantidad,
        precio_unitario = p_precio_unitario,
        subtotal = p_subtotal
    WHERE id_detalle_pedido = p_id;
END //

CREATE PROCEDURE `sp_detalle_pedido_eliminar`(
    IN p_id INT
)
BEGIN
    DELETE FROM detalle_pedido WHERE id_detalle_pedido = p_id;
END //

DELIMITER ;
