using model;

namespace tests.mocks
{
    internal class PassiveCorp
    {
        private Game game;

        public PassiveCorp(Game game)
        {
            this.game = game;
        }

        internal void SkipTurn()
        {
            var clickForCredit = game.corp.actionCard.credit;
            for (int i = 0; i < 3; i++)
            {
                clickForCredit.Trigger(game);
            }
            DiscardRandomCards();
        }

        internal void SkipTurnIgnoringWindows()
        {
            var paidWindow = game.flow.paidWindow;
            paidWindow.Pass();
            paidWindow.Pass();
            var clickForCredit = game.corp.actionCard.credit;
            for (int i = 0; i < 3; i++)
            {
                clickForCredit.Trigger(game);
                paidWindow.Pass();
            }
            DiscardRandomCards();
        }

        internal void DiscardRandomCards()
        {
            var hq = game.corp.zones.hq;
            hq.Discard(hq.Random(), game.corp.zones.archives);
        }
    }
}
