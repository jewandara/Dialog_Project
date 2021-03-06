


----493A9DBF-D64E-4F7D-9D89-7F0D6E04C1E2 ANDroid
----51940474-52B8-4FEA-AF1D-6316D22F1ADC  win
----11B71E63-B5E1-4841-A5D4-334DE069B05B sanuka
----79B8242B-22A1-4AE8-B52B-D6A298B17AE2 testing
---========================================================================----
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
CREATE TABLE dbo.dialog_Application
(
	AppID						INT				IDENTITY(1,1)	UNIQUE		NOT NULL,
	AppProductKey				VARBINARY(MAX)								NOT NULL,
	AppName						nvarchar(17)								NOT NULL,
	AppAuthor					nvarchar(25)								NOT NULL,
	AppAuthorFName				nvarchar(50)								NOT NULL,
	AppContact					nvarchar(20)								NULL,
	AppEmail					nvarchar(50)								NULL,
	AppDedicatedUserName		nvarchar(50)								NOT NULL,
	AppDedicatedUserContact		nvarchar(15)								NOT NULL,
	AppDedicatedUserEmail		nvarchar(50)								NULL,
	AppDedicatedRegion 			nvarchar(50)								NOT NULL,
	AppWelcome					nvarchar(50)								NULL,
	AppDiscription				nvarchar(100)								NULL,
	AppCodeImage				nvarchar(300)								NOT NULL,
	AppVersion					nvarchar(10)								NOT NULL,
	AppIncortCodeKeyCount		int											NOT NULL,
	AppUserCount				int											NOT NULL,
	AppIncortPassWordCount		int											NOT NULL,
	AppPassKey					VARBINARY(MAX)								NOT NULL,
	AppActivate					bit											NOT NULL,
	AppActivateDate				datetime									NOT NULL,
	AppLastLoginDate			datetime									NOT NULL,
	PRIMARY KEY (AppID)
);


--CREATE SYMMETRIC KEY KeyPass				
--WITH	ALGORITHM	=	DESX
--		ENCRYPTION	BY	PASSWORD	=	'DIALOG@Application.CODE';

/*--------------------------------------------------------------------*/
--CREATE SYMMETRIC KEY AppKeyPass				
--WITH	ALGORITHM	=	DESX
--		ENCRYPTION	BY	PASSWORD	=	'1990-1991-0405-1029';
/*--------------------------------------------------------------------*/

ALTER PROCEDURE dbo._sp_dialog_Application_Activate
	@AppDedicatedUserName			nvarchar(50),
	@AppDedicatedUserContact		nvarchar(15),
	@AppDedicatedUserEmail			nvarchar(50),
	@AppDedicatedRegion				nvarchar(50)
AS
BEGIN
	OPEN SYMMETRIC KEY AppKeyPass
		DECRYPTION	BY	PASSWORD	= '1990-1991-0405-1029';	
		DECLARE @encrypted_AppCode VARBINARY(MAX);
		DECLARE @KEYX nvarchar(60)SET @KEYX = 'DIALOG@Application.CODE'
		SET @encrypted_AppCode = EncryptByKey(Key_GUID('AppKeyPass'), @KEYX);
		DECLARE @encrypted_AppProductKey VARBINARY(MAX);
		DECLARE @AppProductKey nvarchar(50) SET @AppProductKey = newID();
		SET @encrypted_AppProductKey = EncryptByKey(Key_GUID('AppKeyPass'), @AppProductKey);			
	CLOSE SYMMETRIC KEY AppKeyPass


	INSERT	INTO dbo.dialog_Application
	VALUES
	(
		@encrypted_AppProductKey,
		'Dialog Axiata PLC',
		'J. R. M. JEEWANDARA',
		'Jeewandarage Rachitha Madusanka Jeewandara',
		'+94 77 3632 682',
		'jewandara@gmail.com',
		@AppDedicatedUserName,
		@AppDedicatedUserContact,
		@AppDedicatedUserEmail,
		@AppDedicatedRegion,
		'Welcome To DIALOG GSM',
		'http://www.dialog.lk/',
		'
******@@@@@@@@******
****@@@@O@@@@@@@****
***@@@@@@@@@@@@@@***
**@@@@********@@@@**
**@@@**********@@@*O
**@@*******O****@@**
***@************@***
O**@****O*******@***
****@**********@****
*****O********@*****
******@******@******
*******O@@@@@*******
		',
		'0.0.0.A',
		0,
		0,
		0,
		@encrypted_AppCode,
		1,
		getdate(),
		getdate()
		);
		SELECT @AppProductKey AS 'KEY'
