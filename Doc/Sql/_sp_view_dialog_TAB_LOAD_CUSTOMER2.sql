----============================================================================================

ALTER PROCEDURE dbo._sp_view_dialog_FORM_LOAD 
	@SYSAPPID			INT, 
	@USERAPPID			INT, 
	@USERNUMBER			NVARCHAR(15), 
	@USERLOGID			UNIQUEIDENTIFIER
AS
BEGIN
	IF(dbo.LOGIN_APP_ID_OK(@SYSAPPID,@USERAPPID,@USERNUMBER,@USERLOGID) = 1)
	BEGIN
		SELECT AppDedicatedRegion AS 'REGION', 'Email : ' + AppDedicatedUserEmail + ' | Number : ' + AppDedicatedUserContact AS 'MESAGE'
		FROM dbo.dialog_Application WHERE AppID = @USERAPPID;
		INSERT	INTO dbo.dialog_access
		VALUES	(NEWID(), @SYSAPPID, @USERLOGID, @USERAPPID,1,'FORM_LOAD','user view the system',CONVERT(VARCHAR(20),dateadd(HOUR, 12, dateadd(MINUTE, 30, getdate()))));
	END
	ELSE
	BEGIN
		INSERT	INTO dbo.dialog_access
		VALUES	(NEWID(), @SYSAPPID, @USERLOGID, @USERAPPID,0,'FORM_LOAD','unauthorised access',CONVERT(VARCHAR(20),dateadd(HOUR, 12, dateadd(MINUTE, 30, getdate()))));
	END
END


----============================================================================================

ALTER PROCEDURE dbo._sp_view_dialog_TAB_LOAD_CUSTOMER
	@SYSAPPID			INT, 
	@USERAPPID			INT, 
	@USERNUMBER			NVARCHAR(15), 
	@USERLOGID			UNIQUEIDENTIFIER,
	@CUSTTYPE			NVARCHAR(5)
AS
BEGIN
	IF(dbo.LOGIN_APP_ID_OK(@SYSAPPID,@USERAPPID,@USERNUMBER,@USERLOGID) = 1)
	BEGIN
		IF (@CUSTTYPE = 'OUT')
		BEGIN
			SELECT TOP 100
				CustNumber,
				CustName, 
				CustType,
				CustEmail,
				CustAddresOne,
				'' AS 'CustCallTime',
				ModifiedDate,
				InsertedDate
			FROM dbo.dialog_Customer
			WHERE (CustType = 'OUT') OR (CustID NOT IN (SELECT CustID FROM dbo.dialog_Login) AND CustType = 'SYS')
			ORDER BY ModifiedDate  DESC;
		END
		ELSE IF(@CUSTTYPE = 'SYS')
		BEGIN
			OPEN SYMMETRIC KEY KeyPass
			DECRYPTION	BY	PASSWORD	= 'DIALOG@Application.CODE';
			SELECT TOP 100
				CUST.CustNumber,
				CUST.CustName, 
				CUST.CustType,
				CUST.CustEmail,
				CUST.CustAddresOne,
				'BETWEEN ' + CUST.CalTimeIN + ' TO ' + CUST.CalTimeOut AS 'CustCallTime',
				CUST.ModifiedDate
			FROM dbo.dialog_Customer  AS CUST, dbo.dialog_Login AS LOGN
			WHERE LOGN.AppID = @USERAPPID AND CUST.CustID = LOGN.CustID AND CustType = 'SYS' AND (CONVERT(NVARCHAR(50),(DecryptByKey(LoginType)))) = 'CUST'
			ORDER BY CUST.ModifiedDate  DESC
			CLOSE SYMMETRIC KEY KeyPass
		END
		INSERT	INTO dbo.dialog_access
		VALUES	(NEWID(), @SYSAPPID, @USERLOGID, @USERAPPID,1,'TAB_LOAD_CUSTOMER/' + @CUSTTYPE,'view 100 SMS Customers',CONVERT(VARCHAR(20),dateadd(HOUR, 12, dateadd(MINUTE, 30, getdate()))));
	END
	ELSE
	BEGIN
		INSERT	INTO dbo.dialog_access
		VALUES	(NEWID(), @SYSAPPID, @USERLOGID, @USERAPPID,0,'TAB_LOAD_CUSTOMER/' + @CUSTTYPE,'unauthorised access',CONVERT(VARCHAR(20),dateadd(HOUR, 12, dateadd(MINUTE, 30, getdate()))));
	END
END


--EXECUTE _sp_view_dialog_TAB_LOAD_CUSTOMER 
--@SYSAPPID			= 7, 
--@USERAPPID			= 8, 
--@USERNUMBER			= '94773632682', 
--@USERLOGID			= 'DC07BD80-BA74-441A-ABF9-D1D9554B48FA',
--@CUSTTYPE			= 'SYS'
	
	
----============================================================================================
----============================================================================================


ALTER PROCEDURE dbo._sp_view_dialog_TAB_LOAD_CUSTOMER_SEARCH
	@SYSAPPID			INT, 
	@USERAPPID			INT, 
	@USERNUMBER			NVARCHAR(15), 
	@USERLOGID			UNIQUEIDENTIFIER,
	@TEXT				NVARCHAR(50),
	@TYPE				NVARCHAR(10)
