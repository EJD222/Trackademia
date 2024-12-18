-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Dec 13, 2024 at 02:53 PM
-- Server version: 10.4.32-MariaDB
-- PHP Version: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `trackademia`
--

-- --------------------------------------------------------

--
-- Table structure for table `tblacademichistory`
--

CREATE TABLE `tblacademichistory` (
  `ID` int(11) NOT NULL,
  `StudentID` int(11) NOT NULL,
  `Program` int(11) NOT NULL,
  `Level` varchar(50) DEFAULT NULL,
  `SchoolYear` varchar(20) DEFAULT NULL,
  `Semester` varchar(20) DEFAULT NULL,
  `Grade` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `tblacademichistory`
--

INSERT INTO `tblacademichistory` (`ID`, `StudentID`, `Program`, `Level`, `SchoolYear`, `Semester`, `Grade`) VALUES
(1, 11, 1, '4th Year', '2024-2025', '1st Sem', 94);

-- --------------------------------------------------------

--
-- Table structure for table `tblattendance`
--

CREATE TABLE `tblattendance` (
  `ID` int(11) NOT NULL,
  `Date` date NOT NULL,
  `StudentID` int(11) NOT NULL,
  `Status` varchar(50) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `tblattendance`
--

INSERT INTO `tblattendance` (`ID`, `Date`, `StudentID`, `Status`) VALUES
(1, '2024-12-13', 11, 'Present');

-- --------------------------------------------------------

--
-- Table structure for table `tblprogram`
--

CREATE TABLE `tblprogram` (
  `ID` int(11) NOT NULL,
  `ProgramName` varchar(255) NOT NULL,
  `ProgramCode` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `tblprogram`
--

INSERT INTO `tblprogram` (`ID`, `ProgramName`, `ProgramCode`) VALUES
(1, 'Bachelor of Science in Information Technology', 'BSIT'),
(2, 'Bachelor of Science in Computer Science', 'BSCS'),
(3, 'Bachelor of Multimedia Arts', 'BMMA');

-- --------------------------------------------------------

--
-- Table structure for table `tbluser`
--

CREATE TABLE `tbluser` (
  `ID` int(11) NOT NULL,
  `Email` varchar(255) NOT NULL,
  `Name` varchar(255) NOT NULL,
  `StudentId` varchar(50) DEFAULT NULL,
  `Address` varchar(255) DEFAULT NULL,
  `Birthdate` date DEFAULT NULL,
  `Program` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `tbluser`
--

INSERT INTO `tbluser` (`ID`, `Email`, `Name`, `StudentId`, `Address`, `Birthdate`, `Program`) VALUES
(11, 'daguio.joshuabenneth@auf.edu.ph', 'Joshua Benneth P. Daguio', '09-0056-557', 'Angeles City', '2002-05-07', 1);

--
-- Indexes for dumped tables
--

--
-- Indexes for table `tblacademichistory`
--
ALTER TABLE `tblacademichistory`
  ADD PRIMARY KEY (`ID`),
  ADD KEY `StudentID` (`StudentID`),
  ADD KEY `Program` (`Program`);

--
-- Indexes for table `tblattendance`
--
ALTER TABLE `tblattendance`
  ADD PRIMARY KEY (`ID`),
  ADD KEY `StudentID` (`StudentID`);

--
-- Indexes for table `tblprogram`
--
ALTER TABLE `tblprogram`
  ADD PRIMARY KEY (`ID`);

--
-- Indexes for table `tbluser`
--
ALTER TABLE `tbluser`
  ADD PRIMARY KEY (`ID`),
  ADD UNIQUE KEY `Email` (`Email`),
  ADD UNIQUE KEY `StudentID` (`StudentId`),
  ADD KEY `Program` (`Program`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `tblacademichistory`
--
ALTER TABLE `tblacademichistory`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT for table `tblattendance`
--
ALTER TABLE `tblattendance`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT for table `tblprogram`
--
ALTER TABLE `tblprogram`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT for table `tbluser`
--
ALTER TABLE `tbluser`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=12;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `tblacademichistory`
--
ALTER TABLE `tblacademichistory`
  ADD CONSTRAINT `tblacademichistory_ibfk_1` FOREIGN KEY (`StudentID`) REFERENCES `tbluser` (`ID`) ON DELETE CASCADE,
  ADD CONSTRAINT `tblacademichistory_ibfk_2` FOREIGN KEY (`Program`) REFERENCES `tblprogram` (`ID`);

--
-- Constraints for table `tblattendance`
--
ALTER TABLE `tblattendance`
  ADD CONSTRAINT `tblattendance_ibfk_1` FOREIGN KEY (`StudentID`) REFERENCES `tbluser` (`ID`) ON DELETE CASCADE;

--
-- Constraints for table `tbluser`
--
ALTER TABLE `tbluser`
  ADD CONSTRAINT `tbluser_ibfk_1` FOREIGN KEY (`Program`) REFERENCES `tblprogram` (`ID`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
