use master
go
if exists(select * from SysObjects where name='LgShopDB')
drop database LgShopDB
go
create database LgShopDB

on primary(
name='LgShopDB',
filename='E:\201817380102֣��\2020׿Խ��Ŀ\��ҵ��ĿԴ�뼰���ݿ�\���ݿ�\LgShopDB.mdf',
maxsize=10mb,
size=6mb
)
log on(
name='LgShopDB_log',
filename='E:\201817380102֣��\2020׿Խ��Ŀ\��ҵ��ĿԴ�뼰���ݿ�\���ݿ�\LgShopDB_log.ldf',
maxsize=10mb,
size=6mb
)
go
use LgShopDB
go

--�û���Ϣ��
create table UserInfo(
UserID int primary key identity(1,1), --�û�id
UserName nvarchar(100) not null,--�û�����
UserAccount nvarchar(20) not null,--�û��˺�
UserPassword nvarchar(30) not null,--�û�����
UserPhoto nvarchar(50) default('') not null,--�û�ͷ��
UserSex nvarchar(2) check(usersex='��' or usersex='Ů') not null,--�û��Ա�
UserAge int not null,--�û�����
UserEmail nvarchar(30) not null,--�û�����
UserPhont nvarchar(50) not null,--�û��绰
UserCard nvarchar(30) not null,--�û����֤��
UserBirthdays date not null,--�û�����
UserWallet decimal(2) default(0),--�û�Ǯ�����˻���
CoverPhoto nvarchar(100),--����ͼƬ
ReceivingAddress nvarchar(100) not null ,--�ջ��ַ
)
select * from UserInfo
update UserInfo set UserName='',UserAccount='',UserEmail='',UserAge='',UserSex='',UserCard='',ReceivingAddress='',UserBirthdays='',UserPhont='' where UserID=''
go

--�����
create table TypeTable(
TypeID int primary key identity(1,1),--����id
TypeName nvarchar(20) not null,--��������
TID int foreign key  references TypeTable(TypeID) ,--��������
)

--��������
insert into TypeTable values('�����װ',null),('���԰칫',null),('��������',null),('ʳƷ��ʳ',null)
insert into TypeTable values('��װ',1),('Ůװ',1),('ͯװ',1),('����װ',1)
insert into TypeTable values('�칫�豸',2),('�칫�ľ�',2),('�칫�Ĳ�',2),('��������',2)
insert into TypeTable values('ֽƷʪ��',3),('�������',3),('�����Ʒ',3),('��ͥ���',3)
insert into TypeTable values('����ʳƷ',4),('�������',4),('������ʳ',4),('����',4)
insert into TypeTable values('��ʿ����',5),('��ʿ��װ',5),('��ʿЬ��',5),('Ƥ��',5)
insert into TypeTable values('Ůʿ����',6),('Ůʿ��װ',6),('ŮʿЬ��',6),('ȹ��',6)
insert into TypeTable values('��ͯ����',7),('��ͯ��װ',7),('ͯЬ',7),('��ͯñ��',7)
insert into TypeTable values('������',8),('���п�',8),('����Ь',8)
insert into TypeTable values('�๦��һ���',9),('��ӡ��',9),('�����豸',9),('���ϻ�',9),('�绰��',9),('���չ�',9)
insert into TypeTable values('�칫��ֽ',10),('�����ľ�',10),('�ļ�����',10),('������',10),('����',10),('������Ʒ',10)
insert into TypeTable values('īˮ',11),('ī��',11),('ī��',11)
insert into TypeTable values('�ʼǱ�',12),('������',12),('��Ϸ��',12),('ƽ�����',12),('̨ʽ��',12),('һ���',12)
insert into TypeTable values('��Ͳֽ',13),('��ֽ',13),('����ֽ',13),('ʪ��',13)
insert into TypeTable values('ϴ��Һ',14),('ϴ�·�',14),('ϴ����',14),('���',14)
insert into TypeTable values('ϴ�ྫ',15),('��޼�',15),('����Һ',15),('�������¼�',15)
insert into TypeTable values('һ������Ʒ',16),('��๤��',16),('������Ʒ',16)
insert into TypeTable values('������ʳ',17),('��������',17)
insert into TypeTable values('����',18),('���',18),('����',18),('����',18),('ɳ����',18)
insert into TypeTable values('������',19),('���ȳ�',19),('�˱���',19),('��ͷ',19)
insert into TypeTable values('ˮ',20),('̼������',20),('��������',20),('����',20),('�̲�',20),('��Ƭ����',20)
select * from TypeTable
go

--select * from TypeTable where TID in (select TypeID from TypeTable where TID is null)
--select * from TypeTable t1,TypeTable t2 where t1.TID=t2.TypeID  and t2.TID=1
--��Ʒ��Ϣ��
create table GoodsTable(
GoodsID int primary key identity(1,1),--��Ʒid
GoodsName nvarchar(30) not null,--��Ʒ����
GoodsPrice decimal not null,--��Ʒ�ּ�
OldGoodsPrice decimal not null,--��Ʒԭ��
GoodsInventory int not null,--��Ʒ���
TID int foreign key references TypeTable(TypeID),--�������ࣨ�����
GoodsDescribe nvarchar(100) ,--��Ʒ�������
GoodsStar int default(0) ,--��Ʒ�Ǽ�
GoodsHot int ,--�����ȶ�
)
go

--��Ʒ��ϢͼƬ��ϵ��
create table GoodsPhoto(
RID int primary key identity(1,1),--id
GoodsID int foreign key references GoodsTable(GoodsID),--��Ʒid�������
PhotoPath nvarchar(50) not null,--ͼƬ·��
)
go

--���ﳵ��
create table ShoppingCartTable(
CartID int primary key identity(1,1),--���ﳵid
UserID int foreign key references UserInfo(UserID),--�û�id
GoodsID int foreign key references GoodsTable(GoodsID),--��Ʒid
ShoppingNum int default(1) ,--��������
ShoppingTime date ,--���빺�ﳵʱ��
)
go

--���۱�
create table CommentTable(
CommentID int primary key identity(1,1),--����id
UserID int foreign key references UserInfo(UserID),--�û�id
GoodsID int foreign key references GoodsTable(GoodsID),--��Ʒid
CommentContent nvarchar(200) not null,--��������
CommentStarRating int,--�����Ǽ�
CommentTime date ,--����ʱ�� 
)
go

--������
create table FeedbackTable(
FeedbackID  int primary key identity(1,1),--����id
UserID int foreign key references UserInfo(UserID),--�û�id
FeedbackContent nvarchar(200) not null,--��������
FeedbackTime date ,--����ʱ��
IsDealwith int default(0) check(IsDealwith='0' or IsDealwith='1')--�Ƿ���0:δ����
)
go

--������
create table OrderTable(
OrderID int primary key identity(1,1),--����id
UserID int foreign key references UserInfo(UserID),--�û�id
GoodsID int foreign key references GoodsTable(GoodsID),--��Ʒid
GoodsNum int ,--��Ʒ����
GetTime date ,--�����ύʱ��
OrderAmount decimal,--�������
IsReceiving int default(0) ,--�Ƿ��ջ�(0:δ�ջ���1:ȷ���ջ�)
IsComment int default(0),--�Ƿ����ۣ�����(0:δ���ۣ�1��������)
)