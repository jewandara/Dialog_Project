ALTER PROCEDURE dbo._sp_view_dialog_TAB_LOAD_DIALOG_SITES
AS
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
		CONVERT(VARCHAR(20),dateadd(MINUTE, 810, ModifiedDate)) AS 'Modified'
	FROM dbo.dialog_Site
	ORDER BY SiteID ASC
END





ALTER VIEW dbo._view_dialog_TAB_LOAD_DIALOG_SITES_LIKE
AS
	SELECT
		SiteID,
		SiteName,
		SiteUID,
		Longitude, 
		Latitude,
		SiteStatus,
		TowerOwner,
		TowerType,
		CONVERT(VARCHAR(20),dateadd(MINUTE, 810, ModifiedDate)) AS 'Modified'
	FROM dbo.dialog_Site
	
	
	
	
ALTER PROCEDURE dbo._sp_view_dialog_TAB_LOAD_DIALOG_SITES_BITWEEN
	@ComLong			FLOAT,	
	@ComLat				FLOAT,
	@ComCust			FLOAT
AS
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
		CONVERT(VARCHAR(20),dateadd(MINUTE, 810, ModifiedDate)) AS 'Modified'
	FROM dbo.dialog_Site
	WHERE (Longitude BETWEEN (@ComLong - @ComCust) AND (@ComLong + @ComCust))
	AND	(Latitude BETWEEN (@ComLat - @ComCust) AND (@ComLat + @ComCust))
END
--EXECUTE _sp_view_dialog_TAB_LOAD_DIALOG_SITES_BITWEEN 
--@ComLong = 80.164468, 	
--@ComLat	=  6.944097,
--@ComCust = 0.03



ALTER PROCEDURE dbo._sp_view_dialog_TAB_LOAD_DIALOG_ANTENNA_BITWEEN
	@ComLong			FLOAT,	
	@ComLat				FLOAT,
	@ComCust			FLOAT
AS
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
		CONVERT(VARCHAR(20),dateadd(MINUTE, 810, ModifiedDate)) AS 'Modified'
	FROM dbo.dialog_Site
	WHERE (Longitude BETWEEN (@ComLong - @ComCust) AND (@ComLong + @ComCust))
	AND	(Latitude BETWEEN (@ComLat - @ComCust) AND (@ComLat + @ComCust))
END



--EXECUTE _sp_view_dialog_TAB_LOAD_DIALOG_SITES_BITWEEN 
--@ComLong = 80.164468, 	
--@ComLat	=  6.944097,
--@ComCust = 0.03
	
	
