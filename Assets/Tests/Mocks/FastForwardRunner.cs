using model;

namespace tests.mocks
{
    public class FastForwardRunner
    {
        private Game game;

        public FastForwardRunner(Game game)
        {
            this.game = game;
        }

        public void FastForwardToActionPhase()
        {
            SkipPaidWindow();
            SkipPaidWindow();
        }

        public void SkipPaidWindow()
        {
            game.runner.paidWindow.Pass();
            game.corp.paidWindow.Pass();
        }
    }
}
