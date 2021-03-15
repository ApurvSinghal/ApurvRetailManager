using ARMDesktopUI.Models;
using System.Threading.Tasks;

namespace ARMDesktopUI.Helpers
{
    public interface IAPIHelper
    {
        Task<AuthenticatedUser> Authenticate(string userName, string password);
    }
}