END


EXECUTE dbo._sp_dialog_Application_Activate
	@AppDedicatedUserName			= 'Windows Phone',
	@AppDedicatedUserContact		= '94777123456',
	@AppDedicatedUserEmail			= 'service@dialog.lk',
	@AppDedicatedRegion				= ''
	
	
--C8D918DB-296E-4105-A652-87A0B4BA3A65 'Western'
--B7970DE6-36D5-4E23-9F3C-9BDDCB09DC59 'Eastern'
--4DB995ED-641A-4000-80B1-47D633402DE1 'Southern'
--68B66937-B2D4-4D24-A56E-49594BF764C7 'Northern' 

--493A9DBF-D64E-4F7D-9D89-7F0D6E04C1E2 ANDroid
--51940474-52B8-4FEA-AF1D-6316D22F1ADC  win
--11B71E63-B5E1-4841-A5D4-334DE069B05B sanuka
--79B8242B-22A1-4AE8-B52B-D6A298B17AE2 testing

SELECT * FROM dialog_Application







ALTER PROCEDURE dbo._sp_dialog_SEARCH_APP_KEY
	@KEY			nvarchar(40)
AS
BEGIN
	OPEN SYMMETRIC KEY AppKeyPass
	DECRYPTION	BY	PASSWORD	= '1990-1991-0405-1029';	
	DECLARE @AppID	INT;
	SET @AppID = (SELECT AppID FROM  dialog_Application WHERE CONVERT(NVARCHAR(50),(DecryptByKey(AppProductKey))) = @KEY)
	CLOSE SYMMETRIC KEY AppKeyPass
	IF EXISTS(SELECT AppID FROM  dialog_Application WHERE AppID = @AppID )
	BEGIN
		SELECT '1' AS 'SUCESS', '0' AS 'ERROR', 'Your product key is valid.' AS 'MESAGE'
		RETURN 
	END
	ELSE
	BEGIN
		SELECT '0' AS 'SUCESS', '1' AS 'ERROR', 'Can Not Find Product Key.' AS 'MESAGE'
		RETURN 
	END
END




---==============================================================================



ALTER PROCEDURE dbo._sp_dialog_LOAD_APP_KEY
	@CODE			nvarchar(500)
AS
BEGIN
	DECLARE @AppCODE nvarchar(40) SET @AppCODE = REPLACE(@CODE,'Z0I4O0N5X','-');
	OPEN SYMMETRIC KEY AppKeyPass
	DECRYPTION	BY	PASSWORD	= '1990-1991-0405-1029';	
	DECLARE @AppID	INT;
	SET @AppID = (SELECT AppID FROM  dialog_Application WHERE CONVERT(NVARCHAR(50),(DecryptByKey(AppProductKey))) = @AppCODE)
	CLOSE SYMMETRIC KEY AppKeyPass
	IF EXISTS(SELECT AppID FROM  dialog_Application WHERE AppID = @AppID AND (AppActivate = 1) AND (AppIncortCodeKeyCount < 20) AND (AppIncortPassWordCount < 10000))
	BEGIN
		SELECT '1' AS 'SUCESS', '0' AS 'ERROR', 'Your product key is valid.' AS 'MESAGE'
		RETURN 
	END
	ELSE
	BEGIN
		SELECT '0' AS 'SUCESS', '1' AS 'ERROR', 'Can Not Find Product Key.' AS 'MESAGE'
		RETURN 
	END
END


⸀唒韱䨎⊋聟콐
---==============================================================================


CREATE PROCEDURE dbo._sp_dialog_LOAD_USER_APP_KEY
	@APPUSER			nvarchar(15)
