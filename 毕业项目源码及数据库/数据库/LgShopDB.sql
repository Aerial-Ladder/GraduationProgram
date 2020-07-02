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
delete from FeedbackTable where FeedbackID=1

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
UserWallet decimal(18,2) default(0),--�û�Ǯ�����˻���
CoverPhoto nvarchar(100),--����ͼƬ
ReceivingAddress nvarchar(100) not null ,--�ջ��ַ
)
select * from UserInfo
update Userinfo set UserWallet+=50 where userid=1
update UserInfo set UserName='',UserAccount='',UserEmail='',UserAge='',UserSex='',UserCard='',ReceivingAddress='',UserBirthdays='',UserPhont='' where UserID=''
go

--�����
create table TypeTable(
TypeID int primary key identity(1,1),--����id
TypeName nvarchar(20) not null,--��������
TID int foreign key  references TypeTable(TypeID) ,--��������
)
select * from TypeTable where tid=5
--��������
insert into TypeTable values('�����װ',null),('���԰칫',null),('��������',null),('ʳƷ��ʳ',null)
insert into TypeTable values('��װ',1),('Ůװ',1),('ͯװ',1),('������װ',1)
insert into TypeTable values('�칫�豸',2),('�칫�ľ�',2),('�칫�Ĳ�',2),('��������',2)
insert into TypeTable values('ֽƷʪ��',3),('�������',3),('�����Ʒ',3),('��ͥ���',3)
insert into TypeTable values('����ʳƷ',4),('�������',4),('������ʳ',4),('����',4)
insert into TypeTable values('��ʿ����',5),('��ʿ��װ',5),('��ʿЬ��',5),('Ƥ��',5)
insert into TypeTable values('Ůʿ����',6),('Ůʿ��װ',6),('ŮʿЬ��',6),('ȹ��',6)
insert into TypeTable values('��ͯ����',7),('��ͯ��װ',7),('ͯЬ',7),('��ͯñ��',7)
insert into TypeTable values('��������',8),('�������',8),('����Ь',8)
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
select * from TypeTable where TID=5
--select * from TypeTable where TID in (select TypeID from TypeTable where TID is null)
--select * from TypeTable t1,TypeTable t2 where t1.TID=t2.TypeID  and t2.TID=1

