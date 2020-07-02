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
delete from FeedbackTable where FeedbackID=1

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
UserWallet decimal(18,2) default(0),--用户钱包（账户）
CoverPhoto nvarchar(100),--封面图片
ReceivingAddress nvarchar(100) not null ,--收获地址
)
select * from UserInfo
update Userinfo set UserWallet+=50 where userid=1
update UserInfo set UserName='',UserAccount='',UserEmail='',UserAge='',UserSex='',UserCard='',ReceivingAddress='',UserBirthdays='',UserPhont='' where UserID=''
go

--分类表
create table TypeTable(
TypeID int primary key identity(1,1),--分类id
TypeName nvarchar(20) not null,--分类名称
TID int foreign key  references TypeTable(TypeID) ,--所属分类
)
select * from TypeTable where tid=5
--分类数据
insert into TypeTable values('经典服装',null),('电脑办公',null),('生活日用',null),('食品饮食',null)
insert into TypeTable values('男装',1),('女装',1),('童装',1),('中老年装',1)
insert into TypeTable values('办公设备',2),('办公文具',2),('办公耗材',2),('电脑整机',2)
insert into TypeTable values('纸品湿巾',3),('衣物清洁',3),('清洁用品',3),('家庭清洁',3)
insert into TypeTable values('休闲食品',4),('饼干甜点',4),('方便速食',4),('饮料',4)
insert into TypeTable values('男士上衣',5),('男士下装',5),('男士鞋子',5),('皮带',5)
insert into TypeTable values('女士上衣',6),('女士下装',6),('女士鞋子',6),('裙子',6)
insert into TypeTable values('儿童上衣',7),('儿童下装',7),('童鞋',7),('儿童帽子',7)
insert into TypeTable values('中老年衣',8),('中老年裤',8),('老年鞋',8)
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
select * from TypeTable where TID=5
--select * from TypeTable where TID in (select TypeID from TypeTable where TID is null)
--select * from TypeTable t1,TypeTable t2 where t1.TID=t2.TypeID  and t2.TID=1

