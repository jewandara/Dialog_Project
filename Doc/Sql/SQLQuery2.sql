
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
ALTER PROCEDURE dbo._sp_dialog_NewCustomerMessage
	@CustNumber				NVARCHAR(10),	
	@CustLog				NVARCHAR(50),
	@CustLat				NVARCHAR(50),
	@ComplaintData			NVARCHAR(2000),
	@MessageTime			NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;
    IF EXISTS(SELECT CustNumber FROM dbo.dialog_Customer WHERE CustNumber = @CustNumber)
	BEGIN
		INSERT INTO dbo.dialog_Complaint VALUES (
		NEWID(),(SELECT CustID FROM dbo.dialog_Customer WHERE CustNumber = @CustNumber),@CustLog,@CustLat,@ComplaintData,@MessageTime,getdate(),getdate());
		SELECT '10' AS 'RETURN', 'Sussfuly' AS 'MESAGE';
	END
	ELSE
	BEGIN
		--Dec@CustName		=	'Customer',		
		--@CustGender		=	'MALE',
		--@CustEmail		=	'dialog@dialog.lk',
		--@CustRegion		=	'WEST'
		OPEN SYMMETRIC KEY CustKeyPass
		DECRYPTION	BY	PASSWORD	= 'DIALOG@Application.CODE';
		DECLARE @encriCustEmail	VARBINARY(MAX) SET @encriCustEmail = EncryptByKey(Key_GUID('CustKeyPass'), 'dialog@dialog.lk');
		INSERT INTO dbo.dialog_Customer VALUES (
		NEWID(),1,@CustNumber,'Customer','MALE',@encriCustEmail,'WEST',NEWID(),getdate(),getdate());
		CLOSE SYMMETRIC KEY CustKeyPass;
		
		INSERT INTO dbo.dialog_Complaint VALUES (
		NEWID(),(SELECT CustID FROM dbo.dialog_Customer WHERE CustNumber = @CustNumber),@CustLog,@CustLat,@ComplaintData,@MessageTime,getdate(),getdate());
		SELECT '11' AS 'RETURN', 'Sussfuly' AS 'MESAGE';
	END			
END





EXECUTE dbo._sp_dialog_NewCustomerMessage
	@CustNumber		=	'0773632383',
	@CustLog		=	'80.0',
	@CustLat		=	'80.0',
	@ComplaintData	=	'Signal Eror',
	@MessageTime	=	'10/10/10'