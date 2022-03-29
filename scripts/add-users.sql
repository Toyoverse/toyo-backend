CREATE TABLE `tb_users` (
    `id_login` varchar(20) CHARACTER SET utf8mb4 NOT NULL,
    `tx_password` varchar(100) CHARACTER SET utf8mb4 NULL,
    `tx_role` varchar(30) CHARACTER SET utf8mb4 NULL,
    CONSTRAINT `pk_login` PRIMARY KEY (`id_login`)
) CHARACTER SET utf8mb4;

INSERT INTO `tb_users` (`id_login`, `tx_password`, `tx_role`)
VALUES ('sync_service', '6fef533d07d5c11e14260529d9cea67978c31aee5d6e84f575f1dc95467dabbd', 'Block Chain Service');