--��Ʒ��Ϣ��
create table GoodsTable(
GoodsID int primary key identity(1,1),--��Ʒid
GoodsName nvarchar(30) not null,--��Ʒ����
GoodsPrice decimal(18,2) not null,--��Ʒ�ּ�
OldGoodsPrice decimal(18,2) not null,--��Ʒԭ��
GoodsInventory int not null,--��Ʒ���
TID int foreign key references TypeTable(TypeID),--�������ࣨ�����
GoodsDescribe nvarchar(100) ,--��Ʒ�������
GoodsStar int default(0) ,--��Ʒ�Ǽ�
GoodsHot int ,--�����ȶ�
)
go
--�����Ʒ��Ϣ����
--��װ
insert into GoodsTable values('�������� �п� ��ɫ',69.00,138.00,1500,21,'ɫ����Ȼ,���������׵�ë���������� ����֯�칤��',4,12),
('���޷� �޷��������ܷ��ȶ��޷� ��ɫ',319.00,638.00,2000,21,'�޷��������ܷ��ȶ��޷������ܵ��£�����˫����ů�����¿�ˮϴ��Ҫ�����籦������Ʒ��������籦',5,600),
('��֯�� ��ˮϴ���� �սſ�',69.00,138.00,600,22,'רҵ�豸���ƣ��ʺϳ��Σ��˶�',4,500),
('����Ь �п� ����ɫ',69.00,138.00,400,23,'���ܶȻ���Ь�棬��ĥ����',4,10),
('�п��˶���Ь S20502 ��ɫ',239.00,469.00,260,23,'��ӯ���ʣ����õ���������',5,1200),
('�ܿ�����ʿͷ��ţƤ�Զ���Ƥ��',149.50,299.00,500,24,'��ѡͷ��ţƤ������˫D���Զ���',3,115)
--Ůװ
insert into GoodsTable values('Ůʽ�������ӡ������',164.50,329.00,1250,25,'ʱ�з�����ƣ�ʱ�д󷽣�����Ů�Բ�����',4,560),
('Ů��ţ�п� ����Ǧ�ʿ� ',69.00,138.00,1250,26,'���ƽ������������',4,120),
('Ů���˶���ЬС��Ь ',128.00,458.00,1250,27,'ϸѡ���ϣ�ƴ�����',4,500)
--ͯװ
insert into GoodsTable values('ͯװ���� Բ�� ',59.00,118.00,400,29,'��������',4,200)
--������װ
insert into GoodsTable values('����ɭ��������װ ',122.00,428.00,520,33,'�������ʣ��ʺ�������',4,120)
--�칫�豸
insert into GoodsTable values('���Ǻڰ׼���๦��һ��� ',868.00,1000.00,400,36,'��ҳ��ӡʱ�䣺���� 8.5 ��(�Դ���ģʽ)',5,20)
insert into GoodsTable values('������ԭװ��ī�ĵ���Ƭ��ӡ�� ',799.00,899.00,100,37,'ԭ��������īˮ',5,50)
insert into GoodsTable values('�����ƶ���Яʽ��ӡ�� ',2188.00,2488.00,20,37,'������������',5,62)
insert into GoodsTable values('������Epson LQ-730KII ��Ʊ��ӡ ',1699.00,1799.00,50,37,'��ӡ��Ʊ��ѡ������',4,40)
insert into GoodsTable values('���������������2�㱣�չ� ',1450.00,1600.00,150,41,'�߰�ȫ�ԣ�����װ',2,10)
insert into GoodsTable values('�������ñ����� ',995.00,1150.00,150,41,'���÷��ı��չ�',4,50)
insert into GoodsTable values('���Ῠ���ܴ����븴�ϻ�',3359.00,4400.00,112,39,'���븴�ϻ�',4,210)
--�칫�ľ�
insert into GoodsTable values('��̫ս����� A4 70g�๦�ܸ�ӡֽ',175.00,220.00,112,42,'��װ��8��/�� 500��/���ͺţ�A4���ͣ���ӡֽ',4,600)
--�칫�Ĳ�
--��������
insert into GoodsTable values('����ʼǱ�����',3988.00,4210.00,400,51,'i7-5557U 4G 8G SSHD+500G HD5500���� Win8.1',4,1200)
insert into GoodsTable values('���볬�ᱡ�ʼǱ�����',5490.00,5600.00,100,52,'i5-7200U 8G 256G SSD 940MX 2G FHD IPS��ɫ',4,1800)
insert into GoodsTable values('��˶���б�����Ϸ�ʼǱ�����',6490.00,6999.00,400,51,'(i7-4720HQ 8G 128G SSD+1TB GTX950M 4G����) ��ɫ i7-4720HQ',4,1150)
--ֽƷʪ��
insert into GoodsTable values('˳�����Ͳֽ',23.50,28.90,120,57,'�ߴ�Ϊ23cm*7cm',4,200)
insert into GoodsTable values('�Ҹ������ֽ ',11.50,16.00,400,58,'ֽƷ����/����',5,160)
--�������
insert into GoodsTable values('���������ྻϴ��Һ',38.50,50.40,120,61,'����/����',5,240)
insert into GoodsTable values('����ֲ�����ϴ��Һ',46.50,49.00,400,61,'����/����',5,450)
insert into GoodsTable values('����ϴ�·�',8.50,12.00,360,62,'��Ч��ø508g ǿЧȥ�������������',5,1140)
--�����Ʒ
insert into GoodsTable values('����ϴ�ྫ',4.50,6.00,1400,65,'ȥ�Ͳ����� ���ϴ�ྫ500gƿװ',4,456)
insert into GoodsTable values('����ϴ�ྫ',16.80,19.00,860,65,'��ˮȥ��1.5kg��ϴ��',4,620)
insert into GoodsTable values('�������� ���Һ',6.80,9.00,560,66,'ȥ�ۣ�����ŷ���Ƚ���������Ч��࣬�Ҳ�����Ͱ���档',4,468)
insert into GoodsTable values('��¶ʿ����;�³�������Һ',30.90,32.00,450,67,'�ɷ��º� �������� �Ҿ����� ����Ʒ����',4,128)
--��ͥ���
insert into GoodsTable values('��ɫ���������� ',2.50,3.00,1500,70,'�������ϴ� ����� ��30*45mm�� ����30��װ/��',4,150)
insert into GoodsTable values('���ٷ�������ˮ�����ϰ� ',45.50,55.00,100,70,'��ѹ��ˮ�����ϰ�����ϴ��ˮ ����',4,500)
insert into GoodsTable values('ǹ���������� ',4.50,5.00,1500,71,'10����������������������� ������',5,480)
--����ʳƷ
insert into GoodsTable values('����ʱ�⺣̦ ',15.50,25.00,600,72,'',4,40)
insert into GoodsTable values('ϲ֮�ɹ��� ',12.50,20.00,1400,73,'Բ�㵱�Ա����',4,5000)
--�������
insert into GoodsTable values('��ͱ��� ',9.50,12.00,120,74,'����ζ�ı���',4,130)
--������ʳ
insert into GoodsTable values('��ʦ������ţ���� ',3.50,4.00,800,79,'',4,1200)
insert into GoodsTable values('��ʦ����̳����� ',3.50,4.00,690,79,'��ζ',5,1244)
--����
insert into GoodsTable values('���� ',1.50,2.00,520,83,'��ζ',5,610)
insert into GoodsTable values('�ɿڿ��� ',3.50,4.00,450,84,'�úȵ�ζ��',5,400)
insert into GoodsTable values('��ţ ',3.50,4.00,500,85,'������������������',5,1050)
--������ϲ�ѯ
select * from GoodsTable t1,GoodsPhoto t2,TypeTable t3 where t1.GoodsID=t2.GoodsID and t1.TID=t3.TypeID

