create database rdm_database;

use rdm_database;

/*會員表*/
create table Member(
    Id varchar(50) primary key comment '會員資料編號 uuid',
    name varchar(50) null,
    email varchar(500) null,
    account varchar(500),
    password varchar(4000),
    isAuth int default 2 comment '1登入，2登出',
    isDelivery int default 2 comment '1外送員，2非外送員',
    lineUid varchar(200) null,
    isUse  int default 1 comment '1使用,2停用',
    createdDate datetime,
    deletedDate datetime null,
    updatedDate datetime null
);

/*廠商表*/
create table Client(
    Id varchar(50) primary key comment '廠商資料編號 uuid',
    name varchar(50) null,
    number varchar(8) null comment '統一編號',
    address varchar(500),
    category int comment '服務類型',
    isUse int default 1 comment '1使用,2停用',
    createdDate datetime,
    deletedDate datetime null,
    updatedDate datetime null
);

/*廠商管理員表*/
create table Admin(
    Id varchar(50) primary key comment '會員資料編號 uuid',
    clientId varchar(50),
    name varchar(50) null,
    email varchar(500) null,
    account varchar(500),
    password varchar(4000),
    isAuth int default 2 comment '1登入，2登出',
    lineUid varchar(200) null,
    payment text comment '陣列型態，可使用的結帳模式',
    isUse int default 1 comment '1使用,2停用',
    createdDate datetime,
    deletedDate datetime null,
    updatedDate datetime null
);

/*權限類型*/
create table Prenission(
    Id int primary key,
    name varchar(50) null,
    isUse int default 1 comment '1使用,2停用'
);

/*訂單表*/
create table Order_table(
    Id varchar(50) primary key comment '訂單資料編號 uuid',
    clientId varchar(50),
    productId text comment '產品編號 陣列', 
    memberId varchar(50),
    email varchar(500) null,
    deliveryId varchar(50) null,
    price text comment '產品價格 陣列',
    joiner text null comment '團購用 名稱備註',
    total decimal comment '總金額',
    paymentId int,
    address varchar(255) null,
    pickerTime datetime null,
    remark text null comment '備註',
    createdDate datetime,
    deletedDate datetime null,
    updatedDate datetime null
);

/*支付選項*/
create table Payment(
    Id int primary key,
    name varchar(50) null,
    isUse int default 1 comment '1使用,2停用'
);

/*產品表*/
create table Product(
    Id varchar(50) primary key comment '商品資料編號 uuid',
    name varchar(50) null,
    adminId varchar(50) null,
    Introduction text null comment '產品說明 ',
    qty decimal null,
    isUse int default 1 comment '1:使用;2:停用',
    createdDate datetime,
    deletedDate datetime null,
    updatedDate datetime null
);

/*優惠設定表*/
create table coupon(
    Id varchar(50) primary key comment '優惠券資料編號 uuid',
    name varchar(50) null,
    code varchar(50) null,
    member varchar(50) null,
    deadline datetime null,
    rule text null comment 'json',
    isUse int default 1 comment '1:使用;2:停用',
    createdDate datetime,
    deletedDate datetime null,
    updatedDate datetime null
);







