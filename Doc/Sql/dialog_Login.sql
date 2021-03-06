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
CREATE TABLE dbo.dialog_Login
(
	LoginID					UNIQUEIDENTIFIER			UNIQUE		 NONCLUSTERED 			NOT NULL	DEFAULT (NEWID()),
	AppID					INT																NOT NULL	REFERENCES dialog_Application(AppID),
	CustID					UNIQUEIDENTIFIER												NOT NULL	REFERENCES dialog_Customer(CustID),
	LoginPass				nvarchar(MAX)													NOT NULL,
	LoginType				nvarchar(MAX)													NOT NULL,
	IsLocked				BIT																NOT NULL,
	FaultPWCount			INT																NOT NULL,
	PWChangeType			nvarchar(4)														NOT NULL,

	LastPWChangedDate		DATETIME														NOT NULL,
	InsertedDate			DATETIME														NOT NULL,
	ModifiedDate			DATETIME														NOT NULL	DEFAULT (getdate()),
	PRIMARY KEY (LoginID)
);



--------------------------------------------------------------------------------
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
--------------------------------------------------------------------------------
--ALTER PROCEDURE dbo._sp_dialog_New_Login
--	@KEY					NVARCHAR(50),
--	@CustNumber				NVARCHAR(15),
--	@LoginPass				NVARCHAR(50),
--	@LoginType				NVARCHAR(15),
--	@CustName				NVARCHAR(150),		
--	@CustGender				NVARCHAR(8),
--	@CustEmail				NVARCHAR(80),
--	@CustAddresOne			NVARCHAR(500),
--	@CustAddresTwo			NVARCHAR(500),
--	@CalTimeIN				NVARCHAR(20),
--	@CalTimeOut				NVARCHAR(20)
--AS
--BEGIN
--	IF (@KEY = '')
--	BEGIN
--		IF EXISTS(SELECT CustID FROM  dialog_Customer WHERE CustNumber = @CustNumber)
--		BEGIN
--			SELECT '0' AS 'SUCESS', 'M' AS 'ERROR', 'Login Is Exixts' AS 'MESAGE'
--			RETURN
--		END
--		ELSE
--		BEGIN
--			INSERT	INTO dbo.dialog_Customer
--			VALUES	(NEWID(),@CustNumber,@CustName,@CustGender,@CustEmail,@CustAddresOne,@CustAddresTwo,@CalTimeIN,@CalTimeOut,'SMS', newid(),getdate(),getdate());
--			SELECT 'M' AS 'SUCESS', '0' AS 'ERROR','New User Was Inserted' AS 'MESAGE'
--			RETURN 
--		END		
--	END
--	ELSE
--	BEGIN
--		OPEN SYMMETRIC KEY AppKeyPass
--		DECRYPTION	BY	PASSWORD	= '1990-1991-0405-1029';	
--		DECLARE		@AppID	INT;
--		SET @AppID = (SELECT AppID FROM  dialog_Application WHERE CONVERT(NVARCHAR(50),(DecryptByKey(AppProductKey))) = @KEY)
--		CLOSE SYMMETRIC KEY AppKeyPass
--		IF EXISTS(SELECT AppID FROM  dialog_Application WHERE AppID = @AppID)
--		BEGIN
--			OPEN SYMMETRIC KEY KeyPass
--			DECRYPTION	BY	PASSWORD	= 'DIALOG@Application.CODE';
--			DECLARE @encrypted_LoginPass VARBINARY(MAX);
--			DECLARE @encrypted_LoginType VARBINARY(MAX)
--			IF (@LoginType = 'USER')
--			BEGIN
				
