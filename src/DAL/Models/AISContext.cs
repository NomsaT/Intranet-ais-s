using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace DAL.Models
{
    public partial class AISContext : DbContext
    {
        public AISContext()
        {
        }

        public AISContext(DbContextOptions<AISContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<BankName> BankNames { get; set; }
        public virtual DbSet<BarcodeTemp> BarcodeTemps { get; set; }
        public virtual DbSet<Bin> Bins { get; set; }
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<CostType> CostTypes { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Currency> Currencies { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Dbversion> Dbversions { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<DepartmentManager> DepartmentManagers { get; set; }
        public virtual DbSet<DepartmentUser> DepartmentUsers { get; set; }
        public virtual DbSet<Discrepency> Discrepencies { get; set; }
        public virtual DbSet<EmployeePosition> EmployeePositions { get; set; }
        public virtual DbSet<Gender> Genders { get; set; }
        public virtual DbSet<Glcode> Glcodes { get; set; }
        public virtual DbSet<Grn> Grns { get; set; }
        public virtual DbSet<Grnitem> Grnitems { get; set; }
        public virtual DbSet<GrnonceOffItem> GrnonceOffItems { get; set; }
        public virtual DbSet<IncreaseType> IncreaseTypes { get; set; }
        public virtual DbSet<InternalOrder> InternalOrders { get; set; }
        public virtual DbSet<InternalOrderItem> InternalOrderItems { get; set; }
        public virtual DbSet<InternalOrderStatus> InternalOrderStatuses { get; set; }
        public virtual DbSet<Invoice> Invoices { get; set; }
        public virtual DbSet<InvoiceItem> InvoiceItems { get; set; }
        public virtual DbSet<InvoiceOnceOffItem> InvoiceOnceOffItems { get; set; }
        public virtual DbSet<InvoiceServiceItem> InvoiceServiceItems { get; set; }
        public virtual DbSet<LatestGrn> LatestGrns { get; set; }
        public virtual DbSet<Law> Laws { get; set; }
        public virtual DbSet<MaritalStatus> MaritalStatuses { get; set; }
        public virtual DbSet<OnceOffItem> OnceOffItems { get; set; }
        public virtual DbSet<PaymentInterval> PaymentIntervals { get; set; }
        public virtual DbSet<PaymentMethod> PaymentMethods { get; set; }
        public virtual DbSet<Permission> Permissions { get; set; }
        public virtual DbSet<PlantLocation> PlantLocations { get; set; }
        public virtual DbSet<PriceIncrease> PriceIncreases { get; set; }
        public virtual DbSet<PriceLookup> PriceLookups { get; set; }
        public virtual DbSet<PriceLookupLog> PriceLookupLogs { get; set; }
        public virtual DbSet<Printer> Printers { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductItem> ProductItems { get; set; }
        public virtual DbSet<ProductStock> ProductStocks { get; set; }
        public virtual DbSet<ProductionOrder> ProductionOrders { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<ProjectStatus> ProjectStatuses { get; set; }
        public virtual DbSet<Quote> Quotes { get; set; }
        public virtual DbSet<QuoteItem> QuoteItems { get; set; }
        public virtual DbSet<QuoteRevision> QuoteRevisions { get; set; }
        public virtual DbSet<QuoteStatus> QuoteStatuses { get; set; }
        public virtual DbSet<QuoteTransport> QuoteTransports { get; set; }
        public virtual DbSet<Race> Races { get; set; }
        public virtual DbSet<Recipe> Recipes { get; set; }
        public virtual DbSet<ReturnToSupplier> ReturnToSuppliers { get; set; }
        public virtual DbSet<RolePermission> RolePermissions { get; set; }
        public virtual DbSet<RoleTemplate> RoleTemplates { get; set; }
        public virtual DbSet<ScannerConfig> ScannerConfigs { get; set; }
        public virtual DbSet<Service> Services { get; set; }
        public virtual DbSet<ShelfLifeType> ShelfLifeTypes { get; set; }
        public virtual DbSet<Stock> Stocks { get; set; }
        public virtual DbSet<StockCategory> StockCategories { get; set; }
        public virtual DbSet<StockGroup> StockGroups { get; set; }
        public virtual DbSet<StockLog> StockLogs { get; set; }
        public virtual DbSet<StockQuantity> StockQuantities { get; set; }
        public virtual DbSet<Stocktake> Stocktakes { get; set; }
        public virtual DbSet<StocktakeCounter> StocktakeCounters { get; set; }
        public virtual DbSet<StocktakeCycle> StocktakeCycles { get; set; }
        public virtual DbSet<StocktakeLog> StocktakeLogs { get; set; }
        public virtual DbSet<StocktakeReport> StocktakeReports { get; set; }
        public virtual DbSet<StorageType> StorageTypes { get; set; }
        public virtual DbSet<Store> Stores { get; set; }
        public virtual DbSet<StoreType> StoreTypes { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }
        public virtual DbSet<TypeEmployment> TypeEmployments { get; set; }
        public virtual DbSet<UnitOfMeasurement> UnitOfMeasurements { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserDetail> UserDetails { get; set; }
        public virtual DbSet<UserPermission> UserPermissions { get; set; }
        public virtual DbSet<Vat> Vats { get; set; }
        public virtual DbSet<VatStored> VatStoreds { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(DAL.DB.ConnectionString);
                optionsBuilder.UseLazyLoadingProxies();
                optionsBuilder.EnableSensitiveDataLogging();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AS");

            modelBuilder.Entity<Address>(entity =>
            {
                entity.ToTable("Address");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CountryId).HasColumnName("CountryID");

                entity.Property(e => e.PostCode)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.StreetAddress1)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.StreetAddress2).HasMaxLength(50);

                entity.Property(e => e.Suburb)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Addresses)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Address_Country");
            });

            modelBuilder.Entity<BankName>(entity =>
            {
                entity.ToTable("BankName");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BankName1)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("BankName");
            });

            modelBuilder.Entity<BarcodeTemp>(entity =>
            {
                entity.ToTable("BarcodeTemp");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Barcode)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Template).IsRequired();
            });

            modelBuilder.Entity<Bin>(entity =>
            {
                entity.ToTable("Bin");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.StoreId).HasColumnName("StoreID");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.Bins)
                    .HasForeignKey(d => d.StoreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Bin_Store");
            });

            modelBuilder.Entity<Company>(entity =>
            {
                entity.ToTable("Company");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AddressId).HasColumnName("AddressID");

                entity.Property(e => e.Name).IsRequired();

                entity.Property(e => e.RegNr)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.VatNr)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.Companies)
                    .HasForeignKey(d => d.AddressId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Company_Address");
            });

            modelBuilder.Entity<Contact>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ContactEmail).HasMaxLength(200);

                entity.Property(e => e.ContactId).HasColumnName("ContactID");

                entity.Property(e => e.ContactNumber)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.PersonName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.PersonPosition)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.SupplierId).HasColumnName("SupplierID");

                entity.Property(e => e.WorkLandlineNumber).HasMaxLength(50);

                entity.HasOne(d => d.ContactNavigation)
                    .WithMany(p => p.Contacts)
                    .HasForeignKey(d => d.ContactId)
                    .HasConstraintName("FK_Contacts_Customer");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.Contacts)
                    .HasForeignKey(d => d.SupplierId)
                    .HasConstraintName("FK_Contacts_Supplier");
            });

            modelBuilder.Entity<CostType>(entity =>
            {
                entity.ToTable("CostType");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Abbreviation).HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.ToTable("Country");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Currency>(entity =>
            {
                entity.ToTable("Currency");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CurrencyName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CurrencyValue).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Iso)
                    .HasMaxLength(50)
                    .HasColumnName("ISO");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customer");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AccountNumber)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CompanyName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.CompanyPrefix)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.DeliveryAddressId).HasColumnName("DeliveryAddressID");

                entity.Property(e => e.PaymentMethodId).HasColumnName("PaymentMethodID");

                entity.Property(e => e.PhysicalAddressId).HasColumnName("PhysicalAddressID");

                entity.HasOne(d => d.DeliveryAddress)
                    .WithMany(p => p.CustomerDeliveryAddresses)
                    .HasForeignKey(d => d.DeliveryAddressId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Customer_Address1");

                entity.HasOne(d => d.PaymentMethod)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.PaymentMethodId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Customer_PaymentMethods");

                entity.HasOne(d => d.PhysicalAddress)
                    .WithMany(p => p.CustomerPhysicalAddresses)
                    .HasForeignKey(d => d.PhysicalAddressId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Customer_Address");
            });

            modelBuilder.Entity<Dbversion>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("DBVersion");

                entity.Property(e => e.DateTime).HasColumnType("datetime");

                entity.Property(e => e.Version)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.ToTable("Department");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Abbreviation).HasMaxLength(50);

                entity.Property(e => e.Color).HasMaxLength(50);

                entity.Property(e => e.CostTypeId).HasColumnName("CostTypeID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.HasOne(d => d.CostType)
                    .WithMany(p => p.Departments)
                    .HasForeignKey(d => d.CostTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Department_CostType");
            });

            modelBuilder.Entity<DepartmentManager>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.DepartmentManagers)
                    .HasForeignKey(d => d.DepartmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DepartmentManagers_Department");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.DepartmentManagers)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DepartmentManagers_User");
            });

            modelBuilder.Entity<DepartmentUser>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.DepartmentUsers)
                    .HasForeignKey(d => d.DepartmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProfitCenterUsers_Department");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.DepartmentUsers)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProfitCenterUsers_User");
            });

            modelBuilder.Entity<Discrepency>(entity =>
            {
                entity.ToTable("Discrepency");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<EmployeePosition>(entity =>
            {
                entity.ToTable("EmployeePosition");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Position)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Gender>(entity =>
            {
                entity.ToTable("Gender");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Gender1)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Gender");
            });

            modelBuilder.Entity<Glcode>(entity =>
            {
                entity.ToTable("GLCode");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Grn>(entity =>
            {
                entity.ToTable("GRN");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CapturerId).HasColumnName("CapturerID");

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.EditorId).HasColumnName("EditorID");

                entity.Property(e => e.GrnNumber)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.InternalOrderId).HasColumnName("InternalOrderID");

                entity.Property(e => e.Total).HasColumnType("decimal(12, 2)");

                entity.HasOne(d => d.Capturer)
                    .WithMany(p => p.GrnCapturers)
                    .HasForeignKey(d => d.CapturerId)
                    .HasConstraintName("FK_GRN_User");

                entity.HasOne(d => d.Editor)
                    .WithMany(p => p.GrnEditors)
                    .HasForeignKey(d => d.EditorId)
                    .HasConstraintName("FK_GRN_User1");

                entity.HasOne(d => d.InternalOrder)
                    .WithMany(p => p.Grns)
                    .HasForeignKey(d => d.InternalOrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GRN_InternalOrder");
            });

            modelBuilder.Entity<Grnitem>(entity =>
            {
                entity.ToTable("GRNItem");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.GrnId).HasColumnName("GrnID");

                entity.Property(e => e.InternalOrderItemId).HasColumnName("InternalOrderItemID");

                entity.Property(e => e.StoreLocationId).HasColumnName("StoreLocationID");

                entity.HasOne(d => d.Grn)
                    .WithMany(p => p.Grnitems)
                    .HasForeignKey(d => d.GrnId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GRNItem_GRN");

                entity.HasOne(d => d.InternalOrderItem)
                    .WithMany(p => p.Grnitems)
                    .HasForeignKey(d => d.InternalOrderItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GRNItem_InternalOrderItem");

                entity.HasOne(d => d.StoreLocation)
                    .WithMany(p => p.Grnitems)
                    .HasForeignKey(d => d.StoreLocationId)
                    .HasConstraintName("FK_GRNItem_Store");
            });

            modelBuilder.Entity<GrnonceOffItem>(entity =>
            {
                entity.ToTable("GRNOnceOffItems");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.GrnId).HasColumnName("GrnID");

                entity.Property(e => e.OnceOffItemsId).HasColumnName("OnceOffItemsID");

                entity.HasOne(d => d.Grn)
                    .WithMany(p => p.GrnonceOffItems)
                    .HasForeignKey(d => d.GrnId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GRNOnceOffItems_GRN1");

                entity.HasOne(d => d.OnceOffItems)
                    .WithMany(p => p.GrnonceOffItems)
                    .HasForeignKey(d => d.OnceOffItemsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GRNOnceOffItems_OnceOffItems");
            });

            modelBuilder.Entity<IncreaseType>(entity =>
            {
                entity.ToTable("IncreaseType");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<InternalOrder>(entity =>
            {
                entity.ToTable("InternalOrder");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ApproveById).HasColumnName("ApproveByID");

                entity.Property(e => e.CompanyId).HasColumnName("CompanyID");

                entity.Property(e => e.DateApproved).HasColumnType("datetime");

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DeliveryDate).HasColumnType("datetime");

                entity.Property(e => e.FollowUpDate).HasColumnType("datetime");

                entity.Property(e => e.PlacedById).HasColumnName("PlacedByID");

                entity.Property(e => e.PlantLocationId).HasColumnName("PlantLocationID");

                entity.Property(e => e.ProjectId).HasColumnName("ProjectID");

                entity.Property(e => e.QuotationNumber).HasMaxLength(50);

                entity.Property(e => e.RequestedById).HasColumnName("RequestedByID");

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.Property(e => e.SupplierId).HasColumnName("SupplierID");

                entity.Property(e => e.Total).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.Vat).HasColumnType("decimal(12, 2)");

                entity.HasOne(d => d.ApproveBy)
                    .WithMany(p => p.InternalOrderApproveBies)
                    .HasForeignKey(d => d.ApproveById)
                    .HasConstraintName("FK_InternalOrder_User2");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.InternalOrders)
                    .HasForeignKey(d => d.CompanyId)
                    .HasConstraintName("FK_InternalOrder_Company");

                entity.HasOne(d => d.PlacedBy)
                    .WithMany(p => p.InternalOrderPlacedBies)
                    .HasForeignKey(d => d.PlacedById)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_InternalOrder_User1");

                entity.HasOne(d => d.PlantLocation)
                    .WithMany(p => p.InternalOrders)
                    .HasForeignKey(d => d.PlantLocationId)
                    .HasConstraintName("FK_InternalOrder_PlantLocation");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.InternalOrders)
                    .HasForeignKey(d => d.ProjectId)
                    .HasConstraintName("FK_InternalOrder_Project");

                entity.HasOne(d => d.RequestedBy)
                    .WithMany(p => p.InternalOrderRequestedBies)
                    .HasForeignKey(d => d.RequestedById)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_InternalOrder_User");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.InternalOrders)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_InternalOrder_InternalOrderStatus");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.InternalOrders)
                    .HasForeignKey(d => d.SupplierId)
                    .HasConstraintName("FK_InternalOrder_Supplier");
            });

            modelBuilder.Entity<InternalOrderItem>(entity =>
            {
                entity.ToTable("InternalOrderItem");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");

                entity.Property(e => e.GlcodeId).HasColumnName("GLCodeID");

                entity.Property(e => e.InternalOrderId).HasColumnName("InternalOrderID");

                entity.Property(e => e.OriginalValue).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.StockId).HasColumnName("StockID");

                entity.Property(e => e.Total).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.UomPrice).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.Uomid).HasColumnName("UOMID");

                entity.Property(e => e.Value).HasColumnType("decimal(12, 2)");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.InternalOrderItems)
                    .HasForeignKey(d => d.DepartmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_InternalOrderItem_Department");

                entity.HasOne(d => d.Glcode)
                    .WithMany(p => p.InternalOrderItems)
                    .HasForeignKey(d => d.GlcodeId)
                    .HasConstraintName("FK_InternalOrderItem_GLCode");

                entity.HasOne(d => d.InternalOrder)
                    .WithMany(p => p.InternalOrderItems)
                    .HasForeignKey(d => d.InternalOrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_InternalOrderItem_InternalOrder");

                entity.HasOne(d => d.Stock)
                    .WithMany(p => p.InternalOrderItems)
                    .HasForeignKey(d => d.StockId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_InternalOrderItem_Stock");

                entity.HasOne(d => d.Uom)
                    .WithMany(p => p.InternalOrderItems)
                    .HasForeignKey(d => d.Uomid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_InternalOrderItem_UnitOfMeasurement");
            });

            modelBuilder.Entity<InternalOrderStatus>(entity =>
            {
                entity.ToTable("InternalOrderStatus");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Invoice>(entity =>
            {
                entity.ToTable("Invoice");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.InternalOrderId).HasColumnName("InternalOrderID");

                entity.Property(e => e.InvoiceNumber)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Total).HasColumnType("decimal(12, 2)");

                entity.HasOne(d => d.InternalOrder)
                    .WithMany(p => p.Invoices)
                    .HasForeignKey(d => d.InternalOrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Invoice_InternalOrder");
            });

            modelBuilder.Entity<InvoiceItem>(entity =>
            {
                entity.ToTable("InvoiceItem");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.GrnitemId).HasColumnName("GRNItemID");

                entity.Property(e => e.InvoiceId).HasColumnName("InvoiceID");

                entity.Property(e => e.InvoicePrice).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.ItemValue).HasColumnType("decimal(12, 2)");

                entity.HasOne(d => d.Grnitem)
                    .WithMany(p => p.InvoiceItems)
                    .HasForeignKey(d => d.GrnitemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_InvoiceItem_GRNItem");

                entity.HasOne(d => d.Invoice)
                    .WithMany(p => p.InvoiceItems)
                    .HasForeignKey(d => d.InvoiceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_InvoiceItem_Invoice");
            });

            modelBuilder.Entity<InvoiceOnceOffItem>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.GrnonceOffItemId).HasColumnName("GRNOnceOffItemID");

                entity.Property(e => e.InvoiceId).HasColumnName("InvoiceID");

                entity.Property(e => e.InvoicePrice).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.ItemValue).HasColumnType("decimal(12, 2)");

                entity.HasOne(d => d.GrnonceOffItem)
                    .WithMany(p => p.InvoiceOnceOffItems)
                    .HasForeignKey(d => d.GrnonceOffItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_InvoiceOnceOffItems_GRNOnceOffItems");

                entity.HasOne(d => d.Invoice)
                    .WithMany(p => p.InvoiceOnceOffItems)
                    .HasForeignKey(d => d.InvoiceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_InvoiceOnceOffItems_Invoice");
            });

            modelBuilder.Entity<InvoiceServiceItem>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.InvoiceId).HasColumnName("InvoiceID");

                entity.Property(e => e.InvoicePrice).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.ItemValue).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.ServiceId).HasColumnName("ServiceID");

                entity.HasOne(d => d.Invoice)
                    .WithMany(p => p.InvoiceServiceItems)
                    .HasForeignKey(d => d.InvoiceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_InvoiceServiceItems_Invoice");

                entity.HasOne(d => d.Service)
                    .WithMany(p => p.InvoiceServiceItems)
                    .HasForeignKey(d => d.ServiceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_InvoiceServiceItems_Services");
            });

            modelBuilder.Entity<LatestGrn>(entity =>
            {
                entity.ToTable("LatestGrn");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.InternalOrderId).HasColumnName("InternalOrderID");

                entity.HasOne(d => d.InternalOrder)
                    .WithMany(p => p.LatestGrns)
                    .HasForeignKey(d => d.InternalOrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LatestGrn_InternalOrder");
            });

            modelBuilder.Entity<Law>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Law1)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Law");
            });

            modelBuilder.Entity<MaritalStatus>(entity =>
            {
                entity.ToTable("MaritalStatus");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<OnceOffItem>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");

                entity.Property(e => e.Description).HasMaxLength(50);

                entity.Property(e => e.GlcodeId).HasColumnName("GLCodeID");

                entity.Property(e => e.InternalOrderId).HasColumnName("InternalOrderID");

                entity.Property(e => e.Total).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.Uomid).HasColumnName("UOMID");

                entity.Property(e => e.Value).HasColumnType("decimal(12, 2)");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.OnceOffItems)
                    .HasForeignKey(d => d.DepartmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OnceOffItems_Department");

                entity.HasOne(d => d.Glcode)
                    .WithMany(p => p.OnceOffItems)
                    .HasForeignKey(d => d.GlcodeId)
                    .HasConstraintName("FK_OnceOffItems_GLCode");

                entity.HasOne(d => d.InternalOrder)
                    .WithMany(p => p.OnceOffItems)
                    .HasForeignKey(d => d.InternalOrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OnceOffItems_InternalOrder");

                entity.HasOne(d => d.Uom)
                    .WithMany(p => p.OnceOffItems)
                    .HasForeignKey(d => d.Uomid)
                    .HasConstraintName("FK_OnceOffItems_UnitOfMeasurement");
            });

            modelBuilder.Entity<PaymentInterval>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Intervals)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<PaymentMethod>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Permission>(entity =>
            {
                entity.ToTable("Permission");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.Component)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Page)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<PlantLocation>(entity =>
            {
                entity.ToTable("PlantLocation");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AddressId).HasColumnName("AddressID");

                entity.Property(e => e.DefaultStoreId).HasColumnName("DefaultStoreID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.PlantLocations)
                    .HasForeignKey(d => d.AddressId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PlantLocation_Address");

                entity.HasOne(d => d.DefaultStore)
                    .WithMany(p => p.PlantLocations)
                    .HasForeignKey(d => d.DefaultStoreId)
                    .HasConstraintName("FK_PlantLocation_Store");
            });

            modelBuilder.Entity<PriceIncrease>(entity =>
            {
                entity.ToTable("PriceIncrease");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Comment).IsRequired();

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Increase).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.IncreaseOrigin).HasMaxLength(50);

                entity.Property(e => e.IncreaseTypeId).HasColumnName("IncreaseTypeID");

                entity.Property(e => e.PriceLookUpId).HasColumnName("PriceLookUpID");

                entity.HasOne(d => d.IncreaseType)
                    .WithMany(p => p.PriceIncreases)
                    .HasForeignKey(d => d.IncreaseTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PriceIncrease_IncreaseType1");

                entity.HasOne(d => d.PriceLookUp)
                    .WithMany(p => p.PriceIncreases)
                    .HasForeignKey(d => d.PriceLookUpId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PriceIncrease_PriceLookup");
            });

            modelBuilder.Entity<PriceLookup>(entity =>
            {
                entity.ToTable("PriceLookup");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.StockId).HasColumnName("StockID");

                entity.Property(e => e.SupplierId).HasColumnName("SupplierID");

                entity.HasOne(d => d.Stock)
                    .WithMany(p => p.PriceLookups)
                    .HasForeignKey(d => d.StockId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PriceLookup_Stock");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.PriceLookups)
                    .HasForeignKey(d => d.SupplierId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PriceLookup_Supplier");
            });

            modelBuilder.Entity<PriceLookupLog>(entity =>
            {
                entity.ToTable("PriceLookupLog");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Application).IsRequired();

                entity.Property(e => e.Currency)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.InternalProductName).IsRequired();

                entity.Property(e => e.NewPrice).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.OldPrice).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Stock).IsRequired();

                entity.Property(e => e.Supplier)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Uom)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("UOM");

                entity.Property(e => e.Username).IsRequired();
            });

            modelBuilder.Entity<Printer>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Location)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");

                entity.Property(e => e.ProductCode)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Quantity).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.DepartmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Product_Department");
            });

            modelBuilder.Entity<ProductItem>(entity =>
            {
                entity.ToTable("ProductItem");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ItemId).HasColumnName("ItemID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.Quantity).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.ProductItemItems)
                    .HasForeignKey(d => d.ItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductItem_Product1");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductItemProducts)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductItem_Product");
            });

            modelBuilder.Entity<ProductStock>(entity =>
            {
                entity.ToTable("ProductStock");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.QtyPerSquareMeter).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Quantity).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.SquaresUsed).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.StockId).HasColumnName("StockID");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductStocks)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductStock_Product");

                entity.HasOne(d => d.Stock)
                    .WithMany(p => p.ProductStocks)
                    .HasForeignKey(d => d.StockId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductStock_Stock");
            });

            modelBuilder.Entity<ProductionOrder>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");
            });

            modelBuilder.Entity<Project>(entity =>
            {
                entity.ToTable("Project");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Budget).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.Name).IsRequired();

                entity.Property(e => e.ProjectStatusId).HasColumnName("ProjectStatusID");

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.HasOne(d => d.ProjectStatus)
                    .WithMany(p => p.Projects)
                    .HasForeignKey(d => d.ProjectStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Project_ProjectStatus");
            });

            modelBuilder.Entity<ProjectStatus>(entity =>
            {
                entity.ToTable("ProjectStatus");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Quote>(entity =>
            {
                entity.ToTable("Quote");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.OnDelivery).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.OnOrder).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.PlacedById).HasColumnName("PlacedByID");

                entity.Property(e => e.QuoteStatusId).HasColumnName("QuoteStatusID");

                entity.Property(e => e.RequestById).HasColumnName("RequestByID");

                entity.Property(e => e.SubmissionDate).HasColumnType("datetime");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Quotes)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Quote_Customer");

                entity.HasOne(d => d.PlacedBy)
                    .WithMany(p => p.QuotePlacedBies)
                    .HasForeignKey(d => d.PlacedById)
                    .HasConstraintName("FK_Quote_User1");

                entity.HasOne(d => d.QuoteStatus)
                    .WithMany(p => p.Quotes)
                    .HasForeignKey(d => d.QuoteStatusId)
                    .HasConstraintName("FK_Quote_QuoteStatus");

                entity.HasOne(d => d.RequestBy)
                    .WithMany(p => p.QuoteRequestBies)
                    .HasForeignKey(d => d.RequestById)
                    .HasConstraintName("FK_Quote_User");
            });

            modelBuilder.Entity<QuoteItem>(entity =>
            {
                entity.ToTable("QuoteItem");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Length).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.Price).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.QuoteId).HasColumnName("QuoteID");

                entity.Property(e => e.Width).HasColumnType("decimal(12, 2)");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.QuoteItems)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_QuoteItem_Product");

                entity.HasOne(d => d.Quote)
                    .WithMany(p => p.QuoteItems)
                    .HasForeignKey(d => d.QuoteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_QuoteItem_Quote");
            });

            modelBuilder.Entity<QuoteRevision>(entity =>
            {
                entity.ToTable("QuoteRevision");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.QuoteId).HasColumnName("QuoteID");

                entity.HasOne(d => d.Quote)
                    .WithMany(p => p.QuoteRevisions)
                    .HasForeignKey(d => d.QuoteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_QuoteRevision_Quote");
            });

            modelBuilder.Entity<QuoteStatus>(entity =>
            {
                entity.ToTable("QuoteStatus");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<QuoteTransport>(entity =>
            {
                entity.ToTable("QuoteTransport");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Price).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.QuoteId).HasColumnName("QuoteID");

                entity.HasOne(d => d.Quote)
                    .WithMany(p => p.QuoteTransports)
                    .HasForeignKey(d => d.QuoteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_QuoteTransport_Quote");
            });

            modelBuilder.Entity<Race>(entity =>
            {
                entity.ToTable("Race");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Race1)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Race");
            });

            modelBuilder.Entity<Recipe>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Quantity).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.StockComponentId).HasColumnName("StockComponentID");

                entity.Property(e => e.StockId).HasColumnName("StockID");

                entity.HasOne(d => d.StockComponent)
                    .WithMany(p => p.RecipeStockComponents)
                    .HasForeignKey(d => d.StockComponentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Recipes_Stock1");

                entity.HasOne(d => d.Stock)
                    .WithMany(p => p.RecipeStocks)
                    .HasForeignKey(d => d.StockId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Recipes_Stock");
            });

            modelBuilder.Entity<ReturnToSupplier>(entity =>
            {
                entity.ToTable("ReturnToSupplier");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.DateReturned).HasColumnType("datetime");

                entity.Property(e => e.InternalProductName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.InternalSku)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.ReturnedQuantity).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.SupplierName)
                    .IsRequired()
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<RolePermission>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.PermissionId).HasColumnName("PermissionID");

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.HasOne(d => d.Permission)
                    .WithMany(p => p.RolePermissions)
                    .HasForeignKey(d => d.PermissionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RolePermissions_Permission");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.RolePermissions)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RolePermissions_Role");
            });

            modelBuilder.Entity<RoleTemplate>(entity =>
            {
                entity.ToTable("RoleTemplate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<ScannerConfig>(entity =>
            {
                entity.ToTable("ScannerConfig");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DeviceId).IsRequired();

                entity.Property(e => e.DeviceName).IsRequired();

                entity.Property(e => e.PlantLocationId).HasColumnName("PlantLocationID");

                entity.HasOne(d => d.PlantLocation)
                    .WithMany(p => p.ScannerConfigs)
                    .HasForeignKey(d => d.PlantLocationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ScannerConfig_PlantLocation");
            });

            modelBuilder.Entity<Service>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");

                entity.Property(e => e.GlcodeId).HasColumnName("GLCodeID");

                entity.Property(e => e.InternalOrderId).HasColumnName("InternalOrderID");

                entity.Property(e => e.Value).HasColumnType("decimal(12, 2)");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.Services)
                    .HasForeignKey(d => d.DepartmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Services_Department");

                entity.HasOne(d => d.Glcode)
                    .WithMany(p => p.Services)
                    .HasForeignKey(d => d.GlcodeId)
                    .HasConstraintName("FK_Services_GLCode");

                entity.HasOne(d => d.InternalOrder)
                    .WithMany(p => p.Services)
                    .HasForeignKey(d => d.InternalOrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Services_InternalOrder");
            });

            modelBuilder.Entity<ShelfLifeType>(entity =>
            {
                entity.ToTable("ShelfLifeType");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Stock>(entity =>
            {
                entity.ToTable("Stock");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BinId).HasColumnName("BinID");

                entity.Property(e => e.CalculatedRatio).HasColumnType("decimal(20, 10)");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ComparisonSecondValue).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.CurrentPrice).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.DeductedValue).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.DefaultDepartmentId).HasColumnName("DefaultDepartmentID");

                entity.Property(e => e.DefaultLocationId).HasColumnName("DefaultLocationID");

                entity.Property(e => e.DefaultStoreId).HasColumnName("DefaultStoreID");

                entity.Property(e => e.InternalProductName).HasMaxLength(50);

                entity.Property(e => e.InternalSku)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ItemPrice).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.ItemQuantity).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.MinThreshold).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.PackQuantity).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.ProductDimensionsHeight).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.ProductDimensionsLength).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.ProductDimensionsWidth).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.ProductWeight).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.SecondaryUomid).HasColumnName("SecondaryUOMID");

                entity.Property(e => e.ShelfLifeTypeId).HasColumnName("ShelfLifeTypeID");

                entity.Property(e => e.ShippingDimensionsHeight).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.ShippingDimensionsLength).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.ShippingDimensionsWidth).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.ShippingWeight).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.StockCategoryId).HasColumnName("StockCategoryID");

                entity.Property(e => e.StockGroupId).HasColumnName("StockGroupID");

                entity.Property(e => e.StorageTypeId).HasColumnName("StorageTypeID");

                entity.Property(e => e.SupplierId).HasColumnName("SupplierID");

                entity.Property(e => e.Uomid).HasColumnName("UOMID");

                entity.HasOne(d => d.Bin)
                    .WithMany(p => p.Stocks)
                    .HasForeignKey(d => d.BinId)
                    .HasConstraintName("FK_Stock_Bin");

                entity.HasOne(d => d.DefaultDepartment)
                    .WithMany(p => p.Stocks)
                    .HasForeignKey(d => d.DefaultDepartmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Stock_Department");

                entity.HasOne(d => d.DefaultLocation)
                    .WithMany(p => p.Stocks)
                    .HasForeignKey(d => d.DefaultLocationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Stock_PlantLocation");

                entity.HasOne(d => d.DefaultStore)
                    .WithMany(p => p.Stocks)
                    .HasForeignKey(d => d.DefaultStoreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Stock_Store");

                entity.HasOne(d => d.SecondaryUom)
                    .WithMany(p => p.StockSecondaryUoms)
                    .HasForeignKey(d => d.SecondaryUomid)
                    .HasConstraintName("FK_Stock_UnitOfMeasurement1");

                entity.HasOne(d => d.ShelfLifeType)
                    .WithMany(p => p.Stocks)
                    .HasForeignKey(d => d.ShelfLifeTypeId)
                    .HasConstraintName("FK_Stock_ShelfLifeType");

                entity.HasOne(d => d.StockCategory)
                    .WithMany(p => p.Stocks)
                    .HasForeignKey(d => d.StockCategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Stock_StockCategory");

                entity.HasOne(d => d.StockGroup)
                    .WithMany(p => p.Stocks)
                    .HasForeignKey(d => d.StockGroupId)
                    .HasConstraintName("FK_Stock_StockGroup");

                entity.HasOne(d => d.StorageType)
                    .WithMany(p => p.Stocks)
                    .HasForeignKey(d => d.StorageTypeId)
                    .HasConstraintName("FK_Stock_StorageType");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.Stocks)
                    .HasForeignKey(d => d.SupplierId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Stock_Supplier");

                entity.HasOne(d => d.Uom)
                    .WithMany(p => p.StockUoms)
                    .HasForeignKey(d => d.Uomid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Stock_UnitOfMeasurement");
            });

            modelBuilder.Entity<StockCategory>(entity =>
            {
                entity.ToTable("StockCategory");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<StockGroup>(entity =>
            {
                entity.ToTable("StockGroup");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.MinThreshold).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<StockLog>(entity =>
            {
                entity.ToTable("StockLog");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Action)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Department).HasMaxLength(50);

                entity.Property(e => e.Location).HasMaxLength(50);

                entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.ProductCodeName).IsRequired();

                entity.Property(e => e.Quantity).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.StockCategory).HasMaxLength(50);

                entity.Property(e => e.StockGroup).HasMaxLength(50);

                entity.Property(e => e.Store).HasMaxLength(50);

                entity.Property(e => e.SupplierCurrency).HasMaxLength(50);

                entity.Property(e => e.Uom)
                    .HasMaxLength(50)
                    .HasColumnName("UOM");
            });

            modelBuilder.Entity<StockQuantity>(entity =>
            {
                entity.ToTable("StockQuantity");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateModified).HasColumnType("datetime");

                entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");

                entity.Property(e => e.GrnNumber).HasMaxLength(50);

                entity.Property(e => e.ItemQuantity).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.LocationId).HasColumnName("LocationID");

                entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.StockId).HasColumnName("StockID");

                entity.Property(e => e.StoreId).HasColumnName("StoreID");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.StockQuantities)
                    .HasForeignKey(d => d.DepartmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StockQuantity_Department");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.StockQuantities)
                    .HasForeignKey(d => d.LocationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StockQuantity_PlantLocation");

                entity.HasOne(d => d.Stock)
                    .WithMany(p => p.StockQuantities)
                    .HasForeignKey(d => d.StockId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StockQuantity_Stock");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.StockQuantities)
                    .HasForeignKey(d => d.StoreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StockQuantity_Store");
            });

            modelBuilder.Entity<Stocktake>(entity =>
            {
                entity.ToTable("Stocktake");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CapturedQty).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.CurrentQty).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.PlantLocationId).HasColumnName("PlantLocationID");

                entity.Property(e => e.StockId).HasColumnName("StockID");

                entity.Property(e => e.StockTakeDate).HasColumnType("datetime");

                entity.Property(e => e.StoreId).HasColumnName("StoreID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.PlantLocation)
                    .WithMany(p => p.Stocktakes)
                    .HasForeignKey(d => d.PlantLocationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Stocktake_PlantLocation");

                entity.HasOne(d => d.Stock)
                    .WithMany(p => p.Stocktakes)
                    .HasForeignKey(d => d.StockId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Stocktake_Stock");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.Stocktakes)
                    .HasForeignKey(d => d.StoreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Stocktake_Store");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Stocktakes)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Stocktake_User");
            });

            modelBuilder.Entity<StocktakeCounter>(entity =>
            {
                entity.ToTable("StocktakeCounter");

                entity.Property(e => e.Id).HasColumnName("ID");
            });

            modelBuilder.Entity<StocktakeCycle>(entity =>
            {
                entity.ToTable("StocktakeCycle");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.StocktakeName).IsRequired();
            });

            modelBuilder.Entity<StocktakeLog>(entity =>
            {
                entity.ToTable("StocktakeLog");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Actions).HasMaxLength(50);

                entity.Property(e => e.ApproveDate).HasColumnType("datetime");

                entity.Property(e => e.CountQty).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.CurrentQty).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.PlantLocationName).IsRequired();

                entity.Property(e => e.RecountDate).HasColumnType("datetime");

                entity.Property(e => e.StockFullName).IsRequired();

                entity.Property(e => e.StockTakeDate).HasColumnType("datetime");

                entity.Property(e => e.StoreName).IsRequired();
            });

            modelBuilder.Entity<StocktakeReport>(entity =>
            {
                entity.ToTable("StocktakeReport");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ClosingQty).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.OpeningQty).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.PlantLocationName).IsRequired();

                entity.Property(e => e.StockFullName).IsRequired();

                entity.Property(e => e.StocktakeCycleId).HasColumnName("StocktakeCycleID");

                entity.Property(e => e.StoreName).IsRequired();

                entity.HasOne(d => d.StocktakeCycle)
                    .WithMany(p => p.StocktakeReports)
                    .HasForeignKey(d => d.StocktakeCycleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StocktakeReport_StocktakeCycle");
            });

            modelBuilder.Entity<StorageType>(entity =>
            {
                entity.ToTable("StorageType");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<Store>(entity =>
            {
                entity.ToTable("Store");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.PlantLocationId).HasColumnName("PlantLocationID");

                entity.Property(e => e.StoreTypeId).HasColumnName("StoreTypeID");

                entity.HasOne(d => d.PlantLocation)
                    .WithMany(p => p.Stores)
                    .HasForeignKey(d => d.PlantLocationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Store_PlantLocation");

                entity.HasOne(d => d.StoreType)
                    .WithMany(p => p.Stores)
                    .HasForeignKey(d => d.StoreTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Store_StoreType");
            });

            modelBuilder.Entity<StoreType>(entity =>
            {
                entity.ToTable("StoreType");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Supplier>(entity =>
            {
                entity.ToTable("Supplier");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AccountNumber).HasMaxLength(50);

                entity.Property(e => e.AddressId).HasColumnName("AddressID");

                entity.Property(e => e.BankNameId).HasColumnName("BankNameID");

                entity.Property(e => e.BranchCode).HasMaxLength(50);

                entity.Property(e => e.CompanyName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.CreditLimit).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.CreditTerms)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CurrencyId).HasColumnName("CurrencyID");

                entity.Property(e => e.Discount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.PaymentMethodId).HasColumnName("PaymentMethodID");

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.Suppliers)
                    .HasForeignKey(d => d.AddressId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Supplier_Address");

                entity.HasOne(d => d.BankName)
                    .WithMany(p => p.Suppliers)
                    .HasForeignKey(d => d.BankNameId)
                    .HasConstraintName("FK_Supplier_BankName");

                entity.HasOne(d => d.Currency)
                    .WithMany(p => p.Suppliers)
                    .HasForeignKey(d => d.CurrencyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Supplier_Currency");

                entity.HasOne(d => d.PaymentMethod)
                    .WithMany(p => p.Suppliers)
                    .HasForeignKey(d => d.PaymentMethodId)
                    .HasConstraintName("FK_Supplier_PaymentMethods");
            });

            modelBuilder.Entity<TypeEmployment>(entity =>
            {
                entity.ToTable("TypeEmployment");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<UnitOfMeasurement>(entity =>
            {
                entity.ToTable("UnitOfMeasurement");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.LastActivity).HasColumnType("date");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Password).HasMaxLength(50);

                entity.Property(e => e.ResetDateTime).HasColumnType("datetime");

                entity.Property(e => e.Surname)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<UserDetail>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AccountNumber)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.BankNameId).HasColumnName("BankNameID");

                entity.Property(e => e.BaseSalaryPerMonth).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Birthday).HasColumnType("date");

                entity.Property(e => e.BranchCode)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ContactNumber)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.EmployeeNumber)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.EmployeePositionId).HasColumnName("EmployeePositionID");

                entity.Property(e => e.GenderId).HasColumnName("GenderID");

                entity.Property(e => e.HighestQualification).IsRequired();

                entity.Property(e => e.HomeAddressId).HasColumnName("HomeAddressID");

                entity.Property(e => e.HourlyRate).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Idnumber)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("IDNumber");

                entity.Property(e => e.IncomeTaxNumber).HasMaxLength(50);

                entity.Property(e => e.LawsId).HasColumnName("LawsID");

                entity.Property(e => e.MaritalStatusId).HasColumnName("MaritalStatusID");

                entity.Property(e => e.NextOfKinContactNumber)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.NextOfKinName).IsRequired();

                entity.Property(e => e.PaymentIntervalsId).HasColumnName("PaymentIntervalsID");

                entity.Property(e => e.RaceId).HasColumnName("RaceID");

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.Property(e => e.TypeOfEmploymentId).HasColumnName("TypeOfEmploymentID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.BankName)
                    .WithMany(p => p.UserDetails)
                    .HasForeignKey(d => d.BankNameId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserDetails_BankName");

                entity.HasOne(d => d.EmployeePosition)
                    .WithMany(p => p.UserDetails)
                    .HasForeignKey(d => d.EmployeePositionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserDetails_EmployeePosition1");

                entity.HasOne(d => d.Gender)
                    .WithMany(p => p.UserDetails)
                    .HasForeignKey(d => d.GenderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserDetails_Gender");

                entity.HasOne(d => d.HomeAddress)
                    .WithMany(p => p.UserDetails)
                    .HasForeignKey(d => d.HomeAddressId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserDetails_Address");

                entity.HasOne(d => d.Laws)
                    .WithMany(p => p.UserDetails)
                    .HasForeignKey(d => d.LawsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserDetails_Laws");

                entity.HasOne(d => d.MaritalStatus)
                    .WithMany(p => p.UserDetails)
                    .HasForeignKey(d => d.MaritalStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserDetails_MaritalStatus");

                entity.HasOne(d => d.PaymentIntervals)
                    .WithMany(p => p.UserDetails)
                    .HasForeignKey(d => d.PaymentIntervalsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserDetails_PaymentIntervals");

                entity.HasOne(d => d.Race)
                    .WithMany(p => p.UserDetails)
                    .HasForeignKey(d => d.RaceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserDetails_Race");

                entity.HasOne(d => d.TypeOfEmployment)
                    .WithMany(p => p.UserDetails)
                    .HasForeignKey(d => d.TypeOfEmploymentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserDetails_TypeEmployment");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserDetails)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserDetails_User");
            });

            modelBuilder.Entity<UserPermission>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.PermissionId).HasColumnName("PermissionID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Permission)
                    .WithMany(p => p.UserPermissions)
                    .HasForeignKey(d => d.PermissionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserPermissions_Permission");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserPermissions)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserPermissions_User");
            });

            modelBuilder.Entity<Vat>(entity =>
            {
                entity.ToTable("VAT");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Vat1)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("Vat");
            });

            modelBuilder.Entity<VatStored>(entity =>
            {
                entity.ToTable("VatStored");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.GlcodeId).HasColumnName("GLCodeID");

                entity.Property(e => e.VatAmount).HasColumnType("decimal(12, 2)");

                entity.HasOne(d => d.Glcode)
                    .WithMany(p => p.VatStoreds)
                    .HasForeignKey(d => d.GlcodeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VatStored_GLCode");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
