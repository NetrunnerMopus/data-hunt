using System.Threading.Tasks;
using model.zones.corp;

namespace model.choices
{
    public interface IRemoteInstallationChoice
    {
        Task<Remote> Choose();
    }
}