AS
BEGIN
	IF EXISTS(SELECT CustID FROM  dialog_Customer WHERE CustNumber = @APPUSER)
	BEGIN
			IF EXISTS(SELECT LoginID FROM  dialog_Login WHERE CustID= (SELECT CustID FROM  dialog_Customer WHERE CustNumber = @APPUSER))
			BEGIN
				SELECT '1' AS 'SUCESS', '0' AS 'ERROR', 'User ID is exixts.' AS 'MESAGE'
				RETURN
			END
			ELSE
			BEGIN
				SELECT '0' AS 'SUCESS', '0' AS 'ERROR', 'Invaled Login ID is entered.' AS 'MESAGE'
				RETURN
			END
	END
	ELSE
	BEGIN
		SELECT '0' AS 'SUCESS', '1' AS 'ERROR', 'Can not find in the database.' AS 'MESAGE'
		RETURN 
	END
END

--EXECUTE dbo._sp_dialog_LOAD_APP_USER
--	@APPUSER			= '94773632682'



---==============================================================================


ALTER PROCEDURE dbo._sp_dialog_SAVE_APP_KEY
	@UserID 			nvarchar(15),
	@UserPass			nvarchar(50),
	@ProKey 			nvarchar(40)
AS
BEGIN
	OPEN SYMMETRIC KEY AppKeyPass
	DECRYPTION	BY	PASSWORD	= '1990-1991-0405-1029';	
	DECLARE	@AppID	INT SET @AppID = (SELECT AppID FROM  dialog_Application WHERE CONVERT(NVARCHAR(50),(DecryptByKey(AppProductKey))) = @ProKey)
	CLOSE SYMMETRIC KEY AppKeyPass
	IF EXISTS(SELECT AppID FROM  dialog_Application WHERE AppID = @AppID)
	BEGIN
		IF EXISTS(SELECT AppID FROM  dialog_Application WHERE AppID = @AppID AND (AppActivate = 1) AND (AppIncortCodeKeyCount < 20) AND (AppIncortPassWordCount < 10000))
		BEGIN
			IF EXISTS(SELECT LoginID FROM  dialog_Login WHERE CustID = (SELECT CustID FROM  dialog_Customer WHERE CustNumber = @UserID))
			BEGIN
				OPEN SYMMETRIC KEY KeyPass
				DECRYPTION	BY	PASSWORD	= 'DIALOG@Application.CODE';
				IF EXISTS(SELECT LoginID FROM  dialog_Login WHERE CustID = (SELECT CustID FROM  dialog_Customer WHERE CustNumber = @UserID) AND (CONVERT(NVARCHAR(50),(DecryptByKey(LoginPass))) = @UserPass) AND (FaultPWCount < 10))
				BEGIN
					IF EXISTS(SELECT LoginID FROM  dialog_Login WHERE CustID = (SELECT CustID FROM  dialog_Customer WHERE CustNumber = @UserID) AND (((CONVERT(NVARCHAR(50),(DecryptByKey(LoginType)))) = 'ADMIN')  OR ((CONVERT(NVARCHAR(50),(DecryptByKey(LoginType)))) = 'USER')))
					BEGIN
						UPDATE dialog_Login
						SET FaultPWCount	=	0
						WHERE LoginID		=	(SELECT LoginID FROM  dialog_Login WHERE CustID = (SELECT CustID FROM  dialog_Customer WHERE CustNumber = @UserID));	
						UPDATE dialog_Application
						SET AppIncortCodeKeyCount	=	0
						WHERE AppID		=	@AppID;	
						SELECT '1' AS 'SUCESS', '0' AS 'ERROR', 'Access granted.' AS 'MESAGE', REPLACE(@ProKey,'-','@') AS 'KEYCODE'
						RETURN 
					END
					ELSE
					BEGIN
						UPDATE dialog_Login
						SET FaultPWCount	=	(FaultPWCount + 1)
						WHERE LoginID		=	(SELECT LoginID FROM  dialog_Login WHERE CustID = (SELECT CustID FROM  dialog_Customer WHERE CustNumber = @UserID));	
						UPDATE dialog_Application
						SET AppIncortPassWordCount  =	(AppIncortPassWordCount + 1)
						WHERE AppID		=	@AppID;	
						SELECT '0' AS 'SUCESS', '1' AS 'ERROR', 'Anauthorized Access.' AS 'MESAGE', 'After 9 times, Login ID will be is temporarily disabled.' AS 'MESAGE2'
						RETURN 
					END
				END
				ELSE
				BEGIN
					UPDATE dialog_Login
					SET FaultPWCount = (FaultPWCount + 1)
					WHERE LoginID = (SELECT LoginID FROM  dialog_Login WHERE CustID = (SELECT CustID FROM  dialog_Customer WHERE CustNumber = @UserID));
					SELECT '0' AS 'SUCESS', '1' AS 'ERROR', 'Invalid User ID or Password.' AS 'MESAGE','' AS 'MESAGE2'
					RETURN 
				END
				CLOSE SYMMETRIC KEY KeyPass
			END
			ELSE
			BEGIN
				SELECT '0' AS 'SUCESS', '1' AS 'ERROR', 'Invalid User ID or Password.' AS 'MESAGE','' AS 'MESAGE2'
				RETURN 		
			END
		END
		ELSE
		BEGIN
			UPDATE dialog_Application
			SET AppIncortCodeKeyCount  = AppIncortCodeKeyCount + 1
			WHERE AppID = @AppID;
			SELECT '0' AS 'SUCESS', '1' AS 'ERROR', 'Anauthorized Access.' AS 'MESAGE', 'Product Key is temporarily disabled. Call the administrator.' AS 'MESAGE2'
			RETURN 
		END
	END
	ELSE
	BEGIN
		UPDATE dialog_Application
		SET AppIncortCodeKeyCount  = AppIncortCodeKeyCount + 1
		WHERE AppID = @AppID;
		SELECT '0' AS 'SUCESS', '1' AS 'ERROR', 'Anauthorized Access.' AS 'MESAGE', '' AS 'MESAGE2'
		RETURN 
	END
