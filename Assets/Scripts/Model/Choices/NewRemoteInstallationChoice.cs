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

        Remote IRemoteInstallationChoice.Choose() => zones.CreateRemote();
    }
}