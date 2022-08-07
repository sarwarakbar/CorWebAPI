--DELETE RECORDS FROM PRIMARY KEY TABLE & FOREIGN KEY TABLE SIMULTANEOUSLY

create table tblCustomer
(
C_ID int primary key,
C_Name varchar (50),
C_Address varchar (max),
City varchar(50)
);

insert into tblCustomer values(1, 'Samir', 'Vasant Vihar Phase-2', 'New Delhi' );
insert into tblCustomer values(2, 'Arman', 'Laxmi Nagar', 'New Delhi' );
insert into tblCustomer values(3, 'Samrita', 'Sector 22, Film City', 'Noida' );
insert into tblCustomer values(4, 'Irfan', 'Colaba', 'Mumbai' );
insert into tblCustomer values(5, 'Varun', 'Aminabad', 'Lucknow' );


create table [order]
(
Ord_Id int,
Item varchar(50),
Quantity int,
Price_Of_1 int,
C_id int foreign key references tblCustomer(C_ID)
);

select * from tblCustomer
select * from [order]

insert into [order] values(111, 'Hard Disk', 2, 500, 3);
insert into [order] values(222, 'RAM', 1, 300, 1);
insert into [order] values(333, 'KeyBoard', 3, 400, 4);
insert into [order] values(444, 'Mouse', 2, 200, 2);
insert into [order] values(555, 'Speaker', 1, 800, 2);

delete from tblCustomer where C_ID=7;
update tblCustomer set C_ID=7 where C_ID=2;
--Deleted the foreign key
alter table [order] drop constraint [FK__order__C_id__147C05D0];

--Added New Foreign Key and Cascade for deleting, updating the records
alter table [order] add constraint FK_Customer_Order_Cascade
foreign key (C_id) references tblCustomer (C_ID)
on delete cascade
on update cascade;