END






---==============================================================================
---==============================================================================
---==============================================================================
---==============================================================================


ALTER PROCEDURE dbo._sp_dialog_LOGIN_APP_KEY
	@APPKEY 			nvarchar(500),
	@UserNumber 		nvarchar(15),
	@UserPass			nvarchar(50)
AS
BEGIN
	DECLARE @AppCODE nvarchar(40) SET @AppCODE = REPLACE(@APPKEY,'Z0I4O0N5X','-');
	OPEN SYMMETRIC KEY AppKeyPass
	DECRYPTION	BY	PASSWORD	= '1990-1991-0405-1029';	
	DECLARE	@AppID	INT SET @AppID = (SELECT AppID FROM  dialog_Application WHERE CONVERT(NVARCHAR(50),(DecryptByKey(AppProductKey))) = @AppCODE)
	CLOSE SYMMETRIC KEY AppKeyPass
	IF EXISTS(SELECT AppID FROM  dialog_Application WHERE AppID = @AppID AND (AppActivate = 1) AND (AppIncortCodeKeyCount < 20) AND (AppIncortPassWordCount < 10000))
	BEGIN
		IF EXISTS(SELECT LoginID FROM  dialog_Login WHERE CustID = (SELECT CustID FROM  dialog_Customer WHERE CustNumber = @UserNumber))
		BEGIN
			DECLARE @LOGID UNIQUEIDENTIFIER SET @LOGID = (SELECT LoginID FROM  dialog_Login WHERE CustID = (SELECT CustID FROM  dialog_Customer WHERE CustNumber = @UserNumber));
			OPEN SYMMETRIC KEY KeyPass
			DECRYPTION	BY	PASSWORD	= 'DIALOG@Application.CODE';
			IF EXISTS(SELECT LoginID FROM  dialog_Login WHERE LoginID = @LOGID AND (IsLocked = 0) AND (FaultPWCount < 10))
			BEGIN
				IF EXISTS(SELECT LoginID FROM  dialog_Login WHERE LoginID = @LOGID AND (CONVERT(NVARCHAR(50),(DecryptByKey(LoginPass))) = @UserPass))
				BEGIN
					IF EXISTS(SELECT LoginID FROM  dialog_Login WHERE LoginID = @LOGID AND (((CONVERT(NVARCHAR(50),(DecryptByKey(LoginType)))) = 'ADMIN')  OR ((CONVERT(NVARCHAR(50),(DecryptByKey(LoginType)))) = 'USER')))
					BEGIN
						CLOSE SYMMETRIC KEY KeyPass
						DECLARE @LOGAPPID INT SET @LOGAPPID = (SELECT AppID FROM  dialog_Login WHERE LoginID = @LOGID);
						IF EXISTS(SELECT AppID FROM  dialog_Application WHERE AppID = @LOGAPPID AND (AppActivate = 1) AND (AppIncortCodeKeyCount < 20) AND (AppIncortPassWordCount < 10000))
						BEGIN
							IF(@LOGAPPID = @AppID) INSERT INTO dbo.dialog_access VALUES	(NEWID(), @AppID, @LOGID, @LOGAPPID,1,'LOGIN_APPLICATION/SYSTEM','user login to system',CONVERT(VARCHAR(20),dateadd(HOUR, 12, dateadd(MINUTE, 30, getdate()))));
							ELSE INSERT	INTO dbo.dialog_access VALUES	(NEWID(), @AppID, @LOGID, @LOGAPPID,1,'LOGIN_APPLICATION/OTHER','user login to other system',CONVERT(VARCHAR(20),dateadd(HOUR, 12, dateadd(MINUTE, 30, getdate()))));
							UPDATE dialog_Login
							SET FaultPWCount	=	0
							WHERE LoginID		=	@LOGID;
							UPDATE dialog_Application
							SET AppLastLoginDate  =	CONVERT(VARCHAR(20),dateadd(HOUR, 12, dateadd(MINUTE, 30, getdate())))
							WHERE AppID		=	@AppID;	
							SELECT '1' AS 'SUCESS', '0' AS 'ERROR', @AppID AS 'USER_LOGIN_APP_NUMBER', @LOGAPPID AS 'USER_OWN_LOGIN_APP_NUMBER', @LOGID AS 'USER_LOGIN_ID', @UserNumber AS 'USER_NUMBER'
						END
						ELSE
						BEGIN
							IF(@LOGAPPID = @AppID) INSERT INTO dbo.dialog_access VALUES	(NEWID(), @AppID, @LOGID, @LOGAPPID,0,'LOGIN_APPLICATION/STEMER','user system is deabled',CONVERT(VARCHAR(20),dateadd(HOUR, 12, dateadd(MINUTE, 30, getdate()))));
							ELSE INSERT	INTO dbo.dialog_access VALUES	(NEWID(), @AppID, @LOGID, @LOGAPPID,0,'LOGIN_APPLICATION/OTHER','user other system is deabled',CONVERT(VARCHAR(20),dateadd(HOUR, 12, dateadd(MINUTE, 30, getdate()))));
							
							UPDATE dialog_Login
							SET FaultPWCount	= (FaultPWCount + 1)
							WHERE LoginID		=	@LOGID;
							
							UPDATE dialog_Application
							SET AppIncortPassWordCount  =	(AppIncortPassWordCount + 1)
							WHERE AppID	= @LOGAPPID;	
							SELECT '0' AS 'SUCESS', '1' AS 'ERROR', 'User system is deabled.' AS 'MESAGE', 'After 9 times, Login ID will be is temporarily disabled.' AS 'MESAGE2'
							RETURN 
						END
					END
					ELSE
					BEGIN
						CLOSE SYMMETRIC KEY KeyPass
						INSERT	INTO dbo.dialog_access
						VALUES	(NEWID(), @AppID, @LOGID, @AppID,0,'LOGIN_APPLICATION/SYSTEM','anauthorized user login to system',CONVERT(VARCHAR(20),dateadd(HOUR, 12, dateadd(MINUTE, 30, getdate()))));
						UPDATE dialog_Login
						SET FaultPWCount	= (FaultPWCount + 1)
						WHERE LoginID		=	@LOGID;	
						UPDATE dialog_Application
						SET AppIncortPassWordCount  =	(AppIncortPassWordCount + 1)
						WHERE AppID	= @AppID;	
						SELECT '0' AS 'SUCESS', '1' AS 'ERROR', 'Anauthorized Access.' AS 'MESAGE', 'After 9 times, Login ID will be is temporarily disabled.' AS 'MESAGE2'
						RETURN 
					END
				END
				ELSE
				BEGIN
					CLOSE SYMMETRIC KEY KeyPass
					UPDATE dialog_Login
					SET FaultPWCount = (FaultPWCount + 1)
					WHERE LoginID = @LOGID;
					SELECT '0' AS 'SUCESS', '1' AS 'ERROR', 'Invalid Password.' AS 'MESAGE','' AS 'MESAGE2'
					RETURN 
				END
			END
			ELSE
			BEGIN
				CLOSE SYMMETRIC KEY KeyPass
				SELECT '0' AS 'SUCESS', '1' AS 'ERROR', 'User ID is temporarily disabled.' AS 'MESAGE','' AS 'MESAGE2'
				RETURN
			END
		END
		ELSE
		BEGIN
			SELECT '0' AS 'SUCESS', '1' AS 'ERROR', 'Invalid User ID.' AS 'MESAGE','' AS 'MESAGE2'
			RETURN 		
		END
	END
	ELSE
	BEGIN
		SELECT '0' AS 'SUCESS', '1' AS 'ERROR', 'Anauthorized Access.' AS 'MESAGE', 'Product Key is temporarily disabled. Call the administrator.' AS 'MESAGE2'
		RETURN 
	END
		
