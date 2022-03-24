ALTER DATABASE CHARACTER SET utf8mb4;
CREATE TABLE IF NOT EXISTS `__EFMigrationsHistory` (
    `MigrationId` varchar(150) CHARACTER SET utf8mb4 NOT NULL,
    `ProductVersion` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK___EFMigrationsHistory` PRIMARY KEY (`MigrationId`)
) CHARACTER SET utf8mb4;

START TRANSACTION;

ALTER DATABASE CHARACTER SET utf8mb4;

CREATE TABLE `Events` (
    `Id` char(36) COLLATE ascii_general_ci NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `CreateTimeStamp` int NOT NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    CONSTRAINT `PK_Events` PRIMARY KEY (`Id`)
) CHARACTER SET utf8mb4;

CREATE TABLE `Parts` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `TorsoId` int NOT NULL,
    `Part` int NOT NULL,
    `RetroBone` int NOT NULL,
    `Size` int NOT NULL,
    `Variants` int NOT NULL,
    `Colors` int NOT NULL,
    `Cyber` int NOT NULL,
    `Prefix` longtext CHARACTER SET utf8mb4 NULL,
    `Desc` longtext CHARACTER SET utf8mb4 NULL,
    CONSTRAINT `PK_Parts` PRIMARY KEY (`Id`)
) CHARACTER SET utf8mb4;

CREATE TABLE `Players` (
    `Id` char(36) COLLATE ascii_general_ci NOT NULL,
    `User` longtext CHARACTER SET utf8mb4 NULL,
    `JoinTimeStamp` longtext CHARACTER SET utf8mb4 NULL,
    `Mail` longtext CHARACTER SET utf8mb4 NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `WalletAddress` longtext CHARACTER SET utf8mb4 NULL,
    CONSTRAINT `PK_Players` PRIMARY KEY (`Id`)
) CHARACTER SET utf8mb4;

CREATE TABLE `SmartContractToyoMints` (
    `TransactionHash` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `TokenId` int NOT NULL,
    `ChainId` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `Sender` longtext CHARACTER SET utf8mb4 NULL,
    `WalletAddress` longtext CHARACTER SET utf8mb4 NULL,
    `TypeId` int NOT NULL,
    `TotalSypply` int NOT NULL,
    `Gwei` bigint NOT NULL,
    `BlockNumber` int NOT NULL,
    CONSTRAINT `PK_SmartContractToyoMints` PRIMARY KEY (`TransactionHash`, `TokenId`, `ChainId`)
) CHARACTER SET utf8mb4;

CREATE TABLE `SmartContractToyoSwaps` (
    `TransactionHash` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `FromTokenId` int NOT NULL,
    `ToTokenId` int NOT NULL,
    `ChainId` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `FromTypeId` int NOT NULL,
    `ToTypeId` int NOT NULL,
    `Sender` longtext CHARACTER SET utf8mb4 NULL,
    `BlockNumber` int NOT NULL,
    CONSTRAINT `PK_SmartContractToyoSwaps` PRIMARY KEY (`TransactionHash`, `FromTokenId`, `ToTokenId`, `ChainId`)
) CHARACTER SET utf8mb4;

CREATE TABLE `SmartContractToyoSyncs` (
    `ChainId` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `ContractAddress` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `EventName` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `LastBlockNumber` int NOT NULL,
    CONSTRAINT `PK_SmartContractToyoSyncs` PRIMARY KEY (`ChainId`, `EventName`, `ContractAddress`)
) CHARACTER SET utf8mb4;

CREATE TABLE `SmartContractToyoTransfers` (
    `TransactionHash` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `TokenId` int NOT NULL,
    `ChainId` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `BlockNumber` int NOT NULL,
    `WalletAddress` longtext CHARACTER SET utf8mb4 NULL,
    CONSTRAINT `PK_SmartContractToyoTransfers` PRIMARY KEY (`TransactionHash`, `TokenId`, `ChainId`)
) CHARACTER SET utf8mb4;

CREATE TABLE `SmartContractToyoTypes` (
    `TransactionHash` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `TypeId` int NOT NULL,
    `ChainId` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `BlockNumber` int NOT NULL,
    `Sender` longtext CHARACTER SET utf8mb4 NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `MetaDataUrl` longtext CHARACTER SET utf8mb4 NULL,
    CONSTRAINT `PK_SmartContractToyoTypes` PRIMARY KEY (`TransactionHash`, `TypeId`, `ChainId`)
) CHARACTER SET utf8mb4;

CREATE TABLE `Stats` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    CONSTRAINT `PK_Stats` PRIMARY KEY (`Id`)
) CHARACTER SET utf8mb4;

CREATE TABLE `tb_users` (
    `id_login` varchar(20) CHARACTER SET utf8mb4 NOT NULL,
    `tx_password` varchar(100) CHARACTER SET utf8mb4 NULL,
    `tx_role` varchar(30) CHARACTER SET utf8mb4 NULL,
    CONSTRAINT `pk_login` PRIMARY KEY (`id_login`)
) CHARACTER SET utf8mb4;

CREATE TABLE `Toyos` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Existe` int NOT NULL,
    `Material` int NOT NULL,
    `BodyType` int NOT NULL,
    `Rarity` int NOT NULL,
    `Size` int NOT NULL,
    `Variants` int NOT NULL,
    `Colors` int NOT NULL,
    `Cyber` int NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Desc` longtext CHARACTER SET utf8mb4 NULL,
    `Thumb` longtext CHARACTER SET utf8mb4 NULL,
    `Video` longtext CHARACTER SET utf8mb4 NULL,
    `Region` longtext CHARACTER SET utf8mb4 NULL,
    CONSTRAINT `PK_Toyos` PRIMARY KEY (`Id`)
) CHARACTER SET utf8mb4;

CREATE TABLE `TypeTokens` (
    `TypeId` int NOT NULL,
    `ChainId` varchar(10) CHARACTER SET utf8mb4 NOT NULL,
    `Name` varchar(50) CHARACTER SET utf8mb4 NULL,
    `Type` varchar(20) CHARACTER SET utf8mb4 NULL,
    CONSTRAINT `PK_TypeTokens` PRIMARY KEY (`TypeId`, `ChainId`),
    CONSTRAINT `AK_TypeTokens_TypeId` UNIQUE (`TypeId`)
) CHARACTER SET utf8mb4;

CREATE TABLE `PartsPlayer` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `PartId` int NOT NULL,
    `StatId` int NOT NULL,
    `BonusStat` int NOT NULL,
    `TokenId` int NOT NULL,
    `WalletAddress` longtext CHARACTER SET utf8mb4 NULL,
    `ChainId` longtext CHARACTER SET utf8mb4 NULL,
    CONSTRAINT `PK_PartsPlayer` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_PartsPlayer_Parts_PartId` FOREIGN KEY (`PartId`) REFERENCES `Parts` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_PartsPlayer_Stats_StatId` FOREIGN KEY (`StatId`) REFERENCES `Stats` (`Id`) ON DELETE CASCADE
) CHARACTER SET utf8mb4;

CREATE TABLE `ToyosPlayer` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `ToyoId` int NOT NULL,
    `TokenId` int NOT NULL,
    `Vitality` int NOT NULL,
    `Strength` int NOT NULL,
    `Resistance` int NOT NULL,
    `CyberForce` int NOT NULL,
    `Resilience` int NOT NULL,
    `Precision` int NOT NULL,
    `Technique` int NOT NULL,
    `Analysis` int NOT NULL,
    `Speed` int NOT NULL,
    `Agility` int NOT NULL,
    `Stamina` int NOT NULL,
    `Luck` int NOT NULL,
    `WalletAddress` longtext CHARACTER SET utf8mb4 NULL,
    `ChainId` longtext CHARACTER SET utf8mb4 NULL,
    `ChangeValue` tinyint(1) NOT NULL,
    CONSTRAINT `PK_ToyosPlayer` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_ToyosPlayer_Toyos_ToyoId` FOREIGN KEY (`ToyoId`) REFERENCES `Toyos` (`Id`) ON DELETE CASCADE
) CHARACTER SET utf8mb4;

