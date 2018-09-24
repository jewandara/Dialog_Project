
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
ALTER PROCEDURE dbo._sp_dialog_NEW_USER_LOGIN
	@KEY					NVARCHAR(50),
	@CustAppID				INT,
	@CustNumber				NVARCHAR(15),
	@LoginPass				NVARCHAR(50),
	@LoginType				NVARCHAR(15),
	@CustName				NVARCHAR(150),
	@CustGender				NVARCHAR(8),
	@CustEmail				NVARCHAR(80),
	@CustAddresOne			NVARCHAR(500),
	@CustAddresTwo			NVARCHAR(500),
	@CalTimeIN				NVARCHAR(20),
	@CalTimeOut				NVARCHAR(20),
	@PassChangeType			NVARCHAR(4)
AS
BEGIN
	OPEN SYMMETRIC KEY AppKeyPass
	DECRYPTION	BY	PASSWORD	= '1990-1991-0405-1029';	
	DECLARE		@AppID	INT;
	SET @AppID = (SELECT AppID FROM  dialog_Application WHERE CONVERT(NVARCHAR(50),(DecryptByKey(AppProductKey))) = @KEY)
	CLOSE SYMMETRIC KEY AppKeyPass
	IF EXISTS(SELECT AppID FROM  dialog_Application WHERE AppID = @AppID AND AppActivate = 1)
	BEGIN
		IF EXISTS(SELECT AppID FROM  dialog_Application WHERE AppID = @CustAppID AND AppActivate = 1)
		BEGIN
			DECLARE @TODAY_DATE DATETIME SET @TODAY_DATE = CONVERT(VARCHAR(20),dateadd(HOUR, 12, dateadd(MINUTE, 30, getdate())));
			OPEN SYMMETRIC KEY KeyPass
			DECRYPTION	BY	PASSWORD	= 'DIALOG@Application.CODE';
			DECLARE @encrypted_LoginPass VARBINARY(MAX);
			DECLARE @encrypted_LoginType VARBINARY(MAX)
			IF (@LoginType = 'USER')
			BEGIN
				SET @encrypted_LoginPass = EncryptByKey(Key_GUID('KeyPass'), @LoginPass);
				SET @encrypted_LoginType = EncryptByKey(Key_GUID('KeyPass'), @LoginType);
			END
			ELSE IF (@LoginType = 'CUST')
			BEGIN
				SET @encrypted_LoginPass = EncryptByKey(Key_GUID('KeyPass'), @LoginPass);
				SET @encrypted_LoginType = EncryptByKey(Key_GUID('KeyPass'), @LoginType);
			END
			ELSE
			BEGIN
				SELECT '0' AS 'SUCESS', '1' AS 'ERROR', 'Login Type Is Invalid' AS 'MESAGE'
				RETURN 
			END
			IF EXISTS(SELECT CustID FROM  dialog_Customer WHERE CustNumber = @CustNumber)
			BEGIN
				IF EXISTS(SELECT LoginID FROM  dialog_Login WHERE CustID = (SELECT CustID FROM  dialog_Customer WHERE CustNumber = @CustNumber) AND IsLocked = 0 AND (FaultPWCount < 10))
				BEGIN
					IF(@PassChangeType = 'NET')
					BEGIN
						UPDATE dialog_Customer
						SET
							CustName		=	@CustName,		
							CustGender		=	@CustGender,
							CustEmail		=	@CustEmail,
							CustAddresOne	=	@CustAddresOne,
							CustAddresTwo	=	@CustAddresTwo,
							CalTimeIN		=	@CalTimeIN,
							CalTimeOut		=	@CalTimeOut,
							ModifiedDate	=	@TODAY_DATE
						WHERE CustNumber = @CustNumber;
						UPDATE dbo.dialog_Login
						SET
							AppID			=	@CustAppID,
							LoginPass		=	@encrypted_LoginPass,
							LoginType		=	@encrypted_LoginType,
							PWChangeType	=	'NET',
							LastPWChangedDate = @TODAY_DATE,
							ModifiedDate	=	@TODAY_DATE
						WHERE CustID = (SELECT CustID FROM  dialog_Customer WHERE CustNumber = @CustNumber);
						SELECT '1' AS 'SUCESS', '0' AS 'ERROR', 'Existing user net login is updated.' AS 'MESAGE'
						RETURN
					END
					ELSE
					BEGIN
						SELECT '2' AS 'SUCESS', '0' AS 'ERROR', 'Existing user sms login.' AS 'MESAGE'
						RETURN
					END
				END
				ELSE IF EXISTS(SELECT LoginID FROM  dialog_Login WHERE CustID = (SELECT CustID FROM  dialog_Customer WHERE CustNumber = @CustNumber) AND IsLocked = 1)
				BEGIN			
					SELECT '0' AS 'SUCESS', '2' AS 'ERROR', 'Account has been disabled. Unauthorised access.' AS 'MESAGE'
					RETURN
				END
				ELSE IF EXISTS(SELECT LoginID FROM  dialog_Login WHERE CustID = (SELECT CustID FROM  dialog_Customer WHERE CustNumber = @CustNumber) AND (FaultPWCount > 10))
				BEGIN			
					SELECT '0' AS 'SUCESS', '3' AS 'ERROR', 'Account has been disabled. Too many password attempts.' AS 'MESAGE'
					RETURN
				END
				ELSE
				BEGIN
					DECLARE @CustID UNIQUEIDENTIFIER SET @CustID = (SELECT CustID FROM  dialog_Customer WHERE CustNumber = @CustNumber);
					INSERT	INTO dbo.dialog_Login
					VALUES	(NEWID(), @CustAppID, @CustID, @encrypted_LoginPass,@encrypted_LoginType,0,0,@PassChangeType,@TODAY_DATE,@TODAY_DATE,@TODAY_DATE);
					UPDATE dialog_Customer
					SET 
						CustName		=	@CustName,		
						CustGender		=	@CustGender,
						CustEmail		=	@CustEmail,
						CustAddresOne	=	@CustAddresOne,
						CustAddresTwo	=	@CustAddresTwo,
						CalTimeIN		=	@CalTimeIN,
						CalTimeOut		=	@CalTimeOut,
						CustType		=	'SYS'
					WHERE CustNumber = @CustNumber;
					SELECT '3' AS 'SUCESS', '0' AS 'ERROR', 'Exixts user is updated to system.' AS 'MESAGE'
					RETURN 
				END
			END
			ELSE
			BEGIN
				DECLARE @UN_CustID UNIQUEIDENTIFIER SET @UN_CustID = NEWID();
				INSERT	INTO dbo.dialog_Customer
				VALUES	(@UN_CustID,@CustNumber,@CustName,@CustGender,@CustEmail,@CustAddresOne,@CustAddresTwo,@CalTimeIN,@CalTimeOut,'SYS', newid(),@TODAY_DATE,@TODAY_DATE);
				INSERT	INTO dbo.dialog_Login
				VALUES	(NEWID(),@CustAppID,@UN_CustID,@encrypted_LoginPass,@encrypted_LoginType,0,0,@PassChangeType,@TODAY_DATE,@TODAY_DATE,@TODAY_DATE);
				SELECT '4' AS 'SUCESS', '0' AS 'ERROR','New user was inserted to system.' AS 'MESAGE'
				RETURN 
			END
		END
		ELSE
		BEGIN
			SELECT '0' AS 'SUCESS', '8' AS 'ERROR', 'Can not find the user application.' AS 'MESAGE'
			RETURN 
		END
	END
	ELSE
	BEGIN
		SELECT '0' AS 'SUCESS', '9' AS 'ERROR', 'Can not find product key' AS 'MESAGE'
		RETURN 
	END