--商品信息表
create table GoodsTable(
GoodsID int primary key identity(1,1),--商品id
GoodsName nvarchar(30) not null,--商品名称
GoodsPrice decimal(18,2) not null,--商品现价
OldGoodsPrice decimal(18,2) not null,--商品原价
GoodsInventory int not null,--商品库存
TID int foreign key references TypeTable(TypeID),--所属分类（外键）
GoodsDescribe nvarchar(100) ,--商品描述简介
GoodsStar int default(0) ,--商品星级
GoodsHot int ,--销售热度
)
go
--添加商品信息数据
--男装
insert into GoodsTable values('凡客卫衣 男款 黑色',69.00,138.00,1500,21,'色泽自然,不易起球不易掉毛，柔软舒适 新型织造工艺',4,12),
('羽绒服 无缝锁温智能发热鹅绒服 黑色',319.00,638.00,2000,21,'无缝锁温智能发热鹅绒服，智能调温，给你双倍温暖，整衣可水洗需要搭配充电宝，本产品不包含充电宝',5,600),
('针织裤 重水洗拉绒 收脚口',69.00,138.00,600,22,'专业设备缝制，适合出游，运动',4,500),
('帆布鞋 男款 藏蓝色',69.00,138.00,400,23,'低密度环保鞋垫，耐磨防滑',4,10),
('男款运动潮鞋 S20502 黑色',239.00,469.00,260,23,'轻盈舒适，看得到的重量！',5,1200),
('杰凯威男士头层牛皮自动扣皮带',149.50,299.00,500,24,'精选头层牛皮，网红双D，自动扣',3,115)
--女装
insert into GoodsTable values('女式柔软防皱印花衬衫',164.50,329.00,1250,25,'时尚翻领设计，时尚大方，尽显女性脖颈美',4,560),
('女款牛仔裤 修身铅笔裤 ',69.00,138.00,1250,26,'外观平滑，柔软舒适',4,120),
('女款运动潮鞋小白鞋 ',128.00,458.00,1250,27,'细选好料，拼接设计',4,500)
--童装
insert into GoodsTable values('童装卫衣 圆领 ',59.00,118.00,400,29,'柔软舒适',4,200)
--中老年装
insert into GoodsTable values('林先森中老年上装 ',122.00,428.00,520,33,'柔软舒适，适合老年人',4,120)
--办公设备
insert into GoodsTable values('三星黑白激光多功能一体机 ',868.00,1000.00,400,36,'首页打印时间：少于 8.5 秒(自待机模式)',5,20)
insert into GoodsTable values('爱普生原装喷墨文档照片打印机 ',799.00,899.00,100,37,'原厂大容量墨水',5,50)
insert into GoodsTable values('惠普移动便携式打印机 ',2188.00,2488.00,20,37,'环保，方便快捷',5,62)
insert into GoodsTable values('爱普生Epson LQ-730KII 发票打印 ',1699.00,1799.00,50,37,'打印发票首选爱普生',4,40)
insert into GoodsTable values('得力电子密码防盗2层保险柜 ',1450.00,1600.00,150,41,'高安全性，放心装',2,10)
insert into GoodsTable values('得力家用保管箱 ',995.00,1150.00,150,41,'家用放心保险柜',4,50)
insert into GoodsTable values('柯尼卡美能达数码复合机',3359.00,4400.00,112,39,'数码复合机',4,210)
--办公文具
insert into GoodsTable values('亚太战斗金刚 A4 70g多功能复印纸',175.00,220.00,112,42,'包装：8包/箱 500张/包型号：A4类型：复印纸',4,600)
--办公耗材
--电脑整机
insert into GoodsTable values('联想笔记本电脑',3988.00,4210.00,400,51,'i7-5557U 4G 8G SSHD+500G HD5500核显 Win8.1',4,1200)
insert into GoodsTable values('联想超轻薄笔记本电脑',5490.00,5600.00,100,52,'i5-7200U 8G 256G SSD 940MX 2G FHD IPS银色',4,1800)
insert into GoodsTable values('华硕飞行堡垒游戏笔记本电脑',6490.00,6999.00,400,51,'(i7-4720HQ 8G 128G SSD+1TB GTX950M 4G独显) 黑色 i7-4720HQ',4,1150)
--纸品湿巾
insert into GoodsTable values('顺清柔卷筒纸',23.50,28.90,120,57,'尺寸为23cm*7cm',4,200)
insert into GoodsTable values('幸福阳光抽纸 ',11.50,16.00,400,58,'纸品健康/环保',5,160)
--衣物清洁
insert into GoodsTable values('蓝月亮深层洁净洗衣液',38.50,50.40,120,61,'健康/环保',5,240)
insert into GoodsTable values('超能植翠低泡洗衣液',46.50,49.00,400,61,'健康/环保',5,450)
insert into GoodsTable values('雕牌洗衣粉',8.50,12.00,360,62,'超效加酶508g 强效去污无磷衣物清洁',5,1140)
--清洁用品
insert into GoodsTable values('立白洗洁精',4.50,6.00,1400,65,'去油不伤手 金桔洗洁精500g瓶装',4,456)
insert into GoodsTable values('雕牌洗洁精',16.80,19.00,860,65,'冷水去油1.5kg餐洗净',4,620)
insert into GoodsTable values('威猛先生 洁厕液',6.80,9.00,560,66,'去污：采用欧洲先进技术，有效清洁，且不伤马桶表面。',4,468)
insert into GoodsTable values('威露士多用途温除菌消毒液',30.90,32.00,450,67,'成分温和 衣物消毒 家居消毒 日用品消毒',4,128)
--家庭清洁
insert into GoodsTable values('黑色手提垃圾袋 ',2.50,3.00,1500,70,'手提塑料袋 购物袋 （30*45mm） 单卷　30个装/卷',4,150)
insert into GoodsTable values('妙洁官方滚轮吸水胶棉拖把 ',45.50,55.00,100,70,'手压挤水海绵拖把免手洗吸水 耐用',4,500)
insert into GoodsTable values('枪手无烟蚊香 ',4.50,5.00,1500,71,'10单盘清香大盘无烟型清香型 清香型',5,480)
--休闲食品
insert into GoodsTable values('美好时光海苔 ',15.50,25.00,600,72,'',4,40)
insert into GoodsTable values('喜之郎果冻 ',12.50,20.00,1400,73,'圆你当宇航员的梦',4,5000)
--饼干甜点
insert into GoodsTable values('早餐饼干 ',9.50,12.00,120,74,'最美味的饼干',4,130)
--方便速食
insert into GoodsTable values('康师傅红烧牛肉面 ',3.50,4.00,800,79,'',4,1200)
insert into GoodsTable values('康师傅老坛酸菜面 ',3.50,4.00,690,79,'美味',5,1244)
--饮料
insert into GoodsTable values('怡宝 ',1.50,2.00,520,83,'美味',5,610)
insert into GoodsTable values('可口可乐 ',3.50,4.00,450,84,'好喝的味道',5,400)
insert into GoodsTable values('红牛 ',3.50,4.00,500,85,'你的能量超乎你的想象',5,1050)
--多表联合查询
select * from GoodsTable t1,GoodsPhoto t2,TypeTable t3 where t1.GoodsID=t2.GoodsID and t1.TID=t3.TypeID

select * from GoodsTable where TID in(
select TypeID from TypeTable where TID in(select TypeID from TypeTable
 where TID=(select TypeID from TypeTable where TypeID=4))
)




go

