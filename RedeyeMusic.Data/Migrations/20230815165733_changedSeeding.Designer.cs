﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RedeyeMusic.Data;

#nullable disable

namespace RedeyeMusic.Data.Migrations
{
    [DbContext(typeof(RedeyeMusicDbContext))]
    [Migration("20230815165733_changedSeeding")]
    partial class changedSeeding
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.21")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("RedeyeMusic.Data.Models.Album", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("ArtistId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("ArtistId");

                    b.ToTable("Albums");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ArtistId = 1,
                            Description = "Mauris suscipit, nunc sit amet sollicitudin varius, nisl eros consectetur diam, nec.",
                            ImageUrl = "https://images.nightcafe.studio/jobs/nrOcbGDvjDgIZPO7rbA6/nrOcbGDvjDgIZPO7rbA6.jpg?tr=w-1600,c-at_max",
                            IsDeleted = false,
                            Name = "FirstAlbum"
                        },
                        new
                        {
                            Id = 2,
                            ArtistId = 1,
                            Description = "Nunc vitae imperdiet felis. Integer ac finibus turpis. Praesent ipsum arcu, placerat.",
                            ImageUrl = "https://images.nightcafe.studio/jobs/nrOcbGDvjDgIZPO7rbA6/nrOcbGDvjDgIZPO7rbA6.jpg?tr=w-1600,c-at_max",
                            IsDeleted = false,
                            Name = "SecondAlbum"
                        });
                });

            modelBuilder.Entity("RedeyeMusic.Data.Models.ApplicationUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)")
                        .HasDefaultValue("Test");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)")
                        .HasDefaultValue("Testov");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("3cdfc504-e0e3-41cd-bd90-7c711143fe69"),
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "49d569d1-e4ac-445c-8749-f1886b3a4415",
                            Email = "admin@redeye.com",
                            EmailConfirmed = false,
                            FirstName = "Admin",
                            IsDeleted = false,
                            LastName = "Admin",
                            LockoutEnabled = false,
                            NormalizedEmail = "ADMIN@REDEYE.COM",
                            NormalizedUserName = "ADMIN@REDEYE.COM",
                            PasswordHash = "AQAAAAEAACcQAAAAEDu7pZEPv3Vm3gGZESDHsjZL21v+FQRAC4GyxElR2BuH+tG8wUykT8Ded4YNoQg5Kg==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "2d0fb04e-5195-44d6-87ca-6e164e473633",
                            TwoFactorEnabled = false,
                            UserName = "admin@redeye.com"
                        },
                        new
                        {
                            Id = new Guid("0b106195-cde9-4c57-95e5-bcb4fc884b0f"),
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "fd33eac2-7626-4d16-afb6-cdcac6884d92",
                            Email = "guest@redeye.com",
                            EmailConfirmed = false,
                            FirstName = "Guest",
                            IsDeleted = false,
                            LastName = "Guestov",
                            LockoutEnabled = false,
                            NormalizedEmail = "GUEST@REDEYE.COM",
                            NormalizedUserName = "GUEST@REDEYE.COM",
                            PasswordHash = "AQAAAAEAACcQAAAAEDnRCkgE65V9LLQt6E6PwsYVgnidVk5YW+L/d3ACDUigx7b9Vkw5gs1gwLqcW+qUsg==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "1c7cc273-61c6-42cd-8cd9-a1af5bb1443f",
                            TwoFactorEnabled = false,
                            UserName = "guest@redeye.com"
                        },
                        new
                        {
                            Id = new Guid("a7b1ecad-7870-4d98-85dd-4e2bb4952fe2"),
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "d72b674d-c860-47fb-98ee-7c91a54e8d66",
                            Email = "artist@redeye.com",
                            EmailConfirmed = false,
                            FirstName = "Artist",
                            IsDeleted = false,
                            LastName = "Artistov",
                            LockoutEnabled = false,
                            NormalizedEmail = "ARTIST@REDEYE.COM",
                            NormalizedUserName = "ARTIST@REDEYE.COM",
                            PasswordHash = "AQAAAAEAACcQAAAAEK+kzKkV8nJymIFum/hdWVPUVWo8icdHTLMVlqtIiFXv+EgkBztjimdTjecpnsbR2A==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "b5574065-612b-4ecf-89cc-359feb7ddec7",
                            TwoFactorEnabled = false,
                            UserName = "artist@redeye.com"
                        });
                });

            modelBuilder.Entity("RedeyeMusic.Data.Models.Artist", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<Guid?>("ApplicationUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationUserId");

                    b.ToTable("Artists");

                    b.HasData(
                        new
                        {
                            Id = -1,
                            ApplicationUserId = new Guid("3cdfc504-e0e3-41cd-bd90-7c711143fe69"),
                            Name = "Admin"
                        },
                        new
                        {
                            Id = -2,
                            ApplicationUserId = new Guid("a7b1ecad-7870-4d98-85dd-4e2bb4952fe2"),
                            Name = "Artist"
                        });
                });

            modelBuilder.Entity("RedeyeMusic.Data.Models.Genre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Genres");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Pop"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Rock"
                        },
                        new
                        {
                            Id = 3,
                            Name = "HipHop"
                        },
                        new
                        {
                            Id = 4,
                            Name = "R&B"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Classical"
                        },
                        new
                        {
                            Id = 6,
                            Name = "Rap"
                        },
                        new
                        {
                            Id = 7,
                            Name = "Country"
                        },
                        new
                        {
                            Id = 8,
                            Name = "Alternative"
                        });
                });

            modelBuilder.Entity("RedeyeMusic.Data.Models.Playlist", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<Guid>("ApplicationUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationUserId");

                    b.ToTable("Playlists");
                });

            modelBuilder.Entity("RedeyeMusic.Data.Models.PlaylistsSongs", b =>
                {
                    b.Property<int>("PlaylistId")
                        .HasColumnType("int");

                    b.Property<int>("SongId")
                        .HasColumnType("int");

                    b.HasKey("PlaylistId", "SongId");

                    b.HasIndex("SongId");

                    b.ToTable("PlaylistsSongs");
                });

            modelBuilder.Entity("RedeyeMusic.Data.Models.Song", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("AlbumId")
                        .HasColumnType("int");

                    b.Property<int>("ArtistId")
                        .HasColumnType("int");

                    b.Property<int>("Duration")
                        .HasMaxLength(600)
                        .HasColumnType("int");

                    b.Property<int>("GenreId")
                        .HasColumnType("int");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<int>("ListenCount")
                        .HasColumnType("int");

                    b.Property<string>("Lyrics")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("Mp3FilePath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("AlbumId");

                    b.HasIndex("ArtistId");

                    b.HasIndex("GenreId");

                    b.ToTable("Songs");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AlbumId = 1,
                            ArtistId = 1,
                            Duration = 0,
                            GenreId = 1,
                            ImageUrl = "https://images.theconversation.com/files/258026/original/file-20190208-174861-nms2kt.jpg?ixlib=rb-1.1.0&q=45&auto=format&w=926&fit=clip",
                            IsDeleted = false,
                            ListenCount = 0,
                            Lyrics = "SampleSongLyrics",
                            Mp3FilePath = "songs/Mp3s/2410a001-848d-449f-bb02-abbafdda6447test.mp3",
                            Title = "SampleSong"
                        },
                        new
                        {
                            Id = 2,
                            AlbumId = 2,
                            ArtistId = 1,
                            Duration = 0,
                            GenreId = 2,
                            ImageUrl = "https://mir-s3-cdn-cf.behance.net/project_modules/1400/fe529a64193929.5aca8500ba9ab.jpg",
                            IsDeleted = false,
                            ListenCount = 0,
                            Lyrics = "SampleSong2Lyrics",
                            Mp3FilePath = "songs/Mp3s/2410a001-848d-449f-bb02-abbafdda6447test.mp3",
                            Title = "SampleSong2"
                        },
                        new
                        {
                            Id = 3,
                            AlbumId = 1,
                            ArtistId = 1,
                            Duration = 0,
                            GenreId = 1,
                            ImageUrl = "https://fiverr-res.cloudinary.com/images/t_main1,q_auto,f_auto,q_auto,f_auto/gigs/149562217/original/fc77d96de1229ad6ca6f83289fd2d4b4c068a568/make-album-and-song-covers.jpg",
                            IsDeleted = false,
                            ListenCount = 0,
                            Lyrics = "SampleSong3Lyrics",
                            Mp3FilePath = "songs/Mp3s/2410a001-848d-449f-bb02-abbafdda6447test.mp3",
                            Title = "SampleSong3"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("RedeyeMusic.Data.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("RedeyeMusic.Data.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RedeyeMusic.Data.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("RedeyeMusic.Data.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RedeyeMusic.Data.Models.Album", b =>
                {
                    b.HasOne("RedeyeMusic.Data.Models.Artist", "Artist")
                        .WithMany("Albums")
                        .HasForeignKey("ArtistId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Artist");
                });

            modelBuilder.Entity("RedeyeMusic.Data.Models.Artist", b =>
                {
                    b.HasOne("RedeyeMusic.Data.Models.ApplicationUser", "ApplicationUser")
                        .WithMany()
                        .HasForeignKey("ApplicationUserId");

                    b.Navigation("ApplicationUser");
                });

            modelBuilder.Entity("RedeyeMusic.Data.Models.Playlist", b =>
                {
                    b.HasOne("RedeyeMusic.Data.Models.ApplicationUser", "ApplicationUser")
                        .WithMany("Playlists")
                        .HasForeignKey("ApplicationUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ApplicationUser");
                });

            modelBuilder.Entity("RedeyeMusic.Data.Models.PlaylistsSongs", b =>
                {
                    b.HasOne("RedeyeMusic.Data.Models.Playlist", "Playlist")
                        .WithMany("PlaylistsSongs")
                        .HasForeignKey("PlaylistId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RedeyeMusic.Data.Models.Song", "Song")
                        .WithMany("PlaylistsSongs")
                        .HasForeignKey("SongId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Playlist");

                    b.Navigation("Song");
                });

            modelBuilder.Entity("RedeyeMusic.Data.Models.Song", b =>
                {
                    b.HasOne("RedeyeMusic.Data.Models.Album", "Album")
                        .WithMany("Songs")
                        .HasForeignKey("AlbumId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RedeyeMusic.Data.Models.Artist", "Artist")
                        .WithMany("Songs")
                        .HasForeignKey("ArtistId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("RedeyeMusic.Data.Models.Genre", "Genre")
                        .WithMany("Songs")
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Album");

                    b.Navigation("Artist");

                    b.Navigation("Genre");
                });

            modelBuilder.Entity("RedeyeMusic.Data.Models.Album", b =>
                {
                    b.Navigation("Songs");
                });

            modelBuilder.Entity("RedeyeMusic.Data.Models.ApplicationUser", b =>
                {
                    b.Navigation("Playlists");
                });

            modelBuilder.Entity("RedeyeMusic.Data.Models.Artist", b =>
                {
                    b.Navigation("Albums");

                    b.Navigation("Songs");
                });

            modelBuilder.Entity("RedeyeMusic.Data.Models.Genre", b =>
                {
                    b.Navigation("Songs");
                });

            modelBuilder.Entity("RedeyeMusic.Data.Models.Playlist", b =>
                {
                    b.Navigation("PlaylistsSongs");
                });

            modelBuilder.Entity("RedeyeMusic.Data.Models.Song", b =>
                {
                    b.Navigation("PlaylistsSongs");
                });
#pragma warning restore 612, 618
        }
    }
}