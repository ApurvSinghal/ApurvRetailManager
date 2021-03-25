using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARMDataManager.Library
{
    public class ConfigHelper
    {
        //TODO: move this to Config from the API
        public static decimal GetTaxRate()
        {

            string rateText = ConfigurationManager.AppSettings["taxRate"];

            bool isValidTaxRate = Decimal.TryParse(rateText, out decimal output);

            if (isValidTaxRate == false)
            {
                throw new ConfigurationErrorsException("The tax rate is not properly set up");
            }

            return output;
        }
    }
}
