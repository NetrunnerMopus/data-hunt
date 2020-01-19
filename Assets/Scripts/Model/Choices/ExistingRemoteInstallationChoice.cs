using System.Threading.Tasks;
using model.zones.corp;

namespace model.choices
{
    public class ExistingRemoteInstallationChoice : IRemoteInstallationChoice
    {
        private readonly Remote remote;

        public ExistingRemoteInstallationChoice(Remote remote)
        {
            this.remote = remote;
        }

        Task<Remote> IRemoteInstallationChoice.Choose() => Task.FromResult(remote);
    }
}