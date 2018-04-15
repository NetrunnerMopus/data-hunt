using UnityEngine;
using UnityEngine.UI;

namespace view.gui
{
    public class CreditPoolText : MonoBehaviour, ICreditPoolView
    {
        void ICreditPoolView.UpdateBalance(int newBalance)
        {
            GetComponent<Text>().text = newBalance + " credits";
        }
    }
}