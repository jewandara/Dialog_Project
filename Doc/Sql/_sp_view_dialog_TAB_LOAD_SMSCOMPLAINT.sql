-----==========================================================---
-----==========================================================---
-----==========================================================---
-----==========================================================---
ALTER PROCEDURE dbo._sp_view_dialog_TAB_LOAD_COMPLAINT_INSERT
	@KEY			NVARCHAR(50),
	@CUSTID			NVARCHAR(15),
	@Log			NVARCHAR(50),
	@Lat			NVARCHAR(50),
	@ComTitle		NVARCHAR(100),
	@ComData		NVARCHAR(1800)
AS
BEGIN
	OPEN SYMMETRIC KEY AppKeyPass
	DECRYPTION	BY	PASSWORD	= '1990-1991-0405-1029';	
	DECLARE		@AppID	INT;
	SET @AppID = (SELECT AppID FROM  dialog_Application WHERE CONVERT(NVARCHAR(50),(DecryptByKey(AppProductKey))) = @KEY)
	CLOSE SYMMETRIC KEY AppKeyPass
	IF EXISTS(SELECT AppID FROM  dialog_Application WHERE AppID = @AppID)
	BEGIN
		IF EXISTS(SELECT AppID FROM  dialog_Application WHERE AppID = (SELECT AppID FROM  dialog_Login WHERE CustID = (SELECT CustID FROM dbo.dialog_Customer WHERE CustNumber = @CUSTID) AND IsLocked = 0))
		BEGIN
			DECLARE	@CompCustID UNIQUEIDENTIFIER SET @CompCustID = (SELECT CustID FROM dbo.dialog_Customer WHERE CustNumber = @CUSTID);
			DECLARE	@CompLogID UNIQUEIDENTIFIER SET @CompLogID = (SELECT LoginID FROM  dialog_Login WHERE CustID = @CompCustID);
			
			INSERT	INTO dbo.dialog_Complaint
			VALUES	(NEWID(), @CompCustID, @Log, @Lat, @ComTitle, @ComData, 'NET', NULL, 0, CONVERT(VARCHAR(20),dateadd(HOUR, 12, dateadd(MINUTE, 30, getdate()))));
						
			INSERT	INTO dbo.dialog_access
			VALUES	(NEWID(), @AppID, @CompLogID, @AppID,1,'TAB_LOAD_COMPLAINT_INSERT','customers number : ' + @CUSTID + ', insert complaints from mobile, successfully',CONVERT(VARCHAR(20),dateadd(HOUR, 12, dateadd(MINUTE, 30, getdate()))));
			
			SELECT '1' AS 'SUCCES','0' AS 'ERROR','Customer new complaint is successfully updated.' AS 'MESSAGE';
		END
		ELSE
		BEGIN
			INSERT	INTO dbo.dialog_access
			VALUES	(NEWID(), @AppID, @CompLogID, @AppID,0,'TAB_LOAD_COMPLAINT_INSERT','unauthorised access',CONVERT(VARCHAR(20),dateadd(HOUR, 12, dateadd(MINUTE, 30, getdate()))));
			
			SELECT '0' AS 'SUCCES','1' AS 'ERROR','Unauthorised Access' AS 'MESSAGE';
		END
	END
END




--EXECUTE _sp_view_dialog_TAB_LOAD_COMPLAINT_INSERT
--	@KEY		= 'C8D918DB-296E-4105-A652-87A0B4BA3A65',
--	@CUSTID		= '94776632687',
--	@Log		= '80.67537',
--	@Lat		= '7.456734',
--	@ComTitle	= 'Signal Error',
--	@ComData	= 'This is testing message'







-----==========================================================---
-----==========================================================---
-----==========================================================---
-----==========================================================---






ALTER PROCEDURE dbo._sp_view_dialog_TAB_LOAD_COMPLAINT_BY_CUST
	@SYSAPPID			INT, 
	@USERAPPID			INT, 
	@USERNUMBER			NVARCHAR(15), 
	@USERLOGID			UNIQUEIDENTIFIER,
	@CUSTNUMBER			NVARCHAR(15)
