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
CREATE TABLE dbo.dialog_Customer
(
	CustID					UNIQUEIDENTIFIER			UNIQUE		 NONCLUSTERED 			NOT NULL	DEFAULT (NEWID()),
	CustNumber				NVARCHAR(15)													NOT NULL,				
	CustName				NVARCHAR(150)													NULL,
	CustGender				NVARCHAR(8)														NULL,
	CustEmail				NVARCHAR(50)													NULL,
	CustAddresOne			NVARCHAR(500)													NULL,
	CustAddresTwo			NVARCHAR(500)													NULL,
	CalTimeIN				NVARCHAR(20)													NULL,
	CalTimeOut				NVARCHAR(20)													NULL,
	CustType				NVARCHAR(5)														NOT NULL,
	encriID					UNIQUEIDENTIFIER												NOT NULL	DEFAULT (NEWID()),
	
	InsertedDate			DATETIME														NOT NULL,
	ModifiedDate			DATETIME														NOT NULL	DEFAULT (getdate()),
	PRIMARY KEY (CustID)
);


SELECT * FROM dialog_Customer