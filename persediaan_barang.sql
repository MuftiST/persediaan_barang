-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Sep 05, 2026 at 02:53 PM
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
-- Database: `persediaan_barang`
--

-- --------------------------------------------------------

--
-- Table structure for table `barang`
--

CREATE TABLE `barang` (
  `idb` int(11) NOT NULL,
  `nama_barang` varchar(100) NOT NULL,
  `kategori_id` int(11) NOT NULL,
  `satuan_id` int(11) NOT NULL,
  `stok` int(11) NOT NULL DEFAULT 0,
  `harga_beli` decimal(12,2) NOT NULL,
  `harga_jual` decimal(12,2) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `barang`
--

INSERT INTO `barang` (`idb`, `nama_barang`, `kategori_id`, `satuan_id`, `stok`, `harga_beli`, `harga_jual`) VALUES
(2, 'mie ayam intel', 1, 2, 526, 10000.00, 15000.00),
(3, 'baso kotak', 1, 3, 969, 6000.00, 80000.00);

-- --------------------------------------------------------

--
-- Table structure for table `kategori`
--

CREATE TABLE `kategori` (
  `id_kategori` int(11) NOT NULL,
  `nama_kategori` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `kategori`
--

INSERT INTO `kategori` (`id_kategori`, `nama_kategori`) VALUES
(1, 'makanan'),
(2, 'minuman'),
(4, 'obat'),
(3, 'pakaian');

-- --------------------------------------------------------

--
-- Table structure for table `roles`
--

CREATE TABLE `roles` (
  `id_role` int(11) NOT NULL,
  `nama_role` varchar(50) NOT NULL,
  `keterangan` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `roles`
--

INSERT INTO `roles` (`id_role`, `nama_role`, `keterangan`) VALUES
(1, 'admin', 'kelola user, roles, dan seluruh data master'),
(2, 'petugas', 'kelola barang & mencatat mutasi stok masuk/keluar');

-- --------------------------------------------------------

--
-- Table structure for table `satuan`
--

CREATE TABLE `satuan` (
  `id_satuan` int(11) NOT NULL,
  `nama_satuan` varchar(20) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `satuan`
--

INSERT INTO `satuan` (`id_satuan`, `nama_satuan`) VALUES
(2, 'box'),
(3, 'kg'),
(1, 'pcs');

-- --------------------------------------------------------

--
-- Table structure for table `stok_barang`
--

CREATE TABLE `stok_barang` (
  `id_transaksi` varchar(100) NOT NULL,
  `idb` int(11) NOT NULL,
  `supplier_id` int(11) DEFAULT NULL COMMENT 'diisi hanya kalau jenis_transaksi = masuk',
  `harga_beli` decimal(12,2) DEFAULT NULL COMMENT 'harga beli per transaksi masuk (bisa beda-beda tiap kedatangan)',
  `tanggal_transaksi` timestamp NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp(),
  `jenis_transaksi` enum('masuk','keluar','penyesuaian') NOT NULL,
  `jumlah_masuk` int(11) NOT NULL DEFAULT 0,
  `jumlah_keluar` int(11) NOT NULL DEFAULT 0,
  `saldo_akhir` int(11) NOT NULL,
  `keterangan` text DEFAULT NULL,
  `id_user` int(11) DEFAULT NULL COMMENT 'petugas yang input transaksi'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `stok_barang`
--

INSERT INTO `stok_barang` (`id_transaksi`, `idb`, `supplier_id`, `harga_beli`, `tanggal_transaksi`, `jenis_transaksi`, `jumlah_masuk`, `jumlah_keluar`, `saldo_akhir`, `keterangan`, `id_user`) VALUES
('TRS-20260822-0001', 2, NULL, NULL, '2026-08-22 07:21:12', 'penyesuaian', 0, 6, 517, '', NULL),
('TRS-20260822-0002', 2, 1, 7000.00, '2026-08-22 07:22:08', 'masuk', 89, 0, 606, '', NULL),
('TRS-20260822-0003', 2, NULL, NULL, '2026-08-22 08:58:17', 'penyesuaian', 0, 80, 526, '', NULL),
('TRS-20260822-0004', 3, 3, 9000.00, '2026-08-22 14:23:35', 'masuk', 90, 0, 1059, '', NULL),
('TRS-20260822-0005', 3, NULL, NULL, '2026-08-22 14:24:19', 'keluar', 0, 90, 969, '', NULL);

-- --------------------------------------------------------

--
-- Table structure for table `supplier`
--

CREATE TABLE `supplier` (
  `id_supplier` int(11) NOT NULL,
  `nama_supplier` varchar(100) NOT NULL,
  `alamat` varchar(200) NOT NULL,
  `telepon` varchar(50) NOT NULL,
  `email` varchar(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `supplier`
--

INSERT INTO `supplier` (`id_supplier`, `nama_supplier`, `alamat`, `telepon`, `email`) VALUES
(1, 'unikloh', 'bandung', '08162816212', 'uniklohrek@gmail.com'),
(2, 'indoput', 'karawang', '08676767', 'indput@gmail.com'),
(3, 'GG MU', 'uu6', '778', 'myj');

-- --------------------------------------------------------

--
-- Table structure for table `users`
--

CREATE TABLE `users` (
  `id_user` int(11) NOT NULL,
  `nama` varchar(100) NOT NULL,
  `username` varchar(100) NOT NULL,
  `password` char(100) NOT NULL,
  `id_role` int(11) NOT NULL,
  `email` varchar(100) NOT NULL,
  `no_telp` varchar(100) NOT NULL,
  `status` enum('aktif','nonaktif') NOT NULL DEFAULT 'aktif'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `users`
--

INSERT INTO `users` (`id_user`, `nama`, `username`, `password`, `id_role`, `email`, `no_telp`, `status`) VALUES
(19, '1', '1', '$2a$11$a79C75Y9KvOPKYasPAJ1V.WPcSZyavFBFbR2Opydv6SzCt0K2XpZm', 1, '1', '1', 'aktif');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `barang`
--
ALTER TABLE `barang`
  ADD PRIMARY KEY (`idb`),
  ADD KEY `fk_barang_kategori` (`kategori_id`),
  ADD KEY `fk_barang_satuan` (`satuan_id`);

--
-- Indexes for table `kategori`
--
ALTER TABLE `kategori`
  ADD PRIMARY KEY (`id_kategori`),
  ADD UNIQUE KEY `nama_kategori` (`nama_kategori`);

--
-- Indexes for table `roles`
--
ALTER TABLE `roles`
  ADD PRIMARY KEY (`id_role`);

--
-- Indexes for table `satuan`
--
ALTER TABLE `satuan`
  ADD PRIMARY KEY (`id_satuan`),
  ADD UNIQUE KEY `nama_satuan` (`nama_satuan`);

--
-- Indexes for table `stok_barang`
--
ALTER TABLE `stok_barang`
  ADD PRIMARY KEY (`id_transaksi`),
  ADD KEY `fk_stok_supplier` (`supplier_id`),
  ADD KEY `fk_stok_barang` (`idb`),
  ADD KEY `fk_stok_user` (`id_user`),
  ADD KEY `idx_tanggal` (`tanggal_transaksi`),
  ADD KEY `idx_jenis` (`jenis_transaksi`);

--
-- Indexes for table `supplier`
--
ALTER TABLE `supplier`
  ADD PRIMARY KEY (`id_supplier`);

--
-- Indexes for table `users`
--
ALTER TABLE `users`
  ADD PRIMARY KEY (`id_user`),
  ADD UNIQUE KEY `username` (`username`),
  ADD KEY `fk_users_role` (`id_role`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `barang`
--
ALTER TABLE `barang`
  MODIFY `idb` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT for table `kategori`
--
ALTER TABLE `kategori`
  MODIFY `id_kategori` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT for table `roles`
--
ALTER TABLE `roles`
  MODIFY `id_role` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT for table `satuan`
--
ALTER TABLE `satuan`
  MODIFY `id_satuan` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT for table `supplier`
--
ALTER TABLE `supplier`
  MODIFY `id_supplier` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT for table `users`
--
ALTER TABLE `users`
  MODIFY `id_user` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=20;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `barang`
--
ALTER TABLE `barang`
  ADD CONSTRAINT `fk_barang_kategori` FOREIGN KEY (`kategori_id`) REFERENCES `kategori` (`id_kategori`),
  ADD CONSTRAINT `fk_barang_satuan` FOREIGN KEY (`satuan_id`) REFERENCES `satuan` (`id_satuan`);

--
-- Constraints for table `stok_barang`
--
ALTER TABLE `stok_barang`
  ADD CONSTRAINT `fk_stok_barang` FOREIGN KEY (`idb`) REFERENCES `barang` (`idb`),
  ADD CONSTRAINT `fk_stok_supplier` FOREIGN KEY (`supplier_id`) REFERENCES `supplier` (`id_supplier`),
  ADD CONSTRAINT `fk_stok_user` FOREIGN KEY (`id_user`) REFERENCES `users` (`id_user`);

--
-- Constraints for table `users`
--
ALTER TABLE `users`
  ADD CONSTRAINT `fk_users_role` FOREIGN KEY (`id_role`) REFERENCES `roles` (`id_role`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
