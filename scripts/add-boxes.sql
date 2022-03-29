CREATE TABLE `BoxTypes` (
    `BoxTypeId` int NOT NULL AUTO_INCREMENT,
    `TypeId` int NOT NULL,
    `IsJakana` tinyint(1) NOT NULL,
    `IsFortified` tinyint(1) NOT NULL,
    CONSTRAINT `pk_boxtypes` PRIMARY KEY (`BoxTypeId`),
    CONSTRAINT `FK_BoxTypes_TypeTokens_TypeId` FOREIGN KEY (`TypeId`) REFERENCES `TypeTokens` (`TypeId`) ON DELETE RESTRICT
) CHARACTER SET utf8mb4;

INSERT INTO `BoxTypes` (`BoxTypeId`, `IsFortified`, `IsJakana`, `TypeId`)
VALUES (1, FALSE, FALSE, 1);
INSERT INTO `BoxTypes` (`BoxTypeId`, `IsFortified`, `IsJakana`, `TypeId`)
VALUES (2, TRUE, FALSE, 2);
INSERT INTO `BoxTypes` (`BoxTypeId`, `IsFortified`, `IsJakana`, `TypeId`)
VALUES (3, FALSE, TRUE, 6);
INSERT INTO `BoxTypes` (`BoxTypeId`, `IsFortified`, `IsJakana`, `TypeId`)
VALUES (4, TRUE, TRUE, 7);