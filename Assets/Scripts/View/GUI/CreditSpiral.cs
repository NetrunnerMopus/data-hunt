using System.Collections.Generic;
using System.Linq;
using model;
using UnityEngine;
using UnityEngine.UI;

namespace view.gui
{
    public class CreditSpiral : MonoBehaviour, IBalanceObserver, ILayoutGroup
    {
        private GameObject creditPrefab;
        private IList<RectTransform> credits = new List<RectTransform>();
        private Spiral spiral = new Spiral();
        private RectTransform scaler;
        // Allocated once to avoid reallocations during gameplay
        private Vector3[] corners = new Vector3[4];

        void Awake()
        {
            creditPrefab = Resources.Load("Prefabs/Credit") as GameObject;
            scaler = new GameObject("Scale control").AddComponent<RectTransform>();
            scaler.SetParent(transform);
            scaler.anchorMin = Vector2.zero;
            scaler.anchorMax = Vector2.one;
            scaler.offsetMin = Vector2.zero;
            scaler.offsetMax = Vector2.zero;
            HackInAutoLayout();
        }

        private void HackInAutoLayout()
        {
            var spy = scaler.gameObject.AddComponent<Image>();
            spy.raycastTarget = false;
            spy.maskable = false;
            spy.color = new Color(0, 0, 0, 0);
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
            Rescale();
        }

        private void AppendHex()
        {
            var hex = Instantiate(creditPrefab, scaler, false);
            var rect = hex.GetComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.50f, 0.50f);
            rect.anchorMax = new Vector2(0.50f, 0.50f);
            rect.anchoredPosition = spiral.PickVector(credits.Count + 1) * 32.0f;
            credits.Add(rect);
        }

        private void Rescale()
        {
            if (credits.Count == 0)
            {
                return;
            }
            var container = Encapsulate(GetComponent<RectTransform>());
            var preScaledContent = Encapsulate(credits.ToArray());
            var xFactor = container.size.x / preScaledContent.size.x;
            var yFactor = container.size.y / preScaledContent.size.y;
            var fitFactor = Mathf.Min(xFactor, yFactor);
            if (float.IsNaN(fitFactor) || float.IsInfinity(fitFactor) || fitFactor == 0.0f)
            {
                return;
            }
            scaler.localScale = Vector3.Scale(scaler.localScale, new Vector3(fitFactor, fitFactor, fitFactor));
            var postScaledContent = Encapsulate(credits.ToArray());
            scaler.position += container.center - postScaledContent.center;
        }

        private Bounds Encapsulate(params RectTransform[] rects)
        {
            var bounds = new Bounds(rects[0].position, Vector3.zero);
            foreach (var rect in rects)
            {
                rect.GetWorldCorners(corners);
                foreach (var corner in corners)
                {
                    bounds.Encapsulate(corner);
                }
            }
            return bounds;
        }

        private void DropHex()
        {
            var last = credits.Last();
            credits.Remove(last);
            UnityEngine.Object.Destroy(last.gameObject);
        }

        void ILayoutController.SetLayoutHorizontal()
        {
            Rescale();
        }

        void ILayoutController.SetLayoutVertical()
        {
            Rescale();
        }
    }
}
