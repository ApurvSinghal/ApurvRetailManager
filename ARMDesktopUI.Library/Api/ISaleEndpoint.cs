using ARMDesktopUI.Library.Models;
using System.Threading.Tasks;

namespace ARMDesktopUI.Library.Api
{
    public interface ISaleEndpoint
    {
        Task PostSale(SaleModel saleModel);
    }
}