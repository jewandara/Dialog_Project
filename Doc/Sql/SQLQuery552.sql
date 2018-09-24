
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


