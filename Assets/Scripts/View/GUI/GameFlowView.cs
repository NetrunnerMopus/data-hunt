using UnityEngine;
using model;
using model.timing;

namespace view.gui
{
    public class GameFlowView
    {
        private readonly float clickRowHeightRatio = 0.20f;
        private GameObject flow;
        public PaidWindowView PaidWindow { get; private set; }

        public void Display(GameObject board, Game game)
        {
            flow = CreateFlow();
            PaidWindow = CreatePaidWindow(game.runner.paidWindow);
            var gameFinish = CreateGameFinish(game);
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

        private PaidWindowView CreatePaidWindow(PaidWindow window)
        {
            var view = new GameObject("Paid window");
            var rectangle = view.AddComponent<RectTransform>();
            rectangle.anchorMin = new Vector2(0.0f, clickRowHeightRatio);
            rectangle.anchorMax = new Vector2(1.0f, 1.0f - clickRowHeightRatio);
            rectangle.offsetMin = Vector2.zero;
            rectangle.offsetMax = Vector2.zero;
            view.AttachTo(flow);
            return new PaidWindowView(view, rectangle, window);
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