select * from GoodsTable where TID in(
select TypeID from TypeTable where TID in(select TypeID from TypeTable
 where TID=(select TypeID from TypeTable where TypeID=4))
)




go

--��Ʒ��ϢͼƬ��ϵ��
create table GoodsPhoto(
RID int primary key identity(1,1),--id
GoodsID int foreign key references GoodsTable(GoodsID),--��Ʒid�������
PhotoPath nvarchar(50) not null,--ͼƬ·��
)
insert into GoodsPhoto values(1,'��������_1.jpg'),(1,'��������_2.jpg'),
(2,'���޷�_1.jpg'),(2,'���޷�_2.jpg'),(2,'���޷�_3.jpg'),(2,'���޷�_4.jpg'),(2,'���޷�_5.jpg'),(2,'���޷�_6.jpg'),
(3,'��֯��_1.jpg'),(4,'��Ь_1.jpg'),(4,'����Ь_1.jpg'),(5,'���˶�Ь_1.jpg'),(6,'Ƥ��_1.jpg'),(7,'Ů����_1.jpg'),
(7,'Ů����_2.jpg'),(8,'Ůʿ��װ_1.jpg'),(9,'ŮЬ_1.jpg'),(10,'ͯװ_1.jpg'),(10,'ͯװ_2.jpg'),(11,'��������װ_1.jpg'),
(12,'�๦��һ���_1.jpg'),(13,'��ӡ��_1.jpg'),(14,'��ӡ��_2.jpg'),(14,'��ӡ��_3.jpg'),(15,'��ӡ��_4.jpg'),
(16,'���չ�_1.jpg'),(17,'���ñ��չ�_1.jpg'),(18,'���ϻ�_1.jpg'),(19,'�칫��ֽ_1.jpg'),(20,'�ʼǱ�_1.jpg'),
(21,'������_1.jpg'),(22,'��Ϸ��_1.jpg'),(23,'��Ͳֽ_1.jpg'),(24,'��ֽ_1.jpg'),(25,'ϴ��Һ_1.jpg'),(26,'ϴ��Һ_2.jpg'),
(27,'ϴ�·�_1.jpg'),(28,'ϴ�ྫ_1.jpg'),(28,'ϴ�ྫ_2.jpg'),(29,'ϴ�ྫ_3.jpg'),(30,'��޼�_1.jpg'),(31,'����Һ_1.jpg'),
(32,'������_1.jpg'),(33,'�ϰ�_1.jpg'),(34,'����_1.jpg'),(35,'����ʱ��_1.jpg'),(36,'����_1.jpg'),(37,'����_1.jpg'),
(38,'����ţ����_1.jpg'),(39,'��̳_1.jpg'),(40,'����_1.jpg'),(41,'�ɿڿ���_1.jpg'),(42,'��ţ_1.jpg')
go
--���ﳵ��
create table ShoppingCartTable(
CartID int primary key identity(1,1),--���ﳵid
UserID int foreign key references UserInfo(UserID),--�û�id
GoodsID int foreign key references GoodsTable(GoodsID),--��Ʒid
ShoppingNum int default(1) ,--��������
ShoppingTime date ,--���빺�ﳵʱ��
)
select * from ShoppingCartTable
delete from ShoppingCartTable where userid=1 and GoodsID=1
insert into ShoppingCartTable values(1,1,2,'')
go

