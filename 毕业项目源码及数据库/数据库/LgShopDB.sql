use master
go
if exists(select * from SysObjects where name='LgShopDB')
drop database LgShopDB
go
create database LgShopDB

on primary(
name='LgShopDB',
filename='E:\201817380102郑靖\2020卓越项目\毕业项目源码及数据库\数据库\LgShopDB.mdf',
maxsize=10mb,
size=6mb
)
log on(
name='LgShopDB_log',
filename='E:\201817380102郑靖\2020卓越项目\毕业项目源码及数据库\数据库\LgShopDB_log.ldf',
maxsize=10mb,
size=6mb
)
go
use LgShopDB
go

--用户信息表
create table UserInfo(
UserID int primary key identity(1,1), --用户id
UserName nvarchar(100) not null,--用户名称
UserAccount nvarchar(20) not null,--用户账号
UserPassword nvarchar(30) not null,--用户密码
UserPhoto nvarchar(50) default('') not null,--用户头像
UserSex nvarchar(2) check(usersex='男' or usersex='女') not null,--用户性别
UserAge int not null,--用户年龄
UserEmail nvarchar(30) not null,--用户邮箱
UserPhont nvarchar(50) not null,--用户电话
UserCard nvarchar(30) not null,--用户身份证号
UserBirthdays date not null,--用户生日
UserWallet decimal(2) default(0),--用户钱包（账户）
CoverPhoto nvarchar(100),--封面图片
ReceivingAddress nvarchar(100) not null ,--收获地址
)
select * from UserInfo
update UserInfo set UserName='',UserAccount='',UserEmail='',UserAge='',UserSex='',UserCard='',ReceivingAddress='',UserBirthdays='',UserPhont='' where UserID=''
go

--分类表
create table TypeTable(
TypeID int primary key identity(1,1),--分类id
TypeName nvarchar(20) not null,--分类名称
TID int foreign key  references TypeTable(TypeID) ,--所属分类
)

--分类数据
insert into TypeTable values('经典服装',null),('电脑办公',null),('生活日用',null),('食品饮食',null)
insert into TypeTable values('男装',1),('女装',1),('童装',1),('休闲装',1)
insert into TypeTable values('办公设备',2),('办公文具',2),('办公耗材',2),('电脑整机',2)
insert into TypeTable values('纸品湿巾',3),('衣物清洁',3),('清洁用品',3),('家庭清洁',3)
insert into TypeTable values('休闲食品',4),('饼干甜点',4),('方便速食',4),('饮料',4)
insert into TypeTable values('男士上衣',5),('男士下装',5),('男士鞋子',5),('皮带',5)
insert into TypeTable values('女士上衣',6),('女士下装',6),('女士鞋子',6),('裙子',6)
insert into TypeTable values('儿童上衣',7),('儿童下装',7),('童鞋',7),('儿童帽子',7)
insert into TypeTable values('休闲衣',8),('休闲裤',8),('休闲鞋',8)
insert into TypeTable values('多功能一体机',9),('打印机',9),('传真设备',9),('复合机',9),('电话机',9),('保险柜',9)
insert into TypeTable values('办公用纸',10),('桌面文具',10),('文件管理',10),('计算器',10),('笔类',10),('财务用品',10)
insert into TypeTable values('墨水',11),('墨粉',11),('墨盒',11)
insert into TypeTable values('笔记本',12),('超极本',12),('游戏本',12),('平板电脑',12),('台式机',12),('一体机',12)
insert into TypeTable values('卷筒纸',13),('抽纸',13),('手帕纸',13),('湿巾',13)
insert into TypeTable values('洗衣液',14),('洗衣粉',14),('洗衣皂',14),('皂粉',14)
insert into TypeTable values('洗洁精',15),('洁厕剂',15),('消毒液',15),('空气清新剂',15)
insert into TypeTable values('一次性用品',16),('清洁工具',16),('驱虫用品',16)
insert into TypeTable values('休闲零食',17),('果冻布丁',17)
insert into TypeTable values('饼干',18),('面包',18),('曲奇',18),('蛋卷',18),('沙琪玛',18)
insert into TypeTable values('方便面',19),('火腿肠',19),('八宝粥',19),('罐头',19)
insert into TypeTable values('水',20),('碳酸饮料',20),('功能饮料',20),('咖啡',20),('奶茶',20),('麦片谷类',20)
select * from TypeTable
go

--select * from TypeTable where TID in (select TypeID from TypeTable where TID is null)
--select * from TypeTable t1,TypeTable t2 where t1.TID=t2.TypeID  and t2.TID=1
--商品信息表
create table GoodsTable(
GoodsID int primary key identity(1,1),--商品id
GoodsName nvarchar(30) not null,--商品名称
GoodsPrice decimal not null,--商品现价
OldGoodsPrice decimal not null,--商品原价
GoodsInventory int not null,--商品库存
TID int foreign key references TypeTable(TypeID),--所属分类（外键）
GoodsDescribe nvarchar(100) ,--商品描述简介
GoodsStar int default(0) ,--商品星级
GoodsHot int ,--销售热度
)
go

--商品信息图片联系表
create table GoodsPhoto(
RID int primary key identity(1,1),--id
GoodsID int foreign key references GoodsTable(GoodsID),--商品id（外键）
PhotoPath nvarchar(50) not null,--图片路径
)
go

--购物车表
create table ShoppingCartTable(
CartID int primary key identity(1,1),--购物车id
UserID int foreign key references UserInfo(UserID),--用户id
GoodsID int foreign key references GoodsTable(GoodsID),--商品id
ShoppingNum int default(1) ,--购买数量
ShoppingTime date ,--加入购物车时间
)
go

--评论表
create table CommentTable(
CommentID int primary key identity(1,1),--评论id
UserID int foreign key references UserInfo(UserID),--用户id
GoodsID int foreign key references GoodsTable(GoodsID),--商品id
CommentContent nvarchar(200) not null,--评论内容
CommentStarRating int,--评分星级
CommentTime date ,--评论时间 
)
go

--反馈表
create table FeedbackTable(
FeedbackID  int primary key identity(1,1),--反馈id
UserID int foreign key references UserInfo(UserID),--用户id
FeedbackContent nvarchar(200) not null,--反馈内容
FeedbackTime date ,--反馈时间
IsDealwith int default(0) check(IsDealwith='0' or IsDealwith='1')--是否处理（0:未处理）
)
go

--订单表
create table OrderTable(
OrderID int primary key identity(1,1),--订单id
UserID int foreign key references UserInfo(UserID),--用户id
GoodsID int foreign key references GoodsTable(GoodsID),--商品id
GoodsNum int ,--商品个数
GetTime date ,--订单提交时间
OrderAmount decimal,--订单金额
IsReceiving int default(0) ,--是否收货(0:未收货，1:确认收货)
IsComment int default(0),--是否评价，评论(0:未评论，1：已评论)
)