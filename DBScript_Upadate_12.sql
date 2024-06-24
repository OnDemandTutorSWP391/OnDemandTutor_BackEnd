USE [OnDemandTutor]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 6/24/2024 5:28:10 PM ******/
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
/****** Object:  Table [dbo].[CoinManagement]    Script Date: 6/24/2024 5:28:10 PM ******/
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
/****** Object:  Table [dbo].[Level]    Script Date: 6/24/2024 5:28:10 PM ******/
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
/****** Object:  Table [dbo].[Rating]    Script Date: 6/24/2024 5:28:10 PM ******/
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
/****** Object:  Table [dbo].[RefreshToken]    Script Date: 6/24/2024 5:28:10 PM ******/
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
/****** Object:  Table [dbo].[Request]    Script Date: 6/24/2024 5:28:10 PM ******/
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
/****** Object:  Table [dbo].[RequestCategory]    Script Date: 6/24/2024 5:28:10 PM ******/
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
/****** Object:  Table [dbo].[Response]    Script Date: 6/24/2024 5:28:10 PM ******/
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
/****** Object:  Table [dbo].[Role]    Script Date: 6/24/2024 5:28:10 PM ******/
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
/****** Object:  Table [dbo].[RoleClaim]    Script Date: 6/24/2024 5:28:10 PM ******/
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
/****** Object:  Table [dbo].[StudentJoin]    Script Date: 6/24/2024 5:28:10 PM ******/
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
/****** Object:  Table [dbo].[Subject]    Script Date: 6/24/2024 5:28:10 PM ******/
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
/****** Object:  Table [dbo].[SubjectLevel]    Script Date: 6/24/2024 5:28:10 PM ******/
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
 CONSTRAINT [PK_SubjectLevel] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Time]    Script Date: 6/24/2024 5:28:10 PM ******/
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
/****** Object:  Table [dbo].[Tutor]    Script Date: 6/24/2024 5:28:10 PM ******/
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
/****** Object:  Table [dbo].[User]    Script Date: 6/24/2024 5:28:10 PM ******/
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
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserClaim]    Script Date: 6/24/2024 5:28:10 PM ******/
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
/****** Object:  Table [dbo].[UserLogin]    Script Date: 6/24/2024 5:28:10 PM ******/
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
/****** Object:  Table [dbo].[UserRoles]    Script Date: 6/24/2024 5:28:10 PM ******/
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
/****** Object:  Table [dbo].[UserToken]    Script Date: 6/24/2024 5:28:10 PM ******/
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
SET IDENTITY_INSERT [dbo].[Rating] OFF
GO
INSERT [dbo].[RefreshToken] ([Id], [UserId], [JwtId], [Token], [IsUsed], [IsRevoked], [IssuedAt], [ExpiredAt]) VALUES (N'1201f8c7-90b7-401b-b49a-63dee0c4c8cc', N'b75dc8dc-3c0b-4ffe-8d03-f23d5df0b6e3', N'0a3a0637-181e-4c53-b87e-96587fbafc9b', N'UYql4QiOX9QY+/ODlMMzVI5SBz2bV1qBglNA81crcGs=', 0, 0, CAST(N'2024-06-11T23:50:18.9463577' AS DateTime2), CAST(N'2024-06-12T00:50:18.9464271' AS DateTime2))
INSERT [dbo].[RefreshToken] ([Id], [UserId], [JwtId], [Token], [IsUsed], [IsRevoked], [IssuedAt], [ExpiredAt]) VALUES (N'2be7d04d-1c25-4985-bb0c-296e2ca98363', N'bf580a91-67f2-416f-a293-d06bd69424b5', N'695d0206-e35f-4e50-81d2-0b5111f17071', N'047Ox4nY/58MOHWEmvgO5qk8leE1IqvEfAT+FJBjEiI=', 0, 0, CAST(N'2024-06-11T00:21:47.9683602' AS DateTime2), CAST(N'2024-06-11T01:21:47.9684730' AS DateTime2))
INSERT [dbo].[RefreshToken] ([Id], [UserId], [JwtId], [Token], [IsUsed], [IsRevoked], [IssuedAt], [ExpiredAt]) VALUES (N'65ed51d7-3268-4f3a-a9a9-ff8d0fc1a2ce', N'd34c6059-4700-49fd-b1fa-6227e2be5470', N'08c8d7c7-8e4f-457b-a6e5-04c5a01ec7db', N'63KX1AXKJHCh4K7E00FEuyWGjkZcFvfCEXntMeEqpBA=', 0, 0, CAST(N'2024-06-12T17:32:07.9766130' AS DateTime2), CAST(N'2024-06-12T18:32:07.9769329' AS DateTime2))
INSERT [dbo].[RefreshToken] ([Id], [UserId], [JwtId], [Token], [IsUsed], [IsRevoked], [IssuedAt], [ExpiredAt]) VALUES (N'92c9b659-134a-46c5-b311-504655747926', N'5f5b0490-f22b-44d7-a0ee-4331fc69cb85', N'191544be-4a4b-4b58-a10d-71934d6e3cd5', N'UxJcwWXlUcmczHfgHPZtJYILZT8+q3eiSPmCaUATWRc=', 0, 0, CAST(N'2024-06-24T17:20:34.3432982' AS DateTime2), CAST(N'2024-06-24T18:20:34.3433498' AS DateTime2))
INSERT [dbo].[RefreshToken] ([Id], [UserId], [JwtId], [Token], [IsUsed], [IsRevoked], [IssuedAt], [ExpiredAt]) VALUES (N'94fd1cc3-9ceb-4cce-a24e-c79091e1f859', N'0c401c73-f733-423a-9e49-839b4ba3241c', N'd47cef18-5f43-457d-99f2-6b82ce6b16e7', N'wgdG3U/Rc+5uqhEmAdTBGi4trvEgOEEoCxwzHOUQdp0=', 0, 0, CAST(N'2024-06-11T02:38:04.5881997' AS DateTime2), CAST(N'2024-06-11T03:38:04.5882209' AS DateTime2))
INSERT [dbo].[RefreshToken] ([Id], [UserId], [JwtId], [Token], [IsUsed], [IsRevoked], [IssuedAt], [ExpiredAt]) VALUES (N'9fd35327-d381-4b5c-8315-b93a98714d54', N'ec446432-53f8-4e5f-84b2-8a33162970f3', N'a688b17a-889c-4213-aa4e-464a261e9797', N'UFFqaJX8DdYsD9kFg11LMObf7lo1etGW54rMPbXSAKI=', 0, 0, CAST(N'2024-06-24T13:54:37.9978770' AS DateTime2), CAST(N'2024-06-24T14:54:37.9979801' AS DateTime2))
INSERT [dbo].[RefreshToken] ([Id], [UserId], [JwtId], [Token], [IsUsed], [IsRevoked], [IssuedAt], [ExpiredAt]) VALUES (N'a6ebfa9b-d851-42bc-8bf1-0a0fefb17b64', N'886152d0-1aa7-42ba-86d1-3a0a0e423105', N'cd0711c0-e62b-4bd3-9459-41c06f1eb916', N'Ae73IcG3cRMcilJVDV6JSJU1N4czYkD2VdFDKblTlZI=', 0, 0, CAST(N'2024-06-23T00:41:03.4311008' AS DateTime2), CAST(N'2024-06-23T01:41:03.4311862' AS DateTime2))
INSERT [dbo].[RefreshToken] ([Id], [UserId], [JwtId], [Token], [IsUsed], [IsRevoked], [IssuedAt], [ExpiredAt]) VALUES (N'bd2a4092-57ab-4f47-b77c-0e36265f0bde', N'3ea23fe3-013b-4f30-812f-5517ec093aaf', N'3e11bbea-41b8-4618-b82e-11e26503e7d5', N'IK5fnkJZbYSJBV1hUGb9n/IhZnMNNs2c376oXsuWyZg=', 0, 0, CAST(N'2024-06-13T14:58:52.9259138' AS DateTime2), CAST(N'2024-06-13T15:58:52.9259158' AS DateTime2))
GO
SET IDENTITY_INSERT [dbo].[Request] ON 

