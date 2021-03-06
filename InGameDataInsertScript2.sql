USE [InGameDb]
GO
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'a9e6aa28-9d60-4934-8791-8c14ca9cf9d7', N'api', N'API', N'd6735068-9fb2-45ec-8a14-04f7947569d4')
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'bfdc6b65-4fc7-4500-b7b4-469113b2c732', N'Admin', N'ADMIN', N'18e55575-0350-49b6-b18b-2fe00c6cd1af')
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'da9defc1-5045-4f47-8340-e737349c25f1', N'Product_view', N'PRODUCT_VIEW', N'd8b1e998-7089-4922-a6ec-776d95633e32')
SET IDENTITY_INSERT [dbo].[AspNetRoleClaims] ON 

INSERT [dbo].[AspNetRoleClaims] ([Id], [RoleId], [ClaimType], [ClaimValue]) VALUES (4, N'bfdc6b65-4fc7-4500-b7b4-469113b2c732', N'default', N'1')
SET IDENTITY_INSERT [dbo].[AspNetRoleClaims] OFF
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Birthdate], [City], [Country]) VALUES (N'0e9056ef-ee7c-4ab5-9702-b470383e8543', N'admin@gmail.com', N'ADMIN@GMAIL.COM', N'admin@gmail.com', N'ADMIN@GMAIL.COM', 0, N'AQAAAAEAACcQAAAAEPYkyO3zE6tUSE2pZVmkeQU8xL2icT3OcTFYHqkWUB4UGA/e2S1Ef0yuijREr47azw==', N'NBJFIKWFFUOYSQTMRJK7CZXXKLFBNZDJ', N'6d30a249-554a-4f1d-b0c6-6f6bc3ee90dc', NULL, 0, 0, NULL, 1, 0, CAST(0x070000000000000000 AS DateTime2), NULL, NULL)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Birthdate], [City], [Country]) VALUES (N'78054f41-3212-45a6-bfe1-9fdd9f0798db', N'default@gmail.com', N'DEFAULT@GMAIL.COM', N'default@gmail.com', N'DEFAULT@GMAIL.COM', 0, N'AQAAAAEAACcQAAAAEOALengs6K/9crMigJFNgNOZrKTup3GysUU9Mu461m9EX3N3lmYmbp+rCP1EsO06MA==', N'VV5PTC4DI4PFIOHTI5YSRCHQMVQMUVZQ', N'8d6a6742-8d99-4c28-989f-e233106373a7', N'54666555444', 0, 0, NULL, 1, 0, CAST(0x070000000000000000 AS DateTime2), NULL, NULL)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Birthdate], [City], [Country]) VALUES (N'8c130b45-8acb-410f-8594-88416cb6361a', N'demouser@microsoft.com', N'DEMOUSER@MICROSOFT.COM', N'demouser@microsoft.com', N'DEMOUSER@MICROSOFT.COM', 0, N'AQAAAAEAACcQAAAAEKNUyFjUduwiIndofxSYXpt6ss3EvJdQ+Vsa4ASRB9yLCmh5uZ1LXFeN6vlb89TLiQ==', N'WWICABKBJBWJIB7CVDVSXRRLHJFS36B2', N'c8315b02-8f8b-4495-90df-6a6dae4f7fe5', NULL, 0, 0, NULL, 1, 0, CAST(0x070000000000000000 AS DateTime2), NULL, NULL)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Birthdate], [City], [Country]) VALUES (N'cbafc1fd-a754-431b-8111-4cbc64cc741e', N'zzeki@gmail.com', N'ZZEKI@GMAIL.COM', N'zzeki@gmail.com', N'ZZEKI@GMAIL.COM', 1, N'AQAAAAEAACcQAAAAEMjVQm0w/SJiTJ3qtBiBjiFyQYsuh1mvXt2lPWbbZnidmR4pLDtedWIkSdx/Duyy3w==', N'TQ23D3R4E2II7GNXOFS37GKC4S5OXJ7E', N'69e203e3-3b7c-4d2d-a9b5-4bf753cf3e21', NULL, 0, 0, NULL, 1, 0, CAST(0x070000000000000000 AS DateTime2), N'istanbul', N'Türkiye')
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Birthdate], [City], [Country]) VALUES (N'ebf50723-a59e-4ef3-8950-b6ecc2c0b638', N'admin@microsoft.com', N'ADMIN@MICROSOFT.COM', N'admin@microsoft.com', N'ADMIN@MICROSOFT.COM', 1, N'AQAAAAEAACcQAAAAEMVRqMW+HhCjTz6/5jNCrSWe0WUMrghbv+IedaXNJa2/lHNx9LllWhbrhc4+rgSOpg==', N'VECZZWAFGMXY3SA75CX4FPS7YANFCPAE', N'1e5c3ecf-6bf2-474d-b6ad-139e4319e57d', NULL, 0, 0, NULL, 1, 0, CAST(0x070000000000000000 AS DateTime2), NULL, NULL)
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'8c130b45-8acb-410f-8594-88416cb6361a', N'a9e6aa28-9d60-4934-8791-8c14ca9cf9d7')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'cbafc1fd-a754-431b-8111-4cbc64cc741e', N'bfdc6b65-4fc7-4500-b7b4-469113b2c732')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'ebf50723-a59e-4ef3-8950-b6ecc2c0b638', N'bfdc6b65-4fc7-4500-b7b4-469113b2c732')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'0e9056ef-ee7c-4ab5-9702-b470383e8543', N'da9defc1-5045-4f47-8340-e737349c25f1')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'cbafc1fd-a754-431b-8111-4cbc64cc741e', N'da9defc1-5045-4f47-8340-e737349c25f1')
SET IDENTITY_INSERT [dbo].[AspNetUserClaims] ON 

