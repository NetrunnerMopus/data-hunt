using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace view.gui
{
    public class Server : MonoBehaviour
    {
        internal CardPrinter Printer { get; private set; }

        void Awake()
        {
            var image = gameObject.AddComponent<Image>();
            image.sprite = AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/Background.psd");
            image.type = Image.Type.Sliced;
            image.fillCenter = false;
            Printer = gameObject.AddComponent<CardPrinter>();
        }
    }
}