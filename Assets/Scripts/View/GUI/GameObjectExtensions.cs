using UnityEngine;

namespace view.gui
{
    public static class GameObjectExtensions
    {
        public static void AttachTo(this GameObject child, GameObject parent)
        {
            child.transform.SetParent(parent.transform, false);
        }
    }
}