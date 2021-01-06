using model.zones.corp;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace view.gui
{
    public class ServerRow : IRemoteObserver
    {
        private GameObject gameObject;
        private HorizontalLayoutGroup layout;
        private readonly Dictionary<IServer, ServerBox> boxesPerServer = new Dictionary<IServer, ServerBox>();
        private HashSet<IServerBoxObserver> observers = new HashSet<IServerBoxObserver>();
        private BoardParts parts;

        public ServerRow(GameObject gameObject, BoardParts parts, Zones zones)
        {
            this.gameObject = gameObject;
            this.parts = parts;
            layout = gameObject.AddComponent<HorizontalLayoutGroup>();
            layout.childControlWidth = true;
            layout.childControlHeight = true;
            layout.childForceExpandWidth = true;
            layout.childForceExpandHeight = true;
            layout.childAlignment = TextAnchor.UpperLeft;
            layout.spacing = 4;
            zones.ObserveRemotes(this);
        }

        public ServerBox Box(IServer server)
        {
            var serverObject = new GameObject(server.Zone.Name);
            var box = new ServerBox(serverObject, server, parts);
            serverObject.transform.SetParent(gameObject.transform, false);
            LayoutRebuilder.ForceRebuildLayoutImmediate(layout.GetComponent<RectTransform>());
            boxesPerServer[server] = box;
            return box;
        }

        public void Observe(IServerBoxObserver observer)
        {
            observers.Add(observer);
            foreach (var box in boxesPerServer.Values)
            {
                observer.NotifyServerBox(box);
            }
        }

        void IRemoteObserver.NotifyRemoteExists(Remote remote)
        {
            var box = Box(remote);
            foreach (var observer in observers)
            {
                observer.NotifyServerBox(box);
            }
            remote.Zone.ObserveAdditions(box);
            remote.Zone.ObserveRemovals(box);
        }

        void IRemoteObserver.NotifyRemoteDisappeared(Remote remote)
        {
            var box = boxesPerServer[remote];
            foreach (var observer in observers)
            {
                observer.NotifyServerBoxDisappeared(box);
            }
            remote.Zone.UnobserveAdditions(box);
            remote.Zone.UnobserveRemovals(box);
            Object.Destroy(box.box);
        }
    }

    public interface IServerBoxObserver
    {
        void NotifyServerBox(ServerBox box);
        void NotifyServerBoxDisappeared(ServerBox box);
    }
}