AS
BEGIN
	OPEN SYMMETRIC KEY KeyPass
	DECRYPTION	BY	PASSWORD	= 'DIALOG@Application.CODE';
	IF(@TYPE = 'BY_NAME')
	BEGIN
		SELECT TOP 10
			CUST.CustNumber,
			CUST.CustName, 
			CUST.CustType,
			CUST.CustEmail,
			CUST.CustAddresOne,
			'BETWEEN ' + CUST.CalTimeIN + ' TO ' + CUST.CalTimeOut AS 'CustCallTime',
			CUST.ModifiedDate
		FROM dbo.dialog_Customer  AS CUST, dbo.dialog_Login AS LOGN
		WHERE LOGN.AppID = @USERAPPID AND CUST.CustID = LOGN.CustID AND CUST.CustType = 'SYS' AND (CONVERT(NVARCHAR(50),(DecryptByKey(LoginType)))) = 'CUST' AND CustName LIKE @TEXT + '%'
		ORDER BY CUST.ModifiedDate  DESC;
	END
	ELSE IF (@TYPE = 'BY_NUMBER')
	BEGIN
		SELECT TOP 10
			CUST.CustNumber,
			CUST.CustName, 
			CUST.CustType,
			CUST.CustEmail,
			CUST.CustAddresOne,
			'BETWEEN ' + CUST.CalTimeIN + ' TO ' + CUST.CalTimeOut AS 'CustCallTime',
			CUST.ModifiedDate
		FROM dbo.dialog_Customer  AS CUST, dbo.dialog_Login AS LOGN
		WHERE LOGN.AppID = @USERAPPID AND CUST.CustID = LOGN.CustID AND CustType = 'SYS' AND (CONVERT(NVARCHAR(50),(DecryptByKey(LoginType)))) = 'CUST' AND CustNumber LIKE @TEXT + '%'
		ORDER BY CUST.ModifiedDate  DESC;
	END
	ELSE IF (@TYPE = 'BY_EMAIL')
	BEGIN
		SELECT TOP 10
			CUST.CustNumber,
			CUST.CustName, 
			CUST.CustType,
			CUST.CustEmail,
			CUST.CustAddresOne,
			'BETWEEN ' + CUST.CalTimeIN + ' TO ' + CUST.CalTimeOut AS 'CustCallTime',
			CUST.ModifiedDate
		FROM dbo.dialog_Customer  AS CUST, dbo.dialog_Login AS LOGN
		WHERE LOGN.AppID = @USERAPPID AND CUST.CustID = LOGN.CustID AND CustType = 'SYS' AND (CONVERT(NVARCHAR(50),(DecryptByKey(LoginType)))) = 'CUST' AND CustEmail LIKE @TEXT + '%'
		ORDER BY CUST.ModifiedDate  DESC;
	END
	CLOSE SYMMETRIC KEY KeyPass
END

--EXECUTE _sp_view_dialog_TAB_LOAD_CUSTOMER_SEARCH 
--@SYSAPPID			= 7, 
--@USERAPPID			= 8, 
--@USERNUMBER			= '94773632682', 
--@USERLOGID			= '4DFDE317-277D-473C-B24D-60A8B6A053B2',
--@TEXT				= '94',
--@TYPE				= 'BY_NUMBER'



----============================================================================================
----============================================================================================


ALTER PROCEDURE dbo._sp_view_dialog_TAB_LOAD_CUSTOMER_SELECT
	@SYSAPPID			INT, 
	@USERAPPID			INT, 
	@USERNUMBER			NVARCHAR(15), 
	@USERLOGID			UNIQUEIDENTIFIER,
	@CUSTNUMBER			NVARCHAR(15)
