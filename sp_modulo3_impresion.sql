USE `db_impresiones3d`;

-- ========================================================
-- STORED PROCEDURES - MÓDULO 3: IMPRESORA_MATERIAL
-- ========================================================

DROP PROCEDURE IF EXISTS `sp_impresora_material_crear`;
DROP PROCEDURE IF EXISTS `sp_impresora_material_obtener_todos`;
DROP PROCEDURE IF EXISTS `sp_impresora_material_obtener_por_id`;
DROP PROCEDURE IF EXISTS `sp_impresora_material_obtener_por_impresora`;
DROP PROCEDURE IF EXISTS `sp_impresora_material_actualizar`;
DROP PROCEDURE IF EXISTS `sp_impresora_material_eliminar`;

DELIMITER //

CREATE PROCEDURE `sp_impresora_material_crear`(
    IN p_id_impresora INT,
    IN p_id_material INT,
    IN p_observacion VARCHAR(255)
)
BEGIN
    INSERT INTO impresora_material (id_impresora, id_material, observacion)
    VALUES (p_id_impresora, p_id_material, p_observacion);
END //

CREATE PROCEDURE `sp_impresora_material_obtener_todos`()
BEGIN
    SELECT id_impresora, id_material, observacion
    FROM impresora_material;
END //

CREATE PROCEDURE `sp_impresora_material_obtener_por_id`(
    IN p_id_impresora INT,
    IN p_id_material INT
)
BEGIN
    SELECT id_impresora, id_material, observacion
    FROM impresora_material
    WHERE id_impresora = p_id_impresora AND id_material = p_id_material;
END //

CREATE PROCEDURE `sp_impresora_material_obtener_por_impresora`(
    IN p_id_impresora INT
)
BEGIN
    SELECT id_impresora, id_material, observacion
    FROM impresora_material
    WHERE id_impresora = p_id_impresora;
END //

CREATE PROCEDURE `sp_impresora_material_actualizar`(
    IN p_id_impresora INT,
    IN p_id_material INT,
    IN p_observacion VARCHAR(255)
)
BEGIN
    UPDATE impresora_material
    SET observacion = p_observacion
    WHERE id_impresora = p_id_impresora AND id_material = p_id_material;
END //

CREATE PROCEDURE `sp_impresora_material_eliminar`(
    IN p_id_impresora INT,
    IN p_id_material INT
)
BEGIN
    DELETE FROM impresora_material
    WHERE id_impresora = p_id_impresora AND id_material = p_id_material;
END //

-- ========================================================
-- STORED PROCEDURES - MÓDULO 3: CONSUMO_MATERIAL
-- ========================================================

DROP PROCEDURE IF EXISTS `sp_consumo_material_crear`;
DROP PROCEDURE IF EXISTS `sp_consumo_material_obtener_todos`;
DROP PROCEDURE IF EXISTS `sp_consumo_material_obtener_por_id`;
DROP PROCEDURE IF EXISTS `sp_consumo_material_obtener_por_trabajo`;
DROP PROCEDURE IF EXISTS `sp_consumo_material_actualizar`;
DROP PROCEDURE IF EXISTS `sp_consumo_material_eliminar`;

CREATE PROCEDURE `sp_consumo_material_crear`(
    IN p_id_trabajo INT,
    IN p_id_material INT,
    IN p_cantidad DECIMAL(5,2),
    IN p_fecha_hora DATETIME
)
BEGIN
    INSERT INTO consumo_material (id_trabajo, id_material, cantidad, fecha_hora)
    VALUES (p_id_trabajo, p_id_material, p_cantidad, p_fecha_hora);
    SELECT LAST_INSERT_ID() AS id_consumo_material;
END //

CREATE PROCEDURE `sp_consumo_material_obtener_todos`()
BEGIN
    SELECT id_consumo_material, id_trabajo, id_material, cantidad, fecha_hora
    FROM consumo_material;
END //

CREATE PROCEDURE `sp_consumo_material_obtener_por_id`(
    IN p_id INT
)
BEGIN
    SELECT id_consumo_material, id_trabajo, id_material, cantidad, fecha_hora
    FROM consumo_material
    WHERE id_consumo_material = p_id;
END //

CREATE PROCEDURE `sp_consumo_material_obtener_por_trabajo`(
    IN p_id_trabajo INT
)
BEGIN
    SELECT id_consumo_material, id_trabajo, id_material, cantidad, fecha_hora
    FROM consumo_material
    WHERE id_trabajo = p_id_trabajo;
END //

CREATE PROCEDURE `sp_consumo_material_actualizar`(
    IN p_id INT,
    IN p_id_trabajo INT,
    IN p_id_material INT,
    IN p_cantidad DECIMAL(5,2),
    IN p_fecha_hora DATETIME
)
BEGIN
    UPDATE consumo_material
    SET id_trabajo = p_id_trabajo,
        id_material = p_id_material,
        cantidad = p_cantidad,
        fecha_hora = p_fecha_hora
    WHERE id_consumo_material = p_id;
END //

CREATE PROCEDURE `sp_consumo_material_eliminar`(
    IN p_id INT
)
BEGIN
    DELETE FROM consumo_material
    WHERE id_consumo_material = p_id;
END //

DELIMITER ;