END	
		

		
--EXECUTE dbo._sp_dialog_NEW_USER_LOGIN
--	@KEY			= '79B8242B-22A1-4AE8-B52B-D6A298B17AE2', test
--	@CustAppID		= 8,
--	@CustNumber		= '94773632682',
--	@LoginPass		= 'WOsop123',
--	@LoginType		= 'USER',
--	@CustName		= 'Rachitha Madusanka',		
--	@CustGender		= 'MALE',
--	@CustEmail		= 'jewandara@hotmail.com',
--	@CustAddresOne	= 'West Weliweriya',
--	@CustAddresTwo	= 'West Weliweriya',
--	@CalTimeIN		= '7:00 PM',
--	@CalTimeOut		= '10:00PM',
--	@PassChangeType	= 'SMS'

--11B71E63-B5E1-4841-A5D4-334DE069B05B sanuka

--EXECUTE _sp_DIALOG_SMS_SERVER_OPEN @KEY = '11B71E63-B5E1-4841-A5D4-334DE069B05B'








------------==========================================================--------
------------==========================================================--------
------------==========================================================--------
------------==========================================================--------
------------==========================================================--------
------------==========================================================--------


ALTER PROCEDURE dbo._sp_DIALOG_SMS_SERVER_OPEN
	@KEY			NVARCHAR(50)