AS
BEGIN
	IF(dbo.LOGIN_APP_ID_OK(@SYSAPPID,@USERAPPID,@USERNUMBER,@USERLOGID) = 1)
	BEGIN
		IF EXISTS(SELECT CustID FROM dbo.dialog_Customer WHERE CustNumber = @CUSTNUMBER AND CustType = 'OUT')
		BEGIN
			SELECT
				CustID,		
				CustNumber,
				CustName,
				CustGender,
				CustEmail,			
				CustAddresOne,
				CustAddresTwo, 
				'- - - -' AS 'CustCallTime', 
				CustType,
				'- - - -' AS AppID, 
				'True' AS 'lock',
				'0' AS FaultPWCount,			
				'- - - -' AS PWChangeType,
				'- - - -' AS 'PassWordChangeDate',
				InsertedDate AS 'InsertedDate',
				ModifiedDate AS 'ModifiedDate'
			FROM dbo.dialog_Customer
			WHERE CustNumber = @CUSTNUMBER
			ORDER BY ModifiedDate  DESC;
			INSERT	INTO dbo.dialog_access
			VALUES	(NEWID(), @SYSAPPID, @USERLOGID, @USERAPPID, 1, 'TAB_LOAD_CUSTOMER_SELECT/OUT/' + @CUSTNUMBER, 'Cuset Number : ' + @CUSTNUMBER, CONVERT(VARCHAR(20),dateadd(HOUR, 12, dateadd(MINUTE, 30, getdate()))));
		END
		ELSE IF EXISTS(SELECT CustID FROM dbo.dialog_Customer WHERE (CustNumber = @CUSTNUMBER) AND (CustType = 'SYS') AND (CustID NOT IN (SELECT CustID FROM dbo.dialog_Login)))
		BEGIN
			SELECT
				CustID,		
				CustNumber,
				CustName,
				CustGender,
				CustEmail,			
				CustAddresOne,
				CustAddresTwo, 
				'- - - -' AS 'CustCallTime', 
				'OUT' AS 'CustType',
				'- - - -' AS AppID, 
				'True' AS 'lock',
				'0' AS FaultPWCount,			
				'- - - -' AS PWChangeType,
				'- - - -' AS 'PassWordChangeDate',
				InsertedDate AS 'InsertedDate',
				ModifiedDate AS 'ModifiedDate'
			FROM dbo.dialog_Customer
			WHERE CustNumber = @CUSTNUMBER
			ORDER BY ModifiedDate  DESC;
			INSERT	INTO dbo.dialog_access
			VALUES	(NEWID(), @SYSAPPID, @USERLOGID, @USERAPPID, 1, 'TAB_LOAD_CUSTOMER_SELECT/SYS(NOTID)/' + @CUSTNUMBER, 'Cuset Number : ' + @CUSTNUMBER, CONVERT(VARCHAR(20),dateadd(HOUR, 12, dateadd(MINUTE, 30, getdate()))));
		END
		ELSE IF EXISTS(SELECT CustID FROM dbo.dialog_Customer WHERE CustNumber = @CUSTNUMBER AND CustType = 'SYS')
		BEGIN
			SELECT
				CUST.CustID,		
				CUST.CustNumber,
				CUST.CustName,
				CUST.CustGender,
				CUST.CustEmail,			
				CUST.CustAddresOne,
				CUST.CustAddresTwo, 
				'BETWEEN ' + CUST.CalTimeIN + ' TO ' + CUST.CalTimeOut AS 'CustCallTime', 
				CUST.CustType,
				LOGN.AppID, 
				LOGN.IsLocked AS 'lock',
				LOGN.FaultPWCount,			
				LOGN.PWChangeType,
				LOGN.LastPWChangedDate AS 'PassWordChangeDate',
				LOGN.InsertedDate AS 'InsertedDate',
				CUST.ModifiedDate AS 'ModifiedDate'
			FROM dbo.dialog_Customer  AS CUST, dbo.dialog_Login AS LOGN
			WHERE CUST.CustID = LOGN.CustID AND CUST.CustNumber = @CUSTNUMBER
			ORDER BY CUST.ModifiedDate  DESC;
			INSERT	INTO dbo.dialog_access
			VALUES	(NEWID(), @SYSAPPID, @USERLOGID, @USERAPPID, 1, 'TAB_LOAD_CUSTOMER_SELECT/SYS/' + @CUSTNUMBER, 'Cuset Number : ' + @CUSTNUMBER, CONVERT(VARCHAR(20),dateadd(HOUR, 12, dateadd(MINUTE, 30, getdate()))));
		END
		ELSE
		BEGIN
			INSERT	INTO dbo.dialog_access
			VALUES	(NEWID(), @SYSAPPID, @USERLOGID, @USERAPPID, 0, 'TAB_LOAD_CUSTOMER_SELECT/' + @CUSTNUMBER, 'invalid customer number', CONVERT(VARCHAR(20),dateadd(HOUR, 12, dateadd(MINUTE, 30, getdate()))));
			SELECT '0' AS 'SUCCES','1' AS 'ERROR','Sorry,Can not find the customer.' AS 'MESSAGE';
		END
	END
	ELSE
	BEGIN
		INSERT	INTO dbo.dialog_access
		VALUES	(NEWID(), @SYSAPPID, @USERLOGID, @USERAPPID, 0, 'TAB_LOAD_CUSTOMER_SELECT/' + @CUSTNUMBER, 'unauthorised access', CONVERT(VARCHAR(20),dateadd(HOUR, 12, dateadd(MINUTE, 30, getdate()))));
		SELECT '0' AS 'SUCCES','1' AS 'ERROR','Unauthorised access' AS 'MESSAGE';
	END
END


--	EXECUTE _sp_view_dialog_TAB_LOAD_CUSTOMER_SELECT 
--	@SYSAPPID			= 8, 
--	@USERAPPID			= 8, 
--	@USERNUMBER			= '94773632682', 
--	@USERLOGID			= '4DFDE317-277D-473C-B24D-60A8B6A053B2',
--	@CUSTNUMBER			= '94773632685'

--SELECT * FROM dbo.dialog_Login WHERE CustID = (SELECT CustID FROM dbo.dialog_Customer WHERE CustNumber = '947734356233')



----============================================================================================
----============================================================================================
----============================================================================================

ALTER PROCEDURE dbo._sp_view_dialog_TAB_LOAD_CUSTOMER_UPDATE
	@SYSAPPID			INT, 
	@USERAPPID			INT, 
	@USERNUMBER			NVARCHAR(15), 
	@USERLOGID			UNIQUEIDENTIFIER,
	@CUSTNUMBER			NVARCHAR(15),
	@UPDATETYPE			NVARCHAR(10),
	@UPDATEDATA			NVARCHAR(500)
