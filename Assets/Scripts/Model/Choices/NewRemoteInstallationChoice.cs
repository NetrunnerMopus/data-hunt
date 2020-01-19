using System.Threading.Tasks;
using model.zones.corp;

namespace model.choices
{
    public class NewRemoteInstallationChoice : IRemoteInstallationChoice
    {
        private readonly zones.corp.Zones zones;

        public NewRemoteInstallationChoice(zones.corp.Zones zones)
        {
            this.zones = zones;
        }

        Task<Remote> IRemoteInstallationChoice.Choose() => Task.FromResult(zones.CreateRemote());
    }
}