END








EXECUTE dbo._sp_dialog_SAVE_APP_KEY
	@UserID 			= '94773632682',
	@UserPass			= 'WOsop123',
	@ProKey 			= 'C8D918DB-296E-4105-A652-87A0B4BA3A65'     

DC07BD80-BA74-441A-ABF9-D1D9554B48FA
--SELECT * FROM dialog_Application
--SELECT * FROM dbo.dialog_Login



-----------------------------------------------------------------------
	DECLARE @LOGID UNIQUEIDENTIFIER SET @LOGID = (SELECT LoginID FROM  dialog_Login WHERE CustID = (SELECT CustID FROM  dialog_Customer WHERE CustNumber = '94773632682'));
	OPEN SYMMETRIC KEY KeyPass
	DECRYPTION	BY	PASSWORD	= 'DIALOG@Application.CODE';
	DECLARE @typeE NVARCHAR(max) set @typeE = (SELECT LoginType FROM dbo.dialog_Login WHERE LoginID = @LOGID)
	DECLARE @type NVARCHAR(50) set @type = DecryptByKey(@typeE)
	SELECT @type
	CLOSE SYMMETRIC KEY KeyPass
--------------------------------------------------------------------------
	
-----------------------------------------------------------------------
ALTER FUNCTION LOGIN_APP_ID_OK(@SYSAPPID INT, @USERAPPID INT, @USERNUMBER NVARCHAR(15), @USERLOGID UNIQUEIDENTIFIER)
RETURNS BIT
AS
BEGIN
	IF(@USERAPPID = (SELECT AppID FROM  dialog_Application WHERE (AppActivate = 1) AND (AppIncortCodeKeyCount < 20)  AND AppID = (SELECT AppID FROM  dialog_Login WHERE (LoginID = @USERLOGID) AND (CustID = (SELECT CustID FROM  dialog_Customer WHERE CustNumber = @USERNUMBER)))))
	BEGIN
		IF EXISTS(SELECT AppID FROM  dialog_Application WHERE (AppActivate = 1) AND (AppIncortCodeKeyCount < 20) AND (AppIncortPassWordCount < 10000) AND (AppID = @SYSAPPID))
		BEGIN
			RETURN 1;
		END
		ELSE 
		BEGIN
			RETURN 0;
		END
	END
	ELSE 
	BEGIN
		RETURN 0;
	END
	RETURN 0
END


SELECT dbo.LOGIN_APP_ID_OK(7,8,'94773632682','4DFDE317-277D-473C-B24D-60A8B6A053B2') AS 'ffdg'

SELECT * FROM  dbo.dialog_Login WHERE CustID = (SELECT CustID FROM dbo.dialog_Customer WHERE CustNumber = '94773632682')