CREATE TABLE `BoxTypes` (
    `BoxTypeId` int NOT NULL AUTO_INCREMENT,
    `TypeId` int NOT NULL,
    `IsJakana` tinyint(1) NOT NULL,
    `IsFortified` tinyint(1) NOT NULL,
    CONSTRAINT `pk_boxtypes` PRIMARY KEY (`BoxTypeId`),
    CONSTRAINT `FK_BoxTypes_TypeTokens_TypeId` FOREIGN KEY (`TypeId`) REFERENCES `TypeTokens` (`TypeId`) ON DELETE RESTRICT
) CHARACTER SET utf8mb4;

CREATE TABLE `Tokens` (
    `Id` char(36) COLLATE ascii_general_ci NOT NULL,
    `NFTId` bigint NOT NULL,
    `TypeId` int NOT NULL,
    CONSTRAINT `PK_Tokens` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_Tokens_TypeTokens_TypeId` FOREIGN KEY (`TypeId`) REFERENCES `TypeTokens` (`TypeId`) ON DELETE CASCADE
) CHARACTER SET utf8mb4;

CREATE TABLE `TxsTokenPlayer` (
    `Id` char(36) COLLATE ascii_general_ci NOT NULL,
    `TxHash` longtext CHARACTER SET utf8mb4 NULL,
    `BlockNumber` bigint NOT NULL,
    `TxTimeStamp` longtext CHARACTER SET utf8mb4 NULL,
    `PlayerId` char(36) COLLATE ascii_general_ci NOT NULL,
    `TokenId` char(36) COLLATE ascii_general_ci NOT NULL,
    `ChainId` bigint NOT NULL,
    `ToyoSku` longtext CHARACTER SET utf8mb4 NULL,
    CONSTRAINT `PK_TxsTokenPlayer` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_TxsTokenPlayer_Players_PlayerId` FOREIGN KEY (`PlayerId`) REFERENCES `Players` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_TxsTokenPlayer_Tokens_TokenId` FOREIGN KEY (`TokenId`) REFERENCES `Tokens` (`Id`) ON DELETE CASCADE
) CHARACTER SET utf8mb4;

INSERT INTO `BoxTypes` (`BoxTypeId`, `IsFortified`, `IsJakana`, `TypeId`)
VALUES (1, FALSE, FALSE, 1);
INSERT INTO `BoxTypes` (`BoxTypeId`, `IsFortified`, `IsJakana`, `TypeId`)
VALUES (2, TRUE, FALSE, 2);
INSERT INTO `BoxTypes` (`BoxTypeId`, `IsFortified`, `IsJakana`, `TypeId`)
VALUES (3, FALSE, TRUE, 6);
INSERT INTO `BoxTypes` (`BoxTypeId`, `IsFortified`, `IsJakana`, `TypeId`)
VALUES (4, TRUE, TRUE, 7);

INSERT INTO `Stats` (`Id`, `Description`, `Name`)
VALUES (11, NULL, 'Stamina');
INSERT INTO `Stats` (`Id`, `Description`, `Name`)
VALUES (10, NULL, 'Agility');
INSERT INTO `Stats` (`Id`, `Description`, `Name`)
VALUES (9, NULL, 'Speed');
INSERT INTO `Stats` (`Id`, `Description`, `Name`)
VALUES (8, NULL, 'Analysis');
INSERT INTO `Stats` (`Id`, `Description`, `Name`)
VALUES (7, NULL, 'Technique');
INSERT INTO `Stats` (`Id`, `Description`, `Name`)
VALUES (5, NULL, 'Resilience');
INSERT INTO `Stats` (`Id`, `Description`, `Name`)
VALUES (12, NULL, 'Luck');
INSERT INTO `Stats` (`Id`, `Description`, `Name`)
VALUES (4, NULL, 'CyberForce');
INSERT INTO `Stats` (`Id`, `Description`, `Name`)
VALUES (3, NULL, 'Resistance');
INSERT INTO `Stats` (`Id`, `Description`, `Name`)
VALUES (2, NULL, 'Strength');
INSERT INTO `Stats` (`Id`, `Description`, `Name`)
VALUES (1, NULL, 'Vitality');
INSERT INTO `Stats` (`Id`, `Description`, `Name`)
VALUES (6, NULL, 'Precision');

INSERT INTO `tb_users` (`id_login`, `tx_password`, `tx_role`)
VALUES ('sync_service', '6fef533d07d5c11e14260529d9cea67978c31aee5d6e84f575f1dc95467dabbd', 'Block Chain Service');

CREATE UNIQUE INDEX `IX_BoxTypes_TypeId` ON `BoxTypes` (`TypeId`);

CREATE INDEX `IX_PartsPlayer_PartId` ON `PartsPlayer` (`PartId`);

CREATE INDEX `IX_PartsPlayer_StatId` ON `PartsPlayer` (`StatId`);

CREATE INDEX `IX_SmartContractToyoTypes_ChainId` ON `SmartContractToyoTypes` (`ChainId`);

CREATE INDEX `IX_Tokens_TypeId` ON `Tokens` (`TypeId`);

CREATE INDEX `IX_ToyosPlayer_ToyoId` ON `ToyosPlayer` (`ToyoId`);

CREATE INDEX `IX_TxsTokenPlayer_PlayerId` ON `TxsTokenPlayer` (`PlayerId`);

CREATE INDEX `IX_TxsTokenPlayer_TokenId` ON `TxsTokenPlayer` (`TokenId`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20220320224808_Initial', '5.0.12');

COMMIT;

