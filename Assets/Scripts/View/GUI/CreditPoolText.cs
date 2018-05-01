using model;
using UnityEngine;
using UnityEngine.UI;

namespace view.gui
{
    public class CreditPoolText : MonoBehaviour, IBalanceObserver
    {
        void IBalanceObserver.NotifyBalance(int balance)
        {
            GetComponent<Text>().text = balance + " credits";
        }
    }
}