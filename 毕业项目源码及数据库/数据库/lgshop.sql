USE [master]
GO
/****** Object:  Database [LgShopDB]    Script Date: 2020/7/23 22:56:16 ******/
CREATE DATABASE [LgShopDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'LgShopDB', FILENAME = N'E:\201817380102郑靖\2020卓越项目\毕业项目源码及数据库\数据库\LgShopDB.mdf' , SIZE = 6144KB , MAXSIZE = 10240KB , FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'LgShopDB_log', FILENAME = N'E:\201817380102郑靖\2020卓越项目\毕业项目源码及数据库\数据库\LgShopDB_log.ldf' , SIZE = 6144KB , MAXSIZE = 10240KB , FILEGROWTH = 10%)
GO
ALTER DATABASE [LgShopDB] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [LgShopDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [LgShopDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [LgShopDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [LgShopDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [LgShopDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [LgShopDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [LgShopDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [LgShopDB] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [LgShopDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [LgShopDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [LgShopDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [LgShopDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [LgShopDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [LgShopDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [LgShopDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [LgShopDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [LgShopDB] SET  ENABLE_BROKER 
GO
ALTER DATABASE [LgShopDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [LgShopDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [LgShopDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [LgShopDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [LgShopDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [LgShopDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [LgShopDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [LgShopDB] SET RECOVERY FULL 
GO
ALTER DATABASE [LgShopDB] SET  MULTI_USER 
GO
ALTER DATABASE [LgShopDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [LgShopDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [LgShopDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [LgShopDB] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
EXEC sys.sp_db_vardecimal_storage_format N'LgShopDB', N'ON'
GO
USE [LgShopDB]
GO
/****** Object:  Table [dbo].[CollectionTable]    Script Date: 2020/7/23 22:56:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CollectionTable](
	[CollectionID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NULL,
	[GoodsID] [int] NULL,
	[IsCollection] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[CollectionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CommentTable]    Script Date: 2020/7/23 22:56:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CommentTable](
	[CommentID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NULL,
	[GoodsID] [int] NULL,
	[CommentContent] [nvarchar](200) NOT NULL,
	[CommentStarRating] [int] NULL,
	[CommentTime] [date] NULL,
	[Reportingnums] [int] NULL,
	[IsTop] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[CommentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[FeedbackTable]    Script Date: 2020/7/23 22:56:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FeedbackTable](
	[FeedbackID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NULL,
	[FeedbackContent] [nvarchar](200) NOT NULL,
	[FeedbackTime] [date] NULL,
	[IsDealwith] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[FeedbackID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[GoodsPhoto]    Script Date: 2020/7/23 22:56:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GoodsPhoto](
	[RID] [int] IDENTITY(1,1) NOT NULL,
	[GoodsID] [int] NULL,
	[PhotoPath] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[RID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[GoodsTable]    Script Date: 2020/7/23 22:56:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GoodsTable](
	[GoodsID] [int] IDENTITY(1,1) NOT NULL,
	[GoodsName] [nvarchar](30) NOT NULL,
	[GoodsPrice] [decimal](18, 2) NOT NULL,
	[OldGoodsPrice] [decimal](18, 2) NOT NULL,
	[GoodsInventory] [int] NOT NULL,
	[TID] [int] NULL,
	[GoodsDescribe] [nvarchar](100) NULL,
	[GoodsStar] [int] NULL,
	[GoodsHot] [int] NULL,
	[IsDelte] [int] NULL,
	[IsGet] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[GoodsID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[NoticeTable]    Script Date: 2020/7/23 22:56:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NoticeTable](
	[NoticeID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NULL,
	[NoticeTitle] [nvarchar](50) NOT NULL,
	[NoticeContent] [nvarchar](200) NOT NULL,
	[NoticeTime] [date] NULL,
	[IsLook] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[NoticeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[OrderTable]    Script Date: 2020/7/23 22:56:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderTable](
	[OrderID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NULL,
	[GoodsID] [int] NULL,
	[GoodsNum] [int] NULL,
	[GetTime] [date] NULL,
	[OrderAmount] [decimal](18, 2) NULL,
	[IsReceiving] [int] NULL,
	[IsComment] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[OrderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ShoppingCartTable]    Script Date: 2020/7/23 22:56:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ShoppingCartTable](
	[CartID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NULL,
	[GoodsID] [int] NULL,
	[ShoppingNum] [int] NULL,
	[ShoppingTime] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[CartID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TypeTable]    Script Date: 2020/7/23 22:56:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TypeTable](
	[TypeID] [int] IDENTITY(1,1) NOT NULL,
	[TypeName] [nvarchar](20) NOT NULL,
	[TID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[TypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserInfo]    Script Date: 2020/7/23 22:56:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserInfo](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](100) NOT NULL,
	[UserAccount] [nvarchar](20) NOT NULL,
	[UserPassword] [nvarchar](30) NOT NULL,
	[UserPhoto] [nvarchar](50) NOT NULL,
	[UserSex] [nvarchar](2) NOT NULL,
	[UserAge] [int] NOT NULL,
	[UserEmail] [nvarchar](30) NOT NULL,
	[UserPhont] [nvarchar](50) NOT NULL,
	[UserCard] [nvarchar](30) NOT NULL,
	[UserBirthdays] [date] NOT NULL,
	[UserWallet] [decimal](18, 2) NULL,
	[CoverPhoto] [nvarchar](100) NULL,
	[ReceivingAddress] [nvarchar](100) NOT NULL,
	[IsDelte] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[CollectionTable] ON 

INSERT [dbo].[CollectionTable] ([CollectionID], [UserID], [GoodsID], [IsCollection]) VALUES (1, 1, 36, 0)
SET IDENTITY_INSERT [dbo].[CollectionTable] OFF
SET IDENTITY_INSERT [dbo].[CommentTable] ON 

INSERT [dbo].[CommentTable] ([CommentID], [UserID], [GoodsID], [CommentContent], [CommentStarRating], [CommentTime], [Reportingnums], [IsTop]) VALUES (1, 1, 36, N'1111', 4, CAST(0x4E410B00 AS Date), 0, 0)
INSERT [dbo].[CommentTable] ([CommentID], [UserID], [GoodsID], [CommentContent], [CommentStarRating], [CommentTime], [Reportingnums], [IsTop]) VALUES (4, 1, 32, N'很好', 4, CAST(0x4F410B00 AS Date), 0, 0)
SET IDENTITY_INSERT [dbo].[CommentTable] OFF
SET IDENTITY_INSERT [dbo].[GoodsPhoto] ON 

INSERT [dbo].[GoodsPhoto] ([RID], [GoodsID], [PhotoPath]) VALUES (1, 1, N'凡客卫衣_1.jpg')
INSERT [dbo].[GoodsPhoto] ([RID], [GoodsID], [PhotoPath]) VALUES (3, 2, N'羽绒服_1.jpg')
INSERT [dbo].[GoodsPhoto] ([RID], [GoodsID], [PhotoPath]) VALUES (4, 2, N'羽绒服_2.jpg')
INSERT [dbo].[GoodsPhoto] ([RID], [GoodsID], [PhotoPath]) VALUES (5, 2, N'羽绒服_3.jpg')
INSERT [dbo].[GoodsPhoto] ([RID], [GoodsID], [PhotoPath]) VALUES (6, 2, N'羽绒服_4.jpg')
INSERT [dbo].[GoodsPhoto] ([RID], [GoodsID], [PhotoPath]) VALUES (7, 2, N'羽绒服_5.jpg')
INSERT [dbo].[GoodsPhoto] ([RID], [GoodsID], [PhotoPath]) VALUES (8, 2, N'羽绒服_6.jpg')
INSERT [dbo].[GoodsPhoto] ([RID], [GoodsID], [PhotoPath]) VALUES (9, 3, N'针织裤_1.jpg')
INSERT [dbo].[GoodsPhoto] ([RID], [GoodsID], [PhotoPath]) VALUES (10, 4, N'男鞋_1.jpg')
INSERT [dbo].[GoodsPhoto] ([RID], [GoodsID], [PhotoPath]) VALUES (11, 4, N'帆布鞋_1.jpg')
INSERT [dbo].[GoodsPhoto] ([RID], [GoodsID], [PhotoPath]) VALUES (12, 5, N'男运动鞋_1.jpg')
INSERT [dbo].[GoodsPhoto] ([RID], [GoodsID], [PhotoPath]) VALUES (13, 6, N'皮带_1.jpg')
INSERT [dbo].[GoodsPhoto] ([RID], [GoodsID], [PhotoPath]) VALUES (14, 7, N'女上衣_1.jpg')
INSERT [dbo].[GoodsPhoto] ([RID], [GoodsID], [PhotoPath]) VALUES (15, 7, N'女上衣_2.jpg')
INSERT [dbo].[GoodsPhoto] ([RID], [GoodsID], [PhotoPath]) VALUES (16, 8, N'女士下装_1.jpg')
INSERT [dbo].[GoodsPhoto] ([RID], [GoodsID], [PhotoPath]) VALUES (17, 9, N'女鞋_1.jpg')
INSERT [dbo].[GoodsPhoto] ([RID], [GoodsID], [PhotoPath]) VALUES (18, 10, N'童装_1.jpg')
INSERT [dbo].[GoodsPhoto] ([RID], [GoodsID], [PhotoPath]) VALUES (19, 10, N'童装_2.jpg')
INSERT [dbo].[GoodsPhoto] ([RID], [GoodsID], [PhotoPath]) VALUES (20, 11, N'中老年上装_1.jpg')
INSERT [dbo].[GoodsPhoto] ([RID], [GoodsID], [PhotoPath]) VALUES (21, 12, N'多功能一体机_1.jpg')
INSERT [dbo].[GoodsPhoto] ([RID], [GoodsID], [PhotoPath]) VALUES (22, 13, N'打印机_1.jpg')
INSERT [dbo].[GoodsPhoto] ([RID], [GoodsID], [PhotoPath]) VALUES (23, 14, N'打印机_2.jpg')
INSERT [dbo].[GoodsPhoto] ([RID], [GoodsID], [PhotoPath]) VALUES (24, 14, N'打印机_3.jpg')
INSERT [dbo].[GoodsPhoto] ([RID], [GoodsID], [PhotoPath]) VALUES (25, 15, N'打印机_4.jpg')
INSERT [dbo].[GoodsPhoto] ([RID], [GoodsID], [PhotoPath]) VALUES (26, 16, N'保险柜_1.jpg')
INSERT [dbo].[GoodsPhoto] ([RID], [GoodsID], [PhotoPath]) VALUES (27, 17, N'家用保险柜_1.jpg')
INSERT [dbo].[GoodsPhoto] ([RID], [GoodsID], [PhotoPath]) VALUES (28, 18, N'复合机_1.jpg')
INSERT [dbo].[GoodsPhoto] ([RID], [GoodsID], [PhotoPath]) VALUES (29, 19, N'办公用纸_1.jpg')
INSERT [dbo].[GoodsPhoto] ([RID], [GoodsID], [PhotoPath]) VALUES (30, 20, N'笔记本_1.jpg')
INSERT [dbo].[GoodsPhoto] ([RID], [GoodsID], [PhotoPath]) VALUES (31, 21, N'超级本_1.jpg')
INSERT [dbo].[GoodsPhoto] ([RID], [GoodsID], [PhotoPath]) VALUES (32, 22, N'游戏本_1.jpg')
INSERT [dbo].[GoodsPhoto] ([RID], [GoodsID], [PhotoPath]) VALUES (33, 23, N'卷筒纸_1.jpg')
INSERT [dbo].[GoodsPhoto] ([RID], [GoodsID], [PhotoPath]) VALUES (34, 24, N'抽纸_1.jpg')
INSERT [dbo].[GoodsPhoto] ([RID], [GoodsID], [PhotoPath]) VALUES (35, 25, N'洗衣液_1.jpg')
INSERT [dbo].[GoodsPhoto] ([RID], [GoodsID], [PhotoPath]) VALUES (36, 26, N'洗衣液_2.jpg')
INSERT [dbo].[GoodsPhoto] ([RID], [GoodsID], [PhotoPath]) VALUES (37, 27, N'洗衣粉_1.jpg')
INSERT [dbo].[GoodsPhoto] ([RID], [GoodsID], [PhotoPath]) VALUES (38, 28, N'洗洁精_1.jpg')
INSERT [dbo].[GoodsPhoto] ([RID], [GoodsID], [PhotoPath]) VALUES (39, 28, N'洗洁精_2.jpg')
INSERT [dbo].[GoodsPhoto] ([RID], [GoodsID], [PhotoPath]) VALUES (40, 29, N'洗洁精_3.jpg')
INSERT [dbo].[GoodsPhoto] ([RID], [GoodsID], [PhotoPath]) VALUES (41, 30, N'洁厕剂_1.jpg')
INSERT [dbo].[GoodsPhoto] ([RID], [GoodsID], [PhotoPath]) VALUES (42, 31, N'消毒液_1.jpg')
INSERT [dbo].[GoodsPhoto] ([RID], [GoodsID], [PhotoPath]) VALUES (43, 32, N'垃圾袋_1.jpg')
INSERT [dbo].[GoodsPhoto] ([RID], [GoodsID], [PhotoPath]) VALUES (44, 33, N'拖把_1.jpg')
INSERT [dbo].[GoodsPhoto] ([RID], [GoodsID], [PhotoPath]) VALUES (45, 34, N'蚊香_1.jpg')
INSERT [dbo].[GoodsPhoto] ([RID], [GoodsID], [PhotoPath]) VALUES (46, 35, N'美好时光_1.jpg')
INSERT [dbo].[GoodsPhoto] ([RID], [GoodsID], [PhotoPath]) VALUES (47, 36, N'果冻_1.jpg')
INSERT [dbo].[GoodsPhoto] ([RID], [GoodsID], [PhotoPath]) VALUES (48, 37, N'饼干_1.jpg')
INSERT [dbo].[GoodsPhoto] ([RID], [GoodsID], [PhotoPath]) VALUES (49, 38, N'红烧牛肉面_1.jpg')
INSERT [dbo].[GoodsPhoto] ([RID], [GoodsID], [PhotoPath]) VALUES (50, 39, N'老坛_1.jpg')
INSERT [dbo].[GoodsPhoto] ([RID], [GoodsID], [PhotoPath]) VALUES (51, 40, N'怡宝_1.jpg')
INSERT [dbo].[GoodsPhoto] ([RID], [GoodsID], [PhotoPath]) VALUES (52, 41, N'可口可乐_1.jpg')
INSERT [dbo].[GoodsPhoto] ([RID], [GoodsID], [PhotoPath]) VALUES (53, 42, N'红牛_1.jpg')
INSERT [dbo].[GoodsPhoto] ([RID], [GoodsID], [PhotoPath]) VALUES (54, 1, N'办公用纸_1.jpg')
INSERT [dbo].[GoodsPhoto] ([RID], [GoodsID], [PhotoPath]) VALUES (55, 1, N'保险柜_1.jpg')
SET IDENTITY_INSERT [dbo].[GoodsPhoto] OFF
SET IDENTITY_INSERT [dbo].[GoodsTable] ON 

INSERT [dbo].[GoodsTable] ([GoodsID], [GoodsName], [GoodsPrice], [OldGoodsPrice], [GoodsInventory], [TID], [GoodsDescribe], [GoodsStar], [GoodsHot], [IsDelte], [IsGet]) VALUES (1, N'凡客卫衣 男款 黑色', CAST(69.00 AS Decimal(18, 2)), CAST(138.00 AS Decimal(18, 2)), 1500, 21, N'色泽自然,不易起球不易掉毛，柔软舒适 新型织造工艺', 4, 12, 1, 1)
INSERT [dbo].[GoodsTable] ([GoodsID], [GoodsName], [GoodsPrice], [OldGoodsPrice], [GoodsInventory], [TID], [GoodsDescribe], [GoodsStar], [GoodsHot], [IsDelte], [IsGet]) VALUES (2, N'羽绒服 无缝锁温智能发热鹅绒服 黑色', CAST(319.00 AS Decimal(18, 2)), CAST(638.00 AS Decimal(18, 2)), 2000, 21, N'无缝锁温智能发热鹅绒服，智能调温，给你双倍温暖，整衣可水洗需要搭配充电宝，本产品不包含充电宝', 5, 600, 0, 0)
INSERT [dbo].[GoodsTable] ([GoodsID], [GoodsName], [GoodsPrice], [OldGoodsPrice], [GoodsInventory], [TID], [GoodsDescribe], [GoodsStar], [GoodsHot], [IsDelte], [IsGet]) VALUES (3, N'针织裤 重水洗拉绒 收脚口', CAST(69.00 AS Decimal(18, 2)), CAST(138.00 AS Decimal(18, 2)), 600, 22, N'专业设备缝制，适合出游，运动', 4, 500, 0, 0)
INSERT [dbo].[GoodsTable] ([GoodsID], [GoodsName], [GoodsPrice], [OldGoodsPrice], [GoodsInventory], [TID], [GoodsDescribe], [GoodsStar], [GoodsHot], [IsDelte], [IsGet]) VALUES (4, N'帆布鞋 男款 藏蓝色', CAST(69.00 AS Decimal(18, 2)), CAST(138.00 AS Decimal(18, 2)), 400, 23, N'低密度环保鞋垫，耐磨防滑', 4, 10, 0, 1)
INSERT [dbo].[GoodsTable] ([GoodsID], [GoodsName], [GoodsPrice], [OldGoodsPrice], [GoodsInventory], [TID], [GoodsDescribe], [GoodsStar], [GoodsHot], [IsDelte], [IsGet]) VALUES (5, N'男款运动潮鞋 S20502 黑色', CAST(239.00 AS Decimal(18, 2)), CAST(469.00 AS Decimal(18, 2)), 260, 23, N'轻盈舒适，看得到的重量！', 5, 1200, 0, 0)
INSERT [dbo].[GoodsTable] ([GoodsID], [GoodsName], [GoodsPrice], [OldGoodsPrice], [GoodsInventory], [TID], [GoodsDescribe], [GoodsStar], [GoodsHot], [IsDelte], [IsGet]) VALUES (6, N'杰凯威男士头层牛皮自动扣皮带', CAST(149.50 AS Decimal(18, 2)), CAST(299.00 AS Decimal(18, 2)), 500, 24, N'精选头层牛皮，网红双D，自动扣', 3, 115, 0, 0)
INSERT [dbo].[GoodsTable] ([GoodsID], [GoodsName], [GoodsPrice], [OldGoodsPrice], [GoodsInventory], [TID], [GoodsDescribe], [GoodsStar], [GoodsHot], [IsDelte], [IsGet]) VALUES (7, N'女式柔软防皱印花衬衫', CAST(164.50 AS Decimal(18, 2)), CAST(329.00 AS Decimal(18, 2)), 1250, 25, N'时尚翻领设计，时尚大方，尽显女性脖颈美', 4, 560, 0, 0)
INSERT [dbo].[GoodsTable] ([GoodsID], [GoodsName], [GoodsPrice], [OldGoodsPrice], [GoodsInventory], [TID], [GoodsDescribe], [GoodsStar], [GoodsHot], [IsDelte], [IsGet]) VALUES (8, N'女款牛仔裤 修身铅笔裤 ', CAST(69.00 AS Decimal(18, 2)), CAST(138.00 AS Decimal(18, 2)), 1250, 26, N'外观平滑，柔软舒适', 4, 120, 0, 0)
INSERT [dbo].[GoodsTable] ([GoodsID], [GoodsName], [GoodsPrice], [OldGoodsPrice], [GoodsInventory], [TID], [GoodsDescribe], [GoodsStar], [GoodsHot], [IsDelte], [IsGet]) VALUES (9, N'女款运动潮鞋小白鞋 ', CAST(128.00 AS Decimal(18, 2)), CAST(458.00 AS Decimal(18, 2)), 1250, 27, N'细选好料，拼接设计', 4, 500, 0, 0)
INSERT [dbo].[GoodsTable] ([GoodsID], [GoodsName], [GoodsPrice], [OldGoodsPrice], [GoodsInventory], [TID], [GoodsDescribe], [GoodsStar], [GoodsHot], [IsDelte], [IsGet]) VALUES (10, N'童装卫衣 圆领 ', CAST(59.00 AS Decimal(18, 2)), CAST(118.00 AS Decimal(18, 2)), 400, 29, N'柔软舒适', 4, 200, 0, 0)
INSERT [dbo].[GoodsTable] ([GoodsID], [GoodsName], [GoodsPrice], [OldGoodsPrice], [GoodsInventory], [TID], [GoodsDescribe], [GoodsStar], [GoodsHot], [IsDelte], [IsGet]) VALUES (11, N'林先森中老年上装 ', CAST(122.00 AS Decimal(18, 2)), CAST(428.00 AS Decimal(18, 2)), 520, 33, N'柔软舒适，适合老年人', 4, 120, 0, 0)
INSERT [dbo].[GoodsTable] ([GoodsID], [GoodsName], [GoodsPrice], [OldGoodsPrice], [GoodsInventory], [TID], [GoodsDescribe], [GoodsStar], [GoodsHot], [IsDelte], [IsGet]) VALUES (12, N'三星黑白激光多功能一体机 ', CAST(868.00 AS Decimal(18, 2)), CAST(1000.00 AS Decimal(18, 2)), 400, 36, N'首页打印时间：少于 8.5 秒(自待机模式)', 5, 20, 0, 0)
INSERT [dbo].[GoodsTable] ([GoodsID], [GoodsName], [GoodsPrice], [OldGoodsPrice], [GoodsInventory], [TID], [GoodsDescribe], [GoodsStar], [GoodsHot], [IsDelte], [IsGet]) VALUES (13, N'爱普生原装喷墨文档照片打印机 ', CAST(799.00 AS Decimal(18, 2)), CAST(899.00 AS Decimal(18, 2)), 100, 37, N'原厂大容量墨水', 5, 50, 0, 0)
INSERT [dbo].[GoodsTable] ([GoodsID], [GoodsName], [GoodsPrice], [OldGoodsPrice], [GoodsInventory], [TID], [GoodsDescribe], [GoodsStar], [GoodsHot], [IsDelte], [IsGet]) VALUES (14, N'惠普移动便携式打印机 ', CAST(2188.00 AS Decimal(18, 2)), CAST(2488.00 AS Decimal(18, 2)), 20, 37, N'环保，方便快捷', 5, 62, 0, 1)
INSERT [dbo].[GoodsTable] ([GoodsID], [GoodsName], [GoodsPrice], [OldGoodsPrice], [GoodsInventory], [TID], [GoodsDescribe], [GoodsStar], [GoodsHot], [IsDelte], [IsGet]) VALUES (15, N'爱普生Epson LQ-730KII 发票打印 ', CAST(1699.00 AS Decimal(18, 2)), CAST(1799.00 AS Decimal(18, 2)), 50, 37, N'打印发票首选爱普生', 4, 40, 0, 0)
INSERT [dbo].[GoodsTable] ([GoodsID], [GoodsName], [GoodsPrice], [OldGoodsPrice], [GoodsInventory], [TID], [GoodsDescribe], [GoodsStar], [GoodsHot], [IsDelte], [IsGet]) VALUES (16, N'得力电子密码防盗2层保险柜 ', CAST(1450.00 AS Decimal(18, 2)), CAST(1600.00 AS Decimal(18, 2)), 150, 41, N'高安全性，放心装', 2, 10, 0, 0)
INSERT [dbo].[GoodsTable] ([GoodsID], [GoodsName], [GoodsPrice], [OldGoodsPrice], [GoodsInventory], [TID], [GoodsDescribe], [GoodsStar], [GoodsHot], [IsDelte], [IsGet]) VALUES (17, N'得力家用保管箱 ', CAST(995.00 AS Decimal(18, 2)), CAST(1150.00 AS Decimal(18, 2)), 150, 41, N'家用放心保险柜', 4, 50, 0, 0)
INSERT [dbo].[GoodsTable] ([GoodsID], [GoodsName], [GoodsPrice], [OldGoodsPrice], [GoodsInventory], [TID], [GoodsDescribe], [GoodsStar], [GoodsHot], [IsDelte], [IsGet]) VALUES (18, N'柯尼卡美能达数码复合机', CAST(3359.00 AS Decimal(18, 2)), CAST(4400.00 AS Decimal(18, 2)), 112, 39, N'数码复合机', 4, 210, 0, 0)
INSERT [dbo].[GoodsTable] ([GoodsID], [GoodsName], [GoodsPrice], [OldGoodsPrice], [GoodsInventory], [TID], [GoodsDescribe], [GoodsStar], [GoodsHot], [IsDelte], [IsGet]) VALUES (19, N'亚太战斗金刚 A4 70g多功能复印纸', CAST(175.00 AS Decimal(18, 2)), CAST(220.00 AS Decimal(18, 2)), 112, 42, N'包装：8包/箱 500张/包型号：A4类型：复印纸', 4, 600, 0, 0)
INSERT [dbo].[GoodsTable] ([GoodsID], [GoodsName], [GoodsPrice], [OldGoodsPrice], [GoodsInventory], [TID], [GoodsDescribe], [GoodsStar], [GoodsHot], [IsDelte], [IsGet]) VALUES (20, N'联想笔记本电脑', CAST(3988.00 AS Decimal(18, 2)), CAST(4210.00 AS Decimal(18, 2)), 400, 51, N'i7-5557U 4G 8G SSHD+500G HD5500核显 Win8.1', 4, 1200, 0, 0)
INSERT [dbo].[GoodsTable] ([GoodsID], [GoodsName], [GoodsPrice], [OldGoodsPrice], [GoodsInventory], [TID], [GoodsDescribe], [GoodsStar], [GoodsHot], [IsDelte], [IsGet]) VALUES (21, N'联想超轻薄笔记本电脑', CAST(5490.00 AS Decimal(18, 2)), CAST(5600.00 AS Decimal(18, 2)), 100, 52, N'i5-7200U 8G 256G SSD 940MX 2G FHD IPS银色', 4, 1800, 0, 0)
INSERT [dbo].[GoodsTable] ([GoodsID], [GoodsName], [GoodsPrice], [OldGoodsPrice], [GoodsInventory], [TID], [GoodsDescribe], [GoodsStar], [GoodsHot], [IsDelte], [IsGet]) VALUES (22, N'华硕飞行堡垒游戏笔记本电脑', CAST(6490.00 AS Decimal(18, 2)), CAST(6999.00 AS Decimal(18, 2)), 400, 51, N'(i7-4720HQ 8G 128G SSD+1TB GTX950M 4G独显) 黑色 i7-4720HQ', 4, 1150, 0, 0)
INSERT [dbo].[GoodsTable] ([GoodsID], [GoodsName], [GoodsPrice], [OldGoodsPrice], [GoodsInventory], [TID], [GoodsDescribe], [GoodsStar], [GoodsHot], [IsDelte], [IsGet]) VALUES (23, N'顺清柔卷筒纸', CAST(23.50 AS Decimal(18, 2)), CAST(28.90 AS Decimal(18, 2)), 120, 57, N'尺寸为23cm*7cm', 4, 200, 0, 0)
INSERT [dbo].[GoodsTable] ([GoodsID], [GoodsName], [GoodsPrice], [OldGoodsPrice], [GoodsInventory], [TID], [GoodsDescribe], [GoodsStar], [GoodsHot], [IsDelte], [IsGet]) VALUES (24, N'幸福阳光抽纸 ', CAST(11.50 AS Decimal(18, 2)), CAST(16.00 AS Decimal(18, 2)), 400, 58, N'纸品健康/环保', 5, 160, 0, 0)
INSERT [dbo].[GoodsTable] ([GoodsID], [GoodsName], [GoodsPrice], [OldGoodsPrice], [GoodsInventory], [TID], [GoodsDescribe], [GoodsStar], [GoodsHot], [IsDelte], [IsGet]) VALUES (25, N'蓝月亮深层洁净洗衣液', CAST(38.50 AS Decimal(18, 2)), CAST(50.40 AS Decimal(18, 2)), 120, 61, N'健康/环保', 5, 240, 0, 0)
INSERT [dbo].[GoodsTable] ([GoodsID], [GoodsName], [GoodsPrice], [OldGoodsPrice], [GoodsInventory], [TID], [GoodsDescribe], [GoodsStar], [GoodsHot], [IsDelte], [IsGet]) VALUES (26, N'超能植翠低泡洗衣液', CAST(46.50 AS Decimal(18, 2)), CAST(49.00 AS Decimal(18, 2)), 400, 61, N'健康/环保', 5, 450, 0, 0)
INSERT [dbo].[GoodsTable] ([GoodsID], [GoodsName], [GoodsPrice], [OldGoodsPrice], [GoodsInventory], [TID], [GoodsDescribe], [GoodsStar], [GoodsHot], [IsDelte], [IsGet]) VALUES (27, N'雕牌洗衣粉', CAST(8.50 AS Decimal(18, 2)), CAST(12.00 AS Decimal(18, 2)), 360, 62, N'超效加酶508g 强效去污无磷衣物清洁', 5, 1140, 0, 0)
INSERT [dbo].[GoodsTable] ([GoodsID], [GoodsName], [GoodsPrice], [OldGoodsPrice], [GoodsInventory], [TID], [GoodsDescribe], [GoodsStar], [GoodsHot], [IsDelte], [IsGet]) VALUES (28, N'立白洗洁精', CAST(4.50 AS Decimal(18, 2)), CAST(6.00 AS Decimal(18, 2)), 1400, 65, N'去油不伤手 金桔洗洁精500g瓶装', 4, 456, 0, 0)
INSERT [dbo].[GoodsTable] ([GoodsID], [GoodsName], [GoodsPrice], [OldGoodsPrice], [GoodsInventory], [TID], [GoodsDescribe], [GoodsStar], [GoodsHot], [IsDelte], [IsGet]) VALUES (29, N'雕牌洗洁精', CAST(16.80 AS Decimal(18, 2)), CAST(19.00 AS Decimal(18, 2)), 860, 65, N'冷水去油1.5kg餐洗净', 4, 620, 0, 0)
INSERT [dbo].[GoodsTable] ([GoodsID], [GoodsName], [GoodsPrice], [OldGoodsPrice], [GoodsInventory], [TID], [GoodsDescribe], [GoodsStar], [GoodsHot], [IsDelte], [IsGet]) VALUES (30, N'威猛先生 洁厕液', CAST(6.80 AS Decimal(18, 2)), CAST(9.00 AS Decimal(18, 2)), 560, 66, N'去污：采用欧洲先进技术，有效清洁，且不伤马桶表面。', 4, 468, 0, 0)
INSERT [dbo].[GoodsTable] ([GoodsID], [GoodsName], [GoodsPrice], [OldGoodsPrice], [GoodsInventory], [TID], [GoodsDescribe], [GoodsStar], [GoodsHot], [IsDelte], [IsGet]) VALUES (31, N'威露士多用途温除菌消毒液', CAST(30.90 AS Decimal(18, 2)), CAST(32.00 AS Decimal(18, 2)), 450, 67, N'成分温和 衣物消毒 家居消毒 日用品消毒', 4, 128, 0, 0)
INSERT [dbo].[GoodsTable] ([GoodsID], [GoodsName], [GoodsPrice], [OldGoodsPrice], [GoodsInventory], [TID], [GoodsDescribe], [GoodsStar], [GoodsHot], [IsDelte], [IsGet]) VALUES (32, N'黑色手提垃圾袋 ', CAST(2.50 AS Decimal(18, 2)), CAST(3.00 AS Decimal(18, 2)), 1499, 70, N'手提塑料袋 购物袋 （30*45mm） 单卷　30个装/卷', 4, 151, 0, 0)
INSERT [dbo].[GoodsTable] ([GoodsID], [GoodsName], [GoodsPrice], [OldGoodsPrice], [GoodsInventory], [TID], [GoodsDescribe], [GoodsStar], [GoodsHot], [IsDelte], [IsGet]) VALUES (33, N'妙洁官方滚轮吸水胶棉拖把 ', CAST(45.50 AS Decimal(18, 2)), CAST(55.00 AS Decimal(18, 2)), 100, 70, N'手压挤水海绵拖把免手洗吸水 耐用', 4, 500, 0, 0)
INSERT [dbo].[GoodsTable] ([GoodsID], [GoodsName], [GoodsPrice], [OldGoodsPrice], [GoodsInventory], [TID], [GoodsDescribe], [GoodsStar], [GoodsHot], [IsDelte], [IsGet]) VALUES (34, N'枪手无烟蚊香 ', CAST(4.50 AS Decimal(18, 2)), CAST(5.00 AS Decimal(18, 2)), 1500, 71, N'10单盘清香大盘无烟型清香型 清香型', 5, 480, 0, 0)
INSERT [dbo].[GoodsTable] ([GoodsID], [GoodsName], [GoodsPrice], [OldGoodsPrice], [GoodsInventory], [TID], [GoodsDescribe], [GoodsStar], [GoodsHot], [IsDelte], [IsGet]) VALUES (35, N'美好时光海苔 ', CAST(15.50 AS Decimal(18, 2)), CAST(25.00 AS Decimal(18, 2)), 600, 72, N'', 4, 40, 0, 0)
INSERT [dbo].[GoodsTable] ([GoodsID], [GoodsName], [GoodsPrice], [OldGoodsPrice], [GoodsInventory], [TID], [GoodsDescribe], [GoodsStar], [GoodsHot], [IsDelte], [IsGet]) VALUES (36, N'喜之郎果冻 ', CAST(12.50 AS Decimal(18, 2)), CAST(20.00 AS Decimal(18, 2)), 1395, 73, N'圆你当宇航员的梦', 4, 5002, 0, 0)
INSERT [dbo].[GoodsTable] ([GoodsID], [GoodsName], [GoodsPrice], [OldGoodsPrice], [GoodsInventory], [TID], [GoodsDescribe], [GoodsStar], [GoodsHot], [IsDelte], [IsGet]) VALUES (37, N'早餐饼干 ', CAST(9.50 AS Decimal(18, 2)), CAST(12.00 AS Decimal(18, 2)), 120, 74, N'最美味的饼干', 4, 130, 0, 1)
INSERT [dbo].[GoodsTable] ([GoodsID], [GoodsName], [GoodsPrice], [OldGoodsPrice], [GoodsInventory], [TID], [GoodsDescribe], [GoodsStar], [GoodsHot], [IsDelte], [IsGet]) VALUES (38, N'康师傅红烧牛肉面 ', CAST(3.50 AS Decimal(18, 2)), CAST(4.00 AS Decimal(18, 2)), 797, 79, N'', 4, 1203, 0, 0)
INSERT [dbo].[GoodsTable] ([GoodsID], [GoodsName], [GoodsPrice], [OldGoodsPrice], [GoodsInventory], [TID], [GoodsDescribe], [GoodsStar], [GoodsHot], [IsDelte], [IsGet]) VALUES (39, N'康师傅老坛酸菜面 ', CAST(3.50 AS Decimal(18, 2)), CAST(4.00 AS Decimal(18, 2)), 689, 79, N'美味', 5, 1244, 0, 0)
INSERT [dbo].[GoodsTable] ([GoodsID], [GoodsName], [GoodsPrice], [OldGoodsPrice], [GoodsInventory], [TID], [GoodsDescribe], [GoodsStar], [GoodsHot], [IsDelte], [IsGet]) VALUES (40, N'怡宝 ', CAST(1.50 AS Decimal(18, 2)), CAST(2.00 AS Decimal(18, 2)), 520, 83, N'美味', 5, 610, 0, 0)
INSERT [dbo].[GoodsTable] ([GoodsID], [GoodsName], [GoodsPrice], [OldGoodsPrice], [GoodsInventory], [TID], [GoodsDescribe], [GoodsStar], [GoodsHot], [IsDelte], [IsGet]) VALUES (41, N'可口可乐 ', CAST(3.50 AS Decimal(18, 2)), CAST(4.00 AS Decimal(18, 2)), 450, 84, N'好喝的味道', 5, 400, 0, 0)
INSERT [dbo].[GoodsTable] ([GoodsID], [GoodsName], [GoodsPrice], [OldGoodsPrice], [GoodsInventory], [TID], [GoodsDescribe], [GoodsStar], [GoodsHot], [IsDelte], [IsGet]) VALUES (42, N'红牛 ', CAST(3.50 AS Decimal(18, 2)), CAST(4.00 AS Decimal(18, 2)), 500, 85, N'你的能量超乎你的想象', 5, 1050, 0, 0)
SET IDENTITY_INSERT [dbo].[GoodsTable] OFF
SET IDENTITY_INSERT [dbo].[NoticeTable] ON 

INSERT [dbo].[NoticeTable] ([NoticeID], [UserID], [NoticeTitle], [NoticeContent], [NoticeTime], [IsLook]) VALUES (1, NULL, N'商品停售公告', N'各位乐购商城用户你们好,于2020年7月9日起凡客卫衣 男款 黑色商品将从商城下架，查看详情请联系TEL:13677447830', CAST(0x4E410B00 AS Date), 1)
INSERT [dbo].[NoticeTable] ([NoticeID], [UserID], [NoticeTitle], [NoticeContent], [NoticeTime], [IsLook]) VALUES (2, NULL, N'商品出售公告', N'各位乐购商城用户你们好,于2020年7月9日起凡客卫衣 男款 黑色商品将重新出售，查看详情请联系TEL:13677447830', CAST(0x4E410B00 AS Date), 1)
INSERT [dbo].[NoticeTable] ([NoticeID], [UserID], [NoticeTitle], [NoticeContent], [NoticeTime], [IsLook]) VALUES (3, 1, N'余额充值成功提醒', N'尊敬的用户，您于2020/7/9 23:11:35充值的金额500元已到账，请注意查收！', CAST(0x4E410B00 AS Date), 1)
INSERT [dbo].[NoticeTable] ([NoticeID], [UserID], [NoticeTitle], [NoticeContent], [NoticeTime], [IsLook]) VALUES (4, 1, N'商品发货通知', N'尊敬的麹义用户，您于2020年7月9日购买的商品喜之郎果冻 已发货，请注意查收！', CAST(0x4E410B00 AS Date), 1)
INSERT [dbo].[NoticeTable] ([NoticeID], [UserID], [NoticeTitle], [NoticeContent], [NoticeTime], [IsLook]) VALUES (5, 1, N'商品发货通知', N'尊敬的麹义用户，您于2020年7月9日购买的商品喜之郎果冻 已发货，请注意查收！', CAST(0x4E410B00 AS Date), 1)
INSERT [dbo].[NoticeTable] ([NoticeID], [UserID], [NoticeTitle], [NoticeContent], [NoticeTime], [IsLook]) VALUES (6, 1, N'商品发货通知', N'尊敬的麹义用户，您于2020年7月9日购买的商品康师傅红烧牛肉面 已发货，请注意查收！', CAST(0x4E410B00 AS Date), 1)
INSERT [dbo].[NoticeTable] ([NoticeID], [UserID], [NoticeTitle], [NoticeContent], [NoticeTime], [IsLook]) VALUES (7, 1, N'商品发货通知', N'尊敬的麹义用户，您于2020年7月9日购买的商品黑色手提垃圾袋 已发货，请注意查收！', CAST(0x4E410B00 AS Date), 1)
INSERT [dbo].[NoticeTable] ([NoticeID], [UserID], [NoticeTitle], [NoticeContent], [NoticeTime], [IsLook]) VALUES (8, 1, N'违规评论处理通知', N'尊敬的用户您好，您评论的内容:rrrrrr 因违反了乐购商城相关规定，已被删除，请您遵守相关规定！', CAST(0x4E410B00 AS Date), 1)
INSERT [dbo].[NoticeTable] ([NoticeID], [UserID], [NoticeTitle], [NoticeContent], [NoticeTime], [IsLook]) VALUES (9, 1, N'违规评论处理通知', N'尊敬的用户您好，您评论的内容:454545 因违反了乐购商城相关规定，已被删除，请您遵守相关规定！', CAST(0x4E410B00 AS Date), 1)
INSERT [dbo].[NoticeTable] ([NoticeID], [UserID], [NoticeTitle], [NoticeContent], [NoticeTime], [IsLook]) VALUES (10, 1, N'商品发货通知', N'尊敬的麹义用户，您于2020年7月10日购买的商品喜之郎果冻 已发货，请注意查收！', CAST(0x4F410B00 AS Date), 1)
INSERT [dbo].[NoticeTable] ([NoticeID], [UserID], [NoticeTitle], [NoticeContent], [NoticeTime], [IsLook]) VALUES (11, 1, N'商品发货通知', N'尊敬的麹义用户，您于2020年7月10日购买的商品康师傅老坛酸菜面 已发货，请注意查收！', CAST(0x4F410B00 AS Date), 1)
INSERT [dbo].[NoticeTable] ([NoticeID], [UserID], [NoticeTitle], [NoticeContent], [NoticeTime], [IsLook]) VALUES (12, NULL, N'商品停售公告', N'各位乐购商城用户你们好,于2020年7月10日起凡客卫衣 男款 黑色商品将从商城下架，查看详情请联系TEL:13677447830', CAST(0x4F410B00 AS Date), 1)
SET IDENTITY_INSERT [dbo].[NoticeTable] OFF
SET IDENTITY_INSERT [dbo].[OrderTable] ON 

INSERT [dbo].[OrderTable] ([OrderID], [UserID], [GoodsID], [GoodsNum], [GetTime], [OrderAmount], [IsReceiving], [IsComment]) VALUES (1, 1, 36, 1, CAST(0x4E410B00 AS Date), CAST(12.50 AS Decimal(18, 2)), 1, 1)
INSERT [dbo].[OrderTable] ([OrderID], [UserID], [GoodsID], [GoodsNum], [GetTime], [OrderAmount], [IsReceiving], [IsComment]) VALUES (2, 1, 36, 1, CAST(0x4E410B00 AS Date), CAST(12.50 AS Decimal(18, 2)), 1, 1)
INSERT [dbo].[OrderTable] ([OrderID], [UserID], [GoodsID], [GoodsNum], [GetTime], [OrderAmount], [IsReceiving], [IsComment]) VALUES (3, 1, 38, 3, CAST(0x4E410B00 AS Date), CAST(10.50 AS Decimal(18, 2)), 1, 1)
INSERT [dbo].[OrderTable] ([OrderID], [UserID], [GoodsID], [GoodsNum], [GetTime], [OrderAmount], [IsReceiving], [IsComment]) VALUES (4, 1, 32, 1, CAST(0x4E410B00 AS Date), CAST(2.50 AS Decimal(18, 2)), 1, 1)
INSERT [dbo].[OrderTable] ([OrderID], [UserID], [GoodsID], [GoodsNum], [GetTime], [OrderAmount], [IsReceiving], [IsComment]) VALUES (5, 1, 36, 3, CAST(0x4F410B00 AS Date), CAST(37.50 AS Decimal(18, 2)), 0, 0)
INSERT [dbo].[OrderTable] ([OrderID], [UserID], [GoodsID], [GoodsNum], [GetTime], [OrderAmount], [IsReceiving], [IsComment]) VALUES (6, 1, 39, 1, CAST(0x4F410B00 AS Date), CAST(3.50 AS Decimal(18, 2)), 0, 0)
SET IDENTITY_INSERT [dbo].[OrderTable] OFF
SET IDENTITY_INSERT [dbo].[TypeTable] ON 

INSERT [dbo].[TypeTable] ([TypeID], [TypeName], [TID]) VALUES (1, N'经典服装', NULL)
INSERT [dbo].[TypeTable] ([TypeID], [TypeName], [TID]) VALUES (2, N'电脑办公', NULL)
INSERT [dbo].[TypeTable] ([TypeID], [TypeName], [TID]) VALUES (3, N'生活日用', NULL)
INSERT [dbo].[TypeTable] ([TypeID], [TypeName], [TID]) VALUES (4, N'食品饮食', NULL)
INSERT [dbo].[TypeTable] ([TypeID], [TypeName], [TID]) VALUES (5, N'男装', 1)
INSERT [dbo].[TypeTable] ([TypeID], [TypeName], [TID]) VALUES (6, N'女装', 1)
INSERT [dbo].[TypeTable] ([TypeID], [TypeName], [TID]) VALUES (7, N'童装', 1)
INSERT [dbo].[TypeTable] ([TypeID], [TypeName], [TID]) VALUES (8, N'中老年装', 1)
INSERT [dbo].[TypeTable] ([TypeID], [TypeName], [TID]) VALUES (9, N'办公设备', 2)
INSERT [dbo].[TypeTable] ([TypeID], [TypeName], [TID]) VALUES (10, N'办公文具', 2)
INSERT [dbo].[TypeTable] ([TypeID], [TypeName], [TID]) VALUES (11, N'办公耗材', 2)
INSERT [dbo].[TypeTable] ([TypeID], [TypeName], [TID]) VALUES (12, N'电脑整机', 2)
INSERT [dbo].[TypeTable] ([TypeID], [TypeName], [TID]) VALUES (13, N'纸品湿巾', 3)
INSERT [dbo].[TypeTable] ([TypeID], [TypeName], [TID]) VALUES (14, N'衣物清洁', 3)
INSERT [dbo].[TypeTable] ([TypeID], [TypeName], [TID]) VALUES (15, N'清洁用品', 3)
INSERT [dbo].[TypeTable] ([TypeID], [TypeName], [TID]) VALUES (16, N'家庭清洁', 3)
INSERT [dbo].[TypeTable] ([TypeID], [TypeName], [TID]) VALUES (17, N'休闲食品', 4)
INSERT [dbo].[TypeTable] ([TypeID], [TypeName], [TID]) VALUES (18, N'饼干甜点', 4)
INSERT [dbo].[TypeTable] ([TypeID], [TypeName], [TID]) VALUES (19, N'方便速食', 4)
INSERT [dbo].[TypeTable] ([TypeID], [TypeName], [TID]) VALUES (20, N'饮料', 4)
INSERT [dbo].[TypeTable] ([TypeID], [TypeName], [TID]) VALUES (21, N'男士上衣', 5)
INSERT [dbo].[TypeTable] ([TypeID], [TypeName], [TID]) VALUES (22, N'男士下装', 5)
INSERT [dbo].[TypeTable] ([TypeID], [TypeName], [TID]) VALUES (23, N'男士鞋子', 5)
INSERT [dbo].[TypeTable] ([TypeID], [TypeName], [TID]) VALUES (24, N'皮带', 5)
INSERT [dbo].[TypeTable] ([TypeID], [TypeName], [TID]) VALUES (25, N'女士上衣', 6)
INSERT [dbo].[TypeTable] ([TypeID], [TypeName], [TID]) VALUES (26, N'女士下装', 6)
INSERT [dbo].[TypeTable] ([TypeID], [TypeName], [TID]) VALUES (27, N'女士鞋子', 6)
INSERT [dbo].[TypeTable] ([TypeID], [TypeName], [TID]) VALUES (28, N'裙子', 6)
INSERT [dbo].[TypeTable] ([TypeID], [TypeName], [TID]) VALUES (29, N'儿童上衣', 7)
INSERT [dbo].[TypeTable] ([TypeID], [TypeName], [TID]) VALUES (30, N'儿童下装', 7)
INSERT [dbo].[TypeTable] ([TypeID], [TypeName], [TID]) VALUES (31, N'童鞋', 7)
INSERT [dbo].[TypeTable] ([TypeID], [TypeName], [TID]) VALUES (32, N'儿童帽子', 7)
INSERT [dbo].[TypeTable] ([TypeID], [TypeName], [TID]) VALUES (33, N'中老年衣', 8)
INSERT [dbo].[TypeTable] ([TypeID], [TypeName], [TID]) VALUES (34, N'中老年裤', 8)
INSERT [dbo].[TypeTable] ([TypeID], [TypeName], [TID]) VALUES (35, N'老年鞋', 8)
INSERT [dbo].[TypeTable] ([TypeID], [TypeName], [TID]) VALUES (36, N'多功能一体机', 9)
INSERT [dbo].[TypeTable] ([TypeID], [TypeName], [TID]) VALUES (37, N'打印机', 9)
INSERT [dbo].[TypeTable] ([TypeID], [TypeName], [TID]) VALUES (38, N'传真设备', 9)
INSERT [dbo].[TypeTable] ([TypeID], [TypeName], [TID]) VALUES (39, N'复合机', 9)
INSERT [dbo].[TypeTable] ([TypeID], [TypeName], [TID]) VALUES (40, N'电话机', 9)
INSERT [dbo].[TypeTable] ([TypeID], [TypeName], [TID]) VALUES (41, N'保险柜', 9)
INSERT [dbo].[TypeTable] ([TypeID], [TypeName], [TID]) VALUES (42, N'办公用纸', 10)
INSERT [dbo].[TypeTable] ([TypeID], [TypeName], [TID]) VALUES (43, N'桌面文具', 10)
INSERT [dbo].[TypeTable] ([TypeID], [TypeName], [TID]) VALUES (44, N'文件管理', 10)
INSERT [dbo].[TypeTable] ([TypeID], [TypeName], [TID]) VALUES (45, N'计算器', 10)
INSERT [dbo].[TypeTable] ([TypeID], [TypeName], [TID]) VALUES (46, N'笔类', 10)
INSERT [dbo].[TypeTable] ([TypeID], [TypeName], [TID]) VALUES (47, N'财务用品', 10)
INSERT [dbo].[TypeTable] ([TypeID], [TypeName], [TID]) VALUES (48, N'墨水', 11)
INSERT [dbo].[TypeTable] ([TypeID], [TypeName], [TID]) VALUES (49, N'墨粉', 11)
INSERT [dbo].[TypeTable] ([TypeID], [TypeName], [TID]) VALUES (50, N'墨盒', 11)
INSERT [dbo].[TypeTable] ([TypeID], [TypeName], [TID]) VALUES (51, N'笔记本', 12)
INSERT [dbo].[TypeTable] ([TypeID], [TypeName], [TID]) VALUES (52, N'超极本', 12)
INSERT [dbo].[TypeTable] ([TypeID], [TypeName], [TID]) VALUES (53, N'游戏本', 12)
INSERT [dbo].[TypeTable] ([TypeID], [TypeName], [TID]) VALUES (54, N'平板电脑', 12)
INSERT [dbo].[TypeTable] ([TypeID], [TypeName], [TID]) VALUES (55, N'台式机', 12)
INSERT [dbo].[TypeTable] ([TypeID], [TypeName], [TID]) VALUES (56, N'一体机', 12)
INSERT [dbo].[TypeTable] ([TypeID], [TypeName], [TID]) VALUES (57, N'卷筒纸', 13)
INSERT [dbo].[TypeTable] ([TypeID], [TypeName], [TID]) VALUES (58, N'抽纸', 13)
INSERT [dbo].[TypeTable] ([TypeID], [TypeName], [TID]) VALUES (59, N'手帕纸', 13)
INSERT [dbo].[TypeTable] ([TypeID], [TypeName], [TID]) VALUES (60, N'湿巾', 13)
INSERT [dbo].[TypeTable] ([TypeID], [TypeName], [TID]) VALUES (61, N'洗衣液', 14)
INSERT [dbo].[TypeTable] ([TypeID], [TypeName], [TID]) VALUES (62, N'洗衣粉', 14)
INSERT [dbo].[TypeTable] ([TypeID], [TypeName], [TID]) VALUES (63, N'洗衣皂', 14)
INSERT [dbo].[TypeTable] ([TypeID], [TypeName], [TID]) VALUES (64, N'皂粉', 14)
INSERT [dbo].[TypeTable] ([TypeID], [TypeName], [TID]) VALUES (65, N'洗洁精', 15)
INSERT [dbo].[TypeTable] ([TypeID], [TypeName], [TID]) VALUES (66, N'洁厕剂', 15)
INSERT [dbo].[TypeTable] ([TypeID], [TypeName], [TID]) VALUES (67, N'消毒液', 15)
INSERT [dbo].[TypeTable] ([TypeID], [TypeName], [TID]) VALUES (68, N'空气清新剂', 15)
INSERT [dbo].[TypeTable] ([TypeID], [TypeName], [TID]) VALUES (69, N'一次性用品', 16)
INSERT [dbo].[TypeTable] ([TypeID], [TypeName], [TID]) VALUES (70, N'清洁工具', 16)
INSERT [dbo].[TypeTable] ([TypeID], [TypeName], [TID]) VALUES (71, N'驱虫用品', 16)
INSERT [dbo].[TypeTable] ([TypeID], [TypeName], [TID]) VALUES (72, N'休闲零食', 17)
INSERT [dbo].[TypeTable] ([TypeID], [TypeName], [TID]) VALUES (73, N'果冻布丁', 17)
INSERT [dbo].[TypeTable] ([TypeID], [TypeName], [TID]) VALUES (74, N'饼干', 18)
INSERT [dbo].[TypeTable] ([TypeID], [TypeName], [TID]) VALUES (75, N'面包', 18)
INSERT [dbo].[TypeTable] ([TypeID], [TypeName], [TID]) VALUES (76, N'曲奇', 18)
INSERT [dbo].[TypeTable] ([TypeID], [TypeName], [TID]) VALUES (77, N'蛋卷', 18)
INSERT [dbo].[TypeTable] ([TypeID], [TypeName], [TID]) VALUES (78, N'沙琪玛', 18)
INSERT [dbo].[TypeTable] ([TypeID], [TypeName], [TID]) VALUES (79, N'方便面', 19)
INSERT [dbo].[TypeTable] ([TypeID], [TypeName], [TID]) VALUES (80, N'火腿肠', 19)
INSERT [dbo].[TypeTable] ([TypeID], [TypeName], [TID]) VALUES (81, N'八宝粥', 19)
INSERT [dbo].[TypeTable] ([TypeID], [TypeName], [TID]) VALUES (82, N'罐头', 19)
INSERT [dbo].[TypeTable] ([TypeID], [TypeName], [TID]) VALUES (83, N'水', 20)
INSERT [dbo].[TypeTable] ([TypeID], [TypeName], [TID]) VALUES (84, N'碳酸饮料', 20)
INSERT [dbo].[TypeTable] ([TypeID], [TypeName], [TID]) VALUES (85, N'功能饮料', 20)
INSERT [dbo].[TypeTable] ([TypeID], [TypeName], [TID]) VALUES (86, N'咖啡', 20)
INSERT [dbo].[TypeTable] ([TypeID], [TypeName], [TID]) VALUES (87, N'奶茶', 20)
INSERT [dbo].[TypeTable] ([TypeID], [TypeName], [TID]) VALUES (88, N'麦片谷类', 20)
SET IDENTITY_INSERT [dbo].[TypeTable] OFF
SET IDENTITY_INSERT [dbo].[UserInfo] ON 

INSERT [dbo].[UserInfo] ([UserID], [UserName], [UserAccount], [UserPassword], [UserPhoto], [UserSex], [UserAge], [UserEmail], [UserPhont], [UserCard], [UserBirthdays], [UserWallet], [CoverPhoto], [ReceivingAddress], [IsDelte]) VALUES (1, N'麹义', N'13677447830', N'666666', N'真禅圣王.jpg', N'男', 19, N'945747089@qq.com', N'13677447830', N'431226200009247010', CAST(0x12250B00 AS Date), CAST(421.00 AS Decimal(18, 2)), N'', N'湖南长沙市雨花区xxxxxxx', 0)
SET IDENTITY_INSERT [dbo].[UserInfo] OFF
ALTER TABLE [dbo].[CollectionTable] ADD  DEFAULT ((1)) FOR [IsCollection]
GO
ALTER TABLE [dbo].[CommentTable] ADD  DEFAULT (getdate()) FOR [CommentTime]
GO
ALTER TABLE [dbo].[CommentTable] ADD  DEFAULT ((0)) FOR [Reportingnums]
GO
ALTER TABLE [dbo].[CommentTable] ADD  DEFAULT ((0)) FOR [IsTop]
GO
ALTER TABLE [dbo].[FeedbackTable] ADD  DEFAULT ((0)) FOR [IsDealwith]
GO
ALTER TABLE [dbo].[GoodsTable] ADD  DEFAULT ((0)) FOR [GoodsStar]
GO
ALTER TABLE [dbo].[GoodsTable] ADD  DEFAULT ((0)) FOR [IsDelte]
GO
ALTER TABLE [dbo].[GoodsTable] ADD  DEFAULT ((0)) FOR [IsGet]
GO
ALTER TABLE [dbo].[NoticeTable] ADD  DEFAULT (getdate()) FOR [NoticeTime]
GO
ALTER TABLE [dbo].[NoticeTable] ADD  DEFAULT ((0)) FOR [IsLook]
GO
ALTER TABLE [dbo].[OrderTable] ADD  DEFAULT ((0)) FOR [IsReceiving]
GO
ALTER TABLE [dbo].[OrderTable] ADD  DEFAULT ((0)) FOR [IsComment]
GO
ALTER TABLE [dbo].[ShoppingCartTable] ADD  DEFAULT ((1)) FOR [ShoppingNum]
GO
ALTER TABLE [dbo].[UserInfo] ADD  DEFAULT ('') FOR [UserPhoto]
GO
ALTER TABLE [dbo].[UserInfo] ADD  DEFAULT ((0)) FOR [UserWallet]
GO
ALTER TABLE [dbo].[UserInfo] ADD  DEFAULT ((0)) FOR [IsDelte]
GO
ALTER TABLE [dbo].[CollectionTable]  WITH CHECK ADD FOREIGN KEY([GoodsID])
REFERENCES [dbo].[GoodsTable] ([GoodsID])
GO
ALTER TABLE [dbo].[CollectionTable]  WITH CHECK ADD FOREIGN KEY([UserID])
REFERENCES [dbo].[UserInfo] ([UserID])
GO
ALTER TABLE [dbo].[CommentTable]  WITH CHECK ADD FOREIGN KEY([GoodsID])
REFERENCES [dbo].[GoodsTable] ([GoodsID])
GO
ALTER TABLE [dbo].[CommentTable]  WITH CHECK ADD FOREIGN KEY([UserID])
REFERENCES [dbo].[UserInfo] ([UserID])
GO
ALTER TABLE [dbo].[FeedbackTable]  WITH CHECK ADD FOREIGN KEY([UserID])
REFERENCES [dbo].[UserInfo] ([UserID])
GO
ALTER TABLE [dbo].[GoodsPhoto]  WITH CHECK ADD FOREIGN KEY([GoodsID])
REFERENCES [dbo].[GoodsTable] ([GoodsID])
GO
ALTER TABLE [dbo].[GoodsTable]  WITH CHECK ADD FOREIGN KEY([TID])
REFERENCES [dbo].[TypeTable] ([TypeID])
GO
ALTER TABLE [dbo].[NoticeTable]  WITH CHECK ADD FOREIGN KEY([UserID])
REFERENCES [dbo].[UserInfo] ([UserID])
GO
ALTER TABLE [dbo].[OrderTable]  WITH CHECK ADD FOREIGN KEY([GoodsID])
REFERENCES [dbo].[GoodsTable] ([GoodsID])
GO
ALTER TABLE [dbo].[OrderTable]  WITH CHECK ADD FOREIGN KEY([UserID])
REFERENCES [dbo].[UserInfo] ([UserID])
GO
ALTER TABLE [dbo].[ShoppingCartTable]  WITH CHECK ADD FOREIGN KEY([GoodsID])
REFERENCES [dbo].[GoodsTable] ([GoodsID])
GO
ALTER TABLE [dbo].[ShoppingCartTable]  WITH CHECK ADD FOREIGN KEY([UserID])
REFERENCES [dbo].[UserInfo] ([UserID])
GO
ALTER TABLE [dbo].[TypeTable]  WITH CHECK ADD FOREIGN KEY([TID])
REFERENCES [dbo].[TypeTable] ([TypeID])
GO
ALTER TABLE [dbo].[CollectionTable]  WITH CHECK ADD CHECK  (([IsCollection]=(1) OR [IsCollection]=(0)))
GO
ALTER TABLE [dbo].[FeedbackTable]  WITH CHECK ADD CHECK  (([IsDealwith]='0' OR [IsDealwith]='1'))
GO
ALTER TABLE [dbo].[NoticeTable]  WITH CHECK ADD CHECK  (([IsLook]=(0) OR [IsLook]=(1)))
GO
ALTER TABLE [dbo].[UserInfo]  WITH CHECK ADD CHECK  (([usersex]='男' OR [usersex]='女'))
GO
USE [master]
GO
ALTER DATABASE [LgShopDB] SET  READ_WRITE 
GO
