CREATE PROCEDURE [dbo].[spSale_Insert]
	@Id int output,
	@CashierId nvarchar(128),
	@Saledate datetime2,
	@subTotal money,
	@Tax money,
	@Total money

AS
begin
	set nocount on;

	insert into dbo.Sale(CashierId,SaleDate,SubTotal,Tax,Total)
	values (@CashierId,@SaleDate,@SubTotal,@Tax,@Total);

	select @Id = @@IDENTITY;

end
