CREATE PROCEDURE AddCard 
	-- Add the parameters for the stored procedure here
@CardNumber as bigint,
@CVC as int,
@ExpiryDate as datetime2(7),
@BankName as nvarchar(50),
@UserId as uniqueIdentifier
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

		-- Insert statements for procedure here
		begin tran
		begin try
		-- Insert statements for procedure here
		declare @cardID AS UNIQUEIDENTIFIER,
				@excryptedCardNo as varbinary(max) = EncryptByKey (Key_GUID('SymKey_Card'), cast(@CardNumber as varchar(50)))
	
		if exists (select top(1) Id from dbo.[Card] where CardNumber_encrypt = @excryptedCardNo)
		begin
			select top(1) @cardID = Id from [Card] where CardNumber_encrypt = @cardNumber
		end
		else
		begin 
			insert into dbo.[Card] (CardNumber_encrypt,CVC_encrypt,ExpiryDate,BankName,UserId)
			values(
					EncryptByKey (Key_GUID('SymKey_Card'), cast(@CardNumber as varchar(50))),
					EncryptByKey (Key_GUID('SymKey_Card'), cast(@CVC as varchar(3))),
					@ExpiryDate,
					@BankName,
					@UserId
					)
			set @cardId = scope_identity()
		end
		select @cardId
		RETURN 0
		commit tran
	end try
	begin catch
		rollback tran
		throw
	end catch
END
GO
