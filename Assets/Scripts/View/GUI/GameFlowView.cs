using UnityEngine;
using model;
using model.timing;
using UnityEngine.UI;

namespace view.gui
{
    public class GameFlowView
    {
        private readonly float clickRowHeightRatio = 0.30f;
        public PaidWindowView PaidWindow { get; private set; }

        public void Display(GameObject board, Game game)
        {
            var flow = CreateFlow();
            var corpPool = CreateCorpClicks(game);
            var runnerPool = CreateRunnerClicks(game);
            PaidWindow = CreatePaidWindow(game.runner.paidWindow);
            var gameFinish = CreateGameFinish(game);
            corpPool.AttachTo(flow);
            runnerPool.AttachTo(flow);
            PaidWindow.gameObject.AttachTo(flow);
            flow.AttachTo(board);
            gameFinish.AttachTo(board);
        }

        private GameObject CreateFlow()
        {
            var view = new GameObject("Game flow");
            var rectangle = view.AddComponent<RectTransform>();
            rectangle.anchorMin = new Vector2(0.30f, 0.40f);
            rectangle.anchorMax = new Vector2(0.70f, 0.60f);
            rectangle.offsetMin = Vector2.zero;
            rectangle.offsetMax = Vector2.zero;
            return view;
        }

        private GameObject CreateCorpClicks(Game game)
        {
            var view = new GameObject("Corp clicks");
            var rectangle = view.AddComponent<RectTransform>();
            rectangle.anchorMin = new Vector2(0.0f, 1.0f - clickRowHeightRatio);
            rectangle.anchorMax = new Vector2(1.0f, 1.0f);
            rectangle.offsetMin = Vector2.zero;
            rectangle.offsetMax = Vector2.zero;
            AddHorizontalLayout(view);
            game.corp.clicks.Observe(view.AddComponent<ClickPoolRow>());
            return view;
        }

        private GameObject CreateRunnerClicks(Game game)
        {
            var view = new GameObject("Runner clicks");
            var rectangle = view.AddComponent<RectTransform>();
            rectangle.anchorMin = new Vector2(0.0f, 0.0f);
            rectangle.anchorMax = new Vector2(1.0f, clickRowHeightRatio);
            rectangle.offsetMin = Vector2.zero;
            rectangle.offsetMax = Vector2.zero;
            AddHorizontalLayout(view);
            game.runner.clicks.Observe(view.AddComponent<ClickPoolRow>());
            return view;
        }

        private void AddHorizontalLayout(GameObject gameObject)
        {
            var layout = gameObject.AddComponent<HorizontalLayoutGroup>();
            layout.childForceExpandWidth = true;
            layout.childForceExpandHeight = true;
            layout.childControlWidth = true;
            layout.childControlHeight = true;
            layout.childAlignment = TextAnchor.MiddleLeft;
        }

        private PaidWindowView CreatePaidWindow(PaidWindow window)
        {
            var view = new GameObject("Paid window");
            var rectangle = view.AddComponent<RectTransform>();
            rectangle.anchorMin = new Vector2(0.0f, clickRowHeightRatio);
            rectangle.anchorMax = new Vector2(1.0f, 1.0f - clickRowHeightRatio);
            rectangle.offsetMin = Vector2.zero;
            rectangle.offsetMax = Vector2.zero;
            return view.AddComponent<PaidWindowView>().Represent(window);
        }

        private GameObject CreateGameFinish(Game game)
        {
            var view = new GameObject("Game finish");
            var rectangle = view.AddComponent<RectTransform>();
            rectangle.anchorMin = new Vector2(0.30f, 0.30f);
            rectangle.anchorMax = new Vector2(0.70f, 0.70f);
            rectangle.offsetMin = Vector2.zero;
            rectangle.offsetMax = Vector2.zero;
            game.ObserveFinish(view.AddComponent<GameFinishPanel>());
            return view;
        }
    }
}