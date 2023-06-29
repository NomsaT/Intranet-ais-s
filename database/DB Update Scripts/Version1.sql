
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
	GO
	ALTER TABLE [dbo].[InternalOrder] ALTER COLUMN [DeliveryCost] [decimal](12, 2) NULL
	GO
	ALTER TABLE [dbo].[InternalOrder] ADD [DateApproved] [datetime] NULL
	GO
	ALTER TABLE [dbo].[InternalOrder] ADD [InternalComment] [nvarchar](max) NULL
	GO
	ALTER TABLE [dbo].[InternalOrderItem] ALTER COLUMN [BackOrder] [varchar](50) NOT NULL
	GO
	ALTER TABLE [dbo].[PriceLookupLog] ADD [Username] [nvarchar](max) NULL
	GO
	UPDATE [dbo].[PriceLookupLog] SET [Username] = ''
	GO
	ALTER TABLE [dbo].[PriceLookupLog] ALTER COLUMN [Username] [nvarchar](max) NOT NULL
	GO
	ALTER TABLE [dbo].[ProfitCenter] ADD [Abbreviation] [nvarchar](3) NULL
	GO
	ALTER TABLE [dbo].[Stock] ADD [InternalProductName] [nvarchar](50) NULL
	GO
	ALTER TABLE [dbo].[Stock] ADD [Monitored] [bit] NULL
	GO
	UPDATE [dbo].[Stock] SET [Monitored] = 0
	GO
	ALTER TABLE [dbo].[Stock] ALTER COLUMN [Monitored] [bit] NOT NULL	
	GO
	ALTER TABLE [dbo].[StorageType] ADD [RequireBarcode] [bit] NULL
	GO
	UPDATE [dbo].[StorageType] SET [RequireBarcode] = 0
	GO
	ALTER TABLE [dbo].[StorageType] ALTER COLUMN [RequireBarcode] [bit] NOT NULL
	GO
	ALTER TABLE [dbo].[Supplier] ADD [LocationTypeID] [int] NULL	
	GO
	INSERT [dbo].[Address] ([StreetAddress1], [StreetAddress2], [Suburb], [City], [PostCode], [CountryID]) VALUES ( N'Rename Street', NULL, N'Rename Suburb', N'Rename City', N'0000', 190)
	GO
	SET IDENTITY_INSERT [dbo].[PlantLocation] ON 
	GO
	INSERT [dbo].[PlantLocation] ([ID], [Name], [Description], [AddressID]) VALUES (1, N'RENAME THIS PLANT', N'RENAME PLANT PLEASE', SCOPE_IDENTITY())
	GO
	SET IDENTITY_INSERT [dbo].[PlantLocation] OFF
	GO

	ALTER TABLE [dbo].[UserDetails] ADD [PlantLocationID] [int] NULL
	GO

	UPDATE [dbo].[UserDetails] SET [PlantLocationID] = 1
	GO
	ALTER TABLE [dbo].[UserDetails] ALTER COLUMN [PlantLocationID] [int] NOT NULL
	GO
	-----------------------------
	ALTER TABLE [dbo].[Invoice]  WITH CHECK ADD  CONSTRAINT [FK_Invoice_Discrepency] FOREIGN KEY([DiscrepencyID])
	REFERENCES [dbo].[Discrepency] ([ID])
	GO
	ALTER TABLE [dbo].[Invoice] CHECK CONSTRAINT [FK_Invoice_Discrepency]
	GO
	ALTER TABLE [dbo].[Invoice]  WITH CHECK ADD  CONSTRAINT [FK_Invoice_InternalOrder] FOREIGN KEY([InternalOrderID])
	REFERENCES [dbo].[InternalOrder] ([ID])
	GO
	ALTER TABLE [dbo].[Invoice] CHECK CONSTRAINT [FK_Invoice_InternalOrder]
	GO
	ALTER TABLE [dbo].[InvoiceItem]  WITH CHECK ADD  CONSTRAINT [FK_InvoiceItem_Invoice] FOREIGN KEY([InvoiceID])
	REFERENCES [dbo].[Invoice] ([ID])
	GO
	ALTER TABLE [dbo].[InvoiceItem] CHECK CONSTRAINT [FK_InvoiceItem_Invoice]
	GO
	ALTER TABLE [dbo].[PlantLocation]  WITH CHECK ADD  CONSTRAINT [FK_PlantLocation_Address] FOREIGN KEY([AddressID])
	REFERENCES [dbo].[Address] ([ID])
	GO
	ALTER TABLE [dbo].[PlantLocation] CHECK CONSTRAINT [FK_PlantLocation_Address]
	GO
	ALTER TABLE [dbo].[Supplier]  WITH CHECK ADD  CONSTRAINT [FK_Supplier_LocationType] FOREIGN KEY([LocationTypeID])
	REFERENCES [dbo].[LocationType] ([ID])
	GO
	ALTER TABLE [dbo].[Supplier] CHECK CONSTRAINT [FK_Supplier_LocationType]
	GO
	ALTER TABLE [dbo].[UserDetails]  WITH CHECK ADD  CONSTRAINT [FK_UserDetails_PlantLocation] FOREIGN KEY([PlantLocationID])
	REFERENCES [dbo].[PlantLocation] ([ID])
	GO
	ALTER TABLE [dbo].[UserDetails] CHECK CONSTRAINT [FK_UserDetails_PlantLocation]
	GO
	----------------------------
	SET IDENTITY_INSERT [dbo].[InternalOrderStatus] ON 

	INSERT [dbo].[InternalOrderStatus] ([ID], [Name]) VALUES (1, N'Approved')
	INSERT [dbo].[InternalOrderStatus] ([ID], [Name]) VALUES (2, N'Pending')
	INSERT [dbo].[InternalOrderStatus] ([ID], [Name]) VALUES (3, N'Denied')
	INSERT [dbo].[InternalOrderStatus] ([ID], [Name]) VALUES (4, N'Pending Monitored Approval')
	INSERT [dbo].[InternalOrderStatus] ([ID], [Name]) VALUES (5, N'Review')
	SET IDENTITY_INSERT [dbo].[InternalOrderStatus] OFF
	GO
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
	GO
	
	-----------------------------
	INSERT INTO DBVersion ([Version],[DateTime]) VALUES (1, GETDATE());
GO