AS
BEGIN
	IF(dbo.LOGIN_APP_ID_OK(@SYSAPPID,@USERAPPID,@USERNUMBER,@USERLOGID) = 1)
	BEGIN
		IF EXISTS(SELECT CustID FROM dbo.dialog_Customer WHERE CustNumber = @CUSTNUMBER)
		BEGIN
			IF(@UPDATETYPE = 'NAME')
			BEGIN
				UPDATE dbo.dialog_Customer
				SET 
					CustName		=  @UPDATEDATA,
					ModifiedDate	=  CONVERT(VARCHAR(20),dateadd(HOUR, 12, dateadd(MINUTE, 30, getdate())))
				WHERE CustNumber	=  @CUSTNUMBER;
				
				INSERT	INTO dbo.dialog_access
				VALUES	(NEWID(), @SYSAPPID, @USERLOGID, @USERAPPID, 1, 'TAB_LOAD_CUSTOMER_UPDATE/' + @UPDATETYPE + '/' + @CUSTNUMBER, @UPDATEDATA, CONVERT(VARCHAR(20),dateadd(HOUR, 12, dateadd(MINUTE, 30, getdate()))));
				SELECT '1' AS 'SUCCES','0' AS 'ERROR','Customer name is successfully updated.' AS 'MESSAGE';
			END
			ELSE IF(@UPDATETYPE = 'GENDER')
			BEGIN
				UPDATE dbo.dialog_Customer
				SET 
					CustGender		=  @UPDATEDATA,
					ModifiedDate	=  CONVERT(VARCHAR(20),dateadd(HOUR, 12, dateadd(MINUTE, 30, getdate())))
				WHERE CustNumber	=  @CUSTNUMBER;
				
				INSERT	INTO dbo.dialog_access
				VALUES	(NEWID(), @SYSAPPID, @USERLOGID, @USERAPPID, 1, 'TAB_LOAD_CUSTOMER_UPDATE/' + @UPDATETYPE + '/' + @CUSTNUMBER, @UPDATEDATA, CONVERT(VARCHAR(20),dateadd(HOUR, 12, dateadd(MINUTE, 30, getdate()))));
				SELECT '1' AS 'SUCCES','0' AS 'ERROR','Customer gender is successfully updated.' AS 'MESSAGE';
			END		
			ELSE IF(@UPDATETYPE = 'ADDRESS')
			BEGIN
				UPDATE dbo.dialog_Customer
				SET 
					CustAddresOne	=  @UPDATEDATA,
					ModifiedDate	=  CONVERT(VARCHAR(20),dateadd(HOUR, 12, dateadd(MINUTE, 30, getdate())))
				WHERE CustNumber	=  @CUSTNUMBER;
				INSERT	INTO dbo.dialog_access
				VALUES	(NEWID(), @SYSAPPID, @USERLOGID, @USERAPPID, 1, 'TAB_LOAD_CUSTOMER_UPDATE/' + @UPDATETYPE + '/' + @CUSTNUMBER, @UPDATEDATA, CONVERT(VARCHAR(20),dateadd(HOUR, 12, dateadd(MINUTE, 30, getdate()))));
				SELECT '1' AS 'SUCCES','0' AS 'ERROR','Customer address is successfully updated.' AS 'MESSAGE';
			END
			ELSE IF(@UPDATETYPE = 'EMAIL')
			BEGIN
				UPDATE dbo.dialog_Customer
				SET 
					CustEmail		=  @UPDATEDATA,
					ModifiedDate	=  CONVERT(VARCHAR(20),dateadd(HOUR, 12, dateadd(MINUTE, 30, getdate())))
				WHERE CustNumber	=  @CUSTNUMBER;
				INSERT	INTO dbo.dialog_access
				VALUES	(NEWID(), @SYSAPPID, @USERLOGID, @USERAPPID, 1, 'TAB_LOAD_CUSTOMER_UPDATE/' + @UPDATETYPE + '/' + @CUSTNUMBER, @UPDATEDATA, CONVERT(VARCHAR(20),dateadd(HOUR, 12, dateadd(MINUTE, 30, getdate()))));
				SELECT '1' AS 'SUCCES','0' AS 'ERROR','Email address is successfully updated.' AS 'MESSAGE';
			END
			ELSE
			BEGIN
				INSERT	INTO dbo.dialog_access
				VALUES	(NEWID(), @SYSAPPID, @USERLOGID, @USERAPPID, 1, 'TAB_LOAD_CUSTOMER_UPDATE/' + @UPDATETYPE + '/' + @CUSTNUMBER, 'invalid update type', CONVERT(VARCHAR(20),dateadd(HOUR, 12, dateadd(MINUTE, 30, getdate()))));
				SELECT '0' AS 'SUCCES','1' AS 'ERROR','Sorry, Update type can not be found.' AS 'MESSAGE';
			END
		END
		ELSE
		BEGIN
			INSERT	INTO dbo.dialog_access
			VALUES	(NEWID(), @SYSAPPID, @USERLOGID, @USERAPPID, 0, 'TAB_LOAD_CUSTOMER_UPDATE/' + @UPDATETYPE + '/' + @CUSTNUMBER, 'invalid customer number', CONVERT(VARCHAR(20),dateadd(HOUR, 12, dateadd(MINUTE, 30, getdate()))));
			SELECT '0' AS 'SUCCES','1' AS 'ERROR','Sorry, Can not find the customer.' AS 'MESSAGE';
		END
	END
	ELSE
	BEGIN
		INSERT	INTO dbo.dialog_access
		VALUES	(NEWID(), @SYSAPPID, @USERLOGID, @USERAPPID, 0, 'TAB_LOAD_CUSTOMER_UPDATE/' + @UPDATETYPE + '/' + @CUSTNUMBER, 'unauthorised access', CONVERT(VARCHAR(20),dateadd(HOUR, 12, dateadd(MINUTE, 30, getdate()))));
		SELECT '0' AS 'SUCCES','1' AS 'ERROR','Unauthorised access' AS 'MESSAGE';
	END
