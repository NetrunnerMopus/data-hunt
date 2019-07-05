using System.Collections.Generic;

namespace model.zones.corp
{
    public class Zones
    {
        public readonly Headquarters hq;
        public readonly ResearchAndDevelopment rd;
        public readonly Archives archives;
        public readonly List<Remote> remotes = new List<Remote>();
        private HashSet<IRemoteObserver> remoteObservers = new HashSet<IRemoteObserver>();

        public Zones(Headquarters hq, ResearchAndDevelopment rd, Archives archives)
        {
            this.hq = hq;
            this.rd = rd;
            this.archives = archives;
        }

        public List<IInstallDestination> RemoteInstalls()
        {
            return new List<IInstallDestination>(remotes)
            {
                new NewRemote(this)
            };
        }

        public Remote CreateRemote()
        {
            var remote = new Remote(archives.Zone);
            remotes.Add(remote);
            foreach (var observer in remoteObservers)
            {
                observer.NotifyRemoteExists(remote);
            }
            return remote;
        }

        public void ObserveRemotes(IRemoteObserver observer)
        {
            remoteObservers.Add(observer);
            foreach (var remote in remotes)
            {
                observer.NotifyRemoteExists(remote);
            }
        }
    }

    public interface IRemoteObserver
    {
        void NotifyRemoteExists(Remote remote);
    }
}
