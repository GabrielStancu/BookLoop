USE [master]
GO
/****** Object:  Database [BookLoopDb]    Script Date: 5/18/2021 7:02:33 PM ******/
CREATE DATABASE [BookLoopDb]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'BookLoopDb', FILENAME = N'D:\Program Files\SQL Server Management Studio 2019\MSSQL15.MSSQLSERVER\MSSQL\DATA\BookLoopDb.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'BookLoopDb_log', FILENAME = N'D:\Program Files\SQL Server Management Studio 2019\MSSQL15.MSSQLSERVER\MSSQL\DATA\BookLoopDb_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [BookLoopDb] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [BookLoopDb].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [BookLoopDb] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [BookLoopDb] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [BookLoopDb] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [BookLoopDb] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [BookLoopDb] SET ARITHABORT OFF 
GO
ALTER DATABASE [BookLoopDb] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [BookLoopDb] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [BookLoopDb] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [BookLoopDb] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [BookLoopDb] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [BookLoopDb] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [BookLoopDb] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [BookLoopDb] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [BookLoopDb] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [BookLoopDb] SET  DISABLE_BROKER 
GO
ALTER DATABASE [BookLoopDb] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [BookLoopDb] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [BookLoopDb] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [BookLoopDb] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [BookLoopDb] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [BookLoopDb] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [BookLoopDb] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [BookLoopDb] SET RECOVERY FULL 
GO
ALTER DATABASE [BookLoopDb] SET  MULTI_USER 
GO
ALTER DATABASE [BookLoopDb] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [BookLoopDb] SET DB_CHAINING OFF 
GO
ALTER DATABASE [BookLoopDb] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [BookLoopDb] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [BookLoopDb] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'BookLoopDb', N'ON'
GO
ALTER DATABASE [BookLoopDb] SET QUERY_STORE = OFF
GO
USE [BookLoopDb]
GO
/****** Object:  Table [dbo].[BookPost]    Script Date: 5/18/2021 7:02:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BookPost](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OfferType] [int] NULL,
	[Title] [nvarchar](30) NULL,
	[Genre] [nvarchar](30) NULL,
	[Author] [nvarchar](30) NULL,
	[Price] [float] NULL,
	[PostOwnerId] [int] NULL,
	[Location] [nvarchar](30) NULL,
	[PhotoFileName] [nvarchar](100) NULL,
	[Description] [text] NULL,
	[TimeOfPosting] [datetime] NULL,
 CONSTRAINT [PK_BookPost] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BookUser]    Script Date: 5/18/2021 7:02:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BookUser](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](20) NULL,
	[Password] [nvarchar](50) NULL,
	[FirstName] [nvarchar](20) NULL,
	[LastName] [nvarchar](20) NULL,
	[Email] [nvarchar](40) NULL,
	[PhotoFileName] [nvarchar](100) NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GroupConversation]    Script Date: 5/18/2021 7:02:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GroupConversation](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](20) NULL,
	[ConversationType] [int] NULL,
 CONSTRAINT [PK_GroupConversation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GroupUser]    Script Date: 5/18/2021 7:02:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GroupUser](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[GroupId] [int] NULL,
	[UserId] [int] NULL,
 CONSTRAINT [PK_GroupUser] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Message]    Script Date: 5/18/2021 7:02:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Message](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MessageContent] [text] NULL,
	[SenderId] [int] NULL,
	[ConversationId] [int] NULL,
	[ConversationType] [int] NULL,
	[SendTime] [datetime] NULL,
 CONSTRAINT [PK_Message] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PrivateConversation]    Script Date: 5/18/2021 7:02:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PrivateConversation](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[User1Id] [int] NULL,
	[User2Id] [int] NULL,
	[ConversationType] [int] NULL,
 CONSTRAINT [PK_PrivateConversation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[BookPost] ON 

INSERT [dbo].[BookPost] ([Id], [OfferType], [Title], [Genre], [Author], [Price], [PostOwnerId], [Location], [PhotoFileName], [Description], [TimeOfPosting]) VALUES (13, 2, N'Dune''s Savior', N'SF', N'Frank Herbert', 0, 1, N'Orastie', N'frank-herbert---seria-dune-2---mantuitorul_dunei---c1.jpg', N'Paul advances in his adventure, now known as Paul Muad''Dib...', CAST(N'2021-05-03T23:15:26.603' AS DateTime))
INSERT [dbo].[BookPost] ([Id], [OfferType], [Title], [Genre], [Author], [Price], [PostOwnerId], [Location], [PhotoFileName], [Description], [TimeOfPosting]) VALUES (14, 1, N'Dune Epilogue', N'SF, Drama', N'Frank Herbert', 15, 1, N'Orastie', N'frank-herbert---seria-dune-6---canonicatul_dunei---c1.jpg', N'The long journey of Paul ends here, together with the whole world built by Frank Herbert for his readers...', CAST(N'2021-05-03T23:16:26.603' AS DateTime))
INSERT [dbo].[BookPost] ([Id], [OfferType], [Title], [Genre], [Author], [Price], [PostOwnerId], [Location], [PhotoFileName], [Description], [TimeOfPosting]) VALUES (22, 1, N'Dune''s Children', N'SF', N'Frank Herbert', 25, 1, N'Orastie', N'frank-herbert---seria-dune-3---copiii_dunei---c1.jpg', N'The next generation appears oon Arakis and it is ready to continue the legacy their parents left to them', CAST(N'2021-05-04T11:16:26.603' AS DateTime))
INSERT [dbo].[BookPost] ([Id], [OfferType], [Title], [Genre], [Author], [Price], [PostOwnerId], [Location], [PhotoFileName], [Description], [TimeOfPosting]) VALUES (23, 3, N'Year 1984', N'Reality', N'George Orwell', 0, 2, N'Simeria', N'descărcare.jpeg', N'A chaotic future, more accurate now than it was when the book was written, or during the year it describes...', CAST(N'2021-05-15T19:34:33.930' AS DateTime))
INSERT [dbo].[BookPost] ([Id], [OfferType], [Title], [Genre], [Author], [Price], [PostOwnerId], [Location], [PhotoFileName], [Description], [TimeOfPosting]) VALUES (24, 1, N'Art Of War', N'History', N'Sun Tzu', 30, 78, N'Timisoara', N'ArtOfWarSunTzu.jpg', N'The great Chinese warmaster describes his tactics and process of thought. Still read today, thousands of years later...', CAST(N'2021-05-15T23:44:00.000' AS DateTime))
INSERT [dbo].[BookPost] ([Id], [OfferType], [Title], [Genre], [Author], [Price], [PostOwnerId], [Location], [PhotoFileName], [Description], [TimeOfPosting]) VALUES (25, 2, N'Assassin''s Creed', N'Fiction', N'Oliver Bowden', 0, 79, N'Deva', N'AssassinsCreedBrotherhood.jpg', N'The game series inspired by this books were awesome, yet this book excells them, must read for anyone who played the game!', CAST(N'2021-05-16T07:40:00.000' AS DateTime))
INSERT [dbo].[BookPost] ([Id], [OfferType], [Title], [Genre], [Author], [Price], [PostOwnerId], [Location], [PhotoFileName], [Description], [TimeOfPosting]) VALUES (26, 3, N'Physics Of The Impossible', N'Physics', N'Michio Kaku', 0, 3, N'Hunedoara', N'PhysiscsOfTheImpossible.jpg', N'Wonderful book, I''ve read it multiple times. Received second copy, donating it now.', CAST(N'2021-05-16T10:34:00.000' AS DateTime))
INSERT [dbo].[BookPost] ([Id], [OfferType], [Title], [Genre], [Author], [Price], [PostOwnerId], [Location], [PhotoFileName], [Description], [TimeOfPosting]) VALUES (28, 1, N'The War Planners', N'War', N'Andrew Watts', 18, 78, N'Timisoara', N'WarPlanners.jpg', N'Big minds revealed during the wars, whose stories were never told. Until this book was written.', CAST(N'2021-05-16T12:40:16.000' AS DateTime))
INSERT [dbo].[BookPost] ([Id], [OfferType], [Title], [Genre], [Author], [Price], [PostOwnerId], [Location], [PhotoFileName], [Description], [TimeOfPosting]) VALUES (29, 1, N'Dune ', N'SF, Adventure', N'Frank Herbert', 20, 1, N'Orastie', N'frank-herbert---seria-dune-1---dune---c1.jpg', N'The Atreides family moves to Arakis and the journey of Paul begins on this desert planet...', CAST(N'2021-05-16T13:14:26.603' AS DateTime))
INSERT [dbo].[BookPost] ([Id], [OfferType], [Title], [Genre], [Author], [Price], [PostOwnerId], [Location], [PhotoFileName], [Description], [TimeOfPosting]) VALUES (30, 2, N'Arthas', N'Fantasy', N'Christie Golden', 21, 79, N'Deva', N'Wow_Arthas.jpg', N'For anyone who played the game and knows the sad story behind this deep character, the book is a must read!', CAST(N'2021-05-16T16:43:00.000' AS DateTime))
INSERT [dbo].[BookPost] ([Id], [OfferType], [Title], [Genre], [Author], [Price], [PostOwnerId], [Location], [PhotoFileName], [Description], [TimeOfPosting]) VALUES (31, 1, N'Origin', N'Mistery', N'Dan Brown', 27, 2, N'Simeria', N'OriginDanBrown.jpg', N'On a trail marked by modern art and enigmatic symbols, Langdon and Vidal uncover clues that ultimately bring them face-to-face with Kirsch''s shocking discovery', CAST(N'2021-05-16T20:04:00.000' AS DateTime))
SET IDENTITY_INSERT [dbo].[BookPost] OFF
GO
SET IDENTITY_INSERT [dbo].[BookUser] ON 

INSERT [dbo].[BookUser] ([Id], [Username], [Password], [FirstName], [LastName], [Email], [PhotoFileName]) VALUES (1, N'GabiS', N'gabi', N'Gabriel', N'Stancu', N'gabriel.stancu07@yahoo.com', N'ProfilGabi.png')
INSERT [dbo].[BookUser] ([Id], [Username], [Password], [FirstName], [LastName], [Email], [PhotoFileName]) VALUES (2, N'OliC', N'oli', N'Oliver', N'Cosma', N'oliver.cosma@gmail.com', N'draw-nice-style-cartoon-caricature-as-a-profile-picture.jpg')
INSERT [dbo].[BookUser] ([Id], [Username], [Password], [FirstName], [LastName], [Email], [PhotoFileName]) VALUES (3, N'BiaV', N'bia', N'Bianca', N'Vulsan', N'biavulsan@yahoo.com', N'ProfilBia.png')
INSERT [dbo].[BookUser] ([Id], [Username], [Password], [FirstName], [LastName], [Email], [PhotoFileName]) VALUES (78, N'AlexS', N'alex', N'Alex', N'Serban', N'alexserban@gmail.com', N'draw-nice-style-cartoon-caricature-as-a-profile-picture.png')
INSERT [dbo].[BookUser] ([Id], [Username], [Password], [FirstName], [LastName], [Email], [PhotoFileName]) VALUES (79, N'VictorD', N'victor', N'Victor', N'Domsa', N'victordomsa@yahoo.com', N'draw-big-head-cartoon-caricature-in-24-hours.jpg')
SET IDENTITY_INSERT [dbo].[BookUser] OFF
GO
SET IDENTITY_INSERT [dbo].[GroupConversation] ON 

INSERT [dbo].[GroupConversation] ([Id], [Name], [ConversationType]) VALUES (1, N'The OG Group', 1)
SET IDENTITY_INSERT [dbo].[GroupConversation] OFF
GO
SET IDENTITY_INSERT [dbo].[GroupUser] ON 

INSERT [dbo].[GroupUser] ([Id], [GroupId], [UserId]) VALUES (1, 1, 1)
INSERT [dbo].[GroupUser] ([Id], [GroupId], [UserId]) VALUES (2, 1, 2)
INSERT [dbo].[GroupUser] ([Id], [GroupId], [UserId]) VALUES (3, 1, 3)
SET IDENTITY_INSERT [dbo].[GroupUser] OFF
GO
SET IDENTITY_INSERT [dbo].[Message] ON 

INSERT [dbo].[Message] ([Id], [MessageContent], [SenderId], [ConversationId], [ConversationType], [SendTime]) VALUES (644, N'Hello guys! ', 2, 1, 1, CAST(N'2021-05-18T18:50:42.233' AS DateTime))
INSERT [dbo].[Message] ([Id], [MessageContent], [SenderId], [ConversationId], [ConversationType], [SendTime]) VALUES (645, N'Hi there! ', 1, 1, 1, CAST(N'2021-05-18T18:51:24.860' AS DateTime))
INSERT [dbo].[Message] ([Id], [MessageContent], [SenderId], [ConversationId], [ConversationType], [SendTime]) VALUES (646, N'Hello! ', 3, 1, 1, CAST(N'2021-05-18T18:52:18.103' AS DateTime))
INSERT [dbo].[Message] ([Id], [MessageContent], [SenderId], [ConversationId], [ConversationType], [SendTime]) VALUES (647, N'What''s up?', 3, 1, 1, CAST(N'2021-05-18T18:52:28.053' AS DateTime))
INSERT [dbo].[Message] ([Id], [MessageContent], [SenderId], [ConversationId], [ConversationType], [SendTime]) VALUES (648, N'Was thinking to see each other at a coffee and discuss about recently read books?', 1, 1, 1, CAST(N'2021-05-18T18:53:44.177' AS DateTime))
INSERT [dbo].[Message] ([Id], [MessageContent], [SenderId], [ConversationId], [ConversationType], [SendTime]) VALUES (649, N'Sounds great!', 2, 1, 1, CAST(N'2021-05-18T18:54:23.523' AS DateTime))
INSERT [dbo].[Message] ([Id], [MessageContent], [SenderId], [ConversationId], [ConversationType], [SendTime]) VALUES (650, N'Hi there! Do you enjoy the Dune series? I''ve heard they made a movie about it!', 2, 1, 0, CAST(N'2021-05-18T18:57:37.827' AS DateTime))
INSERT [dbo].[Message] ([Id], [MessageContent], [SenderId], [ConversationId], [ConversationType], [SendTime]) VALUES (651, N'Definitely! Must watch for me, are you in? ', 1, 1, 0, CAST(N'2021-05-18T18:57:30.740' AS DateTime))
INSERT [dbo].[Message] ([Id], [MessageContent], [SenderId], [ConversationId], [ConversationType], [SendTime]) VALUES (654, N'Would you like hanging out this weekend? ', 1, 2, 0, CAST(N'2021-05-18T18:59:08.440' AS DateTime))
INSERT [dbo].[Message] ([Id], [MessageContent], [SenderId], [ConversationId], [ConversationType], [SendTime]) VALUES (655, N'I will be thinking about it ^^', 3, 2, 0, CAST(N'2021-05-18T19:00:15.017' AS DateTime))
SET IDENTITY_INSERT [dbo].[Message] OFF
GO
SET IDENTITY_INSERT [dbo].[PrivateConversation] ON 

INSERT [dbo].[PrivateConversation] ([Id], [User1Id], [User2Id], [ConversationType]) VALUES (1, 1, 2, 0)
INSERT [dbo].[PrivateConversation] ([Id], [User1Id], [User2Id], [ConversationType]) VALUES (2, 1, 3, 0)
SET IDENTITY_INSERT [dbo].[PrivateConversation] OFF
GO
USE [master]
GO
ALTER DATABASE [BookLoopDb] SET  READ_WRITE 
GO
