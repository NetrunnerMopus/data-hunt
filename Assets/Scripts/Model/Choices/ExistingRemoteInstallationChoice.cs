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

        Remote IRemoteInstallationChoice.Choose() => remote;
    }
}