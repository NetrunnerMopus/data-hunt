using model.zones.corp;

namespace model.choices
{
    public class NewRemoteInstallationChoice : IRemoteInstallationChoice
    {
        private readonly Zones zones;

        public NewRemoteInstallationChoice(Zones zones)
        {
            this.zones = zones;
        }

        Remote IRemoteInstallationChoice.Choose() => zones.CreateRemote();
    }
}