AS
BEGIN
	OPEN SYMMETRIC KEY AppKeyPass
	DECRYPTION	BY	PASSWORD	= '1990-1991-0405-1029';	
	DECLARE		@AppID	INT;
	SET @AppID = (SELECT AppID FROM  dialog_Application WHERE CONVERT(NVARCHAR(50),(DecryptByKey(AppProductKey))) = @KEY)
	CLOSE SYMMETRIC KEY AppKeyPass
	IF EXISTS(SELECT AppID FROM  dialog_Application WHERE AppID = @AppID)
	BEGIN
		UPDATE dialog_Application
			SET  AppLastLoginDate = CONVERT(VARCHAR(20),dateadd(HOUR, 12, dateadd(MINUTE, 30, getdate())))
			WHERE AppID = @AppID;
		SELECT
			AppDedicatedUserContact AS 'AdminNumber', 
			'1' AS 'SUCCES',
			'0' AS 'ERROR',
			'Server product key successfully updated.' AS 'MESSAGE'
		FROM dbo.dialog_Application
		WHERE AppID = @AppID;
	END
	ELSE
	BEGIN
		SELECT
		'' AS 'AdminNumber', 
		'0' AS 'SUCCES',
		'1' AS 'ERROR',
		'Invalid product key. Contact the System Administrator.                      Jeewandara : +94 77 3632682' AS 'MESSAGE'
	END
END



-------==================================================================================================
-------==================================================================================================
-------==================================================================================================

ALTER PROCEDURE dbo._sp_DIALOG_SMS_SERVER_CHANGE_PASS
	@KEY			NVARCHAR(50),
	@USERID			NVARCHAR(15),
	@PASS			NVARCHAR(50)
AS
BEGIN
	IF (LEN(@PASS) > 5)
	BEGIN
		OPEN SYMMETRIC KEY AppKeyPass
		DECRYPTION	BY	PASSWORD	= '1990-1991-0405-1029';	
		DECLARE		@AppID	INT;
		SET @AppID = (SELECT AppID FROM  dialog_Application WHERE CONVERT(NVARCHAR(50),(DecryptByKey(AppProductKey))) = @KEY)
		CLOSE SYMMETRIC KEY AppKeyPass
		IF EXISTS(SELECT AppID FROM  dialog_Application WHERE AppID = @AppID)
		BEGIN
			IF EXISTS(SELECT LoginID FROM  dialog_Login WHERE CustID = (SELECT CustID FROM dbo.dialog_Customer WHERE CustNumber = @USERID))
			BEGIN
				OPEN SYMMETRIC KEY KeyPass
				DECRYPTION	BY	PASSWORD	= 'DIALOG@Application.CODE';
				DECLARE @encrypted_LoginPass VARBINARY(MAX);
				SET @encrypted_LoginPass = EncryptByKey(Key_GUID('KeyPass'), @PASS);
				CLOSE SYMMETRIC KEY KeyPass
				UPDATE dbo.dialog_Login
				SET 
					LoginPass		= @encrypted_LoginPass,
					PWChangeType	= 'SMS',
					ModifiedDate	= getdate()
					WHERE CustID	= (SELECT CustID FROM dbo.dialog_Customer WHERE CustNumber = @USERID);
				SELECT
					'1' AS 'SUCCES','0' AS 'ERROR','Your password is successfully updated.' AS 'MESSAGE';
				RETURN
			END
			ELSE
			BEGIN
				SELECT
					'0' AS 'SUCCES','1' AS 'ERROR','Sorry, You are not registered.' AS 'MESSAGE';
				RETURN
			END
		END
	END
	ELSE
	BEGIN
		SELECT
			'0' AS 'SUCCES','1' AS 'ERROR','Sorry, Password must be minimum 6 characters.' AS 'MESSAGE';
		RETURN
	END
