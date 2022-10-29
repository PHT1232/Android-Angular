-- phpMyAdmin SQL Dump
-- version 5.2.0
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Oct 29, 2022 at 08:47 PM
-- Server version: 10.4.24-MariaDB
-- PHP Version: 7.4.29

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `friedchicken`
--

-- --------------------------------------------------------

--
-- Table structure for table `admin`
--

CREATE TABLE `admin` (
  `username` varchar(150) NOT NULL,
  `password` varchar(150) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Table structure for table `chitiethoadon`
--

CREATE TABLE `chitiethoadon` (
  `maHD` varchar(255) DEFAULT NULL,
  `maSP` varchar(255) DEFAULT NULL,
  `soLuong` int(50) DEFAULT NULL,
  `donGia` double DEFAULT NULL,
  `thanhTien` double DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Table structure for table `hoadon`
--

CREATE TABLE `hoadon` (
  `maHD` varchar(255) NOT NULL,
  `maKH` varchar(255) DEFAULT NULL,
  `ngayLap` varchar(60) DEFAULT NULL,
  `tongtien` double DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Table structure for table `khachhang`
--

CREATE TABLE `khachhang` (
  `maKH` varchar(255) NOT NULL,
  `tenKH` varchar(150) DEFAULT NULL,
  `GT` varchar(4) DEFAULT NULL,
  `email` varchar(150) DEFAULT NULL,
  `sdt` varchar(10) DEFAULT NULL,
  `diaChi` varchar(255) DEFAULT NULL,
  `ngaySinh` varchar(30) DEFAULT NULL,
  `matKhau` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `khachhang`
--

INSERT INTO `khachhang` (`maKH`, `tenKH`, `GT`, `email`, `sdt`, `diaChi`, `ngaySinh`, `matKhau`) VALUES
('1', 'phat', 'nam', 'phat@gmail.com', '0966085995', 'hanoi', '19/09/2001', '');

-- --------------------------------------------------------

--
-- Table structure for table `phanloai`
--

CREATE TABLE `phanloai` (
  `maPL` varchar(255) NOT NULL,
  `tenPL` varchar(60) DEFAULT NULL,
  `anhPhanLoai` varchar(255) CHARACTER SET utf8 DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `phanloai`
--

INSERT INTO `phanloai` (`maPL`, `tenPL`, `anhPhanLoai`) VALUES
('CG', 'Cơm gà', 'Com ga'),
('GR', 'Gà rán', 'ga r'),
('NGK', 'Nước giải khát', 'ngk');

-- --------------------------------------------------------

--
-- Table structure for table `sanpham`
--

CREATE TABLE `sanpham` (
  `maSP` varchar(255) NOT NULL,
  `tenSP` varchar(150) DEFAULT NULL,
  `soLuong` int(30) DEFAULT NULL,
  `chiTiet` varchar(255) DEFAULT NULL,
  `maPL` varchar(255) DEFAULT NULL,
  `dongia` double DEFAULT NULL,
  `anhSanPham` varchar(255) CHARACTER SET utf8 DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `sanpham`
--

INSERT INTO `sanpham` (`maSP`, `tenSP`, `soLuong`, `chiTiet`, `maPL`, `dongia`, `anhSanPham`) VALUES
('CGBT', 'Cơm Gà Bít-tết', 30000, 'Cơm Gà Bít-tết', 'CG', 42000, 'comga'),
('CGR', 'Cánh gà rán', 30000, 'Cánh gà rán ngon bổ rẻ', 'GR', 36000, '5-HW.jpg'),
('CGTYK', 'Com Gà Teriyaki', 30000, 'Com Gà Teriyaki', 'CG', 42000, 'comga'),
('GCYAKI', '3 Miếng Gà Chikoyaki', 5000, '3 Miếng Gà Chikoyaki', 'GR', 114000, 'string'),
('GNUGGET', '5 Gà Miếng Nuggets', 50000, '5 Gà Miếng Nuggets', 'GR', 34000, 'string'),
('GQ', 'Gà Quay', 50000, 'Miếng Đùi Gà Quay Giấy Bạc/Đủi Gà Quay Tiêu', 'GR', 67000, 'string'),
('GRC', 'Ga ran cay', 20, 'Ga ran cay xe luoi', 'GR', 120000, 'string'),
('GRKC', 'Ga ran khong cay', 20, 'Ga ran khong cay giup nguoi an duoc thoai mai', 'GR', 120000, 'string'),
('GRKD', 'Ga ran khong dau', 20, 'Ga ran khong dau tot cho suc khoe gia dinh', 'GR', 100000, 'string'),
('GTT', 'Gà truyền thống', 10000, 'Gà truyền thống', 'GR', 38000, 'string'),
('GV', 'Gà Viên', 20000, 'Gà Viên', 'GR', 58000, 'string'),
('PES', 'PEPSI Lon', 60000, 'PEPSI Lon', 'NGK', 10000, 'PEPSI');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `admin`
--
ALTER TABLE `admin`
  ADD PRIMARY KEY (`username`);

--
-- Indexes for table `chitiethoadon`
--
ALTER TABLE `chitiethoadon`
  ADD KEY `maHD` (`maHD`),
  ADD KEY `maSP` (`maSP`);

--
-- Indexes for table `hoadon`
--
ALTER TABLE `hoadon`
  ADD PRIMARY KEY (`maHD`),
  ADD KEY `maKH` (`maKH`);

--
-- Indexes for table `khachhang`
--
ALTER TABLE `khachhang`
  ADD PRIMARY KEY (`maKH`);

--
-- Indexes for table `phanloai`
--
ALTER TABLE `phanloai`
  ADD PRIMARY KEY (`maPL`);

--
-- Indexes for table `sanpham`
--
ALTER TABLE `sanpham`
  ADD PRIMARY KEY (`maSP`),
  ADD KEY `maPL` (`maPL`);

--
-- Constraints for dumped tables
--

--
-- Constraints for table `chitiethoadon`
--
ALTER TABLE `chitiethoadon`
  ADD CONSTRAINT `chitiethoadon_ibfk_1` FOREIGN KEY (`maHD`) REFERENCES `hoadon` (`maHD`),
  ADD CONSTRAINT `chitiethoadon_ibfk_2` FOREIGN KEY (`maSP`) REFERENCES `sanpham` (`maSP`);

--
-- Constraints for table `hoadon`
--
ALTER TABLE `hoadon`
  ADD CONSTRAINT `hoadon_ibfk_1` FOREIGN KEY (`maKH`) REFERENCES `khachhang` (`maKH`);

--
-- Constraints for table `sanpham`
--
ALTER TABLE `sanpham`
  ADD CONSTRAINT `sanpham_ibfk_1` FOREIGN KEY (`maPL`) REFERENCES `phanloai` (`maPL`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