INSERT [dbo].[AspNetUserClaims] ([Id], [UserId], [ClaimType], [ClaimValue]) VALUES (1, N'cbafc1fd-a754-431b-8111-4cbc64cc741e', N'Authorization', N'Api')
INSERT [dbo].[AspNetUserClaims] ([Id], [UserId], [ClaimType], [ClaimValue]) VALUES (2, N'ebf50723-a59e-4ef3-8950-b6ecc2c0b638', N'IsAdmin', N'true')
INSERT [dbo].[AspNetUserClaims] ([Id], [UserId], [ClaimType], [ClaimValue]) VALUES (3, N'cbafc1fd-a754-431b-8111-4cbc64cc741e', N'product_view', N'product_view')
INSERT [dbo].[AspNetUserClaims] ([Id], [UserId], [ClaimType], [ClaimValue]) VALUES (4, N'cbafc1fd-a754-431b-8111-4cbc64cc741e', N'Api', N'Api')
SET IDENTITY_INSERT [dbo].[AspNetUserClaims] OFF
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20190315122517_InitialDbMigration2', N'2.2.2-servicing-10034')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20190315131507_InitialDbMigration11', N'2.2.2-servicing-10034')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20190315131719_InitialDbMigration12', N'2.2.2-servicing-10034')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20190315133117_InitialDbMigration13', N'2.2.2-servicing-10034')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20190315133823_InitialDbMigration14', N'2.2.2-servicing-10034')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20190317112321_InitialDbMigration21', N'2.2.2-servicing-10034')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20190317113200_InitialDbMigration21', N'2.2.2-servicing-10034')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20190317114151_InitialDbMigration21', N'2.2.2-servicing-10034')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20190318192318_InitialDbMigration2', N'2.2.2-servicing-10034')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20190318192949_InitialDbMigration3', N'2.2.2-servicing-10034')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20190318231623_InitialDbMigration3', N'2.2.2-servicing-10034')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20190319033909_InitialDbMigration', N'2.2.2-servicing-10034')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20190319034705_InitialDbMigration', N'2.2.2-servicing-10034')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20190319035145_InitialDbMigration', N'2.2.2-servicing-10034')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20190319041747_InitialDbMigration', N'2.2.2-servicing-10034')
SET IDENTITY_INSERT [dbo].[Categories] ON 

INSERT [dbo].[Categories] ([Id], [CategoryId], [ParentCategoryId], [CategoryName], [Uri], [PictureUri], [Description]) VALUES (1, 0, NULL, N'ZULA CATEGORY', NULL, NULL, NULL)
INSERT [dbo].[Categories] ([Id], [CategoryId], [ParentCategoryId], [CategoryName], [Uri], [PictureUri], [Description]) VALUES (3, 2000, 0, N'Araçlar', NULL, NULL, NULL)
INSERT [dbo].[Categories] ([Id], [CategoryId], [ParentCategoryId], [CategoryName], [Uri], [PictureUri], [Description]) VALUES (4, 3000, 0, N'Silahlar', NULL, NULL, NULL)
INSERT [dbo].[Categories] ([Id], [CategoryId], [ParentCategoryId], [CategoryName], [Uri], [PictureUri], [Description]) VALUES (5, 2010, 2000, N'Taramalı', NULL, NULL, NULL)
INSERT [dbo].[Categories] ([Id], [CategoryId], [ParentCategoryId], [CategoryName], [Uri], [PictureUri], [Description]) VALUES (6, 2020, 2000, N'Tek atar', NULL, NULL, NULL)
INSERT [dbo].[Categories] ([Id], [CategoryId], [ParentCategoryId], [CategoryName], [Uri], [PictureUri], [Description]) VALUES (7, 2021, 2020, N'M468', NULL, NULL, NULL)
INSERT [dbo].[Categories] ([Id], [CategoryId], [ParentCategoryId], [CategoryName], [Uri], [PictureUri], [Description]) VALUES (8, 2022, 2020, N'Famas', NULL, NULL, NULL)
INSERT [dbo].[Categories] ([Id], [CategoryId], [ParentCategoryId], [CategoryName], [Uri], [PictureUri], [Description]) VALUES (9, 3010, 3000, N'4 kişilik araç', NULL, NULL, NULL)
INSERT [dbo].[Categories] ([Id], [CategoryId], [ParentCategoryId], [CategoryName], [Uri], [PictureUri], [Description]) VALUES (10, 3020, 3000, N'taksi', NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[Categories] OFF
SET IDENTITY_INSERT [dbo].[Products] ON 

INSERT [dbo].[Products] ([Id], [Name], [Description], [Price], [PictureUri], [IsActive], [CategoryId]) VALUES (1, N'silah', N'silah', CAST(5000.00 AS Decimal(18, 2)), NULL, 1, 1000)
SET IDENTITY_INSERT [dbo].[Products] OFF
