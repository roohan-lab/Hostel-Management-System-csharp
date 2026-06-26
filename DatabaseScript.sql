-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Jun 26, 2026 at 12:13 PM
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
-- Database: `hostelmanagementdb`
--

-- --------------------------------------------------------

--
-- Table structure for table `complaints`
--

CREATE TABLE `complaints` (
  `ComplaintID` int(11) NOT NULL,
  `RollNumber` varchar(50) NOT NULL,
  `ComplaintTitle` varchar(100) NOT NULL,
  `ComplaintDescription` varchar(500) DEFAULT NULL,
  `SubmissionDate` date NOT NULL,
  `ComplaintStatus` varchar(20) DEFAULT 'Pending',
  `IsNewNotification` int(11) DEFAULT 1
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `complaints`
--

INSERT INTO `complaints` (`ComplaintID`, `RollNumber`, `ComplaintTitle`, `ComplaintDescription`, `SubmissionDate`, `ComplaintStatus`, `IsNewNotification`) VALUES
(1, '1102', 'Technical Fault', 'The Fan of romm is not working', '2026-06-07', 'Pending', 0),
(3, '1102', 'Cleanliness', 'Washoom has been cleaned', '2026-06-09', 'Resolved', 0),
(4, '1103', 'transportation', 'the trasnportt facility is not good', '2026-06-10', 'Pending', 0);

-- --------------------------------------------------------

--
-- Table structure for table `dailyattendance`
--

CREATE TABLE `dailyattendance` (
  `AttendanceID` int(11) NOT NULL,
  `RollNumber` varchar(50) NOT NULL,
  `AttendanceDate` date NOT NULL,
  `AttendanceStatus` varchar(15) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `dailyattendance`
--

INSERT INTO `dailyattendance` (`AttendanceID`, `RollNumber`, `AttendanceDate`, `AttendanceStatus`) VALUES
(3, '1101', '2026-06-08', 'Present'),
(4, '1102', '2026-06-10', 'Absent');

-- --------------------------------------------------------

--
-- Table structure for table `fees`
--

CREATE TABLE `fees` (
  `FeeID` int(11) NOT NULL,
  `RollNumber` varchar(50) NOT NULL,
  `AmountDue` int(11) DEFAULT 0,
  `AmountPaid` int(11) DEFAULT 0,
  `DueDate` date DEFAULT NULL,
  `PaymentStatus` varchar(15) DEFAULT 'Pending'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `fees`
--

INSERT INTO `fees` (`FeeID`, `RollNumber`, `AmountDue`, `AmountPaid`, `DueDate`, `PaymentStatus`) VALUES
(6, '1101', 2000, 1000, '2026-07-09', 'Partial'),
(7, '1102', 5000, 5000, '2026-07-08', 'Paid');

-- --------------------------------------------------------

--
-- Table structure for table `mess`
--

CREATE TABLE `mess` (
  `MessID` int(11) NOT NULL,
  `HallName` varchar(50) DEFAULT NULL,
  `TotalSittingCapacity` int(11) NOT NULL,
  `RollNumber` varchar(50) NOT NULL,
  `FullName` varchar(100) DEFAULT NULL,
  `Department` varchar(50) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `mess`
--

INSERT INTO `mess` (`MessID`, `HallName`, `TotalSittingCapacity`, `RollNumber`, `FullName`, `Department`) VALUES
(6, 'Umer Hall', 50, '1102', 'Muhammad Arshad', 'Computer Science (CS)'),
(8, 'Umer Hall', 50, '1101', 'Muhammad Ali', 'Electrical Engineering (EE)');

-- --------------------------------------------------------

--
-- Table structure for table `messattendance`
--

CREATE TABLE `messattendance` (
  `MessAttendanceID` int(11) NOT NULL,
  `RollNumber` varchar(50) NOT NULL,
  `AttendanceDate` date NOT NULL,
  `MealType` varchar(15) NOT NULL,
  `Status` varchar(15) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `messattendance`
--

INSERT INTO `messattendance` (`MessAttendanceID`, `RollNumber`, `AttendanceDate`, `MealType`, `Status`) VALUES
(1, '1101', '2026-06-07', 'Aloo Prathy', 'Eaten'),
(2, '1122', '2026-06-09', 'Dahi Pratha', 'Eaten'),
(3, '1123', '2026-06-09', 'Alu Chicken', 'Eaten');

-- --------------------------------------------------------

--
-- Table structure for table `messmenu`
--

CREATE TABLE `messmenu` (
  `MenuID` int(11) NOT NULL,
  `DayName` varchar(15) NOT NULL,
  `MealType` varchar(15) NOT NULL,
  `FoodItem` varchar(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `messmenu`
--

INSERT INTO `messmenu` (`MenuID`, `DayName`, `MealType`, `FoodItem`) VALUES
(1, 'Monday', 'Breakfast', 'aloo pratha'),
(2, 'Wednesday', 'Lunch', 'Chiken Biryani');

-- --------------------------------------------------------

--
-- Table structure for table `owners`
--

CREATE TABLE `owners` (
  `OwnerID` int(11) NOT NULL,
  `Username` varchar(50) NOT NULL,
  `Password` varchar(50) NOT NULL,
  `FullName` varchar(100) DEFAULT NULL,
  `Email` varchar(100) DEFAULT NULL,
  `PhoneNumber` varchar(15) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `owners`
--

INSERT INTO `owners` (`OwnerID`, `Username`, `Password`, `FullName`, `Email`, `PhoneNumber`) VALUES
(1, 'Roohan', 'abc123', 'Muhammad Roohan Hussain', 'rohaan616@gmail.com', '0331-7049612'),
(2, 'Maib', 'bcd234', 'Muhammad Maib Majeed', 'Maib12@gmail.com', '0328-1700484');

-- --------------------------------------------------------

--
-- Table structure for table `rooms`
--

CREATE TABLE `rooms` (
  `RoomID` int(11) NOT NULL,
  `RoomNumber` varchar(10) NOT NULL,
  `HostelType` varchar(20) DEFAULT NULL,
  `TotalCapacity` int(11) NOT NULL,
  `CurrentOccupancy` int(11) DEFAULT 0,
  `RoomStatus` varchar(20) DEFAULT 'Available',
  `StudentName` varchar(100) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `rooms`
--

INSERT INTO `rooms` (`RoomID`, `RoomNumber`, `HostelType`, `TotalCapacity`, `CurrentOccupancy`, `RoomStatus`, `StudentName`) VALUES
(5, '03', 'Girl', 20, 5, 'Available', 'M Ali'),
(7, '2', 'Boy', 10, 5, 'Available', 'Ali');

-- --------------------------------------------------------

--
-- Table structure for table `students`
--

CREATE TABLE `students` (
  `StudentID` int(11) NOT NULL,
  `RollNumber` varchar(50) NOT NULL,
  `FullName` varchar(100) NOT NULL,
  `Gender` varchar(10) DEFAULT NULL,
  `CNIC` varchar(20) DEFAULT NULL,
  `PhoneNumber` varchar(15) DEFAULT NULL,
  `Email` varchar(100) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `students`
--

INSERT INTO `students` (`StudentID`, `RollNumber`, `FullName`, `Gender`, `CNIC`, `PhoneNumber`, `Email`) VALUES
(5, '1102', 'Muhammd Haroon', 'Male', '384032561254', '030012515447', 'HAroon@gmail.com');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `complaints`
--
ALTER TABLE `complaints`
  ADD PRIMARY KEY (`ComplaintID`);

--
-- Indexes for table `dailyattendance`
--
ALTER TABLE `dailyattendance`
  ADD PRIMARY KEY (`AttendanceID`);

--
-- Indexes for table `fees`
--
ALTER TABLE `fees`
  ADD PRIMARY KEY (`FeeID`);

--
-- Indexes for table `mess`
--
ALTER TABLE `mess`
  ADD PRIMARY KEY (`MessID`);

--
-- Indexes for table `messattendance`
--
ALTER TABLE `messattendance`
  ADD PRIMARY KEY (`MessAttendanceID`);

--
-- Indexes for table `messmenu`
--
ALTER TABLE `messmenu`
  ADD PRIMARY KEY (`MenuID`);

--
-- Indexes for table `owners`
--
ALTER TABLE `owners`
  ADD PRIMARY KEY (`OwnerID`);

--
-- Indexes for table `rooms`
--
ALTER TABLE `rooms`
  ADD PRIMARY KEY (`RoomID`);

--
-- Indexes for table `students`
--
ALTER TABLE `students`
  ADD PRIMARY KEY (`StudentID`),
  ADD UNIQUE KEY `RollNumber` (`RollNumber`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `complaints`
--
ALTER TABLE `complaints`
  MODIFY `ComplaintID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT for table `dailyattendance`
--
ALTER TABLE `dailyattendance`
  MODIFY `AttendanceID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- AUTO_INCREMENT for table `fees`
--
ALTER TABLE `fees`
  MODIFY `FeeID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=8;

--
-- AUTO_INCREMENT for table `mess`
--
ALTER TABLE `mess`
  MODIFY `MessID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=9;

--
-- AUTO_INCREMENT for table `messattendance`
--
ALTER TABLE `messattendance`
  MODIFY `MessAttendanceID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT for table `messmenu`
--
ALTER TABLE `messmenu`
  MODIFY `MenuID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT for table `owners`
--
ALTER TABLE `owners`
  MODIFY `OwnerID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT for table `rooms`
--
ALTER TABLE `rooms`
  MODIFY `RoomID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=8;

--
-- AUTO_INCREMENT for table `students`
--
ALTER TABLE `students`
  MODIFY `StudentID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
