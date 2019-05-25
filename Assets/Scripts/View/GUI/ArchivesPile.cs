using UnityEngine;
using model.zones.corp;
using model.cards;
using model.zones;

namespace view.gui
{
    public class ArchivesPile : MonoBehaviour, IZoneAdditionObserver
    {
        void Start()
        {
            gameObject.AddComponent<CardPrinter>();
        }

        void IZoneAdditionObserver.NotifyCardAdded(Card card)
        {
            var printer = GetComponent<CardPrinter>();
            if (card.Faceup)
            {
                printer.Print(card);
            }
            else
            {
                var facedown = printer.PrintCorpFacedown("Facedown in Archives");
                facedown.transform.rotation *= Quaternion.Euler(0.0f, 0.0f, 90.0f);
            }
        }
    }
}