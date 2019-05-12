using model;

namespace tests.mocks
{
    public class PassiveCorp
    {
        private Game game;

        public PassiveCorp(Game game)
        {
            this.game = game;
        }

        public void SkipTurn()
        {
            SkipPaidWindow();
            var clickForCredit = game.corp.actionCard.credit;
            SkipPaidWindow();
            for (int i = 0; i < 3; i++)
            {
                clickForCredit.Trigger(game);
                SkipPaidWindow();
            }
            DiscardRandomCards();
            SkipPaidWindow();
        }

        private void SkipPaidWindow()
        {
            game.corp.paidWindow.Pass();
            game.runner.paidWindow.Pass();
        }

        public void DiscardRandomCards()
        {
            var hq = game.corp.zones.hq;
            hq.Discard(hq.Random(), game.corp.zones.archives);
        }
    }
}
