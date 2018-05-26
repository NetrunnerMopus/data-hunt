using UnityEngine;

namespace view.gui
{
    public class ServerRow : MonoBehaviour
    {
        internal Server CreateServer(string serverName)
        {
            var gameObject = new GameObject(serverName);
            gameObject.transform.parent = transform;
            return gameObject.AddComponent<Server>();
        }
    }
}