INSERT [dbo].[Request] ([Id], [RequestCategoryId], [UserId], [Description], [CreatedDate], [Status]) VALUES (1, 2, N'b75dc8dc-3c0b-4ffe-8d03-f23d5df0b6e3', N'Yêu cầu phê duyệt dịch vụ giảng viên với TutorId: 1.', CAST(N'2024-06-10T02:22:57.0187204' AS DateTime2), N'Đã xong')
INSERT [dbo].[Request] ([Id], [RequestCategoryId], [UserId], [Description], [CreatedDate], [Status]) VALUES (2, 2, N'886152d0-1aa7-42ba-86d1-3a0a0e423105', N'Yêu cầu phê duyệt dịch vụ giảng viên với TutorId: 4.', CAST(N'2024-06-10T18:30:24.9752265' AS DateTime2), N'Đã xong')
INSERT [dbo].[Request] ([Id], [RequestCategoryId], [UserId], [Description], [CreatedDate], [Status]) VALUES (3, 1, N'd34c6059-4700-49fd-b1fa-6227e2be5470', N'Acc của t có vấn đề rồi', CAST(N'2024-06-12T17:32:53.1628704' AS DateTime2), N'Chờ xử lý')
INSERT [dbo].[Request] ([Id], [RequestCategoryId], [UserId], [Description], [CreatedDate], [Status]) VALUES (4, 2, N'd34c6059-4700-49fd-b1fa-6227e2be5470', N'Cho xin dạy được không', CAST(N'2024-06-12T17:33:51.5010522' AS DateTime2), N'Chờ xử lý')
INSERT [dbo].[Request] ([Id], [RequestCategoryId], [UserId], [Description], [CreatedDate], [Status]) VALUES (5, 2, N'd34c6059-4700-49fd-b1fa-6227e2be5470', N'Yêu cầu phê duyệt dịch vụ giảng viên với TutorId: 3.', CAST(N'2024-06-12T17:34:39.6425687' AS DateTime2), N'Chờ xử lý')
INSERT [dbo].[Request] ([Id], [RequestCategoryId], [UserId], [Description], [CreatedDate], [Status]) VALUES (6, 2, N'd34c6059-4700-49fd-b1fa-6227e2be5470', N'Yêu cầu phê duyệt dịch vụ giảng viên với TutorId: 3.', CAST(N'2024-06-12T17:36:56.6786184' AS DateTime2), N'Chờ xử lý')
SET IDENTITY_INSERT [dbo].[Request] OFF
GO
SET IDENTITY_INSERT [dbo].[RequestCategory] ON 

