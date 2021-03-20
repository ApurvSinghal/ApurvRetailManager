CREATE PROCEDURE [dbo].[spUserLookup]
	@Id nvarchar(128)
AS
begin
	set nocount on;

	SELECT Id,FirstName, LastName, EmailAddress,CreatedDate 
	FROM [dbo].[User]
	Where Id = @Id;
end