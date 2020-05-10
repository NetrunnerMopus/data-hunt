using System.Collections.Generic;

namespace model.zones.corp
{
    public class Zones
    {
        private Game game;
        public readonly Headquarters hq;
        public readonly ResearchAndDevelopment rd;
        public readonly Archives archives;
        public readonly List<Remote> remotes = new List<Remote>();
        private HashSet<IRemoteObserver> remoteObservers = new HashSet<IRemoteObserver>();

        public Zones(Headquarters hq, ResearchAndDevelopment rd, Archives archives, Game game)
        {
            this.hq = hq;
            this.rd = rd;
            this.archives = archives;
            this.game = game;
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
            var remote = new Remote(game);
            remotes.Add(remote);
            foreach (var observer in remoteObservers)
            {
                observer.NotifyRemoteExists(remote);
            }
            return remote;
        }

        public void RemoveRemote(Remote remote) {
            remotes.Remove(remote);
            foreach (var observer in remoteObservers)
            {
                observer.NotifyRemoteDisappeared(remote);
            }
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
        void NotifyRemoteDisappeared(Remote remote);
    }
}