--���۱�
create table CommentTable(
CommentID int primary key identity(1,1),--����id
UserID int foreign key references UserInfo(UserID),--�û�id
GoodsID int foreign key references GoodsTable(GoodsID),--��Ʒid
CommentContent nvarchar(200) not null,--��������
CommentStarRating int,--�����Ǽ�
CommentTime date ,--����ʱ�� 
Reportingnums int default(0),--�ٱ���
)
select * from CommentTable
go

--������
create table FeedbackTable(
FeedbackID  int primary key identity(1,1),--����id
UserID int foreign key references UserInfo(UserID),--�û�id
FeedbackContent nvarchar(200) not null,--��������
FeedbackTime date ,--����ʱ��
IsDealwith int default(0) check(IsDealwith='0' or IsDealwith='1')--�Ƿ���0:δ����
)
select * from FeedbackTable
go

--������
create table OrderTable(
OrderID int primary key identity(1,1),--����id
UserID int foreign key references UserInfo(UserID),--�û�id
GoodsID int foreign key references GoodsTable(GoodsID),--��Ʒid
GoodsNum int ,--��Ʒ����
GetTime date ,--�����ύʱ��
OrderAmount decimal(18,2),--�������
IsReceiving int default(0) ,--�Ƿ��ջ�(0:δ�ջ���1:ȷ���ջ�)
IsComment int default(0),--�Ƿ����ۣ�����(0:δ���ۣ�1��������)
)
select * from OrderTable
insert into OrderTable values(1,1,5,'',12.00,0,0)
update OrderTable set IsReceiving=1 where OrderID=1

--�ղر�
create table CollectionTable(
CollectionID int primary key identity(1,1),--�ղر�
UserID int foreign key references UserInfo(UserID),--�û�id
GoodsID int foreign key references GoodsTable(GoodsID),--��Ʒid
IsCollection int default(1) check(IsCollection=1 or IsCollection=0),--�Ƿ��ղأ�0Ϊ��,1Ϊ�ǣ�
)
select * from CollectionTable where userid=1 and GoodsID=1
insert into CollectionTable values(1,25,1)
update CollectionTable set IsCollection=0 where CollectionID=1

--�����
create table NoticeTable(
NoticeID int primary key identity(1,1),--����id
UserID int foreign key references UserInfo(UserID),--�û�id(���)
NoticeTitle nvarchar(50) not null,--�������
NoticeContent nvarchar(200) not null,--��������
NoticeTime date default(getdate()),--����ʱ��
IsLook int default(0) check(IsLook=0 or IsLook=1),--�û��Ƿ�鿴(0Ϊ��)
)
drop table NoticeTable
insert into NoticeTable values(null,'��վ����֪ͨ','�µ���վ�ֹ��̳�Ҫ�����ˣ�','2020-07-01',1)

