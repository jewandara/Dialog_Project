----========================================================================----
----						******@@@@@@@@*******							----
----						****@@@@O@@@@@@@*****							----
----						***@@@@@@@@@@@@@@****							----
----						**@@@@********@@@@***							----
----						**@@@**********@@@**O							----
----						**@@*******O****@@***							----
----						***@************@****							----
----						O**@****O*******@****							----
----						****@**********@*****							----
----						*****O********@******							----
----						******@******@*******							----
----						*******O@@@@@********							----
--------------------------------------------------------------------------------
----		Title		:	DIALOG APPLICATION								----
----		Project		:	DIALOG COMPLAINT								----
----		Author      :	J. R. M. Jeewandara								----
----		Contact     :	+947 7363 2682									----
----		Email       :	jewandara@gmail.com								----
----		Email       :	jewandara@yahoo.com								----
----		Create date :	September  01, 2015								----
----		Description :	This is Table of dbo.dialog_Application			----
----		Publisher	:	Access Network Planning, 						----
----						Western North Region, 							----
----						Dialog Axiata PLC,								----
----========================================================================----
CREATE TABLE dbo.dialog_Complaint
(
	ComplaintID				UNIQUEIDENTIFIER			UNIQUE		 NONCLUSTERED 			NOT NULL	DEFAULT (NEWID()),
	CustID					UNIQUEIDENTIFIER												NOT NULL	REFERENCES dialog_Customer(CustID),
	Longitude				NVARCHAR(50)													NOT NULL,
	Latitude				NVARCHAR(50)													NOT NULL,
	ComplaintTitle			NVARCHAR(100)													NULL,
	ComplaintData			NVARCHAR(2000)													NULL,
	ComplaintType			NVARCHAR(5)														NULL,
	MessageTime				NVARCHAR(100)													NULL,
	ComplaintView			BIT																NULL,
	InsertedDate			DATETIME														NOT NULL,
	PRIMARY KEY (ComplaintID)
);
