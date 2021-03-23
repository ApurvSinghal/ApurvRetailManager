using ARMDesktopUI.Library.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ARMDesktopUI.Library.Api
{
    public interface IProductEndpoint
    {
        Task<List<ProductModel>> GetAll();
    }
}