--				SET @encrypted_LoginPass = EncryptByKey(Key_GUID('KeyPass'), @LoginPass);
--				SET @encrypted_LoginType = EncryptByKey(Key_GUID('KeyPass'), @LoginType);
--			END
--			ELSE IF (@LoginType = 'CUST')
--			BEGIN
--				SET @encrypted_LoginPass = EncryptByKey(Key_GUID('KeyPass'), @LoginPass);
--				SET @encrypted_LoginType = EncryptByKey(Key_GUID('KeyPass'), @LoginType);
--			END
--			ELSE
--			BEGIN
--				SELECT '0' AS 'SUCESS', '1' AS 'ERROR', 'Login Type Is Invalid' AS 'MESAGE'
--				RETURN 
--			END
--			IF EXISTS(SELECT CustID FROM  dialog_Customer WHERE CustNumber = @CustNumber)
--			BEGIN
--				IF EXISTS(SELECT CustID FROM  dialog_Login WHERE CustID = (SELECT CustID FROM  dialog_Customer WHERE CustNumber = @CustNumber))
--				BEGIN			
--					SELECT '0' AS 'SUCESS', '1' AS 'ERROR', 'Login Is Exixts' AS 'MESAGE'
--					RETURN
--				END
--				ELSE
--				BEGIN
--					DECLARE @CustID UNIQUEIDENTIFIER SET @CustID = (SELECT CustID FROM  dialog_Customer WHERE CustNumber = @CustNumber);
--					INSERT	INTO dbo.dialog_Login
--					VALUES	(NEWID(), @AppID, @CustID, @encrypted_LoginPass,@encrypted_LoginType,0,0,'NET',getdate(),getdate(),getdate());
--					UPDATE dialog_Customer
--					SET 
--						CustName		=	@CustName,		
--						CustGender		=	@CustGender,
--						CustEmail		=	@CustEmail,
--						CustAddresOne	=	@CustAddresOne,
--						CustAddresTwo	=	@CustAddresTwo,
--						CalTimeIN		=	@CalTimeIN,
--						CalTimeOut		=	@CalTimeOut,
--						CustType		=	'NET'
--					WHERE CustNumber = @CustNumber;
--					SELECT '1' AS 'SUCESS', '0' AS 'ERROR', 'User Exixts Is Updated' AS 'MESAGE'
--					RETURN 
--				END
--			END
--			ELSE
--			BEGIN
--				DECLARE @UN_CustID UNIQUEIDENTIFIER SET @UN_CustID = NEWID();
--				INSERT	INTO dbo.dialog_Customer
--				VALUES	(@UN_CustID,@CustNumber,@CustName,@CustGender,@CustEmail,@CustAddresOne,@CustAddresTwo,@CalTimeIN,@CalTimeOut,'NET', newid(),getdate(),getdate());
--				INSERT	INTO dbo.dialog_Login
--				VALUES	(NEWID(),@AppID,@UN_CustID,@encrypted_LoginPass,@encrypted_LoginType,0,0,'NET',getdate(),getdate(),getdate());
--				SELECT '1' AS 'SUCESS', '0' AS 'ERROR','New User Was Inserted' AS 'MESAGE'
--				RETURN 
--			END
--		END
--		ELSE
--		BEGIN
--			SELECT '0' AS 'SUCESS', '1' AS 'ERROR', 'Can Not Find Product Key' AS 'MESAGE'
--			RETURN 
--		END
--	END
--END	
		
		
--C8D918DB-296E-4105-A652-87A0B4BA3A65 'Western'
--B7970DE6-36D5-4E23-9F3C-9BDDCB09DC59 'Eastern'
--4DB995ED-641A-4000-80B1-47D633402DE1 'Southern'
--68B66937-B2D4-4D24-A56E-49594BF764C7 'Northern' 

	
		
EXECUTE dbo._sp_dialog_New_Login
	@KEY			= 'C8D918DB-296E-4105-A652-87A0B4BA3A65',
	@CustNumber		= '94776632687',
	@LoginPass		= 'WOsop123',
	@LoginType		= 'CUST',
	@CustName		= 'Bimal',		
	@CustGender		= 'FEMALE',
	@CustEmail		= 'jeert',
	@CustAddresOne	=  'West Weliweriya',
	@CustAddresTwo	= NULL,
	@CalTimeIN		= '3:00 PM',
	@CalTimeOut		=  '10:00PM'
	
--SELECT * FROM dbo.dialog_Login
--SELECT * FROM dbo.dialog_Customer


	
--------------------------------------------------------------------------------
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
--------------------------------------------------------------------------------

--CREATE PROCEDURE dbo._sp_dialog_Login_App
--	@CustNumber				NVARCHAR(15),	
--	@LoginPass				NVARCHAR(50)
--AS
--BEGIN
--    SET NOCOUNT ON;
--	BEGIN
--		OPEN SYMMETRIC KEY AppKeyPass
--		DECRYPTION	BY	PASSWORD	= '1990-1991-0405-1029';	
--		DECLARE		@AppID	INT;
--		SET @AppID = (SELECT AppID FROM  dialog_Application WHERE CONVERT(NVARCHAR(50),(DecryptByKey(AppProductKey))) = @KEY)
--		CLOSE SYMMETRIC KEY AppKeyPass
--		IF EXISTS(SELECT AppID FROM  dialog_Application WHERE AppID = @AppID)
--		BEGIN
--			IF EXISTS(SELECT CustNumber FROM dbo.dialog_Customer WHERE CustNumber = @CustNumber)
--			BEGIN
--				IF EXISTS(SELECT CustNumber FROM dbo.dialog_Login WHERE CustNumber = @CustNumber AND LoginPass = @LoginPass AND LoginType = 'ADMIN')
--				BEGIN
--					SELECT 'TRUE' AS 'RETURN';
--				END
--				ELSE
--				BEGIN
--					SELECT 'False' AS 'RETURN';
--				END	
--			END
--			ELSE
--			BEGIN
--				SELECT 'False' AS 'RETURN';
--			END			
--END








--EXECUTE dbo._sp_dialog_Login_App
--	@CustNumber		=  '94773632682',
--	@LoginPass		=  'jewandara@gmail.com'
	
	
