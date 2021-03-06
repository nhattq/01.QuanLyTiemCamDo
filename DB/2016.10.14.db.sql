USE [camdo.com]
GO
/****** Object:  Table [dbo].[Contracts]    Script Date: 10/14/2016 10:32:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Contracts](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](50) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[Description] [nvarchar](500) NOT NULL,
	[Commodity] [nvarchar](500) NOT NULL,
	[CustomerId] [int] NOT NULL,
	[Status] [tinyint] NOT NULL,
	[UpdatedDate] [datetime] NULL,
	[CreatedBy] [int] NOT NULL,
	[UpdatedBy] [int] NULL,
 CONSTRAINT [PK_Contracts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Customers]    Script Date: 10/14/2016 10:32:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Address] [nvarchar](500) NOT NULL,
	[CMND] [nvarchar](50) NOT NULL,
	[CMNDCreatedDate] [datetime] NOT NULL,
	[CMNDCreatedAt] [nvarchar](50) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[Phone] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Customers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Extends]    Script Date: 10/14/2016 10:32:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Extends](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ContractId] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ExpiredDate] [datetime] NOT NULL,
	[Times] [int] NOT NULL,
 CONSTRAINT [PK_Extends] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[User]    Script Date: 10/14/2016 10:32:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Fullname] [nvarchar](500) NULL,
	[Username] [nvarchar](max) NULL,
	[Password] [nvarchar](max) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[LastVisit] [datetime] NOT NULL,
	[Active] [bit] NOT NULL,
	[RoleId] [int] NOT NULL,
 CONSTRAINT [PK_dbo.User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserController]    Script Date: 10/14/2016 10:32:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserController](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Action] [nvarchar](max) NULL,
	[Type] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.UserController] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserModule]    Script Date: 10/14/2016 10:32:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserModule](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Icon] [nvarchar](max) NULL,
	[Name] [nvarchar](max) NULL,
	[Controller] [nvarchar](max) NULL,
	[Action] [nvarchar](max) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ParentId] [int] NOT NULL,
	[Order] [int] NOT NULL,
	[DisplayMenu] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.UserModule] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserPermission]    Script Date: 10/14/2016 10:32:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserPermission](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [int] NOT NULL,
	[ModuleId] [int] NOT NULL,
	[Access] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.UserPermission] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserRole]    Script Date: 10/14/2016 10:32:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRole](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[LastModified] [datetime] NOT NULL,
	[IsAccessControlCenter] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.UserRole] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[Contracts] ON 

GO
INSERT [dbo].[Contracts] ([Id], [Code], [CreatedDate], [Description], [Commodity], [CustomerId], [Status], [UpdatedDate], [CreatedBy], [UpdatedBy]) VALUES (1, N'1234567890', CAST(0x0000A69C00000000 AS DateTime), N'dddddddddddđ', N'ddddddddddddddd', 1, 1, CAST(0x0000A69C00000000 AS DateTime), 1, 1)
GO
INSERT [dbo].[Contracts] ([Id], [Code], [CreatedDate], [Description], [Commodity], [CustomerId], [Status], [UpdatedDate], [CreatedBy], [UpdatedBy]) VALUES (2, N'104614102016', CAST(0x0000A69F00B18D8C AS DateTime), N'Hôm nay, anh Nhật có đến cầm cố:', N'1. Xe máy Sirius vàng đen
2. Điện thoại Oppo R7 lite', 1, 0, NULL, 1, NULL)
GO
SET IDENTITY_INSERT [dbo].[Contracts] OFF
GO
SET IDENTITY_INSERT [dbo].[Customers] ON 

GO
INSERT [dbo].[Customers] ([Id], [Name], [Address], [CMND], [CMNDCreatedDate], [CMNDCreatedAt], [CreatedDate], [Phone]) VALUES (1, N'Trần Quốc Nhật', N'297D Dien Bien Phu', N'197204370', CAST(0x0000A68D00000000 AS DateTime), N'Công An Quảng Trị', CAST(0x0000A69C00000000 AS DateTime), N'0987654321')
GO
SET IDENTITY_INSERT [dbo].[Customers] OFF
GO
SET IDENTITY_INSERT [dbo].[Extends] ON 

GO
INSERT [dbo].[Extends] ([Id], [ContractId], [CreatedDate], [ExpiredDate], [Times]) VALUES (1, 1, CAST(0x0000A69F00A13AD2 AS DateTime), CAST(0x0000A6A300000000 AS DateTime), 1)
GO
INSERT [dbo].[Extends] ([Id], [ContractId], [CreatedDate], [ExpiredDate], [Times]) VALUES (2, 2, CAST(0x0000A69F00BB4AA5 AS DateTime), CAST(0x0000A6AA00000000 AS DateTime), 1)
GO
SET IDENTITY_INSERT [dbo].[Extends] OFF
GO
SET IDENTITY_INSERT [dbo].[User] ON 

GO
INSERT [dbo].[User] ([Id], [Fullname], [Username], [Password], [CreatedDate], [LastVisit], [Active], [RoleId]) VALUES (1, N'Administrator', N'administrator', N'CD93147E4205DC698893D2AD79290AB3D1111846C9B5022406C71C2AF7C45F82', CAST(0x0000A54F01029314 AS DateTime), CAST(0x0000A54F01029314 AS DateTime), 1, 1)
GO
SET IDENTITY_INSERT [dbo].[User] OFF
GO
SET IDENTITY_INSERT [dbo].[UserController] ON 

GO
INSERT [dbo].[UserController] ([Id], [Name], [Action], [Type]) VALUES (1, N'BaseController', N'get_CurrentUser', N'User')
GO
INSERT [dbo].[UserController] ([Id], [Name], [Action], [Type]) VALUES (2, N'BaseController', N'InitMessage', N'Void')
GO
INSERT [dbo].[UserController] ([Id], [Name], [Action], [Type]) VALUES (3, N'BaseController', N'InitPermission', N'Void')
GO
INSERT [dbo].[UserController] ([Id], [Name], [Action], [Type]) VALUES (4, N'UserController', N'Index', N'ActionResult')
GO
INSERT [dbo].[UserController] ([Id], [Name], [Action], [Type]) VALUES (5, N'UserController', N'Login', N'ActionResult')
GO
INSERT [dbo].[UserController] ([Id], [Name], [Action], [Type]) VALUES (6, N'UserModuleController', N'Create', N'ActionResult')
GO
INSERT [dbo].[UserController] ([Id], [Name], [Action], [Type]) VALUES (7, N'UserModuleController', N'Delete', N'JsonResult')
GO
INSERT [dbo].[UserController] ([Id], [Name], [Action], [Type]) VALUES (8, N'UserModuleController', N'Edit', N'ActionResult')
GO
INSERT [dbo].[UserController] ([Id], [Name], [Action], [Type]) VALUES (9, N'UserModuleController', N'GetAction', N'JsonResult')
GO
INSERT [dbo].[UserController] ([Id], [Name], [Action], [Type]) VALUES (10, N'UserModuleController', N'Index', N'ActionResult')
GO
INSERT [dbo].[UserController] ([Id], [Name], [Action], [Type]) VALUES (11, N'UserPermissionController', N'Index', N'ActionResult')
GO
INSERT [dbo].[UserController] ([Id], [Name], [Action], [Type]) VALUES (12, N'UserPermissionController', N'Update', N'JsonResult')
GO
INSERT [dbo].[UserController] ([Id], [Name], [Action], [Type]) VALUES (13, N'UserRoleController', N'Create', N'ActionResult')
GO
INSERT [dbo].[UserController] ([Id], [Name], [Action], [Type]) VALUES (14, N'UserRoleController', N'Edit', N'ActionResult')
GO
INSERT [dbo].[UserController] ([Id], [Name], [Action], [Type]) VALUES (15, N'UserRoleController', N'Index', N'ActionResult')
GO
INSERT [dbo].[UserController] ([Id], [Name], [Action], [Type]) VALUES (16, N'UserController', N'Dashboard', N'ActionResult')
GO
INSERT [dbo].[UserController] ([Id], [Name], [Action], [Type]) VALUES (17, N'UserController', N'Create', N'ActionResult')
GO
INSERT [dbo].[UserController] ([Id], [Name], [Action], [Type]) VALUES (18, N'UserController', N'Edit', N'ActionResult')
GO
INSERT [dbo].[UserController] ([Id], [Name], [Action], [Type]) VALUES (19, N'UserModuleController', N'GetAll', N'List`1')
GO
INSERT [dbo].[UserController] ([Id], [Name], [Action], [Type]) VALUES (20, N'UserPermissionController', N'GetAll', N'List`1')
GO
INSERT [dbo].[UserController] ([Id], [Name], [Action], [Type]) VALUES (52, N'ContractsController', N'Create', N'ActionResult')
GO
INSERT [dbo].[UserController] ([Id], [Name], [Action], [Type]) VALUES (53, N'ContractsController', N'Delete', N'ActionResult')
GO
INSERT [dbo].[UserController] ([Id], [Name], [Action], [Type]) VALUES (54, N'ContractsController', N'DeleteConfirmed', N'ActionResult')
GO
INSERT [dbo].[UserController] ([Id], [Name], [Action], [Type]) VALUES (55, N'ContractsController', N'Details', N'ActionResult')
GO
INSERT [dbo].[UserController] ([Id], [Name], [Action], [Type]) VALUES (56, N'ContractsController', N'Edit', N'ActionResult')
GO
INSERT [dbo].[UserController] ([Id], [Name], [Action], [Type]) VALUES (57, N'ContractsController', N'Index', N'ActionResult')
GO
INSERT [dbo].[UserController] ([Id], [Name], [Action], [Type]) VALUES (58, N'UserController', N'ChangePassword', N'ActionResult')
GO
INSERT [dbo].[UserController] ([Id], [Name], [Action], [Type]) VALUES (59, N'UserController', N'Logout', N'ActionResult')
GO
INSERT [dbo].[UserController] ([Id], [Name], [Action], [Type]) VALUES (60, N'CustomersController', N'Create', N'ActionResult')
GO
INSERT [dbo].[UserController] ([Id], [Name], [Action], [Type]) VALUES (61, N'CustomersController', N'Delete', N'ActionResult')
GO
INSERT [dbo].[UserController] ([Id], [Name], [Action], [Type]) VALUES (62, N'CustomersController', N'DeleteConfirmed', N'ActionResult')
GO
INSERT [dbo].[UserController] ([Id], [Name], [Action], [Type]) VALUES (63, N'CustomersController', N'Details', N'ActionResult')
GO
INSERT [dbo].[UserController] ([Id], [Name], [Action], [Type]) VALUES (64, N'CustomersController', N'Edit', N'ActionResult')
GO
INSERT [dbo].[UserController] ([Id], [Name], [Action], [Type]) VALUES (65, N'CustomersController', N'Index', N'ActionResult')
GO
INSERT [dbo].[UserController] ([Id], [Name], [Action], [Type]) VALUES (66, N'ExtendsController', N'Create', N'ActionResult')
GO
INSERT [dbo].[UserController] ([Id], [Name], [Action], [Type]) VALUES (67, N'ExtendsController', N'Delete', N'ActionResult')
GO
INSERT [dbo].[UserController] ([Id], [Name], [Action], [Type]) VALUES (68, N'ExtendsController', N'DeleteConfirmed', N'ActionResult')
GO
INSERT [dbo].[UserController] ([Id], [Name], [Action], [Type]) VALUES (69, N'ExtendsController', N'Details', N'ActionResult')
GO
INSERT [dbo].[UserController] ([Id], [Name], [Action], [Type]) VALUES (70, N'ExtendsController', N'Edit', N'ActionResult')
GO
INSERT [dbo].[UserController] ([Id], [Name], [Action], [Type]) VALUES (71, N'ExtendsController', N'Index', N'ActionResult')
GO
SET IDENTITY_INSERT [dbo].[UserController] OFF
GO
SET IDENTITY_INSERT [dbo].[UserModule] ON 

GO
INSERT [dbo].[UserModule] ([Id], [Icon], [Name], [Controller], [Action], [CreatedDate], [ParentId], [Order], [DisplayMenu]) VALUES (1, N'cog', N'Cấu hình hệ thống', N'UserModuleController', N'Index', CAST(0x0000A54F0101E200 AS DateTime), -2147483648, 0, 1)
GO
INSERT [dbo].[UserModule] ([Id], [Icon], [Name], [Controller], [Action], [CreatedDate], [ParentId], [Order], [DisplayMenu]) VALUES (2, N'sun-o', N'Chức năng', N'UserModuleController', N'Index', CAST(0x0000A54F0101E200 AS DateTime), 1, 0, 1)
GO
INSERT [dbo].[UserModule] ([Id], [Icon], [Name], [Controller], [Action], [CreatedDate], [ParentId], [Order], [DisplayMenu]) VALUES (3, N'sun-o', N'Vai trò người dùng', N'UserRoleController', N'Index', CAST(0x0000A54F0103CD79 AS DateTime), 1, 0, 1)
GO
INSERT [dbo].[UserModule] ([Id], [Icon], [Name], [Controller], [Action], [CreatedDate], [ParentId], [Order], [DisplayMenu]) VALUES (4, N'users', N'Quản trị người dùng', N'UserController', N'Index', CAST(0x0000A54F0105D7D9 AS DateTime), 1, 0, 1)
GO
INSERT [dbo].[UserModule] ([Id], [Icon], [Name], [Controller], [Action], [CreatedDate], [ParentId], [Order], [DisplayMenu]) VALUES (11, N'list', N'Danh sách trạm', N'StationController', N'Index', CAST(0x0000A55100EBAFA2 AS DateTime), 10, 1, 1)
GO
INSERT [dbo].[UserModule] ([Id], [Icon], [Name], [Controller], [Action], [CreatedDate], [ParentId], [Order], [DisplayMenu]) VALUES (12, N'plus', N'Thêm mới trạm', N'StationController', N'Create', CAST(0x0000A55101420DEB AS DateTime), 10, 2, 1)
GO
INSERT [dbo].[UserModule] ([Id], [Icon], [Name], [Controller], [Action], [CreatedDate], [ParentId], [Order], [DisplayMenu]) VALUES (14, N'list', N'Danh sách thiết bị', N'FeederController', N'Index', CAST(0x0000A551016D1DFA AS DateTime), 13, 1, 1)
GO
INSERT [dbo].[UserModule] ([Id], [Icon], [Name], [Controller], [Action], [CreatedDate], [ParentId], [Order], [DisplayMenu]) VALUES (15, N'plus', N'Thêm mới thiết bị', N'FeederController', N'Create', CAST(0x0000A551016D351B AS DateTime), 13, 2, 1)
GO
INSERT [dbo].[UserModule] ([Id], [Icon], [Name], [Controller], [Action], [CreatedDate], [ParentId], [Order], [DisplayMenu]) VALUES (20, N'bars', N'Danh sách loại thông số', N'SpecificationTypeController', N'Index', CAST(0x0000A55200AC60B8 AS DateTime), 19, 1, 1)
GO
INSERT [dbo].[UserModule] ([Id], [Icon], [Name], [Controller], [Action], [CreatedDate], [ParentId], [Order], [DisplayMenu]) VALUES (21, N'bars', N'Danh sách thông số', N'SpecificationController', N'Index', CAST(0x0000A55200B66C97 AS DateTime), 19, 2, 1)
GO
INSERT [dbo].[UserModule] ([Id], [Icon], [Name], [Controller], [Action], [CreatedDate], [ParentId], [Order], [DisplayMenu]) VALUES (22, N'adjust', N'Quản lý hợp đồng', N'ContractsController', N'Index', CAST(0x0000A69E016E9249 AS DateTime), -2147483648, 1, 1)
GO
INSERT [dbo].[UserModule] ([Id], [Icon], [Name], [Controller], [Action], [CreatedDate], [ParentId], [Order], [DisplayMenu]) VALUES (23, N'list', N'Danh sách hợp đồng', N'ContractsController', N'Index', CAST(0x0000A69E016EBB26 AS DateTime), 22, 0, 1)
GO
INSERT [dbo].[UserModule] ([Id], [Icon], [Name], [Controller], [Action], [CreatedDate], [ParentId], [Order], [DisplayMenu]) VALUES (24, N'plus', N'Thêm mới hợp đồng', N'ContractsController', N'Create', CAST(0x0000A69E016EF381 AS DateTime), 22, 1, 1)
GO
INSERT [dbo].[UserModule] ([Id], [Icon], [Name], [Controller], [Action], [CreatedDate], [ParentId], [Order], [DisplayMenu]) VALUES (25, N'at', N'Quản lý khách hàng', N'CustomersController', N'Index', CAST(0x0000A69E016FDEF0 AS DateTime), -2147483648, 2, 1)
GO
INSERT [dbo].[UserModule] ([Id], [Icon], [Name], [Controller], [Action], [CreatedDate], [ParentId], [Order], [DisplayMenu]) VALUES (26, N'list', N'Danh sách khách hàng', N'CustomersController', N'Index', CAST(0x0000A69E016FF660 AS DateTime), 25, 0, 1)
GO
INSERT [dbo].[UserModule] ([Id], [Icon], [Name], [Controller], [Action], [CreatedDate], [ParentId], [Order], [DisplayMenu]) VALUES (27, N'plus', N'Thêm mới khách hàng', N'CustomersController', N'Create', CAST(0x0000A69E01700B97 AS DateTime), 25, 2, 1)
GO
INSERT [dbo].[UserModule] ([Id], [Icon], [Name], [Controller], [Action], [CreatedDate], [ParentId], [Order], [DisplayMenu]) VALUES (28, N'balance-scale', N'Quản lý gia hạn', N'ExtendsController', N'Index', CAST(0x0000A69F009EA849 AS DateTime), -2147483648, 3, 1)
GO
INSERT [dbo].[UserModule] ([Id], [Icon], [Name], [Controller], [Action], [CreatedDate], [ParentId], [Order], [DisplayMenu]) VALUES (29, N'list', N'Danh sách gia hạn', N'ExtendsController', N'Index', CAST(0x0000A69F009EC639 AS DateTime), 28, 0, 1)
GO
SET IDENTITY_INSERT [dbo].[UserModule] OFF
GO
SET IDENTITY_INSERT [dbo].[UserRole] ON 

GO
INSERT [dbo].[UserRole] ([Id], [RoleName], [Description], [CreatedDate], [LastModified], [IsAccessControlCenter]) VALUES (1, N'Administrator', N'Administrator', CAST(0x0000A54F0101E10A AS DateTime), CAST(0x0000A54F0101E10A AS DateTime), 1)
GO
INSERT [dbo].[UserRole] ([Id], [RoleName], [Description], [CreatedDate], [LastModified], [IsAccessControlCenter]) VALUES (11, N'Thành viên', N'Thành viên', CAST(0x0000A54F010D13F4 AS DateTime), CAST(0x0000A55300EC70B0 AS DateTime), 0)
GO
SET IDENTITY_INSERT [dbo].[UserRole] OFF
GO
ALTER TABLE [dbo].[UserRole] ADD  CONSTRAINT [DF_UserRole_IsAccessControlCenter]  DEFAULT ((1)) FOR [IsAccessControlCenter]
GO
ALTER TABLE [dbo].[Contracts]  WITH CHECK ADD  CONSTRAINT [FK_Contracts_Customers] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customers] ([Id])
GO
ALTER TABLE [dbo].[Contracts] CHECK CONSTRAINT [FK_Contracts_Customers]
GO
ALTER TABLE [dbo].[Contracts]  WITH CHECK ADD  CONSTRAINT [FK_Contracts_User] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Contracts] CHECK CONSTRAINT [FK_Contracts_User]
GO
ALTER TABLE [dbo].[Contracts]  WITH CHECK ADD  CONSTRAINT [FK_Contracts_User1] FOREIGN KEY([UpdatedBy])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Contracts] CHECK CONSTRAINT [FK_Contracts_User1]
GO
ALTER TABLE [dbo].[Extends]  WITH CHECK ADD  CONSTRAINT [FK_Extends_Contracts] FOREIGN KEY([ContractId])
REFERENCES [dbo].[Contracts] ([Id])
GO
ALTER TABLE [dbo].[Extends] CHECK CONSTRAINT [FK_Extends_Contracts]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_UserRole] FOREIGN KEY([RoleId])
REFERENCES [dbo].[UserRole] ([Id])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_UserRole]
GO
ALTER TABLE [dbo].[UserPermission]  WITH CHECK ADD  CONSTRAINT [FK_UserPermission_UserModule] FOREIGN KEY([ModuleId])
REFERENCES [dbo].[UserModule] ([Id])
GO
ALTER TABLE [dbo].[UserPermission] CHECK CONSTRAINT [FK_UserPermission_UserModule]
GO
ALTER TABLE [dbo].[UserPermission]  WITH CHECK ADD  CONSTRAINT [FK_UserPermission_UserRole] FOREIGN KEY([RoleId])
REFERENCES [dbo].[UserRole] ([Id])
GO
ALTER TABLE [dbo].[UserPermission] CHECK CONSTRAINT [FK_UserPermission_UserRole]
GO
