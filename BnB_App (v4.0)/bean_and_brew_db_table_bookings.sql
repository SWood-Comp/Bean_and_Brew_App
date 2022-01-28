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
-- Table structure for table `table_bookings`
--

DROP TABLE IF EXISTS `table_bookings`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `table_bookings` (
  `table_bookingID` int NOT NULL AUTO_INCREMENT,
  `cust_name` varchar(45) NOT NULL,
  `cust_email` varchar(100) NOT NULL,
  `booking_date` date NOT NULL,
  `booking_time` varchar(8) NOT NULL,
  `num_people` int NOT NULL,
  `booking_store` varchar(45) NOT NULL,
  PRIMARY KEY (`table_bookingID`),
  UNIQUE KEY `table_bookingID_UNIQUE` (`table_bookingID`)
) ENGINE=InnoDB AUTO_INCREMENT=17 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='New Table Bookings';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `table_bookings`
--

LOCK TABLES `table_bookings` WRITE;
/*!40000 ALTER TABLE `table_bookings` DISABLE KEYS */;
INSERT INTO `table_bookings` VALUES (1,'SWood','swood@gmail.com','2021-11-24','11:00:00',3,'Leeds'),(2,'SJohnston','sj@notre.ac.uk','2021-11-30','14:30',4,'Leeds'),(3,'JBloggs','jblogg@hotmail.co.uk','2021-12-10','13:15',3,'Leeds'),(4,'BBrown','bobby21@gmail.com','2021-11-30','12:00',5,'Harrogate'),(5,'Reynolds','robrey@gmail.com','2021-11-29','10:00',2,'Harrogate'),(6,'LMiles','lmiles@notre.ac.uk','2021-11-24','15:30',4,'Leeds'),(7,'RobGreen','rg@gmail.com','2021-11-24','16:00',3,'Leeds'),(8,'MSalah','kingmo@lfc.com','2021-11-30','12:30',6,'Knaresborough'),(9,'StephyG','stephyg@hotmail.co.uk','2021-11-26','14:00',5,'Harrogate'),(10,'StephyG21','stephyg@hotmail.co.uk','2021-11-26','14:00',5,'Harrogate'),(11,'simonb','simon@test.com','2021-11-26','14:15',5,'Harrogate'),(12,'Sammo','sammo@notre.co.uk','2021-11-28','09:30',2,'Leeds'),(14,'Sammo','sammo@badnfl.co.uk','2021-11-28','09:30',2,'Leeds'),(16,'sj','sj@notre.ac.uk','2021-11-29','11:00',4,'Knaresborough Castle');
/*!40000 ALTER TABLE `table_bookings` ENABLE KEYS */;
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
