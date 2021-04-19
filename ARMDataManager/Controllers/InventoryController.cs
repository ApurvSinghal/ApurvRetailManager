using ARMDataManager.Library.DataAccess;
using ARMDataManager.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ARMDataManager.Controllers
{
    [Authorize]
    public class InventoryController : ApiController
    {
        public List<InventoryModel> Get()
        {
            InventoryData inventoryData = new InventoryData();
            return inventoryData.GetInventory();
        }

        public void Post(InventoryModel item)
        {
            InventoryData inventoryData = new InventoryData();
            inventoryData.SaveInventoryRecord(item);
        }
    }
}