END



----============================================================================================
----============================================================================================
----============================================================================================
----============================================================================================
----============================================================================================
----============================================================================================
----============================================================================================
----============================================================================================
----============================================================================================
----============================================================================================
----============================================================================================
----============================================================================================
----============================================================================================
----============================================================================================

ALTER PROCEDURE dbo._sp_view_dialog_TAB_LOAD_CUSTOMER_TYPE
	@SYSAPPID			INT, 
	@USERAPPID			INT, 
	@USERNUMBER			NVARCHAR(15), 
	@USERLOGID			UNIQUEIDENTIFIER,
	@CUSTNUMBER			NVARCHAR(15),
	@CUSTTYPE			NVARCHAR(10)
AS
BEGIN
	OPEN SYMMETRIC KEY AppKeyPass
	DECRYPTION	BY	PASSWORD	= '1990-1991-0405-1029';	
	DECLARE		@AppID	INT;
	SET @AppID = (SELECT AppID FROM  dialog_Application WHERE CONVERT(NVARCHAR(50),(DecryptByKey(AppProductKey))) = @KEY)
	CLOSE SYMMETRIC KEY AppKeyPass
	
	IF EXISTS(SELECT AppID FROM  dialog_Application WHERE AppID = @AppID)
	BEGIN
		IF EXISTS(SELECT AppID FROM  dialog_Application WHERE AppID = (SELECT AppID FROM  dialog_Login WHERE LoginID = @LOGID AND IsLocked = 0))
		BEGIN
			DECLARE	@UserAppID INT;
			SET @UserAppID = (SELECT AppID FROM dialog_Application WHERE AppID = (SELECT AppID FROM  dialog_Login WHERE LoginID = @LOGID ));
			
			IF EXISTS(SELECT CustID FROM dbo.dialog_Customer WHERE CustNumber = @CUSTID)
			BEGIN
				DECLARE	@CusID UNIQUEIDENTIFIER;
				SET @CusID = (SELECT CustID FROM dbo.dialog_Customer WHERE CustNumber = @CUSTID);
				
				IF EXISTS(SELECT LoginID FROM dbo.dialog_Login WHERE CustID = @CusID)
				BEGIN
					UPDATE dbo.dialog_Customer
					SET 
						CustType		=  @TYPE,
						ModifiedDate	=  CONVERT(VARCHAR(20),dateadd(HOUR, 12, dateadd(MINUTE, 30, getdate())))
					WHERE CustNumber	= @CUSTID;
					UPDATE dbo.dialog_Login
					SET 
						AppID		=  @UserAppID,
						ModifiedDate	=  CONVERT(VARCHAR(20),dateadd(HOUR, 12, dateadd(MINUTE, 30, getdate())))
					WHERE CustID	= @CusID;
					
					INSERT	INTO dbo.dialog_access
					VALUES	(NEWID(), @AppID, @LOGID, @UserAppID,1,'TAB_LOAD_CUSTOMER_CHANGE_TYPE/UPDATE_LOGIN','update customer id : ' + @CUSTID + ', type : ' + @TYPE + ', message : update customer login id successful',CONVERT(VARCHAR(20),dateadd(HOUR, 12, dateadd(MINUTE, 30, getdate()))));
					SELECT '1' AS 'SUCCES','0' AS 'ERROR','Customer type is successfully updated.' AS 'MESSAGE';
				END
				ELSE
				BEGIN
					OPEN SYMMETRIC KEY KeyPass
					DECRYPTION	BY	PASSWORD	= 'DIALOG@Application.CODE';
					DECLARE @encrypted_LoginPass VARBINARY(MAX);
					DECLARE @encrypted_LoginType VARBINARY(MAX);
					DECLARE @LoginPass NVARCHAR(50) SET @LoginPass = @CUSTID;
					DECLARE @LoginType NVARCHAR(15) SET @LoginType = 'CUST';
					SET @encrypted_LoginPass = EncryptByKey(Key_GUID('KeyPass'), @LoginPass);
					SET @encrypted_LoginType = EncryptByKey(Key_GUID('KeyPass'), @LoginType);
					CLOSE SYMMETRIC KEY KeyPass;
					
					INSERT	INTO dbo.dialog_Login
					VALUES	(NEWID(), @UserAppID, @CusID, @encrypted_LoginPass, @encrypted_LoginType,0,0,'NET',CONVERT(VARCHAR(20),dateadd(HOUR, 12, dateadd(MINUTE, 30, getdate()))), CONVERT(VARCHAR(20),dateadd(HOUR, 12, dateadd(MINUTE, 30, getdate()))), CONVERT(VARCHAR(20),dateadd(HOUR, 12, dateadd(MINUTE, 30, getdate()))));

					UPDATE dbo.dialog_Customer
					SET 
						CustType		=  @TYPE,
						ModifiedDate	=  CONVERT(VARCHAR(20),dateadd(HOUR, 12, dateadd(MINUTE, 30, getdate())))
					WHERE CustNumber	= @CUSTID;
					INSERT	INTO dbo.dialog_access
					VALUES	(NEWID(), @AppID, @LOGID, @UserAppID,1,'TAB_LOAD_CUSTOMER_CHANGE_TYPE/CREATE_LOGIN','update customer id : ' + @CUSTID + ', type : ' + @TYPE + ', message : insert customer new login id successful',CONVERT(VARCHAR(20),dateadd(HOUR, 12, dateadd(MINUTE, 30, getdate()))));
					SELECT '1' AS 'SUCCES','0' AS 'ERROR','New default login id is created, and customer type is successfully updated.' AS 'MESSAGE';
				END
			END
			ELSE
			BEGIN
				INSERT	INTO dbo.dialog_access
				VALUES	(NEWID(), @AppID, @LOGID, @UserAppID,0,'TAB_LOAD_CUSTOMER_CHANGE_TYPE','update customer id : ' + @CUSTID + ', type : ' + @TYPE + ', message : customer id error',CONVERT(VARCHAR(20),dateadd(HOUR, 12, dateadd(MINUTE, 30, getdate()))));
				SELECT '0' AS 'SUCCES','1' AS 'ERROR','Sorry,Can not find the customer.' AS 'MESSAGE';
			END
		END
		ELSE
		BEGIN
			INSERT	INTO dbo.dialog_access
			VALUES	(NEWID(), @AppID, @LOGID, NULL,0,'TAB_LOAD_CUSTOMER_CHANGE_TYPE','unauthorised access',CONVERT(VARCHAR(20),dateadd(HOUR, 12, dateadd(MINUTE, 30, getdate()))));
			SELECT '0' AS 'SUCCES','1' AS 'ERROR','Unauthorised Access. Stop' AS 'MESSAGE';
		END
	END
