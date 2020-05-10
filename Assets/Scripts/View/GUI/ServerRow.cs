using model.zones.corp;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace view.gui
{
    public class ServerRow : MonoBehaviour, IRemoteObserver
    {
        private HorizontalLayoutGroup layout;
        private readonly Dictionary<IServer, ServerBox> boxesPerServer = new Dictionary<IServer, ServerBox>();
        private HashSet<IServerBoxObserver> observers = new HashSet<IServerBoxObserver>();

        public void Represent(Zones zones)
        {
            zones.ObserveRemotes(this);
        }

        void Awake()
        {
            layout = gameObject.AddComponent<HorizontalLayoutGroup>();
            layout.childControlWidth = true;
            layout.childControlHeight = true;
            layout.childForceExpandWidth = true;
            layout.childForceExpandHeight = true;
            layout.childAlignment = TextAnchor.MiddleRight;
            layout.spacing = 4;
        }

        public ServerBox Box(IServer server)
        {
            var gameObject = new GameObject(server.Zone.Name);
            var box = new ServerBox(gameObject, server);
            gameObject.transform.SetParent(transform, false);
            gameObject.transform.SetAsFirstSibling();
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
            Destroy(box.gameObject);
        }
    }

    public interface IServerBoxObserver
    {
        void NotifyServerBox(ServerBox box);
        void NotifyServerBoxDisappeared(ServerBox box);
    }
}