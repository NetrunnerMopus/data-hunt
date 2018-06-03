using System.Collections.Generic;

namespace model.zones.corp
{
    public class Zones
    {
        public readonly Headquarters hq;
        public readonly ResearchAndDevelopment rd;
        public readonly Archives archives;
        internal readonly List<Remote> remotes = new List<Remote>();
        private HashSet<IServerCreationObserver> creationObservers = new HashSet<IServerCreationObserver>();

        public Zones(Headquarters hq, ResearchAndDevelopment rd, Archives archives)
        {
            this.hq = hq;
            this.rd = rd;
            this.archives = archives;
        }

        internal Remote CreateRemote()
        {
            var remote = new Remote();
            remotes.Add(remote);
            foreach (var observer in creationObservers)
            {
                observer.NotifyRemoteCreated(remote);
            }
            return remote;
        }

        internal void ObserveServerCreation(IServerCreationObserver observer)
        {
            creationObservers.Add(observer);
        }
    }

    internal interface IServerCreationObserver
    {
        void NotifyRemoteCreated(Remote remote);
    }
}
