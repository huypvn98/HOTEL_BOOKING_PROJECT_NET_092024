﻿// <auto-generated />
using System;
using BookingHotel.Core.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BookingHotel.Core.Migrations
{
    [DbContext(typeof(HotelBookingDbContext))]
    partial class HotelBookingDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BackendAPIBookingHotel.Model.Address", b =>
                {
                    b.Property<int>("AddressID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AddressID"));

                    b.Property<string>("AddressType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsPrimary")
                        .HasColumnType("bit");

                    b.Property<int?>("PersonID")
                        .HasColumnType("int");

                    b.Property<string>("StreetAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AddressID");

                    b.HasIndex("PersonID");

                    b.ToTable("BE072024_HB_Addresses");
                });

            modelBuilder.Entity("BackendAPIBookingHotel.Model.Admin", b =>
                {
                    b.Property<int?>("AdminID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("AdminID"));

                    b.Property<string>("AdminSpecificInfo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("AssignedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Position")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AdminID");

                    b.ToTable("BE072024_HB_Admins");
                });

            modelBuilder.Entity("BackendAPIBookingHotel.Model.BedRoom", b =>
                {
                    b.Property<int>("RoomID")
                        .HasColumnType("int")
                        .HasColumnOrder(0);

                    b.Property<int>("BedID")
                        .HasColumnType("int")
                        .HasColumnOrder(1);

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("RoomID", "BedID");

                    b.HasIndex("BedID");

                    b.ToTable("BE072024_HB_BedRooms");
                });

            modelBuilder.Entity("BackendAPIBookingHotel.Model.Booking", b =>
                {
                    b.Property<int>("BookingID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BookingID"));

                    b.Property<string>("BookingStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CheckInDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("CheckOutDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("CustomerID")
                        .HasColumnType("int");

                    b.Property<int?>("DepositID")
                        .HasColumnType("int");

                    b.Property<DateTime>("FromDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("RoomID")
                        .HasColumnType("int");

                    b.Property<DateTime>("ToDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool?>("isActive")
                        .HasColumnType("bit");

                    b.HasKey("BookingID");

                    b.HasIndex("CustomerID");

                    b.HasIndex("RoomID");

                    b.ToTable("BE072024_HB_Bookings");
                });

            modelBuilder.Entity("BackendAPIBookingHotel.Model.BookingDetail", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int>("BookingID")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateService")
                        .HasColumnType("datetime2");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int>("ServiceID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("BookingID");

                    b.HasIndex("ServiceID");

                    b.ToTable("BE072024_HB_BookingDetails");
                });

            modelBuilder.Entity("BackendAPIBookingHotel.Model.CancellationPolicy", b =>
                {
                    b.Property<int>("CancellationPolicyID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CancellationPolicyID"));

                    b.Property<int>("CancellationPeriod")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("PenaltyFee")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("RefundPercentage")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("CancellationPolicyID");

                    b.ToTable("BE072024_HB_CancellationPolicies");
                });

            modelBuilder.Entity("BackendAPIBookingHotel.Model.Customer", b =>
                {
                    b.Property<int>("CustomerID")
                        .HasColumnType("int");

                    b.Property<string>("CustomerSpecificInfo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("RegistrationDate")
                        .HasColumnType("datetime2");

                    b.HasKey("CustomerID");

                    b.ToTable("BE072024_HB_Customers");
                });

            modelBuilder.Entity("BackendAPIBookingHotel.Model.Deposit", b =>
                {
                    b.Property<int>("DepositID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DepositID"));

                    b.Property<int>("BookingID")
                        .HasColumnType("int");

                    b.Property<int>("CancellationPolicyID")
                        .HasColumnType("int");

                    b.Property<int>("DepositAmount")
                        .HasColumnType("int");

                    b.Property<DateTime>("DepositDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("DepositStatus")
                        .HasColumnType("int");

                    b.HasKey("DepositID");

                    b.HasIndex("BookingID");

                    b.HasIndex("CancellationPolicyID");

                    b.ToTable("BE072024_HB_Deposits");
                });

            modelBuilder.Entity("BackendAPIBookingHotel.Model.Email", b =>
                {
                    b.Property<int>("EmailID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EmailID"));

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EmailType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsPrimary")
                        .HasColumnType("bit");

                    b.Property<int>("PersonID")
                        .HasColumnType("int");

                    b.HasKey("EmailID");

                    b.HasIndex("PersonID");

                    b.ToTable("BE072024_HB_Emails");
                });

            modelBuilder.Entity("BackendAPIBookingHotel.Model.Hotel", b =>
                {
                    b.Property<int>("HotelID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("HotelID"));

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HotelName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("UrlImage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("isActive")
                        .HasColumnType("bit");

                    b.HasKey("HotelID");

                    b.ToTable("BE072024_HB_Hotels");
                });

            modelBuilder.Entity("BackendAPIBookingHotel.Model.ImageRooms", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("NameFileImg")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoomID")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoomID");

                    b.ToTable("BE072024_HB_ImageRooms");
                });

            modelBuilder.Entity("BackendAPIBookingHotel.Model.Invoice", b =>
                {
                    b.Property<int>("InvoiceID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("InvoiceID"));

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<int>("BookingID")
                        .HasColumnType("int");

                    b.Property<DateTime>("InvoiceDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("PaymentMethod")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PaymentStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("InvoiceID");

                    b.HasIndex("BookingID");

                    b.ToTable("BE072024_HB_Invoices");
                });

            modelBuilder.Entity("BackendAPIBookingHotel.Model.Person", b =>
                {
                    b.Property<int>("PersonID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PersonID"));

                    b.Property<DateTime>("DOB")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PersonID");

                    b.ToTable("BE072024_HB_People");
                });

            modelBuilder.Entity("BackendAPIBookingHotel.Model.Phone", b =>
                {
                    b.Property<int>("PhoneID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PhoneID"));

                    b.Property<bool>("IsPrimary")
                        .HasColumnType("bit");

                    b.Property<int>("PersonID")
                        .HasColumnType("int");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PhoneID");

                    b.HasIndex("PersonID");

                    b.ToTable("BE072024_HB_Phones");
                });

            modelBuilder.Entity("BackendAPIBookingHotel.Model.Role", b =>
                {
                    b.Property<int>("RoleID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RoleID"));

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PermissionId")
                        .HasColumnType("int");

                    b.Property<string>("RoleName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RoleID");

                    b.HasIndex("PermissionId");

                    b.ToTable("BE072024_HB_Roles");

                    b.HasData(
                        new
                        {
                            RoleID = 1,
                            Description = "Administrator role with full permissions",
                            RoleName = "Admin"
                        },
                        new
                        {
                            RoleID = 2,
                            Description = "Regular user with limited permissions",
                            RoleName = "User"
                        },
                        new
                        {
                            RoleID = 3,
                            Description = "Customer role with permissions to book and view hotels",
                            RoleName = "Customer"
                        },
                        new
                        {
                            RoleID = 4,
                            Description = "Staff role with permissions to manage hotel operations",
                            RoleName = "Staff"
                        });
                });

            modelBuilder.Entity("BackendAPIBookingHotel.Model.Room", b =>
                {
                    b.Property<int>("RoomID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RoomID"));

                    b.Property<int>("HotelID")
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<int>("RoomDetailID")
                        .HasColumnType("int");

                    b.Property<string>("RoomNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoomSquare")
                        .HasColumnType("int");

                    b.HasKey("RoomID");

                    b.HasIndex("HotelID");

                    b.HasIndex("RoomDetailID");

                    b.ToTable("BE072024_HB_Rooms");
                });

            modelBuilder.Entity("BackendAPIBookingHotel.Model.RoomDetail", b =>
                {
                    b.Property<int>("RoomDetailID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RoomDetailID"));

                    b.Property<int>("IsAvailable")
                        .HasColumnType("int");

                    b.Property<int>("PricePerNight")
                        .HasColumnType("int");

                    b.Property<string>("RoomFittings")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoomType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoomView")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RoomDetailID");

                    b.ToTable("BE072024_HB_RoomDetails");
                });

            modelBuilder.Entity("BackendAPIBookingHotel.Model.Service", b =>
                {
                    b.Property<int>("ServiceID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ServiceID"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("IsAvailable")
                        .HasColumnType("bit");

                    b.Property<decimal?>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("ServiceType")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ServiceID");

                    b.ToTable("BE072024_HB_Services");
                });

            modelBuilder.Entity("BackendAPIBookingHotel.Model.Staff", b =>
                {
                    b.Property<int?>("StaffID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("StaffID"));

                    b.Property<string>("HireDate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("HotelID")
                        .HasColumnType("int");

                    b.Property<int?>("Position")
                        .HasColumnType("int");

                    b.HasKey("StaffID");

                    b.HasIndex("HotelID");

                    b.ToTable("BE072024_HB_Staff");
                });

            modelBuilder.Entity("BackendAPIBookingHotel.Model.User", b =>
                {
                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RefreshToken_ExpriredTime")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("isActive")
                        .HasColumnType("bit");

                    b.HasKey("UserID");

                    b.ToTable("BE072024_HB_Users");
                });

            modelBuilder.Entity("BackendAPIBookingHotel.Model.UserRole", b =>
                {
                    b.Property<int>("UserRoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserRoleId"));

                    b.Property<int>("RoleID")
                        .HasColumnType("int");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("UserRoleId");

                    b.HasIndex("RoleID");

                    b.HasIndex("UserID");

                    b.ToTable("BE072024_HB_UserRoles");
                });

            modelBuilder.Entity("BookingHotel.Core.Models.Bed", b =>
                {
                    b.Property<int>("BedID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BedID"));

                    b.Property<string>("BedName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BedID");

                    b.ToTable("BE072024_HB_Beds");
                });

            modelBuilder.Entity("BookingHotel.Core.Models.Permission", b =>
                {
                    b.Property<int>("PermissionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PermissionId"));

                    b.Property<string>("Approve")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Create")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Delete")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Insert")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Screen_Url")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PermissionId");

                    b.ToTable("BE072024_HB_Permission");
                });

            modelBuilder.Entity("BackendAPIBookingHotel.Model.Address", b =>
                {
                    b.HasOne("BackendAPIBookingHotel.Model.Person", "Persons")
                        .WithMany("Addresses")
                        .HasForeignKey("PersonID")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Persons");
                });

            modelBuilder.Entity("BackendAPIBookingHotel.Model.BedRoom", b =>
                {
                    b.HasOne("BookingHotel.Core.Models.Bed", "bed")
                        .WithMany()
                        .HasForeignKey("BedID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BackendAPIBookingHotel.Model.Room", null)
                        .WithMany("BedRooms")
                        .HasForeignKey("RoomID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("bed");
                });

            modelBuilder.Entity("BackendAPIBookingHotel.Model.Booking", b =>
                {
                    b.HasOne("BackendAPIBookingHotel.Model.Customer", "Customer")
                        .WithMany("Bookings")
                        .HasForeignKey("CustomerID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("BackendAPIBookingHotel.Model.Room", "Room")
                        .WithMany("Bookings")
                        .HasForeignKey("RoomID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("Room");
                });

            modelBuilder.Entity("BackendAPIBookingHotel.Model.BookingDetail", b =>
                {
                    b.HasOne("BackendAPIBookingHotel.Model.Booking", "Booking")
                        .WithMany("BookingDetails")
                        .HasForeignKey("BookingID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("BackendAPIBookingHotel.Model.Service", "Service")
                        .WithMany("BookingDetails")
                        .HasForeignKey("ServiceID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Booking");

                    b.Navigation("Service");
                });

            modelBuilder.Entity("BackendAPIBookingHotel.Model.Customer", b =>
                {
                    b.HasOne("BackendAPIBookingHotel.Model.Person", "Person")
                        .WithMany()
                        .HasForeignKey("CustomerID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Person");
                });

            modelBuilder.Entity("BackendAPIBookingHotel.Model.Deposit", b =>
                {
                    b.HasOne("BackendAPIBookingHotel.Model.Booking", "Booking")
                        .WithMany("Deposits")
                        .HasForeignKey("BookingID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("BackendAPIBookingHotel.Model.CancellationPolicy", "CancellationPolicy")
                        .WithMany("Deposits")
                        .HasForeignKey("CancellationPolicyID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Booking");

                    b.Navigation("CancellationPolicy");
                });

            modelBuilder.Entity("BackendAPIBookingHotel.Model.Email", b =>
                {
                    b.HasOne("BackendAPIBookingHotel.Model.Person", "Person")
                        .WithMany("Emails")
                        .HasForeignKey("PersonID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Person");
                });

            modelBuilder.Entity("BackendAPIBookingHotel.Model.ImageRooms", b =>
                {
                    b.HasOne("BackendAPIBookingHotel.Model.Room", "Room")
                        .WithMany("ImageRooms")
                        .HasForeignKey("RoomID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Room");
                });

            modelBuilder.Entity("BackendAPIBookingHotel.Model.Invoice", b =>
                {
                    b.HasOne("BackendAPIBookingHotel.Model.Booking", "Booking")
                        .WithMany("Invoices")
                        .HasForeignKey("BookingID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Booking");
                });

            modelBuilder.Entity("BackendAPIBookingHotel.Model.Phone", b =>
                {
                    b.HasOne("BackendAPIBookingHotel.Model.Person", "Person")
                        .WithMany("Phones")
                        .HasForeignKey("PersonID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Person");
                });

            modelBuilder.Entity("BackendAPIBookingHotel.Model.Role", b =>
                {
                    b.HasOne("BookingHotel.Core.Models.Permission", "Permission")
                        .WithMany("Roles")
                        .HasForeignKey("PermissionId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Permission");
                });

            modelBuilder.Entity("BackendAPIBookingHotel.Model.Room", b =>
                {
                    b.HasOne("BackendAPIBookingHotel.Model.Hotel", "Hotel")
                        .WithMany("Rooms")
                        .HasForeignKey("HotelID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("BackendAPIBookingHotel.Model.RoomDetail", "RoomDetail")
                        .WithMany()
                        .HasForeignKey("RoomDetailID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Hotel");

                    b.Navigation("RoomDetail");
                });

            modelBuilder.Entity("BackendAPIBookingHotel.Model.Staff", b =>
                {
                    b.HasOne("BackendAPIBookingHotel.Model.Hotel", "Hotel")
                        .WithMany("Staffs")
                        .HasForeignKey("HotelID")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Hotel");
                });

            modelBuilder.Entity("BackendAPIBookingHotel.Model.UserRole", b =>
                {
                    b.HasOne("BackendAPIBookingHotel.Model.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("BackendAPIBookingHotel.Model.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("BackendAPIBookingHotel.Model.Booking", b =>
                {
                    b.Navigation("BookingDetails");

                    b.Navigation("Deposits");

                    b.Navigation("Invoices");
                });

            modelBuilder.Entity("BackendAPIBookingHotel.Model.CancellationPolicy", b =>
                {
                    b.Navigation("Deposits");
                });

            modelBuilder.Entity("BackendAPIBookingHotel.Model.Customer", b =>
                {
                    b.Navigation("Bookings");
                });

            modelBuilder.Entity("BackendAPIBookingHotel.Model.Hotel", b =>
                {
                    b.Navigation("Rooms");

                    b.Navigation("Staffs");
                });

            modelBuilder.Entity("BackendAPIBookingHotel.Model.Person", b =>
                {
                    b.Navigation("Addresses");

                    b.Navigation("Emails");

                    b.Navigation("Phones");
                });

            modelBuilder.Entity("BackendAPIBookingHotel.Model.Role", b =>
                {
                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("BackendAPIBookingHotel.Model.Room", b =>
                {
                    b.Navigation("BedRooms");

                    b.Navigation("Bookings");

                    b.Navigation("ImageRooms");
                });

            modelBuilder.Entity("BackendAPIBookingHotel.Model.Service", b =>
                {
                    b.Navigation("BookingDetails");
                });

            modelBuilder.Entity("BackendAPIBookingHotel.Model.User", b =>
                {
                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("BookingHotel.Core.Models.Permission", b =>
                {
                    b.Navigation("Roles");
                });
#pragma warning restore 612, 618
        }
    }
}