INSERT [dbo].[RequestCategory] ([Id], [CategoryName]) VALUES (1, N'Account Support')
INSERT [dbo].[RequestCategory] ([Id], [CategoryName]) VALUES (2, N'Rental Service Support')
INSERT [dbo].[RequestCategory] ([Id], [CategoryName]) VALUES (3, N'Payment Support')
SET IDENTITY_INSERT [dbo].[RequestCategory] OFF
GO
SET IDENTITY_INSERT [dbo].[Response] ON 

INSERT [dbo].[Response] ([Id], [RequestId], [Description], [ResponseDate]) VALUES (1, 1, N'Thư phản hồi yêu cầu phê duyệt giảng viên', CAST(N'2024-06-10T02:25:28.9319419' AS DateTime2))
INSERT [dbo].[Response] ([Id], [RequestId], [Description], [ResponseDate]) VALUES (2, 2, N'Thư phê duyệt yêu cầu đăng kí giảng dạy', CAST(N'2024-06-10T18:32:57.3376901' AS DateTime2))
SET IDENTITY_INSERT [dbo].[Response] OFF
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
INSERT [dbo].[StudentJoin] ([Id], [UserId], [SubjectLevelId]) VALUES (9, N'5f5b0490-f22b-44d7-a0ee-4331fc69cb85', 10)
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