END





----============================================================================================
----============================================================================================
----============================================================================================
----============================================================================================
----============================================================================================
----============================================================================================
----============================================================================================
----============================================================================================
----============================================================================================
----============================================================================================
----============================================================================================
----============================================================================================
----============================================================================================
----============================================================================================
----============================================================================================
----============================================================================================
----============================================================================================
----============================================================================================
----============================================================================================
----============================================================================================






----============================================================================================

ALTER PROCEDURE dbo._sp_view_dialog_TAB_LOAD_CUSTOMER_ACTIVE
	@SYSAPPID			INT, 
	@USERAPPID			INT, 
	@USERNUMBER			NVARCHAR(15), 
	@USERLOGID			UNIQUEIDENTIFIER,
	@CUSTNUMBER			NVARCHAR(15), 
	@ACTIVE				BIT
AS
BEGIN
	IF(dbo.LOGIN_APP_ID_OK(@SYSAPPID,@USERAPPID,@USERNUMBER,@USERLOGID) = 1)
	BEGIN
		IF (@USERAPPID = (SELECT AppID FROM  dialog_Application WHERE AppID = (SELECT AppID FROM  dialog_Login WHERE CustID = (SELECT CustID FROM dbo.dialog_Customer WHERE CustNumber = @CUSTNUMBER))))
		BEGIN
			UPDATE dbo.dialog_Login
			SET IsLocked		=  @ACTIVE,
				ModifiedDate	=  CONVERT(VARCHAR(20),dateadd(HOUR, 12, dateadd(MINUTE, 30, getdate())))
			WHERE CustID		= (SELECT CustID FROM dbo.dialog_Customer WHERE CustNumber = @CUSTNUMBER);
			IF(CONVERT(VARCHAR(2),@ACTIVE) = '0')
			BEGIN
				INSERT	INTO dbo.dialog_access
				VALUES	(NEWID(), @SYSAPPID, @USERLOGID, @USERAPPID,1,'TAB_LOAD_CUSTOMER_ACTIVE/ACTIVE', 'activate customer : ' + @CUSTNUMBER, CONVERT(VARCHAR(20),dateadd(HOUR, 12, dateadd(MINUTE, 30, getdate()))));
				SELECT '1' AS 'SUCCES','0' AS 'ERROR','Customer login is successfully activated.' AS 'MESSAGE';
			END
			ELSE IF (CONVERT(VARCHAR(2),@ACTIVE) = '1')
			BEGIN
				INSERT	INTO dbo.dialog_access
				VALUES	(NEWID(), @SYSAPPID, @USERLOGID, @USERAPPID,1,'TAB_LOAD_CUSTOMER_ACTIVE/DEACTIVE', 'activate customer : ' + @CUSTNUMBER, CONVERT(VARCHAR(20),dateadd(HOUR, 12, dateadd(MINUTE, 30, getdate()))));
				SELECT '1' AS 'SUCCES','0' AS 'ERROR','Customer login is successfully deactivated.' AS 'MESSAGE';
			END
		END
		ELSE
		BEGIN
			INSERT	INTO dbo.dialog_access
			VALUES	(NEWID(), @SYSAPPID, @USERLOGID, @USERAPPID,0,'TAB_LOAD_CUSTOMER_ACTIVE/' +  CONVERT(VARCHAR(2),@ACTIVE), @CUSTNUMBER, CONVERT(VARCHAR(20),dateadd(HOUR, 12, dateadd(MINUTE, 30, getdate()))));
			SELECT '0' AS 'SUCCES','1' AS 'ERROR','Sorry, Customer is not in the system.' AS 'MESSAGE';
		END
	END
	ELSE
	BEGIN
		INSERT	INTO dbo.dialog_access
		VALUES	(NEWID(), @SYSAPPID, @USERLOGID, @USERAPPID, 0, 'TAB_LOAD_CUSTOMER_ACTIVE', 'unauthorised access', CONVERT(VARCHAR(20),dateadd(HOUR, 12, dateadd(MINUTE, 30, getdate()))));
		SELECT '0' AS 'SUCCES','1' AS 'ERROR','Unauthorised access' AS 'MESSAGE';
	END