--商品信息图片联系表
create table GoodsPhoto(
RID int primary key identity(1,1),--id
GoodsID int foreign key references GoodsTable(GoodsID),--商品id（外键）
PhotoPath nvarchar(50) not null,--图片路径
)
insert into GoodsPhoto values(1,'凡客卫衣_1.jpg'),(1,'凡客卫衣_2.jpg'),
(2,'羽绒服_1.jpg'),(2,'羽绒服_2.jpg'),(2,'羽绒服_3.jpg'),(2,'羽绒服_4.jpg'),(2,'羽绒服_5.jpg'),(2,'羽绒服_6.jpg'),
(3,'针织裤_1.jpg'),(4,'男鞋_1.jpg'),(4,'帆布鞋_1.jpg'),(5,'男运动鞋_1.jpg'),(6,'皮带_1.jpg'),(7,'女上衣_1.jpg'),
(7,'女上衣_2.jpg'),(8,'女士下装_1.jpg'),(9,'女鞋_1.jpg'),(10,'童装_1.jpg'),(10,'童装_2.jpg'),(11,'中老年上装_1.jpg'),
(12,'多功能一体机_1.jpg'),(13,'打印机_1.jpg'),(14,'打印机_2.jpg'),(14,'打印机_3.jpg'),(15,'打印机_4.jpg'),
(16,'保险柜_1.jpg'),(17,'家用保险柜_1.jpg'),(18,'复合机_1.jpg'),(19,'办公用纸_1.jpg'),(20,'笔记本_1.jpg'),
(21,'超级本_1.jpg'),(22,'游戏本_1.jpg'),(23,'卷筒纸_1.jpg'),(24,'抽纸_1.jpg'),(25,'洗衣液_1.jpg'),(26,'洗衣液_2.jpg'),
(27,'洗衣粉_1.jpg'),(28,'洗洁精_1.jpg'),(28,'洗洁精_2.jpg'),(29,'洗洁精_3.jpg'),(30,'洁厕剂_1.jpg'),(31,'消毒液_1.jpg'),
(32,'垃圾袋_1.jpg'),(33,'拖把_1.jpg'),(34,'蚊香_1.jpg'),(35,'美好时光_1.jpg'),(36,'果冻_1.jpg'),(37,'饼干_1.jpg'),
(38,'红烧牛肉面_1.jpg'),(39,'老坛_1.jpg'),(40,'怡宝_1.jpg'),(41,'可口可乐_1.jpg'),(42,'红牛_1.jpg')
go
--购物车表
create table ShoppingCartTable(
CartID int primary key identity(1,1),--购物车id
UserID int foreign key references UserInfo(UserID),--用户id
GoodsID int foreign key references GoodsTable(GoodsID),--商品id
ShoppingNum int default(1) ,--购买数量
ShoppingTime date ,--加入购物车时间
)
select * from ShoppingCartTable
delete from ShoppingCartTable where userid=1 and GoodsID=1
insert into ShoppingCartTable values(1,1,2,'')
go

--评论表
create table CommentTable(
CommentID int primary key identity(1,1),--评论id
UserID int foreign key references UserInfo(UserID),--用户id
GoodsID int foreign key references GoodsTable(GoodsID),--商品id
CommentContent nvarchar(200) not null,--评论内容
CommentStarRating int,--评分星级
CommentTime date ,--评论时间 
Reportingnums int default(0),--举报数
)
select * from CommentTable
go

--反馈表
create table FeedbackTable(
FeedbackID  int primary key identity(1,1),--反馈id
UserID int foreign key references UserInfo(UserID),--用户id
FeedbackContent nvarchar(200) not null,--反馈内容
FeedbackTime date ,--反馈时间
IsDealwith int default(0) check(IsDealwith='0' or IsDealwith='1')--是否处理（0:未处理）
)
select * from FeedbackTable
go

--订单表
create table OrderTable(
OrderID int primary key identity(1,1),--订单id
UserID int foreign key references UserInfo(UserID),--用户id
GoodsID int foreign key references GoodsTable(GoodsID),--商品id
GoodsNum int ,--商品个数
GetTime date ,--订单提交时间
OrderAmount decimal(18,2),--订单金额
IsReceiving int default(0) ,--是否收货(0:未收货，1:确认收货)
IsComment int default(0),--是否评价，评论(0:未评论，1：已评论)
)
select * from OrderTable
insert into OrderTable values(1,1,5,'',12.00,0,0)
update OrderTable set IsReceiving=1 where OrderID=1

--收藏表
create table CollectionTable(
CollectionID int primary key identity(1,1),--收藏表
UserID int foreign key references UserInfo(UserID),--用户id
GoodsID int foreign key references GoodsTable(GoodsID),--商品id
IsCollection int default(1) check(IsCollection=1 or IsCollection=0),--是否收藏（0为否,1为是）
)
select * from CollectionTable where userid=1 and GoodsID=1
insert into CollectionTable values(1,25,1)
update CollectionTable set IsCollection=0 where CollectionID=1

--公告表
create table NoticeTable(
NoticeID int primary key identity(1,1),--公告id
UserID int foreign key references UserInfo(UserID),--用户id(外键)
NoticeTitle nvarchar(50) not null,--公告标题
NoticeContent nvarchar(200) not null,--公告内容
NoticeTime date default(getdate()),--公告时间
IsLook int default(0) check(IsLook=0 or IsLook=1),--用户是否查看(0为否)
)
drop table NoticeTable
insert into NoticeTable values(null,'网站上市通知','新的网站乐购商城要上市了！','2020-07-01',1)