END



--declare @string nvarchar(80) = 'helo677'
--select datalength(@string), LEN(@string)


-------==================================================================================================
-------==================================================================================================
-------==================================================================================================

ALTER PROCEDURE dbo._sp_DIALOG_SMS_SERVER_NEW_COMPLAINT
	@KEY			NVARCHAR(50),
	@USERID			NVARCHAR(15),
	@LOG			NVARCHAR(50),
	@LAT			NVARCHAR(50),
	@DATA			NVARCHAR(100),
	@TIME			NVARCHAR(100)
AS
BEGIN
IF((79.5 < CAST(@LOG AS DECIMAL(30,7)) AND CAST(@LOG AS DECIMAL(30,7)) < 81.9) AND (5.9 < CAST(@LAT AS DECIMAL(30,7)) AND CAST(@LAT AS DECIMAL(30,7)) < 9.9))
BEGIN
	OPEN SYMMETRIC KEY AppKeyPass
	DECRYPTION	BY	PASSWORD	= '1990-1991-0405-1029';	
	DECLARE		@AppID	INT;
	SET @AppID = (SELECT AppID FROM  dialog_Application WHERE CONVERT(NVARCHAR(50),(DecryptByKey(AppProductKey))) = @KEY)
	CLOSE SYMMETRIC KEY AppKeyPass
	IF EXISTS(SELECT AppID FROM  dialog_Application WHERE AppID = @AppID)
	BEGIN
		IF EXISTS(SELECT CustID FROM dbo.dialog_Customer WHERE CustNumber = @USERID)
		BEGIN
			DECLARE @CustID UNIQUEIDENTIFIER SET @CustID = (SELECT CustID FROM  dialog_Customer WHERE CustNumber = @USERID);
			
			INSERT	INTO dbo.dialog_Complaint
			VALUES	(NEWID(), @CustID, @LOG, @LAT, 'SMS Message Complaint', @DATA, 'SMS', @TIME, 0, getdate());
			SELECT '1' AS 'SUCCES','0' AS 'ERROR','Your complaint is successfully updated.' AS 'MESSAGE';
			RETURN
		END
		ELSE
		BEGIN
			SELECT '0' AS 'SUCCES','1' AS 'ERROR','Sorry, We cant find the Customer.' AS 'MESSAGE';
			RETURN
		END
	END
END
ELSE
BEGIN
	SELECT '0' AS 'SUCCES','1' AS 'ERROR','Sorry, Wrong coordinates are entered.' AS 'MESSAGE';
	RETURN
END
END



--EXECUTE _sp_DIALOG_SMS_SERVER_NEW_COMPLAINT
--	@KEY			= 'C8D918DB-296E-4105-A652-87A0B4BA3A65',
--	@USERID			= '94773632682',
--	@LOG			= '80.7',
--	@LAT			= '7.78',
--	@DATA			= 'Signal Error',
--	@TIME			= '15/10/09 AT 22:45:12'

-------==================================================================================================
-------==================================================================================================
-------==================================================================================================

ALTER PROCEDURE dbo._sp_DIALOG_SMS_SERVER_NEW_COMPLAINT_DEG
	@KEY			NVARCHAR(50),
	@USERID			NVARCHAR(15),
	@Log_A_Deg		INT, 
	@Log_B_Min		INT, 
	@Log_C_Sec		FLOAT,
	@Lat_A_Deg		INT, 
	@Lat_B_Min		INT, 
	@Lat_C_Sec		FLOAT,
	@DATA			NVARCHAR(100),
	@TIME			NVARCHAR(100)
