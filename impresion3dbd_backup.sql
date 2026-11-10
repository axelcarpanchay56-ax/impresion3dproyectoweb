USE `db_impresiones3d`;

-- --------------------------------------------------------
-- STORED PROCEDURES - MÓDULO 4: TRABAJO IMPRESIÓN
-- --------------------------------------------------------

DROP PROCEDURE IF EXISTS `sp_trabajo_impresion_crear`;
DROP PROCEDURE IF EXISTS `sp_trabajo_impresion_obtener_todos`;
DROP PROCEDURE IF EXISTS `sp_trabajo_impresion_obtener_por_id`;
DROP PROCEDURE IF EXISTS `sp_trabajo_impresion_actualizar`;
DROP PROCEDURE IF EXISTS `sp_trabajo_impresion_eliminar`;

DELIMITER //

CREATE PROCEDURE `sp_trabajo_impresion_crear`(
    IN p_id_pedido INT,
    IN p_id_producto INT,
    IN p_id_impresora INT,
    IN p_id_modelo INT,
    IN p_id_material INT,
    IN p_cantidad INT,
    IN p_id_estado INT,
    IN p_fecha_inicio DATE,
    IN p_peso_estimado DECIMAL(5,2),
    IN p_observacion TEXT
)
BEGIN
    INSERT INTO trabajo_impresion (
        id_pedido, id_producto, id_impresora, id_modelo, id_material,
        cantidad, id_estado, fecha_inicio, peso_estimado, observacion
    ) VALUES (
        p_id_pedido, p_id_producto, p_id_impresora, p_id_modelo, p_id_material,
        p_cantidad, p_id_estado, p_fecha_inicio, p_peso_estimado, p_observacion
    );
    SELECT LAST_INSERT_ID() AS id_trabajo_impresion;
END //

CREATE PROCEDURE `sp_trabajo_impresion_obtener_todos`()
BEGIN
    SELECT 
        id_trabajo_impresion, id_pedido, id_producto, id_impresora, 
        id_modelo, id_material, cantidad, id_estado, 
        fecha_inicio, fecha_fin, tiempo_estimado, peso_estimado, observacion 
    FROM trabajo_impresion;
END //

CREATE PROCEDURE `sp_trabajo_impresion_obtener_por_id`(
    IN p_id INT
)
BEGIN
    SELECT 
        id_trabajo_impresion, id_pedido, id_producto, id_impresora, 
        id_modelo, id_material, cantidad, id_estado, 
        fecha_inicio, fecha_fin, tiempo_estimado, peso_estimado, observacion 
    FROM trabajo_impresion 
    WHERE id_trabajo_impresion = p_id;
END //

CREATE PROCEDURE `sp_trabajo_impresion_actualizar`(
    IN p_id INT,
    IN p_id_pedido INT,
    IN p_id_producto INT,
    IN p_id_impresora INT,
    IN p_id_modelo INT,
    IN p_id_material INT,
    IN p_cantidad INT,
    IN p_id_estado INT,
    IN p_fecha_inicio DATE,
    IN p_fecha_fin DATE,
    IN p_peso_estimado DECIMAL(5,2),
    IN p_observacion TEXT
)
BEGIN
    UPDATE trabajo_impresion
    SET 
        id_pedido = p_id_pedido,
        id_producto = p_id_producto,
        id_impresora = p_id_impresora,
        id_modelo = p_id_modelo,
        id_material = p_id_material,
        cantidad = p_cantidad,
        id_estado = p_id_estado,
        fecha_inicio = p_fecha_inicio,
        fecha_fin = p_fecha_fin,
        peso_estimado = p_peso_estimado,
        observacion = p_observacion
    WHERE id_trabajo_impresion = p_id;
END //

CREATE PROCEDURE `sp_trabajo_impresion_eliminar`(
    IN p_id INT
)
BEGIN
    DELETE FROM trabajo_impresion WHERE id_trabajo_impresion = p_id;
END //

DELIMITER ;
