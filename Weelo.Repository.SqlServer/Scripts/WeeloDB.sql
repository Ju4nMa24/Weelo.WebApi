USE [master]
GO
/****** Object:  Database [WeeloDB]    Script Date: 8/02/2022 12:18:20 a. m. ******/
CREATE DATABASE [WeeloDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'WeeloDB', FILENAME = N'C:\Users\juanm\WeeloDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'WeeloDB_log', FILENAME = N'C:\Users\juanm\WeeloDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [WeeloDB] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [WeeloDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [WeeloDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [WeeloDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [WeeloDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [WeeloDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [WeeloDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [WeeloDB] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [WeeloDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [WeeloDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [WeeloDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [WeeloDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [WeeloDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [WeeloDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [WeeloDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [WeeloDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [WeeloDB] SET  ENABLE_BROKER 
GO
ALTER DATABASE [WeeloDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [WeeloDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [WeeloDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [WeeloDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [WeeloDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [WeeloDB] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [WeeloDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [WeeloDB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [WeeloDB] SET  MULTI_USER 
GO
ALTER DATABASE [WeeloDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [WeeloDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [WeeloDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [WeeloDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [WeeloDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [WeeloDB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [WeeloDB] SET QUERY_STORE = OFF
GO
USE [WeeloDB]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 8/02/2022 12:18:20 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Owners]    Script Date: 8/02/2022 12:18:20 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Owners](
	[IdOwner] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[IdentificationNumber] [nvarchar](15) NOT NULL,
	[Address] [nvarchar](100) NOT NULL,
	[Photo] [nvarchar](max) NOT NULL,
	[CreationDate] [datetime2](7) NOT NULL,
	[Birthday] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Owners] PRIMARY KEY CLUSTERED 
(
	[IdOwner] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Properties]    Script Date: 8/02/2022 12:18:20 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Properties](
	[IdProperty] [uniqueidentifier] NOT NULL,
	[IdOwner] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Address] [nvarchar](max) NOT NULL,
	[Price] [float] NOT NULL,
	[CodeInternal] [nvarchar](max) NOT NULL,
	[CreationDate] [datetime2](7) NOT NULL,
	[Year] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Properties] PRIMARY KEY CLUSTERED 
(
	[IdProperty] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PropertyImages]    Script Date: 8/02/2022 12:18:20 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PropertyImages](
	[IdPropertyImage] [uniqueidentifier] NOT NULL,
	[IdProperty] [uniqueidentifier] NOT NULL,
	[File] [nvarchar](max) NOT NULL,
	[CreationDate] [datetime2](7) NOT NULL,
	[Enabled] [bit] NOT NULL,
 CONSTRAINT [PK_PropertyImages] PRIMARY KEY CLUSTERED 
(
	[IdPropertyImage] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PropertyTraces]    Script Date: 8/02/2022 12:18:20 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PropertyTraces](
	[IdPropertyTrace] [uniqueidentifier] NOT NULL,
	[IdProperty] [uniqueidentifier] NOT NULL,
	[DateSale] [datetime2](7) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Value] [nvarchar](max) NOT NULL,
	[Tax] [nvarchar](15) NOT NULL,
 CONSTRAINT [PK_PropertyTraces] PRIMARY KEY CLUSTERED 
(
	[IdPropertyTrace] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220208051232_Initial', N'5.0.13')
GO
INSERT [dbo].[Owners] ([IdOwner], [Name], [IdentificationNumber], [Address], [Photo], [CreationDate], [Birthday]) VALUES (N'941e9077-5fa2-4cbc-ba14-fefe4ed32fc7', N'Weelo', N'123456789', N'Calle 120 No. 30-19', N'', CAST(N'2022-02-08T05:12:32.2582764' AS DateTime2), N'07-02-2022')
GO
/****** Object:  Index [IX_Properties_IdOwner]    Script Date: 8/02/2022 12:18:20 a. m. ******/
CREATE NONCLUSTERED INDEX [IX_Properties_IdOwner] ON [dbo].[Properties]
(
	[IdOwner] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_PropertyImages_IdProperty]    Script Date: 8/02/2022 12:18:20 a. m. ******/
CREATE NONCLUSTERED INDEX [IX_PropertyImages_IdProperty] ON [dbo].[PropertyImages]
(
	[IdProperty] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_PropertyTraces_IdProperty]    Script Date: 8/02/2022 12:18:20 a. m. ******/
CREATE NONCLUSTERED INDEX [IX_PropertyTraces_IdProperty] ON [dbo].[PropertyTraces]
(
	[IdProperty] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Owners] ADD  DEFAULT (N'NO_CONTENT') FOR [Photo]
GO
ALTER TABLE [dbo].[Owners] ADD  DEFAULT ('2022-02-08T05:12:32.1271287Z') FOR [CreationDate]
GO
ALTER TABLE [dbo].[Properties] ADD  DEFAULT (N'100') FOR [Address]
GO
ALTER TABLE [dbo].[Properties] ADD  DEFAULT ('2022-02-08T05:12:32.2557472Z') FOR [CreationDate]
GO
ALTER TABLE [dbo].[PropertyImages] ADD  DEFAULT (N'NO_CONTENT') FOR [File]
GO
ALTER TABLE [dbo].[PropertyImages] ADD  DEFAULT ('2022-02-08T05:12:32.2571729Z') FOR [CreationDate]
GO
ALTER TABLE [dbo].[PropertyImages] ADD  DEFAULT (CONVERT([bit],(1))) FOR [Enabled]
GO
ALTER TABLE [dbo].[PropertyTraces] ADD  DEFAULT ('2022-02-08T05:12:32.2577791Z') FOR [DateSale]
GO
ALTER TABLE [dbo].[Properties]  WITH CHECK ADD  CONSTRAINT [FK_Properties_Owners_IdOwner] FOREIGN KEY([IdOwner])
REFERENCES [dbo].[Owners] ([IdOwner])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Properties] CHECK CONSTRAINT [FK_Properties_Owners_IdOwner]
GO
ALTER TABLE [dbo].[PropertyImages]  WITH CHECK ADD  CONSTRAINT [FK_PropertyImages_Properties_IdProperty] FOREIGN KEY([IdProperty])
REFERENCES [dbo].[Properties] ([IdProperty])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PropertyImages] CHECK CONSTRAINT [FK_PropertyImages_Properties_IdProperty]
GO
ALTER TABLE [dbo].[PropertyTraces]  WITH CHECK ADD  CONSTRAINT [FK_PropertyTraces_Properties_IdProperty] FOREIGN KEY([IdProperty])
REFERENCES [dbo].[Properties] ([IdProperty])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PropertyTraces] CHECK CONSTRAINT [FK_PropertyTraces_Properties_IdProperty]
GO
USE [master]
GO
ALTER DATABASE [WeeloDB] SET  READ_WRITE 
GO