AS
BEGIN
		DECLARE @LOG NVARCHAR(50) SET @LOG = (SELECT dbo.DEG_TO_DEC(@Log_A_Deg,@Log_B_Min,@Log_C_Sec));
		DECLARE @LAT NVARCHAR(50) SET @LAT = (SELECT dbo.DEG_TO_DEC(@Lat_A_Deg,@Lat_B_Min,@Lat_C_Sec));
		IF((79.5 < CAST(@LOG AS DECIMAL(30,7)) AND CAST(@LOG AS DECIMAL(30,7)) < 81.9) AND (5.9 < CAST(@LAT AS DECIMAL(30,7)) AND CAST(@LAT AS DECIMAL(30,7)) < 9.9))
		BEGIN
			OPEN SYMMETRIC KEY AppKeyPass
			DECRYPTION	BY	PASSWORD	= '1990-1991-0405-1029';	
			DECLARE		@AppID	INT;
			SET @AppID = (SELECT AppID FROM  dialog_Application WHERE CONVERT(NVARCHAR(50),(DecryptByKey(AppProductKey))) = @KEY)
			CLOSE SYMMETRIC KEY AppKeyPass
			IF EXISTS(SELECT AppID FROM  dialog_Application WHERE AppID = @AppID)
			BEGIN
				IF EXISTS(SELECT CustID FROM dbo.dialog_Customer WHERE CustNumber = @USERID)
				BEGIN
					DECLARE @CustID UNIQUEIDENTIFIER SET @CustID = (SELECT CustID FROM  dialog_Customer WHERE CustNumber = @USERID);
					
					INSERT	INTO dbo.dialog_Complaint
					VALUES	(NEWID(), @CustID, @LOG, @LAT, 'SMS Message Complaint', @DATA, 'SMS', @TIME, 0, getdate());
					SELECT '1' AS 'SUCCES','0' AS 'ERROR','Your complaint is successfully updated.' AS 'MESSAGE';
					RETURN
				END
				ELSE
				BEGIN
					SELECT '0' AS 'SUCCES','1' AS 'ERROR','Sorry, We cant find the Customer.' AS 'MESSAGE';
					RETURN
				END
			END
		END
		ELSE
		BEGIN
			SELECT '0' AS 'SUCCES','1' AS 'ERROR','Sorry, Wrong coordinates are entered.' AS 'MESSAGE';
			RETURN
		END
END




--EXECUTE _sp_DIALOG_SMS_SERVER_NEW_COMPLAINT_DEG
--	@KEY			= 'C8D918DB-296E-4105-A652-87A0B4BA3A65',
--	@USERID			= '94773632682',
--	@Log_A_Deg		= 80,
--	@Log_B_Min		= 47,
--	@Log_C_Sec		= 32.60,
--	@Lat_A_Deg		= 7,
--	@Lat_B_Min		= 52,
--	@Lat_C_Sec		= 51.77,
--	@DATA			= 'Signal Error',
--	@TIME			= '15/10/09 AT 22:45:12'



--DECLARE @LOG NVARCHAR(50) SET @LOG = (SELECT dbo.DEG_TO_DEC(80,47,32.60));
--DECLARE @LAT NVARCHAR(50) SET @LAT = (SELECT dbo.DEG_TO_DEC(7,52,51.44));

--SELECT @LOG AS LOGer , @LAT  AS LATI
-------==================================================================================================
-------==================================================================================================
-------==================================================================================================

CREATE PROCEDURE dbo._sp_DIALOG_SMS_SERVER_RESTART_PC
	@KEY			NVARCHAR(50),
	@USERID			NVARCHAR(15)
AS
BEGIN
	OPEN SYMMETRIC KEY AppKeyPass
	DECRYPTION	BY	PASSWORD	= '1990-1991-0405-1029';	
	DECLARE		@AppID	INT;
	SET @AppID = (SELECT AppID FROM  dialog_Application WHERE CONVERT(NVARCHAR(50),(DecryptByKey(AppProductKey))) = @KEY)
	CLOSE SYMMETRIC KEY AppKeyPass
	IF EXISTS(SELECT AppID FROM  dialog_Application WHERE AppID = @AppID AND AppDedicatedUserContact = @USERID)
	BEGIN
		SELECT '1' AS 'SUCCES','0' AS 'ERROR','' AS 'MESSAGE';
		RETURN
	END
	ELSE
	BEGIN
		SELECT '0' AS 'SUCCES','1' AS 'ERROR','Sorry, Unauthorised access.' AS 'MESSAGE';
		RETURN
	END
END






--EXECUTE _sp_DIALOG_SMS_SERVER_RESTART_PC
--	@KEY			= 'C8D918DB-296E-4105-A652-87A0B4BA3A65',
--	@USERID			= '94773632682'