AS
BEGIN
	IF(dbo.LOGIN_APP_ID_OK(@SYSAPPID,@USERAPPID,@USERNUMBER,@USERLOGID) = 1)
	BEGIN
		IF EXISTS(SELECT CustID FROM dbo.dialog_Customer WHERE CustNumber = @CUSTNUMBER)
		BEGIN
			SELECT * FROM dbo.dialog_Complaint WHERE CustID = (SELECT CustID FROM dbo.dialog_Customer WHERE CustNumber = @CUSTNUMBER)
			ORDER BY InsertedDate DESC;
			INSERT	INTO dbo.dialog_access
			VALUES	(NEWID(), @SYSAPPID, @USERLOGID, @USERAPPID,1,'TAB_LOAD_COMPLAINT_BY_CUST','view all complaints : ' + @CUSTNUMBER, CONVERT(VARCHAR(20),dateadd(HOUR, 12, dateadd(MINUTE, 30, getdate()))));
		END
		ELSE
		BEGIN
			INSERT	INTO dbo.dialog_access
			VALUES	(NEWID(), @SYSAPPID, @USERLOGID, @USERAPPID,0,'TAB_LOAD_COMPLAINT_BY_CUST', @CUSTNUMBER, CONVERT(VARCHAR(20),dateadd(HOUR, 12, dateadd(MINUTE, 30, getdate()))));
			SELECT '0' AS 'SUCCES','1' AS 'ERROR','Sorry, Customer is not in the system.' AS 'MESSAGE';
		END
	END
	ELSE
	BEGIN
		INSERT	INTO dbo.dialog_access
		VALUES	(NEWID(), @SYSAPPID, @USERLOGID, @USERAPPID, 0, 'TAB_LOAD_COMPLAINT_BY_CUST', 'unauthorised access', CONVERT(VARCHAR(20),dateadd(HOUR, 12, dateadd(MINUTE, 30, getdate()))));
		SELECT '0' AS 'SUCCES','1' AS 'ERROR','Unauthorised access' AS 'MESSAGE';
	END
END




--EXECUTE _sp_view_dialog_TAB_LOAD_COMPLAINT_BY_CUST
--	@SYSAPPID			= 8, 
--	@USERAPPID			= 8, 
--	@USERNUMBER			= '94773632682', 
--	@USERLOGID			= '4DFDE317-277D-473C-B24D-60A8B6A053B2',
--	@CUSTNUMBER			= '94773632682'

-----==========================================================---
-----==========================================================---
-----==========================================================---
----=========================================================-------
--------=================================================-----------
----=========================================================-------
--------=================================================-----------
----=========================================================-------
----=========================================================-------
--------=================================================-----------
----=========================================================-------
--------=================================================-----------
----=========================================================-------




ALTER PROCEDURE dbo._sp_view_dialog_TAB_LOAD_COMPLAINT
	@SYSAPPID			INT, 
	@USERAPPID			INT, 
	@USERNUMBER			NVARCHAR(15), 
	@USERLOGID			UNIQUEIDENTIFIER,
	@COMPTYPE			NVARCHAR(5)
AS
BEGIN
	IF(dbo.LOGIN_APP_ID_OK(@SYSAPPID,@USERAPPID,@USERNUMBER,@USERLOGID) = 1)
	BEGIN
		SELECT TOP 100 CUST.CustNumber, CUST.CustName, COMP.*
		FROM dbo.dialog_Complaint COMP, dbo.dialog_Login LOGN, dbo.dialog_Customer CUST
		WHERE COMP.ComplaintType = @COMPTYPE AND CUST.CustID = LOGN.CustID AND LOGN.CustID = COMP.CustID AND LOGN.AppID = @USERAPPID
		ORDER BY COMP.InsertedDate  DESC;
		INSERT	INTO dbo.dialog_access
		VALUES	(NEWID(), @SYSAPPID, @USERLOGID, @USERAPPID,1,'TAB_LOAD_COMPLAINT/' + @COMPTYPE,'view 100 SMS Complaints : ' + @COMPTYPE, CONVERT(VARCHAR(20),dateadd(HOUR, 12, dateadd(MINUTE, 30, getdate()))));
	END
	ELSE
	BEGIN
		INSERT	INTO dbo.dialog_access
		VALUES	(NEWID(), @SYSAPPID, @USERLOGID, @USERAPPID, 0, 'TAB_LOAD_COMPLAINT', 'unauthorised access', CONVERT(VARCHAR(20),dateadd(HOUR, 12, dateadd(MINUTE, 30, getdate()))));
		SELECT '0' AS 'SUCCES','1' AS 'ERROR','Unauthorised access' AS 'MESSAGE';
	END
END





--EXECUTE _sp_view_dialog_TAB_LOAD_COMPLAINT

