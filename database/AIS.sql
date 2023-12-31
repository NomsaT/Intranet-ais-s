USE [master]
GO
/****** Object:  Database [AIS]    Script Date: 2021/10/28 23:04:11 ******/
CREATE DATABASE [AIS]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'AIS', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\AIS.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'AIS_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\AIS_log.ldf' , SIZE = 73728KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [AIS] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [AIS].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [AIS] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [AIS] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [AIS] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [AIS] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [AIS] SET ARITHABORT OFF 
GO
ALTER DATABASE [AIS] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [AIS] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [AIS] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [AIS] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [AIS] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [AIS] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [AIS] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [AIS] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [AIS] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [AIS] SET  DISABLE_BROKER 
GO
ALTER DATABASE [AIS] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [AIS] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [AIS] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [AIS] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [AIS] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [AIS] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [AIS] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [AIS] SET RECOVERY FULL 
GO
ALTER DATABASE [AIS] SET  MULTI_USER 
GO
ALTER DATABASE [AIS] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [AIS] SET DB_CHAINING OFF 
GO
ALTER DATABASE [AIS] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [AIS] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [AIS] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'AIS', N'ON'
GO
ALTER DATABASE [AIS] SET QUERY_STORE = OFF
GO
USE [AIS]
GO
/****** Object:  Table [dbo].[Address]    Script Date: 2021/10/28 23:04:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Address](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[StreetAddress1] [nvarchar](50) NOT NULL,
	[StreetAddress2] [nvarchar](50) NULL,
	[Suburb] [nvarchar](50) NOT NULL,
	[City] [nvarchar](50) NOT NULL,
	[PostCode] [nvarchar](50) NOT NULL,
	[Country] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Address] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Contacts]    Script Date: 2021/10/28 23:04:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Contacts](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PersonName] [nvarchar](200) NOT NULL,
	[PersonPosition] [nvarchar](200) NOT NULL,
	[ContactNumber] [nvarchar](50) NOT NULL,
	[ContactEmail] [nvarchar](200) NULL,
	[WorkLandlineNumber] [nvarchar](50) NULL,
	[SupplierID] [int] NULL,
	[ContactID] [int] NULL,
 CONSTRAINT [PK_Contacts] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CostCenter]    Script Date: 2021/10/28 23:04:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CostCenter](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[ProfitCenterID] [int] NOT NULL,
	[CostManagerID] [int] NOT NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CostManagers]    Script Date: 2021/10/28 23:04:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CostManagers](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[EmployeePositionID] [int] NOT NULL,
	[UserID] [int] NOT NULL,
 CONSTRAINT [PK_CostManagers] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 2021/10/28 23:04:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CompanyName] [nvarchar](200) NOT NULL,
	[CompanyPrefix] [nvarchar](50) NOT NULL,
	[PaymentMethodID] [int] NOT NULL,
	[AccountNumber] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[PhysicalAddressID] [int] NOT NULL,
	[DeliveryAddressID] [int] NULL,
 CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EmployeePosition]    Script Date: 2021/10/28 23:04:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmployeePosition](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Position] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_EmployeePosition] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Gender]    Script Date: 2021/10/28 23:04:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Gender](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Gender] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Gender] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[IncreaseType]    Script Date: 2021/10/28 23:04:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[IncreaseType](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Type] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_IncreaseType] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Laws]    Script Date: 2021/10/28 23:04:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Laws](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Law] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_Laws] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MaritalStatus]    Script Date: 2021/10/28 23:04:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MaritalStatus](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Status] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_MaritalStatus] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PaymentIntervals]    Script Date: 2021/10/28 23:04:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PaymentIntervals](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Intervals] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_PaymentIntervals] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PaymentMethods]    Script Date: 2021/10/28 23:04:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PaymentMethods](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Type] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_PaymentMethods] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Permission]    Script Date: 2021/10/28 23:04:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Permission](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Page] [nvarchar](50) NOT NULL,
	[Component] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](50) NULL,
 CONSTRAINT [PK_Permission] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PriceIncrease]    Script Date: 2021/10/28 23:04:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PriceIncrease](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Increase] [decimal](18, 2) NOT NULL,
	[Date] [datetime] NOT NULL,
	[Comment] [nvarchar](max) NOT NULL,
	[IncreaseTypeID] [int] NOT NULL,
	[PriceLookUpID] [int] NOT NULL,
 CONSTRAINT [PK_PriceIncrease] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PriceLookup]    Script Date: 2021/10/28 23:04:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PriceLookup](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Price] [decimal](18, 2) NOT NULL,
	[Date] [datetime] NOT NULL,
	[Comment] [nvarchar](max) NULL,
	[SupplierID] [int] NOT NULL,
	[StockID] [int] NOT NULL,
 CONSTRAINT [PK_PriceLookup] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PriceLookupLog]    Script Date: 2021/10/28 23:04:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PriceLookupLog](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Stock] [nvarchar](50) NOT NULL,
	[OldPrice] [decimal](18, 2) NOT NULL,
	[NewPrice] [decimal](18, 2) NOT NULL,
	[Date] [datetime] NOT NULL,
	[Comment] [nvarchar](max) NULL,
	[UOM] [nvarchar](50) NOT NULL,
	[Application] [nvarchar](max) NOT NULL,
	[Supplier] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_PriceLookupLog] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 2021/10/28 23:04:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ProductCode] [nvarchar](50) NOT NULL,
	[ProductName] [nvarchar](200) NOT NULL,
	[CostCenterID] [int] NOT NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductionOrders]    Script Date: 2021/10/28 23:04:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductionOrders](
	[ID] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_ProductionOrders] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductStock]    Script Date: 2021/10/28 23:04:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductStock](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[StockID] [int] NOT NULL,
	[ProductID] [int] NOT NULL,
	[Quantity] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_ProductStock] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProfitCenter]    Script Date: 2021/10/28 23:04:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProfitCenter](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Color] [nvarchar](50) NULL,
 CONSTRAINT [PK_ProfitCenter] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Race]    Script Date: 2021/10/28 23:04:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Race](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Race] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Race] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Recipes]    Script Date: 2021/10/28 23:04:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Recipes](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[StockID] [int] NOT NULL,
	[StockComponentID] [int] NOT NULL,
	[Quantity] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_Recipes] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RolePermissions]    Script Date: 2021/10/28 23:04:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RolePermissions](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[RoleID] [int] NOT NULL,
	[PermissionID] [int] NOT NULL,
 CONSTRAINT [PK_RolePermissions] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RoleTemplate]    Script Date: 2021/10/28 23:04:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RoleTemplate](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Stock]    Script Date: 2021/10/28 23:04:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Stock](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](50) NOT NULL,
	[ProductName] [nvarchar](200) NOT NULL,
	[UOMID] [int] NOT NULL,
	[StockCategoryID] [int] NOT NULL,
	[MinThreshold] [decimal](18, 0) NULL,
	[OrderNr] [nvarchar](50) NULL,
 CONSTRAINT [PK_Stocklist] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StockCategory]    Script Date: 2021/10/28 23:04:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StockCategory](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_StockCategory] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StockLog]    Script Date: 2021/10/28 23:04:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StockLog](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ProductCodeName] [nvarchar](50) NOT NULL,
	[Date] [datetime] NOT NULL,
	[Quantity] [decimal](18, 2) NOT NULL,
	[Price] [decimal](18, 2) NOT NULL,
	[ProfitCenter] [nvarchar](50) NULL,
	[Action] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_StockLog] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StockQuantity]    Script Date: 2021/10/28 23:04:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StockQuantity](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[StockID] [int] NOT NULL,
	[Quantity] [decimal](18, 2) NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[Price] [decimal](18, 2) NOT NULL,
	[DateModified] [datetime] NOT NULL,
	[ProfitCenterID] [int] NOT NULL,
 CONSTRAINT [PK_StockQuantity] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Supplier]    Script Date: 2021/10/28 23:04:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Supplier](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](50) NOT NULL,
	[CompanyName] [nvarchar](200) NOT NULL,
	[CreditLimit] [decimal](18, 2) NOT NULL,
	[CreditTerms] [nvarchar](50) NOT NULL,
	[Discount] [decimal](18, 2) NOT NULL,
	[AddressID] [int] NOT NULL,
	[SupplierCategoryID] [int] NOT NULL,
 CONSTRAINT [PK_Supplier] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SupplierCategory]    Script Date: 2021/10/28 23:04:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SupplierCategory](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_SupplierType] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TypeEmployment]    Script Date: 2021/10/28 23:04:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TypeEmployment](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Type] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_TypeEmployment] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UnitOfMeasurement]    Script Date: 2021/10/28 23:04:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UnitOfMeasurement](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_UnitOfMeasurement] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 2021/10/28 23:04:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Surname] [nvarchar](50) NOT NULL,
	[UserName] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
	[LastActivity] [date] NULL,
	[IsDisabled] [bit] NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserDetails]    Script Date: 2021/10/28 23:04:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserDetails](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[IDNumber] [nvarchar](50) NOT NULL,
	[EmployeeNumber] [nvarchar](50) NOT NULL,
	[ContactNumber] [nvarchar](50) NOT NULL,
	[Birthday] [date] NOT NULL,
	[IncomeTaxNumber] [nvarchar](50) NOT NULL,
	[RaceID] [int] NOT NULL,
	[GenderID] [int] NOT NULL,
	[HomeAddressID] [int] NOT NULL,
	[NextOfKinName] [nvarchar](max) NOT NULL,
	[NextOfKinContactNumber] [nvarchar](50) NOT NULL,
	[BankName] [nvarchar](max) NOT NULL,
	[AccountNumber] [nvarchar](50) NOT NULL,
	[BranchCode] [nvarchar](50) NOT NULL,
	[BaseSalaryPerMonth] [decimal](18, 2) NOT NULL,
	[HourlyRate] [decimal](18, 2) NOT NULL,
	[HighestQualification] [nvarchar](max) NOT NULL,
	[ProfitCenterID] [int] NOT NULL,
	[MaritalStatusID] [int] NOT NULL,
	[NumberOfDependants] [int] NOT NULL,
	[TypeOfEmploymentID] [int] NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[StartDate] [date] NOT NULL,
	[UserID] [int] NOT NULL,
	[EmployeePositionID] [int] NOT NULL,
	[PaymentIntervalsID] [int] NOT NULL,
	[LawsID] [int] NOT NULL,
 CONSTRAINT [PK_UserDetails] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserPermissions]    Script Date: 2021/10/28 23:04:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserPermissions](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PermissionID] [int] NOT NULL,
	[UserID] [int] NOT NULL,
 CONSTRAINT [PK_UserPermissions] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Contacts]  WITH CHECK ADD  CONSTRAINT [FK_Contacts_Customer] FOREIGN KEY([ContactID])
REFERENCES [dbo].[Customer] ([ID])
GO
ALTER TABLE [dbo].[Contacts] CHECK CONSTRAINT [FK_Contacts_Customer]
GO
ALTER TABLE [dbo].[Contacts]  WITH CHECK ADD  CONSTRAINT [FK_Contacts_Supplier] FOREIGN KEY([SupplierID])
REFERENCES [dbo].[Supplier] ([ID])
GO
ALTER TABLE [dbo].[Contacts] CHECK CONSTRAINT [FK_Contacts_Supplier]
GO
ALTER TABLE [dbo].[CostCenter]  WITH CHECK ADD  CONSTRAINT [FK_CostCenter_CostManagers1] FOREIGN KEY([CostManagerID])
REFERENCES [dbo].[CostManagers] ([ID])
GO
ALTER TABLE [dbo].[CostCenter] CHECK CONSTRAINT [FK_CostCenter_CostManagers1]
GO
ALTER TABLE [dbo].[CostCenter]  WITH CHECK ADD  CONSTRAINT [FK_CostCenter_ProfitCenter1] FOREIGN KEY([ProfitCenterID])
REFERENCES [dbo].[ProfitCenter] ([ID])
GO
ALTER TABLE [dbo].[CostCenter] CHECK CONSTRAINT [FK_CostCenter_ProfitCenter1]
GO
ALTER TABLE [dbo].[CostManagers]  WITH CHECK ADD  CONSTRAINT [FK_CostManagers_EmployeePosition] FOREIGN KEY([EmployeePositionID])
REFERENCES [dbo].[EmployeePosition] ([ID])
GO
ALTER TABLE [dbo].[CostManagers] CHECK CONSTRAINT [FK_CostManagers_EmployeePosition]
GO
ALTER TABLE [dbo].[CostManagers]  WITH CHECK ADD  CONSTRAINT [FK_CostManagers_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[CostManagers] CHECK CONSTRAINT [FK_CostManagers_User]
GO
ALTER TABLE [dbo].[Customer]  WITH CHECK ADD  CONSTRAINT [FK_Customer_Address] FOREIGN KEY([PhysicalAddressID])
REFERENCES [dbo].[Address] ([ID])
GO
ALTER TABLE [dbo].[Customer] CHECK CONSTRAINT [FK_Customer_Address]
GO
ALTER TABLE [dbo].[Customer]  WITH CHECK ADD  CONSTRAINT [FK_Customer_Address1] FOREIGN KEY([DeliveryAddressID])
REFERENCES [dbo].[Address] ([ID])
GO
ALTER TABLE [dbo].[Customer] CHECK CONSTRAINT [FK_Customer_Address1]
GO
ALTER TABLE [dbo].[Customer]  WITH CHECK ADD  CONSTRAINT [FK_Customer_PaymentMethods] FOREIGN KEY([PaymentMethodID])
REFERENCES [dbo].[PaymentMethods] ([ID])
GO
ALTER TABLE [dbo].[Customer] CHECK CONSTRAINT [FK_Customer_PaymentMethods]
GO
ALTER TABLE [dbo].[PriceIncrease]  WITH CHECK ADD  CONSTRAINT [FK_PriceIncrease_IncreaseType1] FOREIGN KEY([IncreaseTypeID])
REFERENCES [dbo].[IncreaseType] ([ID])
GO
ALTER TABLE [dbo].[PriceIncrease] CHECK CONSTRAINT [FK_PriceIncrease_IncreaseType1]
GO
ALTER TABLE [dbo].[PriceIncrease]  WITH CHECK ADD  CONSTRAINT [FK_PriceIncrease_PriceLookup] FOREIGN KEY([PriceLookUpID])
REFERENCES [dbo].[PriceLookup] ([ID])
GO
ALTER TABLE [dbo].[PriceIncrease] CHECK CONSTRAINT [FK_PriceIncrease_PriceLookup]
GO
ALTER TABLE [dbo].[PriceLookup]  WITH CHECK ADD  CONSTRAINT [FK_PriceLookup_Stock] FOREIGN KEY([StockID])
REFERENCES [dbo].[Stock] ([ID])
GO
ALTER TABLE [dbo].[PriceLookup] CHECK CONSTRAINT [FK_PriceLookup_Stock]
GO
ALTER TABLE [dbo].[PriceLookup]  WITH CHECK ADD  CONSTRAINT [FK_PriceLookup_Supplier] FOREIGN KEY([SupplierID])
REFERENCES [dbo].[Supplier] ([ID])
GO
ALTER TABLE [dbo].[PriceLookup] CHECK CONSTRAINT [FK_PriceLookup_Supplier]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_CostCenter] FOREIGN KEY([CostCenterID])
REFERENCES [dbo].[CostCenter] ([ID])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_CostCenter]
GO
ALTER TABLE [dbo].[ProductStock]  WITH CHECK ADD  CONSTRAINT [FK_ProductStock_Product] FOREIGN KEY([ProductID])
REFERENCES [dbo].[Product] ([ID])
GO
ALTER TABLE [dbo].[ProductStock] CHECK CONSTRAINT [FK_ProductStock_Product]
GO
ALTER TABLE [dbo].[ProductStock]  WITH CHECK ADD  CONSTRAINT [FK_ProductStock_Stock] FOREIGN KEY([StockID])
REFERENCES [dbo].[Stock] ([ID])
GO
ALTER TABLE [dbo].[ProductStock] CHECK CONSTRAINT [FK_ProductStock_Stock]
GO
ALTER TABLE [dbo].[Recipes]  WITH CHECK ADD  CONSTRAINT [FK_Recipes_Stock] FOREIGN KEY([StockID])
REFERENCES [dbo].[Stock] ([ID])
GO
ALTER TABLE [dbo].[Recipes] CHECK CONSTRAINT [FK_Recipes_Stock]
GO
ALTER TABLE [dbo].[Recipes]  WITH CHECK ADD  CONSTRAINT [FK_Recipes_Stock1] FOREIGN KEY([StockComponentID])
REFERENCES [dbo].[Stock] ([ID])
GO
ALTER TABLE [dbo].[Recipes] CHECK CONSTRAINT [FK_Recipes_Stock1]
GO
ALTER TABLE [dbo].[RolePermissions]  WITH CHECK ADD  CONSTRAINT [FK_RolePermissions_Permission] FOREIGN KEY([PermissionID])
REFERENCES [dbo].[Permission] ([ID])
GO
ALTER TABLE [dbo].[RolePermissions] CHECK CONSTRAINT [FK_RolePermissions_Permission]
GO
ALTER TABLE [dbo].[RolePermissions]  WITH CHECK ADD  CONSTRAINT [FK_RolePermissions_Role] FOREIGN KEY([RoleID])
REFERENCES [dbo].[RoleTemplate] ([ID])
GO
ALTER TABLE [dbo].[RolePermissions] CHECK CONSTRAINT [FK_RolePermissions_Role]
GO
ALTER TABLE [dbo].[Stock]  WITH CHECK ADD  CONSTRAINT [FK_Stock_StockCategory] FOREIGN KEY([StockCategoryID])
REFERENCES [dbo].[StockCategory] ([ID])
GO
ALTER TABLE [dbo].[Stock] CHECK CONSTRAINT [FK_Stock_StockCategory]
GO
ALTER TABLE [dbo].[Stock]  WITH CHECK ADD  CONSTRAINT [FK_Stock_UnitOfMeasurement] FOREIGN KEY([UOMID])
REFERENCES [dbo].[UnitOfMeasurement] ([ID])
GO
ALTER TABLE [dbo].[Stock] CHECK CONSTRAINT [FK_Stock_UnitOfMeasurement]
GO
ALTER TABLE [dbo].[StockQuantity]  WITH CHECK ADD  CONSTRAINT [FK_StockQuantity_ProfitCenter] FOREIGN KEY([ProfitCenterID])
REFERENCES [dbo].[ProfitCenter] ([ID])
GO
ALTER TABLE [dbo].[StockQuantity] CHECK CONSTRAINT [FK_StockQuantity_ProfitCenter]
GO
ALTER TABLE [dbo].[StockQuantity]  WITH CHECK ADD  CONSTRAINT [FK_StockQuantity_Stock] FOREIGN KEY([StockID])
REFERENCES [dbo].[Stock] ([ID])
GO
ALTER TABLE [dbo].[StockQuantity] CHECK CONSTRAINT [FK_StockQuantity_Stock]
GO
ALTER TABLE [dbo].[Supplier]  WITH CHECK ADD  CONSTRAINT [FK_Supplier_Address] FOREIGN KEY([AddressID])
REFERENCES [dbo].[Address] ([ID])
GO
ALTER TABLE [dbo].[Supplier] CHECK CONSTRAINT [FK_Supplier_Address]
GO
ALTER TABLE [dbo].[Supplier]  WITH CHECK ADD  CONSTRAINT [FK_Supplier_SupplierType] FOREIGN KEY([SupplierCategoryID])
REFERENCES [dbo].[SupplierCategory] ([ID])
GO
ALTER TABLE [dbo].[Supplier] CHECK CONSTRAINT [FK_Supplier_SupplierType]
GO
ALTER TABLE [dbo].[UserDetails]  WITH CHECK ADD  CONSTRAINT [FK_UserDetails_Address] FOREIGN KEY([HomeAddressID])
REFERENCES [dbo].[Address] ([ID])
GO
ALTER TABLE [dbo].[UserDetails] CHECK CONSTRAINT [FK_UserDetails_Address]
GO
ALTER TABLE [dbo].[UserDetails]  WITH CHECK ADD  CONSTRAINT [FK_UserDetails_EmployeePosition1] FOREIGN KEY([EmployeePositionID])
REFERENCES [dbo].[EmployeePosition] ([ID])
GO
ALTER TABLE [dbo].[UserDetails] CHECK CONSTRAINT [FK_UserDetails_EmployeePosition1]
GO
ALTER TABLE [dbo].[UserDetails]  WITH CHECK ADD  CONSTRAINT [FK_UserDetails_Gender] FOREIGN KEY([GenderID])
REFERENCES [dbo].[Gender] ([ID])
GO
ALTER TABLE [dbo].[UserDetails] CHECK CONSTRAINT [FK_UserDetails_Gender]
GO
ALTER TABLE [dbo].[UserDetails]  WITH CHECK ADD  CONSTRAINT [FK_UserDetails_Laws] FOREIGN KEY([LawsID])
REFERENCES [dbo].[Laws] ([ID])
GO
ALTER TABLE [dbo].[UserDetails] CHECK CONSTRAINT [FK_UserDetails_Laws]
GO
ALTER TABLE [dbo].[UserDetails]  WITH CHECK ADD  CONSTRAINT [FK_UserDetails_MaritalStatus] FOREIGN KEY([MaritalStatusID])
REFERENCES [dbo].[MaritalStatus] ([ID])
GO
ALTER TABLE [dbo].[UserDetails] CHECK CONSTRAINT [FK_UserDetails_MaritalStatus]
GO
ALTER TABLE [dbo].[UserDetails]  WITH CHECK ADD  CONSTRAINT [FK_UserDetails_PaymentIntervals] FOREIGN KEY([PaymentIntervalsID])
REFERENCES [dbo].[PaymentIntervals] ([ID])
GO
ALTER TABLE [dbo].[UserDetails] CHECK CONSTRAINT [FK_UserDetails_PaymentIntervals]
GO
ALTER TABLE [dbo].[UserDetails]  WITH CHECK ADD  CONSTRAINT [FK_UserDetails_ProfitCenter] FOREIGN KEY([ProfitCenterID])
REFERENCES [dbo].[ProfitCenter] ([ID])
GO
ALTER TABLE [dbo].[UserDetails] CHECK CONSTRAINT [FK_UserDetails_ProfitCenter]
GO
ALTER TABLE [dbo].[UserDetails]  WITH CHECK ADD  CONSTRAINT [FK_UserDetails_Race] FOREIGN KEY([RaceID])
REFERENCES [dbo].[Race] ([ID])
GO
ALTER TABLE [dbo].[UserDetails] CHECK CONSTRAINT [FK_UserDetails_Race]
GO
ALTER TABLE [dbo].[UserDetails]  WITH CHECK ADD  CONSTRAINT [FK_UserDetails_TypeEmployment] FOREIGN KEY([TypeOfEmploymentID])
REFERENCES [dbo].[TypeEmployment] ([ID])
GO
ALTER TABLE [dbo].[UserDetails] CHECK CONSTRAINT [FK_UserDetails_TypeEmployment]
GO
ALTER TABLE [dbo].[UserDetails]  WITH CHECK ADD  CONSTRAINT [FK_UserDetails_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[UserDetails] CHECK CONSTRAINT [FK_UserDetails_User]
GO
ALTER TABLE [dbo].[UserPermissions]  WITH CHECK ADD  CONSTRAINT [FK_UserPermissions_Permission] FOREIGN KEY([PermissionID])
REFERENCES [dbo].[Permission] ([ID])
GO
ALTER TABLE [dbo].[UserPermissions] CHECK CONSTRAINT [FK_UserPermissions_Permission]
GO
ALTER TABLE [dbo].[UserPermissions]  WITH CHECK ADD  CONSTRAINT [FK_UserPermissions_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[UserPermissions] CHECK CONSTRAINT [FK_UserPermissions_User]
GO
USE [master]
GO
ALTER DATABASE [AIS] SET  READ_WRITE 
GO
