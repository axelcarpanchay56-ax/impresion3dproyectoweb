USE `db_impresiones3d`;

-- --------------------------------------------------------
-- STORED PROCEDURES - MÓDULO 3: MATERIALES
-- --------------------------------------------------------

DROP PROCEDURE IF EXISTS `sp_materiales_crear`;
DROP PROCEDURE IF EXISTS `sp_materiales_obtener_todos`;
DROP PROCEDURE IF EXISTS `sp_materiales_obtener_por_id`;
DROP PROCEDURE IF EXISTS `sp_materiales_actualizar`;
DROP PROCEDURE IF EXISTS `sp_materiales_eliminar`;

DELIMITER //

CREATE PROCEDURE `sp_materiales_crear`(
    IN p_nombre VARCHAR(50),
    IN p_tipo VARCHAR(50),
    IN p_unidad_medida VARCHAR(50),
    IN p_color VARCHAR(50),
    IN p_diametro DECIMAL(5,2),
    IN p_activo TINYINT(1)
)
BEGIN
    INSERT INTO materiales (nombre, tipo, unidad_medida, color, diametro, activo)
    VALUES (p_nombre, p_tipo, p_unidad_medida, p_color, p_diametro, p_activo);
    SELECT LAST_INSERT_ID() AS id_material;
END //

CREATE PROCEDURE `sp_materiales_obtener_todos`()
BEGIN
    SELECT id_material, nombre, tipo, unidad_medida, color, diametro, activo
    FROM materiales;
END //

CREATE PROCEDURE `sp_materiales_obtener_por_id`(
    IN p_id INT
)
BEGIN
    SELECT id_material, nombre, tipo, unidad_medida, color, diametro, activo
    FROM materiales
    WHERE id_material = p_id;
END //

CREATE PROCEDURE `sp_materiales_actualizar`(
    IN p_id INT,
    IN p_nombre VARCHAR(50),
    IN p_tipo VARCHAR(50),
    IN p_unidad_medida VARCHAR(50),
    IN p_color VARCHAR(50),
    IN p_diametro DECIMAL(5,2),
    IN p_activo TINYINT(1)
)
BEGIN
    UPDATE materiales
    SET nombre = p_nombre,
        tipo = p_tipo,
        unidad_medida = p_unidad_medida,
        color = p_color,
        diametro = p_diametro,
        activo = p_activo
    WHERE id_material = p_id;
END //

CREATE PROCEDURE `sp_materiales_eliminar`(
    IN p_id INT
)
BEGIN
    DELETE FROM materiales
    WHERE id_material = p_id;
END //

DELIMITER ;
