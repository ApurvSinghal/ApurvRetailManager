using ARMDataManager.Library.Internal.DataAccess;
using ARMDataManager.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARMDataManager.Library.DataAccess
{
    public class InventoryData
    {
        public List<InventoryModel> GetInventory()
        {
            SqlDataAccess sqlDataAccess = new SqlDataAccess();

            var output = sqlDataAccess.LoadData<InventoryModel,dynamic>("dbo.spInventory_GetAll", new { }, "ARMData");
            return output;
        }

        public void SaveInventoryRecord(InventoryModel item)
        {
            SqlDataAccess sqlDataAccess = new SqlDataAccess();

            sqlDataAccess.SaveData("dbo.spInventory_Insert", item, "ARMData");
        }
    }
}