--SELECT * FROM dbo.dialog_Complaint

--------=================================================-----------
----=========================================================-------
--------=================================================-----------
----=========================================================-------
--------=================================================-----------
----=========================================================-------
----=========================================================-------
--------=================================================-----------
----=========================================================-------
--------=================================================-----------




ALTER PROCEDURE dbo._sp_view_dialog_TAB_LOAD_COMPLAINT_SELECT
	@SYSAPPID			INT, 
	@USERAPPID			INT, 
	@USERNUMBER			NVARCHAR(15), 
	@USERLOGID			UNIQUEIDENTIFIER,
	@COMPLAINTID		UNIQUEIDENTIFIER
AS
BEGIN
	IF(dbo.LOGIN_APP_ID_OK(@SYSAPPID,@USERAPPID,@USERNUMBER,@USERLOGID) = 1)
	BEGIN		
		IF EXISTS(SELECT ComplaintID FROM dbo.dialog_Complaint WHERE ComplaintID = @COMPLAINTID)
		BEGIN
			SELECT CUST.CustNumber, CUST.CustName, COMP.*
			FROM dbo.dialog_Complaint COMP, dbo.dialog_Customer CUST
			WHERE CUST.CustID = COMP.CustID AND COMP.ComplaintID = @COMPLAINTID
			ORDER BY COMP.InsertedDate  DESC;
						
			UPDATE dbo.dialog_Complaint
			SET ComplaintView = 1
			WHERE ComplaintID = @COMPLAINTID;
	
			INSERT	INTO dbo.dialog_access
			VALUES	(NEWID(), @SYSAPPID, @USERLOGID, @USERAPPID,1,'TAB_LOAD_COMPLAINT_SELECT','view Complaint id ' + CONVERT(VARCHAR(50),@COMPLAINTID), CONVERT(VARCHAR(20),dateadd(HOUR, 12, dateadd(MINUTE, 30, getdate()))));
		END
		ELSE
		BEGIN
			INSERT	INTO dbo.dialog_access
			VALUES	(NEWID(), @SYSAPPID, @USERLOGID, @USERAPPID, 0, 'TAB_LOAD_COMPLAINT_SELECT/', 'missing complaint id', CONVERT(VARCHAR(20),dateadd(HOUR, 12, dateadd(MINUTE, 30, getdate()))));
			SELECT '0' AS 'SUCCES','1' AS 'ERROR','Sorry, Can not find the complaint id.' AS 'MESSAGE';
		END
	END
	BEGIN
		INSERT	INTO dbo.dialog_access
		VALUES	(NEWID(), @SYSAPPID, @USERLOGID, @USERAPPID, 0, 'TAB_LOAD_COMPLAINT_SELECT', 'unauthorised access', CONVERT(VARCHAR(20),dateadd(HOUR, 12, dateadd(MINUTE, 30, getdate()))));
		SELECT '0' AS 'SUCCES','1' AS 'ERROR','Unauthorised access' AS 'MESSAGE';
	END
END



--EXECUTE _sp_view_dialog_TAB_LOAD_COMPLAINT_SELECT
--	@KEY		= 'C8D918DB-296E-4105-A652-87A0B4BA3A65',
--	@LOGID		= 'DC07BD80-BA74-441A-ABF9-D1D9554B48FA',
--	@COMPID		= '5F723C1E-7523-4345-8026-1274E304F998'






	
CREATE PROCEDURE dbo._sp_view_dialog_FUNCTION_BITWEEN_SITES
	@ComLong			FLOAT,	
	@ComLat				FLOAT,
	@ComCust			FLOAT,
	@owner				VARCHAR(10)
