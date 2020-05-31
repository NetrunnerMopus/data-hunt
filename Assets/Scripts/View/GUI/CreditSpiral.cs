using System.Collections.Generic;
using System.Linq;
using model;
using UnityEngine;

namespace view.gui
{
    public class CreditSpiral : MonoBehaviour, IBalanceObserver
    {
        private GameObject creditPrefab;
        private IList<GameObject> credits = new List<GameObject>();
        private Spiral spiral = new Spiral();

        void Awake()
        {
            creditPrefab = Resources.Load("Prefabs/Credit") as GameObject;
        }

        void IBalanceObserver.NotifyBalance(int balance)
        {
            var diff = balance - credits.Count;
            if (diff > 0)
            {
                for (int i = 0; i < diff; i++)
                {
                    AppendHex();
                }
            }
            else
            {
                for (int i = 0; i < -diff; i++)
                {
                    DropHex();
                }
            }
        }

        private void AppendHex()
        {
            var hex = Instantiate(creditPrefab, transform, false);
            var rect = hex.GetComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.50f, 0.50f);
            rect.anchorMax = new Vector2(0.50f, 0.50f);
            rect.anchoredPosition = spiral.PickVector(credits.Count + 1) * 32.0f;
            credits.Add(hex);
        }

        private void DropHex()
        {
            var last = credits.Last();
            credits.Remove(last);
            UnityEngine.Object.Destroy(last);
        }
    }
}
