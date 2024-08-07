USE [OnDemandTutor]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 7/2/2024 9:56:53 PM ******/
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
/****** Object:  Table [dbo].[CoinManagement]    Script Date: 7/2/2024 9:56:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CoinManagement](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](450) NOT NULL,
	[Coin] [real] NOT NULL,
	[Date] [datetime2](7) NOT NULL,
	[TransactionId] [bigint] NOT NULL,
 CONSTRAINT [PK_CoinManagement] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Level]    Script Date: 7/2/2024 9:56:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Level](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Level] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Rating]    Script Date: 7/2/2024 9:56:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rating](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TutorId] [int] NOT NULL,
	[UserId] [nvarchar](450) NOT NULL,
	[Star] [real] NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Rating] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RefreshToken]    Script Date: 7/2/2024 9:56:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RefreshToken](
	[Id] [nvarchar](450) NOT NULL,
	[UserId] [nvarchar](450) NOT NULL,
	[JwtId] [nvarchar](max) NOT NULL,
	[Token] [nvarchar](max) NOT NULL,
	[IsUsed] [bit] NOT NULL,
	[IsRevoked] [bit] NOT NULL,
	[IssuedAt] [datetime2](7) NOT NULL,
	[ExpiredAt] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_RefreshToken] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Request]    Script Date: 7/2/2024 9:56:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Request](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RequestCategoryId] [int] NOT NULL,
	[UserId] [nvarchar](450) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[Status] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Request] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RequestCategory]    Script Date: 7/2/2024 9:56:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RequestCategory](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CategoryName] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_RequestCategory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Response]    Script Date: 7/2/2024 9:56:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Response](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RequestId] [int] NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[ResponseDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Response] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 7/2/2024 9:56:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[Id] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](256) NULL,
	[NormalizedName] [nvarchar](256) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RoleClaim]    Script Date: 7/2/2024 9:56:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RoleClaim](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_RoleClaim] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StudentJoin]    Script Date: 7/2/2024 9:56:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StudentJoin](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](450) NOT NULL,
	[SubjectLevelId] [int] NOT NULL,
 CONSTRAINT [PK_StudentJoin] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Subject]    Script Date: 7/2/2024 9:56:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Subject](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Subject] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SubjectLevel]    Script Date: 7/2/2024 9:56:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SubjectLevel](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LevelId] [int] NOT NULL,
	[SubjectId] [int] NOT NULL,
	[TutorId] [int] NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[Url] [nvarchar](max) NOT NULL,
	[Coin] [real] NOT NULL,
	[LimitMember] [int] NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Image] [nvarchar](max) NULL,
 CONSTRAINT [PK_SubjectLevel] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Time]    Script Date: 7/2/2024 9:56:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Time](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SubjectLevelId] [int] NOT NULL,
	[SlotName] [nvarchar](max) NOT NULL,
	[StartSlot] [datetime2](7) NOT NULL,
	[EndSlot] [datetime2](7) NOT NULL,
	[Date] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Time] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tutor]    Script Date: 7/2/2024 9:56:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tutor](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AcademicLevel] [nvarchar](max) NULL,
	[WorkPlace] [nvarchar](max) NULL,
	[OnlineStatus] [nvarchar](max) NULL,
	[AverageStar] [float] NOT NULL,
	[Degree] [nvarchar](max) NULL,
	[CreditCard] [nvarchar](max) NULL,
	[UserId] [nvarchar](450) NOT NULL,
	[Status] [nvarchar](max) NULL,
	[TutorServiceName] [nvarchar](max) NULL,
	[TutorServiceDescription] [nvarchar](max) NULL,
	[TutorServiceVideo] [nvarchar](max) NULL,
	[LearningMaterialDemo] [nvarchar](max) NULL,
 CONSTRAINT [PK_Tutor] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 7/2/2024 9:56:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Id] [nvarchar](450) NOT NULL,
	[FullName] [nvarchar](max) NULL,
	[IdentityCard] [nvarchar](max) NULL,
	[Dob] [datetime2](7) NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[Status] [bit] NOT NULL,
	[Gender] [nvarchar](max) NOT NULL,
	[Avatar] [nvarchar](max) NOT NULL,
	[UserName] [nvarchar](256) NULL,
	[NormalizedUserName] [nvarchar](256) NULL,
	[Email] [nvarchar](256) NULL,
	[NormalizedEmail] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEnd] [datetimeoffset](7) NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[IsLocked] [bit] NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserClaim]    Script Date: 7/2/2024 9:56:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserClaim](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_UserClaim] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserLogin]    Script Date: 7/2/2024 9:56:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserLogin](
	[LoginProvider] [nvarchar](450) NOT NULL,
	[ProviderKey] [nvarchar](450) NOT NULL,
	[ProviderDisplayName] [nvarchar](max) NULL,
	[UserId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_UserLogin] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserRoles]    Script Date: 7/2/2024 9:56:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRoles](
	[UserId] [nvarchar](450) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_UserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserToken]    Script Date: 7/2/2024 9:56:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserToken](
	[UserId] [nvarchar](450) NOT NULL,
	[LoginProvider] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](450) NOT NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_UserToken] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[LoginProvider] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240609181347_Add_All_Table', N'6.0.30')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240611152046_Add_LimitColumn_TblSubjectlevel', N'6.0.30')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240613174609_Update_Time', N'6.0.30')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240614083028_Add_Column_Name_Tbl_SubjectLevel', N'6.0.30')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240624100629_Add_TransactionId_Column', N'6.0.30')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240625134618_add_column_IsLocked', N'6.0.30')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240702144922_add_column_Image_SubjectLevel_tbl', N'6.0.30')
GO
SET IDENTITY_INSERT [dbo].[CoinManagement] ON 

INSERT [dbo].[CoinManagement] ([Id], [UserId], [Coin], [Date], [TransactionId]) VALUES (1, N'5f5b0490-f22b-44d7-a0ee-4331fc69cb85', 1000, CAST(N'2024-06-10T02:01:26.8428450' AS DateTime2), 0)
INSERT [dbo].[CoinManagement] ([Id], [UserId], [Coin], [Date], [TransactionId]) VALUES (3, N'5f5b0490-f22b-44d7-a0ee-4331fc69cb85', -300, CAST(N'2024-06-10T22:43:15.0782433' AS DateTime2), 0)
INSERT [dbo].[CoinManagement] ([Id], [UserId], [Coin], [Date], [TransactionId]) VALUES (4, N'886152d0-1aa7-42ba-86d1-3a0a0e423105', 300, CAST(N'2024-06-10T22:43:15.2887652' AS DateTime2), 0)
INSERT [dbo].[CoinManagement] ([Id], [UserId], [Coin], [Date], [TransactionId]) VALUES (5, N'bf580a91-67f2-416f-a293-d06bd69424b5', 7000, CAST(N'2024-06-11T00:24:56.2972161' AS DateTime2), 0)
INSERT [dbo].[CoinManagement] ([Id], [UserId], [Coin], [Date], [TransactionId]) VALUES (6, N'bf580a91-67f2-416f-a293-d06bd69424b5', -300, CAST(N'2024-06-11T00:25:19.5377040' AS DateTime2), 0)
INSERT [dbo].[CoinManagement] ([Id], [UserId], [Coin], [Date], [TransactionId]) VALUES (7, N'886152d0-1aa7-42ba-86d1-3a0a0e423105', 300, CAST(N'2024-06-11T00:25:19.5572066' AS DateTime2), 0)
INSERT [dbo].[CoinManagement] ([Id], [UserId], [Coin], [Date], [TransactionId]) VALUES (8, N'5f5b0490-f22b-44d7-a0ee-4331fc69cb85', 500, CAST(N'2024-06-11T20:43:40.4912406' AS DateTime2), 0)
INSERT [dbo].[CoinManagement] ([Id], [UserId], [Coin], [Date], [TransactionId]) VALUES (9, N'5f5b0490-f22b-44d7-a0ee-4331fc69cb85', -200, CAST(N'2024-06-11T20:44:47.2507666' AS DateTime2), 0)
INSERT [dbo].[CoinManagement] ([Id], [UserId], [Coin], [Date], [TransactionId]) VALUES (10, N'886152d0-1aa7-42ba-86d1-3a0a0e423105', 200, CAST(N'2024-06-11T20:44:47.3151797' AS DateTime2), 0)
INSERT [dbo].[CoinManagement] ([Id], [UserId], [Coin], [Date], [TransactionId]) VALUES (11, N'5f5b0490-f22b-44d7-a0ee-4331fc69cb85', -300, CAST(N'2024-06-13T11:55:44.1205414' AS DateTime2), 0)
INSERT [dbo].[CoinManagement] ([Id], [UserId], [Coin], [Date], [TransactionId]) VALUES (12, N'886152d0-1aa7-42ba-86d1-3a0a0e423105', 300, CAST(N'2024-06-13T11:55:44.3242826' AS DateTime2), 0)
INSERT [dbo].[CoinManagement] ([Id], [UserId], [Coin], [Date], [TransactionId]) VALUES (13, N'5f5b0490-f22b-44d7-a0ee-4331fc69cb85', -200, CAST(N'2024-06-13T12:00:09.1933966' AS DateTime2), 0)
INSERT [dbo].[CoinManagement] ([Id], [UserId], [Coin], [Date], [TransactionId]) VALUES (14, N'886152d0-1aa7-42ba-86d1-3a0a0e423105', 200, CAST(N'2024-06-13T12:00:09.4048419' AS DateTime2), 0)
INSERT [dbo].[CoinManagement] ([Id], [UserId], [Coin], [Date], [TransactionId]) VALUES (15, N'5f5b0490-f22b-44d7-a0ee-4331fc69cb85', -200, CAST(N'2024-06-13T12:12:27.4330557' AS DateTime2), 0)
INSERT [dbo].[CoinManagement] ([Id], [UserId], [Coin], [Date], [TransactionId]) VALUES (16, N'886152d0-1aa7-42ba-86d1-3a0a0e423105', 200, CAST(N'2024-06-13T12:12:27.6328441' AS DateTime2), 0)
INSERT [dbo].[CoinManagement] ([Id], [UserId], [Coin], [Date], [TransactionId]) VALUES (17, N'5f5b0490-f22b-44d7-a0ee-4331fc69cb85', -300, CAST(N'2024-06-13T14:57:27.8002172' AS DateTime2), 0)
INSERT [dbo].[CoinManagement] ([Id], [UserId], [Coin], [Date], [TransactionId]) VALUES (18, N'886152d0-1aa7-42ba-86d1-3a0a0e423105', 300, CAST(N'2024-06-13T14:57:28.0145179' AS DateTime2), 0)
INSERT [dbo].[CoinManagement] ([Id], [UserId], [Coin], [Date], [TransactionId]) VALUES (19, N'3ea23fe3-013b-4f30-812f-5517ec093aaf', -300, CAST(N'2024-06-13T15:00:02.8669099' AS DateTime2), 0)
INSERT [dbo].[CoinManagement] ([Id], [UserId], [Coin], [Date], [TransactionId]) VALUES (20, N'886152d0-1aa7-42ba-86d1-3a0a0e423105', 300, CAST(N'2024-06-13T15:00:02.8862198' AS DateTime2), 0)
INSERT [dbo].[CoinManagement] ([Id], [UserId], [Coin], [Date], [TransactionId]) VALUES (21, N'886152d0-1aa7-42ba-86d1-3a0a0e423105', -500, CAST(N'2024-06-13T16:18:09.8110332' AS DateTime2), 0)
INSERT [dbo].[CoinManagement] ([Id], [UserId], [Coin], [Date], [TransactionId]) VALUES (22, N'5f5b0490-f22b-44d7-a0ee-4331fc69cb85', 500, CAST(N'2024-06-13T16:18:12.1024693' AS DateTime2), 0)
INSERT [dbo].[CoinManagement] ([Id], [UserId], [Coin], [Date], [TransactionId]) VALUES (23, N'5f5b0490-f22b-44d7-a0ee-4331fc69cb85', -500, CAST(N'2024-06-14T14:10:54.8081961' AS DateTime2), 0)
INSERT [dbo].[CoinManagement] ([Id], [UserId], [Coin], [Date], [TransactionId]) VALUES (24, N'886152d0-1aa7-42ba-86d1-3a0a0e423105', 500, CAST(N'2024-06-14T14:10:59.0828738' AS DateTime2), 0)
INSERT [dbo].[CoinManagement] ([Id], [UserId], [Coin], [Date], [TransactionId]) VALUES (25, N'5f5b0490-f22b-44d7-a0ee-4331fc69cb85', 500, CAST(N'2024-06-14T14:13:03.6910502' AS DateTime2), 0)
INSERT [dbo].[CoinManagement] ([Id], [UserId], [Coin], [Date], [TransactionId]) VALUES (26, N'5f5b0490-f22b-44d7-a0ee-4331fc69cb85', -200, CAST(N'2024-06-14T14:14:36.9846393' AS DateTime2), 0)
INSERT [dbo].[CoinManagement] ([Id], [UserId], [Coin], [Date], [TransactionId]) VALUES (29, N'886152d0-1aa7-42ba-86d1-3a0a0e423105', 500, CAST(N'2024-06-14T14:15:43.9594556' AS DateTime2), 0)
INSERT [dbo].[CoinManagement] ([Id], [UserId], [Coin], [Date], [TransactionId]) VALUES (30, N'5f5b0490-f22b-44d7-a0ee-4331fc69cb85', 1000, CAST(N'2024-06-17T20:05:25.4725530' AS DateTime2), 0)
INSERT [dbo].[CoinManagement] ([Id], [UserId], [Coin], [Date], [TransactionId]) VALUES (31, N'5f5b0490-f22b-44d7-a0ee-4331fc69cb85', 1000, CAST(N'2024-06-24T17:21:57.9794896' AS DateTime2), 14476004)
INSERT [dbo].[CoinManagement] ([Id], [UserId], [Coin], [Date], [TransactionId]) VALUES (32, N'5f5b0490-f22b-44d7-a0ee-4331fc69cb85', 1000, CAST(N'2024-06-24T17:23:40.9016288' AS DateTime2), 14476011)
SET IDENTITY_INSERT [dbo].[CoinManagement] OFF
GO
SET IDENTITY_INSERT [dbo].[Level] ON 

INSERT [dbo].[Level] ([Id], [Name]) VALUES (1, N'Lớp 10')
INSERT [dbo].[Level] ([Id], [Name]) VALUES (2, N'Lớp 11')
INSERT [dbo].[Level] ([Id], [Name]) VALUES (3, N'Lớp 12')
SET IDENTITY_INSERT [dbo].[Level] OFF
GO
SET IDENTITY_INSERT [dbo].[Rating] ON 

INSERT [dbo].[Rating] ([Id], [TutorId], [UserId], [Star], [Description]) VALUES (1, 4, N'5f5b0490-f22b-44d7-a0ee-4331fc69cb85', 5, N'Dạy hay vl')
INSERT [dbo].[Rating] ([Id], [TutorId], [UserId], [Star], [Description]) VALUES (2, 4, N'5f5b0490-f22b-44d7-a0ee-4331fc69cb85', 3, N'dạy tạm')
INSERT [dbo].[Rating] ([Id], [TutorId], [UserId], [Star], [Description]) VALUES (3, 1, N'5f5b0490-f22b-44d7-a0ee-4331fc69cb85', 4, N'tạm được')
INSERT [dbo].[Rating] ([Id], [TutorId], [UserId], [Star], [Description]) VALUES (4, 1, N'5f5b0490-f22b-44d7-a0ee-4331fc69cb85', 2, N'tệ')
SET IDENTITY_INSERT [dbo].[Rating] OFF
GO
INSERT [dbo].[RefreshToken] ([Id], [UserId], [JwtId], [Token], [IsUsed], [IsRevoked], [IssuedAt], [ExpiredAt]) VALUES (N'2be7d04d-1c25-4985-bb0c-296e2ca98363', N'bf580a91-67f2-416f-a293-d06bd69424b5', N'695d0206-e35f-4e50-81d2-0b5111f17071', N'047Ox4nY/58MOHWEmvgO5qk8leE1IqvEfAT+FJBjEiI=', 0, 0, CAST(N'2024-06-11T00:21:47.9683602' AS DateTime2), CAST(N'2024-06-11T01:21:47.9684730' AS DateTime2))
INSERT [dbo].[RefreshToken] ([Id], [UserId], [JwtId], [Token], [IsUsed], [IsRevoked], [IssuedAt], [ExpiredAt]) VALUES (N'55a904d9-85a1-4610-bddf-a813d901b414', N'b75dc8dc-3c0b-4ffe-8d03-f23d5df0b6e3', N'385cab8c-fd90-485e-83ed-46599d3d3fb8', N'R+ZUxLaRmMujO9TBO331J0+L7GThqwlyQC5CxYb+gjk=', 0, 0, CAST(N'2024-06-25T21:04:49.3969470' AS DateTime2), CAST(N'2024-06-25T22:04:49.3970372' AS DateTime2))
INSERT [dbo].[RefreshToken] ([Id], [UserId], [JwtId], [Token], [IsUsed], [IsRevoked], [IssuedAt], [ExpiredAt]) VALUES (N'65ed51d7-3268-4f3a-a9a9-ff8d0fc1a2ce', N'd34c6059-4700-49fd-b1fa-6227e2be5470', N'08c8d7c7-8e4f-457b-a6e5-04c5a01ec7db', N'63KX1AXKJHCh4K7E00FEuyWGjkZcFvfCEXntMeEqpBA=', 0, 0, CAST(N'2024-06-12T17:32:07.9766130' AS DateTime2), CAST(N'2024-06-12T18:32:07.9769329' AS DateTime2))
INSERT [dbo].[RefreshToken] ([Id], [UserId], [JwtId], [Token], [IsUsed], [IsRevoked], [IssuedAt], [ExpiredAt]) VALUES (N'8c68d706-88de-49f8-aeeb-400e27fc1238', N'886152d0-1aa7-42ba-86d1-3a0a0e423105', N'74981aa4-be4d-4319-b096-6c43990397f2', N'rPyWP2qLmByaBwLij06hgdSE4djBUzPdVo8fFln6GG8=', 0, 0, CAST(N'2024-06-28T01:19:17.1230659' AS DateTime2), CAST(N'2024-06-28T02:19:17.1231516' AS DateTime2))
INSERT [dbo].[RefreshToken] ([Id], [UserId], [JwtId], [Token], [IsUsed], [IsRevoked], [IssuedAt], [ExpiredAt]) VALUES (N'957bb331-010c-4781-80a3-e66104b0aeaf', N'ec446432-53f8-4e5f-84b2-8a33162970f3', N'8bdf9da2-521c-4bdd-8355-0919fa5a499f', N'pB58Cr7iXaRJ80kb0U0F00E6kBPuqVdzLb69wGvnaLU=', 0, 0, CAST(N'2024-06-26T00:38:06.1931383' AS DateTime2), CAST(N'2024-06-26T01:38:06.1932294' AS DateTime2))
INSERT [dbo].[RefreshToken] ([Id], [UserId], [JwtId], [Token], [IsUsed], [IsRevoked], [IssuedAt], [ExpiredAt]) VALUES (N'992a85a2-4bf7-49f0-b916-f525023e3bb2', N'0c401c73-f733-423a-9e49-839b4ba3241c', N'45a2f053-2546-4917-a131-bb947f32308a', N'RyZnvCcCiyK40e+fFgznLbgi4Bins4/PWF6irLKffto=', 0, 0, CAST(N'2024-06-27T19:13:16.1831104' AS DateTime2), CAST(N'2024-06-27T20:13:16.1832005' AS DateTime2))
INSERT [dbo].[RefreshToken] ([Id], [UserId], [JwtId], [Token], [IsUsed], [IsRevoked], [IssuedAt], [ExpiredAt]) VALUES (N'bd2a4092-57ab-4f47-b77c-0e36265f0bde', N'3ea23fe3-013b-4f30-812f-5517ec093aaf', N'3e11bbea-41b8-4618-b82e-11e26503e7d5', N'IK5fnkJZbYSJBV1hUGb9n/IhZnMNNs2c376oXsuWyZg=', 0, 0, CAST(N'2024-06-13T14:58:52.9259138' AS DateTime2), CAST(N'2024-06-13T15:58:52.9259158' AS DateTime2))
INSERT [dbo].[RefreshToken] ([Id], [UserId], [JwtId], [Token], [IsUsed], [IsRevoked], [IssuedAt], [ExpiredAt]) VALUES (N'bd3fe7da-91f7-479a-9d40-03c52d4b124d', N'5f5b0490-f22b-44d7-a0ee-4331fc69cb85', N'2d849be2-fd99-457d-9e44-9222975d2e9f', N'3gWj+aco1mtqoM08eKGtsDQHXJbzhm6u1WWxf5U2fFw=', 0, 0, CAST(N'2024-06-28T00:52:55.4091457' AS DateTime2), CAST(N'2024-06-28T01:52:55.4091659' AS DateTime2))
GO
SET IDENTITY_INSERT [dbo].[Request] ON 

INSERT [dbo].[Request] ([Id], [RequestCategoryId], [UserId], [Description], [CreatedDate], [Status]) VALUES (3, 1, N'd34c6059-4700-49fd-b1fa-6227e2be5470', N'Acc của t có vấn đề rồi', CAST(N'2024-06-12T17:32:53.1628704' AS DateTime2), N'Chờ xử lý')
SET IDENTITY_INSERT [dbo].[Request] OFF
GO
SET IDENTITY_INSERT [dbo].[RequestCategory] ON 

INSERT [dbo].[RequestCategory] ([Id], [CategoryName]) VALUES (1, N'Account Support')
INSERT [dbo].[RequestCategory] ([Id], [CategoryName]) VALUES (3, N'Payment Support')
SET IDENTITY_INSERT [dbo].[RequestCategory] OFF
GO
INSERT [dbo].[Role] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'21fbb4e5-6aa0-415b-b291-cef7d067d04e', N'Admin', N'ADMIN', N'1')
INSERT [dbo].[Role] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'aca0397a-ca5b-4ccf-b742-14db92651480', N'Moderator', N'MODERATOR', N'2')
INSERT [dbo].[Role] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'b378a07f-e146-434e-88ce-1cd7dd087dc2', N'Student', N'STUDENT', N'4')
INSERT [dbo].[Role] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'c530249e-5b13-4566-8e5b-36f66e958ef2', N'Tutor', N'TUTOR', N'3')
GO
SET IDENTITY_INSERT [dbo].[StudentJoin] ON 

INSERT [dbo].[StudentJoin] ([Id], [UserId], [SubjectLevelId]) VALUES (3, N'bf580a91-67f2-416f-a293-d06bd69424b5', 9)
INSERT [dbo].[StudentJoin] ([Id], [UserId], [SubjectLevelId]) VALUES (7, N'5f5b0490-f22b-44d7-a0ee-4331fc69cb85', 9)
INSERT [dbo].[StudentJoin] ([Id], [UserId], [SubjectLevelId]) VALUES (8, N'3ea23fe3-013b-4f30-812f-5517ec093aaf', 9)
SET IDENTITY_INSERT [dbo].[StudentJoin] OFF
GO
SET IDENTITY_INSERT [dbo].[Subject] ON 

INSERT [dbo].[Subject] ([Id], [Name]) VALUES (1, N'Toán')
INSERT [dbo].[Subject] ([Id], [Name]) VALUES (2, N'Lý')
INSERT [dbo].[Subject] ([Id], [Name]) VALUES (3, N'Hóa')
INSERT [dbo].[Subject] ([Id], [Name]) VALUES (4, N'Văn')
INSERT [dbo].[Subject] ([Id], [Name]) VALUES (5, N'Sử')
INSERT [dbo].[Subject] ([Id], [Name]) VALUES (6, N'Địa')
INSERT [dbo].[Subject] ([Id], [Name]) VALUES (10, N'GDCD')
SET IDENTITY_INSERT [dbo].[Subject] OFF
GO
SET IDENTITY_INSERT [dbo].[SubjectLevel] ON 

INSERT [dbo].[SubjectLevel] ([Id], [LevelId], [SubjectId], [TutorId], [Description], [Url], [Coin], [LimitMember], [Name], [Image]) VALUES (9, 3, 1, 4, N'string', N'string', 300, 3, N'Khóa lý triệu đô', NULL)
INSERT [dbo].[SubjectLevel] ([Id], [LevelId], [SubjectId], [TutorId], [Description], [Url], [Coin], [LimitMember], [Name], [Image]) VALUES (11, 1, 3, 4, N'hóa chất', N'string hóa', 500, 1, N'dạy hóa CHẤT', NULL)
INSERT [dbo].[SubjectLevel] ([Id], [LevelId], [SubjectId], [TutorId], [Description], [Url], [Coin], [LimitMember], [Name], [Image]) VALUES (12, 3, 4, 4, N'string', N'stringurl', 500, 4, N'khóa 12', NULL)
INSERT [dbo].[SubjectLevel] ([Id], [LevelId], [SubjectId], [TutorId], [Description], [Url], [Coin], [LimitMember], [Name], [Image]) VALUES (14, 2, 2, 1, N'Ngu lý', N'string', 500, 5, N'khóa dốt lý', NULL)
SET IDENTITY_INSERT [dbo].[SubjectLevel] OFF
GO
SET IDENTITY_INSERT [dbo].[Time] ON 

INSERT [dbo].[Time] ([Id], [SubjectLevelId], [SlotName], [StartSlot], [EndSlot], [Date]) VALUES (4, 9, N'Buổi 1', CAST(N'2024-06-14T19:00:00.9360000' AS DateTime2), CAST(N'2024-06-14T21:00:00.9360000' AS DateTime2), CAST(N'2024-06-14T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Time] ([Id], [SubjectLevelId], [SlotName], [StartSlot], [EndSlot], [Date]) VALUES (5, 9, N'buổi 2', CAST(N'2024-06-14T16:00:00.0760000' AS DateTime2), CAST(N'2024-06-14T18:00:00.0760000' AS DateTime2), CAST(N'2024-06-14T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Time] ([Id], [SubjectLevelId], [SlotName], [StartSlot], [EndSlot], [Date]) VALUES (6, 9, N'buổi 2', CAST(N'2024-06-14T14:00:00.0760000' AS DateTime2), CAST(N'2024-06-14T16:00:00.0760000' AS DateTime2), CAST(N'2024-06-14T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Time] ([Id], [SubjectLevelId], [SlotName], [StartSlot], [EndSlot], [Date]) VALUES (7, 9, N'Buổi 3', CAST(N'2024-06-20T19:00:00.0000000' AS DateTime2), CAST(N'2024-06-20T21:00:00.0000000' AS DateTime2), CAST(N'2024-06-20T00:00:00.0000000' AS DateTime2))
SET IDENTITY_INSERT [dbo].[Time] OFF
GO
SET IDENTITY_INSERT [dbo].[Tutor] ON 

INSERT [dbo].[Tutor] ([Id], [AcademicLevel], [WorkPlace], [OnlineStatus], [AverageStar], [Degree], [CreditCard], [UserId], [Status], [TutorServiceName], [TutorServiceDescription], [TutorServiceVideo], [LearningMaterialDemo]) VALUES (1, N'University', N'FSoft', NULL, 0, N'bacherlor', N'000000', N'b75dc8dc-3c0b-4ffe-8d03-f23d5df0b6e3', N'Chấp thuận', N'vua code', N'string', N'string', N'string')
INSERT [dbo].[Tutor] ([Id], [AcademicLevel], [WorkPlace], [OnlineStatus], [AverageStar], [Degree], [CreditCard], [UserId], [Status], [TutorServiceName], [TutorServiceDescription], [TutorServiceVideo], [LearningMaterialDemo]) VALUES (3, N'string1', N'string1', NULL, 0, N'string1', N'string1', N'd34c6059-4700-49fd-b1fa-6227e2be5470', N'Chờ phê duyệt', N'string1', N'string1', N'string1', N'string1')
INSERT [dbo].[Tutor] ([Id], [AcademicLevel], [WorkPlace], [OnlineStatus], [AverageStar], [Degree], [CreditCard], [UserId], [Status], [TutorServiceName], [TutorServiceDescription], [TutorServiceVideo], [LearningMaterialDemo]) VALUES (4, N'Uni', N'FUni', N'Đang online', 0, N'Engineer', N'111111', N'886152d0-1aa7-42ba-86d1-3a0a0e423105', N'Chấp thuận', N'code gym', N'vua code', N'code video', N'tutorial code')
SET IDENTITY_INSERT [dbo].[Tutor] OFF
GO
INSERT [dbo].[User] ([Id], [FullName], [IdentityCard], [Dob], [CreatedDate], [Status], [Gender], [Avatar], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [IsLocked]) VALUES (N'0c401c73-f733-423a-9e49-839b4ba3241c', N'ADMIN', N'admin', CAST(N'2024-06-09T00:00:00.0000000' AS DateTime2), CAST(N'2024-06-10T01:45:21.1157674' AS DateTime2), 0, N'male', N'admin dep trai', N'minhtam14231204@gmail.com', N'MINHTAM14231204@GMAIL.COM', N'minhtam14231204@gmail.com', N'MINHTAM14231204@GMAIL.COM', 0, N'AQAAAAEAACcQAAAAEJtU8DpOEUJkept/y4CcWtH1hDR37Oys8Ro2O+eiqNxRuyX8gteZRtC8DySAaRIDWA==', N'FQVNJZXRFZQFYPVNSI5WXLF72MCSFCEH', N'dd404895-3323-404c-bdc6-4dbd09a4c189', N'0942775673', 0, 0, NULL, 1, 0, 0)
INSERT [dbo].[User] ([Id], [FullName], [IdentityCard], [Dob], [CreatedDate], [Status], [Gender], [Avatar], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [IsLocked]) VALUES (N'3ea23fe3-013b-4f30-812f-5517ec093aaf', N'NguyenVanA', N'anhA', CAST(N'2024-06-04T00:00:00.0000000' AS DateTime2), CAST(N'2024-06-10T01:47:40.1206328' AS DateTime2), 0, N'male', N'ava1', N'nguyenvana@gmail.com', N'NGUYENVANA@GMAIL.COM', N'nguyenvana@gmail.com', N'NGUYENVANA@GMAIL.COM', 0, N'AQAAAAEAACcQAAAAEM0msXFXLmObERxqk83W8Aps30KVgWL6tNdArjcEkQthEd2XUD+4qhp17+Gxq1z92g==', N'UVSRFGWCBXORGQ4YKSSSBSLJ5TQ3US55', N'1c3902d2-6b33-43d9-94fb-9eabb5efd942', N'0123456789', 0, 0, NULL, 1, 0, 0)
INSERT [dbo].[User] ([Id], [FullName], [IdentityCard], [Dob], [CreatedDate], [Status], [Gender], [Avatar], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [IsLocked]) VALUES (N'5f5b0490-f22b-44d7-a0ee-4331fc69cb85', N'TamNguyenDev', N'card1', CAST(N'2004-12-23T00:00:00.0000000' AS DateTime2), CAST(N'2024-06-10T01:55:54.4413485' AS DateTime2), 0, N'male', N'anh tam dep trai', N'th3m1nd05@gmail.com', N'TH3M1ND05@GMAIL.COM', N'th3m1nd05@gmail.com', N'TH3M1ND05@GMAIL.COM', 0, N'AQAAAAEAACcQAAAAEKUZm3fylwHVrQInOkfcuTKzlsgNL1w6nCF5xEve1q/9QWjUl+MgUxHQDxsBEy/fYA==', N'DRTEYUKNIEIR55DCY76J3LYCLJLAFWMQ', N'880b36c6-3398-4a62-bfe0-54e3986bf4fa', N'0942775673', 0, 0, NULL, 1, 0, 0)
INSERT [dbo].[User] ([Id], [FullName], [IdentityCard], [Dob], [CreatedDate], [Status], [Gender], [Avatar], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [IsLocked]) VALUES (N'886152d0-1aa7-42ba-86d1-3a0a0e423105', N'TeacherTam', N'teacher tam', CAST(N'2024-06-10T00:00:00.0000000' AS DateTime2), CAST(N'2024-06-10T18:28:00.3085314' AS DateTime2), 1, N'male', N'tam dep trai', N'tamnguyen.231204@gmail.com', N'TAMNGUYEN.231204@GMAIL.COM', N'tamnguyen.231204@gmail.com', N'TAMNGUYEN.231204@GMAIL.COM', 0, N'AQAAAAEAACcQAAAAEMirmFwLD3zZzdU5FZ09glhCJyjaFYhmZrRwmEZqmt0bgKhhuPYEkbZrgrUKoJ0ZWg==', N'MCEDTTLHFEIRZTOUFTYTLQQ3NVF5RN62', N'35e48eb3-d85e-484e-94e5-3fb484c07740', N'0708483243', 0, 0, NULL, 1, 0, 0)
INSERT [dbo].[User] ([Id], [FullName], [IdentityCard], [Dob], [CreatedDate], [Status], [Gender], [Avatar], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [IsLocked]) VALUES (N'b75dc8dc-3c0b-4ffe-8d03-f23d5df0b6e3', N'NguyenVanC', N'anhC', CAST(N'2024-06-05T00:00:00.0000000' AS DateTime2), CAST(N'2024-06-10T01:23:01.4168100' AS DateTime2), 0, N'male', N'Cdeptrai', N'NguyenVanC@gmail.com', N'NGUYENVANC@GMAIL.COM', N'NguyenVanC@gmail.com', N'NGUYENVANC@GMAIL.COM', 0, N'AQAAAAEAACcQAAAAEFyD8YaBkf1VIJ9pPkBAKC6WbYCmwz9lFc8s+lYtDtF5+tNHtMleUAx5MCsADBYdUw==', N'WVS4QJAQH2WWB4F6OKC6YOV7WQTPQR4D', N'c83634da-4d20-49f9-acee-97958d6537f1', N'0802748371', 0, 0, NULL, 1, 0, 1)
INSERT [dbo].[User] ([Id], [FullName], [IdentityCard], [Dob], [CreatedDate], [Status], [Gender], [Avatar], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [IsLocked]) VALUES (N'bf580a91-67f2-416f-a293-d06bd69424b5', N'TamNguyenTrader', N'tamnguyen', CAST(N'2024-06-10T00:00:00.0000000' AS DateTime2), CAST(N'2024-06-11T00:20:49.8335026' AS DateTime2), 0, N'male', N'string', N'tamnguyenbo231204@gmail.com', N'TAMNGUYENBO231204@GMAIL.COM', N'tamnguyenbo231204@gmail.com', N'TAMNGUYENBO231204@GMAIL.COM', 0, N'AQAAAAEAACcQAAAAEKr3LNcR5UvImVNiqNzJzmpGbLM9UMftcJJNbEN9UTJoC+NbRKnc+bKs3bUtVXKuHQ==', N'UP7GO6KY4LDL75ECZVMK3DMKFY2EWQQB', N'a2eea6a7-d85d-4dff-986c-eaaaa36d679e', N'0912387465', 0, 0, NULL, 1, 0, 0)
INSERT [dbo].[User] ([Id], [FullName], [IdentityCard], [Dob], [CreatedDate], [Status], [Gender], [Avatar], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [IsLocked]) VALUES (N'd34c6059-4700-49fd-b1fa-6227e2be5470', N'TranVanB', N'cardiB', CAST(N'2024-05-29T00:00:00.0000000' AS DateTime2), CAST(N'2024-06-10T01:48:41.8839045' AS DateTime2), 0, N'male', N'avaB', N'TranVanB@gmail.com', N'TRANVANB@GMAIL.COM', N'TranVanB@gmail.com', N'TRANVANB@GMAIL.COM', 0, N'AQAAAAEAACcQAAAAEITF+U6oGjGh2a4+Yay2rxA1gIWg++QkeN1K+I3YZgZlGWkhWjRB4AUQEIz3XShkhw==', N'4BJLMVCTF3A44ZVG7ZWC2P7T4QYV5IO7', N'87eb6c73-5b3c-466a-8e6f-914764499875', N'0123456789', 0, 0, NULL, 1, 0, 0)
INSERT [dbo].[User] ([Id], [FullName], [IdentityCard], [Dob], [CreatedDate], [Status], [Gender], [Avatar], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [IsLocked]) VALUES (N'ec446432-53f8-4e5f-84b2-8a33162970f3', N'Moderator', N'mod', CAST(N'2024-06-09T00:00:00.0000000' AS DateTime2), CAST(N'2024-06-10T01:50:16.5616178' AS DateTime2), 0, N'male', N'mod xinh gai', N'moderator@gmail.com', N'MODERATOR@GMAIL.COM', N'moderator@gmail.com', N'MODERATOR@GMAIL.COM', 0, N'AQAAAAEAACcQAAAAEBUKwH55etmPtdjOt1PeoGaXCfK20SfodLd0ABImNbdtOoHMC+p4+170wqDlBE9vRQ==', N'H7MMR4ROKWDF4SMTEAPJJID5FGWGFQP6', N'6db59edb-8523-4355-8ab8-4dced4efaad1', N'0892736481', 0, 0, NULL, 1, 0, 0)
GO
INSERT [dbo].[UserRoles] ([UserId], [RoleId]) VALUES (N'0c401c73-f733-423a-9e49-839b4ba3241c', N'21fbb4e5-6aa0-415b-b291-cef7d067d04e')
INSERT [dbo].[UserRoles] ([UserId], [RoleId]) VALUES (N'ec446432-53f8-4e5f-84b2-8a33162970f3', N'aca0397a-ca5b-4ccf-b742-14db92651480')
INSERT [dbo].[UserRoles] ([UserId], [RoleId]) VALUES (N'3ea23fe3-013b-4f30-812f-5517ec093aaf', N'b378a07f-e146-434e-88ce-1cd7dd087dc2')
INSERT [dbo].[UserRoles] ([UserId], [RoleId]) VALUES (N'5f5b0490-f22b-44d7-a0ee-4331fc69cb85', N'b378a07f-e146-434e-88ce-1cd7dd087dc2')
INSERT [dbo].[UserRoles] ([UserId], [RoleId]) VALUES (N'bf580a91-67f2-416f-a293-d06bd69424b5', N'b378a07f-e146-434e-88ce-1cd7dd087dc2')
INSERT [dbo].[UserRoles] ([UserId], [RoleId]) VALUES (N'886152d0-1aa7-42ba-86d1-3a0a0e423105', N'c530249e-5b13-4566-8e5b-36f66e958ef2')
INSERT [dbo].[UserRoles] ([UserId], [RoleId]) VALUES (N'b75dc8dc-3c0b-4ffe-8d03-f23d5df0b6e3', N'c530249e-5b13-4566-8e5b-36f66e958ef2')
INSERT [dbo].[UserRoles] ([UserId], [RoleId]) VALUES (N'd34c6059-4700-49fd-b1fa-6227e2be5470', N'c530249e-5b13-4566-8e5b-36f66e958ef2')
GO
INSERT [dbo].[UserToken] ([UserId], [LoginProvider], [Name], [Value]) VALUES (N'0c401c73-f733-423a-9e49-839b4ba3241c', N'ODT_Api', N'AccessToken', N'eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9lbWFpbGFkZHJlc3MiOiJtaW5odGFtMTQyMzEyMDRAZ21haWwuY29tIiwianRpIjoiNDVhMmYwNTMtMjU0Ni00OTE3LWExMzEtYmI5NDdmMzIzMDhhIiwiSWQiOiIwYzQwMWM3My1mNzMzLTQyM2EtOWU0OS04MzliNGJhMzI0MWMiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJBZG1pbiIsImV4cCI6MTcxOTQ5MTU5NSwiaXNzIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NzI1OSIsImF1ZCI6IlVzZXIifQ.RTXghKDLqFaPBX8ud1TlWYsPXNm0310WVDjk5I60epEioX6kt0Z4M2CwZt1nDUkJsN-Qg86SzbXVXASdw91rBA')
INSERT [dbo].[UserToken] ([UserId], [LoginProvider], [Name], [Value]) VALUES (N'3ea23fe3-013b-4f30-812f-5517ec093aaf', N'ODT_Api', N'AccessToken', N'eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9lbWFpbGFkZHJlc3MiOiJuZ3V5ZW52YW5hQGdtYWlsLmNvbSIsImp0aSI6IjNlMTFiYmVhLTQxYjgtNDYxOC1iODJlLTExZTI2NTAzZTdkNSIsIklkIjoiM2VhMjNmZTMtMDEzYi00ZjMwLTgxMmYtNTUxN2VjMDkzYWFmIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoiU3R1ZGVudCIsImV4cCI6MTcxODI2NjczMiwiaXNzIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NzAxNCIsImF1ZCI6IlVzZXIifQ.ffMP1HwZlsFaseQTSGJcPfexVhuApAsiDpAeAsQINMzfJNgc2NGS2hEJGEqIn-NRQDu2Ayg4PPFNjRrMajb0tw')
INSERT [dbo].[UserToken] ([UserId], [LoginProvider], [Name], [Value]) VALUES (N'5f5b0490-f22b-44d7-a0ee-4331fc69cb85', N'ODT_Api', N'AccessToken', N'eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9lbWFpbGFkZHJlc3MiOiJ0aDNtMW5kMDVAZ21haWwuY29tIiwianRpIjoiMmQ4NDliZTItZmQ5OS00NTdkLTllNDQtOTIyMjk3NWQyZTlmIiwiSWQiOiI1ZjViMDQ5MC1mMjJiLTQ0ZDctYTBlZS00MzMxZmM2OWNiODUiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJTdHVkZW50IiwiZXhwIjoxNzE5NTExOTc1LCJpc3MiOiJodHRwczovL2xvY2FsaG9zdDo3MjU5IiwiYXVkIjoiVXNlciJ9.uAsgdBybTU_OIFIxZrLNTIIubfdSbYHg_6rK6cw40FlU8d4ddJP30qEoo8oy4eNl8nWfVBBkomrZcOHEqOiLeQ')
INSERT [dbo].[UserToken] ([UserId], [LoginProvider], [Name], [Value]) VALUES (N'886152d0-1aa7-42ba-86d1-3a0a0e423105', N'ODT_Api', N'AccessToken', N'eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9lbWFpbGFkZHJlc3MiOiJ0YW1uZ3V5ZW4uMjMxMjA0QGdtYWlsLmNvbSIsImp0aSI6Ijc0OTgxYWE0LWJlNGQtNDMxOS1iMDk2LTZjNDM5OTAzOTdmMiIsIklkIjoiODg2MTUyZDAtMWFhNy00MmJhLTg2ZDEtM2EwYTBlNDIzMTA1IiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoiVHV0b3IiLCJleHAiOjE3MTk1MTM1NTYsImlzcyI6Imh0dHBzOi8vbG9jYWxob3N0OjcyNTkiLCJhdWQiOiJVc2VyIn0.EvMfuqHmjk1z9zZoOQstZg0yGD-_NDNj7TIRpNirXoe1v-yvROrxN0GugjMRnJNdtggjb1h51OghZ1Zicml0VA')
INSERT [dbo].[UserToken] ([UserId], [LoginProvider], [Name], [Value]) VALUES (N'b75dc8dc-3c0b-4ffe-8d03-f23d5df0b6e3', N'ODT_Api', N'AccessToken', N'eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9lbWFpbGFkZHJlc3MiOiJOZ3V5ZW5WYW5DQGdtYWlsLmNvbSIsImp0aSI6IjM4NWNhYjhjLWZkOTAtNDg1ZS04M2VkLTQ2NTk5ZDNkM2ZiOCIsIklkIjoiYjc1ZGM4ZGMtM2MwYi00ZmZlLThkMDMtZjIzZDVkZjBiNmUzIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoiVHV0b3IiLCJleHAiOjE3MTkzMjU0ODgsImlzcyI6Imh0dHBzOi8vbG9jYWxob3N0OjcyNTkiLCJhdWQiOiJVc2VyIn0.uscVB9aTN4l0AnnIXFJxpkknlpbnAfEOhdo4cyroluE4lKaZvEalhl8DmMbsU6BYr3gMaAvXaoWIuM5gpGyo8Q')
INSERT [dbo].[UserToken] ([UserId], [LoginProvider], [Name], [Value]) VALUES (N'bf580a91-67f2-416f-a293-d06bd69424b5', N'ODT_Api', N'AccessToken', N'eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9lbWFpbGFkZHJlc3MiOiJ0YW1uZ3V5ZW5ibzIzMTIwNEBnbWFpbC5jb20iLCJqdGkiOiI2OTVkMDIwNi1lMzVmLTRlNTAtODFkMi0wYjUxMTFmMTcwNzEiLCJJZCI6ImJmNTgwYTkxLTY3ZjItNDE2Zi1hMjkzLWQwNmJkNjk0MjRiNSIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6IlN0dWRlbnQiLCJleHAiOjE3MTgwNDEzMDcsImlzcyI6Imh0dHBzOi8vbG9jYWxob3N0OjcwMTQiLCJhdWQiOiJVc2VyIn0.hqMO8S4yUc1s5sNiCSRToBMw-yMW8h5Z_TiO5j8KUg0z-P4CuSTVVK_DAtzPsFob6V1QscP-q7b0gidBvbCGew')
INSERT [dbo].[UserToken] ([UserId], [LoginProvider], [Name], [Value]) VALUES (N'd34c6059-4700-49fd-b1fa-6227e2be5470', N'ODT_Api', N'AccessToken', N'eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9lbWFpbGFkZHJlc3MiOiJUcmFuVmFuQkBnbWFpbC5jb20iLCJqdGkiOiIwOGM4ZDdjNy04ZTRmLTQ1N2ItYTZlNS0wNGM1YTAxZWM3ZGIiLCJJZCI6ImQzNGM2MDU5LTQ3MDAtNDlmZC1iMWZhLTYyMjdlMmJlNTQ3MCIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6IlR1dG9yIiwiZXhwIjoxNzE4MTg5NTI3LCJpc3MiOiJodHRwczovL2xvY2FsaG9zdDo3MDE0IiwiYXVkIjoiVXNlciJ9.XmTunQmtyc3QCtyaeFV5cRDNAssrRKpd6FYXksF2fq4n19DCYn6jf6NzXeb5LnMd0UOEQdxm9NIsOFbO4tjgtQ')
INSERT [dbo].[UserToken] ([UserId], [LoginProvider], [Name], [Value]) VALUES (N'ec446432-53f8-4e5f-84b2-8a33162970f3', N'ODT_Api', N'AccessToken', N'eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9lbWFpbGFkZHJlc3MiOiJtb2RlcmF0b3JAZ21haWwuY29tIiwianRpIjoiOGJkZjlkYTItNTIxYy00YmRkLTgzNTUtMDkxOWZhNWE0OTlmIiwiSWQiOiJlYzQ0NjQzMi01M2Y4LTRlNWYtODRiMi04YTMzMTYyOTcwZjMiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJNb2RlcmF0b3IiLCJleHAiOjE3MTkzMzgyODUsImlzcyI6Imh0dHBzOi8vbG9jYWxob3N0OjcyNTkiLCJhdWQiOiJVc2VyIn0.-74t9mcUZ5b5zOLcHJf3IA6tBp7s197ZRGiqxHgEtfXIEi5eXFntSaQJI6ijO0exbfvrMdLMuFSZSKx1_Y_aCw')
GO
ALTER TABLE [dbo].[CoinManagement] ADD  DEFAULT (CONVERT([bigint],(0))) FOR [TransactionId]
GO
ALTER TABLE [dbo].[SubjectLevel] ADD  DEFAULT ((0)) FOR [LimitMember]
GO
ALTER TABLE [dbo].[SubjectLevel] ADD  DEFAULT (N'') FOR [Name]
GO
ALTER TABLE [dbo].[SubjectLevel] ADD  DEFAULT (N'') FOR [Image]
GO
ALTER TABLE [dbo].[User] ADD  DEFAULT (CONVERT([bit],(0))) FOR [IsLocked]
GO
ALTER TABLE [dbo].[CoinManagement]  WITH CHECK ADD  CONSTRAINT [FK_CoinManagement_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[CoinManagement] CHECK CONSTRAINT [FK_CoinManagement_User]
GO
ALTER TABLE [dbo].[Rating]  WITH CHECK ADD  CONSTRAINT [FK_Rating_Tutor] FOREIGN KEY([TutorId])
REFERENCES [dbo].[Tutor] ([Id])
GO
ALTER TABLE [dbo].[Rating] CHECK CONSTRAINT [FK_Rating_Tutor]
GO
ALTER TABLE [dbo].[Rating]  WITH CHECK ADD  CONSTRAINT [FK_Rating_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Rating] CHECK CONSTRAINT [FK_Rating_User]
GO
ALTER TABLE [dbo].[RefreshToken]  WITH CHECK ADD  CONSTRAINT [FK_RefreshToken_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[RefreshToken] CHECK CONSTRAINT [FK_RefreshToken_User]
GO
ALTER TABLE [dbo].[Request]  WITH CHECK ADD  CONSTRAINT [FK_Request_RequestCategory] FOREIGN KEY([RequestCategoryId])
REFERENCES [dbo].[RequestCategory] ([Id])
GO
ALTER TABLE [dbo].[Request] CHECK CONSTRAINT [FK_Request_RequestCategory]
GO
ALTER TABLE [dbo].[Request]  WITH CHECK ADD  CONSTRAINT [FK_Request_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Request] CHECK CONSTRAINT [FK_Request_User]
GO
ALTER TABLE [dbo].[Response]  WITH CHECK ADD  CONSTRAINT [FK_Response_Request] FOREIGN KEY([RequestId])
REFERENCES [dbo].[Request] ([Id])
GO
ALTER TABLE [dbo].[Response] CHECK CONSTRAINT [FK_Response_Request]
GO
ALTER TABLE [dbo].[RoleClaim]  WITH CHECK ADD  CONSTRAINT [FK_RoleClaim_Role_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Role] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[RoleClaim] CHECK CONSTRAINT [FK_RoleClaim_Role_RoleId]
GO
ALTER TABLE [dbo].[StudentJoin]  WITH CHECK ADD  CONSTRAINT [FK_StudentJoin_SubjectLevel] FOREIGN KEY([SubjectLevelId])
REFERENCES [dbo].[SubjectLevel] ([Id])
GO
ALTER TABLE [dbo].[StudentJoin] CHECK CONSTRAINT [FK_StudentJoin_SubjectLevel]
GO
ALTER TABLE [dbo].[StudentJoin]  WITH CHECK ADD  CONSTRAINT [FK_StudentJoin_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[StudentJoin] CHECK CONSTRAINT [FK_StudentJoin_User]
GO
ALTER TABLE [dbo].[SubjectLevel]  WITH CHECK ADD  CONSTRAINT [FK_SubjectLevel_Level] FOREIGN KEY([LevelId])
REFERENCES [dbo].[Level] ([Id])
GO
ALTER TABLE [dbo].[SubjectLevel] CHECK CONSTRAINT [FK_SubjectLevel_Level]
GO
ALTER TABLE [dbo].[SubjectLevel]  WITH CHECK ADD  CONSTRAINT [FK_SubjectLevel_Subject] FOREIGN KEY([SubjectId])
REFERENCES [dbo].[Subject] ([Id])
GO
ALTER TABLE [dbo].[SubjectLevel] CHECK CONSTRAINT [FK_SubjectLevel_Subject]
GO
ALTER TABLE [dbo].[SubjectLevel]  WITH CHECK ADD  CONSTRAINT [FK_SubjectLevel_Tutor] FOREIGN KEY([TutorId])
REFERENCES [dbo].[Tutor] ([Id])
GO
ALTER TABLE [dbo].[SubjectLevel] CHECK CONSTRAINT [FK_SubjectLevel_Tutor]
GO
ALTER TABLE [dbo].[Time]  WITH CHECK ADD  CONSTRAINT [FK_Time_SubjectLevel] FOREIGN KEY([SubjectLevelId])
REFERENCES [dbo].[SubjectLevel] ([Id])
GO
ALTER TABLE [dbo].[Time] CHECK CONSTRAINT [FK_Time_SubjectLevel]
GO
ALTER TABLE [dbo].[Tutor]  WITH CHECK ADD  CONSTRAINT [FK_Tutor_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Tutor] CHECK CONSTRAINT [FK_Tutor_User]
GO
ALTER TABLE [dbo].[UserClaim]  WITH CHECK ADD  CONSTRAINT [FK_UserClaim_User_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserClaim] CHECK CONSTRAINT [FK_UserClaim_User_UserId]
GO
ALTER TABLE [dbo].[UserLogin]  WITH CHECK ADD  CONSTRAINT [FK_UserLogin_User_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserLogin] CHECK CONSTRAINT [FK_UserLogin_User_UserId]
GO
ALTER TABLE [dbo].[UserRoles]  WITH CHECK ADD  CONSTRAINT [FK_UserRoles_Role_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Role] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserRoles] CHECK CONSTRAINT [FK_UserRoles_Role_RoleId]
GO
ALTER TABLE [dbo].[UserRoles]  WITH CHECK ADD  CONSTRAINT [FK_UserRoles_User_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserRoles] CHECK CONSTRAINT [FK_UserRoles_User_UserId]
GO
ALTER TABLE [dbo].[UserToken]  WITH CHECK ADD  CONSTRAINT [FK_UserToken_User_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserToken] CHECK CONSTRAINT [FK_UserToken_User_UserId]
GO
