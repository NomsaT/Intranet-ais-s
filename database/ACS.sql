USE [AIS]
GO
/****** Object:  Table [dbo].[Address]    Script Date: 2023/01/18 11:49:47 ******/
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
	[CountryID] [int] NOT NULL,
 CONSTRAINT [PK_Address] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BankName]    Script Date: 2023/01/18 11:49:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BankName](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[BankName] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_BankName] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BarcodeTemp]    Script Date: 2023/01/18 11:49:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BarcodeTemp](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Barcode] [nvarchar](50) NOT NULL,
	[Template] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_BarcodeTemp] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Bin]    Script Date: 2023/01/18 11:49:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bin](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[StoreID] [int] NOT NULL,
 CONSTRAINT [PK_Bin] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Company]    Script Date: 2023/01/18 11:49:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Company](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[VatNr] [nvarchar](50) NOT NULL,
	[RegNr] [nvarchar](50) NOT NULL,
	[AddressID] [int] NOT NULL,
	[MotherCompany] [bit] NULL,
 CONSTRAINT [PK_Company] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Contacts]    Script Date: 2023/01/18 11:49:47 ******/
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
/****** Object:  Table [dbo].[CostType]    Script Date: 2023/01/18 11:49:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CostType](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Abbreviation] [nvarchar](50) NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_CostType] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Country]    Script Date: 2023/01/18 11:49:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Country](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Country] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Currency]    Script Date: 2023/01/18 11:49:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Currency](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ISO] [nvarchar](50) NULL,
	[CurrencyName] [nvarchar](50) NOT NULL,
	[CurrencyValue] [decimal](18, 2) NULL,
 CONSTRAINT [PK_Currency] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 2023/01/18 11:49:47 ******/
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
	[DeliveryAddressID] [int] NOT NULL,
	[DifferentDelivery] [bit] NOT NULL,
 CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DBVersion]    Script Date: 2023/01/18 11:49:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DBVersion](
	[Version] [nvarchar](50) NOT NULL,
	[DateTime] [datetime] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Department]    Script Date: 2023/01/18 11:49:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Department](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Abbreviation] [nvarchar](50) NULL,
	[Color] [nvarchar](50) NULL,
	[CostTypeID] [int] NOT NULL,
	[GeneralPurchasing] [bit] NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DepartmentManagers]    Script Date: 2023/01/18 11:49:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DepartmentManagers](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[DepartmentID] [int] NOT NULL,
 CONSTRAINT [PK_CostManagers] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DepartmentUsers]    Script Date: 2023/01/18 11:49:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DepartmentUsers](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[DepartmentID] [int] NOT NULL,
	[Percentage] [int] NOT NULL,
 CONSTRAINT [PK_ProfitCenterUsers] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Discrepency]    Script Date: 2023/01/18 11:49:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Discrepency](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_Discrepency] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EmployeePosition]    Script Date: 2023/01/18 11:49:47 ******/
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
/****** Object:  Table [dbo].[Gender]    Script Date: 2023/01/18 11:49:47 ******/
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
/****** Object:  Table [dbo].[GLCode]    Script Date: 2023/01/18 11:49:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GLCode](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[AssignVat] [bit] NULL,
 CONSTRAINT [PK_GLCode] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GRN]    Script Date: 2023/01/18 11:49:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GRN](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[GrnNumber] [nvarchar](50) NOT NULL,
	[Total] [decimal](12, 2) NOT NULL,
	[InternalOrderID] [int] NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[CapturerID] [int] NULL,
	[Comment] [nvarchar](max) NULL,
	[EditorID] [int] NULL,
 CONSTRAINT [PK_GRN] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GRNItem]    Script Date: 2023/01/18 11:49:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GRNItem](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Quantity] [int] NOT NULL,
	[GrnID] [int] NOT NULL,
	[InternalOrderItemID] [int] NOT NULL,
	[StoreLocationID] [int] NULL,
	[Comment] [nvarchar](max) NULL,
 CONSTRAINT [PK_GRNItem] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GRNOnceOffItems]    Script Date: 2023/01/18 11:49:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GRNOnceOffItems](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Quantity] [int] NOT NULL,
	[GrnID] [int] NOT NULL,
	[OnceOffItemsID] [int] NOT NULL,
	[Comment] [nvarchar](max) NULL,
 CONSTRAINT [PK_GRNOnceOffItems] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[IncreaseType]    Script Date: 2023/01/18 11:49:47 ******/
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
/****** Object:  Table [dbo].[InternalOrder]    Script Date: 2023/01/18 11:49:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InternalOrder](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[RequestedByID] [int] NOT NULL,
	[PlacedByID] [int] NOT NULL,
	[QuotationNumber] [nvarchar](50) NULL,
	[SupplierID] [int] NULL,
	[ApproveByID] [int] NULL,
	[Comment] [nvarchar](max) NULL,
	[FollowUpDate] [datetime] NULL,
	[DateCreated] [datetime] NULL,
	[DeliveryDate] [datetime] NULL,
	[Total] [decimal](12, 2) NULL,
	[StatusID] [int] NOT NULL,
	[DateApproved] [datetime] NULL,
	[InternalComment] [nvarchar](max) NULL,
	[PlantLocationID] [int] NULL,
	[SupplierComment] [nvarchar](max) NULL,
	[Vat] [decimal](12, 2) NULL,
	[ProjectID] [int] NULL,
	[CompanyID] [int] NULL,
 CONSTRAINT [PK_InternalOrder] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[InternalOrderItem]    Script Date: 2023/01/18 11:49:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InternalOrderItem](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[InternalOrderID] [int] NOT NULL,
	[StockID] [int] NOT NULL,
	[OriginalValue] [decimal](12, 2) NOT NULL,
	[Value] [decimal](12, 2) NOT NULL,
	[Quantity] [int] NOT NULL,
	[Total] [decimal](12, 2) NOT NULL,
	[GLCodeID] [int] NULL,
	[DepartmentID] [int] NOT NULL,
	[UOMID] [int] NOT NULL,
	[PackSize] [int] NOT NULL,
	[VatAppl] [bit] NULL,
	[GrnAppl] [bit] NULL,
	[UomPrice] [decimal](12, 2) NULL,
 CONSTRAINT [PK_InternalOrderItem] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[InternalOrderStatus]    Script Date: 2023/01/18 11:49:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InternalOrderStatus](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_InternalOrderStatus] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Invoice]    Script Date: 2023/01/18 11:49:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Invoice](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[InvoiceNumber] [nvarchar](50) NOT NULL,
	[Total] [decimal](12, 2) NOT NULL,
	[InternalOrderID] [int] NOT NULL,
	[DateCreated] [datetime] NOT NULL,
 CONSTRAINT [PK_Invoice] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[InvoiceItem]    Script Date: 2023/01/18 11:49:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InvoiceItem](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Quantity] [int] NOT NULL,
	[ItemValue] [decimal](12, 2) NOT NULL,
	[InvoiceID] [int] NOT NULL,
	[GRNItemID] [int] NOT NULL,
	[InvoicePrice] [decimal](12, 2) NOT NULL,
 CONSTRAINT [PK_InvoiceItem] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[InvoiceOnceOffItems]    Script Date: 2023/01/18 11:49:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InvoiceOnceOffItems](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Quantity] [int] NOT NULL,
	[ItemValue] [decimal](12, 2) NOT NULL,
	[InvoiceID] [int] NOT NULL,
	[GRNOnceOffItemID] [int] NOT NULL,
	[InvoicePrice] [decimal](12, 2) NULL,
 CONSTRAINT [PK_InvoiceOnceOffItems] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[InvoiceServiceItems]    Script Date: 2023/01/18 11:49:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InvoiceServiceItems](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Quantity] [int] NOT NULL,
	[ItemValue] [decimal](12, 2) NOT NULL,
	[InvoiceID] [int] NOT NULL,
	[ServiceID] [int] NOT NULL,
	[InvoicePrice] [decimal](12, 2) NOT NULL,
 CONSTRAINT [PK_InvoiceServiceItems] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LatestGrn]    Script Date: 2023/01/18 11:49:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LatestGrn](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[InternalOrderID] [int] NOT NULL,
	[GrnIncrement] [int] NOT NULL,
 CONSTRAINT [PK_LatestGrn] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Laws]    Script Date: 2023/01/18 11:49:47 ******/
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
/****** Object:  Table [dbo].[MaritalStatus]    Script Date: 2023/01/18 11:49:47 ******/
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
/****** Object:  Table [dbo].[OnceOffItems]    Script Date: 2023/01/18 11:49:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OnceOffItems](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](50) NULL,
	[InternalOrderID] [int] NOT NULL,
	[Value] [decimal](12, 2) NOT NULL,
	[Quantity] [int] NOT NULL,
	[Total] [decimal](12, 2) NULL,
	[DepartmentID] [int] NOT NULL,
	[UOMID] [int] NULL,
	[PackSize] [int] NOT NULL,
	[VatAppl] [bit] NULL,
	[GrnAppl] [bit] NULL,
	[GLCodeID] [int] NULL,
	[Code] [nvarchar](50) NULL,
 CONSTRAINT [PK_OnceOffItems] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PaymentIntervals]    Script Date: 2023/01/18 11:49:47 ******/
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
/****** Object:  Table [dbo].[PaymentMethods]    Script Date: 2023/01/18 11:49:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PaymentMethods](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Type] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[RequireBankingDetails] [bit] NULL,
 CONSTRAINT [PK_PaymentMethods] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Permission]    Script Date: 2023/01/18 11:49:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Permission](
	[ID] [int] NOT NULL,
	[Page] [nvarchar](50) NOT NULL,
	[Component] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_Permission] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PlantLocation]    Script Date: 2023/01/18 11:49:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PlantLocation](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[AddressID] [int] NOT NULL,
	[DefaultStoreID] [int] NULL,
 CONSTRAINT [PK_PlantLocation] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PriceIncrease]    Script Date: 2023/01/18 11:49:47 ******/
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
	[IncreaseOrigin] [nvarchar](50) NULL,
 CONSTRAINT [PK_PriceIncrease] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PriceLookup]    Script Date: 2023/01/18 11:49:47 ******/
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
/****** Object:  Table [dbo].[PriceLookupLog]    Script Date: 2023/01/18 11:49:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PriceLookupLog](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Stock] [nvarchar](max) NOT NULL,
	[OldPrice] [decimal](18, 2) NOT NULL,
	[NewPrice] [decimal](18, 2) NOT NULL,
	[Date] [datetime] NOT NULL,
	[Comment] [nvarchar](max) NULL,
	[UOM] [nvarchar](50) NOT NULL,
	[Application] [nvarchar](max) NOT NULL,
	[Supplier] [nvarchar](50) NOT NULL,
	[Username] [nvarchar](max) NOT NULL,
	[InternalProductName] [nvarchar](max) NOT NULL,
	[Currency] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_PriceLookupLog] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Printers]    Script Date: 2023/01/18 11:49:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Printers](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Location] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Printers] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 2023/01/18 11:49:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ProductCode] [nvarchar](50) NOT NULL,
	[ProductName] [nvarchar](200) NOT NULL,
	[DepartmentID] [int] NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Quantity] [decimal](18, 2) NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductionOrders]    Script Date: 2023/01/18 11:49:47 ******/
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
/****** Object:  Table [dbo].[ProductItem]    Script Date: 2023/01/18 11:49:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductItem](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ProductID] [int] NOT NULL,
	[ItemID] [int] NOT NULL,
	[Quantity] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_ProductItem] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductStock]    Script Date: 2023/01/18 11:49:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductStock](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[StockID] [int] NOT NULL,
	[ProductID] [int] NOT NULL,
	[Quantity] [decimal](18, 2) NOT NULL,
	[QtyPerSquareMeter] [decimal](18, 2) NOT NULL,
	[SquaresUsed] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_ProductStock] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Project]    Script Date: 2023/01/18 11:49:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Project](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[StartDate] [datetime] NOT NULL,
	[EndDate] [datetime] NOT NULL,
	[ProjectStatusID] [int] NOT NULL,
	[Budget] [decimal](12, 2) NOT NULL,
 CONSTRAINT [PK_Project] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProjectStatus]    Script Date: 2023/01/18 11:49:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectStatus](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Status] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_ProjectStatus] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Quote]    Script Date: 2023/01/18 11:49:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Quote](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[QuoteStatusID] [int] NULL,
	[RequestByID] [int] NULL,
	[PlacedByID] [int] NULL,
	[SubmissionDate] [datetime] NULL,
	[ValidFor] [int] NULL,
	[DaysFrom] [int] NULL,
	[OnOrder] [decimal](12, 2) NULL,
	[OnDelivery] [decimal](12, 2) NULL,
	[CustomerID] [int] NOT NULL,
 CONSTRAINT [PK_Quote] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[QuoteItem]    Script Date: 2023/01/18 11:49:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QuoteItem](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ProductID] [int] NULL,
	[Width] [decimal](12, 2) NULL,
	[Length] [decimal](12, 2) NULL,
	[Price] [decimal](12, 2) NULL,
	[Quantity] [int] NULL,
	[QuoteID] [int] NOT NULL,
 CONSTRAINT [PK_QuoteItem] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[QuoteRevision]    Script Date: 2023/01/18 11:49:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QuoteRevision](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[QuoteID] [int] NOT NULL,
	[RevisionNr] [int] NOT NULL,
 CONSTRAINT [PK_QuoteRevision] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[QuoteStatus]    Script Date: 2023/01/18 11:49:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QuoteStatus](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_QuoteStatus] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[QuoteTransport]    Script Date: 2023/01/18 11:49:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QuoteTransport](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Price] [decimal](12, 2) NULL,
	[Quantity] [int] NULL,
	[QuoteID] [int] NOT NULL,
 CONSTRAINT [PK_QuoteTransport] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Race]    Script Date: 2023/01/18 11:49:47 ******/
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
/****** Object:  Table [dbo].[Recipes]    Script Date: 2023/01/18 11:49:47 ******/
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
/****** Object:  Table [dbo].[ReturnToSupplier]    Script Date: 2023/01/18 11:49:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ReturnToSupplier](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](50) NOT NULL,
	[ProductName] [nvarchar](200) NOT NULL,
	[InternalSku] [nvarchar](50) NOT NULL,
	[InternalProductName] [nvarchar](50) NOT NULL,
	[ReturnedQuantity] [decimal](18, 2) NOT NULL,
	[SupplierName] [nvarchar](200) NOT NULL,
	[DateReturned] [datetime] NOT NULL,
	[Price] [decimal](18, 2) NULL,
	[LogEntry] [bit] NULL,
 CONSTRAINT [PK_ReturnToSupplier] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RolePermissions]    Script Date: 2023/01/18 11:49:47 ******/
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
/****** Object:  Table [dbo].[RoleTemplate]    Script Date: 2023/01/18 11:49:47 ******/
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
/****** Object:  Table [dbo].[ScannerConfig]    Script Date: 2023/01/18 11:49:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ScannerConfig](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[DeviceId] [nvarchar](max) NOT NULL,
	[DeviceName] [nvarchar](max) NOT NULL,
	[PlantLocationID] [int] NOT NULL,
 CONSTRAINT [PK_ScannerConfig] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Services]    Script Date: 2023/01/18 11:49:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Services](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[InternalOrderID] [int] NOT NULL,
	[Value] [decimal](12, 2) NOT NULL,
	[VatAppl] [bit] NULL,
	[GrnAppl] [bit] NULL,
	[GLCodeID] [int] NULL,
	[DepartmentID] [int] NOT NULL,
	[Quantity] [int] NULL,
	[Code] [nvarchar](50) NULL,
 CONSTRAINT [PK_Services] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ShelfLifeType]    Script Date: 2023/01/18 11:49:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ShelfLifeType](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Type] [nvarchar](50) NOT NULL,
	[Days] [int] NOT NULL,
 CONSTRAINT [PK_ShelfLifeType] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Stock]    Script Date: 2023/01/18 11:49:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Stock](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](50) NOT NULL,
	[ProductName] [nvarchar](200) NOT NULL,
	[InternalSku] [nvarchar](50) NOT NULL,
	[InternalProductName] [nvarchar](50) NULL,
	[UOMID] [int] NOT NULL,
	[StockCategoryID] [int] NOT NULL,
	[MinThreshold] [decimal](18, 0) NULL,
	[StockGroupID] [int] NULL,
	[Monitored] [bit] NOT NULL,
	[SupplierID] [int] NOT NULL,
	[CurrentPrice] [decimal](18, 2) NOT NULL,
	[DefaultDepartmentID] [int] NOT NULL,
	[DefaultLocationID] [int] NOT NULL,
	[DefaultStoreID] [int] NOT NULL,
	[PackSize] [int] NOT NULL,
	[PackQuantity] [decimal](18, 2) NOT NULL,
	[ItemQuantity] [decimal](18, 2) NOT NULL,
	[ItemPrice] [decimal](18, 2) NOT NULL,
	[SecondaryUOMID] [int] NULL,
	[ComparisonSecondValue] [decimal](18, 4) NULL,
	[CalculatedRatio] [decimal](20, 10) NULL,
	[DeductedValue] [decimal](18, 4) NULL,
	[ShippingDimensionsHeight] [decimal](18, 2) NULL,
	[ProductWeight] [decimal](18, 2) NULL,
	[ProductDimensionsLength] [decimal](18, 2) NULL,
	[ProductDimensionsWidth] [decimal](18, 2) NULL,
	[ProductDimensionsHeight] [decimal](18, 2) NULL,
	[ShippingWeight] [decimal](18, 2) NULL,
	[ShippingDimensionsLength] [decimal](18, 2) NULL,
	[ShippingDimensionsWidth] [decimal](18, 2) NULL,
	[StorageTypeID] [int] NULL,
	[ShelfLifeDays] [int] NULL,
	[ShelfLifeTypeID] [int] NULL,
	[BinID] [int] NULL,
 CONSTRAINT [PK_Stocklist] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StockCategory]    Script Date: 2023/01/18 11:49:47 ******/
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
/****** Object:  Table [dbo].[StockGroup]    Script Date: 2023/01/18 11:49:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StockGroup](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[MinThreshold] [decimal](18, 0) NULL,
 CONSTRAINT [PK_StockGroup] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StockLog]    Script Date: 2023/01/18 11:49:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StockLog](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ProductCodeName] [nvarchar](max) NOT NULL,
	[Date] [datetime] NOT NULL,
	[Quantity] [decimal](18, 2) NOT NULL,
	[Price] [decimal](18, 2) NOT NULL,
	[Department] [nvarchar](50) NULL,
	[Action] [nvarchar](50) NOT NULL,
	[Location] [nvarchar](50) NULL,
	[Store] [nvarchar](50) NULL,
	[StockCategory] [nvarchar](50) NULL,
	[StockGroup] [nvarchar](50) NULL,
	[UOM] [nvarchar](50) NULL,
	[Supplier] [nvarchar](max) NULL,
	[InternalProductName] [nvarchar](max) NULL,
	[SupplierCurrency] [nvarchar](50) NULL,
 CONSTRAINT [PK_StockLog] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StockQuantity]    Script Date: 2023/01/18 11:49:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StockQuantity](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[StockID] [int] NOT NULL,
	[ItemQuantity] [decimal](18, 2) NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[Price] [decimal](18, 2) NOT NULL,
	[DateModified] [datetime] NOT NULL,
	[DepartmentID] [int] NOT NULL,
	[LocationID] [int] NOT NULL,
	[StoreID] [int] NOT NULL,
	[BarcodePrinted] [bit] NULL,
	[VerificationScan] [bit] NULL,
	[Splitted] [bit] NULL,
	[GrnNumber] [nvarchar](50) NULL,
 CONSTRAINT [PK_StockQuantity] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Stocktake]    Script Date: 2023/01/18 11:49:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Stocktake](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[StockID] [int] NOT NULL,
	[PlantLocationID] [int] NOT NULL,
	[StoreID] [int] NOT NULL,
	[CurrentQty] [decimal](18, 2) NOT NULL,
	[CapturedQty] [decimal](18, 2) NOT NULL,
	[Recount] [bit] NULL,
	[StockTakeDate] [datetime] NULL,
	[UserID] [int] NULL,
 CONSTRAINT [PK_Stocktake] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StocktakeCounter]    Script Date: 2023/01/18 11:49:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StocktakeCounter](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CycleCounter] [int] NULL,
	[UnScheduledCounter] [int] NULL,
 CONSTRAINT [PK_StocktakeCounter] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StocktakeCycle]    Script Date: 2023/01/18 11:49:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StocktakeCycle](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[StocktakeName] [nvarchar](max) NOT NULL,
	[StartDate] [datetime] NOT NULL,
	[EndDate] [datetime] NOT NULL,
 CONSTRAINT [PK_StocktakeCycle] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StocktakeLog]    Script Date: 2023/01/18 11:49:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StocktakeLog](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[StockFullName] [nvarchar](max) NOT NULL,
	[PlantLocationName] [nvarchar](max) NOT NULL,
	[StoreName] [nvarchar](max) NOT NULL,
	[CurrentQty] [decimal](18, 2) NOT NULL,
	[CountQty] [decimal](18, 2) NOT NULL,
	[Recount] [bit] NOT NULL,
	[StockTakeDate] [datetime] NOT NULL,
	[RecountDate] [datetime] NULL,
	[ApproveDate] [datetime] NULL,
	[Actions] [nvarchar](50) NULL,
	[UserFullName] [nvarchar](max) NULL,
 CONSTRAINT [PK_StocktakeLog] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StocktakeReport]    Script Date: 2023/01/18 11:49:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StocktakeReport](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[StocktakeCycleID] [int] NOT NULL,
	[StockFullName] [nvarchar](max) NOT NULL,
	[PlantLocationName] [nvarchar](max) NOT NULL,
	[StoreName] [nvarchar](max) NOT NULL,
	[OpeningQty] [decimal](18, 2) NOT NULL,
	[ClosingQty] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_StocktakeReport] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StorageType]    Script Date: 2023/01/18 11:49:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StorageType](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[RequireBarcode] [bit] NOT NULL,
 CONSTRAINT [PK_StorageType] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Store]    Script Date: 2023/01/18 11:49:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Store](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[PlantLocationID] [int] NOT NULL,
	[StoreTypeID] [int] NOT NULL,
	[Quarantine] [bit] NULL,
	[ProductionStore] [bit] NULL,
 CONSTRAINT [PK_Store] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StoreType]    Script Date: 2023/01/18 11:49:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StoreType](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_StoreType] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Supplier]    Script Date: 2023/01/18 11:49:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Supplier](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CompanyName] [nvarchar](200) NOT NULL,
	[CreditLimit] [decimal](18, 2) NOT NULL,
	[CreditTerms] [nvarchar](50) NOT NULL,
	[Discount] [decimal](18, 2) NOT NULL,
	[AddressID] [int] NOT NULL,
	[Comment] [nvarchar](max) NULL,
	[BankNameID] [int] NULL,
	[AccountNumber] [nvarchar](50) NULL,
	[BranchCode] [nvarchar](50) NULL,
	[PaymentMethodID] [int] NULL,
	[CurrencyID] [int] NOT NULL,
 CONSTRAINT [PK_Supplier] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TypeEmployment]    Script Date: 2023/01/18 11:49:47 ******/
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
/****** Object:  Table [dbo].[UnitOfMeasurement]    Script Date: 2023/01/18 11:49:47 ******/
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
/****** Object:  Table [dbo].[User]    Script Date: 2023/01/18 11:49:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Surname] [nvarchar](50) NOT NULL,
	[UserName] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NULL,
	[LastActivity] [date] NULL,
	[IsDisabled] [bit] NOT NULL,
	[ResetDateTime] [datetime] NULL,
	[SendEmail] [bit] NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserDetails]    Script Date: 2023/01/18 11:49:47 ******/
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
	[IncomeTaxNumber] [nvarchar](50) NULL,
	[RaceID] [int] NOT NULL,
	[GenderID] [int] NOT NULL,
	[HomeAddressID] [int] NOT NULL,
	[NextOfKinName] [nvarchar](max) NOT NULL,
	[NextOfKinContactNumber] [nvarchar](50) NOT NULL,
	[BankNameID] [int] NOT NULL,
	[AccountNumber] [nvarchar](50) NOT NULL,
	[BranchCode] [nvarchar](50) NOT NULL,
	[BaseSalaryPerMonth] [decimal](18, 2) NOT NULL,
	[HourlyRate] [decimal](18, 2) NOT NULL,
	[HighestQualification] [nvarchar](max) NOT NULL,
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
/****** Object:  Table [dbo].[UserPermissions]    Script Date: 2023/01/18 11:49:47 ******/
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
/****** Object:  Table [dbo].[VAT]    Script Date: 2023/01/18 11:49:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VAT](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Vat] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_VAT] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[VatStored]    Script Date: 2023/01/18 11:49:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VatStored](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[GLCodeID] [int] NOT NULL,
	[VatAmount] [decimal](12, 2) NOT NULL,
 CONSTRAINT [PK_VatStored] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Address]  WITH CHECK ADD  CONSTRAINT [FK_Address_Country] FOREIGN KEY([CountryID])
REFERENCES [dbo].[Country] ([ID])
GO
ALTER TABLE [dbo].[Address] CHECK CONSTRAINT [FK_Address_Country]
GO
ALTER TABLE [dbo].[Bin]  WITH CHECK ADD  CONSTRAINT [FK_Bin_Store] FOREIGN KEY([StoreID])
REFERENCES [dbo].[Store] ([ID])
GO
ALTER TABLE [dbo].[Bin] CHECK CONSTRAINT [FK_Bin_Store]
GO
ALTER TABLE [dbo].[Company]  WITH CHECK ADD  CONSTRAINT [FK_Company_Address] FOREIGN KEY([AddressID])
REFERENCES [dbo].[Address] ([ID])
GO
ALTER TABLE [dbo].[Company] CHECK CONSTRAINT [FK_Company_Address]
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
ALTER TABLE [dbo].[Department]  WITH CHECK ADD  CONSTRAINT [FK_Department_CostType] FOREIGN KEY([CostTypeID])
REFERENCES [dbo].[CostType] ([ID])
GO
ALTER TABLE [dbo].[Department] CHECK CONSTRAINT [FK_Department_CostType]
GO
ALTER TABLE [dbo].[DepartmentManagers]  WITH CHECK ADD  CONSTRAINT [FK_DepartmentManagers_Department] FOREIGN KEY([DepartmentID])
REFERENCES [dbo].[Department] ([ID])
GO
ALTER TABLE [dbo].[DepartmentManagers] CHECK CONSTRAINT [FK_DepartmentManagers_Department]
GO
ALTER TABLE [dbo].[DepartmentManagers]  WITH CHECK ADD  CONSTRAINT [FK_DepartmentManagers_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[DepartmentManagers] CHECK CONSTRAINT [FK_DepartmentManagers_User]
GO
ALTER TABLE [dbo].[DepartmentUsers]  WITH CHECK ADD  CONSTRAINT [FK_ProfitCenterUsers_Department] FOREIGN KEY([DepartmentID])
REFERENCES [dbo].[Department] ([ID])
GO
ALTER TABLE [dbo].[DepartmentUsers] CHECK CONSTRAINT [FK_ProfitCenterUsers_Department]
GO
ALTER TABLE [dbo].[DepartmentUsers]  WITH CHECK ADD  CONSTRAINT [FK_ProfitCenterUsers_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[DepartmentUsers] CHECK CONSTRAINT [FK_ProfitCenterUsers_User]
GO
ALTER TABLE [dbo].[GRN]  WITH CHECK ADD  CONSTRAINT [FK_GRN_InternalOrder] FOREIGN KEY([InternalOrderID])
REFERENCES [dbo].[InternalOrder] ([ID])
GO
ALTER TABLE [dbo].[GRN] CHECK CONSTRAINT [FK_GRN_InternalOrder]
GO
ALTER TABLE [dbo].[GRN]  WITH CHECK ADD  CONSTRAINT [FK_GRN_User] FOREIGN KEY([CapturerID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[GRN] CHECK CONSTRAINT [FK_GRN_User]
GO
ALTER TABLE [dbo].[GRN]  WITH CHECK ADD  CONSTRAINT [FK_GRN_User1] FOREIGN KEY([EditorID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[GRN] CHECK CONSTRAINT [FK_GRN_User1]
GO
ALTER TABLE [dbo].[GRNItem]  WITH CHECK ADD  CONSTRAINT [FK_GRNItem_GRN] FOREIGN KEY([GrnID])
REFERENCES [dbo].[GRN] ([ID])
GO
ALTER TABLE [dbo].[GRNItem] CHECK CONSTRAINT [FK_GRNItem_GRN]
GO
ALTER TABLE [dbo].[GRNItem]  WITH CHECK ADD  CONSTRAINT [FK_GRNItem_InternalOrderItem] FOREIGN KEY([InternalOrderItemID])
REFERENCES [dbo].[InternalOrderItem] ([ID])
GO
ALTER TABLE [dbo].[GRNItem] CHECK CONSTRAINT [FK_GRNItem_InternalOrderItem]
GO
ALTER TABLE [dbo].[GRNItem]  WITH CHECK ADD  CONSTRAINT [FK_GRNItem_Store] FOREIGN KEY([StoreLocationID])
REFERENCES [dbo].[Store] ([ID])
GO
ALTER TABLE [dbo].[GRNItem] CHECK CONSTRAINT [FK_GRNItem_Store]
GO
ALTER TABLE [dbo].[GRNOnceOffItems]  WITH CHECK ADD  CONSTRAINT [FK_GRNOnceOffItems_GRN1] FOREIGN KEY([GrnID])
REFERENCES [dbo].[GRN] ([ID])
GO
ALTER TABLE [dbo].[GRNOnceOffItems] CHECK CONSTRAINT [FK_GRNOnceOffItems_GRN1]
GO
ALTER TABLE [dbo].[GRNOnceOffItems]  WITH CHECK ADD  CONSTRAINT [FK_GRNOnceOffItems_OnceOffItems] FOREIGN KEY([OnceOffItemsID])
REFERENCES [dbo].[OnceOffItems] ([ID])
GO
ALTER TABLE [dbo].[GRNOnceOffItems] CHECK CONSTRAINT [FK_GRNOnceOffItems_OnceOffItems]
GO
ALTER TABLE [dbo].[InternalOrder]  WITH CHECK ADD  CONSTRAINT [FK_InternalOrder_Company] FOREIGN KEY([CompanyID])
REFERENCES [dbo].[Company] ([ID])
GO
ALTER TABLE [dbo].[InternalOrder] CHECK CONSTRAINT [FK_InternalOrder_Company]
GO
ALTER TABLE [dbo].[InternalOrder]  WITH CHECK ADD  CONSTRAINT [FK_InternalOrder_InternalOrderStatus] FOREIGN KEY([StatusID])
REFERENCES [dbo].[InternalOrderStatus] ([ID])
GO
ALTER TABLE [dbo].[InternalOrder] CHECK CONSTRAINT [FK_InternalOrder_InternalOrderStatus]
GO
ALTER TABLE [dbo].[InternalOrder]  WITH CHECK ADD  CONSTRAINT [FK_InternalOrder_PlantLocation] FOREIGN KEY([PlantLocationID])
REFERENCES [dbo].[PlantLocation] ([ID])
GO
ALTER TABLE [dbo].[InternalOrder] CHECK CONSTRAINT [FK_InternalOrder_PlantLocation]
GO
ALTER TABLE [dbo].[InternalOrder]  WITH CHECK ADD  CONSTRAINT [FK_InternalOrder_Project] FOREIGN KEY([ProjectID])
REFERENCES [dbo].[Project] ([ID])
GO
ALTER TABLE [dbo].[InternalOrder] CHECK CONSTRAINT [FK_InternalOrder_Project]
GO
ALTER TABLE [dbo].[InternalOrder]  WITH CHECK ADD  CONSTRAINT [FK_InternalOrder_Supplier] FOREIGN KEY([SupplierID])
REFERENCES [dbo].[Supplier] ([ID])
GO
ALTER TABLE [dbo].[InternalOrder] CHECK CONSTRAINT [FK_InternalOrder_Supplier]
GO
ALTER TABLE [dbo].[InternalOrder]  WITH CHECK ADD  CONSTRAINT [FK_InternalOrder_User] FOREIGN KEY([RequestedByID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[InternalOrder] CHECK CONSTRAINT [FK_InternalOrder_User]
GO
ALTER TABLE [dbo].[InternalOrder]  WITH CHECK ADD  CONSTRAINT [FK_InternalOrder_User1] FOREIGN KEY([PlacedByID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[InternalOrder] CHECK CONSTRAINT [FK_InternalOrder_User1]
GO
ALTER TABLE [dbo].[InternalOrder]  WITH CHECK ADD  CONSTRAINT [FK_InternalOrder_User2] FOREIGN KEY([ApproveByID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[InternalOrder] CHECK CONSTRAINT [FK_InternalOrder_User2]
GO
ALTER TABLE [dbo].[InternalOrderItem]  WITH CHECK ADD  CONSTRAINT [FK_InternalOrderItem_Department] FOREIGN KEY([DepartmentID])
REFERENCES [dbo].[Department] ([ID])
GO
ALTER TABLE [dbo].[InternalOrderItem] CHECK CONSTRAINT [FK_InternalOrderItem_Department]
GO
ALTER TABLE [dbo].[InternalOrderItem]  WITH CHECK ADD  CONSTRAINT [FK_InternalOrderItem_GLCode] FOREIGN KEY([GLCodeID])
REFERENCES [dbo].[GLCode] ([ID])
GO
ALTER TABLE [dbo].[InternalOrderItem] CHECK CONSTRAINT [FK_InternalOrderItem_GLCode]
GO
ALTER TABLE [dbo].[InternalOrderItem]  WITH CHECK ADD  CONSTRAINT [FK_InternalOrderItem_InternalOrder] FOREIGN KEY([InternalOrderID])
REFERENCES [dbo].[InternalOrder] ([ID])
GO
ALTER TABLE [dbo].[InternalOrderItem] CHECK CONSTRAINT [FK_InternalOrderItem_InternalOrder]
GO
ALTER TABLE [dbo].[InternalOrderItem]  WITH CHECK ADD  CONSTRAINT [FK_InternalOrderItem_Stock] FOREIGN KEY([StockID])
REFERENCES [dbo].[Stock] ([ID])
GO
ALTER TABLE [dbo].[InternalOrderItem] CHECK CONSTRAINT [FK_InternalOrderItem_Stock]
GO
ALTER TABLE [dbo].[InternalOrderItem]  WITH CHECK ADD  CONSTRAINT [FK_InternalOrderItem_UnitOfMeasurement] FOREIGN KEY([UOMID])
REFERENCES [dbo].[UnitOfMeasurement] ([ID])
GO
ALTER TABLE [dbo].[InternalOrderItem] CHECK CONSTRAINT [FK_InternalOrderItem_UnitOfMeasurement]
GO
ALTER TABLE [dbo].[Invoice]  WITH CHECK ADD  CONSTRAINT [FK_Invoice_InternalOrder] FOREIGN KEY([InternalOrderID])
REFERENCES [dbo].[InternalOrder] ([ID])
GO
ALTER TABLE [dbo].[Invoice] CHECK CONSTRAINT [FK_Invoice_InternalOrder]
GO
ALTER TABLE [dbo].[InvoiceItem]  WITH CHECK ADD  CONSTRAINT [FK_InvoiceItem_GRNItem] FOREIGN KEY([GRNItemID])
REFERENCES [dbo].[GRNItem] ([ID])
GO
ALTER TABLE [dbo].[InvoiceItem] CHECK CONSTRAINT [FK_InvoiceItem_GRNItem]
GO
ALTER TABLE [dbo].[InvoiceItem]  WITH CHECK ADD  CONSTRAINT [FK_InvoiceItem_Invoice] FOREIGN KEY([InvoiceID])
REFERENCES [dbo].[Invoice] ([ID])
GO
ALTER TABLE [dbo].[InvoiceItem] CHECK CONSTRAINT [FK_InvoiceItem_Invoice]
GO
ALTER TABLE [dbo].[InvoiceOnceOffItems]  WITH CHECK ADD  CONSTRAINT [FK_InvoiceOnceOffItems_GRNOnceOffItems] FOREIGN KEY([GRNOnceOffItemID])
REFERENCES [dbo].[GRNOnceOffItems] ([ID])
GO
ALTER TABLE [dbo].[InvoiceOnceOffItems] CHECK CONSTRAINT [FK_InvoiceOnceOffItems_GRNOnceOffItems]
GO
ALTER TABLE [dbo].[InvoiceOnceOffItems]  WITH CHECK ADD  CONSTRAINT [FK_InvoiceOnceOffItems_Invoice] FOREIGN KEY([InvoiceID])
REFERENCES [dbo].[Invoice] ([ID])
GO
ALTER TABLE [dbo].[InvoiceOnceOffItems] CHECK CONSTRAINT [FK_InvoiceOnceOffItems_Invoice]
GO
ALTER TABLE [dbo].[InvoiceServiceItems]  WITH CHECK ADD  CONSTRAINT [FK_InvoiceServiceItems_Invoice] FOREIGN KEY([InvoiceID])
REFERENCES [dbo].[Invoice] ([ID])
GO
ALTER TABLE [dbo].[InvoiceServiceItems] CHECK CONSTRAINT [FK_InvoiceServiceItems_Invoice]
GO
ALTER TABLE [dbo].[InvoiceServiceItems]  WITH CHECK ADD  CONSTRAINT [FK_InvoiceServiceItems_Services] FOREIGN KEY([ServiceID])
REFERENCES [dbo].[Services] ([ID])
GO
ALTER TABLE [dbo].[InvoiceServiceItems] CHECK CONSTRAINT [FK_InvoiceServiceItems_Services]
GO
ALTER TABLE [dbo].[LatestGrn]  WITH CHECK ADD  CONSTRAINT [FK_LatestGrn_InternalOrder] FOREIGN KEY([InternalOrderID])
REFERENCES [dbo].[InternalOrder] ([ID])
GO
ALTER TABLE [dbo].[LatestGrn] CHECK CONSTRAINT [FK_LatestGrn_InternalOrder]
GO
ALTER TABLE [dbo].[OnceOffItems]  WITH CHECK ADD  CONSTRAINT [FK_OnceOffItems_Department] FOREIGN KEY([DepartmentID])
REFERENCES [dbo].[Department] ([ID])
GO
ALTER TABLE [dbo].[OnceOffItems] CHECK CONSTRAINT [FK_OnceOffItems_Department]
GO
ALTER TABLE [dbo].[OnceOffItems]  WITH CHECK ADD  CONSTRAINT [FK_OnceOffItems_GLCode] FOREIGN KEY([GLCodeID])
REFERENCES [dbo].[GLCode] ([ID])
GO
ALTER TABLE [dbo].[OnceOffItems] CHECK CONSTRAINT [FK_OnceOffItems_GLCode]
GO
ALTER TABLE [dbo].[OnceOffItems]  WITH CHECK ADD  CONSTRAINT [FK_OnceOffItems_InternalOrder] FOREIGN KEY([InternalOrderID])
REFERENCES [dbo].[InternalOrder] ([ID])
GO
ALTER TABLE [dbo].[OnceOffItems] CHECK CONSTRAINT [FK_OnceOffItems_InternalOrder]
GO
ALTER TABLE [dbo].[OnceOffItems]  WITH CHECK ADD  CONSTRAINT [FK_OnceOffItems_UnitOfMeasurement] FOREIGN KEY([UOMID])
REFERENCES [dbo].[UnitOfMeasurement] ([ID])
GO
ALTER TABLE [dbo].[OnceOffItems] CHECK CONSTRAINT [FK_OnceOffItems_UnitOfMeasurement]
GO
ALTER TABLE [dbo].[PlantLocation]  WITH CHECK ADD  CONSTRAINT [FK_PlantLocation_Address] FOREIGN KEY([AddressID])
REFERENCES [dbo].[Address] ([ID])
GO
ALTER TABLE [dbo].[PlantLocation] CHECK CONSTRAINT [FK_PlantLocation_Address]
GO
ALTER TABLE [dbo].[PlantLocation]  WITH CHECK ADD  CONSTRAINT [FK_PlantLocation_Store] FOREIGN KEY([DefaultStoreID])
REFERENCES [dbo].[Store] ([ID])
GO
ALTER TABLE [dbo].[PlantLocation] CHECK CONSTRAINT [FK_PlantLocation_Store]
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
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_Department] FOREIGN KEY([DepartmentID])
REFERENCES [dbo].[Department] ([ID])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_Department]
GO
ALTER TABLE [dbo].[ProductItem]  WITH CHECK ADD  CONSTRAINT [FK_ProductItem_Product] FOREIGN KEY([ProductID])
REFERENCES [dbo].[Product] ([ID])
GO
ALTER TABLE [dbo].[ProductItem] CHECK CONSTRAINT [FK_ProductItem_Product]
GO
ALTER TABLE [dbo].[ProductItem]  WITH CHECK ADD  CONSTRAINT [FK_ProductItem_Product1] FOREIGN KEY([ItemID])
REFERENCES [dbo].[Product] ([ID])
GO
ALTER TABLE [dbo].[ProductItem] CHECK CONSTRAINT [FK_ProductItem_Product1]
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
ALTER TABLE [dbo].[Project]  WITH CHECK ADD  CONSTRAINT [FK_Project_ProjectStatus] FOREIGN KEY([ProjectStatusID])
REFERENCES [dbo].[ProjectStatus] ([ID])
GO
ALTER TABLE [dbo].[Project] CHECK CONSTRAINT [FK_Project_ProjectStatus]
GO
ALTER TABLE [dbo].[Quote]  WITH CHECK ADD  CONSTRAINT [FK_Quote_Customer] FOREIGN KEY([CustomerID])
REFERENCES [dbo].[Customer] ([ID])
GO
ALTER TABLE [dbo].[Quote] CHECK CONSTRAINT [FK_Quote_Customer]
GO
ALTER TABLE [dbo].[Quote]  WITH CHECK ADD  CONSTRAINT [FK_Quote_QuoteStatus] FOREIGN KEY([QuoteStatusID])
REFERENCES [dbo].[QuoteStatus] ([ID])
GO
ALTER TABLE [dbo].[Quote] CHECK CONSTRAINT [FK_Quote_QuoteStatus]
GO
ALTER TABLE [dbo].[Quote]  WITH CHECK ADD  CONSTRAINT [FK_Quote_User] FOREIGN KEY([RequestByID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[Quote] CHECK CONSTRAINT [FK_Quote_User]
GO
ALTER TABLE [dbo].[Quote]  WITH CHECK ADD  CONSTRAINT [FK_Quote_User1] FOREIGN KEY([PlacedByID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[Quote] CHECK CONSTRAINT [FK_Quote_User1]
GO
ALTER TABLE [dbo].[QuoteItem]  WITH CHECK ADD  CONSTRAINT [FK_QuoteItem_Product] FOREIGN KEY([ProductID])
REFERENCES [dbo].[Product] ([ID])
GO
ALTER TABLE [dbo].[QuoteItem] CHECK CONSTRAINT [FK_QuoteItem_Product]
GO
ALTER TABLE [dbo].[QuoteItem]  WITH CHECK ADD  CONSTRAINT [FK_QuoteItem_Quote] FOREIGN KEY([QuoteID])
REFERENCES [dbo].[Quote] ([ID])
GO
ALTER TABLE [dbo].[QuoteItem] CHECK CONSTRAINT [FK_QuoteItem_Quote]
GO
ALTER TABLE [dbo].[QuoteRevision]  WITH CHECK ADD  CONSTRAINT [FK_QuoteRevision_Quote] FOREIGN KEY([QuoteID])
REFERENCES [dbo].[Quote] ([ID])
GO
ALTER TABLE [dbo].[QuoteRevision] CHECK CONSTRAINT [FK_QuoteRevision_Quote]
GO
ALTER TABLE [dbo].[QuoteTransport]  WITH CHECK ADD  CONSTRAINT [FK_QuoteTransport_Quote] FOREIGN KEY([QuoteID])
REFERENCES [dbo].[Quote] ([ID])
GO
ALTER TABLE [dbo].[QuoteTransport] CHECK CONSTRAINT [FK_QuoteTransport_Quote]
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
ALTER TABLE [dbo].[ScannerConfig]  WITH CHECK ADD  CONSTRAINT [FK_ScannerConfig_PlantLocation] FOREIGN KEY([PlantLocationID])
REFERENCES [dbo].[PlantLocation] ([ID])
GO
ALTER TABLE [dbo].[ScannerConfig] CHECK CONSTRAINT [FK_ScannerConfig_PlantLocation]
GO
ALTER TABLE [dbo].[Services]  WITH CHECK ADD  CONSTRAINT [FK_Services_Department] FOREIGN KEY([DepartmentID])
REFERENCES [dbo].[Department] ([ID])
GO
ALTER TABLE [dbo].[Services] CHECK CONSTRAINT [FK_Services_Department]
GO
ALTER TABLE [dbo].[Services]  WITH CHECK ADD  CONSTRAINT [FK_Services_GLCode] FOREIGN KEY([GLCodeID])
REFERENCES [dbo].[GLCode] ([ID])
GO
ALTER TABLE [dbo].[Services] CHECK CONSTRAINT [FK_Services_GLCode]
GO
ALTER TABLE [dbo].[Services]  WITH CHECK ADD  CONSTRAINT [FK_Services_InternalOrder] FOREIGN KEY([InternalOrderID])
REFERENCES [dbo].[InternalOrder] ([ID])
GO
ALTER TABLE [dbo].[Services] CHECK CONSTRAINT [FK_Services_InternalOrder]
GO
ALTER TABLE [dbo].[Stock]  WITH CHECK ADD  CONSTRAINT [FK_Stock_Bin] FOREIGN KEY([BinID])
REFERENCES [dbo].[Bin] ([ID])
GO
ALTER TABLE [dbo].[Stock] CHECK CONSTRAINT [FK_Stock_Bin]
GO
ALTER TABLE [dbo].[Stock]  WITH CHECK ADD  CONSTRAINT [FK_Stock_Department] FOREIGN KEY([DefaultDepartmentID])
REFERENCES [dbo].[Department] ([ID])
GO
ALTER TABLE [dbo].[Stock] CHECK CONSTRAINT [FK_Stock_Department]
GO
ALTER TABLE [dbo].[Stock]  WITH CHECK ADD  CONSTRAINT [FK_Stock_PlantLocation] FOREIGN KEY([DefaultLocationID])
REFERENCES [dbo].[PlantLocation] ([ID])
GO
ALTER TABLE [dbo].[Stock] CHECK CONSTRAINT [FK_Stock_PlantLocation]
GO
ALTER TABLE [dbo].[Stock]  WITH CHECK ADD  CONSTRAINT [FK_Stock_ShelfLifeType] FOREIGN KEY([ShelfLifeTypeID])
REFERENCES [dbo].[ShelfLifeType] ([ID])
GO
ALTER TABLE [dbo].[Stock] CHECK CONSTRAINT [FK_Stock_ShelfLifeType]
GO
ALTER TABLE [dbo].[Stock]  WITH CHECK ADD  CONSTRAINT [FK_Stock_StockCategory] FOREIGN KEY([StockCategoryID])
REFERENCES [dbo].[StockCategory] ([ID])
GO
ALTER TABLE [dbo].[Stock] CHECK CONSTRAINT [FK_Stock_StockCategory]
GO
ALTER TABLE [dbo].[Stock]  WITH CHECK ADD  CONSTRAINT [FK_Stock_StockGroup] FOREIGN KEY([StockGroupID])
REFERENCES [dbo].[StockGroup] ([ID])
GO
ALTER TABLE [dbo].[Stock] CHECK CONSTRAINT [FK_Stock_StockGroup]
GO
ALTER TABLE [dbo].[Stock]  WITH CHECK ADD  CONSTRAINT [FK_Stock_StorageType] FOREIGN KEY([StorageTypeID])
REFERENCES [dbo].[StorageType] ([ID])
GO
ALTER TABLE [dbo].[Stock] CHECK CONSTRAINT [FK_Stock_StorageType]
GO
ALTER TABLE [dbo].[Stock]  WITH CHECK ADD  CONSTRAINT [FK_Stock_Store] FOREIGN KEY([DefaultStoreID])
REFERENCES [dbo].[Store] ([ID])
GO
ALTER TABLE [dbo].[Stock] CHECK CONSTRAINT [FK_Stock_Store]
GO
ALTER TABLE [dbo].[Stock]  WITH CHECK ADD  CONSTRAINT [FK_Stock_Supplier] FOREIGN KEY([SupplierID])
REFERENCES [dbo].[Supplier] ([ID])
GO
ALTER TABLE [dbo].[Stock] CHECK CONSTRAINT [FK_Stock_Supplier]
GO
ALTER TABLE [dbo].[Stock]  WITH CHECK ADD  CONSTRAINT [FK_Stock_UnitOfMeasurement] FOREIGN KEY([UOMID])
REFERENCES [dbo].[UnitOfMeasurement] ([ID])
GO
ALTER TABLE [dbo].[Stock] CHECK CONSTRAINT [FK_Stock_UnitOfMeasurement]
GO
ALTER TABLE [dbo].[Stock]  WITH CHECK ADD  CONSTRAINT [FK_Stock_UnitOfMeasurement1] FOREIGN KEY([SecondaryUOMID])
REFERENCES [dbo].[UnitOfMeasurement] ([ID])
GO
ALTER TABLE [dbo].[Stock] CHECK CONSTRAINT [FK_Stock_UnitOfMeasurement1]
GO
ALTER TABLE [dbo].[StockQuantity]  WITH CHECK ADD  CONSTRAINT [FK_StockQuantity_Department] FOREIGN KEY([DepartmentID])
REFERENCES [dbo].[Department] ([ID])
GO
ALTER TABLE [dbo].[StockQuantity] CHECK CONSTRAINT [FK_StockQuantity_Department]
GO
ALTER TABLE [dbo].[StockQuantity]  WITH CHECK ADD  CONSTRAINT [FK_StockQuantity_PlantLocation] FOREIGN KEY([LocationID])
REFERENCES [dbo].[PlantLocation] ([ID])
GO
ALTER TABLE [dbo].[StockQuantity] CHECK CONSTRAINT [FK_StockQuantity_PlantLocation]
GO
ALTER TABLE [dbo].[StockQuantity]  WITH CHECK ADD  CONSTRAINT [FK_StockQuantity_Stock] FOREIGN KEY([StockID])
REFERENCES [dbo].[Stock] ([ID])
GO
ALTER TABLE [dbo].[StockQuantity] CHECK CONSTRAINT [FK_StockQuantity_Stock]
GO
ALTER TABLE [dbo].[StockQuantity]  WITH CHECK ADD  CONSTRAINT [FK_StockQuantity_Store] FOREIGN KEY([StoreID])
REFERENCES [dbo].[Store] ([ID])
GO
ALTER TABLE [dbo].[StockQuantity] CHECK CONSTRAINT [FK_StockQuantity_Store]
GO
ALTER TABLE [dbo].[Stocktake]  WITH CHECK ADD  CONSTRAINT [FK_Stocktake_PlantLocation] FOREIGN KEY([PlantLocationID])
REFERENCES [dbo].[PlantLocation] ([ID])
GO
ALTER TABLE [dbo].[Stocktake] CHECK CONSTRAINT [FK_Stocktake_PlantLocation]
GO
ALTER TABLE [dbo].[Stocktake]  WITH CHECK ADD  CONSTRAINT [FK_Stocktake_Stock] FOREIGN KEY([StockID])
REFERENCES [dbo].[Stock] ([ID])
GO
ALTER TABLE [dbo].[Stocktake] CHECK CONSTRAINT [FK_Stocktake_Stock]
GO
ALTER TABLE [dbo].[Stocktake]  WITH CHECK ADD  CONSTRAINT [FK_Stocktake_Store] FOREIGN KEY([StoreID])
REFERENCES [dbo].[Store] ([ID])
GO
ALTER TABLE [dbo].[Stocktake] CHECK CONSTRAINT [FK_Stocktake_Store]
GO
ALTER TABLE [dbo].[Stocktake]  WITH CHECK ADD  CONSTRAINT [FK_Stocktake_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[Stocktake] CHECK CONSTRAINT [FK_Stocktake_User]
GO
ALTER TABLE [dbo].[StocktakeReport]  WITH CHECK ADD  CONSTRAINT [FK_StocktakeReport_StocktakeCycle] FOREIGN KEY([StocktakeCycleID])
REFERENCES [dbo].[StocktakeCycle] ([ID])
GO
ALTER TABLE [dbo].[StocktakeReport] CHECK CONSTRAINT [FK_StocktakeReport_StocktakeCycle]
GO
ALTER TABLE [dbo].[Store]  WITH CHECK ADD  CONSTRAINT [FK_Store_PlantLocation] FOREIGN KEY([PlantLocationID])
REFERENCES [dbo].[PlantLocation] ([ID])
GO
ALTER TABLE [dbo].[Store] CHECK CONSTRAINT [FK_Store_PlantLocation]
GO
ALTER TABLE [dbo].[Store]  WITH CHECK ADD  CONSTRAINT [FK_Store_StoreType] FOREIGN KEY([StoreTypeID])
REFERENCES [dbo].[StoreType] ([ID])
GO
ALTER TABLE [dbo].[Store] CHECK CONSTRAINT [FK_Store_StoreType]
GO
ALTER TABLE [dbo].[Supplier]  WITH CHECK ADD  CONSTRAINT [FK_Supplier_Address] FOREIGN KEY([AddressID])
REFERENCES [dbo].[Address] ([ID])
GO
ALTER TABLE [dbo].[Supplier] CHECK CONSTRAINT [FK_Supplier_Address]
GO
ALTER TABLE [dbo].[Supplier]  WITH CHECK ADD  CONSTRAINT [FK_Supplier_BankName] FOREIGN KEY([BankNameID])
REFERENCES [dbo].[BankName] ([ID])
GO
ALTER TABLE [dbo].[Supplier] CHECK CONSTRAINT [FK_Supplier_BankName]
GO
ALTER TABLE [dbo].[Supplier]  WITH CHECK ADD  CONSTRAINT [FK_Supplier_Currency] FOREIGN KEY([CurrencyID])
REFERENCES [dbo].[Currency] ([ID])
GO
ALTER TABLE [dbo].[Supplier] CHECK CONSTRAINT [FK_Supplier_Currency]
GO
ALTER TABLE [dbo].[Supplier]  WITH CHECK ADD  CONSTRAINT [FK_Supplier_PaymentMethods] FOREIGN KEY([PaymentMethodID])
REFERENCES [dbo].[PaymentMethods] ([ID])
GO
ALTER TABLE [dbo].[Supplier] CHECK CONSTRAINT [FK_Supplier_PaymentMethods]
GO
ALTER TABLE [dbo].[UserDetails]  WITH CHECK ADD  CONSTRAINT [FK_UserDetails_Address] FOREIGN KEY([HomeAddressID])
REFERENCES [dbo].[Address] ([ID])
GO
ALTER TABLE [dbo].[UserDetails] CHECK CONSTRAINT [FK_UserDetails_Address]
GO
ALTER TABLE [dbo].[UserDetails]  WITH CHECK ADD  CONSTRAINT [FK_UserDetails_BankName] FOREIGN KEY([BankNameID])
REFERENCES [dbo].[BankName] ([ID])
GO
ALTER TABLE [dbo].[UserDetails] CHECK CONSTRAINT [FK_UserDetails_BankName]
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
ALTER TABLE [dbo].[VatStored]  WITH CHECK ADD  CONSTRAINT [FK_VatStored_GLCode] FOREIGN KEY([GLCodeID])
REFERENCES [dbo].[GLCode] ([ID])
GO
ALTER TABLE [dbo].[VatStored] CHECK CONSTRAINT [FK_VatStored_GLCode]
GO
/****** Object:  StoredProcedure [dbo].[AddStock]    Script Date: 2023/01/18 11:49:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[AddStock]
	@Packsize int,
	@InsertedQuantity int,
	@Price decimal(18,2),
	@ItemQuantity decimal(18,2),
	@StockId int,
	@DepartmentId int,
	@LocationId int,
	@StoreId int,
	@GrnNumber nvarchar(50)

AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @Counter1 int = 1
	DECLARE @Counter2 int = 1
	WHILE (@Counter1 <= @InsertedQuantity)
    BEGIN
			SET @Counter2 = 1
            WHILE (@Counter2 <= @Packsize)
			BEGIN
             INSERT INTO StockQuantity(StockID,ItemQuantity,DateCreated,Price,DateModified,DepartmentID,LocationID,StoreID,GrnNumber) VALUES 
			 (@StockId,@ItemQuantity,GETDATE(),@Price,GETDATE(),@DepartmentId,@LocationId,@StoreId,@GrnNumber)
            SET @Counter2 = @Counter2 + 1;
			END
        SET @Counter1 = @Counter1 + 1;
    END
END
GO
/****** Object:  StoredProcedure [dbo].[AIS_DB_UPDATE_V1]    Script Date: 2023/01/18 11:49:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[AIS_DB_UPDATE_V1] 
AS
BEGIN 
BEGIN TRY

	DECLARE @NewVersion int = 1
	DECLARE @OldVersion int = (SELECT MAX([Version]) FROM DBVersion)
	IF(@OldVersion IS NULL) SET @OldVersion = 0

	IF((@OldVersion + 1) != @NewVersion)
	BEGIN
		DECLARE @ErrMessage nvarchar(200);
		SET @ErrMessage = CONCAT('Invalid Version number. DB: ', @OldVersion,' Script: ',@NewVersion);
		THROW 51000,  @ErrMessage, 1; 
	END
	----------------------------------------
	
	CREATE TABLE [dbo].[Discrepency](
		[ID] [int] IDENTITY(1,1) NOT NULL,
		[Name] [nvarchar](50) NOT NULL,
		[Description] [nvarchar](max) NULL,
	 CONSTRAINT [PK_Discrepency] PRIMARY KEY CLUSTERED 
	(
		[ID] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
	) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

	CREATE TABLE [dbo].[Invoice](
		[ID] [int] IDENTITY(1,1) NOT NULL,
		[InvoiceNumber] [int] NOT NULL,
		[Total] [decimal](12, 2) NOT NULL,
		[DiscrepencyID] [int] NULL,
		[InternalOrderID] [int] NOT NULL,
		[DateCreated] [datetime] NOT NULL,
		[DiscrepencyDescription] [nvarchar](max) NULL,
		CONSTRAINT [PK_Invoice] PRIMARY KEY CLUSTERED 
	(
		[ID] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
	) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
	
	CREATE TABLE [dbo].[InvoiceItem](
		[ID] [int] IDENTITY(1,1) NOT NULL,
		[Quantity] [int] NOT NULL,
		[ItemValue] [decimal](12, 2) NOT NULL,
		[InvoiceID] [int] NOT NULL,
		[ManufacturerCode] [nvarchar](50) NOT NULL,
		[ManufacturerProductName] [nvarchar](200) NOT NULL,
		[RequiredQuantity] [int] NOT NULL,
		CONSTRAINT [PK_InvoiceItem] PRIMARY KEY CLUSTERED 
	(
		[ID] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
	) ON [PRIMARY]
	
	CREATE TABLE [dbo].[LocationType](
		[ID] [int] IDENTITY(1,1) NOT NULL,
		[Name] [nvarchar](50) NOT NULL,
	 CONSTRAINT [PK_LocationType] PRIMARY KEY CLUSTERED 
	(
		[ID] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
	) ON [PRIMARY]
	
	CREATE TABLE [dbo].[PlantLocation](
		[ID] [int] IDENTITY(1,1) NOT NULL,
		[Name] [nvarchar](50) NOT NULL,
		[Description] [nvarchar](max) NULL,
		[AddressID] [int] NOT NULL,
	 CONSTRAINT [PK_PlantLocation] PRIMARY KEY CLUSTERED 
	(
		[ID] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
	) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
	---------------------------------

	ALTER TABLE [dbo].[InternalOrder] ALTER COLUMN [DeliveryCost] [decimal](12, 2) NULL
	ALTER TABLE [dbo].[InternalOrder] ADD [DateApproved] [datetime] NULL
	ALTER TABLE [dbo].[InternalOrder] ADD [InternalComment] [nvarchar](max) NULL
	ALTER TABLE [dbo].[InternalOrderItem] ALTER COLUMN [BackOrder] [varchar](50) NOT NULL
	ALTER TABLE [dbo].[PriceLookupLog] ADD [Username] [nvarchar](max) NULL
	UPDATE [dbo].[PriceLookupLog] SET [Username] = ''
	ALTER TABLE [dbo].[PriceLookupLog] ALTER COLUMN [Username] [nvarchar](max) NOT NULL
	ALTER TABLE [dbo].[ProfitCenter] ADD [Abbreviation] [nvarchar](3) NULL
	ALTER TABLE [dbo].[Stock] ADD [InternalProductName] [nvarchar](50) NULL
	ALTER TABLE [dbo].[Stock] ADD [Monitored] [bit] NOT NULL
	ALTER TABLE [dbo].[StorageType] ADD [RequireBarcode] [bit] NOT NULL
	ALTER TABLE [dbo].[Supplier] ADD [LocationTypeID] [int] NULL
	ALTER TABLE [dbo].[UserDetails] ADD [PlantLocationID] [int] NOT NULL

	-----------------------------
	ALTER TABLE [dbo].[Invoice]  WITH CHECK ADD  CONSTRAINT [FK_Invoice_Discrepency] FOREIGN KEY([DiscrepencyID])
	REFERENCES [dbo].[Discrepency] ([ID])
	
	ALTER TABLE [dbo].[Invoice] CHECK CONSTRAINT [FK_Invoice_Discrepency]
	
	ALTER TABLE [dbo].[Invoice]  WITH CHECK ADD  CONSTRAINT [FK_Invoice_InternalOrder] FOREIGN KEY([InternalOrderID])
	REFERENCES [dbo].[InternalOrder] ([ID])
	
	ALTER TABLE [dbo].[Invoice] CHECK CONSTRAINT [FK_Invoice_InternalOrder]
	
	ALTER TABLE [dbo].[InvoiceItem]  WITH CHECK ADD  CONSTRAINT [FK_InvoiceItem_Invoice] FOREIGN KEY([InvoiceID])
	REFERENCES [dbo].[Invoice] ([ID])
	
	ALTER TABLE [dbo].[InvoiceItem] CHECK CONSTRAINT [FK_InvoiceItem_Invoice]
	
	ALTER TABLE [dbo].[PlantLocation]  WITH CHECK ADD  CONSTRAINT [FK_PlantLocation_Address] FOREIGN KEY([AddressID])
	REFERENCES [dbo].[Address] ([ID])
	
	ALTER TABLE [dbo].[PlantLocation] CHECK CONSTRAINT [FK_PlantLocation_Address]
	
	ALTER TABLE [dbo].[Supplier]  WITH CHECK ADD  CONSTRAINT [FK_Supplier_LocationType] FOREIGN KEY([LocationTypeID])
	REFERENCES [dbo].[LocationType] ([ID])
	
	ALTER TABLE [dbo].[Supplier] CHECK CONSTRAINT [FK_Supplier_LocationType]
	
	ALTER TABLE [dbo].[UserDetails]  WITH CHECK ADD  CONSTRAINT [FK_UserDetails_PlantLocation] FOREIGN KEY([PlantLocationID])
	REFERENCES [dbo].[PlantLocation] ([ID])
	
	ALTER TABLE [dbo].[UserDetails] CHECK CONSTRAINT [FK_UserDetails_PlantLocation]
	----------------------------
	SET IDENTITY_INSERT [dbo].[InternalOrderStatus] ON 

	INSERT [dbo].[InternalOrderStatus] ([ID], [Name]) VALUES (1, N'Approved')
	INSERT [dbo].[InternalOrderStatus] ([ID], [Name]) VALUES (2, N'Pending')
	INSERT [dbo].[InternalOrderStatus] ([ID], [Name]) VALUES (3, N'Denied')
	INSERT [dbo].[InternalOrderStatus] ([ID], [Name]) VALUES (4, N'Pending Monitored Approval')
	INSERT [dbo].[InternalOrderStatus] ([ID], [Name]) VALUES (5, N'Review')
	SET IDENTITY_INSERT [dbo].[InternalOrderStatus] OFF

	INSERT [dbo].[Permission] ([ID], [Page], [Component], [Description]) VALUES (62, N'File Management', N'View', N'The User will have the ability to view the page')
	INSERT [dbo].[Permission] ([ID], [Page], [Component], [Description]) VALUES (63, N'File Management', N'Modify', N'The User will have the ability to modify the page')
	INSERT [dbo].[Permission] ([ID], [Page], [Component], [Description]) VALUES (64, N'Import File Management', N'View', N'The User will have the ability to view the page')
	INSERT [dbo].[Permission] ([ID], [Page], [Component], [Description]) VALUES (65, N'Import File Management', N'Modify', N'The User will have the ability to modify the page')
	INSERT [dbo].[Permission] ([ID], [Page], [Component], [Description]) VALUES (66, N'Allow Monitored Items', N'Modify', N'The User will be able to allow price increase on monitored stock items')
	INSERT [dbo].[Permission] ([ID], [Page], [Component], [Description]) VALUES (67, N'Receive Monitored Items Email Pending', N'Modify', N'The User will receive a email to notify them of a monitored stock item pending')
	INSERT [dbo].[Permission] ([ID], [Page], [Component], [Description]) VALUES (68, N'Receive Monitored Items Email Approval', N'Modify', N'The User will receive a email to notify them of a monitored stock Item approval')
	INSERT [dbo].[Permission] ([ID], [Page], [Component], [Description]) VALUES (69, N'Approved Orders Report', N'View', N'The User will have the ability to view the page')
	INSERT [dbo].[Permission] ([ID], [Page], [Component], [Description]) VALUES (70, N'System Config - Plant Location', N'View', N'The User will have the ability to view the page')
	INSERT [dbo].[Permission] ([ID], [Page], [Component], [Description]) VALUES (71, N'System Config - Plant Location', N'Modify', N'The User will have the ability to modify the page')
	-----------------------------
	INSERT INTO DBVersion ([Version],[DateTime]) VALUES (@NewVersion, GETDATE());
END TRY
BEGIN CATCH
SELECT  
        ERROR_NUMBER() AS ErrorNumber  
        ,ERROR_SEVERITY() AS ErrorSeverity  
        ,ERROR_STATE() AS ErrorState  
        ,ERROR_PROCEDURE() AS ErrorProcedure  
        ,ERROR_LINE() AS ErrorLine  
        ,ERROR_MESSAGE() AS ErrorMessage; 
END CATCH;
END
GO
/****** Object:  StoredProcedure [dbo].[clearDatabase]    Script Date: 2023/01/18 11:49:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[clearDatabase]

AS
BEGIN
DELETE Supplier
DELETE [Address]
DELETE Contacts
DELETE CostCenter
DELETE CostManagers
DELETE Customer
DELETE EmployeePosition
DELETE PaymentMethods
DELETE PriceIncrease
DELETE PriceLookup
DELETE PriceLookupLog
DELETE Product
DELETE ProductionOrders
DELETE ProductStock
DELETE ProfitCenter
DELETE Recipes
DELETE Stock
DELETE StockCategory
DELETE StockLog
DELETE StockQuantity

DELETE SupplierCategory
DELETE UnitOfMeasurement
DELETE [User]
DELETE UserDetails
DELETE UserPermissions
END
GO
