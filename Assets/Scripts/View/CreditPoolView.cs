using UnityEngine;
using UnityEngine.UI;

namespace view
{
    public class CreditPoolView : MonoBehaviour
    {
        public void UpdateBalance(int newBalance)
        {
            GetComponent<Text>().text = newBalance + " credits";
        }
    }
}