AS
BEGIN
	IF(@owner = 'DIALOG')
	BEGIN
		SELECT 
			SiteID,
			SiteName,
			SiteUID,
			Longitude, 
			Latitude,
			SiteStatus,
			TowerOwner,
			TowerType,
			ModifiedDate
		FROM dbo.dialog_Site
		WHERE (Longitude BETWEEN (@ComLong - @ComCust) AND (@ComLong + @ComCust))
		AND	(Latitude BETWEEN (@ComLat - @ComCust) AND (@ComLat + @ComCust))
	END
	ELSE IF(@owner = 'ETISALAT')
	BEGIN
		SELECT *
		FROM dbo.site_Etisalat
		WHERE (Longitude BETWEEN (@ComLong - @ComCust) AND (@ComLong + @ComCust))
		AND	(Latitude BETWEEN (@ComLat - @ComCust) AND (@ComLat + @ComCust))
	END
	ELSE IF(@owner = 'HUTCH')
	BEGIN
		SELECT *
		FROM dbo.site_Hutch
		WHERE (Longitude BETWEEN (@ComLong - @ComCust) AND (@ComLong + @ComCust))
		AND	(Latitude BETWEEN (@ComLat - @ComCust) AND (@ComLat + @ComCust))
	END
	ELSE IF(@owner = 'LANKABELL')
	BEGIN
		SELECT *
		FROM dbo.site_LankaBell
		WHERE (Longitude BETWEEN (@ComLong - @ComCust) AND (@ComLong + @ComCust))
		AND	(Latitude BETWEEN (@ComLat - @ComCust) AND (@ComLat + @ComCust))
	END
	ELSE IF(@owner = 'MOBITEL')
	BEGIN
		SELECT *
		FROM dbo.site_Mobitel
		WHERE (Longitude BETWEEN (@ComLong - @ComCust) AND (@ComLong + @ComCust))
		AND	(Latitude BETWEEN (@ComLat - @ComCust) AND (@ComLat + @ComCust))
	END
	ELSE
	BEGIN
		SELECT '0' AS 'SUCCESS'
	END
END


--EXECUTE _sp_view_dialog_TAB_LOAD_DIALOG_SITES_BITWEEN 
--@ComLong = 80.164468, 	
--@ComLat	=  6.944097,
--@ComCust = 0.03










-------=======================================================================================================
-------=======================================================================================================

-------=======================================================================================================
-------=======================================================================================================

-------=======================================================================================================
-------=======================================================================================================












	
CREATE PROCEDURE dbo._sp_view_dialog_FUNCTION_SECTORS
	@SiteID				VARCHAR(10),
	@SectorBand			VARCHAR(5)
AS
BEGIN
	SELECT 
		SEC.SiteID,
		SEC.SectorID,
		SEC.AntennaID,
		STE.Longitude,
		STE.Latitude,
		SEC.CellID,
		SEC.Technology,
		SEC.CabinType,
		SEC.Phase,
		SEC.PropertyStatus,
		SEC.Vendor,
		SEC.HorizontalBeamwidth,
		SEC.VerticalBeamwidth,
		SEC.BTSName
	FROM dbo.dialog_Sectors SEC LEFT JOIN dbo.dialog_Site STE ON SEC.SiteID = STE.SiteID
	WHERE (SEC.SectorID LIKE @SectorBand + '%' OR SEC.SectorID LIKE 'Ind_' + @SectorBand + '%') AND (SEC.SiteID = @SiteID)
END

--EXECUTE _sp_view_dialog_FUNCTION_SECTORS
--	@SiteID		= 'CM0188',
--	@SectorBand = 'GSM'
	



-------=======================================================================================================
-------=======================================================================================================
-------=======================================================================================================
-------=======================================================================================================
-------=======================================================================================================
-------=======================================================================================================



CREATE PROCEDURE dbo._sp_view_dialog_FUNCTION_ANTENNA
	@SiteID				NVARCHAR(10),
	@AntennaID			NVARCHAR(2)
AS
BEGIN
	SELECT 
		ANT.SiteID,
		ANT.AntennaID,
		STE.Longitude,
		STE.Latitude,
		ANT.Height,
		ANT.Azimuth,
		ANT.MecTilt,
		--DATA.ETilt,
		--DATA.ElectricalAzimuth,
		--DATA.ElectricalBeamwidth,
		TYP.AntennaType,
		TYP.PortName,
		TYP.MinFrequency_MHz,
		TYP.MaxFrequency_MHz
	FROM dbo.dialog_Antenna ANT 
	--LEFT JOIN dbo.dialog_AntennaData DATA ON ANT.SiteID = DATA.SiteID AND ANT.AntennaID = DATA.AntennaID
	LEFT JOIN dbo.dialog_AntennaType TYP ON ANT.AntennaTypeID = TYP.AntennaTypeID
	LEFT JOIN dbo.dialog_Site STE ON ANT.SiteID = STE.SiteID
	WHERE ANT.SiteID = @SiteID AND ANT.AntennaID = @AntennaID
END



EXECUTE _sp_view_dialog_FUNCTION_ANTENNA
	@SiteID		= 'MT0125',
	@AntennaID	= '1'




-----------------------------------------------------------------