END









----============================================================================================
----============================================================================================
----============================================================================================
----============================================================================================
----============================================================================================
----============================================================================================

ALTER PROCEDURE dbo._sp_view_dialog_TAB_LOAD_CUSTOMER_PASS_DEFAULT
	@SYSAPPID			INT, 
	@USERAPPID			INT, 
	@USERNUMBER			NVARCHAR(15), 
	@USERLOGID			UNIQUEIDENTIFIER,
	@CUSTNUMBER			NVARCHAR(15)
AS
BEGIN
	IF(dbo.LOGIN_APP_ID_OK(@SYSAPPID,@USERAPPID,@USERNUMBER,@USERLOGID) = 1)
	BEGIN
		IF (@USERAPPID = (SELECT AppID FROM  dialog_Application WHERE AppID = (SELECT AppID FROM  dialog_Login WHERE CustID = (SELECT CustID FROM dbo.dialog_Customer WHERE CustNumber = @CUSTNUMBER))))
		BEGIN
			UPDATE dbo.dialog_Login
			SET FaultPWCount	=  0,
				ModifiedDate	=  CONVERT(VARCHAR(20),dateadd(HOUR, 12, dateadd(MINUTE, 30, getdate())))
			WHERE CustID		= (SELECT CustID FROM dbo.dialog_Customer WHERE CustNumber = @CUSTNUMBER);

			INSERT	INTO dbo.dialog_access
			VALUES	(NEWID(), @SYSAPPID, @USERLOGID, @USERAPPID,1,'TAB_LOAD_CUSTOMER_PASS_COUNT_DEFAULT', 'set default customer : ' + @CUSTNUMBER, CONVERT(VARCHAR(20),dateadd(HOUR, 12, dateadd(MINUTE, 30, getdate()))));
			SELECT '1' AS 'SUCCES','0' AS 'ERROR','Customer login password count is set to default.' AS 'MESSAGE';
		END
		ELSE
		BEGIN
			INSERT	INTO dbo.dialog_access
			VALUES	(NEWID(), @SYSAPPID, @USERLOGID, @USERAPPID,0,'TAB_LOAD_CUSTOMER_PASS_COUNT_DEFAULT', @CUSTNUMBER, CONVERT(VARCHAR(20),dateadd(HOUR, 12, dateadd(MINUTE, 30, getdate()))));
			SELECT '0' AS 'SUCCES','1' AS 'ERROR','Sorry, Customer is not in the system.' AS 'MESSAGE';
		END
	END
	ELSE
	BEGIN
		INSERT	INTO dbo.dialog_access
		VALUES	(NEWID(), @SYSAPPID, @USERLOGID, @USERAPPID, 0, 'TAB_LOAD_CUSTOMER_PASS_COUNT_DEFAULT', 'unauthorised access', CONVERT(VARCHAR(20),dateadd(HOUR, 12, dateadd(MINUTE, 30, getdate()))));
		SELECT '0' AS 'SUCCES','1' AS 'ERROR','Unauthorised access' AS 'MESSAGE';
	END
END










----============================================================================================
----============================================================================================
----============================================================================================
----============================================================================================
----============================================================================================
----============================================================================================

ALTER PROCEDURE dbo._sp_view_dialog_TAB_LOAD_CUSTOMER_DELETE
	@SYSAPPID			INT, 
	@USERAPPID			INT, 
	@USERNUMBER			NVARCHAR(15), 
	@USERLOGID			UNIQUEIDENTIFIER,
	@CUSTNUMBER			NVARCHAR(15)
