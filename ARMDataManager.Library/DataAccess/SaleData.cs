using ARMDataManager.Library.Internal.DataAccess;
using ARMDataManager.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARMDataManager.Library.DataAccess
{
    public class SaleData
    {
        public void SaveSale(SaleModel saleInfo, string cashierId)
        {
            List<SaleDetailDBModel> saleDetailList = new List<SaleDetailDBModel>();
            ProductData productData = new ProductData();
            decimal taxRate = ConfigHelper.GetTaxRate()/100;

            foreach(var item in saleInfo.SaleDetails)
            {
                var saleDetail =  new SaleDetailDBModel
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity
                };

                //Get info about this product
                var productInfo = productData.GetProductById(item.ProductId);

                if(productInfo == null)
                {
                    throw new Exception($"This product id of {productInfo.Id} could not be found in the database");
                }

                saleDetail.PurchasePrice = productInfo.RetailPrice * saleDetail.Quantity;

                if(productInfo.IsTaxable)
                {
                    saleDetail.Tax = saleDetail.PurchasePrice * taxRate;
                }
                saleDetailList.Add(saleDetail);
            }

            //Create sale model
            SaleDBModel sale = new SaleDBModel
            {
                SubTotal = saleDetailList.Sum(x => x.PurchasePrice),
                Tax = saleDetailList.Sum(x => x.Tax),
                CashierId = cashierId
            };

            sale.Total = sale.SubTotal + sale.Tax;

            //Save the sale record to DB 
            using (SqlDataAccess sql = new SqlDataAccess())
            {
                try
                {
                    sql.StartTransaction("ARMData");

                    sql.SaveData("dbo.spSale_Insert", sale, "ARMData");

                    //Get the ID from Sale Model
                    sale.Id = sql.LoadDataInTransaction<int, dynamic>("dbo.spSale_lookup", new { sale.CashierId, sale.SaleDate }).FirstOrDefault();

                    //Finish filling in the sale details
                    foreach (var item in saleDetailList)
                    {
                        item.SaleId = sale.Id;

                        //Save sale detail record to DB
                        sql.SaveDataInTransaction<SaleDetailDBModel>("dbo.spSaleDetail_Insert", item);
                    }

                    sql.CommitTransaction();
                }
                catch (Exception ex)
                {
                    sql.RollbackTransaction(); 
                    throw;
                }
            }
            
        }

        public List<SaleReportModel> GetSaleReport()
        {
            SqlDataAccess sqlDataAccess = new SqlDataAccess();
            var output = sqlDataAccess.LoadData<SaleReportModel, dynamic>("dbo.spSale_SaleReport", new { }, "ARMData");
            return output;
        }
    }
}
