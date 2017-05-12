
	
begin
set nocount on;
	declare @UserId int, @LastLogin datetime

	SELECT @UserId=Id, @LastLogin=LastLogin
	FROM Users 
	WHERE UserName='tsvetelina_abadjieva@abv.bg' and [Password]='tsvety'
	IF @UserId IS NOT NULL
		BEGIN
		IF NOT EXISTS (SELECT Id from Users WHERE Id=@UserId)
			begin
				UPDATE Users set LastLogin=getdate() where Id=@UserId select @UserId [Id]
			end
		ELSE
			begin
				select 0--user not activated
			end
		END
	ELSE begin
				select 2--user is invalid
		end
end

declare @PersonID varchar(50)
select @PersonID=PersonID from users where (PersonID='57')
update users set Status=1, role='admin', IsActive='1', LastLogin= getdate() where (PersonID='57') 