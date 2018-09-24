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
CREATE TABLE dbo.dialog_access
(
	accessID				UNIQUEIDENTIFIER			UNIQUE		 NONCLUSTERED 			NOT NULL	DEFAULT (NEWID()),
	accessAppID				INT																NOT NULL	REFERENCES dialog_Application(AppID),
	accessLoginID			UNIQUEIDENTIFIER												NOT NULL	REFERENCES dialog_Login(LoginID),
	accessLoginAppID		INT																NULL		REFERENCES dialog_Application(AppID),	
	accessOK				BIT																NOT NULL,
	accessFunction			NVARCHAR(100)													NOT NULL,
	accessData				NVARCHAR(2000)													NULL,
	InsertedDate			DATETIME														NOT NULL	DEFAULT (getdate())
	PRIMARY KEY (accessID)
);

SELECT * FROM dialog_access