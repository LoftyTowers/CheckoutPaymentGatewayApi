/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

declare @tempTable TABLE( Id int, StatusDesc nvarchar(20))

insert into @tempTable values(0,'Unknown'),
															(10,'RequestRecieved'),
															(20,'RequestSent'),
															(30,'RequestSucceded'),
															(99,'RequestFailed')

MERGE PaymentStatus AS TARGET
USING @tempTable AS SOURCE ON TARGET.Id = SOURCE.Id
WHEN MATCHED AND TARGET.StatusDesc <> SOURCE.StatusDesc
THEN UPDATE SET TARGET.StatusDesc = SOURCE.StatusDesc 
WHEN NOT MATCHED THEN
insert (Id, StatusDesc) VALUES (SOURCE.ID, SOURCE.StatusDesc)
;