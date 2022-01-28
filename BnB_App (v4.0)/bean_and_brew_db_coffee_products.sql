CREATE DATABASE  IF NOT EXISTS `bean_and_brew_db` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `bean_and_brew_db`;
-- MySQL dump 10.13  Distrib 8.0.27, for Win64 (x86_64)
--
-- Host: localhost    Database: bean_and_brew_db
-- ------------------------------------------------------
-- Server version	8.0.20

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `coffee_products`
--

DROP TABLE IF EXISTS `coffee_products`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `coffee_products` (
  `CoffeeID` int NOT NULL AUTO_INCREMENT,
  `CoffeeName` varchar(45) NOT NULL,
  `CoffeePrice` float NOT NULL DEFAULT '0',
  `Caffeinated` tinyint NOT NULL DEFAULT '1',
  PRIMARY KEY (`CoffeeID`),
  UNIQUE KEY `CoffeeID_UNIQUE` (`CoffeeID`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='Table to store details about each Coffee in the database.\nName, Price, Caffeine? etc...';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `coffee_products`
--

LOCK TABLES `coffee_products` WRITE;
/*!40000 ALTER TABLE `coffee_products` DISABLE KEYS */;
INSERT INTO `coffee_products` VALUES (1,'Flat White',2.5,1),(2,'Flat Black',2.5,1),(3,'Latte',4,1),(4,'Cappucino',5,1),(5,'Americano',3,1),(6,'Espresso',3.5,1),(7,'Mocha',4.5,1),(8,'Hot Chocolate',5.5,0),(9,'Iced Latte',5.5,1),(10,'Chai Latte',5,0);
/*!40000 ALTER TABLE `coffee_products` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2021-11-26 10:45:23