INSERT [dbo].[SubjectLevel] ([Id], [LevelId], [SubjectId], [TutorId], [Description], [Url], [Coin], [LimitMember], [Name]) VALUES (9, 3, 1, 4, N'string', N'string', 300, 3, N'')
INSERT [dbo].[SubjectLevel] ([Id], [LevelId], [SubjectId], [TutorId], [Description], [Url], [Coin], [LimitMember], [Name]) VALUES (10, 2, 2, 4, N'vua lý', N'string lý', 200, 2, N'')
INSERT [dbo].[SubjectLevel] ([Id], [LevelId], [SubjectId], [TutorId], [Description], [Url], [Coin], [LimitMember], [Name]) VALUES (11, 1, 3, 4, N'hóa chất', N'string hóa', 500, 1, N'')
INSERT [dbo].[SubjectLevel] ([Id], [LevelId], [SubjectId], [TutorId], [Description], [Url], [Coin], [LimitMember], [Name]) VALUES (12, 3, 4, 4, N'string', N'stringurl', 500, 4, N'')
INSERT [dbo].[SubjectLevel] ([Id], [LevelId], [SubjectId], [TutorId], [Description], [Url], [Coin], [LimitMember], [Name]) VALUES (14, 2, 2, 1, N'Ngu lý', N'string', 500, 5, N'')
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
INSERT [dbo].[Tutor] ([Id], [AcademicLevel], [WorkPlace], [OnlineStatus], [AverageStar], [Degree], [CreditCard], [UserId], [Status], [TutorServiceName], [TutorServiceDescription], [TutorServiceVideo], [LearningMaterialDemo]) VALUES (4, N'Uni', N'FUni', NULL, 0, N'Engineer', N'111111', N'886152d0-1aa7-42ba-86d1-3a0a0e423105', N'Chấp thuận', N'code gym', N'vua code', N'code video', N'tutorial code')
SET IDENTITY_INSERT [dbo].[Tutor] OFF
GO
INSERT [dbo].[User] ([Id], [FullName], [IdentityCard], [Dob], [CreatedDate], [Status], [Gender], [Avatar], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'0c401c73-f733-423a-9e49-839b4ba3241c', N'ADMIN', N'admin', CAST(N'2024-06-09T00:00:00.0000000' AS DateTime2), CAST(N'2024-06-10T01:45:21.1157674' AS DateTime2), 0, N'male', N'admin dep trai', N'minhtam14231204@gmail.com', N'MINHTAM14231204@GMAIL.COM', N'minhtam14231204@gmail.com', N'MINHTAM14231204@GMAIL.COM', 0, N'AQAAAAEAACcQAAAAEJtU8DpOEUJkept/y4CcWtH1hDR37Oys8Ro2O+eiqNxRuyX8gteZRtC8DySAaRIDWA==', N'FQVNJZXRFZQFYPVNSI5WXLF72MCSFCEH', N'dd404895-3323-404c-bdc6-4dbd09a4c189', N'0942775673', 0, 0, NULL, 1, 0)
INSERT [dbo].[User] ([Id], [FullName], [IdentityCard], [Dob], [CreatedDate], [Status], [Gender], [Avatar], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'3ea23fe3-013b-4f30-812f-5517ec093aaf', N'NguyenVanA', N'anhA', CAST(N'2024-06-04T00:00:00.0000000' AS DateTime2), CAST(N'2024-06-10T01:47:40.1206328' AS DateTime2), 0, N'male', N'ava1', N'nguyenvana@gmail.com', N'NGUYENVANA@GMAIL.COM', N'nguyenvana@gmail.com', N'NGUYENVANA@GMAIL.COM', 0, N'AQAAAAEAACcQAAAAEM0msXFXLmObERxqk83W8Aps30KVgWL6tNdArjcEkQthEd2XUD+4qhp17+Gxq1z92g==', N'UVSRFGWCBXORGQ4YKSSSBSLJ5TQ3US55', N'1c3902d2-6b33-43d9-94fb-9eabb5efd942', N'0123456789', 0, 0, NULL, 1, 0)
INSERT [dbo].[User] ([Id], [FullName], [IdentityCard], [Dob], [CreatedDate], [Status], [Gender], [Avatar], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'5f5b0490-f22b-44d7-a0ee-4331fc69cb85', N'TamNguyenDev', N'card1', CAST(N'2004-12-23T00:00:00.0000000' AS DateTime2), CAST(N'2024-06-10T01:55:54.4413485' AS DateTime2), 0, N'male', N'anh tam dep trai', N'th3m1nd05@gmail.com', N'TH3M1ND05@GMAIL.COM', N'th3m1nd05@gmail.com', N'TH3M1ND05@GMAIL.COM', 0, N'AQAAAAEAACcQAAAAEKUZm3fylwHVrQInOkfcuTKzlsgNL1w6nCF5xEve1q/9QWjUl+MgUxHQDxsBEy/fYA==', N'DRTEYUKNIEIR55DCY76J3LYCLJLAFWMQ', N'880b36c6-3398-4a62-bfe0-54e3986bf4fa', N'0942775673', 0, 0, NULL, 1, 0)
INSERT [dbo].[User] ([Id], [FullName], [IdentityCard], [Dob], [CreatedDate], [Status], [Gender], [Avatar], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'886152d0-1aa7-42ba-86d1-3a0a0e423105', N'TeacherTam', N'teacher tam', CAST(N'2024-06-10T00:00:00.0000000' AS DateTime2), CAST(N'2024-06-10T18:28:00.3085314' AS DateTime2), 0, N'male', N'tam dep trai', N'tamnguyen.231204@gmail.com', N'TAMNGUYEN.231204@GMAIL.COM', N'tamnguyen.231204@gmail.com', N'TAMNGUYEN.231204@GMAIL.COM', 0, N'AQAAAAEAACcQAAAAEMirmFwLD3zZzdU5FZ09glhCJyjaFYhmZrRwmEZqmt0bgKhhuPYEkbZrgrUKoJ0ZWg==', N'MCEDTTLHFEIRZTOUFTYTLQQ3NVF5RN62', N'24d790ce-000f-4f38-9be9-5f1bca5ee282', N'0708483243', 0, 0, NULL, 1, 0)
INSERT [dbo].[User] ([Id], [FullName], [IdentityCard], [Dob], [CreatedDate], [Status], [Gender], [Avatar], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'b75dc8dc-3c0b-4ffe-8d03-f23d5df0b6e3', N'NguyenVanC', N'anhC', CAST(N'2024-06-05T00:00:00.0000000' AS DateTime2), CAST(N'2024-06-10T01:23:01.4168100' AS DateTime2), 0, N'male', N'Cdeptrai', N'NguyenVanC@gmail.com', N'NGUYENVANC@GMAIL.COM', N'NguyenVanC@gmail.com', N'NGUYENVANC@GMAIL.COM', 0, N'AQAAAAEAACcQAAAAEFyD8YaBkf1VIJ9pPkBAKC6WbYCmwz9lFc8s+lYtDtF5+tNHtMleUAx5MCsADBYdUw==', N'WVS4QJAQH2WWB4F6OKC6YOV7WQTPQR4D', N'5d7613e0-a5d7-442b-82d5-09fe69721d01', N'0802748371', 0, 0, NULL, 1, 0)
INSERT [dbo].[User] ([Id], [FullName], [IdentityCard], [Dob], [CreatedDate], [Status], [Gender], [Avatar], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'bf580a91-67f2-416f-a293-d06bd69424b5', N'TamNguyenTrader', N'tamnguyen', CAST(N'2024-06-10T00:00:00.0000000' AS DateTime2), CAST(N'2024-06-11T00:20:49.8335026' AS DateTime2), 0, N'male', N'string', N'tamnguyenbo231204@gmail.com', N'TAMNGUYENBO231204@GMAIL.COM', N'tamnguyenbo231204@gmail.com', N'TAMNGUYENBO231204@GMAIL.COM', 0, N'AQAAAAEAACcQAAAAEKr3LNcR5UvImVNiqNzJzmpGbLM9UMftcJJNbEN9UTJoC+NbRKnc+bKs3bUtVXKuHQ==', N'UP7GO6KY4LDL75ECZVMK3DMKFY2EWQQB', N'a2eea6a7-d85d-4dff-986c-eaaaa36d679e', N'0912387465', 0, 0, NULL, 1, 0)
INSERT [dbo].[User] ([Id], [FullName], [IdentityCard], [Dob], [CreatedDate], [Status], [Gender], [Avatar], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'd34c6059-4700-49fd-b1fa-6227e2be5470', N'TranVanB', N'cardiB', CAST(N'2024-05-29T00:00:00.0000000' AS DateTime2), CAST(N'2024-06-10T01:48:41.8839045' AS DateTime2), 0, N'male', N'avaB', N'TranVanB@gmail.com', N'TRANVANB@GMAIL.COM', N'TranVanB@gmail.com', N'TRANVANB@GMAIL.COM', 0, N'AQAAAAEAACcQAAAAEITF+U6oGjGh2a4+Yay2rxA1gIWg++QkeN1K+I3YZgZlGWkhWjRB4AUQEIz3XShkhw==', N'4BJLMVCTF3A44ZVG7ZWC2P7T4QYV5IO7', N'87eb6c73-5b3c-466a-8e6f-914764499875', N'0123456789', 0, 0, NULL, 1, 0)
INSERT [dbo].[User] ([Id], [FullName], [IdentityCard], [Dob], [CreatedDate], [Status], [Gender], [Avatar], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'ec446432-53f8-4e5f-84b2-8a33162970f3', N'Moderator', N'mod', CAST(N'2024-06-09T00:00:00.0000000' AS DateTime2), CAST(N'2024-06-10T01:50:16.5616178' AS DateTime2), 0, N'male', N'mod xinh gai', N'moderator@gmail.com', N'MODERATOR@GMAIL.COM', N'moderator@gmail.com', N'MODERATOR@GMAIL.COM', 0, N'AQAAAAEAACcQAAAAEBUKwH55etmPtdjOt1PeoGaXCfK20SfodLd0ABImNbdtOoHMC+p4+170wqDlBE9vRQ==', N'H7MMR4ROKWDF4SMTEAPJJID5FGWGFQP6', N'6db59edb-8523-4355-8ab8-4dced4efaad1', N'0892736481', 0, 0, NULL, 1, 0)
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
INSERT [dbo].[UserToken] ([UserId], [LoginProvider], [Name], [Value]) VALUES (N'0c401c73-f733-423a-9e49-839b4ba3241c', N'ODT_Api', N'AccessToken', N'eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9lbWFpbGFkZHJlc3MiOiJtaW5odGFtMTQyMzEyMDRAZ21haWwuY29tIiwianRpIjoiZDQ3Y2VmMTgtNWY0My00NTdkLTk5ZjItNmI4MmNlNmIxNmU3IiwiSWQiOiIwYzQwMWM3My1mNzMzLTQyM2EtOWU0OS04MzliNGJhMzI0MWMiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJBZG1pbiIsImV4cCI6MTcxODA0OTQ4NCwiaXNzIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NzAxNCIsImF1ZCI6IlVzZXIifQ.sei5J9Y3PGzXjrFoPpu6ux3GWD0pzsA4K_DVDQYHQhZRYgxtz_4a2GHHpj55kClzLGDUSEw94EFiuyLNkIBwZw')
INSERT [dbo].[UserToken] ([UserId], [LoginProvider], [Name], [Value]) VALUES (N'3ea23fe3-013b-4f30-812f-5517ec093aaf', N'ODT_Api', N'AccessToken', N'eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9lbWFpbGFkZHJlc3MiOiJuZ3V5ZW52YW5hQGdtYWlsLmNvbSIsImp0aSI6IjNlMTFiYmVhLTQxYjgtNDYxOC1iODJlLTExZTI2NTAzZTdkNSIsIklkIjoiM2VhMjNmZTMtMDEzYi00ZjMwLTgxMmYtNTUxN2VjMDkzYWFmIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoiU3R1ZGVudCIsImV4cCI6MTcxODI2NjczMiwiaXNzIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NzAxNCIsImF1ZCI6IlVzZXIifQ.ffMP1HwZlsFaseQTSGJcPfexVhuApAsiDpAeAsQINMzfJNgc2NGS2hEJGEqIn-NRQDu2Ayg4PPFNjRrMajb0tw')
INSERT [dbo].[UserToken] ([UserId], [LoginProvider], [Name], [Value]) VALUES (N'5f5b0490-f22b-44d7-a0ee-4331fc69cb85', N'ODT_Api', N'AccessToken', N'eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9lbWFpbGFkZHJlc3MiOiJ0aDNtMW5kMDVAZ21haWwuY29tIiwianRpIjoiMTkxNTQ0YmUtNGE0Yi00YjU4LWExMGQtNzE5MzRkNmUzY2Q1IiwiSWQiOiI1ZjViMDQ5MC1mMjJiLTQ0ZDctYTBlZS00MzMxZmM2OWNiODUiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJTdHVkZW50IiwiZXhwIjoxNzE5MjI1NjMzLCJpc3MiOiJodHRwczovL2xvY2FsaG9zdDo3MjU5IiwiYXVkIjoiVXNlciJ9.-83cyHpZpbbkMLFBcSke3e23JZpgmVkvGoZmK9hcWPp-OaQDE4JuaqnEPcladyjma9gu38QRwo9xWK8qixYfLg')
INSERT [dbo].[UserToken] ([UserId], [LoginProvider], [Name], [Value]) VALUES (N'886152d0-1aa7-42ba-86d1-3a0a0e423105', N'ODT_Api', N'AccessToken', N'eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9lbWFpbGFkZHJlc3MiOiJ0YW1uZ3V5ZW4uMjMxMjA0QGdtYWlsLmNvbSIsImp0aSI6ImNkMDcxMWMwLWU2MmItNGJkMy05NDU5LTQxYzA2ZjFlYjkxNiIsIklkIjoiODg2MTUyZDAtMWFhNy00MmJhLTg2ZDEtM2EwYTBlNDIzMTA1IiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoiVHV0b3IiLCJleHAiOjE3MTkwNzkyNjIsImlzcyI6Imh0dHBzOi8vbG9jYWxob3N0OjcwMTQiLCJhdWQiOiJVc2VyIn0.wFc8g9odV7elQjlVxQFY-_M6lExEMPQ_IbeKyFFG-cIIcHT3oOG0GVdvivmlYKgSt_FSPUpWgWIZV5Ha3lqKGg')
INSERT [dbo].[UserToken] ([UserId], [LoginProvider], [Name], [Value]) VALUES (N'b75dc8dc-3c0b-4ffe-8d03-f23d5df0b6e3', N'ODT_Api', N'AccessToken', N'eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9lbWFpbGFkZHJlc3MiOiJOZ3V5ZW5WYW5DQGdtYWlsLmNvbSIsImp0aSI6IjBhM2EwNjM3LTE4MWUtNGM1My1iODdlLTk2NTg3ZmJhZmM5YiIsIklkIjoiYjc1ZGM4ZGMtM2MwYi00ZmZlLThkMDMtZjIzZDVkZjBiNmUzIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoiVHV0b3IiLCJleHAiOjE3MTgxMjU4MTgsImlzcyI6Imh0dHBzOi8vbG9jYWxob3N0OjcwMTQiLCJhdWQiOiJVc2VyIn0._RCTDDko2ShfbHINV7LJzNvMcc7WqvIlTS8v01Ptrl0_WZlBvwWt_dId-xaj_xkJy9UAJ1mLL1XxiWPru-8DeQ')
INSERT [dbo].[UserToken] ([UserId], [LoginProvider], [Name], [Value]) VALUES (N'bf580a91-67f2-416f-a293-d06bd69424b5', N'ODT_Api', N'AccessToken', N'eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9lbWFpbGFkZHJlc3MiOiJ0YW1uZ3V5ZW5ibzIzMTIwNEBnbWFpbC5jb20iLCJqdGkiOiI2OTVkMDIwNi1lMzVmLTRlNTAtODFkMi0wYjUxMTFmMTcwNzEiLCJJZCI6ImJmNTgwYTkxLTY3ZjItNDE2Zi1hMjkzLWQwNmJkNjk0MjRiNSIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6IlN0dWRlbnQiLCJleHAiOjE3MTgwNDEzMDcsImlzcyI6Imh0dHBzOi8vbG9jYWxob3N0OjcwMTQiLCJhdWQiOiJVc2VyIn0.hqMO8S4yUc1s5sNiCSRToBMw-yMW8h5Z_TiO5j8KUg0z-P4CuSTVVK_DAtzPsFob6V1QscP-q7b0gidBvbCGew')
INSERT [dbo].[UserToken] ([UserId], [LoginProvider], [Name], [Value]) VALUES (N'd34c6059-4700-49fd-b1fa-6227e2be5470', N'ODT_Api', N'AccessToken', N'eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9lbWFpbGFkZHJlc3MiOiJUcmFuVmFuQkBnbWFpbC5jb20iLCJqdGkiOiIwOGM4ZDdjNy04ZTRmLTQ1N2ItYTZlNS0wNGM1YTAxZWM3ZGIiLCJJZCI6ImQzNGM2MDU5LTQ3MDAtNDlmZC1iMWZhLTYyMjdlMmJlNTQ3MCIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6IlR1dG9yIiwiZXhwIjoxNzE4MTg5NTI3LCJpc3MiOiJodHRwczovL2xvY2FsaG9zdDo3MDE0IiwiYXVkIjoiVXNlciJ9.XmTunQmtyc3QCtyaeFV5cRDNAssrRKpd6FYXksF2fq4n19DCYn6jf6NzXeb5LnMd0UOEQdxm9NIsOFbO4tjgtQ')
INSERT [dbo].[UserToken] ([UserId], [LoginProvider], [Name], [Value]) VALUES (N'ec446432-53f8-4e5f-84b2-8a33162970f3', N'ODT_Api', N'AccessToken', N'eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9lbWFpbGFkZHJlc3MiOiJtb2RlcmF0b3JAZ21haWwuY29tIiwianRpIjoiYTY4OGIxN2EtODg5Yy00MjEzLWFhNGUtNDY0YTI2MWU5Nzk3IiwiSWQiOiJlYzQ0NjQzMi01M2Y4LTRlNWYtODRiMi04YTMzMTYyOTcwZjMiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJNb2RlcmF0b3IiLCJleHAiOjE3MTkyMTMyNzcsImlzcyI6Imh0dHBzOi8vbG9jYWxob3N0OjcyNTkiLCJhdWQiOiJVc2VyIn0.GkMY5OlUYZ8bTQ_0j2rcdUEI1LAE6f1ekZ5r7UMY_DpvADbAksiswgFgP4okJ3xeUAdPrYLUFt0XM54Kv_5mBA')
GO
ALTER TABLE [dbo].[CoinManagement] ADD  DEFAULT (CONVERT([bigint],(0))) FOR [TransactionId]
GO
ALTER TABLE [dbo].[SubjectLevel] ADD  DEFAULT ((0)) FOR [LimitMember]
GO
ALTER TABLE [dbo].[SubjectLevel] ADD  DEFAULT (N'') FOR [Name]
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