AS
BEGIN
	IF(dbo.LOGIN_APP_ID_OK(@SYSAPPID,@USERAPPID,@USERNUMBER,@USERLOGID) = 1)
	BEGIN
		IF (@USERAPPID = (SELECT AppID FROM  dialog_Application WHERE AppID = (SELECT AppID FROM  dialog_Login WHERE CustID = (SELECT CustID FROM dbo.dialog_Customer WHERE CustNumber = @CUSTNUMBER))))
		BEGIN
			DECLARE	@CID UNIQUEIDENTIFIER;
			SET @CID = (SELECT CustID FROM dbo.dialog_Customer WHERE CustNumber = @CUSTNUMBER);
			
			DELETE FROM dbo.dialog_Complaint
			WHERE CustID = @CID;
							
			DELETE FROM dbo.dialog_Login
			WHERE CustID = @CID;
											
			DELETE FROM dbo.dialog_Customer
			WHERE CustID = @CID;

			INSERT	INTO dbo.dialog_access
			VALUES	(NEWID(), @SYSAPPID, @USERLOGID, @USERAPPID,1,'TAB_LOAD_CUSTOMER_DELETE/' + @CUSTNUMBER, 'delete customer ' + CONVERT(VARCHAR(50),@CID) ,CONVERT(VARCHAR(20),dateadd(HOUR, 12, dateadd(MINUTE, 30, getdate()))));
			SELECT '1' AS 'SUCCES','0' AS 'ERROR','Customer profile and all customer complaints were deleted.' AS 'MESSAGE';
		END
		ELSE
		BEGIN
			INSERT	INTO dbo.dialog_access
			VALUES	(NEWID(), @SYSAPPID, @USERLOGID, @USERAPPID,0,'TAB_LOAD_CUSTOMER_DELETE', @CUSTNUMBER, CONVERT(VARCHAR(20),dateadd(HOUR, 12, dateadd(MINUTE, 30, getdate()))));
			SELECT '0' AS 'SUCCES','1' AS 'ERROR','Sorry, Customer is not in the system.' AS 'MESSAGE';
		END
	END
	ELSE
	BEGIN
		INSERT	INTO dbo.dialog_access
		VALUES	(NEWID(), @SYSAPPID, @USERLOGID, @USERAPPID, 0, 'TAB_LOAD_CUSTOMER_DELETE', 'unauthorised access', CONVERT(VARCHAR(20),dateadd(HOUR, 12, dateadd(MINUTE, 30, getdate()))));
		SELECT '0' AS 'SUCCES','1' AS 'ERROR','Unauthorised access' AS 'MESSAGE';
	END
END











----============================================================================================
----============================================================================================
----============================================================================================
----============================================================================================
----============================================================================================
----============================================================================================

ALTER PROCEDURE dbo._sp_view_dialog_TAB_LOAD_CUSTOMER_LOGIN_DELETE
	@SYSAPPID			INT, 
	@USERAPPID			INT, 
	@USERNUMBER			NVARCHAR(15), 
	@USERLOGID			UNIQUEIDENTIFIER,
	@CUSTNUMBER			NVARCHAR(15)
AS
BEGIN
	IF(dbo.LOGIN_APP_ID_OK(@SYSAPPID,@USERAPPID,@USERNUMBER,@USERLOGID) = 1)
	BEGIN
		IF (@USERAPPID = (SELECT AppID FROM  dialog_Application WHERE AppID = (SELECT AppID FROM  dialog_Login WHERE CustID = (SELECT CustID FROM dbo.dialog_Customer WHERE CustNumber = @CUSTNUMBER))))
		BEGIN
			DECLARE	@CID UNIQUEIDENTIFIER;
			SET @CID = (SELECT CustID FROM dbo.dialog_Customer WHERE CustNumber = @CUSTNUMBER);
			DELETE FROM dbo.dialog_Login
			WHERE CustID = @CID;
			
			UPDATE dbo.dialog_Customer
			SET 
				CustType		= 'OUT',
				ModifiedDate	= CONVERT(VARCHAR(20),dateadd(HOUR, 12, dateadd(MINUTE, 30, getdate())))
			WHERE CustNumber	= @CUSTNUMBER;
			INSERT	INTO dbo.dialog_access
			VALUES	(NEWID(), @SYSAPPID, @USERLOGID, @USERAPPID,1,'TAB_LOAD_CUSTOMER_LOGIN_DELETE/' + @CUSTNUMBER, 'delete customer login ' + CONVERT(VARCHAR(50),@CID) ,CONVERT(VARCHAR(20),dateadd(HOUR, 12, dateadd(MINUTE, 30, getdate()))));
			SELECT '1' AS 'SUCCES','0' AS 'ERROR','Customer login data and password were deleted. Customer type is set to OUT customer.' AS 'MESSAGE';
		END
		ELSE
		BEGIN
			INSERT	INTO dbo.dialog_access
			VALUES	(NEWID(), @SYSAPPID, @USERLOGID, @USERAPPID,0,'TAB_LOAD_CUSTOMER_LOGIN_DELETE', @CUSTNUMBER, CONVERT(VARCHAR(20),dateadd(HOUR, 12, dateadd(MINUTE, 30, getdate()))));
			SELECT '0' AS 'SUCCES','1' AS 'ERROR','Sorry, Customer is not in the system.' AS 'MESSAGE';
		END
	END
	ELSE
	BEGIN
		INSERT	INTO dbo.dialog_access
		VALUES	(NEWID(), @SYSAPPID, @USERLOGID, @USERAPPID, 0, 'TAB_LOAD_CUSTOMER_LOGIN_DELETE', 'unauthorised access', CONVERT(VARCHAR(20),dateadd(HOUR, 12, dateadd(MINUTE, 30, getdate()))));
		SELECT '0' AS 'SUCCES','1' AS 'ERROR','Unauthorised access' AS 'MESSAGE';
	END
END
